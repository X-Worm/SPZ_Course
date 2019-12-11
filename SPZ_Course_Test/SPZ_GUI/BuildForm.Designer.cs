namespace SPZ_GUI
{
    partial class BuildForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lexemGrid = new System.Windows.Forms.DataGridView();
            this.errorOutput = new System.Windows.Forms.RichTextBox();
            this.fileLink = new System.Windows.Forms.LinkLabel();
            this.runButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.identGrid = new System.Windows.Forms.DataGridView();
            this.lettersGrid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lexemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.identGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lettersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lexemGrid
            // 
            this.lexemGrid.AllowUserToAddRows = false;
            this.lexemGrid.AllowUserToDeleteRows = false;
            this.lexemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lexemGrid.Location = new System.Drawing.Point(13, 13);
            this.lexemGrid.Name = "lexemGrid";
            this.lexemGrid.ReadOnly = true;
            this.lexemGrid.Size = new System.Drawing.Size(349, 213);
            this.lexemGrid.TabIndex = 0;
            // 
            // errorOutput
            // 
            this.errorOutput.Location = new System.Drawing.Point(13, 233);
            this.errorOutput.Name = "errorOutput";
            this.errorOutput.Size = new System.Drawing.Size(220, 112);
            this.errorOutput.TabIndex = 1;
            this.errorOutput.Text = "";
            // 
            // fileLink
            // 
            this.fileLink.AutoSize = true;
            this.fileLink.Location = new System.Drawing.Point(255, 236);
            this.fileLink.Name = "fileLink";
            this.fileLink.Size = new System.Drawing.Size(94, 13);
            this.fileLink.TabIndex = 2;
            this.fileLink.TabStop = true;
            this.fileLink.Text = "File resources link:";
            this.fileLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fileLink_LinkClicked);
            // 
            // runButton
            // 
            this.runButton.Image = global::SPZ_GUI.Properties.Resources.RunAndBuild;
            this.runButton.Location = new System.Drawing.Point(274, 267);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(51, 52);
            this.runButton.TabIndex = 3;
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Compile and Run";
            // 
            // identGrid
            // 
            this.identGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.identGrid.Location = new System.Drawing.Point(369, 33);
            this.identGrid.Name = "identGrid";
            this.identGrid.Size = new System.Drawing.Size(240, 150);
            this.identGrid.TabIndex = 5;
            // 
            // lettersGrid
            // 
            this.lettersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lettersGrid.Location = new System.Drawing.Point(369, 205);
            this.lettersGrid.Name = "lettersGrid";
            this.lettersGrid.Size = new System.Drawing.Size(240, 140);
            this.lettersGrid.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Identifier table";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(372, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Letters table";
            // 
            // BuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 357);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lettersGrid);
            this.Controls.Add(this.identGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.fileLink);
            this.Controls.Add(this.errorOutput);
            this.Controls.Add(this.lexemGrid);
            this.Name = "BuildForm";
            this.Text = "BuildForm";
            ((System.ComponentModel.ISupportInitialize)(this.lexemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.identGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lettersGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView lexemGrid;
        public System.Windows.Forms.RichTextBox errorOutput;
        public System.Windows.Forms.LinkLabel fileLink;
        public System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView identGrid;
        public System.Windows.Forms.DataGridView lettersGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}