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
using System.Xml.Serialization;

namespace ReqRaportsApp
{
    public partial class Form1 : Form
    {
        List<request> RequestsList = new List<request>();

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

                string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                this.addedFilesListView.Items.Add(fileName);

                if (filePath.EndsWith(".xml"))
                {
                    MyXmlSerializer.DeserializeXmlObject(filePath, RequestsList);
                }
                else if (filePath.EndsWith(".json"))
                {
                    MyJsonSerializer.DesarializeJsonObject(filePath, RequestsList);
                }
                else if (filePath.EndsWith(".csv"))
                {
                    MyCsvSerializer.DeserializeCsvObject(filePath, RequestsList);
                }
            }
        }

        public void ReqQuantBtn_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Ilość zamówień: " + RequestsList.Count.ToString() + "\nCzy chcesz zapisać raport?", "Ilość zamówień", MessageBoxButtons.OKCancel);

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
