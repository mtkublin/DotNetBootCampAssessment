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

    public class ProductObject
    {
        public int requestQuantity { get; set; }
        public int productQuantity { get; set; }
        public double productValue { get; set; }

        public ProductObject(int rq, int pq, double pv)
        {
            requestQuantity = rq;
            productQuantity = pq;
            productValue = pv;
        }
    }

    public class GridViewData
    {
        public string[] ColNames { get; set; }
        public List<List<object>> Rows { get; set; }

        public GridViewData(string[] cn, List<List<object>> r)
        {
            ColNames = cn;
            Rows = r;
        }
    }
}
