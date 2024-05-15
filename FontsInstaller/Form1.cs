
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
            string[] fontsList = { "*.otf" , "*.ttf", "*.ttc", "*.pfb", "*.fnt", "*.fon" };
            string[] list = null;
            string CurrenDir = Directory.GetCurrentDirectory();

            foreach (string font in fontsList)
            {
                list = Directory.GetFiles(CurrenDir, font);
                installer(list);
            }

            textboxAppend("All Done.");
            Cursor = Cursors.Default;

        }

        private void installer(string[] list)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts", true);
            foreach (string file in list)
            {
                try 
                {
                    CFileInfo oDetailedFileInfo = new CFileInfo(file);

                    string dest = System.Environment.GetFolderPath(
    System.Environment.SpecialFolder.Fonts) + "\\" + oDetailedFileInfo.FileName;
                    File.Copy(file, dest, true);

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