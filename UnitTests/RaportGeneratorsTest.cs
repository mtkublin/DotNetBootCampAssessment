using System;
using System.Windows.Forms;
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
            int correctQuant = 11;
            GridViewData testData = RapGen.ReqQuant();
            int testQuant = Convert.ToInt32(testData.Rows[0][0]);

            if (testQuant == correctQuant)
            {
                MessageBox.Show("Test passed");
            }
            else
            {
                MessageBox.Show("Test failed\n" + testQuant.ToString() + " != 11");
            }
        }

        [TestMethod]
        public void ReqQuantForClientTest()
        {
            Dictionary<string, int> clientIdsWithCorrectValues = new Dictionary<string, int>()
            {
                { "1", 2 },
                { "2", 2 },
                { "3", 2 },
                { "4", 2 },
                { "5", 2 },
                { "6", 1 }
            };

            foreach (string cid in clientIdsWithCorrectValues.Keys)
            {
                GridViewData testData = RapGen.ReqQuantForClient(cid);
                int testQuant = Convert.ToInt32(testData.Rows[0][1]);

                if (testQuant == clientIdsWithCorrectValues[cid])
                {
                    MessageBox.Show("Test passed for client \"" + cid + "\"") ;
                }
                else
                {
                    MessageBox.Show("Test failed for client \"" + cid + "\"\n" + testQuant.ToString() + " != " + clientIdsWithCorrectValues[cid]);
                }
            }
        }

        [TestMethod]
        public void ReqValueSumTest()
        {
            double correctValue = 319.46;

            GridViewData testData = RapGen.ReqValueSum();
            double testValue = Convert.ToDouble(testData.Rows[0][0]);

            if (testValue == correctValue)
            {
                MessageBox.Show("Test passed");
            }
            else
            {
                MessageBox.Show("Test failed\n" + testValue.ToString() + " != 319.46");
            }
        }

        [TestMethod]
        public void ReqValueSumForClientTest()
        {
            Dictionary<string, double> clientIdsWithCorrectValues = new Dictionary<string, double>()
            {
                { "1", 48.94 },
                { "2", 64.77 },
                { "3", 142.95 },
                { "4", 20.97 },
                { "5", 20.83 },
                { "6", 21 }
            };

            foreach(string cid in clientIdsWithCorrectValues.Keys)
            {
                GridViewData testData = RapGen.ReqValueSumForClientId(cid);
                double testValue = Math.Round(Convert.ToDouble(testData.Rows[0][1]), 2);

                if (testValue == clientIdsWithCorrectValues[cid])
                {
                    MessageBox.Show("Test passed for client \"" + cid + "\"");
                }
                else
                {
                    MessageBox.Show("Test failed for client \"" + cid + "\"\n" + testValue.ToString() + " != " + clientIdsWithCorrectValues[cid]);
                }
            }
        }

        [TestMethod]
        public void AllReqsTest()
        {
            int correctRowQuant = 20;

            GridViewData testData = RapGen.AllReqsList();
            List<List<object>> testRows = testData.Rows;

            if (testRows.Count == correctRowQuant)
            {
                foreach (List<object> row in testRows)
                {
                    if (row.Count != 5)
                    {
                        MessageBox.Show("Row is missing elements");
                        break;
                    }
                    else if (row[0].GetType() != typeof(string))
                    {
                        MessageBox.Show("Wrong type for client id");
                        break;
                    }
                    else if (row[1].GetType() != typeof(long))
                    {
                        MessageBox.Show("Wrong type for request id");
                        break;
                    }
                    else if (row[2].GetType() != typeof(string))
                    {
                        MessageBox.Show("Wrong type for name");
                        break;
                    }
                    else if (row[3].GetType() != typeof(int))
                    {
                        MessageBox.Show("Wrong type for quantity");
                        break;
                    }
                    else if (row[4].GetType() != typeof(double))
                    {
                        MessageBox.Show("Wrong type for price");
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Rows are missing");
            }
        }

        [TestMethod]
        public void AllReqsForClientTest()
        {
            Dictionary<string, int> clientIdsWithCorrectValues = new Dictionary<string, int>()
            {
                { "1", 3 },
                { "2", 5 },
                { "3", 4 },
                { "4", 4 },
                { "5", 3 },
                { "6", 1 }
            };

            foreach (string cid in clientIdsWithCorrectValues.Keys)
            {
                GridViewData testData = RapGen.ReqsListForClientId(cid);
                List<List<object>> testRows = testData.Rows;

                if (testRows.Count == clientIdsWithCorrectValues[cid])
                {
                    foreach (List<object> row in testRows)
                    {
                        if (row.Count != 5)
                        {
                            MessageBox.Show("Row is missing elements");
                            break;
                        }
                        else if (row[0].GetType() != typeof(string))
                        {
                            MessageBox.Show("Wrong type for client id");
                            break;
                        }
                        else if (row[1].GetType() != typeof(long))
                        {
                            MessageBox.Show("Wrong type for request id");
                            break;
                        }
                        else if (row[2].GetType() != typeof(string))
                        {
                            MessageBox.Show("Wrong type for name");
                            break;
                        }
                        else if (row[3].GetType() != typeof(int))
                        {
                            MessageBox.Show("Wrong type for quantity");
                            break;
                        }
                        else if (row[4].GetType() != typeof(double))
                        {
                            MessageBox.Show("Wrong type for price");
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Rows are missing");
                }
            }
        }

        [TestMethod]
        public void AverageReqValueTest()
        {
            double correctAvgVal = 29.04;

            GridViewData testData = RapGen.AverageReqValue();
            double testValue = Convert.ToDouble(testData.Rows[0][0]);

            if (testValue == correctAvgVal)
            {
                MessageBox.Show("Test passed");
            }
            else
            {
                MessageBox.Show("Test failed\n" + testValue.ToString() + " != 29.04");
            }
        }

        [TestMethod]
        public void AverageReqValueForClientTest()
        {
            Dictionary<string, double> clientIdsWithCorrectValues = new Dictionary<string, double>()
            {
                { "1", 24.47 },
                { "2", 32.39 },
                { "3", 71.48 },
                { "4", 10.49 },
                { "5", 10.42 },
                { "6", 21 }
            };

            foreach (string cid in clientIdsWithCorrectValues.Keys)
            {
                GridViewData testData = RapGen.AverageReqValueForClientId(cid);
                double testValue = Convert.ToDouble(testData.Rows[0][1]);

                if (testValue == clientIdsWithCorrectValues[cid])
                {
                    MessageBox.Show("Test passed for client \"" + cid + "\"");
                }
                else
                {
                    MessageBox.Show("Test failed for client \"" + cid + "\"\n" + testValue.ToString() + " != " + clientIdsWithCorrectValues[cid]);
                }
            }
        }
    }
}