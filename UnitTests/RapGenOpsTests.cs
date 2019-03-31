using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReqRaportsApp;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class RapGenOpsTests
    {
        List<request> ReqsList { get; set; }
        RapGenOperations RapGenOps { get; set; }

        string[] cidList = { "1", "2", "3", "4", "2", "3", "3", "3", "4", "1", "2", "4", "4", "2", "6", "5", "5", "1", "2", "5" };
        long[] ridList = { 1, 1, 1, 1, 1, 1, 2, 1, 1, 7, 1, 2, 2, 3, 6, 2, 1, 1, 1, 1 };
        string[] nameList = { "Chleb", "Bułka", "Chleb", "Bułka", "Bułka", "Bułka", "Masło", "Chleb", "Chleb", "Masło", "Chleb", "Masło", "Bułka", "Chleb", "Mleko", "Mleko", "Bułka", "Masło", "Bułka", "Chleb" };
        int[] quantList = { 1, 2, 5, 1, 2, 3, 10, 7, 2, 4, 4, 1, 1, 6, 5, 2, 1, 2, 3, 2 };
        double[] priceList = { 5.56, 1.31, 5.56, 1.31, 1.31, 1.31, 7.23, 5.56, 5.56, 7.23, 5.56, 7.23, 1.31, 5.56, 4.20, 4.20, 1.31, 7.23, 1.31, 5.56 };

        public RapGenOpsTests()
        {
            ReqsList = new List<request>();
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
                ReqsList.Add(req);
            }
            RapGenOps = new RapGenOperations(ReqsList);
        }

        [TestMethod]
        public void AllReqsForClientTest()
        {
            Dictionary<string, int> clientIdsWithExpectedValues = new Dictionary<string, int>()
            {
                { "1", 2 },
                { "2", 2 },
                { "3", 2 },
                { "4", 2 },
                { "5", 2 },
                { "6", 1 }
            };

            foreach (string cid in clientIdsWithExpectedValues.Keys)
            {
                HashSet<long> reqIdsForClient = RapGenOps.AllReqsForClient(cid);
                int testQuant = reqIdsForClient.Count;

                Assert.AreEqual(clientIdsWithExpectedValues[cid], testQuant);
            }
        }

        [TestMethod]
        public void AllReqsTest()
        {
            int expectedQuant = 11;

            Dictionary<string, List<long>> allReqDict = RapGenOps.AllRequests();
            int testQuant = 0;
            foreach (string k in allReqDict.Keys)
            {
                testQuant += allReqDict[k].Count;
            }

            Assert.AreEqual(expectedQuant, testQuant);
        }

        [TestMethod]
        public void wholeReqValueDictTest()
        {
            Dictionary<string, Dictionary<long, double>> expectedReqWholeValDict = new Dictionary<string, Dictionary<long, double>>();

            expectedReqWholeValDict["1"] = new Dictionary<long, double>()
            {
                { 1, 20.02 },
                { 7, 28.92 }
            };
            expectedReqWholeValDict["2"] = new Dictionary<long, double>()
            {
                { 1, 31.41 },
                { 3, 33.36 }
            };
            expectedReqWholeValDict["3"] = new Dictionary<long, double>()
            {
                { 1, 70.65 },
                { 2, 72.30 }
            };
            expectedReqWholeValDict["4"] = new Dictionary<long, double>()
            {
                { 1, 12.43 },
                { 2, 8.54 }
            };
            expectedReqWholeValDict["5"] = new Dictionary<long, double>()
            {
                { 1, 12.43 },
                { 2, 8.40 }
            };
            expectedReqWholeValDict["6"] = new Dictionary<long, double>()
            {
                { 6, 21.00 }
            };

            Dictionary<string, Dictionary<long, double>> testReqWholeValDict = RapGenOps.wholeReqValueDict();

            foreach (string cid in testReqWholeValDict.Keys)
            {
                Assert.IsTrue(expectedReqWholeValDict.Keys.Contains(cid));
                foreach (long rid in testReqWholeValDict[cid].Keys)
                {
                    Assert.IsTrue(expectedReqWholeValDict[cid].Keys.Contains(rid));
                    Assert.AreEqual(expectedReqWholeValDict[cid][rid], testReqWholeValDict[cid][rid]);
                }
            }

        }

        [TestMethod]
        public void maxPriceTest()
        {
            double expectedMaxPrice = 72.30;
            double testMaxPrice = RapGenOps.maxPrice();

            Assert.AreEqual(expectedMaxPrice, testMaxPrice);
        }

        [TestMethod]
        public void RequestsValuesSumTest()
        {
            double expectedValuesSum = 319.46;
            double testValueSum = RapGenOps.RequestsValuesSum(ReqsList);

            Assert.AreEqual(expectedValuesSum, testValueSum);
        }

        [TestMethod]
        public void ClientsValuesSumTest()
        {
            Dictionary<string, double> expectedClientValueDict = new Dictionary<string, double>()
            {
                { "1", 48.94 },
                { "2", 64.77 },
                { "3", 142.95 },
                { "4", 20.97 },
                { "5", 20.83 },
                { "6", 21.00 }
            };

            foreach (string cid in expectedClientValueDict.Keys)
            {
                double testClientValue = RapGenOps.ClientsValuesSum(cid);
                Assert.AreEqual(expectedClientValueDict[cid], testClientValue);
            }
        }

        [TestMethod]
        public void ProductReqIdsTest()
        {
            Dictionary<string, ProductObject> expectedOutputDict = new Dictionary<string, ProductObject>();
            expectedOutputDict["Bułka"] = new ProductObject(5, 13, 17.03);
            expectedOutputDict["Chleb"] = new ProductObject(6, 27, 150.12);
            expectedOutputDict["Masło"] = new ProductObject(4, 17, 122.91);
            expectedOutputDict["Mleko"] = new ProductObject(2, 7, 29.40);

            Dictionary<string, ProductObject> testOutputDict = RapGenOps.ProductReqIds(ReqsList);

            foreach (string pn in testOutputDict.Keys)
            {
                Assert.IsTrue(expectedOutputDict.Keys.Contains(pn));

                Assert.AreEqual(expectedOutputDict[pn].requestQuantity, testOutputDict[pn].requestQuantity);
                Assert.AreEqual(expectedOutputDict[pn].productQuantity, testOutputDict[pn].productQuantity);
                Assert.AreEqual(expectedOutputDict[pn].productValue, testOutputDict[pn].productValue);
            }
        }
    }
}
