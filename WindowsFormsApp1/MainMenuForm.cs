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
                        MyXmlSerializer.DeserializeXmlObject(fp, RequestsList);
                    }
                    else if (fp.EndsWith(".json"))
                    {
                        MyJsonSerializer.DesarializeJsonObject(fp, RequestsList);
                    }
                    else if (fp.EndsWith(".csv"))
                    {
                        MyCsvSerializer.DeserializeCsvObject(fp, RequestsList);
                    }

                    AddedFiles.Add(fp);
                }
            }
        }
        
        public void raportsComboBoxSelectionChange(object sender, EventArgs e)
        {
            string selectedItem = raportsComboBox.SelectedItem.ToString();
            if (clientIdRaportsList.Contains(selectedItem))
            {
                showClientIdsComboBox();
            }
            else
            {
                clientIdComboBox.Visible = false;
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
