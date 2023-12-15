
Public Class AfsEnvironment

  Private Sub New()
  End Sub


  Public Shared Property DefaultProviderFactory As IAfsSourceProviderFactory


  Public Shared Function FindSourceForUri(afsUri As AfsUri) As IAfsSource
    Return FindSourceForUri(afsUri.AfsSourceIdentifier)
  End Function

  Public Shared Function FindSourceForUri(afsSourceIdentifier As String) As IAfsSource

    If (afsSourceIdentifier.Length = 2 AndAlso afsSourceIdentifier(1) = ":"c) Then
      Dim cfg = DefaultProviderFactory.CreateConfigObject()
      Return DefaultProviderFactory.InitializeProvider(cfg)
    End If


    Return Nothing
  End Function

End Class
