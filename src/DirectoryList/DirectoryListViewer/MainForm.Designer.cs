namespace DirectoryListViewer
{
    partial class MainForm
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressFiles = new System.Windows.Forms.ProgressBar();
            this.gridFiles = new System.Windows.Forms.DataGridView();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastTouchedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(16, 15);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(128, 28);
            this.btnSelectFolder.TabIndex = 0;
            this.btnSelectFolder.Text = "Select a Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "No folder selected";
            // 
            // progressFiles
            // 
            this.progressFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressFiles.Location = new System.Drawing.Point(16, 62);
            this.progressFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressFiles.Name = "progressFiles";
            this.progressFiles.Size = new System.Drawing.Size(692, 28);
            this.progressFiles.TabIndex = 2;
            // 
            // gridFiles
            // 
            this.gridFiles.AllowUserToAddRows = false;
            this.gridFiles.AllowUserToDeleteRows = false;
            this.gridFiles.AllowUserToResizeRows = false;
            this.gridFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sequence,
            this.Filename,
            this.FilePath,
            this.FileSize,
            this.LastTouchedDate});
            this.gridFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridFiles.Location = new System.Drawing.Point(16, 121);
            this.gridFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridFiles.Name = "gridFiles";
            this.gridFiles.ReadOnly = true;
            this.gridFiles.RowHeadersVisible = false;
            this.gridFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.gridFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFiles.Size = new System.Drawing.Size(692, 474);
            this.gridFiles.TabIndex = 3;
            // 
            // Sequence
            // 
            this.Sequence.HeaderText = "Seq";
            this.Sequence.Name = "Sequence";
            this.Sequence.ReadOnly = true;
            this.Sequence.Width = 50;
            // 
            // Filename
            // 
            this.Filename.HeaderText = "Filename";
            this.Filename.Name = "Filename";
            this.Filename.ReadOnly = true;
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "Path";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            // 
            // FileSize
            // 
            this.FileSize.HeaderText = "Size";
            this.FileSize.Name = "FileSize";
            this.FileSize.ReadOnly = true;
            // 
            // LastTouchedDate
            // 
            this.LastTouchedDate.HeaderText = "Date";
            this.LastTouchedDate.Name = "LastTouchedDate";
            this.LastTouchedDate.ReadOnly = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(580, 615);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 28);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 661);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gridFiles);
            this.Controls.Add(this.progressFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectFolder);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Directory List";
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressFiles;
        private System.Windows.Forms.DataGridView gridFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn Filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastTouchedDate;
        private System.Windows.Forms.Button btnClose;
    }
}

