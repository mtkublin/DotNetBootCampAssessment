using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqRaportsApp
{
    public partial class Form1
    {
        public Dictionary<string, List<int>> AllRequests()
        {
            IEnumerable<string> getClientIds = from request in RequestsList
                                               select request.clientId;

            HashSet<string> clientIds = getClientIds.ToHashSet();

            Dictionary<string, List<int>> allReqsDict = new Dictionary<string, List<int>>();
            foreach (string cid in clientIds)
            {
                IEnumerable<int> getClientsReqIds = from request in RequestsList
                                                    where request.clientId == cid
                                                    select request.requestId;

                HashSet<int> reqIds = getClientsReqIds.ToHashSet();

                allReqsDict[cid] = reqIds.ToList();
            }

            return allReqsDict;
        }
    }
}
