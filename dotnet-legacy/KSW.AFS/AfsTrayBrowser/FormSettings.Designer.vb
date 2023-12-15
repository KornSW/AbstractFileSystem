<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.gTxtUrl = New System.Windows.Forms.TextBox()
    Me.gLblUrl = New System.Windows.Forms.Label()
    Me.gBtnCancel = New System.Windows.Forms.Button()
    Me.gBtnOk = New System.Windows.Forms.Button()
    Me.gLblUser = New System.Windows.Forms.Label()
    Me.gTxtUser = New System.Windows.Forms.TextBox()
    Me.gLblPassword = New System.Windows.Forms.Label()
    Me.gTxtPassword = New System.Windows.Forms.TextBox()
    Me.SuspendLayout()
    '
    'gTxtUrl
    '
    Me.gTxtUrl.Location = New System.Drawing.Point(15, 25)
    Me.gTxtUrl.Name = "gTxtUrl"
    Me.gTxtUrl.Size = New System.Drawing.Size(304, 20)
    Me.gTxtUrl.TabIndex = 0
    '
    'gLblUrl
    '
    Me.gLblUrl.AutoSize = True
    Me.gLblUrl.Location = New System.Drawing.Point(12, 9)
    Me.gLblUrl.Name = "gLblUrl"
    Me.gLblUrl.Size = New System.Drawing.Size(29, 13)
    Me.gLblUrl.TabIndex = 1
    Me.gLblUrl.Text = "URL"
    '
    'gBtnCancel
    '
    Me.gBtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gBtnCancel.Location = New System.Drawing.Point(123, 140)
    Me.gBtnCancel.Name = "gBtnCancel"
    Me.gBtnCancel.Size = New System.Drawing.Size(95, 30)
    Me.gBtnCancel.TabIndex = 3
    Me.gBtnCancel.Text = "Cancel"
    Me.gBtnCancel.UseVisualStyleBackColor = True
    '
    'gBtnOk
    '
    Me.gBtnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gBtnOk.Location = New System.Drawing.Point(224, 140)
    Me.gBtnOk.Name = "gBtnOk"
    Me.gBtnOk.Size = New System.Drawing.Size(95, 30)
    Me.gBtnOk.TabIndex = 4
    Me.gBtnOk.Text = "OK"
    Me.gBtnOk.UseVisualStyleBackColor = True
    '
    'gLblUser
    '
    Me.gLblUser.AutoSize = True
    Me.gLblUser.Location = New System.Drawing.Point(12, 48)
    Me.gLblUser.Name = "gLblUser"
    Me.gLblUser.Size = New System.Drawing.Size(29, 13)
    Me.gLblUser.TabIndex = 1
    Me.gLblUser.Text = "User"
    '
    'gTxtUser
    '
    Me.gTxtUser.Location = New System.Drawing.Point(12, 64)
    Me.gTxtUser.Name = "gTxtUser"
    Me.gTxtUser.Size = New System.Drawing.Size(307, 20)
    Me.gTxtUser.TabIndex = 1
    '
    'gLblPassword
    '
    Me.gLblPassword.AutoSize = True
    Me.gLblPassword.Location = New System.Drawing.Point(12, 87)
    Me.gLblPassword.Name = "gLblPassword"
    Me.gLblPassword.Size = New System.Drawing.Size(53, 13)
    Me.gLblPassword.TabIndex = 1
    Me.gLblPassword.Text = "Password"
    '
    'gTxtPassword
    '
    Me.gTxtPassword.Location = New System.Drawing.Point(12, 103)
    Me.gTxtPassword.Name = "gTxtPassword"
    Me.gTxtPassword.Size = New System.Drawing.Size(307, 20)
    Me.gTxtPassword.TabIndex = 2
    Me.gTxtPassword.UseSystemPasswordChar = True
    '
    'FormSettings
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(331, 182)
    Me.Controls.Add(Me.gBtnOk)
    Me.Controls.Add(Me.gBtnCancel)
    Me.Controls.Add(Me.gLblPassword)
    Me.Controls.Add(Me.gLblUser)
    Me.Controls.Add(Me.gLblUrl)
    Me.Controls.Add(Me.gTxtPassword)
    Me.Controls.Add(Me.gTxtUser)
    Me.Controls.Add(Me.gTxtUrl)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FormSettings"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Settings"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents gTxtUrl As System.Windows.Forms.TextBox
  Friend WithEvents gLblUrl As System.Windows.Forms.Label
  Friend WithEvents gBtnCancel As System.Windows.Forms.Button
  Friend WithEvents gBtnOk As System.Windows.Forms.Button
  Friend WithEvents gLblUser As System.Windows.Forms.Label
  Friend WithEvents gTxtUser As System.Windows.Forms.TextBox
  Friend WithEvents gLblPassword As System.Windows.Forms.Label
  Friend WithEvents gTxtPassword As System.Windows.Forms.TextBox
End Class
