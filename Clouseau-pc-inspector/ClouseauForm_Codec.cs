using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Management;

namespace PC_Inspector
{
    public partial class ClouseauForm : Form
    {
        private int mCodecSortColumn = -1;
        private List<CodecInfo> mAllCodecInfo = new List<CodecInfo>();

        private void codecColumnHeader_Click(object sender, ColumnClickEventArgs e)
        {
            if(e.Column != mCodecSortColumn)
            {
                mCodecSortColumn = e.Column;
                this.codecListView.Sorting = SortOrder.Ascending;
            }
            else
            {
                if(this.codecListView.Sorting == SortOrder.Ascending)
                {
                    this.codecListView.Sorting = SortOrder.Descending;
                }
                else
                {
                    this.codecListView.Sorting = SortOrder.Ascending;
                }
            }

            this.codecListView.SetSortIcon(e.Column, this.codecListView.Sorting);
            this.codecListView.ListViewItemSorter = new ListViewItemComparer(e.Column,this.codecListView.Sorting);
            this.codecListView.Sort();
        }

        private void inspectCodecBtn_Click(object sender, EventArgs e)
        {

            this.inAppLogTextBox.AppendText("Codec drivers" + Environment.NewLine);
            // Codec drivers
            using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
            using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Drivers32"))
            {
                List<CodecInfo> driverInfos = CodecInfo.AllCodecDriversInfoForRegKey(key, this.inAppLogTextBox);
                processCodecInfos(driverInfos);

                this.inAppLogTextBox.AppendText(string.Format("{0} codecs driver found", driverInfos.Count) + Environment.NewLine);
            }


            // DirectShow filters

            this.inAppLogTextBox.AppendText("DirectShow Filters " + Environment.NewLine);

            using(RegistryKey hkcr = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
            using(RegistryKey key = hkcr.OpenSubKey(@"CLSID\{083863F1-70DE-11d0-BD40-00A0C911CE86}\Instance"))
            {
                List<CodecInfo> directShowFilterInfos = CodecInfo.AllDirectShowFilterInfoForRegKey(key, this.inAppLogTextBox);
                processCodecInfos(directShowFilterInfos);

                this.inAppLogTextBox.AppendText(string.Format("{0} direct show filters found", directShowFilterInfos.Count) + Environment.NewLine);
            }
        }
        
        private void saveCodecBtn_Click(object sender, EventArgs e)
        {
            codecInfoSaveFileDialog.FileName = "codecReport";
            codecInfoSaveFileDialog.Filter = "Json|*.json";
            codecInfoSaveFileDialog.Title = "Save Codec Report";
            codecInfoSaveFileDialog.ShowDialog();
        }

        private void codecInfoSaveFileOk(object sender, CancelEventArgs e)
        {
            if((this.mAllCodecInfo != null) &&
                (this.mAllCodecInfo.Count > 0))
            {
                if(!string.IsNullOrWhiteSpace(codecInfoSaveFileDialog.FileName))
                {
                    this.inAppLogTextBox.AppendText("Saving PC's Codec report to ... " + codecInfoSaveFileDialog.FileName + Environment.NewLine, Color.Green);

                    File.WriteAllText(this.codecInfoSaveFileDialog.FileName, JsonConvert.SerializeObject(this.mAllCodecInfo, Formatting.Indented));
                }
            }
            else
            {
                this.inAppLogTextBox.AppendText("Nothing to save no report was generated ... " + Environment.NewLine, Color.Orange);
            }
        }

        private void processCodecInfos(List<CodecInfo> regCodecInfos)
        {
            foreach(CodecInfo codecInfo in regCodecInfos)
            {
                ListViewItem item = new ListViewItem(codecInfo.FormatForFormList());
                this.codecListView.Items.Add(item);

                mAllCodecInfo.Add(codecInfo);
            }
        }
    }
}
