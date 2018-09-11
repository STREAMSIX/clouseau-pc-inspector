using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_Inspector
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(uint pid);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        const uint ATTACH_PARENT_PROCESS = 0x0ffffffff;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdConsole = false;
            string[] args = Environment.GetCommandLineArgs();
            
            // We'll always have one argument (the program's exe is args[0])
            if(args.Length == 1)
            {
                // Run windows forms app
                FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                // First, try to attach to parent process console.
                // If that fails, create our own console
                if(!AttachConsole(ATTACH_PARENT_PROCESS))
                {
                    AllocConsole();
                    createdConsole = true;
                }

                Console.WriteLine("We'll run as a console app now");

                Console.WriteLine("arguments count {0}", args.Length);
                foreach(string str in args)
                {
                    Console.WriteLine(str);
                }

                Console.ReadKey();

                // At end, free our console
                if(createdConsole)
                    FreeConsole();
            }

        }


    }
}
