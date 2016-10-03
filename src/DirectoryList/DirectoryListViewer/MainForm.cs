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

                    listFiles.Items.Clear();

                    var config = new AppConfig();
                    config.QueueName = @".\Private$\DirectoryList";
                    config.IdleTimeout = 5;


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

            Debug.WriteLine(@"Populating grid with file {0}; {1}", value.Sequence, value.FileName);

            string[] columnData = new string[5];
            columnData[0] = string.Format(@"{0}", value.Sequence);
            columnData[1] = value.FileName;
            columnData[2] = value.FilePath;
            columnData[3] = string.Format(@"{0:#,##0}", value.FileSize);
            columnData[4] = string.Format(@"{0:f}", value.DateLastTouched);

            ListViewItem item = new ListViewItem(columnData);
            listFiles.BeginUpdate();
            listFiles.Items.Add(item);
            listFiles.EndUpdate();

        }


        void DiscoverFilesProgress(int value)
        {
            Debug.WriteLine(@"Found file {0}", value);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
