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
            this.addedFilesListView = new System.Windows.Forms.ListBox();
            this.AddFilesBtn = new System.Windows.Forms.Button();
            this.raportGenBtn = new System.Windows.Forms.Button();
            this.raportsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clientIdComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addFilesDialog
            // 
            this.addFilesDialog.FileName = "openFileDialog1";
            this.addFilesDialog.Filter = "xml, csv and json files (*.xml, *.csv, *.json)|*.csv; *.json; *.xml|xml files (*." +
    "xml)|*.xml|csv files (*.csv)|*.csv|json files (*.json)|*.json";
            this.addFilesDialog.InitialDirectory = "C:\\Users\\Użytkownik\\source\\repos\\WindowsFormsApp1\\Docs";
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
            this.splitContainer1.Panel1.Controls.Add(this.addedFilesListView);
            this.splitContainer1.Panel1.Controls.Add(this.AddFilesBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.clientIdComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.raportGenBtn);
            this.splitContainer1.Panel2.Controls.Add(this.raportsComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(1378, 669);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 0;
            // 
            // addedFilesListView
            // 
            this.addedFilesListView.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.addedFilesListView.ItemHeight = 16;
            this.addedFilesListView.Location = new System.Drawing.Point(12, 42);
            this.addedFilesListView.Name = "addedFilesListView";
            this.addedFilesListView.Size = new System.Drawing.Size(197, 404);
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
            // raportGenBtn
            // 
            this.raportGenBtn.Location = new System.Drawing.Point(680, 18);
            this.raportGenBtn.Name = "raportGenBtn";
            this.raportGenBtn.Size = new System.Drawing.Size(102, 24);
            this.raportGenBtn.TabIndex = 3;
            this.raportGenBtn.Text = "Generuj";
            this.raportGenBtn.UseVisualStyleBackColor = true;
            this.raportGenBtn.Click += new System.EventHandler(this.raportGenBtn_Click);
            // 
            // raportsComboBox
            // 
            this.raportsComboBox.FormattingEnabled = true;
            this.raportsComboBox.Location = new System.Drawing.Point(216, 18);
            this.raportsComboBox.Name = "raportsComboBox";
            this.raportsComboBox.Size = new System.Drawing.Size(458, 24);
            this.raportsComboBox.TabIndex = 2;
            this.raportsComboBox.SelectionChangeCommitted += new System.EventHandler(raportsComboBoxSelectionChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Raporty do wygenerowania:";
            // 
            // clientIdComboBox
            // 
            this.clientIdComboBox.FormattingEnabled = true;
            this.clientIdComboBox.Location = new System.Drawing.Point(216, 48);
            this.clientIdComboBox.Name = "clientIdComboBox";
            this.clientIdComboBox.Size = new System.Drawing.Size(458, 24);
            this.clientIdComboBox.TabIndex = 1;
            this.clientIdComboBox.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Identyfikator klienta:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 669);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox clientIdComboBox;
    }
}

