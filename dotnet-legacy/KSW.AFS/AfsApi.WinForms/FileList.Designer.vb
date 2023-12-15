<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileList
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
    Me.components = New System.ComponentModel.Container()
    Me.ListView1 = New System.Windows.Forms.ListView()
    Me.ILSmall = New System.Windows.Forms.ImageList(Me.components)
    Me.ILBig = New System.Windows.Forms.ImageList(Me.components)
    Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.SuspendLayout()
    '
    'ListView1
    '
    Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
    Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ListView1.HideSelection = False
    Me.ListView1.LargeImageList = Me.ILBig
    Me.ListView1.Location = New System.Drawing.Point(0, 0)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(644, 519)
    Me.ListView1.SmallImageList = Me.ILSmall
    Me.ListView1.TabIndex = 0
    Me.ListView1.UseCompatibleStateImageBehavior = False
    '
    'ILSmall
    '
    Me.ILSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.ILSmall.ImageSize = New System.Drawing.Size(16, 16)
    Me.ILSmall.TransparentColor = System.Drawing.Color.Transparent
    '
    'ILBig
    '
    Me.ILBig.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.ILBig.ImageSize = New System.Drawing.Size(32, 32)
    Me.ILBig.TransparentColor = System.Drawing.Color.Transparent
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Name"
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Size"
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "Modified"
    '
    'ColumnHeader4
    '
    Me.ColumnHeader4.Text = "Created"
    '
    'FileList
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.ListView1)
    Me.Name = "FileList"
    Me.Size = New System.Drawing.Size(644, 519)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ILBig As System.Windows.Forms.ImageList
  Friend WithEvents ILSmall As System.Windows.Forms.ImageList

End Class
