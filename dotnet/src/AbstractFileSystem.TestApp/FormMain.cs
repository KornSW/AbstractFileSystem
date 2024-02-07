using System;
using System.IO;
using System.IO.Abstraction;
using System.Net.Http;
using System.Web.UJMW;
using System.Windows.Forms;

namespace AFS.TestApp {

  public partial class FormMain : Form {

    public FormMain() {
      this.InitializeComponent();

      txtPathOrUrl.KeyDown += this.TxtPathOrUrl_KeyDown;
      txtPathOrUrl.Leave += this.TxtPathOrUrl_Leave;
      txtAccessToken.KeyDown += this.TxtPathOrUrl_KeyDown;
      txtAccessToken.Leave += this.TxtPathOrUrl_Leave;

      //TODO: stattdessen connection aus registry laden und falls da ReinitConnection aufrufen
      txtPathOrUrl.Text = "C:\\Temp";
      this.ReinitConnection();

    }

    private void TxtPathOrUrl_Leave(object sender, EventArgs e) {
      this.ReinitConnection();
    }

    private void TxtPathOrUrl_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter) {
        this.ReinitConnection();
      }
    }

    private HttpClient _HttpClient = null;

    private void ReinitConnection(bool force = false) {
      if (!force && this.afsExplorer.DataSource != null) {
        return;
      }
      this.Text = "AFS Testapp";
      try {
        if (txtPathOrUrl.Text.StartsWith("http")) {
          this.afsExplorer.DataSource = new AfsHttpRepositoryConnector(txtPathOrUrl.Text, txtAccessToken.Text);
          this.Text = $"{this.Text} ({this.afsExplorer.DataSource.GetOriginIdentity()})";
        }
        else if (txtPathOrUrl.Text.Contains("\\")) {
          string pth = txtPathOrUrl.Text;
          if (!Path.IsPathRooted(pth)) {
            pth = Path.GetFullPath(pth);
          }
          this.afsExplorer.DataSource = new AfsLocalRepository(pth);
          this.Text = $"{this.Text} ({this.afsExplorer.DataSource.GetOriginIdentity()})";
        }
        else {
          this.afsExplorer.DataSource = null;
        }
        //TODO: on success connection abspeichern in registry
      }
      catch (Exception ex) {
        MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.afsExplorer.DataSource = null;
      }
    }

    private void FormMain_Load(object sender, EventArgs e) {
    }

    private void txtPathOrUrl_TextChanged(object sender, EventArgs e) {
      this.afsExplorer.DataSource = null;
    }

    private void txtAccessToken_TextChanged(object sender, EventArgs e) {
      this.afsExplorer.DataSource = null;
    }

  }

}
