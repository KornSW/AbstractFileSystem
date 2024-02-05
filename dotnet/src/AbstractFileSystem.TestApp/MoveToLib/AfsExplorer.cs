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
      if(this.DataSource == null) {
        return;
      }

     string[] fileKeys  = this.DataSource.ListAllFiles(AfsWellknownAttributeNames.FileName, 100, 0);
     foreach (string fileKey in fileKeys) {
        fileList.Items.Add(fileKey);
     }

    }







    private void toolStripButton2_Click(object sender, EventArgs e) {








    }

    private void areaTree_AfterSelect(object sender, TreeViewEventArgs e) {

    }


  }
}
