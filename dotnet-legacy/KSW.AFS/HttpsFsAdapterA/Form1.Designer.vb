<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
    Me.TextBox1 = New System.Windows.Forms.TextBox()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
    Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
    Me.Favoriten = New System.Windows.Forms.ToolStripDropDownButton()
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
    Me.ZuFavoritenHinzufügenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
    Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
    Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.TreeView1 = New System.Windows.Forms.TreeView()
    Me.ToolStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TextBox1
    '
    Me.TextBox1.Location = New System.Drawing.Point(758, 17)
    Me.TextBox1.Multiline = True
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.TextBox1.Size = New System.Drawing.Size(93, 518)
    Me.TextBox1.TabIndex = 0
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(516, 277)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(95, 25)
    Me.Button1.TabIndex = 1
    Me.Button1.Text = "Button1"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.Favoriten, Me.ToolStripButton1, Me.ToolStripButton3, Me.ToolStripButton2})
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(945, 25)
    Me.ToolStrip1.TabIndex = 2
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'ToolStripDropDownButton1
    '
    Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
    Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
    Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(95, 22)
    Me.ToolStripDropDownButton1.Text = "Verbindungen"
    '
    'Favoriten
    '
    Me.Favoriten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.Favoriten.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ZuFavoritenHinzufügenToolStripMenuItem})
    Me.Favoriten.Image = CType(resources.GetObject("Favoriten.Image"), System.Drawing.Image)
    Me.Favoriten.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.Favoriten.Name = "Favoriten"
    Me.Favoriten.Size = New System.Drawing.Size(69, 22)
    Me.Favoriten.Text = "Favoriten"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(200, 6)
    '
    'ZuFavoritenHinzufügenToolStripMenuItem
    '
    Me.ZuFavoritenHinzufügenToolStripMenuItem.Name = "ZuFavoritenHinzufügenToolStripMenuItem"
    Me.ZuFavoritenHinzufügenToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
    Me.ZuFavoritenHinzufügenToolStripMenuItem.Text = "zu Favoriten Hinzufügen"
    '
    'ToolStripButton1
    '
    Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
    Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton1.Name = "ToolStripButton1"
    Me.ToolStripButton1.Size = New System.Drawing.Size(85, 22)
    Me.ToolStripButton1.Text = "nach Oben"
    '
    'ToolStripButton3
    '
    Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
    Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton3.Name = "ToolStripButton3"
    Me.ToolStripButton3.Size = New System.Drawing.Size(115, 22)
    Me.ToolStripButton3.Text = "Datei Hochladen"
    '
    'ToolStripButton2
    '
    Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
    Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton2.Name = "ToolStripButton2"
    Me.ToolStripButton2.Size = New System.Drawing.Size(143, 22)
    Me.ToolStripButton2.Text = "Ordner Herunterladen"
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(32, 32)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'NotifyIcon1
    '
    Me.NotifyIcon1.Text = "NotifyIcon1"
    Me.NotifyIcon1.Visible = True
    '
    'TreeView1
    '
    Me.TreeView1.Location = New System.Drawing.Point(70, 90)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.Size = New System.Drawing.Size(121, 97)
    Me.TreeView1.TabIndex = 5
    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(945, 609)
    Me.Controls.Add(Me.TreeView1)
    Me.Controls.Add(Me.ToolStrip1)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.TextBox1)
    Me.Name = "Form1"
    Me.Text = "Form1"
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents Favoriten As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ZuFavoritenHinzufügenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
  Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
  Friend WithEvents CollectionViewControl1 As KSW.Base.Ui.WinForms.CollectionViewControl
  Friend WithEvents TreeView1 As System.Windows.Forms.TreeView

End Class
