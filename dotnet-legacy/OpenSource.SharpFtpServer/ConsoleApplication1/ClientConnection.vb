Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports System.Net.Sockets
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Public Class ClientConnection
  Implements IDisposable

#Region " Declarations "

  'Common

  Private _Log As ILog = LogManager.GetLogger(GetType(ClientConnection))
  Private _IsDisposed As Boolean = False

  'Communication

  Private _DataEndpoint As IPEndPoint
  Private _RemoteEndPoint As IPEndPoint
  Private _PassiveListener As TcpListener
  Private _ControlClient As TcpClient
  Private _DataClient As TcpClient
  Private _ControlStream As NetworkStream
  Private _ControlReader As StreamReader
  Private _ControlWriter As StreamWriter
  Private _ClientIp As String

  'State

  Private _CurrentTransferType As TransferType = TransferType.Ascii
  Private _CurrentFormatControlType As FormatControlType = FormatControlType.NonPrint
  Private _DataConnectionType As DataConnectionType = DataConnectionType.Active
  Private _FileStructureType As FileStructureType = FileStructureType.File

  'Encryption

  Private _SslCert As X509Certificate = Nothing
  Private _SslStream As SslStream

  'Identity

  Private _CurrentUser As User
  Private _CurrentUserName As String

  'Processing

  Private _ValidCommands As List(Of String)
  Private _CommandProcessor As FtpCommandProcessor
  Private _BaseCommandHandler As FtpBaseCommandHandler
  Private _FilesystemCommandHandler As FtpFilesystemCommandHandler
  Private _DmsCommandHandler As FtpDmsCommandHandler

#End Region

#Region " Constuctor "

  Public Sub New(client As TcpClient, fileSystem As IFileSystemProvider)
    _ControlClient = client

    _BaseCommandHandler = New FtpBaseCommandHandler(Me)
    _FilesystemCommandHandler = New FtpFilesystemCommandHandler(Me, fileSystem)
    _DmsCommandHandler = New FtpDmsCommandHandler(Me)

    _CommandProcessor = New FtpCommandProcessor(_BaseCommandHandler, _FilesystemCommandHandler, _DmsCommandHandler)

    _ValidCommands = New List(Of String)()
  End Sub

#End Region

#Region " Properties "

  Public ReadOnly Property DataEndpoint As IPEndPoint
    Get
      Return _DataEndpoint
    End Get
  End Property

  Public ReadOnly Property PassiveListener As TcpListener
    Get
      Return _PassiveListener
    End Get
  End Property

  Public ReadOnly Property ControlWriter As StreamWriter
    Get
      Return _ControlWriter
    End Get
  End Property

  Public ReadOnly Property CurrentUser As User
    Get
      Return _CurrentUser
    End Get
  End Property

#End Region

#Region " Main Command Handler "

  Public Sub HandleClient(obj As Object)

    Dim line As String = String.Empty

    _RemoteEndPoint = DirectCast(_ControlClient.Client.RemoteEndPoint, IPEndPoint)
    _ClientIp = _RemoteEndPoint.Address.ToString()
    _ControlStream = _ControlClient.GetStream()
    _ControlReader = New StreamReader(_ControlStream)
    _ControlWriter = New StreamWriter(_ControlStream)
    _ControlWriter.WriteLine(FtpMessages.GetMessage(FtpCommunicationMessage.ServiceReady))
    _ControlWriter.Flush()
    _DataClient = New TcpClient()

    _ValidCommands.AddRange(New String() {"AUTH", "USER", "PASS", "QUIT", "HELP", "NOOP"})

    Try
      While (InlineAssignHelper(line, _ControlReader.ReadLine())) IsNot Nothing

        Dim response As String = Nothing
        Dim command As String() = line.Split(" "c)
        Dim cmd As String = command(0).ToUpperInvariant()
        Dim arguments As String = If(command.Length > 1, line.Substring(command(0).Length + 1), Nothing)

        If (arguments IsNot Nothing AndAlso arguments.Trim().Length = 0) Then
          arguments = Nothing
        End If

        Dim logEntry As New LogEntry() With { _
         .[Date] = DateTime.Now, _
         .CIP = _ClientIp, _
         .CSUriStem = arguments _
        }

        If (Not _ValidCommands.Contains(cmd)) Then
          If (_CurrentUser Is Nothing) Then
            response = FtpMessages.GetMessage(FtpCommunicationMessage.NotLoggedIn)
          Else
            response = Nothing
          End If
        End If

        If (response Is Nothing) Then
          response = _CommandProcessor.ProcessFtpCommand(cmd, arguments, logEntry)
        End If

        If (response Is Nothing) Then
          response = FtpMessages.GetMessage(FtpCommunicationMessage.UnknownCommand)
        End If

        logEntry.CSMethod = cmd
        logEntry.CSUsername = _CurrentUserName
        logEntry.SCStatus = response.Substring(0, response.IndexOf(" "c))

        _Log.Info(logEntry)

        If (_ControlClient Is Nothing OrElse Not _ControlClient.Connected) Then
          Exit While
        Else
          _ControlWriter.WriteLine(response)
          _ControlWriter.Flush()

          If (response.StartsWith("221")) Then
            Exit While
          End If

          If (cmd = "AUTH") Then
            _SslCert = New X509Certificate("server.cer")
            _SslStream = New SslStream(_ControlStream)
            _SslStream.AuthenticateAsServer(_SslCert)
            _ControlReader = New StreamReader(_SslStream)
            _ControlWriter = New StreamWriter(_SslStream)
          End If

        End If
      End While
    Catch ex As Exception When Program.CatchblocksEnabled
      _Log.[Error](ex.Message)
    End Try

    Dispose()
  End Sub

#End Region

#Region " Connection Handling "

  Public Function EnterPassiveModeEx(ByRef extractedPortInfo As String) As Integer
    _DataConnectionType = DataConnectionType.Passive

    Dim localIp As IPAddress = DirectCast(_ControlClient.Client.LocalEndPoint, IPEndPoint).Address
    Dim localEpPort As Integer

    _PassiveListener = New TcpListener(localIp, 0)
    _PassiveListener.Start()

    localEpPort = DirectCast(_PassiveListener.LocalEndpoint, IPEndPoint).Port

    extractedPortInfo = localEpPort.ToString()

    Return localEpPort
  End Function

  Public Function EnterPassiveMode(ByRef extractedPortInfo As String) As Byte()

    Dim passiveEndpointInfo(5) As Byte
    Dim localIp As IPAddress
    Dim passiveListenerEndpoint As IPEndPoint

    _DataConnectionType = DataConnectionType.Passive
    localIp = DirectCast(_ControlClient.Client.LocalEndPoint, IPEndPoint).Address

    _PassiveListener = New TcpListener(localIp, 0)
    _PassiveListener.Start()

    passiveListenerEndpoint = DirectCast(_PassiveListener.LocalEndpoint, IPEndPoint)

    Dim address As Byte() = passiveListenerEndpoint.Address.GetAddressBytes()
    Dim port As Short = CShort(passiveListenerEndpoint.Port)

    Dim portArray As Byte() = BitConverter.GetBytes(port)

    If BitConverter.IsLittleEndian Then
      Array.Reverse(portArray)
    End If

    passiveEndpointInfo(0) = address(0)
    passiveEndpointInfo(1) = address(1)
    passiveEndpointInfo(2) = address(2)
    passiveEndpointInfo(3) = address(3)
    passiveEndpointInfo(4) = portArray(0)
    passiveEndpointInfo(5) = portArray(1)

    Return passiveEndpointInfo
  End Function

  Public Sub ActiveModePort(hostPort As String, ByRef extractedPortInfo As String)
    _DataConnectionType = DataConnectionType.Active

    Dim ipAndPort As String() = hostPort.Split(","c)

    Dim ipAddress As Byte() = New Byte(3) {}
    Dim portBytes As Byte() = New Byte(1) {}

    For i As Integer = 0 To 3
      ipAddress(i) = Convert.ToByte(ipAndPort(i))
    Next

    For i As Integer = 4 To 5
      portBytes(i - 4) = Convert.ToByte(ipAndPort(i))
    Next

    If BitConverter.IsLittleEndian Then
      Array.Reverse(portBytes)
    End If

    _DataEndpoint = New IPEndPoint(New IPAddress(ipAddress), BitConverter.ToInt16(portBytes, 0))

    extractedPortInfo = _DataEndpoint.Port.ToString()
  End Sub

  Public Sub ActiveModePortEx(hostPort As String, ByRef extractedPortInfo As String)
    _DataConnectionType = DataConnectionType.Active

    Dim delimiter As Char = hostPort(0)
    Dim rawSplit As String() = hostPort.Split(New Char() {delimiter}, StringSplitOptions.RemoveEmptyEntries)
    Dim ipType As Char = rawSplit(0)(0)
    Dim ipAddress__1 As String = rawSplit(1)
    Dim port As String = rawSplit(2)

    _DataEndpoint = New IPEndPoint(IPAddress.Parse(ipAddress__1), Integer.Parse(port))

    extractedPortInfo = _DataEndpoint.Port.ToString()
  End Sub

  Public Function SetTransferType(typeCode As Char, formatControl As Char, ByRef newConnectionType As String) As Boolean

    Select Case Char.ToUpperInvariant(typeCode)
      Case "A" : _CurrentTransferType = TransferType.Ascii
      Case "I" : _CurrentTransferType = TransferType.Image
      Case Else : Return False
    End Select

    If (Not String.IsNullOrWhiteSpace(formatControl)) Then
      Select Case Char.ToUpperInvariant(formatControl)
        Case "N" : _CurrentFormatControlType = FormatControlType.NonPrint
        Case Else : Return False
      End Select
    End If

    newConnectionType = _CurrentTransferType
    Return True
  End Function

  Public Function SetFileStructureType(structureIdentifier As Char, ByRef notImplemented As Boolean) As Boolean
    notImplemented = False
    Select Case structureIdentifier
      Case "F"
        _FileStructureType = FileStructureType.File
      Case "R", "P"
        notImplemented = True
        Return False
      Case Else
        Return False
    End Select

    Return True
  End Function

  Public Function SetMode(modeIdentifier As Char) As Boolean
    Return (Char.ToUpperInvariant(modeIdentifier) = "S")
  End Function

#End Region

  Public Sub ChangeIdentity(newUser As User)

    If (newUser Is Nothing) Then

      _CurrentUser = Nothing
      _CurrentUserName = Nothing


      '_FsCommandHandler.EmulatedRootFolder = Nothing

      _FsCommandHandler.CurrentDir = Nothing

      _PassiveListener = Nothing
      _DataClient = Nothing

    Else

      _FsCommandHandler()


      _CurrentUser = newUser
      '_FsCommandHandler.EmulatedR = _currentUser.HomeDir
      _FsCommandHandler.CurrentDir = _FsCommandHandler.EmulatedRootFolder
    End If
  End Sub

#Region " DataConnection Operations "

  Private Sub HandleAsyncResult(result As IAsyncResult)
    If (_DataConnectionType = DataConnectionType.Active) Then
      _DataClient.EndConnect(result)
    Else
      _DataClient = _PassiveListener.EndAcceptTcpClient(result)
    End If
  End Sub

  Public Sub SetupDataConnectionOperation(state As DataConnectionOperation)
    If (_DataConnectionType = DataConnectionType.Active) Then
      _DataClient = New TcpClient(_DataEndpoint.AddressFamily)
      _DataClient.BeginConnect(_DataEndpoint.Address, _DataEndpoint.Port, AddressOf DoDataConnectionOperation, state)
    Else
      _PassiveListener.BeginAcceptTcpClient(AddressOf DoDataConnectionOperation, state)
    End If
  End Sub

  Private Sub DoDataConnectionOperation(result As IAsyncResult)
    HandleAsyncResult(result)

    Dim op As DataConnectionOperation = TryCast(result.AsyncState, DataConnectionOperation)

    Dim response As String

    Using dataStream As NetworkStream = _DataClient.GetStream()
      response = op.Operation(dataStream, op.Arguments, _FsCommandHandler)
    End Using

    _DataClient.Close()
    _DataClient = Nothing

    _ControlWriter.WriteLine(response)
    _ControlWriter.Flush()
  End Sub

  Private Function RetrieveOperation(dataStream As NetworkStream, pathname As String, fileSystem As IFileSystemProvider) As String
    Dim bytes As Long = 0
    Using fs As Stream = fileSystem.OpenFileForRead(pathname)
      bytes = CopyStream(fs, dataStream)
    End Using
    Return FtpMessages.GetMessage(FtpCommunicationMessage.TransferCompleted)
  End Function

  Private Function StoreOperation(dataStream As NetworkStream, pathname As String, fileSystem As IFileSystemProvider) As String
    Dim bytes As Long = 0
    Using fs As Stream = fileSystem.OpenFileForRead(pathname)
      bytes = CopyStream(dataStream, fs)
    End Using
    Dim logEntry As New LogEntry() With { _
     .[Date] = DateTime.Now, _
     .CIP = _ClientIp, _
     .CSMethod = "STOR", _
     .CSUsername = _CurrentUserName, _
     .SCStatus = "226", _
     .CSBytes = bytes.ToString() _
    }
    _Log.Info(logEntry)
    Return FtpMessages.GetMessage(FtpCommunicationMessage.TransferCompleted)
  End Function

  Private Function AppendOperation(dataStream As NetworkStream, pathname As String, fileSystem As IFileSystemProvider) As String
    Dim bytes As Long = 0
    Using fs As Stream = fileSystem.OpenFileForAppend(pathname)
      bytes = CopyStream(dataStream, fs)
    End Using
    Dim logEntry As New LogEntry() With { _
     .[Date] = DateTime.Now, _
     .CIP = _ClientIp, _
     .CSMethod = "APPE", _
     .CSUsername = _CurrentUserName, _
     .SCStatus = "226", _
     .CSBytes = bytes.ToString() _
    }
    _Log.Info(logEntry)
    Return FtpMessages.GetMessage(FtpCommunicationMessage.TransferCompleted)
  End Function

  Private Function ListOperation(dataStream As NetworkStream, pathname As String, fileSystem As IFileSystemProvider) As String

    Dim dataWriter As New StreamWriter(dataStream, Encoding.ASCII)


    Dim directories As IEnumerable(Of String) = _FsCommandHandler.GetDirectoryNames(pathname)
    For Each dir As String In directories
      Dim lastWriteTime As DateTime = _FsCommandHandler.DirModificationTime(pathname)

      Dim [date] As String = If(lastWriteTime < DateTime.Now - TimeSpan.FromDays(180),
                                lastWriteTime.ToString("MMM dd  yyyy"),
                                lastWriteTime.ToString("MMM dd HH:mm"))

      Dim line As String = String.Format("drwxr-xr-x    2 2003     2003     {0,8} {1} {2}", "4096", [date], dir)

      dataWriter.WriteLine(line)
      dataWriter.Flush()
    Next

    Dim files As IEnumerable(Of String) = _FsCommandHandler.GetFileNames(pathname)
    For Each file As String In files

      Dim lastWriteTime As DateTime = _FsCommandHandler.FileModificationTime(pathname)
      Dim fileSize As Long
      If (pathname.EndsWith("\")) Then
        fileSize = _FsCommandHandler.FileSize(pathname & file)
      Else
        fileSize = _FsCommandHandler.FileSize(pathname & "\" & file)
      End If

      Dim [date] As String = If(lastWriteTime < DateTime.Now - TimeSpan.FromDays(180),
                                lastWriteTime.ToString("MMM dd  yyyy"),
                                lastWriteTime.ToString("MMM dd HH:mm"))

      Dim line As String = String.Format("-rw-r--r--    2 2003     2003     {0,8} {1} {2}", fileSize, [date], file)

      dataWriter.WriteLine(line)
      dataWriter.Flush()
    Next




    Dim logEntry As New LogEntry() With { _
     .[Date] = DateTime.Now, _
     .CIP = _ClientIp, _
     .CSMethod = "LIST", _
     .CSUsername = _CurrentUserName, _
     .SCStatus = "226" _
    }

    _Log.Info(logEntry)

    Return "226 Transfer complete"
  End Function

#End Region

#Region " Copy Stream Implementations "

  Private Shared Function CopyStream(input As Stream, output As Stream, bufferSize As Integer) As Long
    Dim buffer As Byte() = New Byte(bufferSize - 1) {}
    Dim count As Integer = 0
    Dim total As Long = 0

    While (InlineAssignHelper(count, input.Read(buffer, 0, buffer.Length))) > 0
      output.Write(buffer, 0, count)
      total += count
    End While

    Return total
  End Function

  Private Shared Function CopyStreamAscii(input As Stream, output As Stream, bufferSize As Integer) As Long
    Dim buffer As Char() = New Char(bufferSize - 1) {}
    Dim count As Integer = 0
    Dim total As Long = 0

    Using rdr As New StreamReader(input, Encoding.ASCII)
      Using wtr As New StreamWriter(output, Encoding.ASCII)
        While (InlineAssignHelper(count, rdr.Read(buffer, 0, buffer.Length))) > 0
          wtr.Write(buffer, 0, count)
          total += count
        End While
      End Using
    End Using

    Return total
  End Function

  Private Function CopyStream(input As Stream, output As Stream) As Long
    Dim limitedStream As Stream = output
    ' new RateLimitingStream(output, 131072, 0.5);
    If _CurrentTransferType = TransferType.Image Then
      Return CopyStream(input, limitedStream, 4096)
    Else
      Return CopyStreamAscii(input, limitedStream, 4096)
    End If
  End Function

#End Region

#Region " IDisposable "

  Public Sub Dispose() Implements System.IDisposable.Dispose
    Dispose(True)
  End Sub

  Protected Overridable Sub Dispose(disposing As Boolean)
    If (Not _IsDisposed) Then

      If disposing Then
        If _ControlClient IsNot Nothing Then
          _ControlClient.Close()
        End If

        If _DataClient IsNot Nothing Then
          _DataClient.Close()
        End If

        If _ControlStream IsNot Nothing Then
          _ControlStream.Close()
        End If

        If _ControlReader IsNot Nothing Then
          _ControlReader.Close()
        End If

        If _ControlWriter IsNot Nothing Then
          _ControlWriter.Close()
        End If
      End If

    End If

    _IsDisposed = True
  End Sub

  Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
    target = value
    Return value
  End Function

#End Region

End Class
