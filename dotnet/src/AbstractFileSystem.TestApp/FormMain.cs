using System;
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
      txtPathOrUrl.Text = "C:\\";
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

    private void ReinitConnection() {
      try {
        if (txtPathOrUrl.Text.StartsWith("http")) {
          if (_HttpClient == null) {
            _HttpClient = new HttpClient();
          }
          _HttpClient.DefaultRequestHeaders.Clear();
          if (!string.IsNullOrWhiteSpace(txtAccessToken.Text)) {
            _HttpClient.DefaultRequestHeaders.Add("Authorization", txtAccessToken.Text);
          }
          this.afsExplorer.DataSource = DynamicClientFactory.CreateInstance<IAfsRepository>(
            _HttpClient,
            () => txtPathOrUrl.Text
          );
        }
        else if (txtPathOrUrl.Text.Contains("\\")) {
          this.afsExplorer.DataSource = new AfsLocalRepository(txtPathOrUrl.Text);
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

    }
  }
}
