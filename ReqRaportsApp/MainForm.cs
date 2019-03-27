using System;
using System.Collections.Generic;
using System.IO;
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
            MainFormFunctions.AddFiles(addFilesDialog, addedFilesListBox, raportsComboBox, clientIdComboBox, raportGenBtn, deleteFilesBtn, maxMaxValLabel, minValueTextBox, maxValueTextBox);
        }

        private void deleteFilesBtn_Click(object sender, EventArgs e)
        {
            MainFormFunctions.DeleteFiles(addedFilesListBox, raportsComboBox, clientIdBox, clientIdComboBox, valueRangeBox, raportGenBtn, deleteFilesBtn, saveRaportBtn, raportsDataGrid, maxMaxValLabel, minValueTextBox, maxValueTextBox);
        }

        private void raportsComboBoxSelectionChange(object sender, EventArgs e)
        {
            MainFormFunctions.RaportTypeChanged(raportsComboBox, valueRangeBox, clientIdBox, clientIdComboBox, clientIdLabel, maxMaxValLabel, minValueTextBox, maxValueTextBox);
        }

        private void minValueTextBox_Leave(object sender, EventArgs e)
        {
            MainFormFunctions.MinMaxValueValidate(minValueTextBox, maxValueTextBox, maxMaxValLabel, true);
        }

        private void maxValueTextBox_Leave(object sender, EventArgs e)
        {
            MainFormFunctions.MinMaxValueValidate(maxValueTextBox, minValueTextBox, maxMaxValLabel, false);
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
