using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace String_Search
{
    public partial class FileStringSearcher : Form
    {
        public FileStringSearcher()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                labelShowPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

            listBoxResults.Items.Clear();
            string dirScan = labelShowPath.Text;
            string filename;
            string noFileFound = "String Not Found";
            if (string.IsNullOrWhiteSpace(textBoxStringToSearch.Text) || string.IsNullOrWhiteSpace(dirScan))
            {
                MessageBox.Show("Missing Path or Search String");
                return;
            }
            string[] allFiles = System.IO.Directory.GetFiles(dirScan, "*.*");
            foreach (string file in allFiles)
            {
                string[] lines = System.IO.File.ReadAllLines(file);
                string firstOccurrence = lines.FirstOrDefault(l => l.Contains(textBoxStringToSearch.Text));
                if (firstOccurrence != null)
                {
                    filename = System.IO.Path.GetFileName(file);
                    listBoxResults.Items.Add(filename);
                }
            }
            if (listBoxResults.Items.Count == 0)
            {
                listBoxResults.Items.Add(noFileFound);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBoxResults.Items.Clear();
            textBoxStringToSearch.Text = "";
            labelShowPath.Text = "";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            const string sPath = "save.txt";
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach(var item in listBoxResults.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.Close();

            MessageBox.Show("Results Saved!");
        }
    }
}
