using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public class MainFormOperator
    {
        OpenFileDialog AddFilesDialog { get; set; }
        ListBox AddedFilesListBox { get; set; }
        ComboBox RaportsComboBox { get; set; }
        ComboBox ClientIdComboBox { get; set; }
        Button RaportGenBtn { get; set; }
        Button DeleteFilesBtn { get; set; }
        Label MaxMaxValLabel { get; set; }
        TextBox MinValueTextBox { get; set; }
        TextBox MaxValueTextBox { get; set; }
        GroupBox ClientIdBox { get; set; }
        GroupBox ValueRangeBox { get; set; }
        Button SaveRaportBtn { get; set; }
        DataGridView RaportsDataGrid { get; set; }
        Label ClientIdLabel { get; set; }
        SaveFileDialog SaveRaportDialog { get; set; }

        List<request> ReqsList { get; set; }
        Dictionary<string, List<request>> AddedFiles { get; set; }

        Deserializer Deserial { get; set; }
        GridViewHandler GridViewHand { get; set; }
        RapGenOperations RapGenOps { get; set; }
        RaportGenerators RapGens { get; set; }

        public MainFormOperator(OpenFileDialog afDialog, ListBox afListBox, ComboBox rCombo, ComboBox cidCombo, Button rgBtn, Button dfBtn,
                                Label maxmLabel, TextBox minvTextBox, TextBox maxvTextBox, GroupBox cidBox, GroupBox vrBox, Button srBtn,
                                DataGridView rDataGrid, Label cidLabel, SaveFileDialog srDialog)
        {
            AddFilesDialog = afDialog;
            AddedFilesListBox = afListBox;
            RaportsComboBox = rCombo;
            ClientIdComboBox = cidCombo;
            RaportGenBtn = rgBtn;
            DeleteFilesBtn = dfBtn;
            MaxMaxValLabel = maxmLabel;
            MinValueTextBox = minvTextBox;
            MaxValueTextBox = maxvTextBox;
            ClientIdBox = cidBox;
            ValueRangeBox = vrBox;
            SaveRaportBtn = srBtn;
            RaportsDataGrid = rDataGrid;
            ClientIdLabel = cidLabel;
            SaveRaportDialog = srDialog;

            ReqsList = new List<request>();
            AddedFiles = new Dictionary<string, List<request>>();

            Deserial = new Deserializer(ReqsList, AddedFiles);
            GridViewHand = new GridViewHandler(rDataGrid);
            RapGenOps = new RapGenOperations(ReqsList);
            RapGens = new RaportGenerators(GridViewHand, RapGenOps, ReqsList, rCombo, cidCombo, minvTextBox, maxvTextBox, rDataGrid);
        }

        public void AddFiles()
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
                                Deserial.DeserializeXmlObject(fp);
                            }
                            else if (fp.EndsWith(".json"))
                            {
                                Deserial.DesarializeJsonObject(fp);
                            }
                            else if (fp.EndsWith(".csv"))
                            {
                                Deserial.DeserializeCsvObject(fp);
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

                    double maxPriceVal = RapGenOps.maxPrice();
                    MaxMaxValLabel.Text = maxPriceVal.ToString();
                    MinValueTextBox.Text = 0.ToString();
                    MaxValueTextBox.Text = maxPriceVal.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteFiles()
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

                    foreach (request r in AddedFiles[item.ToString()])
                    {
                        ReqsList.Remove(r);
                        clientIdsToCheckList.Add(r.clientId);
                    }

                    AddedFiles.Remove(item.ToString());
                }
                toRemove.Clear();

                HashSet<string> clientIdsToCheckSet = new HashSet<string>(clientIdsToCheckList);
                foreach (string cid in clientIdsToCheckSet)
                {
                    IEnumerable<string> getClientIdsToRemove = from req in ReqsList
                                                               where req.clientId == cid
                                                               select req.clientId;

                    if (getClientIdsToRemove.Count() == 0)
                    {
                        ClientIdComboBox.Items.Remove(cid);
                    }
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

                double maxPriceVal = RapGenOps.maxPrice();
                MaxMaxValLabel.Text = maxPriceVal.ToString();
                MinValueTextBox.Text = 0.ToString();
                MaxValueTextBox.Text = maxPriceVal.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private HashSet<string> getClientIds()
        {
            IEnumerable<string> getClientIdsQuery = from request in ReqsList
                                                    select request.clientId;

            HashSet<string> clientIds = new HashSet<string>(getClientIdsQuery.ToList());

            return clientIds;
        }

        private void showClientIdsComboBox()
        {
            try
            {
                ClientIdBox.Visible = true;
                ClientIdComboBox.Visible = true;
                ClientIdLabel.Visible = true;

                HashSet<string> clientIds = getClientIds();

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
                    showClientIdsComboBox();
                }
                else if (selectedItem == RaportTypes.ReqsInValueRangeType)
                {
                    ClientIdBox.Visible = false;
                    ClientIdComboBox.Visible = false;
                    ClientIdLabel.Visible = false;
                    ValueRangeBox.Visible = true;

                    try
                    {
                        double maxPriceVal = RapGenOps.maxPrice();
                        MaxMaxValLabel.Text = maxPriceVal.ToString();
                        MinValueTextBox.Text = 0.ToString();
                        MaxValueTextBox.Text = maxPriceVal.ToString();
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
            try
            {
                TextBox minMaxValueTextBox = new TextBox();
                TextBox otherTextBox = new TextBox();

                double maxMaxValue = Double.Parse(MaxMaxValLabel.Text);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (isMin)
                {
                    MinValueTextBox.Text = null;
                }
                else
                {
                    MaxValueTextBox.Text = null;
                }
            }
        }

        public GridViewData GenerateRaport()
        {
            GridViewData gridViewData = RapGens.RaportChoiceSwitch();
            return gridViewData;
        }

        public void SaveRaportToCsv()
        {
            if (SaveRaportDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> dataToWrite = GridViewHand.GatherGridDataToCsv();

                using (StreamWriter outputFile = new StreamWriter(SaveRaportDialog.FileName))
                {
                    foreach (string line in dataToWrite)
                    {
                        outputFile.WriteLine(line);
                    }
                }
            }
        }
    }
}
