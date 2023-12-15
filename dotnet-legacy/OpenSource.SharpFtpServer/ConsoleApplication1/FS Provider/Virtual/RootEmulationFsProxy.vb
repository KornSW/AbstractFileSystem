Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Collections.Generic
Imports System

Public NotInheritable Class RootEmulationFsProxy
  Implements IFileSystemProvider


#Region " Constructor & Declarations "

  Private _Source As IFileSystemProvider
  Private _EmulatedRoot As AbsolutePath

  Public Sub New(source As IFileSystemProvider, emulatedRoot As AbsolutePath)
    _Source = source
    _EmulatedRoot = emulatedRoot
  End Sub

  Protected ReadOnly Property Source As IFileSystemProvider
    Get
      Return _Source
    End Get
  End Property

  Protected ReadOnly Property EmulatedRoot As AbsolutePath
    Get
      Return _EmulatedRoot
    End Get
  End Property

#End Region

#Region " Proxy "

  Public Function CreateDirectory(pathName As AbsolutePath) As Boolean Implements IFileSystemProvider.CreateDirectory
    Return _Source.CreateDirectory(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function DeleteFile(pathName As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteFile
    Return _Source.DeleteFile(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function DeleteDirectory(pathName As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteDirectory
    Return _Source.DeleteDirectory(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function FileExists(pathName As AbsolutePath) As Boolean Implements IFileSystemProvider.FileExists
    Return _Source.FileExists(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function GetFileModificationTime(pathName As AbsolutePath) As DateTime Implements IFileSystemProvider.GetFileModificationTime
    Return _Source.GetFileModificationTime(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function GetFileSize(pathName As AbsolutePath) As Long Implements IFileSystemProvider.GetFileSize
    Return _Source.GetFileSize(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function GetDirectoryModificationTime(pathName As AbsolutePath) As DateTime Implements IFileSystemProvider.GetDirectoryModificationTime
    Return _Source.GetDirectoryModificationTime(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function GetFileNames(pathName As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetFileNames
    Return _Source.GetFileNames(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function GetDirectoryNames(pathName As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetDirectoryNames
    Return _Source.GetDirectoryNames(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function PathIsValid(pathName As AbsolutePath) As Boolean Implements IFileSystemProvider.PathIsValid
    Return _Source.PathIsValid(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function OpenFileForAppend(pathName As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForAppend
    Return _Source.OpenFileForAppend(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function OpenFileForRead(pathName As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForRead
    Return _Source.OpenFileForRead(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function OpenFileForWrite(pathName As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForWrite
    Return _Source.OpenFileForWrite(pathName.IncreaseRoot(_EmulatedRoot))
  End Function

  Public Function SetFileModificationTime(filePath As AbsolutePath, newModificationTime As DateTime) As Boolean Implements IFileSystemProvider.SetFileModificationTime
    Return _Source.SetFileModificationTime(filePath.IncreaseRoot(_EmulatedRoot), newModificationTime)
  End Function

  Public Function RenameFile(renameFrom As AbsolutePath, renameTo As AbsolutePath) As Boolean Implements IFileSystemProvider.RenameFile
    Return _Source.RenameFile(renameFrom.IncreaseRoot(_EmulatedRoot), renameTo.IncreaseRoot(_EmulatedRoot))
  End Function

#End Region

  Public Function GetFileAttributes(filePath As AbsolutePath) As IEnumerable(Of IFileAttribute) Implements IFileSystemProvider.GetFileAttributes

  End Function
End Class
