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
            //Update the UI to reflect the progress value that is passed back.
        }

    }
}
