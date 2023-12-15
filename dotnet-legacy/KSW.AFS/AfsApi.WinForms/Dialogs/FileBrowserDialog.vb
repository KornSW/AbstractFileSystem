
Public Class FileBrowserDialog

  Public Property Mode As FileDialogMode = FileDialogMode.OpenFile

  Public Property Filter As String
  Public Property FileName As String
  Public Property Title As String
  Public Property InitialDirectory As String
  Private Sub FileBrowserDialog_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    gFileBrowser.FileName = Me.FileName
  End Sub

  Private Sub gBtnOk_Click(sender As Object, e As EventArgs) Handles gBtnOk.Click
    Me.FileName = gFileBrowser.FileName
    Me.DialogResult = Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub gBtnCancel_Click(sender As Object, e As EventArgs) Handles gBtnCancel.Click
    Me.FileName = String.Empty
    Me.DialogResult = Forms.DialogResult.Cancel
    Me.Close()
  End Sub

End Class
Public Enum FileDialogMode
  OpenFile = 0
  SaveFile = 1
End Enum