using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReqRaportsApp;

namespace UnitTests
{
    [TestClass]
    public class RaportGeneratorsTest
    {
        List<request> ReqsList { get; set; }

        RapGenOperations RapGenOps { get; set; }
        RaportGenerators RapGen { get; set; }

        string[] cidList = { "1", "2", "3", "4", "2", "3", "3", "3", "4", "1", "2", "4", "4", "2", "6", "5", "5", "1", "2", "5" };
        long[] ridList = { 1, 1, 1, 1, 1, 1, 2, 1, 1, 7, 1, 2, 2, 3, 6, 2, 1, 1, 1, 1 };
        string[] nameList = { "Chleb", "Bułka", "Chleb", "Bułka", "Bułka", "Bułka", "Masło", "Chleb", "Chleb", "Masło", "Chleb", "Masło", "Bułka", "Chleb", "Mleko", "Mleko", "Bułka", "Masło", "Bułka", "Chleb" };
        int[] quantList = { 1, 2, 5, 1, 2, 3, 10, 7, 2, 4, 4, 1, 1, 6, 5, 2, 1, 2, 3, 2 };
        double[] priceList = { 5.56, 1.31, 5.56, 1.31, 1.31, 1.31, 7.23, 5.56, 5.56, 7.23, 5.56, 7.23, 1.31, 5.56, 4.20, 4.20, 1.31, 7.23, 1.31, 5.56 };

        public RaportGeneratorsTest()
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
            RapGen = new RaportGenerators(RapGenOps, ReqsList);
        }

        [TestMethod]
        public void ReqQuantTest()
        {
            int expectedQuant = 11;
            GridViewData testData = RapGen.ReqQuant();
            int testQuant = Convert.ToInt32(testData.Rows[0][0]);

            Assert.AreEqual(expectedQuant, testQuant);
        }

        [TestMethod]
        public void ReqQuantForClientTest()
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
                GridViewData testData = RapGen.ReqQuantForClient(cid);
                int testQuant = Convert.ToInt32(testData.Rows[0][1]);

                Assert.AreEqual(clientIdsWithExpectedValues[cid], testQuant);
            }
        }

        [TestMethod]
        public void ReqValueSumTest()
        {
            double expectedValue = 319.46;

            GridViewData testData = RapGen.ReqValueSum();
            double testValue = Convert.ToDouble(testData.Rows[0][0]);

            Assert.AreEqual(expectedValue, testValue);
        }

        [TestMethod]
        public void ReqValueSumForClientTest()
        {
            Dictionary<string, double> clientIdsWithExpectedValues = new Dictionary<string, double>()
            {
                { "1", 48.94 },
                { "2", 64.77 },
                { "3", 142.95 },
                { "4", 20.97 },
                { "5", 20.83 },
                { "6", 21 }
            };

            foreach(string cid in clientIdsWithExpectedValues.Keys)
            {
                GridViewData testData = RapGen.ReqValueSumForClientId(cid);
                double testValue = Math.Round(Convert.ToDouble(testData.Rows[0][1]), 2);

                Assert.AreEqual(clientIdsWithExpectedValues[cid], testValue);
            }
        }

        [TestMethod]
        public void AllReqsTest()
        {
            int expectedRowCount = 20;

            GridViewData testData = RapGen.AllReqsList();
            List<List<object>> testRows = testData.Rows;

            Assert.AreEqual(expectedRowCount, testRows.Count);

            foreach (List<object> row in testRows)
            {
                Assert.AreEqual(5, row.Count);
                Assert.AreEqual(typeof(string), row[0].GetType());
                Assert.AreEqual(typeof(long), row[1].GetType());
                Assert.AreEqual(typeof(string), row[2].GetType());
                Assert.AreEqual(typeof(int), row[3].GetType());
                Assert.AreEqual(typeof(double), row[4].GetType());
            }
        }

        [TestMethod]
        public void AllReqsForClientTest()
        {
            Dictionary<string, int> clientIdsWithexpectedValues = new Dictionary<string, int>()
            {
                { "1", 3 },
                { "2", 5 },
                { "3", 4 },
                { "4", 4 },
                { "5", 3 },
                { "6", 1 }
            };

            foreach (string cid in clientIdsWithexpectedValues.Keys)
            {
                GridViewData testData = RapGen.ReqsListForClientId(cid);
                List<List<object>> testRows = testData.Rows;

                Assert.AreEqual(clientIdsWithexpectedValues[cid], testRows.Count);

                foreach (List<object> row in testRows)
                {
                    Assert.AreEqual(5, row.Count);
                    Assert.AreEqual(typeof(string), row[0].GetType());
                    Assert.AreEqual(typeof(long), row[1].GetType());
                    Assert.AreEqual(typeof(string), row[2].GetType());
                    Assert.AreEqual(typeof(int), row[3].GetType());
                    Assert.AreEqual(typeof(double), row[4].GetType());
                }
            }
        }

        [TestMethod]
        public void AverageReqValueTest()
        {
            double expectedAvgVal = 29.04;

            GridViewData testData = RapGen.AverageReqValue();
            double testValue = Convert.ToDouble(testData.Rows[0][0]);

            Assert.AreEqual(expectedAvgVal, testValue);
        }

        [TestMethod]
        public void AverageReqValueForClientTest()
        {
            Dictionary<string, double> clientIdsWithexpectedValues = new Dictionary<string, double>()
            {
                { "1", 24.47 },
                { "2", 32.39 },
                { "3", 71.47 },
                { "4", 10.49 },
                { "5", 10.42 },
                { "6", 21 }
            };

            foreach (string cid in clientIdsWithexpectedValues.Keys)
            {
                GridViewData testData = RapGen.AverageReqValueForClientId(cid);
                double testValue = Convert.ToDouble(testData.Rows[0][1]);

                Assert.AreEqual(clientIdsWithexpectedValues[cid], testValue);
            }
        }

        [TestMethod]
        public void ReqQuantByNameTest()
        {
            Dictionary<string, int> prodNamesWithexpectedValues = new Dictionary<string, int>()
            {
                { "Bułka", 5 },
                { "Chleb", 6 },
                { "Masło", 4 },
                { "Mleko", 2 }
            };

            GridViewData testData = RapGen.ReqQuantByName();

            foreach (string name in prodNamesWithexpectedValues.Keys)
            {
                IEnumerable<object> getReqsByName = from row in testData.Rows
                                                          where row[0] as string == name
                                                          select row[1];

                int testValue = Convert.ToInt32(getReqsByName.ToList()[0]);

                Assert.AreEqual(prodNamesWithexpectedValues[name], testValue);
            }
        }

        [TestMethod]
        public void ReqQuantByNameForClientTest()
        {
            Dictionary<string, Dictionary<string, int>> prodNamesForClientsWithexpectedValues = new Dictionary<string, Dictionary<string, int>>();

            prodNamesForClientsWithexpectedValues["1"] = new Dictionary<string, int>()
            {
                { "Chleb", 1 },
                { "Masło", 2 },
            };
            prodNamesForClientsWithexpectedValues["2"] = new Dictionary<string, int>()
            {
                { "Bułka", 1 },
                { "Chleb", 2 }
            };
            prodNamesForClientsWithexpectedValues["3"] = new Dictionary<string, int>()
            {
                { "Bułka", 1 },
                { "Chleb", 1 },
                { "Masło", 1 },
            };
            prodNamesForClientsWithexpectedValues["4"] = new Dictionary<string, int>()
            {
                { "Bułka", 2 },
                { "Chleb", 1 },
                { "Masło", 1 },
            };
            prodNamesForClientsWithexpectedValues["5"] = new Dictionary<string, int>()
            {
                { "Bułka", 1 },
                { "Chleb", 1 },
                { "Mleko", 1 }
            };
            prodNamesForClientsWithexpectedValues["6"] = new Dictionary<string, int>()
            {
                { "Mleko", 1 }
            };

            foreach (string cid in prodNamesForClientsWithexpectedValues.Keys)
            {
                GridViewData testData = RapGen.ReqQuantByNameForClientId(cid);

                foreach (string name in prodNamesForClientsWithexpectedValues[cid].Keys)
                {
                    IEnumerable<object> getReqsByName = from row in testData.Rows
                                                        where row[1] as string == name
                                                        select row[2];

                    int testValue = Convert.ToInt32(getReqsByName.ToList()[0]);

                    Assert.AreEqual(prodNamesForClientsWithexpectedValues[cid][name], testValue);
                }
            }
        }
    }
}