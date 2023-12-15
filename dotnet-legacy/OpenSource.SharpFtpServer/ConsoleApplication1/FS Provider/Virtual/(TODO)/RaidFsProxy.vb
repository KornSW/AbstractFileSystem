'Imports System.IO
'Imports System.Drawing
'Imports System.Drawing.Imaging
'Imports System.Collections.Generic

'Public Class RaidFsProxy
'  Implements IFileSystemProvider


'#Region " Declarations & Constructor "

'  Public Enum RaidLevels
'    Raid0 = 0
'    Raid1 = 1
'    Raid5 = 5
'  End Enum

'  Private _RaidLevel As RaidLevels
'  Private _Sources() As IFileSystemProvider

'  Public Sub New(raidLevel As RaidLevels, ParamArray sources() As IFileSystemProvider)
'    _RaidLevel = raidLevel
'    _Sources = sources
'    If (sources Is Nothing OrElse sources.Length < 1) Then
'      Throw New ArgumentException("Parameter 'sources' is not optional", "sources")
'    End If

'    Select Case _RaidLevel
'      Case RaidLevels.Raid0
'        If (sources.Length = 2) Then
'          Throw New ArgumentException("Raid 0 requires an exact count of 2 base filesystems.", "raidLevel")
'        End If
'      Case RaidLevels.Raid1
'        If (sources.Length = 2) Then
'          Throw New ArgumentException("Raid 1 requires an exact count of 2 base filesystems.", "raidLevel")
'        End If
'      Case RaidLevels.Raid5
'        If (sources.Length < 3) Then
'          Throw New ArgumentException("Raid 5 requires a minimum count of 3 base filesystems.", "raidLevel")
'        End If
'      Case Else
'        Throw New NotImplementedException()
'    End Select
'  End Sub

'#End Region

'#Region " Raid Operations "

'  Public ReadOnly Property RaidLevel As RaidLevels
'    Get
'      Return _RaidLevel
'    End Get
'  End Property

'  Public Function Verify() As AbsolutePath()
'    Select Case Me.RaidLevel
'      Case RaidLevels.Raid0 : Throw New InvalidOperationException("Verification is not possible an a Raid 0.")
'      Case RaidLevels.Raid1 : Return Me.VerifyRaid1
'      Case RaidLevels.Raid5 : Return Me.VerifyRaid5
'      Case Else : Throw New NotImplementedException
'    End Select
'  End Function

'  Public Sub Rebuild(fileSystemIndex As String)
'    Select Case Me.RaidLevel
'      Case RaidLevels.Raid0 : Throw New InvalidOperationException("Rebuild is not possible an a Raid 0.")
'      Case RaidLevels.Raid1 : Me.RebuildRaid1(fileSystemIndex)
'      Case RaidLevels.Raid5 : Me.RebuildRaid5(fileSystemIndex)
'      Case Else : Throw New NotImplementedException
'    End Select
'  End Sub

'  Private Function VerifyRaid1() As AbsolutePath()

'  End Function

'  Private Function VerifyRaid5() As AbsolutePath()

'  End Function

'  Private Sub RebuildRaid1(fileSystemIndex As String)

'  End Sub

'  Private Sub RebuildRaid5(fileSystemIndex As String)

'  End Sub

'#End Region

'  Public Function CreateDirectory(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.CreateDirectory

'  End Function

'  Public Function DeleteDirectory(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteDirectory

'  End Function

'  Public Function DeleteFile(filePath As AbsolutePath) As Boolean Implements IFileSystemProvider.DeleteFile

'  End Function

'  Public Function FileExists(filePath As AbsolutePath) As Boolean Implements IFileSystemProvider.FileExists

'  End Function

'  Public Function GetDirectoryModificationTime(directoryPath As AbsolutePath) As Date Implements IFileSystemProvider.GetDirectoryModificationTime

'  End Function

'  Public Function GetDirectoryNames(directoryPath As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetDirectoryNames

'  End Function

'  Public Function GetFileModificationTime(filePath As AbsolutePath) As Date Implements IFileSystemProvider.GetFileModificationTime

'  End Function

'  Public Function GetFileNames(directoryPath As AbsolutePath) As IEnumerable(Of String) Implements IFileSystemProvider.GetFileNames

'  End Function

'  Public Function GetFileSize(filePath As AbsolutePath) As Long Implements IFileSystemProvider.GetFileSize

'  End Function

'  Public Function OpenFileForAppend(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForAppend

'  End Function

'  Public Function OpenFileForRead(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForRead

'  End Function

'  Public Function OpenFileForWrite(filePath As AbsolutePath) As Stream Implements IFileSystemProvider.OpenFileForWrite

'  End Function

'  Public Function PathIsValid(directoryPath As AbsolutePath) As Boolean Implements IFileSystemProvider.PathIsValid

'  End Function

'  Public Function RenameFile(oldFilePath As AbsolutePath, newFilePath As AbsolutePath) As Boolean Implements IFileSystemProvider.RenameFile

'  End Function

'  Public Function SetFileModificationTime(filePath As AbsolutePath, newModificationTime As Date) As Boolean Implements IFileSystemProvider.SetFileModificationTime

'  End Function

'  Public Function GetDirectoryNames(directoryPath As AbsolutePath) As System.Collections.Generic.IEnumerable(Of String) Implements IFileSystemProvider.GetDirectoryNames

'  End Function

'  Public Function GetFileAttributes(filePath As AbsolutePath) As System.Collections.Generic.IEnumerable(Of IFileAttribute) Implements IFileSystemProvider.GetFileAttributes

'  End Function

'  Public Function GetFileNames(directoryPath As AbsolutePath) As System.Collections.Generic.IEnumerable(Of String) Implements IFileSystemProvider.GetFileNames

'  End Function
'End Class
