using System.Collections.Generic;

namespace ReqRaportsApp
{
    public class request
    {
        public string clientId { get; set; }
        public int requestId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }

    public class WholeReq
    {
        public string clientId { get; set; }
        public int requestId { get; set; }
        public List<string> names { get; set; }
        public double value { get; set; }
    }
}
