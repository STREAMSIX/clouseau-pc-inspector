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
using System.Management;

namespace PC_Inspector
{
    public partial class Form1 : Form
    {
        private void inspectCodecBtn_Click(object sender, EventArgs e)
        {
            string NamespacePath = "\\\\.\\ROOT\\cimv2";
            string ClassName = "Win32_CodecFile";

            ManagementClass oClass = new ManagementClass(NamespacePath + ":" + ClassName);

            foreach(ManagementObject obj in oClass.GetInstances())
            {
                try
                {
                    if(obj == null)
                    {
                        continue;
                    }

                    string name = obj.GetPropertyValue("Name")?.ToString();
                    //string csname = obj.GetPropertyValue("CSName")?.ToString();
                    string description = obj.GetPropertyValue("Description")?.ToString();

                    this.richTextBox1.AppendText(string.Format("Name: {0}" + Environment.NewLine, name));
                    //this.richTextBox1.AppendText(string.Format("CSName: {0}" + Environment.NewLine, csname));
                    this.richTextBox1.AppendText(string.Format("description: {0}" + Environment.NewLine, description));



                    this.richTextBox1.AppendText(Environment.NewLine);
                }
                catch(Exception ex)
                {
                    this.richTextBox1.AppendText(string.Format("Exception: {0}" + Environment.NewLine, ex.Message),Color.Red);
                }
                
                
            }
        }
    }
}
