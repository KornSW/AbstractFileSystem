Imports System
Imports System.Reflection
Imports System.ServiceProcess
Imports KSW.Base
Imports KSW.AFS
Imports KSW.AFS.Service
Imports KSW.AFS.Service.Api

#Region " (Process Entry Point) "

Friend Module Main

  <MTAThread()>
  Public Function Main(arguments() As String) As Integer
    Using applicationInstance As New Application
      Return applicationInstance.Run(arguments)
    End Using
  End Function

End Module

#End Region

Public Class Application
  Inherits ServiceApplication

#Region " Installation Config "

  Friend Shared Sub ConfigureInstallers(serviceProcessInstaller As ServiceProcessInstaller, serviceInstaller As ServiceInstaller)
    Dim a = Assembly.GetExecutingAssembly()
    With serviceInstaller
      .Description = a.GetAssemblyDescription
      .DisplayName = String.Format("{0} ({1})", a.GetAssemblyTitle, a.GetAssemblyFileVersion)
      .ServiceName = a.GetName.Name()
      .StartType = ServiceStartMode.Automatic
    End With
    With serviceProcessInstaller

      .Account = ServiceAccount.LocalSystem

      '.Account = ServiceAccount.User
      '.Username = "USER"
      '.Password = "PASS"

    End With
  End Sub

#End Region

#Region " Startup "

  Public Property Config As AfsFtpServiceConfig

  Protected Overrides Sub PrepareStartup(ByRef cancel As Boolean)
    MyBase.PrepareStartup(cancel)
    'InjectedTypes.InjectAssembliesFromWorkdir(True)
    Me.InitServices()

    Me.EnableLogFile()

    Me.RegisterThread(AddressOf Me.WorkerServiceThreadA)
    Me.RegisterThread(AddressOf Me.WorkerServiceThreadB)

  End Sub

  Private Sub InitServices()

    Me.Logging.WriteLine(LogMessageType.Debug, "Loading Configuration from 'ServiceConfig.xml'")
    Me.Config = AppConfig.Load(Of AfsFtpServiceConfig)("AfsFtpServiceConfig.xml")
    If (Me.Config Is Nothing) Then
      Me.Config = New AfsFtpServiceConfig
    End If

    Me.Logging.WriteLine(LogMessageType.Information, "ServiceStarted")

    Me.WorkerServiceA = New WorkerService With {.FileName = "C:\testoutputA.txt"}
    Me.WorkerServiceB = New WorkerService With {.FileName = "C:\testoutputB.txt"}

  End Sub

#End Region

#Region " Commands "

  'when running directly from console....
  Protected Overrides Sub HandleCommand(command As String, ByRef continueRunning As Boolean)
    Select Case command.ToLower()
      Case "boom"
        Throw New Exception("BOOM")


      Case Else
        MyBase.HandleCommand(command, continueRunning)
    End Select
  End Sub

#End Region

#Region " Worker "

  Public Property WorkerServiceA As WorkerService
  Private Sub WorkerServiceThreadA(continueRunning As Func(Of Boolean))
    Me.WorkerServiceA.Run(continueRunning, Me.Logging)
  End Sub

  Public Property WorkerServiceB As WorkerService
  Private Sub WorkerServiceThreadB(continueRunning As Func(Of Boolean))
    Me.WorkerServiceB.Run(continueRunning, Me.Logging)
  End Sub

#End Region

#Region " Shutdown "

  Protected Overrides Sub PrepareShutdown()
    Me.ShutdownServices()
    MyBase.PrepareShutdown()
  End Sub

  Private Sub ShutdownServices()
    Me.WorkerServiceA.TryDispose(True)
    Me.WorkerServiceA = Nothing
    Me.WorkerServiceB.TryDispose(True)
    Me.WorkerServiceB = Nothing
  End Sub

#End Region

End Class
