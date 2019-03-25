using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
            Dictionary<string, List<int>> allReqDict = AllRequests();

            int allReqsCount = 0;
            foreach (string k in allReqDict.Keys)
            {
                allReqsCount += allReqDict[k].Count;
            }

            DialogResult res = MessageBox.Show("Ilość zamówień: " + allReqsCount.ToString() + "\nCzy chcesz zapisać raport?", "Ilość zamówień", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Zapisano raport");
            }
        }

        public void ReqQuantForClientIdBtn_Click(object sender, EventArgs e)
        {
            string currentClientId = "1";

            IEnumerable<int> getClientReqs = from request in RequestsList
                                where request.clientId == currentClientId
                                select request.requestId;

            HashSet<int> reqIdsForClient = new HashSet<int>(getClientReqs.ToList());

            int clientReqsCount = reqIdsForClient.Count();

            DialogResult res = MessageBox.Show("Ilość zamówień dla klienta o identyfikatorze " + currentClientId + ": " + clientReqsCount.ToString() + "\nCzy chcesz zapisać raport?", "Ilość zamówień dla klienta", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Zapisano raport");
            }
        }

        public void ReqValueSumBtn_Click(object sender, EventArgs e)
        {
            double allReqsValueSum = 0;

            foreach (request r in RequestsList)
            {
                int quant = r.quantity;
                double price = r.price;
                double val = quant * price;

                allReqsValueSum += val;
            }

            DialogResult res = MessageBox.Show("Łączna kwota zamówień: " + allReqsValueSum.ToString() + "\nCzy chcesz zapisać raport?", "Łączna kwota zamówień", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Zapisano raport");
            }
        }

        public void ReqValueSumForClientIdBtn_Click(object sender, EventArgs e)
        {
            string currentClientId = "1";

            var getClientReqs = from request in RequestsList
                                where request.clientId == currentClientId
                                select request;

            List<request> clientReqs = getClientReqs.ToList();

            double clientReqsValueSum = 0;

            foreach (request r in clientReqs)
            {
                int quant = r.quantity;
                double price = r.price;
                double val = quant * price;

                clientReqsValueSum += val;
            }

            DialogResult res = MessageBox.Show("Łączna kwota zamówień dla klienta o identyfikatorze " + currentClientId + ": " + clientReqsValueSum.ToString() + "\nCzy chcesz zapisać raport?", "Łączna kwota zamówień dla klienta", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Zapisano raport");
            }
        }

        public void AllReqsListBtn_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<int>> allReqDict = AllRequests();
            string reqsListString = string.Empty;

            foreach (string k in allReqDict.Keys)
            {
                reqsListString += k + ":\n";
                foreach (int rid in allReqDict[k])
                {
                    reqsListString += "    - " + rid.ToString() + "\n";
                }
            }

            DialogResult res = MessageBox.Show(reqsListString + "\nCzy chcesz zapisać raport?", "Łączna kwota zamówień dla klienta", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Zapisano raport");
            }
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
