<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PathNavigator
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PathNavigator))
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
    Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
    Me.ToolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton()
    Me.ToolStripDropDownButton3 = New System.Windows.Forms.ToolStripDropDownButton()
    Me.TextBox1 = New System.Windows.Forms.TextBox()
    Me.ToolStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ToolStrip1.AutoSize = False
    Me.ToolStrip1.BackColor = System.Drawing.Color.White
    Me.ToolStrip1.CanOverflow = False
    Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
    Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(10, 10)
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripDropDownButton2, Me.ToolStripDropDownButton3})
    Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 1)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(592, 19)
    Me.ToolStrip1.TabIndex = 0
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'ToolStripDropDownButton1
    '
    Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
    Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
    Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(62, 17)
    Me.ToolStripDropDownButton1.Text = "Folder 1"
    '
    'ToolStripDropDownButton2
    '
    Me.ToolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.ToolStripDropDownButton2.Image = CType(resources.GetObject("ToolStripDropDownButton2.Image"), System.Drawing.Image)
    Me.ToolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
    Me.ToolStripDropDownButton2.Size = New System.Drawing.Size(62, 17)
    Me.ToolStripDropDownButton2.Text = "Folder 2"
    '
    'ToolStripDropDownButton3
    '
    Me.ToolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.ToolStripDropDownButton3.Image = CType(resources.GetObject("ToolStripDropDownButton3.Image"), System.Drawing.Image)
    Me.ToolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripDropDownButton3.Name = "ToolStripDropDownButton3"
    Me.ToolStripDropDownButton3.Size = New System.Drawing.Size(62, 17)
    Me.ToolStripDropDownButton3.Text = "Folder 3"
    '
    'TextBox1
    '
    Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TextBox1.Location = New System.Drawing.Point(0, 0)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(592, 22)
    Me.TextBox1.TabIndex = 1
    '
    'PathNavigator
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.ToolStrip1)
    Me.Controls.Add(Me.TextBox1)
    Me.Name = "PathNavigator"
    Me.Size = New System.Drawing.Size(592, 22)
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents ToolStripDropDownButton2 As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents ToolStripDropDownButton3 As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
