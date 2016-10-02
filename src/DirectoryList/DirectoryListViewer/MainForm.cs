using DirectoryListLibrary;
using DirectoryProcessor;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
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
                    config.PullBatchSize = 100;
                    config.IdleTimeout = 5;

                    var processor = new Processor(new MsmqProcessor(config), config);


                    // Step 1 - this task 
                    Task discoverFiles = processor.ProcessDirectoryAsync(path, null);

                    var populateGridProgress = new Progress<List<IFileDetails>>(PopulateGridProgress);

                    populateGridProgress.ProgressChanged += PopulateGridProgress_ProgressChanged;


                    Task populateGrid = processor.PopulateFromQueueAsync(populateGridProgress);

                    await Task.WhenAll(discoverFiles, populateGrid).ConfigureAwait(false);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void PopulateGridProgress_ProgressChanged(object sender, List<IFileDetails> filesBuffer)
        {
            Debug.WriteLine(@"Populating grid with file buffer data; {0}", filesBuffer.Count);


            foreach (FileDetails fileItem in filesBuffer)
            {

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridFiles);
                row.Cells[0].Value = fileItem.Sequence;
                row.Cells[1].Value = fileItem.FileName;
                row.Cells[2].Value = fileItem.FilePath;
                row.Cells[3].Value = string.Format(@"{0:#,##0}", fileItem.FileSize);
                row.Cells[4].Value = string.Format(@"{0:f}", fileItem.DateLastTouched);
                gridFiles.Rows.Add(row);
            }
        }

        /// <summary>
        /// This action is called by the progress bar
        /// </summary>
        /// <param name="value"></param>
        void PopulateGridProgress(List<IFileDetails> filesBuffer)
        {

            //Debug.WriteLine(@"Populating grid with file buffer data; {0}", filesBuffer.Count);


            //foreach (FileDetails fileItem in filesBuffer)
            //{

            //    DataGridViewRow row = new DataGridViewRow();
            //    row.CreateCells(gridFiles);
            //    row.Cells[0].Value = fileItem.Sequence;
            //    row.Cells[1].Value = fileItem.FileName;
            //    row.Cells[2].Value = fileItem.FilePath;
            //    row.Cells[3].Value = string.Format(@"{0:#,##0}", fileItem.FileSize);
            //    row.Cells[4].Value = string.Format(@"{0:f}", fileItem.DateLastTouched);
            //    gridFiles.Rows.Add(row);
            //}
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
