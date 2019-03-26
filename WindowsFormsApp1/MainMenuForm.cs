using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

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
        List<string> AddedFiles = new List<string>();

        public List<request> RequestsList = new List<request>();
        
        public Form1()
        {
            InitializeComponent();
            clientIdBox.Visible = false;
            clientIdComboBox.Visible = false;
            clientIdLabel.Visible = false;
            valueRangeBox.Visible = false;

            foreach (string n in dropListItemsList)
            {
                this.raportsComboBox.Items.Add(n);
                this.raportsComboBox.SelectedItem = this.raportsComboBox.Items[0];
            }
        }

        public void AddFilesBtn_Click(object sender, EventArgs e)
        {
            List<string> fileContents = new List<string>();

            if (this.addFilesDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = addFilesDialog.FileNames;
                
                foreach (string fp in filePaths) if (!AddedFiles.Contains(fp))
                {
                    string fileName = fp.Substring(fp.LastIndexOf("\\") + 1);
                    this.addedFilesListView.Items.Add(fileName);
                    
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
                    MessageBox.Show("Wartość nie może być mniejsza od zera");
                    minValueTextBox.Text = null;
                }
                else if (minValue >= maxMaxValue)
                {
                    MessageBox.Show("Wartość nie może być większa od najwyższej ceny zamówienia (" + maxMaxValue.ToString() + ")");
                    minValueTextBox.Text = null;
                }
                else if (maxValueTextBox.Text != "" & maxValueTextBox.Text != null) 
                {
                    double maxValue = Double.Parse(maxValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                    if (minValue >= maxValue)
                    {
                        MessageBox.Show("Wartość minimalna nie może być większa od maksymalnej");
                        minValueTextBox.Text = null;
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
                    MessageBox.Show("Wartość nie może być mniejsza od zera");
                    maxValueTextBox.Text = null;
                }
                else if (maxValue > maxMaxValue)
                {
                    MessageBox.Show("Wartość nie może być większa od najwyższej ceny zamówienia (" + maxMaxValue.ToString() + ")");
                    maxValueTextBox.Text = null;
                }
                else if(minValueTextBox.Text != "" & minValueTextBox.Text != null)
                {
                    double minValue = Double.Parse(minValueTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

                    if (minValue >= maxValue)
                    {
                        MessageBox.Show("Wartość maksymalna nie może być mniejsza od minimalnej");
                        maxValueTextBox.Text = null;
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
            string raportType = this.raportsComboBox.SelectedItem.ToString();

            if (raportType == dropListItemsList[0])
            {
                ReqQuant();
            }
            else if (raportType == dropListItemsList[1])
            {
                ReqQuantForClient();
            }
            else if (raportType == dropListItemsList[2])
            {
                ReqValueSum();
            }
            else if (raportType == dropListItemsList[3])
            {
                ReqValueSumForClientId();
            }
            else if (raportType == dropListItemsList[4])
            {
                AllReqsList();
            }
            else if (raportType == dropListItemsList[5])
            {
                ReqsListForClientId();
            }
            else if (raportType == dropListItemsList[6])
            {
                AverageReqValue();
            }
            else if (raportType == dropListItemsList[7])
            {
                AverageReqValueForClientId();
            }
            else if (raportType == dropListItemsList[8])
            {
                ReqQuantByName();
            }
            else if (raportType == dropListItemsList[9])
            {
                ReqQuantByNameForClientId();
            }
            else if (raportType == dropListItemsList[10])
            {
                ReqsForValueRange();
            }
        }
    }
}
