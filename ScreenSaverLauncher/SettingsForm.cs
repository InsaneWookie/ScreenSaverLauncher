using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace ScreenSaverLauncher
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey(Program.KEY);
            tbFilePath.Text = (string)reg.GetValue("FilePath", "");
            tbArgs.Text = (string)reg.GetValue("FileArgs", "");
            reg.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                RegistryKey reg = Registry.CurrentUser.CreateSubKey(Program.KEY);
                reg.SetValue("FilePath", tbFilePath.Text);
                reg.SetValue("FileArgs", tbArgs.Text);
                reg.Close();
            }

            base.OnClosed(e);
        }
        
        
        private void settingsButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {

            this.openFileDialog.InitialDirectory = Path.GetDirectoryName(tbFilePath.Text);

            if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (this.openFileDialog.CheckFileExists)
                {
                    this.tbFilePath.Text = this.openFileDialog.FileName;
                }
            }
        }
    }
}
