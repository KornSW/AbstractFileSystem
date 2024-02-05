Imports System.IO
Imports System.IO.AbstractFilesystem

Public Class HfsClientFactory
  Implements IAfsSourceProviderFactory

  Public ReadOnly Property Identifier As Guid Implements IAfsSourceProviderFactory.Identifier
    Get
      Return Guid.Parse("152AFEB7-AB86-469F-8FA2-790DFA03D559")
    End Get
  End Property

  Public ReadOnly Property DisplayName As String Implements IAfsSourceProviderFactory.DisplayName
    Get
      Return "HFS Connector"
    End Get
  End Property

  Public Function CreateConfigObject() As Object Implements IAfsSourceProviderFactory.CreateConfigObject
    Return New HfsClientConfiguration
  End Function

  Public Function InitializeProvider(configObject As Object) As IAfsSource Implements IAfsSourceProviderFactory.InitializeProvider
    With DirectCast(configObject, HfsClientConfiguration)
      Dim connection As New HfsConection(.Url)
      If (Not String.IsNullOrEmpty(.UserName)) Then
        connection.Authenticate(.UserName, .Password)
      End If
      Return connection
    End With
  End Function

End Class

Public Class HfsClientConfiguration

  Public Property Url As String = "http://myServerUrl"

  Public Property UserName As String = String.Empty

  Public Property Password As Security.SecureString = Nothing

End Class


Partial Class HfsConection
  Implements IAfsSource

  Public ReadOnly Property SourceIdentifier As String Implements IAfsSource.SourceIdentifier
    Get
      Throw New NotImplementedException()
    End Get
  End Property

  Public Function GetDirectories(ralativePath As String) As IEnumerable(Of AfsDirectoryInfo) Implements IAfsSource.GetDirectories













  End Function

  Public Function GetDirectories() As IEnumerable(Of AfsDirectoryInfo) Implements IAfsDirectoryInfo.GetDirectories
    Throw New NotImplementedException()
  End Function

  Public Function OpenRead(relativePath As String) As Stream Implements IAfsSource.OpenRead
    Throw New NotImplementedException()
  End Function

  Public Function OpenWrite(relativePath As String) As Stream Implements IAfsSource.OpenWrite
    Throw New NotImplementedException()
  End Function

  Public Function TestAccessability(relativePath As String) As Boolean Implements IAfsSource.TestAccessability
    Throw New NotImplementedException()
  End Function

  Public Function GetFileLastWriteTime(relativePath As String) As Date Implements IAfsSource.GetFileLastWriteTime
    Throw New NotImplementedException()
  End Function

  Public Function GetFileCreationTime(relativePath As String) As Date Implements IAfsSource.GetFileCreationTime
    Throw New NotImplementedException()
  End Function

  Public Function GetFileLength(relativePath As String) As Long Implements IAfsSource.GetFileLength
    Throw New NotImplementedException()
  End Function

  Public Function SearchFiles(startDirectory As String, mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo) Implements IAfsSource.SearchFiles
    Throw New NotImplementedException()
  End Function

  Public Function SearchFiles(mask As String, recursive As Boolean, limitResults As Integer) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.SearchFiles
    Throw New NotImplementedException()
  End Function

  Public Function GetSource() As IAfsSource Implements IAfsDirectoryInfo.GetSource
    Throw New NotImplementedException()
  End Function

  Public Function GetDirectory(relativePath As String) As AfsDirectoryInfo Implements IAfsDirectoryInfo.GetDirectory
    Throw New NotImplementedException()
  End Function

  Public Function GetFiles(relativePath As String) As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles
    Throw New NotImplementedException()
  End Function

  Public Function GetFiles() As IEnumerable(Of AfsFileInfo) Implements IAfsDirectoryInfo.GetFiles
    Throw New NotImplementedException()
  End Function

  Public Function GetFile(relativePath As String) As AfsFileInfo Implements IAfsDirectoryInfo.GetFile
    Throw New NotImplementedException()
  End Function
End Class
