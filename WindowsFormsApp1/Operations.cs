using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqRaportsApp
{
    public partial class Form1
    {
        public void RaportMessageBox(string messageText, string messageLabel)
        {
            DialogResult res = MessageBox.Show(messageText + "\nCzy chcesz zapisać raport?", messageLabel, MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Zapisano raport");
            }
        }

        public double maxPrice()
        {
            Dictionary<string, List<long>> allReqs = AllRequests();

            List<double> reqValuesList = new List<double>();

            foreach(string cid in allReqs.Keys)
            {
                foreach(long rid in allReqs[cid])
                {
                    IEnumerable<request> getCurrentRequest = from req in RequestsList
                                                             where req.clientId == cid & req.requestId == rid
                                                             select req;

                    double reqValue = 0;

                    foreach (request r in getCurrentRequest)
                    {
                        reqValue += r.quantity * r.price;
                    }
                    reqValuesList.Add(reqValue);
                }
            }
            double maxPr = reqValuesList.Max();
            return maxPr;
        }

        public HashSet<long> AllReqsForClient(string currentClientId)
        {
            IEnumerable<long> getClientReqs = from request in RequestsList
                                             where request.clientId == currentClientId
                                             select request.requestId;

            HashSet<long> reqIdsForClient = new HashSet<long>(getClientReqs.ToList());

            return reqIdsForClient;
        }

        public Dictionary<string, List<long>> AllRequests()
        {
            IEnumerable<string> getClientIds = from request in RequestsList
                                               select request.clientId;

            HashSet<string> clientIds = getClientIds.ToHashSet();

            Dictionary<string, List<long>> allReqsDict = new Dictionary<string, List<long>>();
            foreach (string cid in clientIds)
            {
                HashSet<long> reqIds = AllReqsForClient(cid);

                allReqsDict[cid] = reqIds.ToList();
            }

            return allReqsDict;
        }

        public double RequestsValuesSum(List<request> CurrentRequestsList)
        {
            double allReqsValueSum = 0;
            foreach (request r in CurrentRequestsList)
            {
                int quant = r.quantity;
                double price = r.price;
                double val = quant * price;

                allReqsValueSum += val;
            }

            return allReqsValueSum;
        }

        public double ClientsValuesSum(string currentClientId)
        {
            var getClientReqs = from request in RequestsList
                                where request.clientId == currentClientId
                                select request;
            List<request> clientReqs = getClientReqs.ToList();
            double clientReqsValueSum = RequestsValuesSum(clientReqs);

            return clientReqsValueSum;
        }

        public string ProductReqIds(List<request> currentRequestsList)
        {
            IEnumerable<string> getProdNames = from request in currentRequestsList
                                               select request.name;

            HashSet<string> prodNames = new HashSet<string>(getProdNames.ToList());

            Dictionary<string, int> prodReqIdsDict = new Dictionary<string, int>();

            foreach (string pn in prodNames)
            {
                IEnumerable<request> getReqIdsForProd = from request in currentRequestsList
                                                        where request.name == pn
                                                        select request;

                prodReqIdsDict[pn] = getReqIdsForProd.Count();
            }

            string reqsListString = string.Empty;

            foreach (string k in prodReqIdsDict.Keys)
            {
                reqsListString += k + ": " + prodReqIdsDict[k].ToString() + "\n";
            }

            return reqsListString;
        }

        public HashSet<string> getClientIds()
        {
            IEnumerable<string> getClientIdsQuery = from request in RequestsList
                                                    select request.clientId;

            HashSet<string> clientIds = new HashSet<string>(getClientIdsQuery.ToList());

            return clientIds;
        }

        public void showClientIdsComboBox()
        {
            clientIdBox.Visible = true;
            clientIdComboBox.Visible = true;
            clientIdLabel.Visible = true;

            HashSet<string> clientIds = getClientIds();

            foreach (string id in clientIds) if (!clientIdComboBox.Items.Contains(id))
            {
                clientIdComboBox.Items.Add(id);
            }

            if (clientIdComboBox.SelectedItem == null & clientIdComboBox.Items.Count != 0)
            {
                clientIdComboBox.SelectedItem = clientIdComboBox.Items[0];
            }
        }

        public void ReqQuant()
        {
            Dictionary<string, List<long>> allReqDict = AllRequests();
            int allReqsCount = 0;
            foreach (string k in allReqDict.Keys)
            {
                allReqsCount += allReqDict[k].Count;
            }

            RaportMessageBox("Ilość zamówień: " + allReqsCount.ToString(), "Ilość zamówień");
        }

        public void ReqQuantForClient()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                HashSet<long> reqIdsForClient = AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                RaportMessageBox("Ilość zamówień dla klienta o identyfikatorze " + currentClientId + ": " + clientReqsCount.ToString(), "Ilość zamówień dla klienta");
            }
        }

        public void ReqValueSum()
        {
            double allReqsValueSum = RequestsValuesSum(RequestsList);

            RaportMessageBox("Łączna kwota zamówień: " + allReqsValueSum.ToString(), "Łączna kwota zamówień");
        }

        public void ReqValueSumForClientId()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                double clientReqsValueSum = ClientsValuesSum(currentClientId);

                RaportMessageBox("Łączna kwota zamówień dla klienta o identyfikatorze " + currentClientId + ": " + clientReqsValueSum.ToString(), "Łączna kwota zamówień dla klienta");
            }
        }

        public void AllReqsList()
        {
            Dictionary<string, List<long>> allReqDict = AllRequests();
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

        public void ReqsListForClientId()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                HashSet<long> clientsReqs = AllReqsForClient(currentClientId);

                string reqsListString = currentClientId + ":\n";
                foreach (int rid in clientsReqs)
                {
                    reqsListString += "    - " + rid.ToString() + "\n";
                }

                RaportMessageBox(reqsListString, "Lista zamówień dla klienta");
            }
        }

        public void AverageReqValue()
        {
            double allReqsValueSum = RequestsValuesSum(RequestsList);
            Dictionary<string, List<long>> allReqDict = AllRequests();

            int allReqsCount = 0;
            foreach (string k in allReqDict.Keys)
            {
                allReqsCount += allReqDict[k].Count;
            }

            double avgReqValue = allReqsValueSum / allReqsCount;
            double roundedAvgReqValue = Math.Round(avgReqValue, 2);

            RaportMessageBox("Średnia wartość zamówienia: " + roundedAvgReqValue.ToString(), "Średnia wartość zamówienia");
        }

        public void AverageReqValueForClientId()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                double clientReqsValueSum = ClientsValuesSum(currentClientId);

                HashSet<long> reqIdsForClient = AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                double avgReqValue = clientReqsValueSum / clientReqsCount;
                double roundedAvgReqValue = Math.Round(avgReqValue, 2);

                RaportMessageBox("Średnia wartość zamówienia dla klienta " + currentClientId + ": " + roundedAvgReqValue.ToString(), "Średnia wartość zamówienia dla klienta");
            }
        }

        public void ReqQuantByName()
        {
            string reqsListString = ProductReqIds(RequestsList);

            RaportMessageBox(reqsListString, "Ilość zamówień pogrupowanych po nazwie");
        }

        public void ReqQuantByNameForClientId()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                IEnumerable<request> getClientsReqs = from request in RequestsList
                                                      where request.clientId == currentClientId
                                                      select request;

                List<request> currentClientRequests = getClientsReqs.ToList();

                string reqsListString = ProductReqIds(currentClientRequests);

                RaportMessageBox(reqsListString, "Ilość zamówień pogrupowanych po nazwie");
            }
        }

        public void ReqsForValueRange()
        {
            try
            {
                double startValue = Double.Parse(minValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double endValue = Double.Parse(maxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                Dictionary<string, List<long>> allReqDict = AllRequests();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
