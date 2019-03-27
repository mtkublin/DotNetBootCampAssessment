using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public class DataFormatValidator
    {

        public bool isRequestFormatCorrect { get; set; }
        public string errMessage { get; set; }

        public DataFormatValidator(bool irfc, string em, request r)
        {
            isRequestFormatCorrect = irfc;
            errMessage = em;

            if (r.clientId.Length > 6 || r.clientId.Contains(" "))
            {
                isRequestFormatCorrect = false;
                errMessage = "Zły format identyfikatora klienta";
            }
            else if (r.clientId == null)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak identyfikatora klienta";
            }
            else if (r.name == null)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak nazwy produktu";
            }
            else if (r.name.Length > 255)
            {
                isRequestFormatCorrect = false;
                errMessage = "Zły format nazwy produktu";
            }
        }
    }

    public class Deserializer
    {
        List<request> MainReqList { get; set; }
        Dictionary<string, List<request>> AddedFiles { get; set; }

        public Deserializer(List<request> rList, Dictionary<string, List<request>> afDict)
        {
            MainReqList = rList;
            AddedFiles = afDict;
        }

        public void DeserializeXmlObject(string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<request>), new XmlRootAttribute("requests"));

                StreamReader reader = new StreamReader(path);

                List<request> XmlRequestsList = (List<request>)serializer.Deserialize(reader);

                AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)] = new List<request>();
                foreach (request r in XmlRequestsList)
                {
                    bool isRequestFormatCorrect = true;
                    string errMessage = string.Empty;
                    DataFormatValidator dataFormatValidator = new DataFormatValidator(isRequestFormatCorrect, errMessage, r);
                    if (dataFormatValidator.isRequestFormatCorrect)
                    {
                        MainReqList.Add(r);
                        AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)].Add(r);
                    }
                    else
                    {
                        MessageBox.Show(dataFormatValidator.errMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private class JsonRequestsObject
        {
            public List<request> requests { get; set; }
        }

        public void DesarializeJsonObject(string path)
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

                AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)] = new List<request>();
                foreach (request r in JsonRequestsList)
                {
                    bool isRequestFormatCorrect = true;
                    string errMessage = string.Empty;
                    DataFormatValidator dataFormatValidator = new DataFormatValidator(isRequestFormatCorrect, errMessage, r);
                    if (dataFormatValidator.isRequestFormatCorrect)
                    {
                        MainReqList.Add(r);
                        AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)].Add(r);
                    }
                    else
                    {
                        MessageBox.Show(dataFormatValidator.errMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeserializeCsvObject(string path)
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

                AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)] = new List<request>();
                foreach (request r in CsvRequestsList)
                {
                    bool isRequestFormatCorrect = true;
                    string errMessage = string.Empty;
                    DataFormatValidator dataFormatValidator = new DataFormatValidator(isRequestFormatCorrect, errMessage, r);
                    if (dataFormatValidator.isRequestFormatCorrect)
                    {
                        MainReqList.Add(r);
                        AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)].Add(r);
                    }
                    else
                    {
                        MessageBox.Show(dataFormatValidator.errMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
