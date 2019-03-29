using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public class RaportGenerators
    {
        List<request> ReqsList { get; set; }
        RapGenOperations RapGenOps { get; set; }

        public RaportGenerators(RapGenOperations rgOps, List<request> rList)
        {
            ReqsList = rList;
            RapGenOps = rgOps;
        }

        public GridViewData ReqQuant()
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

        public GridViewData ReqQuantForClient(string currentClientId)
        {
            try
            {
                HashSet<long> reqIdsForClient = RapGenOps.AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                string[] colNames = { "Identyfikator klienta", "Ilość zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>
                {
                    currentClientId,
                    clientReqsCount
                };
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

        public GridViewData ReqValueSum()
        {
            double allReqsValueSum = RapGenOps.RequestsValuesSum(ReqsList);

            string[] colNames = { "Łączna kwota zamówień" };
            List<List<object>> rows = new List<List<object>>();
            List<object> row = new List<object>
            {
                allReqsValueSum
            };
            rows.Add(row);

            return new GridViewData(colNames, rows);
        }

        public GridViewData ReqValueSumForClientId(string currentClientId)
        {
            try
            {
                double clientReqsValueSum = RapGenOps.ClientsValuesSum(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Łączna kwota zamówień" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>
                {
                    currentClientId,
                    clientReqsValueSum
                };

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

        public GridViewData AllReqsList()
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
                        List<object> row = new List<object>
                        {
                            cid,
                            rid,
                            r.name,
                            r.quantity,
                            Math.Round(r.price, 2)
                        };

                        rows.Add(row);
                    }
                }
            }
            return new GridViewData(colNames, rows);
        }

        public GridViewData ReqsListForClientId(string currentClientId)
        {
            try
            {
                HashSet<long> clientsReqs = RapGenOps.AllReqsForClient(currentClientId);

                string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Nazwa produktu", "Ilość", "Cena produktu" };
                List<List<object>> rows = new List<List<object>>();

                foreach (long rid in clientsReqs)
                {
                    IEnumerable<request> getAllInstancesOfRequest = from req in ReqsList
                                                                    where req.clientId == currentClientId & req.requestId == rid
                                                                    select req;
                    foreach (request r in getAllInstancesOfRequest)
                    {
                        List<object> row = new List<object>
                        {
                            currentClientId,
                            rid,
                            r.name,
                            r.quantity,
                            Math.Round(r.price, 2)
                        };

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

        public GridViewData AverageReqValue()
        {
            double allReqsValueSum = RapGenOps.RequestsValuesSum(ReqsList);
            Dictionary<string, List<long>> allReqDict = RapGenOps.AllRequests();

            int allReqsCount = 0;
            foreach (string k in allReqDict.Keys)
            {
                allReqsCount += allReqDict[k].Count;
            }

            double avgReqValue = allReqsValueSum / allReqsCount;
            double roundedAvgReqValue = Math.Round(avgReqValue, 2, MidpointRounding.AwayFromZero);

            string[] colNames = { "Średnia wartość zamówienia" };
            List<List<object>> rows = new List<List<object>>();
            List<object> row = new List<object>
            {
                roundedAvgReqValue
            };

            rows.Add(row);
            return new GridViewData(colNames, rows);
        }

        public GridViewData AverageReqValueForClientId(string currentClientId)
        {
            try
            {
                double clientReqsValueSum = RapGenOps.ClientsValuesSum(currentClientId);

                HashSet<long> reqIdsForClient = RapGenOps.AllReqsForClient(currentClientId);
                int clientReqsCount = reqIdsForClient.Count();

                double avgReqValue = clientReqsValueSum / clientReqsCount;
                double roundedAvgReqValue = Math.Round(avgReqValue, 2, MidpointRounding.AwayFromZero);

                string[] colNames = { "Identyfikator klienta", "Średnia wartość zamówienia" };
                List<List<object>> rows = new List<List<object>>();
                List<object> row = new List<object>
                {
                    currentClientId,
                    roundedAvgReqValue
                };

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

        public GridViewData ReqQuantByName()
        {
            Dictionary<string, ProductObject> reqsListString = RapGenOps.ProductReqIds(ReqsList);

            string[] colNames = { "Nazwa produktu", "Ilość zamówień", "Ilość produktu we wszystkich zamówienia", "Sumaryczna kwota produktu" };
            List<List<object>> rows = new List<List<object>>();
            foreach (string prodName in reqsListString.Keys)
            {
                List<object> row = new List<object>
                {
                    prodName,
                    reqsListString[prodName].requestQuantity,
                    reqsListString[prodName].productQuantity,
                    reqsListString[prodName].productValue
                };

                rows.Add(row);
            }
            return new GridViewData(colNames, rows);
        }

        public GridViewData ReqQuantByNameForClientId(string currentClientId)
        {
            try
            {
                IEnumerable<request> getClientsReqs = from request in ReqsList
                                                      where request.clientId == currentClientId
                                                      select request;

                List<request> currentClientRequests = getClientsReqs.ToList();

                Dictionary<string, ProductObject> reqsListString = RapGenOps.ProductReqIds(currentClientRequests);

                string[] colNames = { "Identyfikator klienta", "Nazwa produktu", "Ilość zamówień", "Ilość produktu we wszystkich zamówienia", "Sumaryczna kwota produktu" };
                List<List<object>> rows = new List<List<object>>();
                foreach (string prodName in reqsListString.Keys)
                {
                    List<object> row = new List<object>
                    {
                        currentClientId,
                        prodName,
                        reqsListString[prodName].requestQuantity,
                        reqsListString[prodName].productQuantity,
                        reqsListString[prodName].productValue
                    };

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

        public GridViewData ReqsForValueRange(double minValue, double maxValue)
        {
            try
            {
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
                                                                          where Math.Round(req.value, 2) >= minValue & Math.Round(req.value, 2) <= maxValue
                                                                          select req;

                string[] colNames = { "Identyfikator klienta", "Identyfikator zamówienia", "Wartość zamówienia" };
                List<List<object>> rows = new List<List<object>>();
                foreach (RequestWithSummaricValue r in getReqValsInRange)
                {
                    List<object> row = new List<object>
                    {
                        r.clientId,
                        r.requestId,
                        Math.Round(r.value, 2)
                    };

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
    }
}
