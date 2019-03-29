using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReqRaportsApp;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class SerializerTests
    {
        #region TestData
        string jsonDataToDeserialize = "{\"requests\": [" +
            "{\"clientId\": \"1\",\"requestId\": 1,\"name\": \"Chleb\",\"quantity\": 1,\"price\": 5.56}," +
            "{\"clientId\": \"1\",\"requestId\": 1,\"name\": \"Masło\",\"quantity\": 2,\"price\": 7.23}," +
            "{\"clientId\": \"1\",\"requestId\": 7,\"name\": \"Masło\",\"quantity\": 4,\"price\": 7.23}," +
            "{\"clientId\": \"2\",\"requestId\": 1,\"name\": \"Bułka\",\"quantity\": 2,\"price\": 1.31}," +
            "{\"clientId\": \"2\",\"requestId\": 1,\"name\": \"Bułka\",\"quantity\": 2,\"price\": 1.31}," +
            "{\"clientId\": \"2\",\"requestId\": 1,\"name\": \"Bułka\",\"quantity\": 3,\"price\": 1.31}," +
            "{\"clientId\": \"2\",\"requestId\": 1,\"name\": \"Chleb\",\"quantity\": 4,\"price\": 5.56}," +
            "{\"clientId\": \"2\",\"requestId\": 3,\"name\": \"Chleb\",\"quantity\": 6,\"price\": 5.56}," +
            "{\"clientId\": \"3\",\"requestId\": 1,\"name\": \"Bułka\",\"quantity\": 3,\"price\": 1.31}," +
            "{\"clientId\": \"3\",\"requestId\": 1,\"name\": \"Chleb\",\"quantity\": 5,\"price\": 5.56}," +
            "{\"clientId\": \"3\",\"requestId\": 1,\"name\": \"Chleb\",\"quantity\": 7,\"price\": 5.56}," +
            "{\"clientId\": \"3\",\"requestId\": 2,\"name\": \"Masło\",\"quantity\": 10,\"price\": 7.23}," +
            "{\"clientId\": \"4\",\"requestId\": 1,\"name\": \"Bułka\",\"quantity\": 1,\"price\": 1.31}," +
            "{\"clientId\": \"4\",\"requestId\": 1,\"name\": \"Chleb\",\"quantity\": 2,\"price\": 5.56}," +
            "{\"clientId\": \"4\",\"requestId\": 2,\"name\": \"Bułka\",\"quantity\": 1,\"price\": 1.31}," +
            "{\"clientId\": \"4\",\"requestId\": 2,\"name\": \"Masło\",\"quantity\": 1,\"price\": 7.23}," +
            "{\"clientId\": \"5\",\"requestId\": 1,\"name\": \"Bułka\",\"quantity\": 1,\"price\": 1.31}," +
            "{\"clientId\": \"5\",\"requestId\": 1,\"name\": \"Chleb\",\"quantity\": 2,\"price\": 5.56}," +
            "{\"clientId\": \"5\",\"requestId\": 2,\"name\": \"Mleko\",\"quantity\": 2,\"price\": 4.20}," +
            "{\"clientId\": \"6\",\"requestId\": 6,\"name\": \"Mleko\",\"quantity\": 5,\"price\": 4.20}]}";

        string xmlDataToDeserialize = "<requests>< request >< clientId > 1 </ clientId >< requestId > 1 </ requestId >< name > Chleb </ name >< quantity > 1 </ quantity >< price > 5.56 </ price ></ request >" +
            "< request >< clientId > 1 </ clientId >< requestId > 1 </ requestId >< name > Masło </ name >< quantity > 2 </ quantity >< price > 7.23 </ price ></ request >" +
            "< request >< clientId > 1 </ clientId >< requestId > 7 </ requestId >< name > Masło </ name >< quantity > 4 </ quantity >< price > 7.23 </ price ></ request >" +
            "< request >< clientId > 2 </ clientId >< requestId > 1 </ requestId >< name > Bułka </ name >< quantity > 2 </ quantity >< price > 1.31 </ price ></ request >" +
            "< request >< clientId > 2 </ clientId >< requestId > 1 </ requestId >< name > Bułka </ name >< quantity > 2 </ quantity >< price > 1.31 </ price ></ request >" +
            "< request >< clientId > 2 </ clientId >< requestId > 1 </ requestId >< name > Bułka </ name >< quantity > 3 </ quantity >< price > 1.31 </ price ></ request >" +
            "< request >< clientId > 2 </ clientId >< requestId > 1 </ requestId >< name > Chleb </ name >< quantity > 4 </ quantity >< price > 5.56 </ price ></ request >" +
            "< request >< clientId > 2 </ clientId >< requestId > 3 </ requestId >< name > Chleb </ name >< quantity > 6 </ quantity >< price > 5.56 </ price ></ request >" +
            "< request >< clientId > 3 </ clientId >< requestId > 1 </ requestId >< name > Bułka </ name >< quantity > 3 </ quantity >< price > 1.31 </ price ></ request >" +
            "< request >< clientId > 3 </ clientId >< requestId > 1 </ requestId >< name > Chleb </ name >< quantity > 5 </ quantity >< price > 5.56 </ price ></ request >" +
            "< request >< clientId > 3 </ clientId >< requestId > 1 </ requestId >< name > Chleb </ name >< quantity > 7 </ quantity >< price > 5.56 </ price ></ request >" +
            "< request >< clientId > 3 </ clientId >< requestId > 2 </ requestId >< name > Masło </ name >< quantity > 10 </ quantity >< price > 7.23 </ price ></ request >" +
            "< request >< clientId > 4 </ clientId >< requestId > 1 </ requestId >< name > Bułka </ name >< quantity > 1 </ quantity >< price > 1.31 </ price ></ request >" +
            "< request >< clientId > 4 </ clientId >< requestId > 1 </ requestId >< name > Chleb </ name >< quantity > 2 </ quantity >< price > 5.56 </ price ></ request >" +
            "< request >< clientId > 4 </ clientId >< requestId > 2 </ requestId >< name > Bułka </ name >< quantity > 1 </ quantity >< price > 1.31 </ price ></ request >" +
            "< request >< clientId > 4 </ clientId >< requestId > 2 </ requestId >< name > Masło </ name >< quantity > 1 </ quantity >< price > 7.23 </ price ></ request >" +
            "< request >< clientId > 5 </ clientId >< requestId > 1 </ requestId >< name > Bułka </ name >< quantity > 1 </ quantity >< price > 1.31 </ price ></ request >" +
            "< request >< clientId > 5 </ clientId >< requestId > 1 </ requestId >< name > Chleb </ name >< quantity > 2 </ quantity >< price > 5.56 </ price ></ request >" +
            "< request >< clientId > 5 </ clientId >< requestId > 2 </ requestId >< name > Mleko </ name >< quantity > 2 </ quantity >< price > 4.2 </ price ></ request >" +
            "< request >< clientId > 6 </ clientId >< requestId > 6 </ requestId >< name > Mleko </ name >< quantity > 5 </ quantity >< price > 4.2 </ price ></ request ></ requests > ";

        string asdsa = "clientId,requestId,name,quantity,price\n" +
            "1,1, Chleb,1,5.56\n" +
            "1,1, Masło,2,7.23\n" +
            "1,7, Masło,4,7.23\n" +
            "2,1, Bułka,2,1.31\n" +
            "2,1, Bułka,2,1.31\n" +
            "2,1, Bułka,3,1.31\n" +
            "2,1, Chleb,4,5.56\n" +
            "2,3, Chleb,6,5.56\n" +
            "3,1, Bułka,3,1.31\n" +
            "3,1, Chleb,5,5.56\n" +
            "3,1, Chleb,7,5.56\n" +
            "3,2, Masło,10,7.23\n" +
            "4,1, Bułka,1,1.31\n" +
            "4,1, Chleb,2,5.56\n" +
            "4,2, Bułka,1,1.31\n" +
            "4,2, Masło,1,7.23\n" +
            "5,1, Bułka,1,1.31\n" +
            "5,1, Chleb,2,5.56\n" +
            "5,2, Mleko,2,4.20\n" +
            "6,6, Mleko,5,4.20";
        #endregion

        List<request> ExpectedReqsList { get; set; }
        List<request> ActualReqsList { get; set; }

        Serializers Serializer { get; set; }
        Dictionary<string, List<request>> AddedFiles { get; set; }

        string[] cidList = { "1", "2", "3", "4", "2", "3", "3", "3", "4", "1", "2", "4", "4", "2", "6", "5", "5", "1", "2", "5" };
        long[] ridList = { 1, 1, 1, 1, 1, 1, 2, 1, 1, 7, 1, 2, 2, 3, 6, 2, 1, 1, 1, 1 };
        string[] nameList = { "Chleb", "Bułka", "Chleb", "Bułka", "Bułka", "Bułka", "Masło", "Chleb", "Chleb", "Masło", "Chleb", "Masło", "Bułka", "Chleb", "Mleko", "Mleko", "Bułka", "Masło", "Bułka", "Chleb" };
        int[] quantList = { 1, 2, 5, 1, 2, 3, 10, 7, 2, 4, 4, 1, 1, 6, 5, 2, 1, 2, 3, 2 };
        double[] priceList = { 5.56, 1.31, 5.56, 1.31, 1.31, 1.31, 7.23, 5.56, 5.56, 7.23, 5.56, 7.23, 1.31, 5.56, 4.20, 4.20, 1.31, 7.23, 1.31, 5.56 };

        public SerializerTests()
        {
            ExpectedReqsList = new List<request>();
            for (int i = 0; i < 20; i++)
            {
                request req = new request()
                {
                    clientId = cidList[i],
                    requestId = ridList[i],
                    name = nameList[i],
                    quantity = quantList[i],
                    price = priceList[i]
                };
                ExpectedReqsList.Add(req);
            }

            ActualReqsList = new List<request>();
            AddedFiles = new Dictionary<string, List<request>>();
            Serializer = new Serializers(ActualReqsList, AddedFiles);
        }

        [TestMethod]
        public void DeserializeXmlObjectTest()
        {
            string filePath = Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf('\\'));
            }
            filePath += "\\TestData";

            Serializer.DeserializeXmlObject(filePath);
        }
    }
}
