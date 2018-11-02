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
    public partial class Form1 : Form
    {
        private int codecSortColumn = -1;

        private void codecColumnHeader_Click(object sender, ColumnClickEventArgs e)
        {
            if(e.Column != codecSortColumn)
            {
                codecSortColumn = e.Column;
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
            // HACK: Temporarly disabled
            if(false)
            {
                this.richTextBox1.AppendText("Codec drivers" + Environment.NewLine);
                // Codec drivers
                using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
                using(RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Drivers32"))
                {
                    inspectCodecDrivers(key);
                }
            }

            // DirectShow filters

            this.richTextBox1.AppendText("DirectShow Filters " + Environment.NewLine);

            using(RegistryKey hkcr = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
            using(RegistryKey key = hkcr.OpenSubKey(@"CLSID\{083863F1-70DE-11d0-BD40-00A0C911CE86}\Instance"))
            {
                foreach(string subKeyName in key.GetSubKeyNames())
                {
                    using(RegistryKey subkey = key.OpenSubKey(subKeyName))
                    {
                        string friendlyName = subkey.GetValue("FriendlyName")?.ToString().Trim();
                        string clsid = subkey.GetValue("CLSID")?.ToString().Trim();

                        this.richTextBox1.AppendText(friendlyName + " : " + clsid);
                        this.richTextBox1.AppendText(Environment.NewLine);

                        string fileName = "";

                        #region Related registry location
                        using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
                        using(RegistryKey relatedKey = hklm.OpenSubKey(@"SOFTWARE\Classes\CLSID\" + clsid + @"\InprocServer32"))
                        {
                            if(relatedKey != null)
                            {
                                foreach(string valueName in relatedKey.GetValueNames())
                                {
                                    if(valueName.Length == 0)
                                    {
                                        string dataValue = relatedKey.GetValue(valueName).ToString();

                                        if(!string.IsNullOrWhiteSpace(dataValue))
                                        {
                                            fileName = dataValue;
                                        }

                                        this.richTextBox1.AppendText(valueName + " : " + dataValue);
                                        this.richTextBox1.AppendText(Environment.NewLine);
                                    }

                                }
                            }
                        }

                        if(File.Exists(fileName))
                        {
                            FileInfo fi = new FileInfo(fileName);
                            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(fileName);

                            CodecInfo ci = new CodecInfo
                            {
                                DisplayName = friendlyName,
                                FileVersion = fvi.FileVersion?.ToString().Trim(),
                                FileDescription = fvi.FileDescription?.ToString().Trim(),
                                InstallModDate = fi.LastWriteTime.ToLongDateString()?.ToString().Trim(),
                                FileName = fileName,
                                DriverType = "DirectShow Filter",
                            };

                            ListViewItem audItem = new ListViewItem(ci.FormatForFormList());
                            this.codecListView.Items.Add(audItem);
                        }

                        #endregion
                    }
                }
            }
        }

        private void inspectCodecDrivers(RegistryKey key)
        {
            string system32Path = Environment.SystemDirectory;

            int audioAndVideoDriverCount = 0;
            foreach(string valueName in key.GetValueNames())
            {
                string dataValue = key.GetValue(valueName).ToString();
                this.richTextBox1.AppendText(valueName + " : " + dataValue);

                string driverPath = "";

                string dirName = Path.GetDirectoryName(dataValue);

                if(!string.IsNullOrWhiteSpace(dirName) &&
                    dirName.ToUpperInvariant().Equals(system32Path.ToUpperInvariant()))
                {
                    this.richTextBox1.AppendText(" <-----");
                    driverPath = dataValue;
                }
                else
                {
                    driverPath = Path.Combine(system32Path, dataValue);
                }

                this.richTextBox1.AppendText(Environment.NewLine);

                this.richTextBox1.AppendText(driverPath);
                bool isFileExists = File.Exists(driverPath);
                this.richTextBox1.AppendText(isFileExists ? " -Exists" : " -NotFound", isFileExists ? Color.Green : Color.Red);


                FileInfo fi = new FileInfo(driverPath);
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(driverPath);

                CodecInfo ci = new CodecInfo
                {
                    DisplayName = valueName,
                    FileVersion = fvi.FileVersion?.ToString().Trim(),
                    FileDescription = fvi.FileDescription?.ToString().Trim(),
                    InstallModDate = fi.LastWriteTime.ToLongDateString()?.ToString().Trim(),
                    FileName = driverPath,
                    DriverKey = valueName
                };


                string driverFileType = Path.GetExtension(driverPath);
                Color fileTypeColor = Color.Black;
                switch(driverFileType)
                {
                    case ".dll":
                    fileTypeColor = Color.Blue;
                    ci.DriverType = "Video";

                    ListViewItem vidItem = new ListViewItem(ci.FormatForFormList());
                    this.codecListView.Items.Add(vidItem);

                    audioAndVideoDriverCount++;
                    break;

                    case ".acm":
                    fileTypeColor = Color.Brown;
                    ci.DriverType = "Audio";

                    ListViewItem audItem = new ListViewItem(ci.FormatForFormList());
                    this.codecListView.Items.Add(audItem);

                    audioAndVideoDriverCount++;
                    break;

                    case ".drv":
                    ci.DriverType = "HardWare";
                    fileTypeColor = Color.DarkGoldenrod;
                    break;

                    default:
                    break;
                }

                this.richTextBox1.AppendText(" " + driverFileType, fileTypeColor);

                this.richTextBox1.AppendText(Environment.NewLine);
            }

            this.richTextBox1.AppendText("ValueName count " + key.GetValueNames().Length + Environment.NewLine);
            this.richTextBox1.AppendText("audioAndVideoDriverCount " + audioAndVideoDriverCount + Environment.NewLine);
        }
    }

    class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;

            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                       ((ListViewItem)y).SubItems[col].Text);

            if(order == SortOrder.Descending)
            {
                returnVal *= -1;
            }

            return returnVal;
        }
    }

    public class CodecInfo
    {
        public string DisplayName;
        public string FileVersion;
        public string FileDescription;
        public string InstallModDate;
        public string DriverType;
        public string FileName;
        public string DriverKey;

        public string[] FormatForFormList()
        {
            return new string[]
            {
                DisplayName,
                FileVersion,
                FileDescription,
                InstallModDate,
                DriverType,
                FileName,
                DriverKey
            };
        }
    }
}
