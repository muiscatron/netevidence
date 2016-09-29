﻿using DirectoryListLibrary;
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
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var path =  folderBrowserDialog1.SelectedPath;

                label1.Text = path;

                var config = new AppConfig();
                config.QueueName = "";

                var processor = new Processor(config, new ListProcessor());

                var discoverFilesProgress = new Progress<int>(DiscoverFilesProgress);

                Task discoverFiles = processor.ProcessFolderAsync(path, discoverFilesProgress);

                var populateGridProgress = new Progress<IFileInfo>(UpdateGridProgress);

                Task populateGrid = processor.PopulateFromQueue(populateGridProgress);

                await Task.WhenAll(discoverFiles, populateGrid);

            }


        }

        /// <summary>
        /// This action is called by the progress bar
        /// </summary>
        /// <param name="value"></param>
        void UpdateGridProgress(IFileInfo value)
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
