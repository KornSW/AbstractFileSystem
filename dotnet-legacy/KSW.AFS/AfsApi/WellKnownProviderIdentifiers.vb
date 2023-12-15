
Public NotInheritable Class WellKnownClientProviderIdentifiers
  Private Sub New()
  End Sub

  Public Shared ReadOnly Property LocalFilesystem As Guid
    Get
      Return Guid.Parse("780AE092-300B-4BF6-B75B-E56A0AAD9B78")
    End Get
  End Property



End Class
