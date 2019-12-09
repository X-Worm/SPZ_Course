using SPZ_GUI.ASM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPZ_GUI
{
    public partial class SP_Course : Form
    {
        private readonly string newFile = "NewFile";
        private readonly string errorReport = "errorReport.txt";
        public SP_Course()
        {
            InitializeComponent();
            labelFileName.Text = newFile;
            labelFileName.ForeColor = Color.Black;
            labelLine.Text = "Line: 0";
            richTextBox1.ShortcutsEnabled = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false; this.MinimizeBox = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            labelFileName.ForeColor = Color.Red;
            labelLine.Text = "Line: " + richTextBox1.Lines.Length;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            OpenNewFile();
        }

        public void OpenNewFile()
        {
            // check if it is new file and it modified
            if (labelFileName.Text == newFile && labelFileName.ForeColor == Color.Red)
            {
                // dialog to save current file
                var dialogRes = MessageBox.Show("Save cuurent file?", "", MessageBoxButtons.OKCancel);
                if(dialogRes == DialogResult.OK)
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    if(saveFile.ShowDialog() == DialogResult.OK)
                    {
                        using(StreamWriter writer = new StreamWriter(saveFile.FileName))
                        {
                            foreach(var line in richTextBox1.Lines)
                            {
                                writer.WriteLine(line);
                            }
                        }
                        MessageBox.Show($"File saved to: {saveFile.FileName}");
                    }
                }
            }
            // else if it is existing file but not saved
            if(labelFileName.Text != newFile && labelFileName.ForeColor == Color.Red)
            {
                // dialog to save current file
                var dialogRes = MessageBox.Show("Save cuurent changes?", "", MessageBoxButtons.OKCancel);
                if (dialogRes == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(labelFileName.Text))
                    {
                        foreach (var line in richTextBox1.Lines)
                        {
                            writer.WriteLine(line);
                        }
                    }
                    MessageBox.Show($"Changes saved");
                }
            }
            richTextBox1.Clear();
            labelFileName.Text = newFile;
            labelFileName.ForeColor = Color.Black;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenNewFile();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using(StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    richTextBox1.Text = reader.ReadToEnd();
                }
                labelFileName.Text = openFileDialog.FileName;
                labelFileName.ForeColor = Color.Black;
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (labelFileName.ForeColor == Color.Black)
                return;
            else
            {
                if(labelFileName.Text == newFile)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    if(saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            foreach(var line in richTextBox1.Lines)
                            {
                                writer.WriteLine(line);
                            }
                        }
                        labelFileName.Text = saveFileDialog.FileName;
                        labelFileName.ForeColor = Color.Black;
                        MessageBox.Show($"File saved to: {saveFileDialog.FileName}");
                    }
                }
                else if(labelFileName.Text != newFile)
                {
                    using (StreamWriter writer = new StreamWriter(labelFileName.Text))
                    {
                        foreach (var line in richTextBox1.Lines)
                        {
                            writer.WriteLine(line);
                        }
                    }
                    labelFileName.ForeColor = Color.Black;
                    MessageBox.Show("File saved!");
                }
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            string repoLink = "https://github.com/X-Worm/SPZ_Course";
            MessageBox.Show($"Currently empty: Repository link: {repoLink}");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // check file info and extension
            if (labelFileName.Text == newFile)
            {
                MessageBox.Show("Previously save current file with .k65 extension");
                return;
            }
            else if(labelFileName.ForeColor == Color.Red)
            {
                MessageBox.Show("Previously save file");
                return;
            }
            if(Path.GetExtension(labelFileName.Text) != ".k65")
            {
                MessageBox.Show("File must be with \'.k65\' extension");
                return;
            }
            // build file
            // get token list
            List<KeyWordToken> keyWordTokens = new List<KeyWordToken>();
            try
            {
                using (StreamReader reader = new StreamReader(labelFileName.Text)) 
                {
                    keyWordTokens = Analize.AnalisisTokens(reader);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"System Error: {ex}");
                return;
            }
            // create resources folder
            var localFolder = Path.GetDirectoryName(labelFileName.Text);
            string resourceFolder = localFolder + "\\" + Path.GetFileNameWithoutExtension(labelFileName.Text);
            Directory.CreateDirectory(resourceFolder);

            // error checking
            string errorMessages = "";
            int errorNumber = 0;
            string errorFilePath = Path.Combine(resourceFolder, errorReport);
            try
            {
                using (StreamWriter writer = new StreamWriter(errorFilePath))
                {
                    errorNumber = Analize.ErrorChecking(writer);
                }
                using (StreamReader reader = new StreamReader(errorFilePath))
                {
                    errorMessages = reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"System Error: {ex}");
                return;
            }

            // check if error exists
            if(errorNumber != 0)
            {
                // show form with error and tokens
                BuildForm buildForm = new BuildForm();
                buildForm.lexemGrid.DataSource = keyWordTokens;
                buildForm.errorOutput.Text = errorMessages;
                buildForm.fileLink.Enabled = false;
                buildForm.runButton.Enabled = false;
                buildForm.Show();
            }
            else
            {
                // show form with error and tokens
                BuildForm buildForm = new BuildForm();
                buildForm.lexemGrid.DataSource = keyWordTokens;
                buildForm.errorOutput.Text = "Error not found.";
                buildForm.fileLink.Enabled = true;
                buildForm.fileResources = resourceFolder;

                buildForm.Show();

                // generate code
                string codeFile = resourceFolder + "\\" + Path.GetFileNameWithoutExtension(labelFileName.Text) + ".asm";
                try
                {
                    using (StreamWriter writer = new StreamWriter(codeFile))
                    {
                        Analize.GenerateCode(writer);
                    }
                    buildForm.asmFile = codeFile;
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Asm code was not generated: {ex.Message}");
                    return;
                }

                // build and run program

            }
        }
    }
}
