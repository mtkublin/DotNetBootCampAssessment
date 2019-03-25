using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void AddFilesBtn_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileContent = string.Empty;

            if (this.addFilesDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = addFilesDialog.FileName;

                var fileStream = addFilesDialog.OpenFile();

                this.addedFilesListView.Items.Add(addFilesDialog.FileName);

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
        }
    }
}
