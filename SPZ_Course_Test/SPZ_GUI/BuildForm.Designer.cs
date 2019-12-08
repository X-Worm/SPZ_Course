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
            ((System.ComponentModel.ISupportInitialize)(this.lexemGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lexemGrid
            // 
            this.lexemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lexemGrid.Location = new System.Drawing.Point(13, 13);
            this.lexemGrid.Name = "lexemGrid";
            this.lexemGrid.Size = new System.Drawing.Size(669, 213);
            this.lexemGrid.TabIndex = 0;
            // 
            // errorOutput
            // 
            this.errorOutput.Location = new System.Drawing.Point(13, 233);
            this.errorOutput.Name = "errorOutput";
            this.errorOutput.Size = new System.Drawing.Size(349, 112);
            this.errorOutput.TabIndex = 1;
            this.errorOutput.Text = "";
            // 
            // fileLink
            // 
            this.fileLink.AutoSize = true;
            this.fileLink.Location = new System.Drawing.Point(369, 233);
            this.fileLink.Name = "fileLink";
            this.fileLink.Size = new System.Drawing.Size(94, 13);
            this.fileLink.TabIndex = 2;
            this.fileLink.TabStop = true;
            this.fileLink.Text = "File resources link:";
            // 
            // BuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 357);
            this.Controls.Add(this.fileLink);
            this.Controls.Add(this.errorOutput);
            this.Controls.Add(this.lexemGrid);
            this.Name = "BuildForm";
            this.Text = "BuildForm";
            ((System.ComponentModel.ISupportInitialize)(this.lexemGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView lexemGrid;
        private System.Windows.Forms.RichTextBox errorOutput;
        private System.Windows.Forms.LinkLabel fileLink;
    }
}