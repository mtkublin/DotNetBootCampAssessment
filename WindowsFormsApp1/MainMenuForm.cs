using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ReqRaportsApp
{
    public partial class Form1 : Form
    {
        List<request> RequestsList = new List<request>();
        List<string> AddedFiles = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        public void AddFilesBtn_Click(object sender, EventArgs e)
        {
            List<string> fileContents = new List<string>();

            if (this.addFilesDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = addFilesDialog.FileNames;

                foreach (string fp in filePaths) if (!AddedFiles.Contains(fp))
                {
                    string fileName = fp.Substring(fp.LastIndexOf("\\") + 1);
                    this.addedFilesListView.Items.Add(fileName);

                    if (fp.EndsWith(".xml"))
                    {
                        MyXmlSerializer.DeserializeXmlObject(fp, RequestsList);
                    }
                    else if (fp.EndsWith(".json"))
                    {
                        MyJsonSerializer.DesarializeJsonObject(fp, RequestsList);
                    }
                    else if (fp.EndsWith(".csv"))
                    {
                        MyCsvSerializer.DeserializeCsvObject(fp, RequestsList);
                    }

                    AddedFiles.Add(fp);
                }
            }
        }

        public void ReqQuantBtn_Click(object sender, EventArgs e)
        {
            int allReqsCount = RequestsList.Count;

            DialogResult res = MessageBox.Show("Ilość zamówień: " + allReqsCount.ToString() + "\nCzy chcesz zapisać raport?", "Ilość zamówień", MessageBoxButtons.OKCancel);

            if (res == DialogResult.OK)
            {
                MessageBox.Show("Zapisano raport");
            }
        }

        public void ReqQuantForClientIdBtn_Click(object sender, EventArgs e)
        {

        }

        public void ReqValueSumBtn_Click(object sender, EventArgs e)
        {

        }

        public void ReqValueSumForClientIdBtn_Click(object sender, EventArgs e)
        {

        }

        public void AllReqsListBtn_Click(object sender, EventArgs e)
        {

        }

        public void ReqsListForClientIdBtn_Click(object sender, EventArgs e)
        {

        }

        public void AverageReqValueBtn_Click(object sender, EventArgs e)
        {

        }

        public void AverageReqValueForClientIdBtn_Click(object sender, EventArgs e)
        {

        }

        public void ReqQuantByNameBtn_Click(object sender, EventArgs e)
        {

        }

        public void ReqQuantByNameForClientIdBtn_Click(object sender, EventArgs e)
        {

        }

        private void ReqsForValueRangeBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
