Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Security
Imports System.Security.AccessControl
Imports System.Security.Cryptography
Imports System.Security.Principal
Imports System.Text
Imports System.Web
Imports KSW.Base
Imports KSW.Base.Shell.Web
Imports KSW.Base.Ui.Web

Public Class WebbridgeServer
  Inherits KSW.Base.Shell.Web.WebApplication

  Private Shared assemblySourceDir As DirectoryInfo

  Shared Sub New()

    'the Webserver has another workdir (someth. like: C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\vs\aaee8600\1bbfb413\assembly\dl3\0e496470\00700ef0_2458d101\)
    'but this assembly here will be loaded via reflection, so lets evaluate our loaction,
    'because this is the real one where were want to be running from
    Dim assemblyLoc As String = Assembly.GetExecutingAssembly().Location
    assemblySourceDir = New DirectoryInfo(IO.Path.GetDirectoryName(assemblyLoc))

    'the .net framework should load assemblies also from here
    AppDomain.CurrentDomain.AddAssemblyResolver(assemblySourceDir, True)

    'With InjectedTypes.AssemblyMatchingRules
    '  .AddExcludeMasks("*UI_WinForms*")
    '  .AddExcludeMasks("*UI.WinForms*")
    '  .AddExcludeMasks("*WinViews*")
    '  .AddExcludeMasks("Microsoft*")
    '  .AddExcludeMasks("System*")
    'End With

    'InjectedTypes.InjectAssemblies(assemblySourceDir, True, "Adapters.*.dll")
    'InjectedTypes.InjectAssemblies(assemblySourceDir, True, "*KSW*.dll")
    'InjectedTypes.InjectAssemblies(assemblySourceDir, True, "*plugin*.dll")

  End Sub

  Public Sub New()
    MyBase.New()
    _Secret.AppendString(My.Settings.SharedSecret)
  End Sub

  Private _Secret As New SecureString

  Const fakeHelloMessage As String = "secure gateway to EWS Managed API - service not available for browsers"

  Protected Overrides Sub Process(request As IWebRequest, response As IWebResponse, state As IWebSessionState)

    If (request.PostData.Keys.Count < 2) Then
      response.ContentWriter.WriteLine(fakeHelloMessage)
      Exit Sub
    End If

    Dim oneTimeKey As New SecureString
    oneTimeKey.AppendString((_Secret.GetMD5Hash() & request.PostData("Hash") & "!").MD5())

    Dim transportWrapper As New ObjectTransportWrapper(oneTimeKey)

    Dim args As Object()
    Dim result As Object = Nothing
    Try
      Dim raw As String = request.PostData("CompressedItemData")
      args = transportWrapper.UnWrapFromString(Of Object())(raw)
    Catch ex As Exception
      response.ContentWriter.WriteLine("BAD REQUEST")
      Exit Sub
    End Try

    Dim internalService = state.GetItem(Of BridgedAfsSession)()
    Dim impersonation = state.GetItem(Of ImpersonationInfo)()
    Try
      Dim methodName As String = args(0).ToString()

      Select Case methodName
        Case "Logon"
          With impersonation
            .Domain = "."
            .UserName = DirectCast(args(1), String)
            If (.UserName.Contains("\")) Then
              .Domain = .UserName.Substring(0, .UserName.IndexOf("\"))
              .UserName = .UserName.Substring(.UserName.IndexOf("\") + 1)
            End If
            .Password.DecryptFrom(DirectCast(args(2), String), _Secret)
            WindowsIdentityImpersonator.OpenScope(impersonation, True)
            result = internalService.InitializeUserProfile(.UserName)
          End With
        Case "Logoff"
          impersonation.UserName = ""
          WindowsIdentityImpersonator.Reset()
          result = internalService.InitializeUserProfile(Nothing)
        Case Else
          Dim m = internalService.GetType().GetMethod(methodName)
          If (m Is Nothing) Then
            Throw New NotImplementedException
          End If

          Dim exec As Action =
            Sub()
              If (m.ReturnType IsNot Nothing) Then
                result = m.Invoke(internalService, args.Skip(1).ToArray())
              Else
                m.Invoke(internalService, args.Skip(1).ToArray())
              End If
            End Sub

          If (String.IsNullOrWhiteSpace(impersonation.UserName)) Then
            WindowsIdentityImpersonator.Reset()
            exec.Invoke()
          Else
            WindowsIdentityImpersonator.OpenScope(impersonation, True)
            exec.InvokeImpersonated(impersonation)
          End If
      End Select

    Catch ex As Exception
      result = New Exception(ex.ToFullString)
    End Try
    response.ContentWriter.Write(transportWrapper.WrapToString(result))
  End Sub

End Class
