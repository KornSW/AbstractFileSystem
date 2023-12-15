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

  Public Function GetDirectories(ralativePath As String) As IEnumerable(Of AfsDirectoryInfo) Implements IAfsSource.GetDirectories













  End Function

End Class
