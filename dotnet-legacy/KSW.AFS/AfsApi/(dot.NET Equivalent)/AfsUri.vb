Imports System
Imports System.Diagnostics

<DebuggerDisplay("AfsUri: {FullPath}")>
Public Class AfsUri


  Private _AfsSourceIdentifier As String
  Private _RelativePath As String

  Public Sub New(localDirectory As DirectoryInfo)
    Me.New("afs:\\" & localDirectory.FullName)
  End Sub

  Public Sub New(localFile As FileInfo)
    Me.New("afs:\\" & localFile.FullName)
  End Sub

  Public Sub New(fullyQualifiedAfsPath As String)
    If (Not fullyQualifiedAfsPath.StartsWith("afs:\\")) Then
      fullyQualifiedAfsPath = "afs:\\" & fullyQualifiedAfsPath
    End If
    Dim r = fullyQualifiedAfsPath.Split(Path.DirectorySeparatorChar)
    If (r.Length < 5) Then
      Throw New Exception
    End If
    _AfsSourceIdentifier = r(2)
    _RelativePath = fullyQualifiedAfsPath.Substring(6 + r(2).Length)
  End Sub

  Public Sub New(source As IAfsSource, relativePathFromSource As String)
    _AfsSourceIdentifier = source.SourceIdentifier
    _RelativePath = relativePathFromSource
  End Sub




  Public ReadOnly Property AfsSourceIdentifier As String
    Get
      Return _AfsSourceIdentifier
    End Get
  End Property

  ''' <summary>
  ''' relative to source
  ''' </summary>
  Public ReadOnly Property RelativePath As String
    Get
      Return _RelativePath
    End Get
  End Property

  Public ReadOnly Property FullyQualifiedAfsPath As String
    Get
      Return String.Format("afs:\\{0}\{1}")
    End Get
  End Property

  Public Overrides Function ToString() As String
    Return Me.FullyQualifiedAfsPath
  End Function

  Public Overrides Function GetHashCode() As Integer
    Return Me.FullyQualifiedAfsPath.GetHashCode()
  End Function

End Class
