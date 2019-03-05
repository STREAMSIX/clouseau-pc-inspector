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
        private void runAndSaveDxDiag()
        {
            var psi = new ProcessStartInfo();
            if(IntPtr.Size == 4 && Environment.Is64BitOperatingSystem)
            {
                // Need to run the 64-bit version
                psi.FileName = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                    "sysnative\\dxdiag.exe");

                Invoke(new Action(() =>
                {
                    this.inAppLogTextBox.AppendText("64bit dxdiag ..." + Environment.NewLine);
                }));
            }
            else
            {
                // Okay with the native version
                psi.FileName = System.IO.Path.Combine(
                    Environment.SystemDirectory,
                    "dxdiag.exe");

                Invoke(new Action(() =>
                {
                    this.inAppLogTextBox.AppendText("32bit dxdiag ..." + Environment.NewLine);
                }));
            }

            try
            {
                psi.Arguments = "/t " + dxDiagSaveFileDialog.FileName;
                using(var prc = Process.Start(psi))
                {
                    prc.WaitForExit();

                    if(prc.ExitCode != 0)
                    {
                        Invoke(new Action(() =>
                        {
                            this.inAppLogTextBox.AppendText("Error: DXDIAG failed with exit code " + prc.ExitCode.ToString(), Color.Red);
                        }));
                    }
                }
            }
            catch(Exception ex)
            {
                Invoke(new Action(() =>
                {
                    this.inAppLogTextBox.AppendText("Exception: " + ex.ToString() + " occured trying to run dxdiag ...", Color.Red);
                }));
            }
        }

        private void dxDiagSaveFileOk(object sender, CancelEventArgs e)
        {
            this.inAppLogTextBox.AppendText("Started up dxdiag ... " + Environment.NewLine);
            this.dxdiagBackgroundWorker.RunWorkerAsync();
        }

        private void saveDxDiagButton_Click(object sender, EventArgs e)
        {
            dxDiagSaveFileDialog.FileName = "dxdiag";
            dxDiagSaveFileDialog.Filter = "Text|*.txt";
            dxDiagSaveFileDialog.Title = "Save dxdiag report";
            dxDiagSaveFileDialog.ShowDialog();
        }

        private void dxdiagBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            runAndSaveDxDiag();
        }

        private void DxdiagBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.inAppLogTextBox.AppendText("finished saving dxdiag to file ... " + dxDiagSaveFileDialog.FileName + Environment.NewLine, Color.Green);
        }
    }   
}