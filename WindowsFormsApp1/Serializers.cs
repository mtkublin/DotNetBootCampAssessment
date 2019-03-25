using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;

namespace ReqRaportsApp
{
    public class MyXmlSerializer
    {
        public static void DeserializeXmlObject(string path, List<request> MainReqList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<request>), new XmlRootAttribute("requests"));

            StreamReader reader = new StreamReader(path);

            List<request> XmlRequestsList = (List<request>)serializer.Deserialize(reader);

            foreach (request r in XmlRequestsList)
            {
                MainReqList.Add(r);
            }
        }
    }

    public class MyJsonSerializer
    {
        public class JsonRequestsObject
        {
            public List<request> requests { get; set; }
        }

        public static void DesarializeJsonObject(string path, List<request> MainReqList)
        {
            string jsonData = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    jsonData += line;
                }
            }

            JsonRequestsObject jsonReqObject = new JavaScriptSerializer().Deserialize<JsonRequestsObject>(jsonData);

            List<request> JsonRequestsList = jsonReqObject.requests;

            foreach (request r in JsonRequestsList)
            {
                MainReqList.Add(r);
            }
        }
    }

    public class MyCsvSerializer
    {
        public static void DeserializeCsvObject(string path, List<request> MainReqList)
        {
            string[] table = File.ReadAllLines(path);
            var requests = table.Skip(1);

            IEnumerable<request> queryRequests =
            from requestLine in requests
            let splitRequest = requestLine.Split(',')
            select new request()
            {
                clientId = splitRequest[0],
                requestId = Convert.ToInt32(splitRequest[1]),
                name = splitRequest[2],
                quantity = Convert.ToInt32(splitRequest[3]),
                //price = Convert.ToDouble(splitRequest[4])
                prices = splitRequest[4]
            };

            List<request> CsvRequestsList = queryRequests.ToList();

            string toShow = string.Empty;
            foreach (request r in CsvRequestsList)
            {
                MainReqList.Add(r);
                toShow += r.ToString() + "/n";
            }
        }
    }

}
