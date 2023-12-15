'Imports System
'Imports System.Drawing
'Imports System.Drawing.Imaging
'Imports System.IO

'Public Class CachingFsProxy
'  Inherits FsProxy

'#Region " Declarations & Constructor "

'  Private _Cache As IFileSystemProvider

'  Public Sub New(source As IFileSystemProvider, cache As DirectoryInfo)
'    Me.New(source, New LocalFsProvider(cache.FullName))
'  End Sub

'  Public Sub New(source As IFileSystemProvider, cache As IFileSystemProvider)
'    MyBase.New(source)
'    _Cache = cache
'  End Sub

'#End Region

'#Region " Properties "

'  Public ReadOnly Property Cache As IFileSystemProvider
'    Get
'      Return _Cache
'    End Get
'  End Property

'#End Region

'#Region " Methods "

'  Public Overrides Function OpenFileForRead(pathName As AbsolutePath) As Stream
'    Dim originalModificationTime As DateTime = MyBase.FileModificationTime(pathName)

'    If (Not Me.CacheIsUptodate(pathName, originalModificationTime)) Then
'      If (Not Me.UpdateCache(pathName, originalModificationTime)) Then
'        Return MyBase.OpenFileForRead(pathName)
'      End If
'    End If

'    Return _Cache.OpenFileForRead(pathName)
'  End Function

'  Private Function CacheIsUptodate(pathName As AbsolutePath, originalModificationTime As DateTime) As Boolean
'    Dim cachedModificationTime As DateTime

'    If (_Cache.FileExists(pathName)) Then
'      cachedModificationTime = _Cache.FileModificationTime(pathName)
'      Return (cachedModificationTime = originalModificationTime)
'    End If

'    Return False
'  End Function

'  Private Function UpdateCache(pathName As AbsolutePath, modificationTime As DateTime) As Boolean

'    If (_Cache.FileExists(pathName)) Then
'      If (Not _Cache.DeleteFile(pathName)) Then
'        Return False
'      End If
'    End If

'    Using src = MyBase.OpenFileForRead(pathName), tgt = _Cache.OpenFileForWrite(pathName)
'      src.CopyTo(tgt)
'    End Using

'    Return _Cache.SetFileModificationTime(modificationTime)
'  End Function

'#End Region

'End Class
