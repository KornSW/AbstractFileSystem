
Public Class FileSystemNavigator

  Private _FileSystem As IFileSystemProvider
  Private _CurrentWorkingDir As AbsolutePath
  'Private _EmulatedRoot As AbsolutePath

  Public Sub New(fileSystem As IFileSystemProvider)
    'If (emulatedRoot IsNot Nothing) Then
    '  emulatedRoot = AbsolutePath.RootDirectory
    'End If
    _FileSystem = fileSystem
    '_EmulatedRoot = emulatedRoot
    '_CurrentWorkingDir = _EmulatedRoot
  End Sub

  Public ReadOnly Property FileSystem As IFileSystemProvider
    Get
      Return _FileSystem
    End Get
  End Property

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="targetDirectoryName">fullPath or relativePath</param>
  Public Sub NavigateToSubDir(targetDirectoryName As String)
    ' _CurrentWorkingDir = _CurrentWorkingDir.
  End Sub

  Public Sub NavigateUp()
    ' Me.NavigateTo(_RelativeToParentDirectory & _DirectorySeparatorChar)
  End Sub

  Public Sub NavigateToRoot()
    '  Me.NavigateTo(_DirectorySeparatorChar)
  End Sub

  Public ReadOnly Property IsRoot As Boolean
    Get

    End Get
  End Property

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <value></value>
  ''' <returns>fullPath</returns>
  Public ReadOnly Property CurrentWorkingDir As AbsolutePath
    Get
      Return _CurrentWorkingDir
    End Get
  End Property

  Public Function GetRelativePathFor(fullPath As String) As String

  End Function

  Public Function GetFullPathFor(relativePath As String) As String

  End Function

End Class
