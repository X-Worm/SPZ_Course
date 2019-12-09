using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPZ_GUI
{
    public partial class BuildForm : Form
    {
        public string fileResources = "";
        public BuildForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false; this.MinimizeBox = false;
            lexemGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void fileLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (!String.IsNullOrWhiteSpace(fileResources))
            {
                try
                {
                    System.Diagnostics.Process.Start("explorer.exe", fileResources);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show($"Incorrect file path: {fileResources}");
        }
    }
}
