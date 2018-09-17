using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PC_Inspector
{
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
