using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ReqRaportsApp
{
    public class Serializers
    {
        List<request> MainReqList { get; set; }
        Dictionary<string, List<request>> AddedFiles { get; set; }

        public Serializers(List<request> rList, Dictionary<string, List<request>> afDict)
        {
            MainReqList = rList;
            AddedFiles = afDict;
        }

        private Dictionary<bool, string> DataFormatValidate(request r)
        {
            bool isRequestFormatCorrect = true;
            string errMessage = string.Empty;

            Dictionary<bool, string> dictToReturn = new Dictionary<bool, string>();

            if (r.clientId.Length > 6 || r.clientId.Contains(" "))
            {
                isRequestFormatCorrect = false;
                errMessage = "Zły format identyfikatora klienta w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
            }
            else if (r.clientId == null || r.clientId == string.Empty)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak identyfikatora klienta w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
            }
            else if (r.name == null || r.name == string.Empty)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak nazwy produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
            }
            else if (r.name.Length > 255)
            {
                isRequestFormatCorrect = false;
                errMessage = "Za długa nazwa produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
            }
            else if (r.price == 0)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak ceny produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
            }
            else if (r.quantity == 0)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak ilości produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
            }

            dictToReturn[isRequestFormatCorrect] = errMessage;

            return dictToReturn;
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
                    Dictionary<bool, string> dataValidDict = DataFormatValidate(r);
                    bool isRequestFormatCorrect = dataValidDict.Keys.ToList()[0];
                    string errMessage = dataValidDict[isRequestFormatCorrect];
                    if (isRequestFormatCorrect)
                    {
                        MainReqList.Add(r);
                        AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)].Add(r);
                    }
                    else
                    {
                        MessageBox.Show(errMessage);
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
                    Dictionary<bool, string> dataValidDict = DataFormatValidate(r);
                    bool isRequestFormatCorrect = dataValidDict.Keys.ToList()[0];
                    string errMessage = dataValidDict[isRequestFormatCorrect];
                    if (isRequestFormatCorrect)
                    {
                        MainReqList.Add(r);
                        AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)].Add(r);
                    }
                    else
                    {
                        MessageBox.Show(errMessage);
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
                if (table[0] == "Client_Id,Request_id,Name,Quantity,Price")
                {
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
                        Dictionary<bool, string> dataValidDict = DataFormatValidate(r);
                        bool isRequestFormatCorrect = dataValidDict.Keys.ToList()[0];
                        string errMessage = dataValidDict[isRequestFormatCorrect];
                        if (isRequestFormatCorrect)
                        {
                            MainReqList.Add(r);
                            AddedFiles[path.Substring(path.LastIndexOf("\\") + 1)].Add(r);
                        }
                        else
                        {
                            MessageBox.Show(errMessage);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe nazwy kolumn");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<string> SerializeToCsv(List<List<string>> RowsList)
        {
            List<string> textData = new List<string>();

            foreach (List<string> row in RowsList)
            {
                string rowText = string.Empty;
                for (int cell = 0; cell < row.Count; cell++)
                {
                    if (cell != 0)
                    {
                        rowText += ",";
                    }
                    string cellToAdd = Regex.Replace(row[cell], ",", ".");
                    rowText += cellToAdd;
                }
                textData.Add(rowText);
            }
            return textData;
        }
    }

}
