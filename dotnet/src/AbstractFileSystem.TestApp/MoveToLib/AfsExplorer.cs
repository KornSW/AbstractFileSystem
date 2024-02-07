using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Abstraction;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbstractFileSystem.TestApp {

  public partial class AfsExplorer : UserControl {

    public AfsExplorer() {
      this.InitializeComponent();
    }

    private IAfsRepository _DataSource = null;
    public IAfsRepository DataSource {
      get {
        return _DataSource;
      }
      set {
        _DataSource = value;
        this.Reload();
      }
    }

    public bool AllowDownload { get; set; } = true;
    public bool AllowUpload { get; set; } = true;
    public bool AllowDelete { get; set; } = true;

    public string[] AttributeDisplayWhitelist { get; set; } = null;
    public string[] AttributeUpdateWhitelist { get; set; } = null;

    public bool AllowMultiselectOpen { get; set; } = false;

    public string[] SelectedFileKeys { get; } = new string[] { };

    public event EventHandler Open;

    private void btnReload_Click(object sender, EventArgs e) {
      try {
        this.Reload();
      }
      catch (Exception ex) {
        MessageBox.Show(this.ParentForm, ex.Message);
      }
    }

    public void Reload() {

      fileList.Items.Clear();
      if (this.DataSource == null) {
        return;
      }



      var attripDescriptors = this.DataSource.GetAvailableAttributes();
      string[] attribNames = attripDescriptors.Select((a)=>a.AttributeName).ToArray();

      fileList.Columns.Clear();
      foreach (var attripDescriptor in attripDescriptors) {
        var col =  fileList.Columns.Add(attripDescriptor.AttributeName);
        col.Width = 120;
      
      }
      fileList.Columns[0].Width = 0; //hide KEY colum

      string[] fileKeys = this.DataSource.ListAllFiles("^" + AfsWellknownAttributeNames.FileSizeBytes, 100, 0);
      var fas = this.DataSource.LoadFileAttributes(fileKeys, attribNames);

      foreach (var fa in fas) {
        var item = fileList.Items.Add(fa[attribNames[0]]);
        for (int i = 1; i < fa.Count; i++) {
          item.SubItems.Add(fa[attribNames[i]]);
        }
        item.ToolTipText = fa[attribNames[0]];//key
      }

    }







    private void toolStripButton2_Click(object sender, EventArgs e) {








    }

    private void areaTree_AfterSelect(object sender, TreeViewEventArgs e) {

    }


  }
}
