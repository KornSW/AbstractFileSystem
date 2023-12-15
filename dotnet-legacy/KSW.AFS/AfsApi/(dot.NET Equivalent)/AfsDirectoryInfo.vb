Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO

Public Class AfsDirectoryInfo
  Implements IAfsDirectoryInfo

#Region " Declarations & Constructors "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _Source As IAfsSource
  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _RelativePathFromSource As String

  Public Sub New(fullyQualifiedAfsPath As String)
    Me.New(New AfsUri(fullyQualifiedAfsPath))
  End Sub

  Public Sub New(afsUri As AfsUri)
    _Source = AfsEnvironment.FindSourceForUri(afsUri)
    _RelativePathFromSource = AfsPath.Defragment(afsUri.RelativePath, True, True)
  End Sub

  Public Sub New(source As IAfsSource)
    _Source = source
    _RelativePathFromSource = Path.DirectorySeparatorChar
  End Sub

  Public Sub New(source As IAfsSource, relativePathFromSource As String)
    _Source = source
    _RelativePathFromSource = AfsPath.Defragment(relativePathFromSource, True, True)
  End Sub

#End Region

  Public ReadOnly Property Source As IAfsSource
    Get
      Return _Source
    End Get
  End Property

  Public ReadOnly Property RelativePathFromSource As String
    Get
      Return _RelativePathFromSource
    End Get
  End Property

  Public Function ToUri() As AfsUri
    Return New AfsUri(_Source, _RelativePathFromSource)
  End Function

  Public ReadOnly Property FullName As String
    Get
      Return _Source.SourceIdentifier & RelativePathFromSource
    End Get
  End Property

  Public ReadOnly Property Name As String
    Get
      Return AfsPath.GetName(_RelativePathFromSource)
    End Get
  End Property

  Public ReadOnly Property Exists As Boolean
    Get
      'why do we have an Exists function, when a source returns nothing?
      'Answer: our AfsDirectoryInfo can also be initialized using a string or uri
      'and in this case we need the exists method!
      Return _Source.GetDirectory(_RelativePathFromSource) IsNot Nothing
    End Get
  End Property

  Public Function GetSource() As IAfsSource Implements IAfsDirectoryInfo.GetSource
    Return _Source
  End Function

  Public Function IsAccessable() As Boolean
    Return _Source.TestAccessability(_RelativePathFromSource)
  End Function





#Region " Directories "

  Private Function GetDirectories(parentDirectoryPath As String) As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories
    Return _Source.GetDirectories(Path.Combine(_RelativePathFromSource, parentDirectoryPath))
  End Function
  Public Function GetDirectories() As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories
    Return _Source.GetDirectories(_RelativePathFromSource)
  End Function

  Public Function GetDirectory(relativePath As String) As AfsDirectoryInfo Implements IAfsDirectoryInfo.GetDirectory
    Return _Source.GetDirectory(Path.Combine(_RelativePathFromSource, relativePath))
  End Function

#End Region

#Region " Files "

  Public Function GetFiles(relativePath As String) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles
    Return _Source.GetFiles(Path.Combine(_RelativePathFromSource, relativePath))
  End Function
  Public Function GetFiles() As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles
    Return _Source.GetFiles(_RelativePathFromSource)
  End Function
  Public Function GetFile(relativePath As String) As AfsFileInfo Implements IAfsDirectoryInfo.GetFile
    Return _Source.GetFile(Path.Combine(_RelativePathFromSource, relativePath))
  End Function

  Public Function SearchFiles(mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.SearchFiles
    Return _Source.SearchFiles(_RelativePathFromSource, mask, recursive, limitResults)
  End Function

#End Region

End Class
