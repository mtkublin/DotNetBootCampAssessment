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
            this.addedFilesListView = new System.Windows.Forms.ListView();
            this.AddFilesBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.raportBtnsGroup = new System.Windows.Forms.GroupBox();
            this.ReqsForValueRangeBtn = new System.Windows.Forms.Button();
            this.ReqQuantByNameForClientIdBtn = new System.Windows.Forms.Button();
            this.ReqQuantByNameBtn = new System.Windows.Forms.Button();
            this.AverageReqValueForClientIdBtn = new System.Windows.Forms.Button();
            this.AverageReqValueBtn = new System.Windows.Forms.Button();
            this.ReqsListForClientIdBtn = new System.Windows.Forms.Button();
            this.AllReqsListBtn = new System.Windows.Forms.Button();
            this.ReqValueSumForClientIdBtn = new System.Windows.Forms.Button();
            this.ReqValueSumBtn = new System.Windows.Forms.Button();
            this.ReqQuantForClientIdBtn = new System.Windows.Forms.Button();
            this.ReqQuantBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.raportBtnsGroup.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.raportBtnsGroup);
            this.splitContainer1.Size = new System.Drawing.Size(797, 477);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 0;
            // 
            // addedFilesListView
            // 
            this.addedFilesListView.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.addedFilesListView.Location = new System.Drawing.Point(12, 42);
            this.addedFilesListView.Name = "addedFilesListView";
            this.addedFilesListView.Size = new System.Drawing.Size(197, 412);
            this.addedFilesListView.TabIndex = 1;
            this.addedFilesListView.UseCompatibleStateImageBehavior = false;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Raporty do wygenerowania:";
            // 
            // raportBtnsGroup
            // 
            this.raportBtnsGroup.Controls.Add(this.ReqsForValueRangeBtn);
            this.raportBtnsGroup.Controls.Add(this.ReqQuantByNameForClientIdBtn);
            this.raportBtnsGroup.Controls.Add(this.ReqQuantByNameBtn);
            this.raportBtnsGroup.Controls.Add(this.AverageReqValueForClientIdBtn);
            this.raportBtnsGroup.Controls.Add(this.AverageReqValueBtn);
            this.raportBtnsGroup.Controls.Add(this.ReqsListForClientIdBtn);
            this.raportBtnsGroup.Controls.Add(this.AllReqsListBtn);
            this.raportBtnsGroup.Controls.Add(this.ReqValueSumForClientIdBtn);
            this.raportBtnsGroup.Controls.Add(this.ReqValueSumBtn);
            this.raportBtnsGroup.Controls.Add(this.ReqQuantForClientIdBtn);
            this.raportBtnsGroup.Controls.Add(this.ReqQuantBtn);
            this.raportBtnsGroup.Location = new System.Drawing.Point(33, 42);
            this.raportBtnsGroup.Name = "raportBtnsGroup";
            this.raportBtnsGroup.Size = new System.Drawing.Size(492, 412);
            this.raportBtnsGroup.TabIndex = 0;
            this.raportBtnsGroup.TabStop = false;
            // 
            // ReqsForValueRangeBtn
            // 
            this.ReqsForValueRangeBtn.Location = new System.Drawing.Point(6, 371);
            this.ReqsForValueRangeBtn.Name = "ReqsForValueRangeBtn";
            this.ReqsForValueRangeBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqsForValueRangeBtn.TabIndex = 10;
            this.ReqsForValueRangeBtn.Text = "Zamówienia w podanym przedziale cenowym";
            this.ReqsForValueRangeBtn.UseVisualStyleBackColor = true;
            this.ReqsForValueRangeBtn.Click += new System.EventHandler(this.ReqsForValueRangeBtn_Click);
            // 
            // ReqQuantByNameForClientIdBtn
            // 
            this.ReqQuantByNameForClientIdBtn.Location = new System.Drawing.Point(6, 336);
            this.ReqQuantByNameForClientIdBtn.Name = "ReqQuantByNameForClientIdBtn";
            this.ReqQuantByNameForClientIdBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqQuantByNameForClientIdBtn.TabIndex = 9;
            this.ReqQuantByNameForClientIdBtn.Text = "Ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym identyfikatorze";
            this.ReqQuantByNameForClientIdBtn.UseVisualStyleBackColor = true;
            this.ReqQuantByNameForClientIdBtn.Click += new System.EventHandler(this.ReqQuantByNameForClientIdBtn_Click);
            // 
            // ReqQuantByNameBtn
            // 
            this.ReqQuantByNameBtn.Location = new System.Drawing.Point(6, 301);
            this.ReqQuantByNameBtn.Name = "ReqQuantByNameBtn";
            this.ReqQuantByNameBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqQuantByNameBtn.TabIndex = 8;
            this.ReqQuantByNameBtn.Text = "Ilość zamówień pogrupowanych po nazwie";
            this.ReqQuantByNameBtn.UseVisualStyleBackColor = true;
            this.ReqQuantByNameBtn.Click += new System.EventHandler(this.ReqQuantByNameBtn_Click);
            // 
            // AverageReqValueForClientIdBtn
            // 
            this.AverageReqValueForClientIdBtn.Location = new System.Drawing.Point(6, 266);
            this.AverageReqValueForClientIdBtn.Name = "AverageReqValueForClientIdBtn";
            this.AverageReqValueForClientIdBtn.Size = new System.Drawing.Size(480, 29);
            this.AverageReqValueForClientIdBtn.TabIndex = 7;
            this.AverageReqValueForClientIdBtn.Text = "Średnia wartość zamówienia dla klienta o wskazanym identyfikatorze";
            this.AverageReqValueForClientIdBtn.UseVisualStyleBackColor = true;
            this.AverageReqValueForClientIdBtn.Click += new System.EventHandler(this.AverageReqValueForClientIdBtn_Click);
            // 
            // AverageReqValueBtn
            // 
            this.AverageReqValueBtn.Location = new System.Drawing.Point(6, 231);
            this.AverageReqValueBtn.Name = "AverageReqValueBtn";
            this.AverageReqValueBtn.Size = new System.Drawing.Size(480, 29);
            this.AverageReqValueBtn.TabIndex = 6;
            this.AverageReqValueBtn.Text = "Średnia wartość zamówienia";
            this.AverageReqValueBtn.UseVisualStyleBackColor = true;
            this.AverageReqValueBtn.Click += new System.EventHandler(this.AverageReqValueBtn_Click);
            // 
            // ReqsListForClientIdBtn
            // 
            this.ReqsListForClientIdBtn.Location = new System.Drawing.Point(6, 196);
            this.ReqsListForClientIdBtn.Name = "ReqsListForClientIdBtn";
            this.ReqsListForClientIdBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqsListForClientIdBtn.TabIndex = 5;
            this.ReqsListForClientIdBtn.Text = "Lista zamówień dla klienta o wskazanym identyfikatorze";
            this.ReqsListForClientIdBtn.UseVisualStyleBackColor = true;
            this.ReqsListForClientIdBtn.Click += new System.EventHandler(this.ReqsListForClientIdBtn_Click);
            // 
            // AllReqsListBtn
            // 
            this.AllReqsListBtn.Location = new System.Drawing.Point(6, 161);
            this.AllReqsListBtn.Name = "AllReqsListBtn";
            this.AllReqsListBtn.Size = new System.Drawing.Size(480, 29);
            this.AllReqsListBtn.TabIndex = 4;
            this.AllReqsListBtn.Text = "Lista wszystkich zamówień";
            this.AllReqsListBtn.UseVisualStyleBackColor = true;
            this.AllReqsListBtn.Click += new System.EventHandler(this.AllReqsListBtn_Click);
            // 
            // ReqValueSumForClientIdBtn
            // 
            this.ReqValueSumForClientIdBtn.Location = new System.Drawing.Point(6, 126);
            this.ReqValueSumForClientIdBtn.Name = "ReqValueSumForClientIdBtn";
            this.ReqValueSumForClientIdBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqValueSumForClientIdBtn.TabIndex = 3;
            this.ReqValueSumForClientIdBtn.Text = "Łączna kwota zamówień dla klienta o wskazanym identyfikatorze";
            this.ReqValueSumForClientIdBtn.UseVisualStyleBackColor = true;
            this.ReqValueSumForClientIdBtn.Click += new System.EventHandler(this.ReqValueSumForClientIdBtn_Click);
            // 
            // ReqValueSumBtn
            // 
            this.ReqValueSumBtn.Location = new System.Drawing.Point(6, 91);
            this.ReqValueSumBtn.Name = "ReqValueSumBtn";
            this.ReqValueSumBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqValueSumBtn.TabIndex = 2;
            this.ReqValueSumBtn.Text = "Łączna kwota zamówień";
            this.ReqValueSumBtn.UseVisualStyleBackColor = true;
            this.ReqValueSumBtn.Click += new System.EventHandler(this.ReqValueSumBtn_Click);
            // 
            // ReqQuantForClientIdBtn
            // 
            this.ReqQuantForClientIdBtn.Location = new System.Drawing.Point(6, 56);
            this.ReqQuantForClientIdBtn.Name = "ReqQuantForClientIdBtn";
            this.ReqQuantForClientIdBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqQuantForClientIdBtn.TabIndex = 1;
            this.ReqQuantForClientIdBtn.Text = "Ilość zamówień dla klienta o wskazanym identyfikatorze";
            this.ReqQuantForClientIdBtn.UseVisualStyleBackColor = true;
            this.ReqQuantForClientIdBtn.Click += new System.EventHandler(this.ReqQuantForClientIdBtn_Click);
            // 
            // ReqQuantBtn
            // 
            this.ReqQuantBtn.Location = new System.Drawing.Point(6, 21);
            this.ReqQuantBtn.Name = "ReqQuantBtn";
            this.ReqQuantBtn.Size = new System.Drawing.Size(480, 29);
            this.ReqQuantBtn.TabIndex = 0;
            this.ReqQuantBtn.Text = "Ilość zamówień";
            this.ReqQuantBtn.UseVisualStyleBackColor = true;
            this.ReqQuantBtn.Click += new System.EventHandler(this.ReqQuantBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 477);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.raportBtnsGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog addFilesDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button AddFilesBtn;
        private System.Windows.Forms.ListView addedFilesListView;
        private System.Windows.Forms.GroupBox raportBtnsGroup;
        private System.Windows.Forms.Button ReqsForValueRangeBtn;
        private System.Windows.Forms.Button ReqQuantByNameForClientIdBtn;
        private System.Windows.Forms.Button ReqQuantByNameBtn;
        private System.Windows.Forms.Button AverageReqValueForClientIdBtn;
        private System.Windows.Forms.Button AverageReqValueBtn;
        private System.Windows.Forms.Button ReqsListForClientIdBtn;
        private System.Windows.Forms.Button AllReqsListBtn;
        private System.Windows.Forms.Button ReqValueSumForClientIdBtn;
        private System.Windows.Forms.Button ReqValueSumBtn;
        private System.Windows.Forms.Button ReqQuantForClientIdBtn;
        private System.Windows.Forms.Button ReqQuantBtn;
        private System.Windows.Forms.Label label1;
    }
}

