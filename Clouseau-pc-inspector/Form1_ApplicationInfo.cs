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
    public partial class Form1 : Form
    {
        private List<ApplicationInfo> allApplicationInfos = new List<ApplicationInfo>();

        private void startButton_Click(object sender, EventArgs e)
        {
            clearApplicationsDetails();

            // LocalMachine_32
            using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                List<ApplicationInfo> x32Applications = ApplicationInfo.AllApplicationInfoForRegKey(key, "x32", this.richTextBox1);
                processApplicationInfos(x32Applications);

                this.richTextBox1.AppendText(string.Format("x32 had {0} installed applications", x32Applications.Count) + Environment.NewLine);
            }

            // LocalMachine_64
            using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                List<ApplicationInfo> x64Applications = ApplicationInfo.AllApplicationInfoForRegKey(key, "x64", this.richTextBox1);
                processApplicationInfos(x64Applications);

                this.richTextBox1.AppendText(string.Format("x64 had {0} installed applications", x64Applications.Count) + Environment.NewLine);
            }


            // localuser
            using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
            using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                List<ApplicationInfo> localUserApplications = ApplicationInfo.AllApplicationInfoForRegKey(key, "localuser", this.richTextBox1);
                processApplicationInfos(localUserApplications);

                this.richTextBox1.AppendText(string.Format("localuser had {0} installed applications", localUserApplications.Count) + Environment.NewLine);
            }

            this.richTextBox1.AppendText(string.Format("Found {0} installed applications", allApplicationInfos.Count) + Environment.NewLine);
        }

        private void saveApplicationBtn_Click(object sender, EventArgs e)
        {
            applicationInfoSaveFileDialog.FileName = "applicationReport";
            applicationInfoSaveFileDialog.Filter = "Json|*.json";
            applicationInfoSaveFileDialog.Title = "Save pc report";
            applicationInfoSaveFileDialog.ShowDialog();
        }

        private void applicationInfoSaveFileOk(object sender, CancelEventArgs e)
        {
            if(this.allApplicationInfos != null && this.allApplicationInfos.Count > 0)
            {
                if(!string.IsNullOrWhiteSpace(applicationInfoSaveFileDialog.FileName))
                {
                    this.richTextBox1.AppendText("Saving PC report to ... " + applicationInfoSaveFileDialog.FileName + Environment.NewLine, Color.Green);

                    File.WriteAllText(this.applicationInfoSaveFileDialog.FileName, JsonConvert.SerializeObject(this.allApplicationInfos, Formatting.Indented));
                }
            }
            else
            {
                this.richTextBox1.AppendText("Nothing to save no report was generated ... " + Environment.NewLine, Color.Orange);
            }

        }

        private void processApplicationInfos(List<ApplicationInfo> regApplicationInfo)
        {
            foreach(ApplicationInfo appInfo in regApplicationInfo)
            {
                ListViewItem item = new ListViewItem(appInfo.FormatForFormList());
                this.applicationDetailedList.Items.Add(item);

                allApplicationInfos.Add(appInfo);
            }
        }

        private void clearApplicationsDetails()
        {
            this.allApplicationInfos.Clear();
            this.applicationDetailedList.Items.Clear();
        }
    }
}
