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
    }
}
