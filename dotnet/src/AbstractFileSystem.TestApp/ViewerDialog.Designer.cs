namespace AbstractFileSystem.TestApp {
  partial class ViewerDialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      splitContainer1 = new System.Windows.Forms.SplitContainer();
      attributeForm = new AttributeForm();
      lblAttributes = new System.Windows.Forms.Label();
      btnWriteAttributes = new System.Windows.Forms.Button();
      txtContentDisplay = new System.Windows.Forms.TextBox();
      picContentDisplay = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)picContentDisplay).BeginInit();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      splitContainer1.Location = new System.Drawing.Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      splitContainer1.Panel1.Controls.Add(picContentDisplay);
      splitContainer1.Panel1.Controls.Add(txtContentDisplay);
      // 
      // splitContainer1.Panel2
      // 
      splitContainer1.Panel2.Controls.Add(btnWriteAttributes);
      splitContainer1.Panel2.Controls.Add(lblAttributes);
      splitContainer1.Panel2.Controls.Add(attributeForm);
      splitContainer1.Size = new System.Drawing.Size(913, 591);
      splitContainer1.SplitterDistance = 674;
      splitContainer1.SplitterWidth = 6;
      splitContainer1.TabIndex = 0;
      // 
      // attributeForm
      // 
      attributeForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      attributeForm.Location = new System.Drawing.Point(3, 24);
      attributeForm.Name = "attributeForm";
      attributeForm.Size = new System.Drawing.Size(226, 523);
      attributeForm.TabIndex = 0;
      // 
      // lblAttributes
      // 
      lblAttributes.AutoSize = true;
      lblAttributes.Location = new System.Drawing.Point(3, 6);
      lblAttributes.Name = "lblAttributes";
      lblAttributes.Size = new System.Drawing.Size(62, 15);
      lblAttributes.TabIndex = 1;
      lblAttributes.Text = "Attributes:";
      // 
      // btnWriteAttributes
      // 
      btnWriteAttributes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      btnWriteAttributes.Location = new System.Drawing.Point(75, 557);
      btnWriteAttributes.Name = "btnWriteAttributes";
      btnWriteAttributes.Size = new System.Drawing.Size(154, 26);
      btnWriteAttributes.TabIndex = 2;
      btnWriteAttributes.Text = "Write Attributes to File!";
      btnWriteAttributes.UseVisualStyleBackColor = true;
      // 
      // txtContentDisplay
      // 
      txtContentDisplay.BackColor = System.Drawing.SystemColors.Window;
      txtContentDisplay.Location = new System.Drawing.Point(42, 221);
      txtContentDisplay.Multiline = true;
      txtContentDisplay.Name = "txtContentDisplay";
      txtContentDisplay.ReadOnly = true;
      txtContentDisplay.Size = new System.Drawing.Size(360, 326);
      txtContentDisplay.TabIndex = 0;
      // 
      // picContentDisplay
      // 
      picContentDisplay.BackColor = System.Drawing.Color.Silver;
      picContentDisplay.Location = new System.Drawing.Point(268, 24);
      picContentDisplay.Name = "picContentDisplay";
      picContentDisplay.Size = new System.Drawing.Size(338, 331);
      picContentDisplay.TabIndex = 1;
      picContentDisplay.TabStop = false;
      // 
      // ViewerDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(913, 591);
      this.Controls.Add(splitContainer1);
      this.Name = "ViewerDialog";
      this.Text = "ViewerDialog";
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel1.PerformLayout();
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
      splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)picContentDisplay).EndInit();
      this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label lblAttributes;
    private AttributeForm attributeForm;
    private System.Windows.Forms.Button btnWriteAttributes;
    private System.Windows.Forms.TextBox txtContentDisplay;
    private System.Windows.Forms.PictureBox picContentDisplay;
  }
}