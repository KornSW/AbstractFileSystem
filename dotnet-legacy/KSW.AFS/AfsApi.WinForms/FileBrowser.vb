
Public Class FileBrowser

  Public Property FileName As String
    Get
      Return gCboFileName.Text
    End Get
    Set(value As String)
      gCboFileName.Text = value
    End Set
  End Property

End Class
