Imports System.Collections.Generic
Imports System

Public Class AfsVirtualDirectory
  Implements IVirtualDirectory




  Private _VirtualSubDirectories As New Dictionary(Of String, AfsVirtualDirectory)
  Public ReadOnly Property VirtualSubDirectories As Dictionary(Of String, AfsVirtualDirectory) Implements IVirtualDirectory.VirtualSubDirectories
    Get
      Return _VirtualSubDirectories
    End Get
  End Property

  Public Sub UnmnountAll()



  End Sub

  Public Sub MountByConfig(config As AfsVirtualDirectoryConfig, Optional keepExisting As Boolean = True)
    If (Not keepExisting) Then
      Me.UnmnountAll()
    End If


    For Each v In config.SubDirs

    Next








  End Sub

  Public Property MountedSource As IAfsSource Implements IVirtualDirectory.MountedSource

















  Public Function GetDirectories() As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories

  End Function

  Public Function GetDirectories(relativePath As String) As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories

  End Function

  Public Function GetDirectory(relativePath As String) As AfsDirectoryInfo Implements IAfsDirectoryInfo.GetDirectory

  End Function

  Public Function GetFile(relativePath As String) As AfsFileInfo Implements IAfsDirectoryInfo.GetFile

  End Function

  Public Function GetFiles() As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles

  End Function

  Public Function GetFiles(relativePath As String) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles

  End Function

  Public Function GetSource() As IAfsSource Implements IAfsDirectoryInfo.GetSource

  End Function

  Public Function OpenRead(relativePath As String) As Stream Implements IAfsSource.OpenRead

  End Function

  Public Function OpenWrite(relativePath As String) As Stream Implements IAfsSource.OpenWrite

  End Function

  Public ReadOnly Property SourceIdentifier As String Implements IAfsSource.SourceIdentifier
    Get

    End Get
  End Property

  Public Function TestAccessability(relativePath As String) As Boolean Implements IAfsSource.TestAccessability

  End Function

  Public Function GetFileCreationTime(relativePath As String) As Date Implements IAfsSource.GetFileCreationTime

  End Function

  Public Function GetFileLastWriteTime(relativePath As String) As Date Implements IAfsSource.GetFileLastWriteTime

  End Function

  Public Function GetFileLength(relativePath As String) As Long Implements IAfsSource.GetFileLength

  End Function

  Public Function SearchFiles(mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.SearchFiles

  End Function

  Public Function SearchFiles(startDirectory As String, mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo) Implements IAfsSource.SearchFiles

  End Function

End Class
