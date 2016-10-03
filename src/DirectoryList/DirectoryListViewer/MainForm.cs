using DirectoryListLibrary;
using DirectoryProcessor;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileQueueProcessor.Memory;
using FileQueueProcessor.MSMQ;

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
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    var path = folderBrowserDialog1.SelectedPath;

                    label1.Text = path;
                    gridFiles.Rows.Clear();

                    var config = new AppConfig();
                    config.QueueName = @".\Private$\DirectoryList";
                    config.IdleTimeout = 5;

                    foreach (DataGridViewColumn c in gridFiles.Columns)
                    {
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }


                    var processor = new Processor(new MsmqProcessor(config), config);


                    var discoverFilesProgress = new Progress<int>(DiscoverFilesProgress);

                    // Step 1 - this task 
                    Task discoverFiles = processor.ProcessDirectoryAsync(path, discoverFilesProgress);

                    var populateGridProgress = new Progress<IFileDetails>(PopulateGridProgress);

                    Task populateGrid = processor.PopulateFromQueueAsync(populateGridProgress);

                    await Task.WhenAll(discoverFiles, populateGrid);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            row.Cells[3].Value = string.Format(@"{0:#,##0}", value.FileSize);
            row.Cells[4].Value = string.Format(@"{0:f}", value.DateLastTouched);
            gridFiles.Rows.Add(row);

        }


        void DiscoverFilesProgress(int value)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
