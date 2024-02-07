namespace AbstractFileSystem.TestApp {
  partial class AfsExplorer {
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      components = new System.ComponentModel.Container();
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("/");
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AfsExplorer));
      splitContainer1 = new System.Windows.Forms.SplitContainer();
      areaTree = new System.Windows.Forms.TreeView();
      ctxManagementValueCollectionNode = new System.Windows.Forms.ContextMenuStrip(components);
      reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      imageList20 = new System.Windows.Forms.ImageList(components);
      panSearch = new System.Windows.Forms.Panel();
      lblFulltextFilter = new System.Windows.Forms.Label();
      lblAttribFilter = new System.Windows.Forms.Label();
      txtFulltextFilter = new System.Windows.Forms.TextBox();
      button1 = new System.Windows.Forms.Button();
      attributeFilter = new AttributeForm();
      fileList = new System.Windows.Forms.ListView();
      colKey = new System.Windows.Forms.ColumnHeader();
      toolStrip1 = new System.Windows.Forms.ToolStrip();
      toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
      aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      bToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      toolbar = new System.Windows.Forms.ToolStrip();
      btnReload = new System.Windows.Forms.ToolStripButton();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      ctxManagementValueCollectionNode.SuspendLayout();
      panSearch.SuspendLayout();
      toolStrip1.SuspendLayout();
      toolbar.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      splitContainer1.Location = new System.Drawing.Point(0, 25);
      splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      splitContainer1.Panel1.Controls.Add(panSearch);
      splitContainer1.Panel1.Controls.Add(areaTree);
      // 
      // splitContainer1.Panel2
      // 
      splitContainer1.Panel2.Controls.Add(fileList);
      splitContainer1.Panel2.Controls.Add(toolStrip1);
      splitContainer1.Size = new System.Drawing.Size(946, 670);
      splitContainer1.SplitterDistance = 318;
      splitContainer1.SplitterWidth = 6;
      splitContainer1.TabIndex = 1;
      // 
      // areaTree
      // 
      areaTree.ContextMenuStrip = ctxManagementValueCollectionNode;
      areaTree.Dock = System.Windows.Forms.DockStyle.Fill;
      areaTree.FullRowSelect = true;
      areaTree.HideSelection = false;
      areaTree.ImageIndex = 0;
      areaTree.ImageList = imageList20;
      areaTree.Location = new System.Drawing.Point(0, 0);
      areaTree.Name = "areaTree";
      treeNode1.Name = "root";
      treeNode1.Text = "/";
      areaTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode1 });
      areaTree.SelectedImageIndex = 0;
      areaTree.ShowRootLines = false;
      areaTree.Size = new System.Drawing.Size(318, 670);
      areaTree.TabIndex = 0;
      areaTree.AfterSelect += this.areaTree_AfterSelect;
      // 
      // ctxManagementValueCollectionNode
      // 
      ctxManagementValueCollectionNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { reloadToolStripMenuItem, addToolStripMenuItem, deleteToolStripMenuItem });
      ctxManagementValueCollectionNode.Name = "ctxManagementValueCollectionNode";
      ctxManagementValueCollectionNode.Size = new System.Drawing.Size(111, 70);
      // 
      // reloadToolStripMenuItem
      // 
      reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
      reloadToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
      reloadToolStripMenuItem.Text = "Reload";
      // 
      // addToolStripMenuItem
      // 
      addToolStripMenuItem.Name = "addToolStripMenuItem";
      addToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
      addToolStripMenuItem.Text = "Add...";
      // 
      // deleteToolStripMenuItem
      // 
      deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      deleteToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
      deleteToolStripMenuItem.Text = "Delete";
      // 
      // imageList20
      // 
      imageList20.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      imageList20.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList20.ImageStream");
      imageList20.TransparentColor = System.Drawing.Color.Transparent;
      imageList20.Images.SetKeyName(0, "root");
      imageList20.Images.SetKeyName(1, "area");
      imageList20.Images.SetKeyName(2, "value");
      imageList20.Images.SetKeyName(3, "file");
      // 
      // panSearch
      // 
      panSearch.Controls.Add(lblFulltextFilter);
      panSearch.Controls.Add(lblAttribFilter);
      panSearch.Controls.Add(txtFulltextFilter);
      panSearch.Controls.Add(button1);
      panSearch.Controls.Add(attributeFilter);
      panSearch.Dock = System.Windows.Forms.DockStyle.Fill;
      panSearch.Location = new System.Drawing.Point(0, 0);
      panSearch.Name = "panSearch";
      panSearch.Size = new System.Drawing.Size(318, 670);
      panSearch.TabIndex = 1;
      // 
      // lblFulltextFilter
      // 
      lblFulltextFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
      lblFulltextFilter.AutoSize = true;
      lblFulltextFilter.Location = new System.Drawing.Point(3, 591);
      lblFulltextFilter.Name = "lblFulltextFilter";
      lblFulltextFilter.Size = new System.Drawing.Size(158, 15);
      lblFulltextFilter.TabIndex = 3;
      lblFulltextFilter.Text = "Filter by Text within Content:";
      // 
      // lblAttribFilter
      // 
      lblAttribFilter.AutoSize = true;
      lblAttribFilter.Location = new System.Drawing.Point(3, 4);
      lblAttribFilter.Name = "lblAttribFilter";
      lblAttribFilter.Size = new System.Drawing.Size(107, 15);
      lblAttribFilter.TabIndex = 3;
      lblAttribFilter.Text = "Filter by Attributes:";
      // 
      // txtFulltextFilter
      // 
      txtFulltextFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      txtFulltextFilter.Location = new System.Drawing.Point(3, 609);
      txtFulltextFilter.Name = "txtFulltextFilter";
      txtFulltextFilter.Size = new System.Drawing.Size(309, 23);
      txtFulltextFilter.TabIndex = 1;
      // 
      // button1
      // 
      button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      button1.Location = new System.Drawing.Point(199, 638);
      button1.Name = "button1";
      button1.Size = new System.Drawing.Size(113, 26);
      button1.TabIndex = 0;
      button1.Text = "Search";
      button1.UseVisualStyleBackColor = true;
      // 
      // attributeFilter
      // 
      attributeFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      attributeFilter.Location = new System.Drawing.Point(3, 22);
      attributeFilter.Name = "attributeFilter";
      attributeFilter.Size = new System.Drawing.Size(309, 566);
      attributeFilter.TabIndex = 2;
      // 
      // fileList
      // 
      fileList.CheckBoxes = true;
      fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colKey });
      fileList.Dock = System.Windows.Forms.DockStyle.Fill;
      fileList.FullRowSelect = true;
      fileList.GridLines = true;
      fileList.HideSelection = true;
      fileList.Location = new System.Drawing.Point(0, 0);
      fileList.Name = "fileList";
      fileList.Size = new System.Drawing.Size(622, 645);
      fileList.SmallImageList = imageList20;
      fileList.TabIndex = 0;
      fileList.UseCompatibleStateImageBehavior = false;
      fileList.View = System.Windows.Forms.View.Details;
      // 
      // colKey
      // 
      colKey.Text = "Key";
      // 
      // toolStrip1
      // 
      toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
      toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton3 });
      toolStrip1.Location = new System.Drawing.Point(0, 645);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Size = new System.Drawing.Size(622, 25);
      toolStrip1.TabIndex = 1;
      toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton3
      // 
      toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      toolStripButton3.Image = (System.Drawing.Image)resources.GetObject("toolStripButton3.Image");
      toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      toolStripButton3.Name = "toolStripButton3";
      toolStripButton3.Size = new System.Drawing.Size(23, 22);
      toolStripButton3.Text = "toolStripButton3";
      // 
      // toolStripSplitButton1
      // 
      toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aToolStripMenuItem, bToolStripMenuItem });
      toolStripSplitButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripSplitButton1.Image");
      toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      toolStripSplitButton1.Name = "toolStripSplitButton1";
      toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
      toolStripSplitButton1.Text = "toolStripSplitButton1";
      // 
      // aToolStripMenuItem
      // 
      aToolStripMenuItem.Name = "aToolStripMenuItem";
      aToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      aToolStripMenuItem.Text = "A";
      // 
      // bToolStripMenuItem
      // 
      bToolStripMenuItem.Name = "bToolStripMenuItem";
      bToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      bToolStripMenuItem.Text = "B";
      // 
      // toolbar
      // 
      toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnReload, toolStripSeparator1, toolStripSplitButton1, toolStripButton1, toolStripButton2 });
      toolbar.Location = new System.Drawing.Point(0, 0);
      toolbar.Name = "toolbar";
      toolbar.Size = new System.Drawing.Size(946, 25);
      toolbar.TabIndex = 0;
      toolbar.Text = "toolStrip1";
      // 
      // btnReload
      // 
      btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      btnReload.Image = (System.Drawing.Image)resources.GetObject("btnReload.Image");
      btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
      btnReload.Name = "btnReload";
      btnReload.Size = new System.Drawing.Size(23, 22);
      btnReload.Text = "toolStripButton4";
      btnReload.Click += this.btnReload_Click;
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton1
      // 
      toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
      toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      toolStripButton1.Name = "toolStripButton1";
      toolStripButton1.Size = new System.Drawing.Size(23, 22);
      toolStripButton1.Text = "toolStripButton1";
      // 
      // toolStripButton2
      // 
      toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      toolStripButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripButton2.Image");
      toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      toolStripButton2.Name = "toolStripButton2";
      toolStripButton2.Size = new System.Drawing.Size(23, 22);
      toolStripButton2.Text = "toolStripButton2";
      toolStripButton2.Click += this.toolStripButton2_Click;
      // 
      // AfsExplorer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(splitContainer1);
      this.Controls.Add(toolbar);
      this.Name = "AfsExplorer";
      this.Size = new System.Drawing.Size(946, 695);
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel2.ResumeLayout(false);
      splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
      splitContainer1.ResumeLayout(false);
      ctxManagementValueCollectionNode.ResumeLayout(false);
      panSearch.ResumeLayout(false);
      panSearch.PerformLayout();
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      toolbar.ResumeLayout(false);
      toolbar.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem bToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolbar;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.TreeView areaTree;
    private System.Windows.Forms.ListView fileList;
    private System.Windows.Forms.Panel panSearch;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label lblFulltextFilter;
    private System.Windows.Forms.Label lblAttribFilter;
    private AttributeForm attributeFilter;
    private System.Windows.Forms.TextBox txtFulltextFilter;
    private System.Windows.Forms.ImageList imageList20;
    private System.Windows.Forms.ColumnHeader colKey;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ContextMenuStrip ctxManagementValueCollectionNode;
    private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton btnReload;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
  }
}
