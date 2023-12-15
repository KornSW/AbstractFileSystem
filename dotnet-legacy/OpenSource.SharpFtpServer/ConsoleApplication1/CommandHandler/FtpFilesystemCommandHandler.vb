Imports System
Imports System.IO

Public Class FtpFilesystemCommandHandler
  Implements IFtpCommandHandler

  Private _Connection As ClientConnection
  Private _FileSystem As IFileSystemProvider
  Private _Navigator As FileSystemNavigator

  Private _CurrentUserName As String = String.Empty

  Private _RenameFrom As String = Nothing

  Public Sub New(connection As ClientConnection, fileSystem As IFileSystemProvider)
    _Connection = connection
    _FileSystem = fileSystem
  End Sub

  Private Const _FtpPathSeparatorChar As Char = "\"c

  Public Function HandleFtpCommand(command As String, arguments As String, logEntry As LogEntry) As String Implements IFtpCommandHandler.HandleFtpCommand
    Dim response As String = Nothing

    If (Not _CurrentUserName = _Connection.CurrentUser.Username) Then
      _CurrentUserName = _Connection.CurrentUser.Username
      _Navigator = Nothing
    End If

    Dim rootEmulator As IFileSystemProvider = New FileSystemRootEmulator(
      _FileSystem, New AbsolutePath(_Connection.CurrentUser.HomeDir))

    If (_Navigator Is Nothing) Then
      _Navigator = New FileSystemNavigator(rootEmulator)
    End If

    If (Not command = "RNTO") Then
      _RenameFrom = Nothing
    End If

    Select Case command



      Case "CWD"
        _FileSystem.ChangeWorkingFolder(arguments)
        response = Me.ChangeWorkingDirectory(arguments)
        Exit Select
      Case "CDUP"
        response = Me.ChangeWorkingDirectory("..")
        Exit Select



      Case "DELE"
        response = _FileSystem.DeleteFile(arguments)
        Exit Select
      Case "RMD"
        response = _FileSystem.DeleteDirectory(arguments)
        Exit Select
      Case "MKD"
        response = _FileSystem.CreateDirectory(arguments)
        Exit Select
      Case "PWD"
        response = Me.PrintWorkingDirectory()
        Exit Select
      Case "RETR"
        response = Me.Retrieve(arguments)
        logEntry.[Date] = DateTime.Now
        Exit Select
      Case "STOR"
        response = Me.Store(arguments)
        logEntry.[Date] = DateTime.Now
        Exit Select
      Case "STOU"
        response = Me.StoreUnique()
        logEntry.[Date] = DateTime.Now
        Exit Select
      Case "APPE"
        response = Me.Append(arguments)
        logEntry.[Date] = DateTime.Now
        Exit Select
      Case "LIST"
        response = Me.List(If(arguments, _FileSystem.CurrentWorkingFolder))
        logEntry.[Date] = DateTime.Now
        Exit Select




        ' Extensions defined by rfc 3659
      Case "MDTM"
        response = Me.FileModificationTime(arguments)
        Exit Select
      Case "SIZE"
        response = Me.FileSize(arguments)
        Exit Select




      Case "RNFR" : _RenameFrom = arguments
      Case "RNTO" : _FileSystem.ProcessFtpCommand(cmd, arguments)



        renameFrom = arguments
        response = "350 Requested file action pending further information"
        response = _FileSystem.Rename(renameFrom, arguments)





      Case Else
    End Select
    Return response
  End Function









  Private Function PrintWorkingDirectory() As String
    Dim current As String = _Navigator.CurrentWorkingDir

    If current.Length = 0 Then
      current = "/"
    End If

    Return String.Format("257 ""{0}"" is current directory.", current)
  End Function

  Private Function ChangeWorkingDirectory(pathname As String) As String
    _FsCommandHandler.ChangeWorkingFolder(pathname)
    Return "250 Changed to new directory"
  End Function

  Private Function FileSize(pathname As String) As String
    If _FsCommandHandler.IsPathValid(pathname) Then
      Dim length As Long = _FsCommandHandler.FileSize(pathname)
      If (length >= 0) Then
        Return String.Format("213 {0}", length)
      End If
    End If
    Return "550 File Not Found"
  End Function

  Private Function Retrieve(pathname As String) As String

    If _FileSystem.PathIsValid(pathname) Then
      If (_FileSystem.FileExists(pathname)) Then
        Dim state = New DataConnectionOperation() With {.Arguments = pathname, .Operation = AddressOf RetrieveOperation}

        _Connection.SetupDataConnectionOperation(state)

        Return String.Format("150 Opening {0} mode data transfer for RETR", _dataConnectionType)
      End If
    End If

    Return "550 File Not Found"
  End Function

  Private Function Store(pathname As String) As String

    If _FsCommandHandler.IsPathValid(pathname) Then

      Dim state = New DataConnectionOperation() With {.Arguments = pathname, .Operation = AddressOf StoreOperation}


      _Connection.SetupDataConnectionOperation(state)

      Return String.Format("150 Opening {0} mode data transfer for STOR", _dataConnectionType)
    End If

    Return "450 Requested file action not taken"
  End Function

  Private Function FileModificationTime(pathname As String) As String

    Dim t As DateTime = _FsCommandHandler.FileModificationTime(pathname)
    If (t = DateTime.MinValue) Then
      Return "550 File Not Found"
    Else
      Return String.Format("213 {0}", t.ToString("yyyyMMddHHmmss.fff"))
    End If

  End Function

  Private Function Append(pathname As String) As String



    If _FsCommandHandler.IsPathValid(pathname) Then
      Dim state = New DataConnectionOperation() With {.Arguments = pathname, .Operation = AddressOf AppendOperation}

      _Connection.SetupDataConnectionOperation(state)

      Return String.Format("150 Opening {0} mode data transfer for APPE", _dataConnectionType)
    End If

    Return "450 Requested file action not taken"
  End Function

  Private Function StoreUnique() As String


    Dim pathname As String = New Guid().ToString()

    Dim state = New DataConnectionOperation() With {.Arguments = pathname, .Operation = AddressOf StoreOperation}

    _Connection.SetupDataConnectionOperation(state)

    Return String.Format("150 Opening {0} mode data transfer for STOU", _dataConnectionType)
  End Function

  Private Function List(pathname As String) As String

    If _FsCommandHandler.IsPathValid(pathname) Then
      Dim state = New DataConnectionOperation() With {.Arguments = pathname, .Operation = AddressOf ListOperation}

      _Connection.SetupDataConnectionOperation(state)

      Return String.Format("150 Opening {0} mode data transfer for LIST", _dataConnectionType)
    End If

    Return "450 Requested file action not taken"
  End Function

















#Region " Navigation "

  Public Property UserRootDir As String
    Get
      Return "\" & _UserRootDir
    End Get
    Set(value As String)
      If (value.StartsWith("\")) Then
        value = value.Substring(1, value.Length - 1)
      End If
      _UserRootDir = value
    End Set
  End Property

  Private _CurrentDir As String
  Public Property CurrentDir As String
    Get
      Return _CurrentDir
    End Get
    Set(value As String)
      If value = String.Empty Then
        _CurrentDir = "\"
      Else
        _CurrentDir = value
      End If
    End Set
  End Property

  Private Sub ChangeWorkingDirectory(pathname As String)
    If pathname = "/" Then
      Me.CurrentDir = Me.UserRootDir
    Else
      Dim newDir As String

      If pathname.StartsWith("/") Then
        pathname = pathname.Substring(1).Replace("/"c, "\"c)
        newDir = Path.Combine(Me.UserRootDir, pathname)
      Else
        pathname = pathname.Replace("/"c, "\"c)
        newDir = Path.Combine(Me.CurrentDir, pathname)
      End If

      If Directory.Exists(newDir) Then
        Me.CurrentDir = New DirectoryInfo(newDir).FullName

        If Not IsPathValid(Me.CurrentDir) Then
          Me.CurrentDir = Me.UserRootDir
        End If
      Else
        Me.CurrentDir = Me.UserRootDir
      End If
    End If

  End Sub

  Private Function NormalizeFilename(inputPath As String) As String

    If String.IsNullOrWhiteSpace(inputPath) Then
      inputPath = Path.DirectorySeparatorChar
    Else
      inputPath = inputPath.Replace("/", Path.DirectorySeparatorChar)
    End If

    If inputPath = Path.DirectorySeparatorChar Then
      inputPath = Path.Combine(GlobalRootDir, _UserRootDir)
    ElseIf inputPath.StartsWith(Path.DirectorySeparatorChar) Then
      inputPath = New FileInfo(Path.Combine(GlobalRootDir, Me.UserRootDir, inputPath.Substring(1))).FullName
    Else
      inputPath = New FileInfo(Path.Combine(GlobalRootDir, Me.CurrentDir, inputPath)).FullName
    End If

    Return If(Me.IsPathValid(inputPath), inputPath, Nothing)
  End Function

  Private Function IsPathValid(path As String) As Boolean
    Return path.StartsWith(IO.Path.Combine(GlobalRootDir, _UserRootDir))
  End Function

#End Region

#Region " Enumeration "

  Public Function Exists(pathname As String) As Boolean
    Return IO.File.Exists(Me.NormalizeFilename(pathname))
  End Function

  Public Function GetDirectoryNames(pathname As String) As IEnumerable(Of String)
    Dim di As New IO.DirectoryInfo(Me.NormalizeFilename(pathname))
    Dim result As New List(Of String)
    For Each fi As IO.FileInfo In di.GetFiles()
      result.Add(fi.Name)
    Next
    Return result 'Directory.EnumerateDirectories(pathname)
  End Function

  Public Function GetFileNames(pathname As String) As IEnumerable(Of String)
    Dim di As New IO.DirectoryInfo(Me.NormalizeFilename(pathname))
    Dim result As New List(Of String)
    For Each sdi As IO.DirectoryInfo In di.GetDirectories()
      result.Add(sdi.Name)
    Next
    Return result 'Directory.EnumerateFiles(pathname)
  End Function

#End Region

#Region " Delete and Rename "

  Private Function Delete(pathname As String) As String
    pathname = Me.NormalizeFilename(pathname)

    If pathname IsNot Nothing Then
      If File.Exists(pathname) Then
        File.Delete(pathname)
      Else
        Return "550 File Not Found"
      End If

      Return "250 Requested file action okay, completed"
    End If

    Return "550 File Not Found"
  End Function

  Private Function RemoveDir(pathname As String) As String
    pathname = NormalizeFilename(pathname)

    If pathname IsNot Nothing Then
      If Directory.Exists(pathname) Then
        Directory.Delete(pathname)
      Else
        Return "550 Directory Not Found"
      End If

      Return "250 Requested file action okay, completed"
    End If

    Return "550 Directory Not Found"
  End Function

  Private Function CreateDir(pathname As String) As String
    pathname = NormalizeFilename(pathname)

    If pathname IsNot Nothing Then
      If Not Directory.Exists(pathname) Then
        Directory.CreateDirectory(pathname)
      Else
        Return "550 Directory already exists"
      End If

      Return "250 Requested file action okay, completed"
    End If

    Return "550 Directory Not Found"
  End Function

  Private Function Rename(renameFrom As String, renameTo As String) As String
    If String.IsNullOrWhiteSpace(renameFrom) OrElse String.IsNullOrWhiteSpace(renameTo) Then
      Return "450 Requested file action not taken"
    End If

    renameFrom = NormalizeFilename(renameFrom)
    renameTo = NormalizeFilename(renameTo)

    If renameFrom IsNot Nothing AndAlso renameTo IsNot Nothing Then
      If File.Exists(renameFrom) Then
        File.Move(renameFrom, renameTo)
      ElseIf Directory.Exists(renameFrom) Then
        Directory.Move(renameFrom, renameTo)
      Else
        Return "450 Requested file action not taken"
      End If

      Return "250 Requested file action okay, completed"
    End If

    Return "450 Requested file action not taken"
  End Function

#End Region

#Region " Additional Attributes "

  Public Function FileSize(pathname As String) As Long
    'Dim fi As New IO.FileInfo(NormalizeFilename(pathname))
    'Return fi.Length

    Dim length As Long = -1
    If (File.Exists(NormalizeFilename(pathname))) Then
      Using fs As FileStream = File.Open(pathname, FileMode.Open, FileAccess.Read, FileShare.Read)
        length = fs.Length
      End Using
    End If

    Return length
  End Function

  Private Function FileModificationTime(pathname As String) As DateTime
    pathname = NormalizeFilename(pathname)

    If pathname IsNot Nothing Then
      If File.Exists(pathname) Then
        Return File.GetLastWriteTime(pathname)
      End If
    End If

    Return DateTime.MinValue
  End Function

  Public Function DirModificationTime(pathname As String) As Date
    pathname = NormalizeFilename(pathname)

    If pathname IsNot Nothing Then
      If Directory.Exists(pathname) Then
        Return Directory.GetLastWriteTime(pathname)
      End If
    End If

    Return DateTime.MinValue
  End Function

#End Region

#Region " Read and Write Streams "

  Public Function OpenRead(pathname As String) As Stream
    Return New IO.FileStream(NormalizeFilename(pathname), FileMode.Open, FileAccess.Read)
  End Function

  Public Function OpenWrite(pathname As String) As Stream
    Return New IO.FileStream(NormalizeFilename(pathname), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 4096, FileOptions.SequentialScan)
  End Function

  Public Function OpenAppend(pathname As String) As Stream
    Return New IO.FileStream(NormalizeFilename(pathname), FileMode.Append, FileAccess.Write, FileShare.None, 4096, FileOptions.SequentialScan)
  End Function

#End Region

End Class
