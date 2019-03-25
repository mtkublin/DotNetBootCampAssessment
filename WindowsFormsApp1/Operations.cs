using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqRaportsApp
{
    public partial class Form1
    {
        public HashSet<int> AllReqsForClient(string currentClientId)
        {
            IEnumerable<int> getClientReqs = from request in RequestsList
                                             where request.clientId == currentClientId
                                             select request.requestId;

            HashSet<int> reqIdsForClient = new HashSet<int>(getClientReqs.ToList());

            return reqIdsForClient;
        }

        public Dictionary<string, List<int>> AllRequests()
        {
            IEnumerable<string> getClientIds = from request in RequestsList
                                               select request.clientId;

            HashSet<string> clientIds = getClientIds.ToHashSet();

            Dictionary<string, List<int>> allReqsDict = new Dictionary<string, List<int>>();
            foreach (string cid in clientIds)
            {
                HashSet<int> reqIds = AllReqsForClient(cid);

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
    }
}
