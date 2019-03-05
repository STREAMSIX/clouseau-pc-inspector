using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PC_Inspector
{
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DisplayName  :  " + DisplayName + Environment.NewLine);
            sb.Append("FileVersion  :  " + FileVersion + Environment.NewLine);
            sb.Append("FileDescription  :  " + FileDescription + Environment.NewLine);
            sb.Append("InstallModDate  :  " + InstallModDate + Environment.NewLine);
            sb.Append("DriverType  :  " + DriverType + Environment.NewLine);
            sb.Append("FileName  :  " + FileName + Environment.NewLine);
            sb.Append("DriverKey  :  " + DriverKey + Environment.NewLine);

            return sb.ToString();
        }

        public static List<CodecInfo> AllCodecDriversInfoForRegKey(RegistryKey key, RichTextBox logBox)
        {
            List<CodecInfo> codecDriverInfo = new List<CodecInfo>();

            string system32Path = Environment.SystemDirectory;

            int audioAndVideoDriverCount = 0;
            foreach(string valueName in key.GetValueNames())
            {
                string dataValue = key.GetValue(valueName).ToString();
                logBox.AppendText(valueName + " : " + dataValue);

                string driverPath = "";

                string dirName = Path.GetDirectoryName(dataValue);

                if(!string.IsNullOrWhiteSpace(dirName) &&
                    dirName.ToUpperInvariant().Equals(system32Path.ToUpperInvariant()))
                {
                    logBox.AppendText(" <-----");
                    driverPath = dataValue;
                }
                else
                {
                    driverPath = Path.Combine(system32Path, dataValue);
                }

                logBox.AppendText(Environment.NewLine);

                logBox.AppendText(driverPath);
                bool isFileExists = File.Exists(driverPath);
                logBox.AppendText(isFileExists ? " -Exists" : " -NotFound", isFileExists ? Color.Green : Color.Red);


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

                    codecDriverInfo.Add(ci);

                    audioAndVideoDriverCount++;
                    break;

                    case ".acm":
                    fileTypeColor = Color.Brown;
                    ci.DriverType = "Audio";

                    codecDriverInfo.Add(ci);

                    audioAndVideoDriverCount++;
                    break;

                    case ".drv":
                    ci.DriverType = "HardWare";
                    fileTypeColor = Color.DarkGoldenrod;
                    break;

                    default:
                    break;
                }

                logBox.AppendText(" " + driverFileType, fileTypeColor);

                logBox.AppendText(Environment.NewLine);
            }

            logBox.AppendText("ValueName count " + key.GetValueNames().Length + Environment.NewLine);
            logBox.AppendText("audioAndVideoDriverCount " + audioAndVideoDriverCount + Environment.NewLine);
            
            return codecDriverInfo;
        }

        public static List<CodecInfo> AllDirectShowFilterInfoForRegKey(RegistryKey key, RichTextBox logBox)
        {
            List<CodecInfo> directShowFiltersInfo = new List<CodecInfo>();
            
            foreach(string subKeyName in key.GetSubKeyNames())
            {
                using(RegistryKey subkey = key.OpenSubKey(subKeyName))
                {
                    string friendlyName = subkey.GetValue("FriendlyName")?.ToString().Trim();
                    string clsid = subkey.GetValue("CLSID")?.ToString().Trim();

                    logBox.AppendText(friendlyName + " : " + clsid);
                    logBox.AppendText(Environment.NewLine);

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

                                    logBox.AppendText(valueName + " : " + dataValue);
                                    logBox.AppendText(Environment.NewLine);
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

                        directShowFiltersInfo.Add(ci);
                    }

                    #endregion
                }
            }

            return directShowFiltersInfo;
        }
    }
}
