<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
    Me.gToolbar = New System.Windows.Forms.ToolStrip()
    Me.gBtnNew = New System.Windows.Forms.ToolStripButton()
    Me.gBtnOpen = New System.Windows.Forms.ToolStripButton()
    Me.gSep1 = New System.Windows.Forms.ToolStripSeparator()
    Me.gBtnSave = New System.Windows.Forms.ToolStripButton()
    Me.gBtnSaveAs = New System.Windows.Forms.ToolStripButton()
    Me.gSep2 = New System.Windows.Forms.ToolStripSeparator()
    Me.gTxtContent = New System.Windows.Forms.TextBox()
    Me.gToolbar.SuspendLayout()
    Me.SuspendLayout()
    '
    'gToolbar
    '
    Me.gToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.gBtnNew, Me.gBtnOpen, Me.gSep1, Me.gBtnSave, Me.gBtnSaveAs, Me.gSep2})
    Me.gToolbar.Location = New System.Drawing.Point(0, 0)
    Me.gToolbar.Name = "gToolbar"
    Me.gToolbar.Size = New System.Drawing.Size(584, 25)
    Me.gToolbar.TabIndex = 0
    Me.gToolbar.Text = "ToolStrip1"
    '
    'gBtnNew
    '
    Me.gBtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnNew.Image = CType(resources.GetObject("gBtnNew.Image"), System.Drawing.Image)
    Me.gBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnNew.Name = "gBtnNew"
    Me.gBtnNew.Size = New System.Drawing.Size(23, 22)
    Me.gBtnNew.Text = "Neu"
    '
    'gBtnOpen
    '
    Me.gBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnOpen.Image = CType(resources.GetObject("gBtnOpen.Image"), System.Drawing.Image)
    Me.gBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnOpen.Name = "gBtnOpen"
    Me.gBtnOpen.Size = New System.Drawing.Size(23, 22)
    Me.gBtnOpen.Text = "Öffnen"
    '
    'gSep1
    '
    Me.gSep1.Name = "gSep1"
    Me.gSep1.Size = New System.Drawing.Size(6, 25)
    '
    'gBtnSave
    '
    Me.gBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnSave.Image = CType(resources.GetObject("gBtnSave.Image"), System.Drawing.Image)
    Me.gBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnSave.Name = "gBtnSave"
    Me.gBtnSave.Size = New System.Drawing.Size(23, 22)
    Me.gBtnSave.Text = "Speichern"
    '
    'gBtnSaveAs
    '
    Me.gBtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnSaveAs.Image = CType(resources.GetObject("gBtnSaveAs.Image"), System.Drawing.Image)
    Me.gBtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnSaveAs.Name = "gBtnSaveAs"
    Me.gBtnSaveAs.Size = New System.Drawing.Size(23, 22)
    Me.gBtnSaveAs.Text = "Speichern Unter"
    '
    'gSep2
    '
    Me.gSep2.Name = "gSep2"
    Me.gSep2.Size = New System.Drawing.Size(6, 25)
    '
    'gTxtContent
    '
    Me.gTxtContent.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gTxtContent.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.gTxtContent.Location = New System.Drawing.Point(0, 25)
    Me.gTxtContent.Multiline = True
    Me.gTxtContent.Name = "gTxtContent"
    Me.gTxtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.gTxtContent.Size = New System.Drawing.Size(584, 437)
    Me.gTxtContent.TabIndex = 1
    '
    'FormMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(584, 462)
    Me.Controls.Add(Me.gTxtContent)
    Me.Controls.Add(Me.gToolbar)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "FormMain"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "AFS Notepad"
    Me.gToolbar.ResumeLayout(False)
    Me.gToolbar.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents gToolbar As System.Windows.Forms.ToolStrip
  Friend WithEvents gBtnNew As System.Windows.Forms.ToolStripButton
  Friend WithEvents gBtnOpen As System.Windows.Forms.ToolStripButton
  Friend WithEvents gSep1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents gBtnSave As System.Windows.Forms.ToolStripButton
  Friend WithEvents gBtnSaveAs As System.Windows.Forms.ToolStripButton
  Friend WithEvents gSep2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents gTxtContent As System.Windows.Forms.TextBox

End Class
