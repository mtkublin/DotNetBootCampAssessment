namespace ReqRaportsApp
{
    partial class MainForm
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
            this.AddFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DeleteFilesBtn = new System.Windows.Forms.Button();
            this.AddedFilesListBox = new System.Windows.Forms.ListBox();
            this.AddFilesBtn = new System.Windows.Forms.Button();
            this.SaveRaportBtn = new System.Windows.Forms.Button();
            this.RaportsDataGrid = new System.Windows.Forms.DataGridView();
            this.ValueRangeBox = new System.Windows.Forms.GroupBox();
            this.MaxMaxValLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maxMaxValue = new System.Windows.Forms.Label();
            this.MaxValueTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MinValueTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ClientIdBox = new System.Windows.Forms.GroupBox();
            this.ClientIdLabel = new System.Windows.Forms.Label();
            this.ClientIdComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RaportsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RaportGenBtn = new System.Windows.Forms.Button();
            this.SaveRaportDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RaportsDataGrid)).BeginInit();
            this.ValueRangeBox.SuspendLayout();
            this.ClientIdBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddFilesDialog
            // 
            this.AddFilesDialog.FileName = "openFileDialog1";
            this.AddFilesDialog.Filter = "xml, csv and json files (*.xml, *.csv, *.json)|*.csv; *.json; *.xml|xml files (*." +
    "xml)|*.xml|csv files (*.csv)|*.csv|json files (*.json)|*.json";
            this.AddFilesDialog.InitialDirectory = "C:\\Users\\Użytkownik\\source\\repos\\WindowsFormsApp1\\SampleData";
            this.AddFilesDialog.Multiselect = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DeleteFilesBtn);
            this.splitContainer1.Panel1.Controls.Add(this.AddedFilesListBox);
            this.splitContainer1.Panel1.Controls.Add(this.AddFilesBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SaveRaportBtn);
            this.splitContainer1.Panel2.Controls.Add(this.RaportsDataGrid);
            this.splitContainer1.Panel2.Controls.Add(this.ValueRangeBox);
            this.splitContainer1.Panel2.Controls.Add(this.ClientIdBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.RaportGenBtn);
            this.splitContainer1.Size = new System.Drawing.Size(1099, 719);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 0;
            // 
            // DeleteFilesBtn
            // 
            this.DeleteFilesBtn.Location = new System.Drawing.Point(12, 41);
            this.DeleteFilesBtn.Name = "DeleteFilesBtn";
            this.DeleteFilesBtn.Size = new System.Drawing.Size(197, 23);
            this.DeleteFilesBtn.TabIndex = 2;
            this.DeleteFilesBtn.Text = "Usuń pliki";
            this.DeleteFilesBtn.UseVisualStyleBackColor = true;
            this.DeleteFilesBtn.Click += new System.EventHandler(this.DeleteFilesBtn_Click);
            // 
            // AddedFilesListBox
            // 
            this.AddedFilesListBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.AddedFilesListBox.ItemHeight = 16;
            this.AddedFilesListBox.Location = new System.Drawing.Point(12, 74);
            this.AddedFilesListBox.Name = "AddedFilesListBox";
            this.AddedFilesListBox.Size = new System.Drawing.Size(197, 628);
            this.AddedFilesListBox.TabIndex = 1;
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
            // SaveRaportBtn
            // 
            this.SaveRaportBtn.Location = new System.Drawing.Point(680, 666);
            this.SaveRaportBtn.Name = "SaveRaportBtn";
            this.SaveRaportBtn.Size = new System.Drawing.Size(160, 41);
            this.SaveRaportBtn.TabIndex = 14;
            this.SaveRaportBtn.Text = "Zapisz raport";
            this.SaveRaportBtn.UseVisualStyleBackColor = true;
            this.SaveRaportBtn.Click += new System.EventHandler(this.SaveRaportBtn_Click);
            // 
            // RaportsDataGrid
            // 
            this.RaportsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RaportsDataGrid.Location = new System.Drawing.Point(10, 125);
            this.RaportsDataGrid.Name = "RaportsDataGrid";
            this.RaportsDataGrid.RowTemplate.Height = 24;
            this.RaportsDataGrid.Size = new System.Drawing.Size(830, 530);
            this.RaportsDataGrid.TabIndex = 13;
            // 
            // ValueRangeBox
            // 
            this.ValueRangeBox.Controls.Add(this.MaxMaxValLabel);
            this.ValueRangeBox.Controls.Add(this.label4);
            this.ValueRangeBox.Controls.Add(this.maxMaxValue);
            this.ValueRangeBox.Controls.Add(this.MaxValueTextBox);
            this.ValueRangeBox.Controls.Add(this.label2);
            this.ValueRangeBox.Controls.Add(this.MinValueTextBox);
            this.ValueRangeBox.Controls.Add(this.label3);
            this.ValueRangeBox.Location = new System.Drawing.Point(10, 68);
            this.ValueRangeBox.Name = "ValueRangeBox";
            this.ValueRangeBox.Size = new System.Drawing.Size(664, 50);
            this.ValueRangeBox.TabIndex = 12;
            this.ValueRangeBox.TabStop = false;
            this.ValueRangeBox.Visible = false;
            // 
            // MaxMaxValLabel
            // 
            this.MaxMaxValLabel.AutoSize = true;
            this.MaxMaxValLabel.Location = new System.Drawing.Point(599, 18);
            this.MaxMaxValLabel.Name = "MaxMaxValLabel";
            this.MaxMaxValLabel.Size = new System.Drawing.Size(16, 17);
            this.MaxMaxValLabel.TabIndex = 12;
            this.MaxMaxValLabel.Text = "0";
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
            // MaxValueTextBox
            // 
            this.MaxValueTextBox.Location = new System.Drawing.Point(363, 15);
            this.MaxValueTextBox.Name = "MaxValueTextBox";
            this.MaxValueTextBox.Size = new System.Drawing.Size(107, 22);
            this.MaxValueTextBox.TabIndex = 9;
            this.MaxValueTextBox.Leave += new System.EventHandler(this.MaxValueTextBox_Leave);
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
            // MinValueTextBox
            // 
            this.MinValueTextBox.Location = new System.Drawing.Point(195, 15);
            this.MinValueTextBox.Name = "MinValueTextBox";
            this.MinValueTextBox.Size = new System.Drawing.Size(107, 22);
            this.MinValueTextBox.TabIndex = 6;
            this.MinValueTextBox.Leave += new System.EventHandler(this.MinValueTextBox_Leave);
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
            // ClientIdBox
            // 
            this.ClientIdBox.Controls.Add(this.ClientIdLabel);
            this.ClientIdBox.Controls.Add(this.ClientIdComboBox);
            this.ClientIdBox.Location = new System.Drawing.Point(10, 68);
            this.ClientIdBox.Name = "ClientIdBox";
            this.ClientIdBox.Size = new System.Drawing.Size(664, 50);
            this.ClientIdBox.TabIndex = 11;
            this.ClientIdBox.TabStop = false;
            // 
            // ClientIdLabel
            // 
            this.ClientIdLabel.AutoSize = true;
            this.ClientIdLabel.Location = new System.Drawing.Point(6, 18);
            this.ClientIdLabel.Name = "ClientIdLabel";
            this.ClientIdLabel.Size = new System.Drawing.Size(134, 17);
            this.ClientIdLabel.TabIndex = 4;
            this.ClientIdLabel.Text = "Identyfikator klienta:";
            // 
            // ClientIdComboBox
            // 
            this.ClientIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClientIdComboBox.FormattingEnabled = true;
            this.ClientIdComboBox.Location = new System.Drawing.Point(195, 15);
            this.ClientIdComboBox.Name = "ClientIdComboBox";
            this.ClientIdComboBox.Size = new System.Drawing.Size(458, 24);
            this.ClientIdComboBox.TabIndex = 1;
            this.ClientIdComboBox.Visible = false;
            this.ClientIdComboBox.Sorted = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RaportsComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 50);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // RaportsComboBox
            // 
            this.RaportsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RaportsComboBox.FormattingEnabled = true;
            this.RaportsComboBox.Location = new System.Drawing.Point(195, 15);
            this.RaportsComboBox.Name = "RaportsComboBox";
            this.RaportsComboBox.Size = new System.Drawing.Size(458, 24);
            this.RaportsComboBox.TabIndex = 2;
            this.RaportsComboBox.SelectionChangeCommitted += new System.EventHandler(this.RaportsComboBoxSelectionChange);
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
            // RaportGenBtn
            // 
            this.RaportGenBtn.Location = new System.Drawing.Point(680, 12);
            this.RaportGenBtn.Name = "RaportGenBtn";
            this.RaportGenBtn.Size = new System.Drawing.Size(160, 107);
            this.RaportGenBtn.TabIndex = 3;
            this.RaportGenBtn.Text = "Generuj";
            this.RaportGenBtn.UseVisualStyleBackColor = true;
            this.RaportGenBtn.Click += new System.EventHandler(this.RaportGenBtn_Click);
            // 
            // SaveRaportDialog
            // 
            this.SaveRaportDialog.Filter = "csv files (*.csv)|*.csv";
            this.SaveRaportDialog.InitialDirectory = "C:";
            this.SaveRaportDialog.Title = "Zapisz raport";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 719);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RaportsDataGrid)).EndInit();
            this.ValueRangeBox.ResumeLayout(false);
            this.ValueRangeBox.PerformLayout();
            this.ClientIdBox.ResumeLayout(false);
            this.ClientIdBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog AddFilesDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button AddFilesBtn;
        private System.Windows.Forms.ListBox AddedFilesListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox RaportsComboBox;
        private System.Windows.Forms.Button RaportGenBtn;
        private System.Windows.Forms.Label ClientIdLabel;
        private System.Windows.Forms.ComboBox ClientIdComboBox;
        private System.Windows.Forms.TextBox MinValueTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox ValueRangeBox;
        private System.Windows.Forms.TextBox MaxValueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox ClientIdBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label maxMaxValue;
        private System.Windows.Forms.Label MaxMaxValLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DeleteFilesBtn;
        public System.Windows.Forms.DataGridView RaportsDataGrid;
        private System.Windows.Forms.Button SaveRaportBtn;
        private System.Windows.Forms.SaveFileDialog SaveRaportDialog;
    }
}

