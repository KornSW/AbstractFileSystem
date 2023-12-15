Imports System
Imports System.Diagnostics
Imports System.IO

Public Class AfsFileInfo
  Implements IAfsFileInfo

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
    _RelativePathFromSource = AfsPath.Defragment(afsUri.RelativePath, True, False)
  End Sub

  Public Sub New(source As IAfsSource, relativePathFromSource As String)
    _Source = source
    _RelativePathFromSource = AfsPath.Defragment(relativePathFromSource, True, False)
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
      Return _Source.GetFile(_RelativePathFromSource) IsNot Nothing
    End Get
  End Property

  Public Function GetSource() As IAfsSource Implements IAfsFileInfo.GetSource
    Return _Source
  End Function

  Public Function GetParentDirectory() As IAfsDirectoryInfo Implements IAfsFileInfo.GetParentDirectory
    Return _Source.GetDirectory(AfsPath.Defragment(Path.Combine(_RelativePathFromSource, ".." & Path.DirectorySeparatorChar)))
  End Function

  Public Function OpenRead() As Stream Implements IAfsFileInfo.OpenRead
    Return _Source.OpenRead(_RelativePathFromSource)
  End Function

  Public Function OpenWrite() As Stream Implements IAfsFileInfo.OpenWrite
    Return _Source.OpenWrite(_RelativePathFromSource)
  End Function

  Public ReadOnly Property LastWriteTime As DateTime
    Get
      Return _Source.GetFileLastWriteTime(_RelativePathFromSource)
    End Get
  End Property

  Public ReadOnly Property CreationTime As DateTime
    Get
      Return _Source.GetFileCreationTime(_RelativePathFromSource)
    End Get
  End Property

  Public ReadOnly Property Length As Long
    Get
      Return _Source.GetFileLength(_RelativePathFromSource)
    End Get
  End Property

End Class
