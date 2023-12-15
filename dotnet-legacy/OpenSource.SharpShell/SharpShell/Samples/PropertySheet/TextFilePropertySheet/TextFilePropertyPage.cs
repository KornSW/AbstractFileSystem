using System;
using System.Linq;
using System.Windows.Forms;
using SharpShell.SharpPropertySheet;

namespace TextFilePropertySheet
{
    public partial class TextFilePropertyPage : SharpPropertyPage
    {
        public TextFilePropertyPage()
        {
            InitializeComponent();

            //  Set the title.
            PageTitle = "inno DMS";
        }

        private void DrivePropertyPage_Load(object sender, EventArgs e)
        {
            SharpShell.Diagnostics.Logging.Log("inno DMS Property Page Loaded");
        }

        public override void OnPageInitialised(SharpPropertySheet parent)
        {
            //  Set the name of the file in the text box.
            textBoxFileName.Text = parent.SelectedItemPaths.FirstOrDefault();

            if (parent.SelectedItemPaths.Count()>1){
                tabControl1.TabPages.RemoveAt (1);
                tabControl1.TabPages.RemoveAt (1);
                tabControl1.TabPages.RemoveAt (1);
                tabControl1.TabPages.RemoveAt (1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SetPageDataChanged(true);
        }
    }
}
