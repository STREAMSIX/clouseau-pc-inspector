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

namespace PC_Inspector
{
    public partial class Form1 : Form
    {
        private List<ApplicationInfo> allApplicationInfos = new List<ApplicationInfo>();

        public Form1()
        {
            InitializeComponent();
        }

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

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Json|*.json";
            saveFileDialog1.Title = "Save pc report";
            saveFileDialog1.ShowDialog();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            clearApplicationsDetails();
        }

        private void clearApplicationsDetails()
        {
            this.allApplicationInfos.Clear();
            this.richTextBox1.Clear();
            this.applicationDetailedList.Items.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if(this.allApplicationInfos != null && this.allApplicationInfos.Count > 0)
            {
                if(!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                {
                    this.richTextBox1.AppendText("Saving PC report to ... " + saveFileDialog1.FileName + Environment.NewLine, Color.Green);

                    File.WriteAllText(this.saveFileDialog1.FileName, JsonConvert.SerializeObject(this.allApplicationInfos, Formatting.Indented));
                }
            }
            else
            {
                this.richTextBox1.AppendText("Nothing to save no report was generated ... " + Environment.NewLine,Color.Orange);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }

    public class ApplicationInfo
    {
        public string DisplayName;
        public string DisplayVersion;
        public string Version;
        public string InstallDate;
        public string RegLocation;

        public string[] FormatForFormList()
        {
            return new string[]
            {
                DisplayName,
                DisplayVersion,
                Version,
                InstallDate,
                RegLocation
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DisplayName  :  " + DisplayName + Environment.NewLine);
            sb.Append("DisplayVersion  :  " + DisplayVersion + Environment.NewLine);
            sb.Append("Version  :  " + Version + Environment.NewLine);
            sb.Append("InstallDate  :  " + InstallDate + Environment.NewLine);
            sb.Append("RegLocation  :  " + RegLocation + Environment.NewLine);

            return sb.ToString();
        }

        public static List<ApplicationInfo> AllApplicationInfoForRegKey(RegistryKey key, string regLocation, RichTextBox logBox)
        {
            List<ApplicationInfo> regApplicationInfos = new List<ApplicationInfo>();

            if(key == null)
            {
                logBox.AppendText(string.Format("Error: Could not get {0}'s installed program registry", regLocation) + Environment.NewLine, Color.Red);
            }
            else if(key.SubKeyCount <= 0)
            {
                logBox.AppendText(string.Format("Error: {0}'s installed program registry contained no subkeys", regLocation) + Environment.NewLine, Color.Red);
            }
            else
            {
                foreach(string subkey_name in key.GetSubKeyNames())
                {
                    using(RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        try
                        {
                            if(subkey.GetValue("DisplayName") == null)
                            {
                                continue;
                            }

                            ApplicationInfo appInfo = new ApplicationInfo
                            {
                                DisplayName = subkey.GetValue("DisplayName")?.ToString().Trim(),
                                DisplayVersion = subkey.GetValue("DisplayVersion")?.ToString().Trim(),
                                Version = subkey.GetValue("Version")?.ToString().Trim(),
                                InstallDate = subkey.GetValue("InstallDate")?.ToString().Trim(),
                                RegLocation = regLocation
                            };
                            
                            regApplicationInfos.Add(appInfo);
                        }
                        catch(Exception ex)
                        {
                            logBox.AppendText("Exception: " + ex.ToString() + " occured trying to get applications from CurrentUser ..." + Environment.NewLine, Color.Red);
                        }
                    }
                }
            }

            return regApplicationInfos;
        }
    }
}
