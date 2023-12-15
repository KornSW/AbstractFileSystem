Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Text
Imports System.Net.Sockets
Imports System.IO
Imports System.Threading

Public Class FtpServer
  Implements IDisposable

  Private _Log As ILog = LogManager.GetLogger(GetType(FtpServer))

  Private _Disposed As Boolean = False
  Private _Listening As Boolean = False

  Private _Listener As TcpListener
  Private _ActiveConnections As List(Of ClientConnection)

  Private _localEndPoint As IPEndPoint

  Private _UserManager As IUserManager
  Private _FileSystemProviderFactory As FileSystemProviderFactory

  Public Delegate Function FileSystemProviderFactory() As IFileSystemProvider

  Public Sub New(userManager As IUserManager, fileSystemProviderFactory As FileSystemProviderFactory)
    Me.New(userManager, fileSystemProviderFactory, IPAddress.Any, 21)
  End Sub

  Public Sub New(userManager As IUserManager, fileSystemProviderFactory As FileSystemProviderFactory, ipAddress As IPAddress, port As Integer)
    _UserManager = userManager
    _FileSystemProviderFactory = fileSystemProviderFactory
    _localEndPoint = New IPEndPoint(ipAddress, port)
  End Sub

  Public Sub Start()
    _Listener = New TcpListener(_localEndPoint)

    _Log.Info("#Version: 1.0")
    _Log.Info("#Fields: date time c-ip c-port cs-username cs-method cs-uri-stem sc-status sc-bytes cs-bytes s-name s-port")

    _Listening = True
    _Listener.Start()

    _ActiveConnections = New List(Of ClientConnection)()

    _Listener.BeginAcceptTcpClient(AddressOf HandleAcceptTcpClient, _Listener)
  End Sub

  Public Sub [Stop]()
    _Log.Info("Stopping FtpServer")

    _Listening = False
    _Listener.[Stop]()

    _Listener = Nothing
  End Sub

  Private Sub HandleAcceptTcpClient(result As IAsyncResult)
    If _Listening Then
      _Listener.BeginAcceptTcpClient(AddressOf HandleAcceptTcpClient, _Listener)

      Dim client As TcpClient = _Listener.EndAcceptTcpClient(result)

      Dim connection As New ClientConnection(client, _FileSystemProviderFactory.Invoke())

      _ActiveConnections.Add(connection)

      ThreadPool.QueueUserWorkItem(AddressOf connection.HandleClient, client)
    End If
  End Sub

  Public Sub Dispose() Implements System.IDisposable.Dispose
    Dispose(True)
  End Sub

  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not _Disposed Then
      If disposing Then
        [Stop]()

        For Each conn As ClientConnection In _ActiveConnections
          conn.Dispose()
        Next
      End If
    End If

    _Disposed = True
  End Sub

End Class
