<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileBrowserDialog
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
    Me.gPanButtons = New System.Windows.Forms.Panel()
    Me.gBtnCancel = New System.Windows.Forms.Button()
    Me.gBtnOk = New System.Windows.Forms.Button()
    Me.gFileBrowser = New System.Windows.Forms.AbstractFilesystem.FileBrowser()
    Me.gPanButtons.SuspendLayout()
    Me.SuspendLayout()
    '
    'gPanButtons
    '
    Me.gPanButtons.Controls.Add(Me.gBtnCancel)
    Me.gPanButtons.Controls.Add(Me.gBtnOk)
    Me.gPanButtons.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.gPanButtons.Location = New System.Drawing.Point(0, 421)
    Me.gPanButtons.Name = "gPanButtons"
    Me.gPanButtons.Size = New System.Drawing.Size(734, 41)
    Me.gPanButtons.TabIndex = 1
    '
    'gBtnCancel
    '
    Me.gBtnCancel.Location = New System.Drawing.Point(627, 3)
    Me.gBtnCancel.Name = "gBtnCancel"
    Me.gBtnCancel.Size = New System.Drawing.Size(96, 30)
    Me.gBtnCancel.TabIndex = 0
    Me.gBtnCancel.Text = "CANCEL"
    Me.gBtnCancel.UseVisualStyleBackColor = True
    '
    'gBtnOk
    '
    Me.gBtnOk.Location = New System.Drawing.Point(517, 3)
    Me.gBtnOk.Name = "gBtnOk"
    Me.gBtnOk.Size = New System.Drawing.Size(96, 30)
    Me.gBtnOk.TabIndex = 0
    Me.gBtnOk.Text = "OK"
    Me.gBtnOk.UseVisualStyleBackColor = True
    '
    'gFileBrowser
    '
    Me.gFileBrowser.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gFileBrowser.Location = New System.Drawing.Point(0, 0)
    Me.gFileBrowser.Name = "gFileBrowser"
    Me.gFileBrowser.Size = New System.Drawing.Size(734, 421)
    Me.gFileBrowser.TabIndex = 0
    '
    'FileBrowserDialog
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(734, 462)
    Me.Controls.Add(Me.gFileBrowser)
    Me.Controls.Add(Me.gPanButtons)
    Me.MinimumSize = New System.Drawing.Size(450, 350)
    Me.Name = "FileBrowserDialog"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Browse File"
    Me.gPanButtons.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents gFileBrowser As FileBrowser
  Friend WithEvents gPanButtons As System.Windows.Forms.Panel
  Friend WithEvents gBtnCancel As System.Windows.Forms.Button
  Friend WithEvents gBtnOk As System.Windows.Forms.Button
End Class
