using System.Collections.Generic;
using System.Linq;

namespace ReqRaportsApp
{
    public class DataHandler
    {
        List<request> ReqsList { get; set; }
        Dictionary<string, List<request>> AddedFiles { get; set; }

        public DataHandler(Dictionary<string, List<request>> addFiles, List<request> reqList)
        {
            ReqsList = reqList;
            AddedFiles = addFiles;
        }

        public void RemoveData(List<string> clientIdsToCheckList, object item)
        {
            foreach (request r in AddedFiles[item.ToString()])
            {
                ReqsList.Remove(r);
                clientIdsToCheckList.Add(r.clientId);
            }

            AddedFiles.Remove(item.ToString());
        }

        public List<string> checkClientIds(HashSet<string> clientIdsToCheckSet)
        {
            List<string> clientIdsToRemove = new List<string>();
            foreach (string cid in clientIdsToCheckSet)
            {
                IEnumerable<string> getClientIdsToRemove = from req in ReqsList
                                                           where req.clientId == cid
                                                           select req.clientId;

                if (getClientIdsToRemove.Count() == 0)
                {
                    clientIdsToRemove.Add(cid);
                }
            }
            return clientIdsToRemove;
        }

        public HashSet<string> getClientIds()
        {
            IEnumerable<string> getClientIdsQuery = from request in ReqsList
                                                    select request.clientId;

            HashSet<string> clientIds = new HashSet<string>(getClientIdsQuery.ToList());

            return clientIds;
        }
    }
}
