using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public partial class Form1 : Form
    {
        string[] dropListItemsList =
        {
            "Ilość zamówień",
            "Ilość zamówień dla klienta o wskazanym identyfikatorze",
            "Łączna kwota zamówień",
            "Łączna kwota zamówień dla klienta o wskazanym identyfikatorze",
            "Lista wszystkich zamówień",
            "Lista zamówień dla klienta o wskazanym identyfikatorze",
            "Średnia wartość zamówienia",
            "Średnia wartość zamówienia dla klienta o wskazanym identyfikatorze",
            "Ilość zamówień pogrupowanych po nazwie",
            "Ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym identyfikatorze",
            "Zamówienia w podanym przedziale cenowym"
        };
        string[] clientIdRaportsList =
        {
            "Ilość zamówień dla klienta o wskazanym identyfikatorze",
            "Łączna kwota zamówień dla klienta o wskazanym identyfikatorze",
            "Lista zamówień dla klienta o wskazanym identyfikatorze",
            "Średnia wartość zamówienia dla klienta o wskazanym identyfikatorze",
            "Ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym identyfikatorze",
        };
        Dictionary<string, List<request>> AddedFiles = new Dictionary<string, List<request>>();
        public List<request> RequestsList = new List<request>();
        
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

            foreach (string n in dropListItemsList)
            {
                this.raportsComboBox.Items.Add(n);
                this.raportsComboBox.SelectedItem = this.raportsComboBox.Items[0];
            }
        }

        public void AddFilesBtn_Click(object sender, EventArgs e)
        {
            if (addFilesDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = addFilesDialog.FileNames;
                
                foreach (string fp in filePaths) if (!AddedFiles.Keys.Contains(fp))
                {                    
                    if (fp.EndsWith(".xml"))
                    {
                        MyXmlSerializer.DeserializeXmlObject(fp, RequestsList, AddedFiles);
                    }
                    else if (fp.EndsWith(".json"))
                    {
                        MyJsonSerializer.DesarializeJsonObject(fp, RequestsList, AddedFiles);
                    }
                    else if (fp.EndsWith(".csv"))
                    {
                        MyCsvSerializer.DeserializeCsvObject(fp, RequestsList, AddedFiles);
                    }

                    string fileName = fp.Substring(fp.LastIndexOf("\\") + 1);
                    if (AddedFiles.Keys.Contains(fileName))
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
        
        public void raportsComboBoxSelectionChange(object sender, EventArgs e)
        {
            string selectedItem = raportsComboBox.SelectedItem.ToString();
            if (clientIdRaportsList.Contains(selectedItem))
            {
                valueRangeBox.Visible = false;
                showClientIdsComboBox();
            }
            else if (selectedItem == dropListItemsList[dropListItemsList.Length - 1])
            {
                clientIdBox.Visible = false;
                clientIdComboBox.Visible = false;
                clientIdLabel.Visible = false;
                valueRangeBox.Visible = true;
                
                try
                {
                    double maxPriceVal = maxPrice();
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

        public void minValueTextBox_Leave(object sender, EventArgs e)
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

        public void maxValueTextBox_Leave(object sender, EventArgs e)
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

        public void raportGenBtn_Click(object sender, EventArgs e)
        {
            RaportChoiceSwitch();

            if (raportsDataGrid.ColumnCount != 0)
            {
                saveRaportBtn.Enabled = true;
            }
        }

        private void saveRaportBtn_Click(object sender, EventArgs e)
        {
            if (saveRaportDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> dataToWrite = GatherGridDataToCsv();

                using (StreamWriter outputFile = new StreamWriter(saveRaportDialog.FileName))
                {
                    foreach (string line in dataToWrite)
                    {
                        outputFile.WriteLine(line);
                    }
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

                    foreach (request r in AddedFiles[item.ToString()])
                    {
                        RequestsList.Remove(r);
                    }

                    AddedFiles.Remove(item.ToString());
                }
                toRemove.Clear();

                if (addedFilesListView.Items.Count == 0)
                {
                    raportsComboBox.Enabled = false;
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
    }
}
