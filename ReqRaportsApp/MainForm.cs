using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Windows.Forms;

namespace ReqRaportsApp
{
    public partial class MainForm : Form
    {
        MainFormOperator mainFormOperator { get; set; }

        public MainForm()
        {
            InitializeComponent();

            mainFormOperator = new MainFormOperator(AddFilesDialog, AddedFilesListBox, RaportsComboBox, ClientIdComboBox, RaportGenBtn,
                                                                    DeleteFilesBtn, MaxMaxValLabel, MinValueTextBox, MaxValueTextBox, ClientIdBox, ValueRangeBox,
                                                                    SaveRaportBtn, RaportsDataGrid, ClientIdLabel, SaveRaportDialog);

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
            mainFormOperator.AddFiles();
        }

        private void DeleteFilesBtn_Click(object sender, EventArgs e)
        {
            mainFormOperator.DeleteFiles();
        }

        private void RaportsComboBoxSelectionChange(object sender, EventArgs e)
        {
            mainFormOperator.RaportTypeChanged();
        }

        private void MinValueTextBox_Leave(object sender, EventArgs e)
        {
            mainFormOperator.MinMaxValueValidate(true);
        }

        private void MaxValueTextBox_Leave(object sender, EventArgs e)
        {
            mainFormOperator.MinMaxValueValidate(false);
        }

        private void RaportGenBtn_Click(object sender, EventArgs e)
        {
            mainFormOperator.RaportChoiceChanged();
        }

        private void SaveRaportBtn_Click(object sender, EventArgs e)
        {
            mainFormOperator.SaveRaportToCsv();
        }
    }
}
