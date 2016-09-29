using DirectoryListLibrary;
using DirectoryProcessor;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileQueueProcessor.Memory;

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

                label1.Text = path;
                gridFiles.Rows.Clear();

                var config = new AppConfig();
                config.QueueName = "";

                var processor = new Processor(new ListProcessor());

                
                var discoverFilesProgress = new Progress<int>(DiscoverFilesProgress);
                
                // Step 1 - this task 
                Task discoverFiles = processor.ProcessDirectoryAsync(path, discoverFilesProgress);

                var populateGridProgress = new Progress<IFileDetails>(PopulateGridProgress);

                Task populateGrid = processor.PopulateFromQueueAsync(populateGridProgress);

                await Task.WhenAll(discoverFiles, populateGrid);

            }


        }

        /// <summary>
        /// This action is called by the progress bar
        /// </summary>
        /// <param name="value"></param>
        void PopulateGridProgress(IFileDetails value)
        {

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gridFiles);
            row.Cells[0].Value = value.Sequence;
            row.Cells[1].Value = value.FileName;
            row.Cells[2].Value = value.FilePath;
            row.Cells[3].Value = string.Format(@"{0:#,##0}", value.Size);
            row.Cells[4].Value = string.Format(@"{0:f}", value.DateLastTouched);
            gridFiles.Rows.Add(row);

        }


        void DiscoverFilesProgress(int value)
        {
            Debug.WriteLine(@"Found file {0}", value);
        }


    }
}
