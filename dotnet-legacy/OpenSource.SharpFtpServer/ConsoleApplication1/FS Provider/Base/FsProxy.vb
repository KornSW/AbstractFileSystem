Imports System
Imports System.IO
Imports System.Collections
Imports System.Collections.Generic

Public MustInherit Class FsProxy
  Implements IFileSystemProvider


#Region " Constructor & Declarations "

  Private _Source As IFileSystemProvider

  Public Sub New(source As IFileSystemProvider)
    _Source = source
  End Sub

  Protected ReadOnly Property Source As IFileSystemProvider
    Get
      Return _Source
    End Get
  End Property

#End Region

#Region " Proxy "

  Public Overridable Function CreateDirectory(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.CreateDirectory
    Return _Source.CreateDirectory(directoryPath)
  End Function

  Public Overridable Function DeleteFile(filePath As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteFile
    Return _Source.DeleteFile(filePath)
  End Function

  Public Overridable Function DeleteDirectory(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteDirectory
    Return _Source.DeleteDirectory(directoryPath)
  End Function

  Public Overridable Function FileExists(filePath As AbsolutePath) As Boolean Implements IFileSystemProvider.FileExists
    Return _Source.FileExists(filePath)
  End Function

  Public Overridable Function GetFileModificationTime(filePath As AbsolutePath) As DateTime Implements IFileSystemProvider.GetFileModificationTime
    Return _Source.GetFileModificationTime(filePath)
  End Function

  Public Overridable Function GetFileSize(filePath As AbsolutePath) As Long Implements IFileSystemProvider.GetFileSize
    Return _Source.GetFileSize(filePath)
  End Function

  Public Overridable Function GetDirectoryModificationTime(directoryPath As AbsolutePath) As DateTime Implements IFileSystemProvider.GetDirectoryModificationTime
    Return _Source.GetDirectoryModificationTime(directoryPath)
  End Function

  Public Overridable Function GetFileNames(directoryPath As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetFileNames
    Return _Source.GetFileNames(directoryPath)
  End Function

  Public Overridable Function GetDirectoryNames(directoryPath As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetDirectoryNames
    Return _Source.GetDirectoryNames(directoryPath)
  End Function

  Public Overridable Function PathIsValid(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.PathIsValid
    Return _Source.PathIsValid(directoryPath)
  End Function

  Public Overridable Function OpenFileForAppend(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForAppend
    Return _Source.OpenFileForAppend(filePath)
  End Function

  Public Overridable Function OpenFileForRead(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForRead
    Return _Source.OpenFileForRead(filePath)
  End Function

  Public Overridable Function OpenFileForWrite(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForWrite
    Return _Source.OpenFileForWrite(filePath)
  End Function

  Public Overridable Function SetFileModificationTime(filePath As AbsolutePath, newModificationTime As DateTime) As Boolean Implements IFileSystemProvider.SetFileModificationTime
    Return _Source.SetFileModificationTime(filePath, newModificationTime)
  End Function

  Public Overridable Function RenameFile(oldFilePath As AbsolutePath, newFilePath As AbsolutePath) As Boolean Implements IFileSystemProvider.RenameFile
    Return _Source.RenameFile(oldFilePath, newFilePath)
  End Function

#End Region

  Public Function GetFileAttributes(filePath As AbsolutePath) As IEnumerable(Of IFileAttribute) Implements IFileSystemProvider.GetFileAttributes

  End Function

End Class
