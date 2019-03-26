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

            RaportMessageBox("Ilość zamówień: " + allReqsCount.ToString(), "Ilość zamówień");
        }

        public void ReqQuantForClientIdBtn_Click(object sender, EventArgs e)
        {
            string currentClientId = "1";
            HashSet<int> reqIdsForClient = AllReqsForClient(currentClientId);
            int clientReqsCount = reqIdsForClient.Count();

            RaportMessageBox("Ilość zamówień dla klienta o identyfikatorze " + currentClientId + ": " + clientReqsCount.ToString(), "Ilość zamówień dla klienta");
        }

        public void ReqValueSumBtn_Click(object sender, EventArgs e)
        {
            double allReqsValueSum = RequestsValuesSum(RequestsList);

            RaportMessageBox("Łączna kwota zamówień: " + allReqsValueSum.ToString(), "Łączna kwota zamówień");
        }

        public void ReqValueSumForClientIdBtn_Click(object sender, EventArgs e)
        {
            string currentClientId = "1";
            double clientReqsValueSum = ClientsValuesSum(currentClientId);

            RaportMessageBox("Łączna kwota zamówień dla klienta o identyfikatorze " + currentClientId + ": " + clientReqsValueSum.ToString(), "Łączna kwota zamówień dla klienta");
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

            RaportMessageBox(reqsListString, "Lista zamówień");
        }

        public void ReqsListForClientIdBtn_Click(object sender, EventArgs e)
        {
            string currentClientId = "1";
            HashSet<int> clientsReqs = AllReqsForClient(currentClientId);

            string reqsListString = currentClientId + ":\n";
            foreach (int rid in clientsReqs)
            {
                reqsListString += "    - " + rid.ToString() + "\n";
            }

            RaportMessageBox(reqsListString, "Lista zamówień dla klienta");
        }

        public void AverageReqValueBtn_Click(object sender, EventArgs e)
        {
            double allReqsValueSum = RequestsValuesSum(RequestsList);
            Dictionary<string, List<int>> allReqDict = AllRequests();

            int allReqsCount = 0;
            foreach (string k in allReqDict.Keys)
            {
                allReqsCount += allReqDict[k].Count;
            }

            double avgReqValue = allReqsValueSum / allReqsCount;
            double roundedAvgReqValue = Math.Round(avgReqValue, 2);

            RaportMessageBox("Średnia wartość zamówienia: " + roundedAvgReqValue.ToString(), "Średnia wartość zamówienia");
        }

        public void AverageReqValueForClientIdBtn_Click(object sender, EventArgs e)
        {
            string currentClientId = "1";
            double clientReqsValueSum = ClientsValuesSum(currentClientId);

            HashSet<int> reqIdsForClient = AllReqsForClient(currentClientId);
            int clientReqsCount = reqIdsForClient.Count();

            double avgReqValue = clientReqsValueSum / clientReqsCount;
            double roundedAvgReqValue = Math.Round(avgReqValue, 2);

            RaportMessageBox("Średnia wartość zamówienia dla klienta " + currentClientId + ": " + roundedAvgReqValue.ToString(), "Średnia wartość zamówienia dla klienta");
        }

        public void ReqQuantByNameBtn_Click(object sender, EventArgs e)
        {
            string reqsListString = ProductReqIds(RequestsList);

            RaportMessageBox(reqsListString, "Ilość zamówień pogrupowanych po nazwie");
        }

        public void ReqQuantByNameForClientIdBtn_Click(object sender, EventArgs e)
        {
            string currentClientId = "1";
            IEnumerable<request> getClientsReqs = from request in RequestsList
                                                  where request.clientId == currentClientId
                                                  select request;

            List<request> currentClientRequests = getClientsReqs.ToList();

            string reqsListString = ProductReqIds(currentClientRequests);

            RaportMessageBox(reqsListString, "Ilość zamówień pogrupowanych po nazwie");
        }

        private void ReqsForValueRangeBtn_Click(object sender, EventArgs e)
        {
            double startValue = 0;
            double endValue = 120.00;

            Dictionary<string, List<int>> allReqDict = AllRequests();
            List<WholeReq> AllReqsWithValues = new List<WholeReq>();
            foreach (string cid in allReqDict.Keys)
            {
                foreach (int rid in allReqDict[cid])
                {
                    var getReq = from req in RequestsList
                                 where req.clientId == cid & req.requestId == rid
                                 select req;

                    WholeReq curReq = new WholeReq();
                    curReq.clientId = cid;
                    curReq.requestId = rid;
                    double val = 0;
                    foreach (var r in getReq)
                    {
                        val += r.quantity * r.price;
                    }
                    curReq.value = val;
                    AllReqsWithValues.Add(curReq);
                }
            }

            IEnumerable<WholeReq> getReqsInRange = from wr in AllReqsWithValues
                                                   where startValue < wr.value & wr.value < endValue
                                                   select wr;

            string reqValues = string.Empty;
            foreach (WholeReq w in getReqsInRange)
            {
                reqValues += "Client " + w.clientId + ": request " + w.requestId + ": " + w.value + "\n";
            }

            RaportMessageBox(reqValues, "Zamówienia w podanym przedziale cenowym");
        }
    }
}
