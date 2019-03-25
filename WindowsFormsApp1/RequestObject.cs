using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqRaportsApp
{
    public class request
    {
        public string clientId { get; set; }
        public int requestId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string prices { get; set; }
    }
}
