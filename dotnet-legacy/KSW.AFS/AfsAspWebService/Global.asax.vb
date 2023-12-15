Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Web.Optimization
Imports KSW.Base
Imports System.Net

Public Class Global_asax
  Inherits HttpApplication
  Implements IWebRuntimeHost
  Implements IWebSiteConfig

  'Initializes an "IWebSite" as configured in:
  '    My.Settings.AssemblyLocation
  '    My.Settings.ApplicationAssemblyName
  '    My.Settings.ApplicationTypeName

#Region " Entrypoint "

  Private Shared _Application As IWebSite = Nothing
  Private Shared _ApplicationInitialized As Boolean = False

  Shared Sub New()
    Dim workDir As New DirectoryInfo(My.Settings.AssemblyLocation)
    AppDomain.CurrentDomain.AddAssemblyResolver(workDir, True)
  End Sub

  Public Sub New()
    If (_Application Is Nothing) Then
      Dim ass = Assembly.LoadFrom(IO.Path.Combine(My.Settings.AssemblyLocation, My.Settings.ApplicationAssemblyName))
      Dim t = ass.GetType(My.Settings.ApplicationTypeName)
      _Application = DirectCast(Activator.CreateInstance(t), IWebSite)
    End If
  End Sub

  Private Function InitializeApplication() As IWebSite
    If (_Application IsNot Nothing AndAlso Not _ApplicationInitialized) Then
      _Application.Initialize(Me, Me)
      _ApplicationInitialized = True
    End If
    Return _Application
  End Function

  Sub Application_Start(sender As Object, e As EventArgs)
    Me.InitializeApplication()
  End Sub

  Protected Sub Session_Start(sender As Object, e As EventArgs)
    Me.Session("init") = 0
  End Sub

  Private Sub Global_asax_PostRequestHandlerExecute(sender As Object, e As EventArgs) Handles Me.PostRequestHandlerExecute
    Dim app = Me.InitializeApplication()

    Using ses As New AspSessionState(Me.Server, Me.Session, Me.Application, Me.Context),
          req As New AspRequest(Me.Request, ses),
          res As New AspResponse(Me.Response, ses)

      app.ProcessRequest(req, res, ses)

    End Using

  End Sub

  Private Sub Global_asax_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    Try
      If (_Application IsNot Nothing) Then
        _Application.Dispose()
        _Application = Nothing
      End If
    Catch
    End Try
  End Sub

  Public ReadOnly Property ServerRuntime As String Implements IWebRuntimeHost.ServerRuntime
    Get
      Return "ASP.NET"
    End Get
  End Property

#End Region

#Region " Abstraction "

  Private Class AspRequest
    Implements IWebRequest

    Private _Request As HttpRequest
    Private _State As AspSessionState

    Public Sub New(request As HttpRequest, state As AspSessionState)
      _Request = request
      _State = state
    End Sub

#Region " Properties "

    Public ReadOnly Property PostData As NameValueCollection Implements IWebRequest.PostData
      Get
        Return _Request.Headers
      End Get
    End Property

    Public ReadOnly Property Url As Uri Implements IWebRequest.Url
      Get
        Return _Request.Url
      End Get
    End Property

    Public ReadOnly Property ClientHostname As String Implements IWebRequest.ClientHostname
      Get
        Select Case (_Request.UserHostName)
          Case "::1" : Return "localhost"
          Case "127.0.0.1" : Return "localhost"
          Case Else
            Return _Request.UserHostName
        End Select
      End Get
    End Property

    Public ReadOnly Property ClientIpAddress As IPAddress Implements IWebRequest.ClientIpAddress
      Get
        Return IPAddress.Parse(_Request.UserHostAddress)
      End Get
    End Property

#End Region

#Region " IDisposable "

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _AlreadyDisposed As Boolean = False

    ''' <summary>
    ''' Dispose the current object instance
    ''' </summary>
    Protected Overridable Sub Dispose(disposing As Boolean)
      If (Not _AlreadyDisposed) Then
        If (disposing) Then



        End If
        _AlreadyDisposed = True
      End If
    End Sub

    ''' <summary>
    ''' Dispose the current object instance and suppress the finalizer
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
      Me.Dispose(True)
      GC.SuppressFinalize(Me)
    End Sub

#End Region

  End Class

  Private Class AspResponse
    Implements IWebResponse

    Private _Response As HttpResponse
    Private _State As AspSessionState
    Private _Writer As StreamWriter = Nothing

    Public Sub New(response As HttpResponse, state As AspSessionState)
      _Response = response
      _State = state
    End Sub

    Public Property ContentMimeType As String Implements IWebResponse.ContentMimeType




    Public ReadOnly Property ContentWriter As StreamWriter Implements IWebResponse.ContentWriter
      Get
        If (_Writer Is Nothing) Then
          _Writer = New StreamWriter(_Response.OutputStream)
          _Writer.AutoFlush = True
        End If
        Return _Writer
      End Get
    End Property

#Region " IDisposable "

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _AlreadyDisposed As Boolean = False

    ''' <summary>
    ''' Dispose the current object instance
    ''' </summary>
    Protected Overridable Sub Dispose(disposing As Boolean)
      If (Not _AlreadyDisposed) Then
        If (disposing) Then
          If (_Writer IsNot Nothing) Then
            _Writer.Flush()
            _Writer.Dispose()
            _Writer = Nothing
          End If
        End If
        _AlreadyDisposed = True
      End If
    End Sub

    ''' <summary>
    ''' Dispose the current object instance and suppress the finalizer
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
      Me.Dispose(True)
      GC.SuppressFinalize(Me)
    End Sub

#End Region

  End Class

  Private Class AspSessionState
    Implements IWebSessionState

    Private _Server As HttpServerUtility
    Private _SessionState As HttpSessionState
    Private _AppState As HttpApplicationState
    Private _Context As HttpContext

    Public Sub New(server As HttpServerUtility, sessionState As HttpSessionState, appState As HttpApplicationState, context As HttpContext)
      _Server = server
      _SessionState = sessionState
      _AppState = appState
      _Context = context
    End Sub

    Friend ReadOnly Property Server As HttpServerUtility
      Get
        Return _Server
      End Get
    End Property

    Friend ReadOnly Property Application As HttpApplicationState
      Get
        Return _AppState
      End Get
    End Property

    Friend ReadOnly Property Context As HttpContext
      Get
        Return _Context
      End Get
    End Property

    Public ReadOnly Property SessionId As String Implements IWebSessionState.SessionId
      Get
        Return _SessionState.SessionID
      End Get
    End Property

    Public Function GetItem(Of TStateItem As {New, IDisposable})() As TStateItem Implements IWebSessionState.GetItem
      Dim typeName As String = GetType(TStateItem).FullName
      Dim stateItem As TStateItem
      stateItem = DirectCast(_SessionState.Item(typeName), TStateItem)

      If (stateItem Is Nothing) Then
        stateItem = New TStateItem
        _SessionState.Item(typeName) = stateItem
      End If

      Return stateItem
    End Function

#Region " IDisposable "

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _AlreadyDisposed As Boolean = False

    ''' <summary>
    ''' Dispose the current object instance
    ''' </summary>
    Protected Overridable Sub Dispose(disposing As Boolean)
      If (Not _AlreadyDisposed) Then
        If (disposing) Then



        End If
        _AlreadyDisposed = True
      End If
    End Sub

    ''' <summary>
    ''' Dispose the current object instance and suppress the finalizer
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
      Me.Dispose(True)
      GC.SuppressFinalize(Me)
    End Sub

#End Region

  End Class

#End Region

End Class
