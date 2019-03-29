using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ReqRaportsApp
{
    public partial class MainForm : Form
    {
        DataHandler DataHandler { get; set; }
        RaportGenerators RapGens { get; set; }
        RapGenOperations RapGenOps { get; set; }
        Serializers Serializer { get; set; }

        List<request> ReqsList { get; set; }
        Dictionary<string, List<request>> AddedFiles { get; set; }


        public MainForm()
        {
            InitializeComponent();

            ReqsList = new List<request>();
            AddedFiles = new Dictionary<string, List<request>>();

            Serializer = new Serializers(ReqsList, AddedFiles);
            RapGenOps = new RapGenOperations(ReqsList);
            RapGens = new RaportGenerators(RapGenOps, ReqsList);
            DataHandler = new DataHandler(AddedFiles, ReqsList, Serializer);
            
            ClientIdBox.Visible = false;
            ClientIdComboBox.Visible = false;
            ClientIdLabel.Visible = false;
            ValueRangeBox.Visible = false;

            RaportsComboBox.Enabled = false;
            RaportGenBtn.Enabled = false;
            DeleteFilesBtn.Enabled = false;
            SaveRaportBtn.Enabled = false;

            RaportsDataGrid.AllowUserToAddRows = false;
            RaportsDataGrid.AllowUserToDeleteRows = false;
            RaportsDataGrid.AllowUserToOrderColumns = false;
            RaportsDataGrid.AllowUserToResizeColumns = false;
            RaportsDataGrid.AllowUserToResizeRows = false;
            RaportsDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            RaportsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            string addFilesInitialDir = Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
            {
                addFilesInitialDir = addFilesInitialDir.Substring(0, addFilesInitialDir.LastIndexOf('\\'));
            }
            addFilesInitialDir += "\\SampleData";
            AddFilesDialog.InitialDirectory = addFilesInitialDir;
            
            SaveRaportDialog.InitialDirectory = SpecialDirectories.MyDocuments;

            foreach (string n in RaportTypes.dropListItemsList)
            {
                this.RaportsComboBox.Items.Add(n);
                this.RaportsComboBox.SelectedItem = this.RaportsComboBox.Items[0];
            }
        }

        private void AddFilesBtn_Click(object sender, EventArgs e)
        {
            AddFiles();
        }

        private void DeleteFilesBtn_Click(object sender, EventArgs e)
        {
            DeleteFiles();
        }

        private void RaportsComboBoxSelectionChange(object sender, EventArgs e)
        {
            RaportTypeChanged();
        }

        private void MinValueTextBox_Leave(object sender, EventArgs e)
        {
            MinMaxValueValidate(true);
        }

        private void MaxValueTextBox_Leave(object sender, EventArgs e)
        {
            MinMaxValueValidate(false);
        }

        private void RaportGenBtn_Click(object sender, EventArgs e)
        {
            GenerateRaport();
        }

        private void SaveRaportBtn_Click(object sender, EventArgs e)
        {
            SaveRaportToCsv();
        }
    }
}
