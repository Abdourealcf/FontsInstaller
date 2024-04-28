
using System.IO;
using System.Windows.Forms;
using System;
using Microsoft.Win32;

namespace FontsInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;
            textBox1.Text = string.Empty;

            string[] list = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.otf");
            installer(list);
            list = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ttf");
            installer(list);
            list = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ttc");
            installer(list);
            list = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pfb");
            installer(list);
            list = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.fnt");
            installer(list);
            list = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.fon");
            installer(list);

            textboxAppend("All Done.");
            Cursor = Cursors.Default;

        }

        private void installer(string[] list)
        {
            foreach (string file in list)
            {
                try 
                {
                    CFileInfo oDetailedFileInfo = new CFileInfo(file);

                    string dest = System.Environment.GetFolderPath(
    System.Environment.SpecialFolder.Fonts) + "\\" + oDetailedFileInfo.FileName;
                    File.Copy(file, dest, true);

                    RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts", true);

                    key.SetValue(oDetailedFileInfo.FileTitle, oDetailedFileInfo.FileName);
                    textboxAppend("-> " + Path.GetFileName(file) + " -> OK" );
                } 
                catch (Exception ex)
                {
                    textboxAppend("-> " +Path.GetFileName(file) + " -> " + ex.Message);
                }
                

            }

        }



        private void textboxAppend(string text)
        {
            textBox1.AppendText(text + "\r\n");

        }
    }


}