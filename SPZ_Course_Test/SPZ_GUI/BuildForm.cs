using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPZ_GUI
{
    public partial class BuildForm : Form
    {
        public string fileResources = "";
        public string asmFile = "";
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

        private void runButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(asmFile))
                {
                    MessageBox.Show("Asm file not exists");
                    return;
                }

                // generate the bat file to assemble code
                string codeBat = fileResources + "\\" + "tasmFile.bat";
                using (StreamWriter writer = new StreamWriter(codeBat))
                {
                    string tasmCommand = $"tasm {asmFile}";
                    writer.WriteLine(tasmCommand);
                }

                // run bat file to tasm
                Process p = new Process();
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = codeBat;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                string asmObjFile = Path.Combine(Path.GetDirectoryName(asmFile), Path.GetFileNameWithoutExtension(asmFile) + ".obj");
                // check if obj file was generated
                if (!File.Exists(asmObjFile))
                {
                    MessageBox.Show($"Object file is not generated: {asmObjFile}");
                    return;
                }

                // start creating .exe file
                // create ba file
                string exeBat = fileResources + "\\" + "tlinkFile.bat";
                using (StreamWriter writer = new StreamWriter(exeBat))
                {
                    string tlinkCommand = $"tlink {asmObjFile}";
                    writer.WriteLine(tlinkCommand);
                }

                // run bat file to tlink
                p = new Process();
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = exeBat;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                string exeFilePath = Path.Combine(Path.GetDirectoryName(asmFile), Path.GetFileNameWithoutExtension(asmFile) + ".exe");
                // check if exe file was generated
                if (!File.Exists(exeFilePath))
                {
                    MessageBox.Show($"Exe file is not generated: {exeFilePath}");
                    return;
                }
                else
                {
                    MessageBox.Show($"File generated to: {exeFilePath}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error while generating .exe file: {ex}");
                return;
            }

        }

    }
}
