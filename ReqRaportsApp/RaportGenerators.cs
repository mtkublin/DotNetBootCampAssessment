﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public class RaportGenerators
    {
        List<request> ReqsList { get; set; }
        ComboBox RaportsComboBox { get; set; }
        ComboBox ClientIdComboBox { get; set; }
        TextBox MinValueTextBox { get; set; }
        TextBox MaxValueTextBox { get; set; }
        DataGridView RaportsDataGrid { get; set; }

        GridViewHandler GridViewHand { get; set; }
        RapGenOperations RapGenOps { get; set; }

        public RaportGenerators(GridViewHandler gvHand, RapGenOperations rgOps, List<request> rList, ComboBox rCombo, ComboBox cidCombo, TextBox minvTextBox, TextBox maxvTextBox, DataGridView rDataGrid)
        {
            ReqsList = rList;
            RaportsComboBox = rCombo;
            ClientIdComboBox = cidCombo;
            MinValueTextBox = minvTextBox;
            MaxValueTextBox = maxvTextBox;
            RaportsDataGrid = rDataGrid;

            GridViewHand = gvHand;
            RapGenOps = rgOps;
        }

        private GridViewData ReqQuant()
        {
            Dictionary<string, List<long>> allReqDict = RapGenOps.AllRequests();
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

            return new GridViewData(colNames, rows);
        }

        private GridViewData ReqQuantForClient()
        {
            try
            {
                string currentClientId = ClientIdComboBox.SelectedItem.ToString();

                HashSet<long> reqIdsForClient = RapGenOps.AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                string[] colNames = { "Identyfikator klienta", "Ilość zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(clientReqsCount);

                rows.Add(row);
                return new GridViewData(colNames, rows);
            }
            catch
            {
                MessageBox.Show("Nie wybrano identyfikatora klienta");

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                return new GridViewData(cN, rW);
            }
        }

        private GridViewData ReqValueSum()
        {
            double allReqsValueSum = RapGenOps.RequestsValuesSum(ReqsList);

            string[] colNames = { "Łączna kwota zamówień" };
            List<List<object>> rows = new List<List<object>>();
            List<object> row = new List<object>();
            row.Add(Math.Round(allReqsValueSum, 2));

            rows.Add(row);
            return new GridViewData(colNames, rows);
        }

        private GridViewData ReqValueSumForClientId()
        {
            try
            {
                string currentClientId = ClientIdComboBox.SelectedItem.ToString();

                double clientReqsValueSum = RapGenOps.ClientsValuesSum(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Łączna kwota zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(Math.Round(clientReqsValueSum, 2));

                rows.Add(row);
                return new GridViewData(colNames, rows);
            }
            catch
            {
                MessageBox.Show("Nie wybrano identyfikatora klienta");

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                return new GridViewData(cN, rW);
            }
        }

        private GridViewData AllReqsList()
        {
            Dictionary<string, List<long>> allReqDict = RapGenOps.AllRequests();
            string reqsListString = string.Empty;

            string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Nazwa produktu", "Ilość", "Cena produktu" };
            List<List<object>> rows = new List<List<object>>();

            foreach (string cid in allReqDict.Keys)
            {
                foreach (long rid in allReqDict[cid])
                {
                    IEnumerable<request> getAllInstancesOfRequest = from req in ReqsList
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
            return new GridViewData(colNames, rows);
        }

        private GridViewData ReqsListForClientId()
        {
            try
            {
                string currentClientId = ClientIdComboBox.SelectedItem.ToString();

                HashSet<long> clientsReqs = RapGenOps.AllReqsForClient(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Nazwa produktu", "Ilość", "Cena produktu" };
                List<List<object>> rows = new List<List<object>>();

                foreach (int rid in clientsReqs)
                {
                    IEnumerable<request> getAllInstancesOfRequest = from req in ReqsList
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
                return new GridViewData(colNames, rows);
            }
            catch
            {
                MessageBox.Show("Nie wybrano identyfikatora klienta");

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                return new GridViewData(cN, rW);
            }
        }

        private GridViewData AverageReqValue()
        {
            double allReqsValueSum = RapGenOps.RequestsValuesSum(ReqsList);
            Dictionary<string, List<long>> allReqDict = RapGenOps.AllRequests();

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
            return new GridViewData(colNames, rows);
        }

        private GridViewData AverageReqValueForClientId()
        {
            try
            {
                string currentClientId = ClientIdComboBox.SelectedItem.ToString();

                double clientReqsValueSum = RapGenOps.ClientsValuesSum(currentClientId);

                HashSet<long> reqIdsForClient = RapGenOps.AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                double avgReqValue = clientReqsValueSum / clientReqsCount;
                double roundedAvgReqValue = Math.Round(avgReqValue, 2);

                string[] colNames = { "Identyfikator klienta", "Średnia wartość zamówienia" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>();
                row.Add(currentClientId);
                row.Add(roundedAvgReqValue);

                rows.Add(row);
                return new GridViewData(colNames, rows);
            }
            catch
            {
                MessageBox.Show("Nie wybrano identyfikatora klienta");

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                return new GridViewData(cN, rW);
            }
        }

        private GridViewData ReqQuantByName()
        {
            Dictionary<string, ProductObject> reqsListString = RapGenOps.ProductReqIds(ReqsList);

            string[] colNames = { "Nazwa produktu", "Ilość zamówień", "Ilość produktu we wszystkich zamówienia", "Sumaryczna kwota produktu" };
            List<List<object>> rows = new List<List<object>>();
            foreach (string prodName in reqsListString.Keys)
            {
                List<object> row = new List<object>();
                row.Add(prodName);
                row.Add(reqsListString[prodName].requestQuantity);
                row.Add(reqsListString[prodName].productQuantity);
                row.Add(reqsListString[prodName].productValue);

                rows.Add(row);
            }
            return new GridViewData(colNames, rows);
        }

        private GridViewData ReqQuantByNameForClientId()
        {
            try
            {
                string currentClientId = ClientIdComboBox.SelectedItem.ToString();

                IEnumerable<request> getClientsReqs = from request in ReqsList
                                                      where request.clientId == currentClientId
                                                      select request;

                List<request> currentClientRequests = getClientsReqs.ToList();

                Dictionary<string, ProductObject> reqsListString = RapGenOps.ProductReqIds(currentClientRequests);

                string[] colNames = { "Identyfikator klienta", "Nazwa produktu", "Ilość zamówień", "Ilość produktu we wszystkich zamówienia", "Sumaryczna kwota produktu" };
                List<List<object>> rows = new List<List<object>>();
                foreach (string prodName in reqsListString.Keys)
                {
                    List<object> row = new List<object>();
                    row.Add(currentClientId);
                    row.Add(prodName);
                    row.Add(reqsListString[prodName].requestQuantity);
                    row.Add(reqsListString[prodName].productQuantity);
                    row.Add(reqsListString[prodName].productValue);

                    rows.Add(row);
                }
                return new GridViewData(colNames, rows);
            }
            catch
            {
                MessageBox.Show("Nie wybrano identyfikatora klienta");

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                return new GridViewData(cN, rW);
            }
        }

        private GridViewData ReqsForValueRange()
        {
            try
            {
                double minValue = Double.Parse(MinValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double maxValue = Double.Parse(MaxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                List<RequestWithSummaricValue> requestWithSummaricValueList = new List<RequestWithSummaricValue>();

                Dictionary<string, Dictionary<long, double>> allReqValsDict = RapGenOps.wholeReqValueDict();
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
                return new GridViewData(colNames, rows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                return new GridViewData(cN, rW);
            }
        }

        public GridViewData RaportChoiceSwitch()
        {
            string raportType = RaportsComboBox.SelectedItem.ToString();

            string[] cN = { };
            List<List<object>> rW = new List<List<object>>();
            GridViewData gridViewData = new GridViewData(cN, rW);

            switch (raportType)
            {
                case RaportTypes.ReqQuantType:
                    gridViewData = ReqQuant();
                    break;
                case RaportTypes.ReqQuantForClientType:
                    gridViewData = ReqQuantForClient();
                    break;
                case RaportTypes.ReqValueSumType:
                    gridViewData = ReqValueSum();
                    break;
                case RaportTypes.ReqValueSumForClientType:
                    gridViewData = ReqValueSumForClientId();
                    break;
                case RaportTypes.AllReqsListType:
                    gridViewData = AllReqsList();
                    break;
                case RaportTypes.AllReqsListForClientType:
                    gridViewData = ReqsListForClientId();
                    break;
                case RaportTypes.AverageReqValueType:
                    gridViewData = AverageReqValue();
                    break;
                case RaportTypes.AverageReqValueForClientType:
                    gridViewData = AverageReqValueForClientId();
                    break;
                case RaportTypes.ReqQuantByProdNameType:
                    gridViewData = ReqQuantByName();
                    break;
                case RaportTypes.ReqQuantByProdNameForClientType:
                    gridViewData = ReqQuantByNameForClientId();
                    break;
                case RaportTypes.ReqsInValueRangeType:
                    gridViewData = ReqsForValueRange();
                    break;
            }

            return gridViewData;
        }
    }
}
