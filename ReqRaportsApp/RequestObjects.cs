using System.Collections.Generic;

namespace ReqRaportsApp
{
    public class request
    {
        public string clientId { get; set; }
        public long requestId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }

    public class RequestList
    {
        public static List<request> ReqsList = new List<request>();
        public static Dictionary<string, List<request>> AddedFiles = new Dictionary<string, List<request>>();

    }

    public class RequestWithSummaricValue
    {
        public string clientId { get; set; }
        public long requestId { get; set; }
        public double value { get; set; }

        public RequestWithSummaricValue (string cid, long rid, double val)
        {
            clientId = cid;
            requestId = rid;
            value = val;
        }
    }
}
