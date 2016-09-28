using DirectoryListLibrary;
using DirectoryProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryListViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var path =  folderBrowserDialog1.SelectedPath;

                var processor = new Processor(new AppConfig());

                var progressIndicator = new Progress<IFileInfo>(UpdateProgress);

                await processor.ProcessFolderAsync(path, progressIndicator);

            }


        }

        void UpdateProgress(IFileInfo value)
        {
            Debug.WriteLine(value.FileName);

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gridFiles);
            row.Cells[0].Value = value.Sequence;
            row.Cells[1].Value = value.FileName;
            row.Cells[2].Value = value.FilePath;
            row.Cells[3].Value = string.Format(@"{0:#,##0}", value.Size);
            row.Cells[4].Value = string.Format(@"{0:f}", value.DateLastTouched);
            gridFiles.Rows.Add(row);

        }

    }
}
