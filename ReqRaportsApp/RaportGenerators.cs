using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public class RaportGenerators
    {
        private static void ReqQuant(DataGridView raportsDataGrid)
        {
            Dictionary<string, List<long>> allReqDict = RapGenOperations.AllRequests();
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
            GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
        }

        private static void ReqQuantForClient(DataGridView raportsDataGrid, ComboBox clientIdComboBox)
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                HashSet<long> reqIdsForClient = RapGenOperations.AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                string[] colNames = { "Identyfikator klienta", "Ilość zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(clientReqsCount);

                rows.Add(row);
                GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
            }
        }

        private static void ReqValueSum(DataGridView raportsDataGrid)
        {
            double allReqsValueSum = RapGenOperations.RequestsValuesSum(RequestList.ReqsList);

            string[] colNames = { "Łączna kwota zamówień" };
            List<List<object>> rows = new List<List<object>>();
            List<object> row = new List<object>();
            row.Add(Math.Round(allReqsValueSum, 2));

            rows.Add(row);
            GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
        }

        private static void ReqValueSumForClientId(DataGridView raportsDataGrid, ComboBox clientIdComboBox)
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                double clientReqsValueSum = RapGenOperations.ClientsValuesSum(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Łączna kwota zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(Math.Round(clientReqsValueSum, 2));

                rows.Add(row);
                GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
            }
        }

        private static void AllReqsList(DataGridView raportsDataGrid)
        {
            Dictionary<string, List<long>> allReqDict = RapGenOperations.AllRequests();
            string reqsListString = string.Empty;

            string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Nazwa produktu", "Ilość", "Cena produktu" };
            List<List<object>> rows = new List<List<object>>();

            foreach (string cid in allReqDict.Keys)
            {
                foreach (long rid in allReqDict[cid])
                {
                    IEnumerable<request> getAllInstancesOfRequest = from req in RequestList.ReqsList
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
            GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
        }

        private static void ReqsListForClientId(DataGridView raportsDataGrid, ComboBox clientIdComboBox)
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                HashSet<long> clientsReqs = RapGenOperations.AllReqsForClient(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Nazwa produktu", "Ilość", "Cena produktu" };
                List<List<object>> rows = new List<List<object>>();

                foreach (int rid in clientsReqs)
                {
                    IEnumerable<request> getAllInstancesOfRequest = from req in RequestList.ReqsList
                                                                    where req.clientId == currentClientId & req.requestId == rid
                                                                    select req;
                    foreach (request r in getAllInstancesOfRequest)
                    {
                        List<object> row = new List<object>();
                        row.Add(currentClientId);
                        row.Add(rid);
                        row.Add(r.name);
                        row.Add(r.quantity);
                        row.Add(Math.Round(r.price, 2));

                        rows.Add(row);
                    }
                }
                GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
            }
        }

        private static void AverageReqValue(DataGridView raportsDataGrid)
        {
            double allReqsValueSum = RapGenOperations.RequestsValuesSum(RequestList.ReqsList);
            Dictionary<string, List<long>> allReqDict = RapGenOperations.AllRequests();

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
            GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
        }

        private static void AverageReqValueForClientId(DataGridView raportsDataGrid, ComboBox clientIdComboBox)
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                double clientReqsValueSum = RapGenOperations.ClientsValuesSum(currentClientId);

                HashSet<long> reqIdsForClient = RapGenOperations.AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                double avgReqValue = clientReqsValueSum / clientReqsCount;
                double roundedAvgReqValue = Math.Round(avgReqValue, 2);

                string[] colNames = { "Identyfikator klienta", "Średnia wartość zamówienia" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(roundedAvgReqValue);

                rows.Add(row);
                GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
            }
        }

        private static void ReqQuantByName(DataGridView raportsDataGrid)
        {
            Dictionary<string, int> reqsListString = RapGenOperations.ProductReqIds(RequestList.ReqsList);

            string[] colNames = { "Nazwa produktu", "Ilość zamówień" };
            List<List<object>> rows = new List<List<object>>();
            foreach (string prodName in reqsListString.Keys)
            {
                List<object> row = new List<object>();
                row.Add(prodName);
                row.Add(reqsListString[prodName]);

                rows.Add(row);
            }
            GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
        }

        private static void ReqQuantByNameForClientId(DataGridView raportsDataGrid, ComboBox clientIdComboBox)
        {
            if (clientIdComboBox.SelectedItem != null)
            {
                string currentClientId = clientIdComboBox.SelectedItem.ToString();

                IEnumerable<request> getClientsReqs = from request in RequestList.ReqsList
                                                      where request.clientId == currentClientId
                                                      select request;

                List<request> currentClientRequests = getClientsReqs.ToList();

                Dictionary<string, int> reqsListString = RapGenOperations.ProductReqIds(currentClientRequests);

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
                GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
            }
        }

        private static void ReqsForValueRange(DataGridView raportsDataGrid, TextBox minValueTextBox, TextBox maxValueTextBox)
        {
            try
            {
                double minValue = Double.Parse(minValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double maxValue = Double.Parse(maxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                List<RequestWithSummaricValue> requestWithSummaricValueList = new List<RequestWithSummaricValue>();

                Dictionary<string, Dictionary<long, double>> allReqValsDict = RapGenOperations.wholeReqValueDict();
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
                    row.Add(Math.Round(r.value, 2));

                    rows.Add(row);
                }
                GridViewHandler.GridViewPopulate(colNames, rows, raportsDataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void RaportChoiceSwitch(ComboBox raportsComboBox, DataGridView raportsDataGrid, ComboBox clientIdComboBox, TextBox minValueTextBox, TextBox maxValueTextBox)
        {
            string raportType = raportsComboBox.SelectedItem.ToString();

            switch (raportType)
            {
                case RaportTypes.ReqQuantType:
                    ReqQuant(raportsDataGrid);
                    break;
                case RaportTypes.ReqQuantForClientType:
                    ReqQuantForClient(raportsDataGrid, clientIdComboBox);
                    break;
                case RaportTypes.ReqValueSumType:
                    ReqValueSum(raportsDataGrid);
                    break;
                case RaportTypes.ReqValueSumForClientType:
                    ReqValueSumForClientId(raportsDataGrid, clientIdComboBox);
                    break;
                case RaportTypes.AllReqsListType:
                    AllReqsList(raportsDataGrid);
                    break;
                case RaportTypes.AllReqsListForClientType:
                    ReqsListForClientId(raportsDataGrid, clientIdComboBox);
                    break;
                case RaportTypes.AverageReqValueType:
                    AverageReqValue(raportsDataGrid);
                    break;
                case RaportTypes.AverageReqValueForClientType:
                    AverageReqValueForClientId(raportsDataGrid, clientIdComboBox);
                    break;
                case RaportTypes.ReqQuantByProdNameType:
                    ReqQuantByName(raportsDataGrid);
                    break;
                case RaportTypes.ReqQuantByProdNameForClientType:
                    ReqQuantByNameForClientId(raportsDataGrid, clientIdComboBox);
                    break;
                case RaportTypes.ReqsInValueRangeType:
                    ReqsForValueRange(raportsDataGrid, minValueTextBox, maxValueTextBox);
                    break;
            }
        }
    }
}
