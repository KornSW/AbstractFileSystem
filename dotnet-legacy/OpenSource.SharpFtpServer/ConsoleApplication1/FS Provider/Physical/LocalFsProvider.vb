Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports System.Net.Sockets
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Public Class LocalFsProvider
  Implements IFileSystemProvider


#Region " Declarations "

  Private _RootDirectory As String

#End Region

#Region " Constructor "

  Public Sub New(rootDirectory As String)
    _RootDirectory = rootDirectory
    If (_RootDirectory.EndsWith("\")) Then
      _RootDirectory = _RootDirectory.Substring(0, _RootDirectory.Length - 1)
    End If
  End Sub

#End Region

#Region " Navigation "

  Private _CurrentWorkingFolder As String
  Public Property CurrentWorkingFolder As String Implements IFileSystemProvider.CurrentWorkingFolder
    Get
      Return _CurrentWorkingFolder
    End Get
    Set(value As String)
      If value = String.Empty Then
        _CurrentWorkingFolder = "\"
      Else
        _CurrentWorkingFolder = value
      End If
    End Set
  End Property

  Private Sub ChangeWorkingDirectory(pathname As String) Implements IFileSystemProvider.ChangeWorkingFolder
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
      inputPath = Path.Combine(_GlobalRootDir, _UserRootDir)
    ElseIf inputPath.StartsWith(Path.DirectorySeparatorChar) Then
      inputPath = New FileInfo(Path.Combine(_GlobalRootDir, Me.UserRootDir, inputPath.Substring(1))).FullName
    Else
      inputPath = New FileInfo(Path.Combine(_RootDirectory, Me.CurrentDir, inputPath)).FullName
    End If

    Return If(Me.IsPathValid(inputPath), inputPath, Nothing)
  End Function

  Private Function IsPathValid(path As String) As Boolean Implements IFileSystemProvider.PathIsValid
    Return path.StartsWith(IO.Path.Combine(_GlobalRootDir, _UserRootDir))
  End Function

#End Region

#Region " Enumeration "

  Public Function Exists(pathname As String) As Boolean Implements IFileSystemProvider.FileExists
    Return IO.File.Exists(Me.NormalizeFilename(pathname))
  End Function

  Public Function GetDirectoryNames(pathname As String) As IEnumerable(Of String) Implements IFileSystemProvider.GetDirectoryNames
    Dim di As New IO.DirectoryInfo(Me.NormalizeFilename(pathname))
    Dim result As New List(Of String)
    For Each fi As IO.FileInfo In di.GetFiles()
      result.Add(fi.Name)
    Next
    Return result 'Directory.EnumerateDirectories(pathname)
  End Function

  Public Function GetFileNames(pathname As String) As IEnumerable(Of String) Implements IFileSystemProvider.GetFileNames
    Dim di As New IO.DirectoryInfo(Me.NormalizeFilename(pathname))
    Dim result As New List(Of String)
    For Each sdi As IO.DirectoryInfo In di.GetDirectories()
      result.Add(sdi.Name)
    Next
    Return result 'Directory.EnumerateFiles(pathname)
  End Function

#End Region

#Region " Delete and Rename "

  Private Function Delete(pathname As String) As String Implements IFileSystemProvider.DeleteFile
    pathname = Me.NormalizeFilename(pathname)

    If (pathname IsNot Nothing) Then
      If (File.Exists(pathname)) Then
        File.Delete(pathname)
      Else
        Return "550 File Not Found"
      End If

      Return "250 Requested file action okay, completed"
    End If

    Return "550 File Not Found"
  End Function

  Private Function RemoveDir(pathname As String) As String Implements IFileSystemProvider.DeleteDirectory
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

  Private Function CreateDir(pathname As String) As String Implements IFileSystemProvider.CreateDirectory
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

  Private Function Rename(renameFrom As String, renameTo As String) As String Implements IFileSystemProvider.RenameFile
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

  Public Function FileSize(pathname As String) As Long Implements IFileSystemProvider.GetFileSize

    Dim length As Long = -1
    If (File.Exists(NormalizeFilename(pathname))) Then
      Using fs As FileStream = File.Open(pathname, FileMode.Open, FileAccess.Read, FileShare.Read)
        length = fs.Length
      End Using
    End If

    Return length
  End Function

  Private Function FileModificationTime(pathname As String) As DateTime Implements IFileSystemProvider.GetFileModificationTime
    pathname = NormalizeFilename(pathname)

    If (pathname IsNot Nothing) Then
      If (File.Exists(pathname)) Then
        Return File.GetLastWriteTime(pathname)
      End If
    End If

    Return DateTime.MinValue
  End Function

  Public Function DirModificationTime(pathname As String) As Date Implements IFileSystemProvider.GetDirectoryModificationTime
    pathname = NormalizeFilename(pathname)

    If (pathname IsNot Nothing) Then
      If (Directory.Exists(pathname)) Then
        Return Directory.GetLastWriteTime(pathname)
      End If
    End If

    Return DateTime.MinValue
  End Function

#End Region

#Region " Read and Write Streams "

  Public Function OpenRead(pathname As String) As Stream Implements IFileSystemProvider.OpenFileForRead
    Return New FileStream(NormalizeFilename(pathname), FileMode.Open, FileAccess.Read)
  End Function

  Public Function OpenWrite(pathname As String) As Stream Implements IFileSystemProvider.OpenFileForWrite
    Return New FileStream(NormalizeFilename(pathname), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 4096, FileOptions.SequentialScan)
  End Function

  Public Function OpenAppend(pathname As String) As Stream Implements IFileSystemProvider.OpenFileForAppend
    Return New FileStream(NormalizeFilename(pathname), FileMode.Append, FileAccess.Write, FileShare.None, 4096, FileOptions.SequentialScan)
  End Function

#End Region










  Public Function CreateDirectory(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.CreateDirectory

  End Function

  Public Function DeleteDirectory(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteDirectory

  End Function

  Public Function DeleteFile(filePath As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteFile

  End Function

  Public Function FileExists(filePath As AbsolutePath) As Boolean Implements IFileSystemProvider.FileExists

  End Function

  Public Function GetDirectoryModificationTime(directoryPath As AbsolutePath) As Date Implements IFileSystemProvider.GetDirectoryModificationTime

  End Function

  Public Function GetDirectoryNames(directoryPath As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetDirectoryNames

  End Function

  Public Function GetFileAttributes(filePath As AbsolutePath) As IEnumerable(Of IFileAttribute) Implements IFileSystemProvider.GetFileAttributes

  End Function

  Public Function GetFileModificationTime(filePath As AbsolutePath) As Date Implements IFileSystemProvider.GetFileModificationTime

  End Function

  Public Function GetFileNames(directoryPath As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetFileNames

  End Function

  Public Function GetFileSize(filePath As AbsolutePath) As Long Implements IFileSystemProvider.GetFileSize

  End Function

  Public Function OpenFileForAppend(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForAppend

  End Function

  Public Function OpenFileForRead(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForRead

  End Function

  Public Function OpenFileForWrite(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForWrite

  End Function

  Public Function PathIsValid(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.PathIsValid

  End Function

  Public Function RenameFile(oldFilePath As AbsolutePath, newFilePath As AbsolutePath) As Boolean Implements IFileSystemProvider.RenameFile

  End Function

  Public Function SetFileModificationTime(filePath As AbsolutePath, newModificationTime As Date) As Boolean Implements IFileSystemProvider.SetFileModificationTime

  End Function
End Class
