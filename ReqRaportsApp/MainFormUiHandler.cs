using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ReqRaportsApp
{
    public partial class MainForm
    {
        private void UpdateMaxValue()
        {
            double maxPriceVal = RapGenOps.maxPrice();
            MaxMaxValLabel.Text = maxPriceVal.ToString();
            MinValueTextBox.Text = 0.ToString();
            MaxValueTextBox.Text = maxPriceVal.ToString();
        }

        private void AddFiles()
        {
            try
            {
                if (AddFilesDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] filePaths = AddFilesDialog.FileNames;

                    foreach (string fp in filePaths) if (!AddedFiles.Keys.Contains(fp.Substring(fp.LastIndexOf("\\") + 1)))
                        {
                            if (fp.EndsWith(".xml"))
                            {
                                Serializer.DeserializeXmlObject(fp);
                            }
                            else if (fp.EndsWith(".json"))
                            {
                                Serializer.DesarializeJsonObject(fp);
                            }
                            else if (fp.EndsWith(".csv"))
                            {
                                Serializer.DeserializeCsvObject(fp);
                            }

                            string fileName = fp.Substring(fp.LastIndexOf("\\") + 1);
                            if (AddedFiles.Keys.Contains(fileName))
                            {
                                AddedFilesListBox.Items.Add(fileName);
                            }

                            foreach (request r in AddedFiles[fileName])
                            {
                                string cid = r.clientId;
                                if (!ClientIdComboBox.Items.Contains(cid))
                                {
                                    ClientIdComboBox.Items.Add(cid);
                                }
                            }
                        }

                    if (AddedFilesListBox.Items.Count != 0)
                    {
                        RaportsComboBox.Enabled = true;
                        RaportGenBtn.Enabled = true;
                        DeleteFilesBtn.Enabled = true;
                    }

                    UpdateMaxValue();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteFiles()
        {
            try
            {
                List<object> toRemove = new List<object>();
                foreach (object item in AddedFilesListBox.SelectedItems)
                {
                    toRemove.Add(item);
                }

                List<string> clientIdsToCheckList = new List<string>();
                foreach (object item in toRemove)
                {
                    AddedFilesListBox.Items.Remove(item);

                    DataHandler.RemoveData(clientIdsToCheckList, item);
                }
                toRemove.Clear();

                HashSet<string> clientIdsToCheckSet = new HashSet<string>(clientIdsToCheckList);
                List<string> clientIdsToRemove = DataHandler.checkClientIds(clientIdsToCheckSet);
                foreach (string cid in clientIdsToRemove)
                {
                    ClientIdComboBox.Items.Remove(cid);
                }


                if (AddedFilesListBox.Items.Count == 0)
                {
                    RaportsComboBox.SelectedItem = RaportsComboBox.Items[0];
                    RaportsComboBox.Enabled = false;
                    ClientIdBox.Visible = false;
                    ValueRangeBox.Visible = false;
                    RaportGenBtn.Enabled = false;
                    DeleteFilesBtn.Enabled = false;
                    SaveRaportBtn.Enabled = false;

                    RaportsDataGrid.ColumnCount = 0;
                }

                UpdateMaxValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowClientIdsComboBox()
        {
            try
            {
                ClientIdBox.Visible = true;
                ClientIdComboBox.Visible = true;
                ClientIdLabel.Visible = true;

                HashSet<string> clientIds = DataHandler.getClientIds();

                List<object> toRemove = new List<object>();
                foreach (object item in ClientIdComboBox.Items) if (!clientIds.Contains(item.ToString()))
                    {
                        toRemove.Add(item);
                    }
                foreach (object tr in toRemove)
                {
                    ClientIdComboBox.Items.Remove(tr);
                }
                toRemove.Clear();

                foreach (string id in clientIds) if (!ClientIdComboBox.Items.Contains(id))
                    {
                        ClientIdComboBox.Items.Add(id);
                    }

                if (ClientIdComboBox.SelectedItem == null & ClientIdComboBox.Items.Count != 0)
                {
                    ClientIdComboBox.SelectedItem = ClientIdComboBox.Items[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RaportTypeChanged()
        {
            try
            {
                string selectedItem = RaportsComboBox.SelectedItem.ToString();
                if (RaportTypes.clientIdRaportsList.Contains(selectedItem))
                {
                    ValueRangeBox.Visible = false;
                    ShowClientIdsComboBox();
                }
                else if (selectedItem == RaportTypes.ReqsInValueRangeType)
                {
                    ClientIdBox.Visible = false;
                    ClientIdComboBox.Visible = false;
                    ClientIdLabel.Visible = false;
                    ValueRangeBox.Visible = true;

                    try
                    {
                        UpdateMaxValue();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    ClientIdBox.Visible = false;
                    ClientIdComboBox.Visible = false;
                    ClientIdLabel.Visible = false;
                    ValueRangeBox.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MinMaxValueValidate(bool isMin)
        {
            double maxMaxValue = Double.Parse(MaxMaxValLabel.Text);

            try
            {
                TextBox minMaxValueTextBox = new TextBox();
                TextBox otherTextBox = new TextBox();

                double defaultVal;
                if (isMin)
                {
                    defaultVal = 0;
                    minMaxValueTextBox = MinValueTextBox;
                    otherTextBox = MaxValueTextBox;
                }
                else
                {
                    defaultVal = maxMaxValue;
                    minMaxValueTextBox = MaxValueTextBox;
                    otherTextBox = MinValueTextBox;
                }

                double minMaxValue = Double.Parse(minMaxValueTextBox.Text);

                if (minMaxValue < 0)
                {
                    minMaxValueTextBox.Text = defaultVal.ToString();
                }
                else if (minMaxValue > maxMaxValue)
                {
                    minMaxValueTextBox.Text = defaultVal.ToString();
                }
                else if (otherTextBox.Text != "" & otherTextBox.Text != null)
                {
                    double otherValue = Double.Parse(otherTextBox.Text);

                    if (isMin & minMaxValue >= otherValue)
                    {
                        minMaxValueTextBox.Text = defaultVal.ToString();
                    }
                    else if (!isMin & minMaxValue < otherValue)
                    {
                        minMaxValueTextBox.Text = defaultVal.ToString();
                    }
                }
            }
            catch
            {
                if (isMin)
                {
                    MinValueTextBox.Text = 0.ToString();
                }
                else
                {
                    MaxValueTextBox.Text = maxMaxValue.ToString();
                }
            }
        }

        private GridViewData RaportChoiceSwitch()
        {
            try
            {
                string raportType = RaportsComboBox.SelectedItem.ToString();

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                GridViewData gridViewData = new GridViewData(cN, rW);

                if (RaportTypes.clientIdRaportsList.Contains(raportType))
                {
                    string currentClientId = ClientIdComboBox.SelectedItem.ToString();

                    switch (raportType)
                    {
                        case RaportTypes.ReqQuantForClientType:
                            gridViewData = RapGens.ReqQuantForClient(currentClientId);
                            break;

                        case RaportTypes.ReqValueSumForClientType:
                            gridViewData = RapGens.ReqValueSumForClientId(currentClientId);
                            break;

                        case RaportTypes.AllReqsListForClientType:
                            gridViewData = RapGens.ReqsListForClientId(currentClientId);
                            break;

                        case RaportTypes.AverageReqValueForClientType:
                            gridViewData = RapGens.AverageReqValueForClientId(currentClientId);
                            break;

                        case RaportTypes.ReqQuantByProdNameForClientType:
                            gridViewData = RapGens.ReqQuantByNameForClientId(currentClientId);
                            break;
                    }
                }
                else
                {
                    switch (raportType)
                    {
                        case RaportTypes.ReqQuantType:
                            gridViewData = RapGens.ReqQuant();
                            break;

                        case RaportTypes.ReqValueSumType:
                            gridViewData = RapGens.ReqValueSum();
                            break;

                        case RaportTypes.AllReqsListType:
                            gridViewData = RapGens.AllReqsList();
                            break;

                        case RaportTypes.AverageReqValueType:
                            gridViewData = RapGens.AverageReqValue();
                            break;

                        case RaportTypes.ReqQuantByProdNameType:
                            gridViewData = RapGens.ReqQuantByName();
                            break;

                        case RaportTypes.ReqsInValueRangeType:
                            double minValue = Double.Parse(MinValueTextBox.Text);
                            double maxValue = Double.Parse(MaxValueTextBox.Text);

                            gridViewData = RapGens.ReqsForValueRange(minValue, maxValue);
                            break;
                    }
                }
                return gridViewData;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                string[] cN = { };
                List<List<object>> rW = new List<List<object>>();
                GridViewData gridViewData = new GridViewData(cN, rW);
                return gridViewData;
            }
            
        }

        private void GridViewPopulate(string[] colNames, List<List<object>> rows)
        {
            RaportsDataGrid.Rows.Clear();

            RaportsDataGrid.ColumnCount = colNames.Count();

            int i = 0;
            foreach (string cn in colNames)
            {
                RaportsDataGrid.Columns[i].Name = cn;
                i++;
            }

            int rowCount = 0;
            foreach (List<object> row in rows)
            {
                RaportsDataGrid.Rows.Add();

                int cellCount = 0;
                foreach (object cellValue in row)
                {
                    RaportsDataGrid.Rows[rowCount].Cells[cellCount].ValueType = cellValue.GetType();
                    RaportsDataGrid.Rows[rowCount].Cells[cellCount].Value = cellValue;

                    cellCount++;
                }

                rowCount++;
            }

            if (RaportsDataGrid.ColumnCount != 0)
            {
                SaveRaportBtn.Enabled = true;
            }
        }

        private void GenerateRaport()
        {
            GridViewData gridViewData = RaportChoiceSwitch();
            GridViewPopulate(gridViewData.ColNames, gridViewData.Rows);
        }

        private void SaveRaportToCsv()
        {
            string selectedItem = RaportsComboBox.SelectedItem.ToString();
            string raportName = "raport_" + Regex.Replace(selectedItem, " ", "-") + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            SaveRaportDialog.FileName = raportName;

            if (SaveRaportDialog.ShowDialog() == DialogResult.OK)
            {
                List<List<string>> RowsList = new List<List<string>>();

                List<string> rowText = new List<string>();
                for (int col = 0; col < RaportsDataGrid.ColumnCount; col++)
                {
                    rowText.Add(RaportsDataGrid.Columns[col].Name);
                }
                RowsList.Add(rowText);

                for (int row = 0; row < RaportsDataGrid.RowCount; row++)
                {
                    rowText = new List<string>();
                    for (int cell = 0; cell < RaportsDataGrid.ColumnCount; cell++)
                    {
                        rowText.Add(RaportsDataGrid.Rows[row].Cells[cell].Value.ToString());
                    }
                    RowsList.Add(rowText);
                }

                DataHandler.ExportGridDataToFile(RowsList, SaveRaportDialog.FileName);

                SaveRaportDialog.FileName = string.Empty;
            }
        }
    }
}
