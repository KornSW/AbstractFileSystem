Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection

Public Interface IAfsDirectoryInfo

  Function GetSource() As IAfsSource
  'Function GetParentDirectory() As IAfsDirectoryInfo


  Function GetDirectories(relativePath As String) As IEnumerable(Of AfsDirectoryInfo)
  Function GetDirectories() As IEnumerable(Of AfsDirectoryInfo)

  ''' <summary>
  ''' If the directory does not exist, nothing will be returned
  ''' </summary>
  Function GetDirectory(relativePath As String) As AfsDirectoryInfo


  Function GetFiles(relativePath As String) As IEnumerable(Of AfsFileInfo)
  Function GetFiles() As IEnumerable(Of AfsFileInfo)

  ''' <summary>
  ''' If the file does not exist, nothing will be returned
  ''' </summary>
  Function GetFile(relativePath As String) As AfsFileInfo

  'Sub Delete()
  Function SearchFiles(mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo)

End Interface
