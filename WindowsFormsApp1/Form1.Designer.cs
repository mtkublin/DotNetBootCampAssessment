namespace WindowsFormsApp1
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
            this.AddFilesBtn = new System.Windows.Forms.Button();
            this.addedFilesListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addFilesDialog
            // 
            this.addFilesDialog.FileName = "openFileDialog1";
            this.addFilesDialog.Filter = "csv files (*.csv)|*.csv|xml files (*.xml)|*.xml|json files (*.json)|*.json";
            this.addFilesDialog.InitialDirectory = "C:\\Users\\Użytkownik\\source\\repos\\WindowsFormsApp1\\Docs";
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
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.TabIndex = 0;
            // 
            // AddFilesBtn
            // 
            this.AddFilesBtn.Location = new System.Drawing.Point(12, 12);
            this.AddFilesBtn.Name = "AddFilesBtn";
            this.AddFilesBtn.Size = new System.Drawing.Size(75, 23);
            this.AddFilesBtn.TabIndex = 0;
            this.AddFilesBtn.Text = "AddFilesBtn";
            this.AddFilesBtn.UseVisualStyleBackColor = true;
            this.AddFilesBtn.Click += new System.EventHandler(this.AddFilesBtn_Click);
            // 
            // addedFilesListView
            // 
            this.addedFilesListView.Location = new System.Drawing.Point(12, 42);
            this.addedFilesListView.Name = "addedFilesListView";
            this.addedFilesListView.Size = new System.Drawing.Size(308, 365);
            this.addedFilesListView.TabIndex = 1;
            this.addedFilesListView.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog addFilesDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button AddFilesBtn;
        private System.Windows.Forms.ListView addedFilesListView;
    }
}

