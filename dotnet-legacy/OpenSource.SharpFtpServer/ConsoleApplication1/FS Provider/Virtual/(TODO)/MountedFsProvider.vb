
'Public Class MountedFsProvider
'  Inherits FsProxy

'  Private _MountPoint As String
'  Private _SubSource As IFileSystemProvider

'  Public Sub New(source As IFileSystemProvider, mountPoint As String, subSource As IFileSystemProvider)
'    MyBase.New(source)
'    _MountPoint = mountPoint.ToLower()
'    _SubSource = subSource
'    MyBase.CurrentWorkingFolder = source.CurrentWorkingFolder
'  End Sub

'  Protected ReadOnly Property SubSource
'    Get
'      Return _SubSource
'    End Get
'  End Property

'  Private Function ResolvePath(ByRef path As String) As IFileSystemProvider
'    If (path.ToLower().StartsWith(_MountPoint)) Then
'      path = path.Substring(_MountPoint.Length, path.Length - _MountPoint.Length)
'      Return Me.SubSource
'    Else
'      Return Me.Source
'    End If
'  End Function

'  Public Overrides Function CreateFolder(pathName As String) As String
'    Return Me.ResolvePath(pathName).CreateDirectory(pathName)
'  End Function

'End Class
