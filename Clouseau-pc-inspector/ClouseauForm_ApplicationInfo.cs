using Microsoft.Win32;
using System;
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

namespace PC_Inspector
{
    public partial class ClouseauForm : Form
    {
        private int mApplicationSortColumn = -1;
        private List<ApplicationInfo> mAllApplicationInfos = new List<ApplicationInfo>();

        private void applicationColumnHeader_Click(Object sender, ColumnClickEventArgs e)
        {
            if(e.Column != mApplicationSortColumn)
            {
                mApplicationSortColumn = e.Column;
                this.applicationListView.Sorting = SortOrder.Ascending;
            }
            else
            {
                if(this.applicationListView.Sorting == SortOrder.Ascending)
                {
                    this.applicationListView.Sorting = SortOrder.Descending;
                }
                else
                {
                    this.applicationListView.Sorting = SortOrder.Ascending;
                }
            }

            this.applicationListView.SetSortIcon(e.Column, this.applicationListView.Sorting);
            this.applicationListView.ListViewItemSorter = new ListViewItemComparer(e.Column, this.applicationListView.Sorting);
            this.applicationListView.Sort();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.mAllApplicationInfos.Clear();
            this.applicationListView.Items.Clear();

            // LocalMachine_32
            using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                List<ApplicationInfo> x32Applications = ApplicationInfo.AllApplicationInfoForRegKey(key, "x32", this.inAppLogTextBox);
                processApplicationInfos(x32Applications);

                this.inAppLogTextBox.AppendText(string.Format("x32 had {0} installed applications", x32Applications.Count) + Environment.NewLine);
            }

            // LocalMachine_64
            using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                List<ApplicationInfo> x64Applications = ApplicationInfo.AllApplicationInfoForRegKey(key, "x64", this.inAppLogTextBox);
                processApplicationInfos(x64Applications);

                this.inAppLogTextBox.AppendText(string.Format("x64 had {0} installed applications", x64Applications.Count) + Environment.NewLine);
            }


            // localuser
            using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
            using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                List<ApplicationInfo> localUserApplications = ApplicationInfo.AllApplicationInfoForRegKey(key, "localuser", this.inAppLogTextBox);
                processApplicationInfos(localUserApplications);

                this.inAppLogTextBox.AppendText(string.Format("localuser had {0} installed applications", localUserApplications.Count) + Environment.NewLine);
            }

            this.inAppLogTextBox.AppendText(string.Format("Found {0} installed applications", mAllApplicationInfos.Count) + Environment.NewLine);
        }

        private void saveApplicationBtn_Click(object sender, EventArgs e)
        {
            applicationInfoSaveFileDialog.FileName = "applicationReport";
            applicationInfoSaveFileDialog.Filter = "Json|*.json";
            applicationInfoSaveFileDialog.Title = "Save Application report";
            applicationInfoSaveFileDialog.ShowDialog();
        }

        private void applicationInfoSaveFileOk(object sender, CancelEventArgs e)
        {
            if(this.mAllApplicationInfos != null && this.mAllApplicationInfos.Count > 0)
            {
                if(!string.IsNullOrWhiteSpace(applicationInfoSaveFileDialog.FileName))
                {
                    this.inAppLogTextBox.AppendText("Saving PC report to ... " + applicationInfoSaveFileDialog.FileName + Environment.NewLine, Color.Green);

                    File.WriteAllText(this.applicationInfoSaveFileDialog.FileName, JsonConvert.SerializeObject(this.mAllApplicationInfos, Formatting.Indented));
                }
            }
            else
            {
                this.inAppLogTextBox.AppendText("Nothing to save no report was generated ... " + Environment.NewLine, Color.Orange);
            }

        }

        private void processApplicationInfos(List<ApplicationInfo> regApplicationInfo)
        {
            foreach(ApplicationInfo appInfo in regApplicationInfo)
            {
                ListViewItem item = new ListViewItem(appInfo.FormatForFormList());
                this.applicationListView.Items.Add(item);

                mAllApplicationInfos.Add(appInfo);
            }
        }
    }
}
