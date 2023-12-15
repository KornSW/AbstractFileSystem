<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileBrowser
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FileBrowser))
    Me.gSplitMain = New System.Windows.Forms.SplitContainer()
    Me.gToolbar = New System.Windows.Forms.ToolStrip()
    Me.gPanFooter = New System.Windows.Forms.Panel()
    Me.gSep1 = New System.Windows.Forms.ToolStripSeparator()
    Me.gBtnNewFolder = New System.Windows.Forms.ToolStripButton()
    Me.gBtnNavigateUp = New System.Windows.Forms.ToolStripButton()
    Me.gSep2 = New System.Windows.Forms.ToolStripSeparator()
    Me.gBtnOrganize = New System.Windows.Forms.ToolStripDropDownButton()
    Me.gBtnManageFs = New System.Windows.Forms.ToolStripMenuItem()
    Me.gBtnView = New System.Windows.Forms.ToolStripDropDownButton()
    Me.gBtnTogglePreview = New System.Windows.Forms.ToolStripButton()
    Me.gSplitPreview = New System.Windows.Forms.SplitContainer()
    Me.gCboFileName = New System.Windows.Forms.ComboBox()
    Me.gCboFileExtension = New System.Windows.Forms.ComboBox()
    Me.gPanHeader = New System.Windows.Forms.SplitContainer()
    Me.gSearch = New System.Windows.Forms.TextBox()
    Me.gLblFileName = New System.Windows.Forms.Label()
    Me.gFolders = New DirectoryTree()
    Me.gFiles = New FileList()
    Me.gPreview = New FilePreview()
    Me.gPath = New PathNavigator()
    CType(Me.gSplitMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gSplitMain.Panel1.SuspendLayout()
    Me.gSplitMain.Panel2.SuspendLayout()
    Me.gSplitMain.SuspendLayout()
    Me.gToolbar.SuspendLayout()
    Me.gPanFooter.SuspendLayout()
    CType(Me.gSplitPreview, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gSplitPreview.Panel1.SuspendLayout()
    Me.gSplitPreview.Panel2.SuspendLayout()
    Me.gSplitPreview.SuspendLayout()
    CType(Me.gPanHeader, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gPanHeader.Panel1.SuspendLayout()
    Me.gPanHeader.Panel2.SuspendLayout()
    Me.gPanHeader.SuspendLayout()
    Me.SuspendLayout()
    '
    'gSplitMain
    '
    Me.gSplitMain.BackColor = System.Drawing.SystemColors.ActiveBorder
    Me.gSplitMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gSplitMain.Location = New System.Drawing.Point(0, 59)
    Me.gSplitMain.Name = "gSplitMain"
    '
    'gSplitMain.Panel1
    '
    Me.gSplitMain.Panel1.Controls.Add(Me.gFolders)
    '
    'gSplitMain.Panel2
    '
    Me.gSplitMain.Panel2.Controls.Add(Me.gSplitPreview)
    Me.gSplitMain.Size = New System.Drawing.Size(800, 404)
    Me.gSplitMain.SplitterDistance = 231
    Me.gSplitMain.SplitterWidth = 2
    Me.gSplitMain.TabIndex = 2
    '
    'gToolbar
    '
    Me.gToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.gToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.gToolbar.ImageScalingSize = New System.Drawing.Size(24, 24)
    Me.gToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.gBtnOrganize, Me.gSep1, Me.gBtnNewFolder, Me.gSep2, Me.gBtnNavigateUp, Me.gBtnTogglePreview, Me.gBtnView})
    Me.gToolbar.Location = New System.Drawing.Point(0, 28)
    Me.gToolbar.Name = "gToolbar"
    Me.gToolbar.Size = New System.Drawing.Size(800, 31)
    Me.gToolbar.TabIndex = 4
    Me.gToolbar.Text = "ToolStrip1"
    '
    'gPanFooter
    '
    Me.gPanFooter.Controls.Add(Me.gLblFileName)
    Me.gPanFooter.Controls.Add(Me.gCboFileExtension)
    Me.gPanFooter.Controls.Add(Me.gCboFileName)
    Me.gPanFooter.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.gPanFooter.Location = New System.Drawing.Point(0, 463)
    Me.gPanFooter.Name = "gPanFooter"
    Me.gPanFooter.Size = New System.Drawing.Size(800, 37)
    Me.gPanFooter.TabIndex = 5
    '
    'gSep1
    '
    Me.gSep1.Name = "gSep1"
    Me.gSep1.Size = New System.Drawing.Size(6, 31)
    '
    'gBtnNewFolder
    '
    Me.gBtnNewFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.gBtnNewFolder.Image = CType(resources.GetObject("gBtnNewFolder.Image"), System.Drawing.Image)
    Me.gBtnNewFolder.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnNewFolder.Name = "gBtnNewFolder"
    Me.gBtnNewFolder.Size = New System.Drawing.Size(83, 28)
    Me.gBtnNewFolder.Text = "Neuer Ordner"
    '
    'gBtnNavigateUp
    '
    Me.gBtnNavigateUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnNavigateUp.Image = CType(resources.GetObject("gBtnNavigateUp.Image"), System.Drawing.Image)
    Me.gBtnNavigateUp.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnNavigateUp.Name = "gBtnNavigateUp"
    Me.gBtnNavigateUp.Size = New System.Drawing.Size(28, 28)
    Me.gBtnNavigateUp.Text = "nach Oben"
    '
    'gSep2
    '
    Me.gSep2.Name = "gSep2"
    Me.gSep2.Size = New System.Drawing.Size(6, 31)
    '
    'gBtnOrganize
    '
    Me.gBtnOrganize.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.gBtnManageFs})
    Me.gBtnOrganize.Image = CType(resources.GetObject("gBtnOrganize.Image"), System.Drawing.Image)
    Me.gBtnOrganize.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnOrganize.Name = "gBtnOrganize"
    Me.gBtnOrganize.Size = New System.Drawing.Size(111, 28)
    Me.gBtnOrganize.Text = "Organisieren"
    '
    'gBtnManageFs
    '
    Me.gBtnManageFs.Name = "gBtnManageFs"
    Me.gBtnManageFs.Size = New System.Drawing.Size(152, 22)
    Me.gBtnManageFs.Text = "Dateisysteme"
    '
    'gBtnView
    '
    Me.gBtnView.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
    Me.gBtnView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnView.Image = CType(resources.GetObject("gBtnView.Image"), System.Drawing.Image)
    Me.gBtnView.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnView.Name = "gBtnView"
    Me.gBtnView.Size = New System.Drawing.Size(37, 28)
    Me.gBtnView.Text = "Ansicht"
    '
    'gBtnTogglePreview
    '
    Me.gBtnTogglePreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
    Me.gBtnTogglePreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.gBtnTogglePreview.Image = CType(resources.GetObject("gBtnTogglePreview.Image"), System.Drawing.Image)
    Me.gBtnTogglePreview.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.gBtnTogglePreview.Name = "gBtnTogglePreview"
    Me.gBtnTogglePreview.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
    Me.gBtnTogglePreview.Size = New System.Drawing.Size(28, 28)
    Me.gBtnTogglePreview.Text = "Vorschaufenseter"
    '
    'gSplitPreview
    '
    Me.gSplitPreview.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gSplitPreview.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
    Me.gSplitPreview.Location = New System.Drawing.Point(0, 0)
    Me.gSplitPreview.Name = "gSplitPreview"
    '
    'gSplitPreview.Panel1
    '
    Me.gSplitPreview.Panel1.Controls.Add(Me.gFiles)
    '
    'gSplitPreview.Panel2
    '
    Me.gSplitPreview.Panel2.Controls.Add(Me.gPreview)
    Me.gSplitPreview.Panel2Collapsed = True
    Me.gSplitPreview.Size = New System.Drawing.Size(567, 404)
    Me.gSplitPreview.SplitterDistance = 346
    Me.gSplitPreview.SplitterWidth = 2
    Me.gSplitPreview.TabIndex = 2
    '
    'gCboFileName
    '
    Me.gCboFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gCboFileName.FormattingEnabled = True
    Me.gCboFileName.Location = New System.Drawing.Point(72, 13)
    Me.gCboFileName.Name = "gCboFileName"
    Me.gCboFileName.Size = New System.Drawing.Size(506, 21)
    Me.gCboFileName.TabIndex = 0
    '
    'gCboFileExtension
    '
    Me.gCboFileExtension.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gCboFileExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.gCboFileExtension.FormattingEnabled = True
    Me.gCboFileExtension.Location = New System.Drawing.Point(584, 13)
    Me.gCboFileExtension.Name = "gCboFileExtension"
    Me.gCboFileExtension.Size = New System.Drawing.Size(205, 21)
    Me.gCboFileExtension.TabIndex = 0
    '
    'gPanHeader
    '
    Me.gPanHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.gPanHeader.Dock = System.Windows.Forms.DockStyle.Top
    Me.gPanHeader.Location = New System.Drawing.Point(0, 0)
    Me.gPanHeader.Name = "gPanHeader"
    '
    'gPanHeader.Panel1
    '
    Me.gPanHeader.Panel1.Controls.Add(Me.gPath)
    '
    'gPanHeader.Panel2
    '
    Me.gPanHeader.Panel2.Controls.Add(Me.gSearch)
    Me.gPanHeader.Size = New System.Drawing.Size(800, 28)
    Me.gPanHeader.SplitterDistance = 633
    Me.gPanHeader.TabIndex = 2
    '
    'gSearch
    '
    Me.gSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gSearch.Location = New System.Drawing.Point(0, 3)
    Me.gSearch.Name = "gSearch"
    Me.gSearch.Size = New System.Drawing.Size(160, 20)
    Me.gSearch.TabIndex = 0
    '
    'gLblFileName
    '
    Me.gLblFileName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.gLblFileName.AutoSize = True
    Me.gLblFileName.Location = New System.Drawing.Point(8, 16)
    Me.gLblFileName.Name = "gLblFileName"
    Me.gLblFileName.Size = New System.Drawing.Size(58, 13)
    Me.gLblFileName.TabIndex = 1
    Me.gLblFileName.Text = "Dateiname"
    '
    'gFolders
    '
    Me.gFolders.BackColor = System.Drawing.SystemColors.Window
    Me.gFolders.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gFolders.Location = New System.Drawing.Point(0, 0)
    Me.gFolders.Name = "gFolders"
    Me.gFolders.Size = New System.Drawing.Size(231, 404)
    Me.gFolders.TabIndex = 0
    '
    'gFiles
    '
    Me.gFiles.BackColor = System.Drawing.SystemColors.Window
    Me.gFiles.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gFiles.Location = New System.Drawing.Point(0, 0)
    Me.gFiles.Name = "gFiles"
    Me.gFiles.Size = New System.Drawing.Size(567, 404)
    Me.gFiles.TabIndex = 1
    '
    'gPreview
    '
    Me.gPreview.Dock = System.Windows.Forms.DockStyle.Fill
    Me.gPreview.Location = New System.Drawing.Point(0, 0)
    Me.gPreview.Name = "gPreview"
    Me.gPreview.Size = New System.Drawing.Size(219, 385)
    Me.gPreview.TabIndex = 0
    '
    'gPath
    '
    Me.gPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gPath.Location = New System.Drawing.Point(3, 3)
    Me.gPath.Name = "gPath"
    Me.gPath.Size = New System.Drawing.Size(630, 20)
    Me.gPath.TabIndex = 0
    '
    'FileBrowser
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.gSplitMain)
    Me.Controls.Add(Me.gPanFooter)
    Me.Controls.Add(Me.gToolbar)
    Me.Controls.Add(Me.gPanHeader)
    Me.Name = "FileBrowser"
    Me.Size = New System.Drawing.Size(800, 500)
    Me.gSplitMain.Panel1.ResumeLayout(False)
    Me.gSplitMain.Panel2.ResumeLayout(False)
    CType(Me.gSplitMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gSplitMain.ResumeLayout(False)
    Me.gToolbar.ResumeLayout(False)
    Me.gToolbar.PerformLayout()
    Me.gPanFooter.ResumeLayout(False)
    Me.gPanFooter.PerformLayout()
    Me.gSplitPreview.Panel1.ResumeLayout(False)
    Me.gSplitPreview.Panel2.ResumeLayout(False)
    CType(Me.gSplitPreview, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gSplitPreview.ResumeLayout(False)
    Me.gPanHeader.Panel1.ResumeLayout(False)
    Me.gPanHeader.Panel2.ResumeLayout(False)
    Me.gPanHeader.Panel2.PerformLayout()
    CType(Me.gPanHeader, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gPanHeader.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents gFolders As DirectoryTree
  Friend WithEvents gFiles As FileList
  Friend WithEvents gSplitMain As System.Windows.Forms.SplitContainer
  Friend WithEvents gToolbar As System.Windows.Forms.ToolStrip
  Friend WithEvents gPanFooter As System.Windows.Forms.Panel
  Friend WithEvents gSplitPreview As System.Windows.Forms.SplitContainer
  Friend WithEvents gBtnOrganize As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents gBtnManageFs As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents gSep1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents gBtnNewFolder As System.Windows.Forms.ToolStripButton
  Friend WithEvents gSep2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents gBtnNavigateUp As System.Windows.Forms.ToolStripButton
  Friend WithEvents gBtnTogglePreview As System.Windows.Forms.ToolStripButton
  Friend WithEvents gBtnView As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents gPreview As FilePreview
  Friend WithEvents gCboFileExtension As System.Windows.Forms.ComboBox
  Friend WithEvents gCboFileName As System.Windows.Forms.ComboBox
  Friend WithEvents gPanHeader As System.Windows.Forms.SplitContainer
  Friend WithEvents gPath As PathNavigator
  Friend WithEvents gSearch As System.Windows.Forms.TextBox
  Friend WithEvents gLblFileName As System.Windows.Forms.Label

End Class
