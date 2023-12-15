Imports System.Windows.Forms

Public Class FormSettings

  Private Sub FormSettings_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
    ' Me.DialogResult = DialogResult.Cancel
  End Sub

  Private Sub gBtnOk_Click(sender As Object, e As System.EventArgs) Handles gBtnOk.Click
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub gBtnCancel_Click(sender As Object, e As System.EventArgs) Handles gBtnCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub gTxtPassword_GotFocus(sender As Object, e As System.EventArgs) Handles gTxtPassword.GotFocus
    If (gTxtPassword.Tag IsNot Nothing) Then
      gTxtPassword.Text = ""
    End If
  End Sub

  Private Sub gTxtPassword_LostFocus(sender As Object, e As System.EventArgs) Handles gTxtPassword.LostFocus
    If (gTxtPassword.Tag IsNot Nothing) Then
      gTxtPassword.Text = gTxtPassword.Tag.ToString()
    End If
  End Sub

  Private Sub gTxtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles gTxtPassword.KeyDown
    gTxtPassword.Tag = Nothing
  End Sub

End Class