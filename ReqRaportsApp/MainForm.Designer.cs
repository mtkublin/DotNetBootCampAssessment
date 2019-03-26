namespace ReqRaportsApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.deleteFilesBtn = new System.Windows.Forms.Button();
            this.addedFilesListView = new System.Windows.Forms.ListBox();
            this.AddFilesBtn = new System.Windows.Forms.Button();
            this.saveRaportBtn = new System.Windows.Forms.Button();
            this.raportsDataGrid = new System.Windows.Forms.DataGridView();
            this.valueRangeBox = new System.Windows.Forms.GroupBox();
            this.maxMaxValLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maxMaxValue = new System.Windows.Forms.Label();
            this.maxValueTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.minValueTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clientIdBox = new System.Windows.Forms.GroupBox();
            this.clientIdLabel = new System.Windows.Forms.Label();
            this.clientIdComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.raportsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.raportGenBtn = new System.Windows.Forms.Button();
            this.saveRaportDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.raportsDataGrid)).BeginInit();
            this.valueRangeBox.SuspendLayout();
            this.clientIdBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addFilesDialog
            // 
            this.addFilesDialog.FileName = "openFileDialog1";
            this.addFilesDialog.Filter = "xml, csv and json files (*.xml, *.csv, *.json)|*.csv; *.json; *.xml|xml files (*." +
    "xml)|*.xml|csv files (*.csv)|*.csv|json files (*.json)|*.json";
            this.addFilesDialog.InitialDirectory = "C:\\Users\\Użytkownik\\source\\repos\\WindowsFormsApp1\\SampleData";
            this.addFilesDialog.Multiselect = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.deleteFilesBtn);
            this.splitContainer1.Panel1.Controls.Add(this.addedFilesListView);
            this.splitContainer1.Panel1.Controls.Add(this.AddFilesBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.saveRaportBtn);
            this.splitContainer1.Panel2.Controls.Add(this.raportsDataGrid);
            this.splitContainer1.Panel2.Controls.Add(this.valueRangeBox);
            this.splitContainer1.Panel2.Controls.Add(this.clientIdBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.raportGenBtn);
            this.splitContainer1.Size = new System.Drawing.Size(1099, 719);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 0;
            // 
            // deleteFilesBtn
            // 
            this.deleteFilesBtn.Location = new System.Drawing.Point(12, 41);
            this.deleteFilesBtn.Name = "deleteFilesBtn";
            this.deleteFilesBtn.Size = new System.Drawing.Size(197, 23);
            this.deleteFilesBtn.TabIndex = 2;
            this.deleteFilesBtn.Text = "Usuń pliki";
            this.deleteFilesBtn.UseVisualStyleBackColor = true;
            this.deleteFilesBtn.Click += new System.EventHandler(this.deleteFilesBtn_Click);
            // 
            // addedFilesListView
            // 
            this.addedFilesListView.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.addedFilesListView.ItemHeight = 16;
            this.addedFilesListView.Location = new System.Drawing.Point(12, 74);
            this.addedFilesListView.Name = "addedFilesListView";
            this.addedFilesListView.Size = new System.Drawing.Size(197, 628);
            this.addedFilesListView.TabIndex = 1;
            // 
            // AddFilesBtn
            // 
            this.AddFilesBtn.Location = new System.Drawing.Point(12, 12);
            this.AddFilesBtn.Name = "AddFilesBtn";
            this.AddFilesBtn.Size = new System.Drawing.Size(197, 23);
            this.AddFilesBtn.TabIndex = 0;
            this.AddFilesBtn.Text = "Dodaj pliki";
            this.AddFilesBtn.UseVisualStyleBackColor = true;
            this.AddFilesBtn.Click += new System.EventHandler(this.AddFilesBtn_Click);
            // 
            // saveRaportBtn
            // 
            this.saveRaportBtn.Location = new System.Drawing.Point(680, 666);
            this.saveRaportBtn.Name = "saveRaportBtn";
            this.saveRaportBtn.Size = new System.Drawing.Size(160, 41);
            this.saveRaportBtn.TabIndex = 14;
            this.saveRaportBtn.Text = "Zapisz raport";
            this.saveRaportBtn.UseVisualStyleBackColor = true;
            this.saveRaportBtn.Click += new System.EventHandler(this.saveRaportBtn_Click);
            // 
            // raportsDataGrid
            // 
            this.raportsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.raportsDataGrid.Location = new System.Drawing.Point(10, 125);
            this.raportsDataGrid.Name = "raportsDataGrid";
            this.raportsDataGrid.RowTemplate.Height = 24;
            this.raportsDataGrid.Size = new System.Drawing.Size(830, 530);
            this.raportsDataGrid.TabIndex = 13;
            // 
            // valueRangeBox
            // 
            this.valueRangeBox.Controls.Add(this.maxMaxValLabel);
            this.valueRangeBox.Controls.Add(this.label4);
            this.valueRangeBox.Controls.Add(this.maxMaxValue);
            this.valueRangeBox.Controls.Add(this.maxValueTextBox);
            this.valueRangeBox.Controls.Add(this.label2);
            this.valueRangeBox.Controls.Add(this.minValueTextBox);
            this.valueRangeBox.Controls.Add(this.label3);
            this.valueRangeBox.Location = new System.Drawing.Point(10, 68);
            this.valueRangeBox.Name = "valueRangeBox";
            this.valueRangeBox.Size = new System.Drawing.Size(664, 50);
            this.valueRangeBox.TabIndex = 12;
            this.valueRangeBox.TabStop = false;
            this.valueRangeBox.Visible = false;
            // 
            // maxMaxValLabel
            // 
            this.maxMaxValLabel.AutoSize = true;
            this.maxMaxValLabel.Location = new System.Drawing.Point(599, 18);
            this.maxMaxValLabel.Name = "maxMaxValLabel";
            this.maxMaxValLabel.Size = new System.Drawing.Size(16, 17);
            this.maxMaxValLabel.TabIndex = 12;
            this.maxMaxValLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(476, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Najwyższa cena: ";
            // 
            // maxMaxValue
            // 
            this.maxMaxValue.AutoSize = true;
            this.maxMaxValue.Location = new System.Drawing.Point(532, 18);
            this.maxMaxValue.Name = "maxMaxValue";
            this.maxMaxValue.Size = new System.Drawing.Size(0, 17);
            this.maxMaxValue.TabIndex = 10;
            // 
            // maxValueTextBox
            // 
            this.maxValueTextBox.Location = new System.Drawing.Point(363, 15);
            this.maxValueTextBox.Name = "maxValueTextBox";
            this.maxValueTextBox.Size = new System.Drawing.Size(107, 22);
            this.maxValueTextBox.TabIndex = 9;
            this.maxValueTextBox.Leave += new System.EventHandler(this.maxValueTextBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Przedział cenowy:";
            // 
            // minValueTextBox
            // 
            this.minValueTextBox.Location = new System.Drawing.Point(195, 15);
            this.minValueTextBox.Name = "minValueTextBox";
            this.minValueTextBox.Size = new System.Drawing.Size(107, 22);
            this.minValueTextBox.TabIndex = 6;
            this.minValueTextBox.Leave += new System.EventHandler(this.minValueTextBox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "do";
            // 
            // clientIdBox
            // 
            this.clientIdBox.Controls.Add(this.clientIdLabel);
            this.clientIdBox.Controls.Add(this.clientIdComboBox);
            this.clientIdBox.Location = new System.Drawing.Point(10, 68);
            this.clientIdBox.Name = "clientIdBox";
            this.clientIdBox.Size = new System.Drawing.Size(664, 50);
            this.clientIdBox.TabIndex = 11;
            this.clientIdBox.TabStop = false;
            // 
            // clientIdLabel
            // 
            this.clientIdLabel.AutoSize = true;
            this.clientIdLabel.Location = new System.Drawing.Point(6, 18);
            this.clientIdLabel.Name = "clientIdLabel";
            this.clientIdLabel.Size = new System.Drawing.Size(134, 17);
            this.clientIdLabel.TabIndex = 4;
            this.clientIdLabel.Text = "Identyfikator klienta:";
            // 
            // clientIdComboBox
            // 
            this.clientIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientIdComboBox.FormattingEnabled = true;
            this.clientIdComboBox.Location = new System.Drawing.Point(195, 15);
            this.clientIdComboBox.Name = "clientIdComboBox";
            this.clientIdComboBox.Size = new System.Drawing.Size(458, 24);
            this.clientIdComboBox.TabIndex = 1;
            this.clientIdComboBox.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raportsComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 50);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // raportsComboBox
            // 
            this.raportsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.raportsComboBox.FormattingEnabled = true;
            this.raportsComboBox.Location = new System.Drawing.Point(195, 15);
            this.raportsComboBox.Name = "raportsComboBox";
            this.raportsComboBox.Size = new System.Drawing.Size(458, 24);
            this.raportsComboBox.TabIndex = 2;
            this.raportsComboBox.SelectionChangeCommitted += new System.EventHandler(this.raportsComboBoxSelectionChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Raporty do wygenerowania:";
            // 
            // raportGenBtn
            // 
            this.raportGenBtn.Location = new System.Drawing.Point(680, 12);
            this.raportGenBtn.Name = "raportGenBtn";
            this.raportGenBtn.Size = new System.Drawing.Size(160, 107);
            this.raportGenBtn.TabIndex = 3;
            this.raportGenBtn.Text = "Generuj";
            this.raportGenBtn.UseVisualStyleBackColor = true;
            this.raportGenBtn.Click += new System.EventHandler(this.raportGenBtn_Click);
            // 
            // saveRaportDialog
            // 
            this.saveRaportDialog.Filter = "csv files (*.csv)|*.csv";
            this.saveRaportDialog.InitialDirectory = "C:";
            this.saveRaportDialog.Title = "Zapisz raport";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 719);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.raportsDataGrid)).EndInit();
            this.valueRangeBox.ResumeLayout(false);
            this.valueRangeBox.PerformLayout();
            this.clientIdBox.ResumeLayout(false);
            this.clientIdBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog addFilesDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button AddFilesBtn;
        private System.Windows.Forms.ListBox addedFilesListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox raportsComboBox;
        private System.Windows.Forms.Button raportGenBtn;
        private System.Windows.Forms.Label clientIdLabel;
        private System.Windows.Forms.ComboBox clientIdComboBox;
        private System.Windows.Forms.TextBox minValueTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox valueRangeBox;
        private System.Windows.Forms.TextBox maxValueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox clientIdBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label maxMaxValue;
        private System.Windows.Forms.Label maxMaxValLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteFilesBtn;
        public System.Windows.Forms.DataGridView raportsDataGrid;
        private System.Windows.Forms.Button saveRaportBtn;
        private System.Windows.Forms.SaveFileDialog saveRaportDialog;
    }
}

