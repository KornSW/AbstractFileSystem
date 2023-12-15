Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.ComponentModel

Public Interface IAfsSource
  Inherits IAfsDirectoryInfo

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  ReadOnly Property SourceIdentifier As String

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Function OpenRead(relativePath As String) As Stream

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Function OpenWrite(relativePath As String) As Stream


  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Function TestAccessability(relativePath As String) As Boolean

  Function GetFileLastWriteTime(relativePath As String) As DateTime

  Function GetFileCreationTime(relativePath As String) As DateTime

  Function GetFileLength(relativePath As String) As Long
  Function SearchFiles(startDirectory As String, mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo)

End Interface
