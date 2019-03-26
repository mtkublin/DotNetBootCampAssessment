using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        public Dictionary<string, Dictionary<long, double>> wholeReqValueDict()
        {
            Dictionary<string, List<long>> allReqs = AllRequests();

            Dictionary<string, Dictionary<long, double>> clientReqWholeValDict = new Dictionary<string, Dictionary<long, double>>();
            foreach (string cid in allReqs.Keys)
            {
                Dictionary<long, double> reqValuesDict = new Dictionary<long, double>();
                foreach (long rid in allReqs[cid])
                {
                    IEnumerable<request> getCurrentRequest = from req in RequestsList
                                                             where req.clientId == cid & req.requestId == rid
                                                             select req;

                    double reqValue = 0;

                    foreach (request r in getCurrentRequest)
                    {
                        reqValue += r.quantity * r.price;
                    }
                    reqValuesDict[rid] = reqValue;
                }
                clientReqWholeValDict[cid] = reqValuesDict;
            }
            return clientReqWholeValDict;
        }

        public double maxPrice()
        {
            List<double> reqValuesList = new List<double>();

            Dictionary<string, Dictionary<long, double>> reqValsDict = wholeReqValueDict();
            foreach (string cid in reqValsDict.Keys)
            {
                foreach (long r in reqValsDict[cid].Keys)
                {
                    reqValuesList.Add(reqValsDict[cid][r]);
                }
            }

            double maxPr = reqValuesList.Max();
            maxPr = Math.Round(maxPr, 2);
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

        public Dictionary<string, int> ProductReqIds(List<request> currentRequestsList)
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

            return prodReqIdsDict;
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

            string[] colNames = { "Ilość zamówień" };
            List<List<object>> rows = new List<List<object>>();
            List<object> row = new List<object>();
            row.Add(allReqsCount);

            rows.Add(row);
            GridViewPopulate(colNames, rows);
        }

        public void ReqQuantForClient()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                HashSet<long> reqIdsForClient = AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                string[] colNames = { "Identyfikator klienta", "Ilość zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(clientReqsCount);

                rows.Add(row);
                GridViewPopulate(colNames, rows);
            }
        }

        public void ReqValueSum()
        {
            double allReqsValueSum = RequestsValuesSum(RequestsList);

            string[] colNames = { "Łączna kwota zamówień" };
            List<List<object>> rows = new List<List<object>>();
            List<object> row = new List<object>();
            row.Add(Math.Round(allReqsValueSum, 2));

            rows.Add(row);
            GridViewPopulate(colNames, rows);
        }

        public void ReqValueSumForClientId()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                double clientReqsValueSum = ClientsValuesSum(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Łączna kwota zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(Math.Round(clientReqsValueSum, 2));

                rows.Add(row);
                GridViewPopulate(colNames, rows);
            }
        }

        public void AllReqsList()
        {
            Dictionary<string, List<long>> allReqDict = AllRequests();
            string reqsListString = string.Empty;

            string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Nazwa produktu", "Ilość", "Cena produktu" };
            List<List<object>> rows = new List<List<object>>();

            foreach (string cid in allReqDict.Keys)
            {
                foreach (long rid in allReqDict[cid])
                {
                    IEnumerable<request> getAllInstancesOfRequest = from req in RequestsList
                                                                    where req.clientId == cid & req.requestId == rid
                                                                    select req;
                    foreach (request r in getAllInstancesOfRequest)
                    {
                        List<object> row = new List<object>();
                        row.Add(cid);
                        row.Add(rid);
                        row.Add(r.name);
                        row.Add(r.quantity);
                        row.Add(Math.Round(r.price, 2));

                        rows.Add(row);
                    }
                }
            }
            GridViewPopulate(colNames, rows);
        }

        public void ReqsListForClientId()
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                HashSet<long> clientsReqs = AllReqsForClient(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Nazwa produktu", "Ilość", "Cena produktu" };
                List<List<object>> rows = new List<List<object>>();

                foreach (int rid in clientsReqs)
                {
                    IEnumerable<request> getAllInstancesOfRequest = from req in RequestsList
                                                                    where req.clientId == currentClientId & req.requestId == rid
                                                                    select req;
                    foreach (request r in getAllInstancesOfRequest)
                    {
                        List<object> row = new List<object>();
                        row.Add(currentClientId);
                        row.Add(rid);
                        row.Add(r.name);
                        row.Add(r.quantity);
                        row.Add(Math.Round(r.price,2));

                        rows.Add(row);
                    }
                }
                GridViewPopulate(colNames, rows);
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

            string[] colNames = { "Średnia wartość zamówienia" };
            List<List<object>> rows = new List<List<object>>();
            List<object> row = new List<object>();
            row.Add(roundedAvgReqValue);

            rows.Add(row);
            GridViewPopulate(colNames, rows);
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

                string[] colNames = { "Identyfikator klienta", "Średnia wartość zamówienia" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(roundedAvgReqValue);

                rows.Add(row);
                GridViewPopulate(colNames, rows);
            }
        }

        public void ReqQuantByName()
        {
            Dictionary<string, int> reqsListString = ProductReqIds(RequestsList);

            string[] colNames = { "Nazwa produktu", "Ilość zamówień" };
            List<List<object>> rows = new List<List<object>>();
            foreach (string prodName in reqsListString.Keys)
            {
                List<object> row = new List<object>();
                row.Add(prodName);
                row.Add(reqsListString[prodName]);

                rows.Add(row);
            }
            GridViewPopulate(colNames, rows);
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

                Dictionary<string, int> reqsListString = ProductReqIds(currentClientRequests);

                string[] colNames = { "Identyfikator klienta", "Nazwa produktu", "Ilość zamówień" };
                List<List<object>> rows = new List<List<object>>();
                foreach (string prodName in reqsListString.Keys)
                {
                    List<object> row = new List<object>();
                    row.Add(currentClientId);
                    row.Add(prodName);
                    row.Add(reqsListString[prodName]);

                    rows.Add(row);
                }
                GridViewPopulate(colNames, rows);
            }
        }

        public void ReqsForValueRange()
        {
            try
            {
                double minValue = Double.Parse(minValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double maxValue = Double.Parse(maxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                List<RequestWithSummaricValue> requestWithSummaricValueList = new List<RequestWithSummaricValue>();

                Dictionary<string, Dictionary<long, double>> allReqValsDict = wholeReqValueDict();
                foreach (string cid in allReqValsDict.Keys)
                {
                    foreach (long rid in allReqValsDict[cid].Keys)
                    {
                        RequestWithSummaricValue requestWithSummaricValue = new RequestWithSummaricValue(cid, rid, allReqValsDict[cid][rid]);
                        requestWithSummaricValueList.Add(requestWithSummaricValue);
                    }
                }

                IEnumerable<RequestWithSummaricValue> getReqValsInRange = from req in requestWithSummaricValueList
                                                                          where req.value >= minValue & req.value <= maxValue
                                                                          select req;

                string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Wartość zamówienia" };
                List<List<object>> rows = new List<List<object>>();
                foreach (RequestWithSummaricValue r in getReqValsInRange)
                {
                    List<object> row = new List<object>();
                    row.Add(r.clientId);
                    row.Add(r.requestId);
                    row.Add(Math.Round(r.value,2));

                    rows.Add(row);
                }
                GridViewPopulate(colNames, rows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RaportChoiceSwitch()
        {
            string raportType = this.raportsComboBox.SelectedItem.ToString();

            switch (raportType)
            {
                case "Ilość zamówień":
                    ReqQuant();
                    break;
                case "Ilość zamówień dla klienta o wskazanym identyfikatorze":
                    ReqQuantForClient();
                    break;
                case "Łączna kwota zamówień":
                    ReqValueSum();
                    break;
                case "Łączna kwota zamówień dla klienta o wskazanym identyfikatorze":
                    ReqValueSumForClientId();
                    break;
                case "Lista wszystkich zamówień":
                    AllReqsList();
                    break;
                case "Lista zamówień dla klienta o wskazanym identyfikatorze":
                    ReqsListForClientId();
                    break;
                case "Średnia wartość zamówienia":
                    AverageReqValue();
                    break;
                case "Średnia wartość zamówienia dla klienta o wskazanym identyfikatorze":
                    AverageReqValueForClientId();
                    break;
                case "Ilość zamówień pogrupowanych po nazwie":
                    ReqQuantByName();
                    break;
                case "Ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym identyfikatorze":
                    ReqQuantByNameForClientId();
                    break;
                case "Zamówienia w podanym przedziale cenowym":
                    ReqsForValueRange();
                    break;
            }
        }
    }
}
