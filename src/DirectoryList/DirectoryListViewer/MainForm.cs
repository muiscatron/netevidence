using DirectoryListLibrary;
using DirectoryProcessor;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileQueueProcessor.Memory;
using FileQueueProcessor.MSMQ;
using System.Collections.Generic;
using System.ComponentModel;

namespace DirectoryListViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private List<IFileDetails> _fileList = new List<IFileDetails>();
        private BindingList<IFileDetails> _bindingList;
        private BindingSource _source;
        


        private async void btnSelectFolder_Click(object sender, EventArgs e)
        {
            try
            {
                _bindingList = new BindingList<IFileDetails>(_fileList);
                _source = new BindingSource(_bindingList, null);
                gridFiles.DataSource = _source;

                gridFiles.Refresh();

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    var path = folderBrowserDialog1.SelectedPath;

                    label1.Text = path;
                    

                    var config = new AppConfig();
                    config.QueueName = @".\Private$\DirectoryList";

                    var processor = new Processor(new MsmqProcessor(config));


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
            _fileList.Add(value);
            gridFiles.DataSource = _source;
            

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
