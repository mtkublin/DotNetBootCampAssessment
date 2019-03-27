using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqRaportsApp
{
    public class RapGenOperations
    {
        List<request> ReqsList { get; set; }

        public RapGenOperations(List<request> rList)
        {
            ReqsList = rList;
        }

        public HashSet<long> AllReqsForClient(string currentClientId)
        {
            IEnumerable<long> getClientReqs = from request in ReqsList
                                              where request.clientId == currentClientId
                                              select request.requestId;

            HashSet<long> reqIdsForClient = new HashSet<long>(getClientReqs.ToList());

            return reqIdsForClient;
        }

        public Dictionary<string, List<long>> AllRequests()
        {
            IEnumerable<string> getClientIds = from request in ReqsList
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

        public Dictionary<string, Dictionary<long, double>> wholeReqValueDict()
        {
            Dictionary<string, List<long>> allReqs = AllRequests();

            Dictionary<string, Dictionary<long, double>> clientReqWholeValDict = new Dictionary<string, Dictionary<long, double>>();
            foreach (string cid in allReqs.Keys)
            {
                Dictionary<long, double> reqValuesDict = new Dictionary<long, double>();
                foreach (long rid in allReqs[cid])
                {
                    IEnumerable<request> getCurrentRequest = from req in ReqsList
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
            var getClientReqs = from request in ReqsList
                                where request.clientId == currentClientId
                                select request;
            List<request> clientReqs = getClientReqs.ToList();
            double clientReqsValueSum = RequestsValuesSum(clientReqs);

            return clientReqsValueSum;
        }

        public Dictionary<string, ProductObject> ProductReqIds(List<request> currentRequestsList)
        {
            IEnumerable<string> getProdNames = from request in currentRequestsList
                                               select request.name;

            HashSet<string> prodNames = new HashSet<string>(getProdNames.ToList());

            Dictionary<string, ProductObject> prodReqIdsDict = new Dictionary<string, ProductObject>();

            foreach (string pn in prodNames)
            {
                IEnumerable<request> getReqIdsForProd = from request in currentRequestsList
                                                        where request.name == pn
                                                        select request;

                Dictionary<string, List<long>> workDict = new Dictionary<string, List<long>>();
                int reqCounter = 0;
                int prodQuant = 0;
                double prodPrice = 0;
                foreach (request r in getReqIdsForProd)
                {
                    if (workDict.Keys.Contains(r.clientId))
                    {
                        if (!workDict[r.clientId].Contains(r.requestId))
                        {
                            workDict[r.clientId].Add(r.requestId);
                            reqCounter++;
                        }
                    }
                    else
                    {
                        workDict[r.clientId] = new List<long>();
                        workDict[r.clientId].Add(r.requestId);
                        reqCounter++;
                    }
                    prodQuant += r.quantity;
                    prodPrice += r.price * r.quantity;
                }
                prodReqIdsDict[pn] = new ProductObject(reqCounter, prodQuant, Math.Round(prodPrice, 2));
            }
            return prodReqIdsDict;
        }
    }
}
