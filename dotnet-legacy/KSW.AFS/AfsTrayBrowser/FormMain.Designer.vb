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
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
    Me.gTrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.gTrayMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.gBtnExit = New System.Windows.Forms.ToolStripMenuItem()
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.gDirectories = New System.Windows.Forms.AbstractFilesystem.DirectoryTree()
    Me.gFiles = New System.Windows.Forms.AbstractFilesystem.FileList()
    Me.gSplit = New System.Windows.Forms.SplitContainer()
    Me.gToolbar = New System.Windows.Forms.ToolStrip()
    Me.gBtnEditSettings = New System.Windows.Forms.ToolStripButton()
    Me.gTrayMenu.SuspendLayout()
    CType(Me.gSplit, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gSplit.Panel1.SuspendLayout()
    Me.gSplit.Panel2.SuspendLayout()
    Me.gSplit.SuspendLayout()
    Me.gToolbar.SuspendLayout()
    Me.SuspendLayout()
    '
    'gTrayIcon
    '
    Me.gTrayIcon.ContextMenuStrip = Me.gTrayMenu
    Me.gTrayIcon.Icon = CType(resources.GetObject("gTrayIcon.Icon"), System.Drawing.Icon)
    Me.gTrayIcon.Text = "AFS Tray Browser"
    Me.gTrayIcon.Visible = True
    '
    'gTrayMenu
    '
    Me.gTrayMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.gBtnExit})
    Me.gTrayMenu.Name = "gTrayMenu"
    Me.gTrayMenu.Size = New System.Drawing.Size(121, 26)
    '
    'gBtnExit
    '
    Me.gBtnExit.Name = "gBtnExit"
    Me.gBtnExit.Size = New System.Drawing.Size(120, 22)
    Me.gBtnExit.Text = "Beenden"
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "folder_open.png")
    Me.ImageList1.Images.SetKeyName(1, "lock.png")
    '
    'gDirectories
    '
    Me.gDirectories.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gDirectories.Location = New System.Drawing.Point(0, 0)
    Me.gDirectories.Name = "gDirectories"
    Me.gDirectories.Size = New System.Drawing.Size(239, 429)
    Me.gDirectories.TabIndex = 2
    '
    'gFiles
    '
    Me.gFiles.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gFiles.Location = New System.Drawing.Point(0, 0)
    Me.gFiles.Name = "gFiles"
    Me.gFiles.Size = New System.Drawing.Size(475, 429)
    Me.gFiles.TabIndex = 3
    '
    'gSplit
    '
    Me.gSplit.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gSplit.Location = New System.Drawing.Point(0, 25)
    Me.gSplit.Name = "gSplit"
    '
    'gSplit.Panel1
    '
    Me.gSplit.Panel1.Controls.Add(Me.gDirectories)
    '
    'gSplit.Panel2
    '
    Me.gSplit.Panel2.Controls.Add(Me.gFiles)
    Me.gSplit.Size = New System.Drawing.Size(718, 429)
    Me.gSplit.SplitterDistance = 239
    Me.gSplit.TabIndex = 4
    '
    'gToolbar
    '
    Me.gToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.gBtnEditSettings})
    Me.gToolbar.Location = New System.Drawing.Point(0, 0)
    Me.gToolbar.Name = "gToolbar"
    Me.gToolbar.Size = New System.Drawing.Size(718, 25)
    Me.gToolbar.TabIndex = 5
    Me.gToolbar.Text = "ToolStrip1"
    '
    'gBtnEditSettings
    '
    Me.gBtnEditSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnEditSettings.Image = CType(resources.GetObject("gBtnEditSettings.Image"), System.Drawing.Image)
    Me.gBtnEditSettings.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnEditSettings.Name = "gBtnEditSettings"
    Me.gBtnEditSettings.Size = New System.Drawing.Size(23, 22)
    Me.gBtnEditSettings.Text = "Edit Settings"
    '
    'FormMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(718, 454)
    Me.Controls.Add(Me.gSplit)
    Me.Controls.Add(Me.gToolbar)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.Name = "FormMain"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "AFS Browser"
    Me.gTrayMenu.ResumeLayout(False)
    Me.gSplit.Panel1.ResumeLayout(False)
    Me.gSplit.Panel2.ResumeLayout(False)
    CType(Me.gSplit, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gSplit.ResumeLayout(False)
    Me.gToolbar.ResumeLayout(False)
    Me.gToolbar.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents gTrayIcon As System.Windows.Forms.NotifyIcon
  Friend WithEvents gTrayMenu As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents gBtnExit As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents gDirectories As System.Windows.Forms.AbstractFilesystem.DirectoryTree
  Friend WithEvents gFiles As System.Windows.Forms.AbstractFilesystem.FileList
  Friend WithEvents gSplit As System.Windows.Forms.SplitContainer
  Friend WithEvents gToolbar As System.Windows.Forms.ToolStrip
  Friend WithEvents gBtnEditSettings As System.Windows.Forms.ToolStripButton

End Class
