using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
            clientIdBox.Visible = false;
            clientIdComboBox.Visible = false;
            clientIdLabel.Visible = false;
            valueRangeBox.Visible = false;

            raportsComboBox.Enabled = false;
            raportGenBtn.Enabled = false;
            deleteFilesBtn.Enabled = false;
            saveRaportBtn.Enabled = false;

            raportsDataGrid.AllowUserToAddRows = false;
            raportsDataGrid.AllowUserToDeleteRows = false;
            raportsDataGrid.AllowUserToOrderColumns = false;
            raportsDataGrid.AllowUserToResizeColumns = false;
            raportsDataGrid.AllowUserToResizeRows = false;
            raportsDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            raportsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (string n in RaportTypes.dropListItemsList)
            {
                this.raportsComboBox.Items.Add(n);
                this.raportsComboBox.SelectedItem = this.raportsComboBox.Items[0];
            }
        }

        private void AddFilesBtn_Click(object sender, EventArgs e)
        {
            if (addFilesDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = addFilesDialog.FileNames;
                
                foreach (string fp in filePaths) if (!RequestList.AddedFiles.Keys.Contains(fp))
                {                    
                    if (fp.EndsWith(".xml"))
                    {
                        MyXmlSerializer.DeserializeXmlObject(fp, RequestList.ReqsList, RequestList.AddedFiles);
                    }
                    else if (fp.EndsWith(".json"))
                    {
                        MyJsonSerializer.DesarializeJsonObject(fp, RequestList.ReqsList, RequestList.AddedFiles);
                    }
                    else if (fp.EndsWith(".csv"))
                    {
                        MyCsvSerializer.DeserializeCsvObject(fp, RequestList.ReqsList, RequestList.AddedFiles);
                    }

                    string fileName = fp.Substring(fp.LastIndexOf("\\") + 1);
                    if (RequestList.AddedFiles.Keys.Contains(fileName))
                    {
                        addedFilesListView.Items.Add(fileName);
                    }
                }

                if (addedFilesListView.Items.Count != 0)
                {
                    raportsComboBox.Enabled = true;
                    raportGenBtn.Enabled = true;
                    deleteFilesBtn.Enabled = true;
                }
            }
        }

        private void deleteFilesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> toRemove = new List<object>();
                foreach (object item in addedFilesListView.SelectedItems)
                {
                    toRemove.Add(item);
                }
                foreach (object item in toRemove)
                {
                    addedFilesListView.Items.Remove(item);

                    foreach (request r in RequestList.AddedFiles[item.ToString()])
                    {
                        RequestList.ReqsList.Remove(r);
                    }

                    RequestList.AddedFiles.Remove(item.ToString());
                }
                toRemove.Clear();

                if (addedFilesListView.Items.Count == 0)
                {
                    raportsComboBox.SelectedItem = raportsComboBox.Items[0];
                    raportsComboBox.Enabled = false;
                    clientIdBox.Visible = false;
                    valueRangeBox.Visible = false;
                    raportGenBtn.Enabled = false;
                    deleteFilesBtn.Enabled = false;
                    saveRaportBtn.Enabled = false;

                    raportsDataGrid.ColumnCount = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void raportsComboBoxSelectionChange(object sender, EventArgs e)
        {
            string selectedItem = raportsComboBox.SelectedItem.ToString();
            if (RaportTypes.clientIdRaportsList.Contains(selectedItem))
            {
                valueRangeBox.Visible = false;
                showClientIdsComboBox();
            }
            else if (selectedItem == RaportTypes.dropListItemsList[RaportTypes.dropListItemsList.Length - 1])
            {
                clientIdBox.Visible = false;
                clientIdComboBox.Visible = false;
                clientIdLabel.Visible = false;
                valueRangeBox.Visible = true;

                try
                {
                    double maxPriceVal = RapGenOperations.maxPrice();
                    maxMaxValLabel.Text = maxPriceVal.ToString();
                    minValueTextBox.Text = 0.ToString();
                    maxValueTextBox.Text = maxPriceVal.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                clientIdBox.Visible = false;
                clientIdComboBox.Visible = false;
                clientIdLabel.Visible = false;
                valueRangeBox.Visible = false;
            }
        }

        private static HashSet<string> getClientIds()
        {
            IEnumerable<string> getClientIdsQuery = from request in RequestList.ReqsList
                                                    select request.clientId;

            HashSet<string> clientIds = new HashSet<string>(getClientIdsQuery.ToList());

            return clientIds;
        }

        private void showClientIdsComboBox()
        {
            clientIdBox.Visible = true;
            clientIdComboBox.Visible = true;
            clientIdLabel.Visible = true;

            HashSet<string> clientIds = getClientIds();

            List<object> toRemove = new List<object>();
            foreach (object item in clientIdComboBox.Items) if (!clientIds.Contains(item.ToString()))
            {
                toRemove.Add(item);
            }
            foreach (object tr in toRemove)
            {
                clientIdComboBox.Items.Remove(tr);
            }
            toRemove.Clear();

            foreach (string id in clientIds) if (!clientIdComboBox.Items.Contains(id))
            {
                clientIdComboBox.Items.Add(id);
            }

            if (clientIdComboBox.SelectedItem == null & clientIdComboBox.Items.Count != 0)
            {
                clientIdComboBox.SelectedItem = clientIdComboBox.Items[0];
            }
        }

        private void minValueTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double minValue = Double.Parse(minValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double maxMaxValue = Double.Parse(maxMaxValLabel.Text, System.Globalization.CultureInfo.InvariantCulture);

                if (minValue < 0)
                {
                    minValueTextBox.Text = 0.ToString();
                }
                else if (minValue >= maxMaxValue)
                {
                    minValueTextBox.Text = 0.ToString();
                }
                else if (maxValueTextBox.Text != "" & maxValueTextBox.Text != null) 
                {
                    double maxValue = Double.Parse(maxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                    if (minValue >= maxValue)
                    {
                        minValueTextBox.Text = 0.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                minValueTextBox.Text = null;
            }
        }

        private void maxValueTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                double maxValue = Double.Parse(maxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double maxMaxValue = Double.Parse(maxMaxValLabel.Text, System.Globalization.CultureInfo.InvariantCulture);

                if (maxValue < 0)
                {
                    maxValueTextBox.Text = maxMaxValue.ToString();
                }
                else if (maxValue > maxMaxValue)
                {
                    maxValueTextBox.Text = maxMaxValue.ToString();
                }
                else if(minValueTextBox.Text != "" & minValueTextBox.Text != null)
                {
                    double minValue = Double.Parse(minValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                    if (minValue >= maxValue)
                    {
                        maxValueTextBox.Text = maxMaxValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                maxValueTextBox.Text = null;
            }
        }

        private void raportGenBtn_Click(object sender, EventArgs e)
        {
            RaportGenerators.RaportChoiceSwitch(raportsComboBox, raportsDataGrid, clientIdComboBox, minValueTextBox, maxValueTextBox);

            if (raportsDataGrid.ColumnCount != 0)
            {
                saveRaportBtn.Enabled = true;
            }
        }

        private void saveRaportBtn_Click(object sender, EventArgs e)
        {
            if (saveRaportDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> dataToWrite = GridViewHandler.GatherGridDataToCsv(raportsDataGrid);

                using (StreamWriter outputFile = new StreamWriter(saveRaportDialog.FileName))
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
