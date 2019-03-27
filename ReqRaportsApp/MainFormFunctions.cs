using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public class MainFormFunctions
    {
        public static void AddFiles(OpenFileDialog addFilesDialog, ListBox addedFilesListBox, ComboBox raportsComboBox, Button raportGenBtn, Button deleteFilesBtn)
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
                            addedFilesListBox.Items.Add(fileName);
                        }
                    }

                if (addedFilesListBox.Items.Count != 0)
                {
                    raportsComboBox.Enabled = true;
                    raportGenBtn.Enabled = true;
                    deleteFilesBtn.Enabled = true;
                }
            }
        }

        public static void DeleteFiles(ListBox addedFilesListBox, ComboBox raportsComboBox, GroupBox clientIdBox, GroupBox valueRangeBox, 
                                        Button raportGenBtn, Button deleteFilesBtn, Button saveRaportBtn, DataGridView raportsDataGrid)
        {
            try
            {
                List<object> toRemove = new List<object>();
                foreach (object item in addedFilesListBox.SelectedItems)
                {
                    toRemove.Add(item);
                }
                foreach (object item in toRemove)
                {
                    addedFilesListBox.Items.Remove(item);

                    foreach (request r in RequestList.AddedFiles[item.ToString()])
                    {
                        RequestList.ReqsList.Remove(r);
                    }

                    RequestList.AddedFiles.Remove(item.ToString());
                }
                toRemove.Clear();

                if (addedFilesListBox.Items.Count == 0)
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

        private static HashSet<string> getClientIds()
        {
            IEnumerable<string> getClientIdsQuery = from request in RequestList.ReqsList
                                                    select request.clientId;

            HashSet<string> clientIds = new HashSet<string>(getClientIdsQuery.ToList());

            return clientIds;
        }

        private static void showClientIdsComboBox(GroupBox clientIdBox, ComboBox clientIdComboBox, Label clientIdLabel)
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

        public static void RaportTypeChanged(ComboBox raportsComboBox, GroupBox valueRangeBox, GroupBox clientIdBox, ComboBox clientIdComboBox, 
                                                Label clientIdLabel, Label maxMaxValLabel, TextBox minValueTextBox, TextBox maxValueTextBox)
        {
            string selectedItem = raportsComboBox.SelectedItem.ToString();
            if (RaportTypes.clientIdRaportsList.Contains(selectedItem))
            {
                valueRangeBox.Visible = false;
                showClientIdsComboBox(clientIdBox, clientIdComboBox, clientIdLabel);
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
                    maxValueTextBox.Text = Math.Round(maxPriceVal, 2, MidpointRounding.AwayFromZero).ToString();
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

        public static void MinMaxValueValidate(TextBox minMaxValueTextBox, TextBox otherTextBox, Label maxMaxValLabel, bool isMin)
        {
            try
            {
                double minMaxValue = Double.Parse(minMaxValueTextBox.Text);
                double maxMaxValue = Double.Parse(maxMaxValLabel.Text);

                double defaultVal;
                if (isMin)
                {
                    defaultVal = 0;
                }
                else
                {
                    defaultVal = maxMaxValue;
                }

                if (minMaxValue < 0)
                {
                    minMaxValueTextBox.Text = defaultVal.ToString();
                }
                else if (minMaxValue > Math.Round(maxMaxValue))
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
                minMaxValueTextBox.Text = null;
            }
        }
    }
}
