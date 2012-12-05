using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ScreenSaverLauncher
{
    static class Program
    {
        public static readonly string KEY = "Software\\WebkitScreenSaver";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0].ToLower().Contains("/p"))
                return;

            if (args.Length > 0 && args[0].ToLower().Contains("/c"))
                Application.Run(new SettingsForm());
            else
                RunApplication();

            
        }


        private static void RunApplication()
        {

            RegistryKey reg = Registry.CurrentUser.CreateSubKey(Program.KEY);
            string filePath = (string)reg.GetValue("FilePath", "");
            string fileArgs = (string)reg.GetValue("FileArgs", "");
            reg.Close();

            if (!File.Exists(filePath))
            {
                return;
            }
            

            //string filename = "D:\\Development\\WebkitScreenSaverWPF\\WebkitScreenSaverWPF\\bin\\Debug\\WebkitScreenSaverWPF.exe";
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(filePath, fileArgs);
            psi.WorkingDirectory = Path.GetDirectoryName(filePath);
            //psi.WorkingDirectory = "D:\\Development\\WebkitScreenSaverWPF\\WebkitScreenSaverWPF\\bin\\Debug\\";
            psi.RedirectStandardOutput = false;
            psi.RedirectStandardError = false;
            psi.RedirectStandardInput = false;
            // psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.UseShellExecute = false;

            System.Diagnostics.Process listFiles;

            listFiles = System.Diagnostics.Process.Start(psi);
            //System.IO.StreamReader myOutput = listFiles.StandardOutput;
            listFiles.WaitForExit();

            //if (!listFiles.HasExited)
            //{
            //    listFiles.Kill();
            //}
        }
    }
}
