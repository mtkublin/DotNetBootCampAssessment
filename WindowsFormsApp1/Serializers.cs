using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public class MyXmlSerializer
    {
        public static void DeserializeXmlObject(string path, List<request> MainReqList, List<string> AddedFiles)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<request>), new XmlRootAttribute("requests"));

                StreamReader reader = new StreamReader(path);

                List<request> XmlRequestsList = (List<request>)serializer.Deserialize(reader);

                foreach (request r in XmlRequestsList)
                {
                    MainReqList.Add(r);
                }

                AddedFiles.Add(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class MyJsonSerializer
    {
        public class JsonRequestsObject
        {
            public List<request> requests { get; set; }
        }

        public static void DesarializeJsonObject(string path, List<request> MainReqList, List<string> AddedFiles)
        {
            try
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

                AddedFiles.Add(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class MyCsvSerializer
    {
        public static void DeserializeCsvObject(string path, List<request> MainReqList, List<string> AddedFiles)
        {
            try
            {
                string[] table = File.ReadAllLines(path);
                var requests = table.Skip(1);

                IEnumerable<request> queryRequests =
                from requestLine in requests
                let splitRequest = requestLine.Split(',')
                select new request()
                {
                    clientId = splitRequest[0],
                    requestId = Int32.Parse(splitRequest[1]),
                    name = splitRequest[2],
                    quantity = Int32.Parse(splitRequest[3]),
                    price = Double.Parse(splitRequest[4], System.Globalization.CultureInfo.InvariantCulture)
                };

                List<request> CsvRequestsList = queryRequests.ToList();

                foreach (request r in CsvRequestsList)
                {
                    MainReqList.Add(r);
                }

                AddedFiles.Add(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
