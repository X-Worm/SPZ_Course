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
            lettersGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
                #region CheckIfAsmFileExists

                if (string.IsNullOrWhiteSpace(asmFile))
                {
                    MessageBox.Show("Asm file not exists");
                    return;
                }

                string filePath = System.AppDomain.CurrentDomain.BaseDirectory;
                string resPath = string.Format("{0}Resources\\", Path.GetFullPath(Path.Combine(filePath, @"..\..\")));

                // copy asm file to folder with tasm and tlink
                try
                {
                    // previously delete file if it exists in res folder
                    if (File.Exists(Path.Combine(resPath, Path.GetFileName(asmFile))))
                    {
                        File.Delete(Path.Combine(resPath, Path.GetFileName(asmFile)));
                    }
                    File.Copy(asmFile, Path.Combine(resPath, Path.GetFileName(asmFile)));
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Cannot copy .asm file to resources folder: {asmFile} to {resPath}");
                    return;
                }

                // check if file exists in res folder (are copied)
                if(!File.Exists(Path.Combine(resPath, Path.GetFileName(asmFile))))
                {
                    MessageBox.Show($"Cannot find {Path.GetFileName(asmFile)} in {resPath} folder");
                    return;
                }

                #endregion

                #region Generate_obj

                // generate the bat file to assemble code
                string codeBat = resPath + "\\" + "tasmFile.bat";
                using (StreamWriter writer = new StreamWriter(codeBat))
                {
                    //writer.WriteLine("cd " + resPath);
                    string tasmCommand = $"tasm {Path.GetFileName(asmFile)}";
                    writer.WriteLine(tasmCommand);
                }

                // check if in folder exists old .obf file -> if exist delete it
                if(File.Exists(Path.Combine(resPath, Path.GetFileNameWithoutExtension(asmFile) + ".obj")))
                {
                    // delete it file
                    File.Delete(Path.Combine(resPath, Path.GetFileNameWithoutExtension(asmFile) + ".obj"));
                }

                // run bat file to tasm
                Process p = new Process();
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = codeBat;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                string asmObjFile = Path.Combine(resPath, Path.GetFileNameWithoutExtension(asmFile) + ".obj");
                // check if obj file was generated
                if (!File.Exists(asmObjFile))
                {
                    MessageBox.Show($"Object file is not generated: {asmObjFile}");
                    return;
                }

                #endregion

                #region GenerateExe

                // start creating .exe file
                // create bat file
                string exeBat = resPath + "\\" + "tlinkFile.bat";
                using (StreamWriter writer = new StreamWriter(exeBat))
                {
                   // writer.WriteLine("cd " + resPath);
                    string tlinkCommand = $"tlink {asmObjFile}";
                    writer.WriteLine(tlinkCommand);
                }

                // delete old .obj file if it exists
                if (File.Exists(Path.Combine(resPath, Path.GetFileNameWithoutExtension(asmFile) + ".obj")))
                {
                    // delete it file
                    File.Delete(Path.Combine(resPath, Path.GetFileNameWithoutExtension(asmFile) + ".obj"));
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

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while generating .exe file: {ex}");
                return;
            }

        }

    }
}
