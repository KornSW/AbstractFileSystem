Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

#Region " Factory "

Public Class LocalDriveFactory
  Implements IAfsSourceProviderFactory

  Public ReadOnly Property Identifier As Guid Implements IAfsSourceProviderFactory.Identifier
    Get
      Return WellKnownClientProviderIdentifiers.LocalFilesystem
    End Get
  End Property

  Public ReadOnly Property DisplayName As String Implements IAfsSourceProviderFactory.DisplayName
    Get
      Return "Local Dirve"
    End Get
  End Property

  Public Function CreateConfigObject() As Object Implements IAfsSourceProviderFactory.CreateConfigObject
    Return New LocalDriveConfiguration
  End Function

  Public Function InitializeProvider(configObject As Object) As IAfsSource Implements IAfsSourceProviderFactory.InitializeProvider
    With DirectCast(configObject, LocalDriveConfiguration)
      Return New LocalDriveProvider(.DriveLetter)
    End With
  End Function

End Class

Public Class LocalDriveConfiguration

  'favoritefolders (wie desktop) als dictionary

  Public Property DriveLetter As Char = "C"c

End Class

#End Region

Public Class LocalDriveProvider
  Implements IAfsSource


#Region " Declarations & Constructor "

  Private _Root As DriveInfo

  Public Sub New(driveLetter As Char)
    Me.New(New DriveInfo(driveLetter.ToString()))
  End Sub

  Public Sub New(drive As DriveInfo)
    _Root = drive
  End Sub

  Public ReadOnly Property SourceIdentifier As String Implements IAfsSource.SourceIdentifier
    Get
      Return _Root.Name
    End Get
  End Property

  Private Function GetSource() As IAfsSource Implements IAfsDirectoryInfo.GetSource
    Return Me
  End Function

#End Region

#Region " Directories "

  Public Function GetDirectories() As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories
    Return Me.GetDirectories(Path.DirectorySeparatorChar)
  End Function

  Private Function GetDirectories(parentDirectoryPath As String) As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories
    Dim result As New List(Of AfsDirectoryInfo)
    Dim physicalParentDirectory As New DirectoryInfo(Path.Combine(_Root.Name, parentDirectoryPath))
    For Each physicalDirectory In physicalParentDirectory.GetDirectories()
      result.Add(New AfsDirectoryInfo(Me, Path.Combine(parentDirectoryPath, physicalDirectory.Name)))
    Next
    Return result
  End Function

  Public Function GetDirectory(relativePath As String) As AfsDirectoryInfo Implements IAfsDirectoryInfo.GetDirectory
    ' Dim info As New AfsDirectoryInfo(Me, Path.Combine(parentDirectoryPath, relativePath))



  End Function

#End Region

#Region " Files "
  Public Function GetFiles() As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles
    Return Me.GetFiles(Path.DirectorySeparatorChar)
  End Function

  Public Function GetFiles(relativePath As String) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles
    Dim result As New List(Of AfsFileInfo)
    Try
      Dim physicalParentDirectory As New DirectoryInfo(Path.Combine(_Root.Name, relativePath))
      For Each physicalFile In physicalParentDirectory.GetFiles()
        result.Add(New AfsFileInfo(Me, Path.Combine(relativePath, physicalFile.Name)))
      Next
    Catch ex As UnauthorizedAccessException
    End Try
    Return result
  End Function

  Public Function GetFile(relativePath As String) As AfsFileInfo Implements IAfsDirectoryInfo.GetFile

    '  Return Me.



  End Function

  Public Function OpenRead(relativePath As String) As Stream Implements IAfsSource.OpenRead
    Return File.OpenRead(Path.Combine(_Root.Name, relativePath))
  End Function

  Public Function OpenWrite(relativePath As String) As Stream Implements IAfsSource.OpenWrite
    Return File.OpenWrite(Path.Combine(_Root.Name, relativePath))
  End Function

#End Region

  Public Function TestAccessability(relativePath As String) As Boolean Implements IAfsSource.TestAccessability
    Try
      Dim physicalParentDirectory As New DirectoryInfo(Path.Combine(_Root.Name, relativePath))
      physicalParentDirectory.EnumerateFiles()
    Catch ex As UnauthorizedAccessException
      Return False
    End Try
    Return True
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
