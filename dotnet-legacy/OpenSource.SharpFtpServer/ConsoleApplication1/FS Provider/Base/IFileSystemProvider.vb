Imports System
Imports System.IO
Imports System.Collections
Imports System.Collections.Generic

Public Interface IFileSystemProvider

  Function PathIsValid(directoryPath As AbsolutePath) As Boolean
  Function FileExists(filePath As AbsolutePath) As Boolean
  Function GetFileNames(directoryPath As AbsolutePath) As IEnumerable(Of String)
  Function GetDirectoryNames(directoryPath As AbsolutePath) As IEnumerable(Of String)
  Function RenameFile(oldFilePath As AbsolutePath, newFilePath As AbsolutePath) As Boolean
  Function DeleteFile(filePath As AbsolutePath) As Boolean
  Function DeleteDirectory(directoryPath As AbsolutePath) As Boolean
  Function CreateDirectory(directoryPath As AbsolutePath) As Boolean
  Function GetDirectoryModificationTime(directoryPath As AbsolutePath) As DateTime
  Function GetFileModificationTime(filePath As AbsolutePath) As DateTime
  Function GetFileSize(filePath As AbsolutePath) As Long
  Function OpenFileForRead(filePath As AbsolutePath) As Stream
  Function OpenFileForWrite(filePath As AbsolutePath) As Stream
  Function OpenFileForAppend(filePath As AbsolutePath) As Stream
  Function SetFileModificationTime(filePath As AbsolutePath, newModificationTime As DateTime) As Boolean
  Function GetFileAttributes(filePath As AbsolutePath) As IEnumerable(Of IFileAttribute)

End Interface

'System:
'ModificationTime,CreationTime,IsReadOnly,SizeBytes,FileTitle,Extension,MimeType

'DMS:
'ALLES ANDERE

Public Interface IFileAttribute

  ReadOnly Property Name As String
  ReadOnly Property DisplayName As String
  ReadOnly Property DisplayValue As String

End Interface

