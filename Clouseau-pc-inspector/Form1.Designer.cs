namespace PC_Inspector
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
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.inAppLogTextBox = new System.Windows.Forms.RichTextBox();
            this.applicationInfoSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.applicationListView = new System.Windows.Forms.ListView();
            this.DisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DisplayVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InstallDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RegLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveDxDiagButton = new System.Windows.Forms.Button();
            this.dxDiagSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.dxdiagBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Codecs = new System.Windows.Forms.TabPage();
            this.saveCodecBtn = new System.Windows.Forms.Button();
            this.inspectCodecBtn = new System.Windows.Forms.Button();
            this.codecListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage_Application = new System.Windows.Forms.TabPage();
            this.saveApplicationBtn = new System.Windows.Forms.Button();
            this.inspectApplicationBtn = new System.Windows.Forms.Button();
            this.tabPage_DxDiag = new System.Windows.Forms.TabPage();
            this.dxdiagPreviewTextBox = new System.Windows.Forms.RichTextBox();
            this.codecInfoSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage_Codecs.SuspendLayout();
            this.tabPage_Application.SuspendLayout();
            this.tabPage_DxDiag.SuspendLayout();
            this.SuspendLayout();
            // 
            // inAppLogTextBox
            // 
            this.inAppLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inAppLogTextBox.Location = new System.Drawing.Point(12, 605);
            this.inAppLogTextBox.Name = "inAppLogTextBox";
            this.inAppLogTextBox.ReadOnly = true;
            this.inAppLogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.inAppLogTextBox.Size = new System.Drawing.Size(1235, 104);
            this.inAppLogTextBox.TabIndex = 1;
            this.inAppLogTextBox.Text = "";
            this.inAppLogTextBox.TextChanged += new System.EventHandler(this.inAppLogTextbox_TextChanged);
            // 
            // applicationInfoSaveFileDialog
            // 
            this.applicationInfoSaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.applicationInfoSaveFileOk);
            // 
            // applicationListView
            // 
            this.applicationListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applicationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DisplayName,
            this.DisplayVersion,
            this.Version,
            this.InstallDate,
            this.RegLocation});
            this.applicationListView.Location = new System.Drawing.Point(6, 65);
            this.applicationListView.Name = "applicationListView";
            this.applicationListView.Size = new System.Drawing.Size(1215, 483);
            this.applicationListView.TabIndex = 6;
            this.applicationListView.UseCompatibleStateImageBehavior = false;
            this.applicationListView.View = System.Windows.Forms.View.Details;
            this.applicationListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.applicationColumnHeader_Click);
            // 
            // DisplayName
            // 
            this.DisplayName.Text = "DisplayName";
            this.DisplayName.Width = 328;
            // 
            // DisplayVersion
            // 
            this.DisplayVersion.Text = "DisplayVersion";
            this.DisplayVersion.Width = 120;
            // 
            // Version
            // 
            this.Version.Text = "Version";
            this.Version.Width = 77;
            // 
            // InstallDate
            // 
            this.InstallDate.Text = "InstallDate";
            this.InstallDate.Width = 72;
            // 
            // RegLocation
            // 
            this.RegLocation.Text = "RegLocation";
            this.RegLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RegLocation.Width = 80;
            // 
            // saveDxDiagButton
            // 
            this.saveDxDiagButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveDxDiagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveDxDiagButton.Location = new System.Drawing.Point(6, 6);
            this.saveDxDiagButton.Name = "saveDxDiagButton";
            this.saveDxDiagButton.Size = new System.Drawing.Size(1215, 53);
            this.saveDxDiagButton.TabIndex = 7;
            this.saveDxDiagButton.Text = "Save DxDiag";
            this.saveDxDiagButton.UseVisualStyleBackColor = true;
            this.saveDxDiagButton.Click += new System.EventHandler(this.saveDxDiagButton_Click);
            // 
            // dxDiagSaveFileDialog
            // 
            this.dxDiagSaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.dxDiagSaveFileOk);
            // 
            // dxdiagBackgroundWorker
            // 
            this.dxdiagBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dxdiagBackgroundWorker_DoWork);
            this.dxdiagBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DxdiagBackgroundWorker_RunWorkerCompleted);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage_Codecs);
            this.tabControl1.Controls.Add(this.tabPage_Application);
            this.tabControl1.Controls.Add(this.tabPage_DxDiag);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1235, 580);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage_Codecs
            // 
            this.tabPage_Codecs.Controls.Add(this.saveCodecBtn);
            this.tabPage_Codecs.Controls.Add(this.inspectCodecBtn);
            this.tabPage_Codecs.Controls.Add(this.codecListView);
            this.tabPage_Codecs.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Codecs.Name = "tabPage_Codecs";
            this.tabPage_Codecs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Codecs.Size = new System.Drawing.Size(1227, 554);
            this.tabPage_Codecs.TabIndex = 2;
            this.tabPage_Codecs.Text = "Codecs";
            this.tabPage_Codecs.UseVisualStyleBackColor = true;
            // 
            // saveCodecBtn
            // 
            this.saveCodecBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveCodecBtn.Location = new System.Drawing.Point(1100, 6);
            this.saveCodecBtn.Name = "saveCodecBtn";
            this.saveCodecBtn.Size = new System.Drawing.Size(121, 53);
            this.saveCodecBtn.TabIndex = 10;
            this.saveCodecBtn.Text = "Save Codec Info";
            this.saveCodecBtn.UseVisualStyleBackColor = true;
            this.saveCodecBtn.Click += new System.EventHandler(this.saveCodecBtn_Click);
            // 
            // inspectCodecBtn
            // 
            this.inspectCodecBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectCodecBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inspectCodecBtn.Location = new System.Drawing.Point(6, 6);
            this.inspectCodecBtn.Name = "inspectCodecBtn";
            this.inspectCodecBtn.Size = new System.Drawing.Size(1088, 53);
            this.inspectCodecBtn.TabIndex = 9;
            this.inspectCodecBtn.Text = "Inspect Codecs";
            this.inspectCodecBtn.UseVisualStyleBackColor = true;
            this.inspectCodecBtn.Click += new System.EventHandler(this.inspectCodecBtn_Click);
            // 
            // codecListView
            // 
            this.codecListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codecListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.codecListView.Location = new System.Drawing.Point(6, 65);
            this.codecListView.Name = "codecListView";
            this.codecListView.Size = new System.Drawing.Size(1215, 483);
            this.codecListView.TabIndex = 7;
            this.codecListView.UseCompatibleStateImageBehavior = false;
            this.codecListView.View = System.Windows.Forms.View.Details;
            this.codecListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.codecColumnHeader_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "DisplayName";
            this.columnHeader1.Width = 182;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Version";
            this.columnHeader2.Width = 210;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "File Description";
            this.columnHeader3.Width = 181;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Install/Modified Date";
            this.columnHeader4.Width = 115;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            this.columnHeader5.Width = 158;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Filename";
            this.columnHeader6.Width = 202;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Driver Key";
            this.columnHeader7.Width = 111;
            // 
            // tabPage_Application
            // 
            this.tabPage_Application.Controls.Add(this.saveApplicationBtn);
            this.tabPage_Application.Controls.Add(this.inspectApplicationBtn);
            this.tabPage_Application.Controls.Add(this.applicationListView);
            this.tabPage_Application.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Application.Name = "tabPage_Application";
            this.tabPage_Application.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Application.Size = new System.Drawing.Size(1227, 554);
            this.tabPage_Application.TabIndex = 0;
            this.tabPage_Application.Text = "Applications";
            this.tabPage_Application.UseVisualStyleBackColor = true;
            // 
            // saveApplicationBtn
            // 
            this.saveApplicationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveApplicationBtn.Location = new System.Drawing.Point(1100, 6);
            this.saveApplicationBtn.Name = "saveApplicationBtn";
            this.saveApplicationBtn.Size = new System.Drawing.Size(121, 53);
            this.saveApplicationBtn.TabIndex = 11;
            this.saveApplicationBtn.Text = "Save Application Info";
            this.saveApplicationBtn.UseVisualStyleBackColor = true;
            this.saveApplicationBtn.Click += new System.EventHandler(this.saveApplicationBtn_Click);
            // 
            // inspectApplicationBtn
            // 
            this.inspectApplicationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectApplicationBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inspectApplicationBtn.Location = new System.Drawing.Point(6, 6);
            this.inspectApplicationBtn.Name = "inspectApplicationBtn";
            this.inspectApplicationBtn.Size = new System.Drawing.Size(1088, 53);
            this.inspectApplicationBtn.TabIndex = 8;
            this.inspectApplicationBtn.Text = "Inspect Applications";
            this.inspectApplicationBtn.UseVisualStyleBackColor = true;
            this.inspectApplicationBtn.Click += new System.EventHandler(this.startButton_Click);
            // 
            // tabPage_DxDiag
            // 
            this.tabPage_DxDiag.Controls.Add(this.dxdiagPreviewTextBox);
            this.tabPage_DxDiag.Controls.Add(this.saveDxDiagButton);
            this.tabPage_DxDiag.Location = new System.Drawing.Point(4, 22);
            this.tabPage_DxDiag.Name = "tabPage_DxDiag";
            this.tabPage_DxDiag.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_DxDiag.Size = new System.Drawing.Size(1227, 554);
            this.tabPage_DxDiag.TabIndex = 1;
            this.tabPage_DxDiag.Text = "DxDiag";
            this.tabPage_DxDiag.UseVisualStyleBackColor = true;
            // 
            // dxdiagPreviewTextBox
            // 
            this.dxdiagPreviewTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dxdiagPreviewTextBox.Location = new System.Drawing.Point(6, 65);
            this.dxdiagPreviewTextBox.Name = "dxdiagPreviewTextBox";
            this.dxdiagPreviewTextBox.ReadOnly = true;
            this.dxdiagPreviewTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.dxdiagPreviewTextBox.Size = new System.Drawing.Size(1215, 483);
            this.dxdiagPreviewTextBox.TabIndex = 8;
            this.dxdiagPreviewTextBox.Text = "";
            // 
            // codecInfoSaveFileDialog
            // 
            this.codecInfoSaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.codecInfoSaveFileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 721);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.inAppLogTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 240);
            this.Name = "Form1";
            this.Text = "Clouseau - PC Inspector";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Codecs.ResumeLayout(false);
            this.tabPage_Application.ResumeLayout(false);
            this.tabPage_DxDiag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox inAppLogTextBox;
        private System.Windows.Forms.SaveFileDialog applicationInfoSaveFileDialog;
        private System.Windows.Forms.ListView applicationListView;
        private System.Windows.Forms.ColumnHeader DisplayName;
        private System.Windows.Forms.ColumnHeader DisplayVersion;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader InstallDate;
        private System.Windows.Forms.ColumnHeader RegLocation;
        private System.Windows.Forms.Button saveDxDiagButton;
        private System.Windows.Forms.SaveFileDialog dxDiagSaveFileDialog;
        private System.ComponentModel.BackgroundWorker dxdiagBackgroundWorker;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Application;
        private System.Windows.Forms.Button inspectApplicationBtn;
        private System.Windows.Forms.TabPage tabPage_DxDiag;
        private System.Windows.Forms.TabPage tabPage_Codecs;
        private System.Windows.Forms.ListView codecListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button inspectCodecBtn;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.RichTextBox dxdiagPreviewTextBox;
        private System.Windows.Forms.Button saveCodecBtn;
        private System.Windows.Forms.Button saveApplicationBtn;
        private System.Windows.Forms.SaveFileDialog codecInfoSaveFileDialog;
    }
}

