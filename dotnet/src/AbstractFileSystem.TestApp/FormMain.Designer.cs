
using AbstractFileSystem.TestApp;
using System.IO.Abstraction;

namespace AFS.TestApp {
  partial class FormMain {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      afsExplorer = new AfsExplorer();
      txtPathOrUrl = new System.Windows.Forms.TextBox();
      txtAccessToken = new System.Windows.Forms.TextBox();
      panelTop = new System.Windows.Forms.Panel();
      lblAccessToken = new System.Windows.Forms.Label();
      lblPathOrUrl = new System.Windows.Forms.Label();
      panelTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // afsExplorer
      // 
      afsExplorer.AllowDelete = true;
      afsExplorer.AllowDownload = true;
      afsExplorer.AllowMultiselectOpen = false;
      afsExplorer.AllowUpload = true;
      afsExplorer.AttributeDisplayWhitelist = null;
      afsExplorer.AttributeUpdateWhitelist = null;
      afsExplorer.DataSource = null;
      afsExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
      afsExplorer.Location = new System.Drawing.Point(0, 49);
      afsExplorer.Name = "afsExplorer";
      afsExplorer.Size = new System.Drawing.Size(970, 624);
      afsExplorer.TabIndex = 0;
      // 
      // txtPathOrUrl
      // 
      txtPathOrUrl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      txtPathOrUrl.Location = new System.Drawing.Point(3, 20);
      txtPathOrUrl.Name = "txtPathOrUrl";
      txtPathOrUrl.Size = new System.Drawing.Size(796, 23);
      txtPathOrUrl.TabIndex = 1;
      txtPathOrUrl.Text = "c:\\Temp";
      txtPathOrUrl.TextChanged += this.txtPathOrUrl_TextChanged;
      // 
      // txtAccessToken
      // 
      txtAccessToken.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      txtAccessToken.Location = new System.Drawing.Point(805, 21);
      txtAccessToken.Name = "txtAccessToken";
      txtAccessToken.Size = new System.Drawing.Size(162, 23);
      txtAccessToken.TabIndex = 1;
      // 
      // panelTop
      // 
      panelTop.Controls.Add(lblAccessToken);
      panelTop.Controls.Add(lblPathOrUrl);
      panelTop.Controls.Add(txtPathOrUrl);
      panelTop.Controls.Add(txtAccessToken);
      panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      panelTop.Location = new System.Drawing.Point(0, 0);
      panelTop.Name = "panelTop";
      panelTop.Size = new System.Drawing.Size(970, 49);
      panelTop.TabIndex = 2;
      // 
      // lblAccessToken
      // 
      lblAccessToken.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      lblAccessToken.AutoSize = true;
      lblAccessToken.Location = new System.Drawing.Point(805, 3);
      lblAccessToken.Name = "lblAccessToken";
      lblAccessToken.Size = new System.Drawing.Size(80, 15);
      lblAccessToken.TabIndex = 2;
      lblAccessToken.Text = "Access Token:";
      // 
      // lblPathOrUrl
      // 
      lblPathOrUrl.AutoSize = true;
      lblPathOrUrl.Location = new System.Drawing.Point(3, 3);
      lblPathOrUrl.Name = "lblPathOrUrl";
      lblPathOrUrl.Size = new System.Drawing.Size(146, 15);
      lblPathOrUrl.TabIndex = 2;
      lblPathOrUrl.Text = "Local-Path OR Service-Url:";
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(970, 673);
      this.Controls.Add(afsExplorer);
      this.Controls.Add(panelTop);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.MinimumSize = new System.Drawing.Size(480, 290);
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "AFS Testapp";
      this.Load += this.FormMain_Load;
      panelTop.ResumeLayout(false);
      panelTop.PerformLayout();
      this.ResumeLayout(false);
    }

    #endregion

    private AfsExplorer afsExplorer;
    private System.Windows.Forms.TextBox txtPathOrUrl;
    private System.Windows.Forms.TextBox txtAccessToken;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Label lblPathOrUrl;
    private System.Windows.Forms.Label lblAccessToken;
  }
}

