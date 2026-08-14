namespace AdvancedConnectPlugin.GUI
{
    partial class Options
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.groupBoxFieldMappings = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableBuiltinRDP = new System.Windows.Forms.CheckBox();
            this.labelEnableBuiltinRDP = new System.Windows.Forms.Label();
            this.labelMainSeparator1 = new System.Windows.Forms.Label();
            this.labelConncectionOptions = new System.Windows.Forms.Label();
            this.comboBoxConncectionOptions = new System.Windows.Forms.ComboBox();
            this.comboBoxConnectionMethod = new System.Windows.Forms.ComboBox();
            this.labelConnectionMethod = new System.Windows.Forms.Label();
            this.tabPageApplications = new System.Windows.Forms.TabPage();
            this.groupBoxApplications = new System.Windows.Forms.GroupBox();
            this.buttonApplicationRemove = new System.Windows.Forms.Button();
            this.buttonApplicationAdd = new System.Windows.Forms.Button();
            this.dataGridViewApplications = new System.Windows.Forms.DataGridView();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelRDPConncectionAddress = new System.Windows.Forms.Label();
            this.comboBoxRDPConncectionAddress = new System.Windows.Forms.ComboBox();
            this.textBoxRDPCustomParameter = new System.Windows.Forms.TextBox();
            this.labelRDPConncectionMethod = new System.Windows.Forms.Label();
            this.textBoxRDPConnectionMethod = new System.Windows.Forms.TextBox();
            this.labelRDPConnectionParameter = new System.Windows.Forms.Label();
            this.labelRDPInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSuppressUnresolvedFieldWarning = new System.Windows.Forms.CheckBox();
            this.labelSuppressUnresolvedFieldWarning = new System.Windows.Forms.Label();
            this.labelRDPCredentialCleanupDelay = new System.Windows.Forms.Label();
            this.textBoxRDPCredentialCleanupDelay = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.groupBoxFieldMappings.SuspendLayout();
            this.tabPageApplications.SuspendLayout();
            this.groupBoxApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewApplications)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPageApplications);
            this.tabControl.Location = new System.Drawing.Point(13, 13);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(777, 636);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.groupBoxFieldMappings);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(765, 597);
            this.tabPageMain.TabIndex = 1;
            this.tabPageMain.Text = "Main";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // groupBoxFieldMappings
            // 
            this.groupBoxFieldMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFieldMappings.Controls.Add(this.label1);
            this.groupBoxFieldMappings.Controls.Add(this.labelRDPInfo);
            this.groupBoxFieldMappings.Controls.Add(this.labelRDPConnectionParameter);
            this.groupBoxFieldMappings.Controls.Add(this.checkBoxSuppressUnresolvedFieldWarning);
            this.groupBoxFieldMappings.Controls.Add(this.labelSuppressUnresolvedFieldWarning);
            this.groupBoxFieldMappings.Controls.Add(this.textBoxRDPCredentialCleanupDelay);
            this.groupBoxFieldMappings.Controls.Add(this.labelRDPCredentialCleanupDelay);
            this.groupBoxFieldMappings.Controls.Add(this.textBoxRDPConnectionMethod);
            this.groupBoxFieldMappings.Controls.Add(this.labelRDPConncectionMethod);
            this.groupBoxFieldMappings.Controls.Add(this.textBoxRDPCustomParameter);
            this.groupBoxFieldMappings.Controls.Add(this.comboBoxRDPConncectionAddress);
            this.groupBoxFieldMappings.Controls.Add(this.labelRDPConncectionAddress);
            this.groupBoxFieldMappings.Controls.Add(this.checkBoxEnableBuiltinRDP);
            this.groupBoxFieldMappings.Controls.Add(this.labelEnableBuiltinRDP);
            this.groupBoxFieldMappings.Controls.Add(this.labelMainSeparator1);
            this.groupBoxFieldMappings.Controls.Add(this.labelConncectionOptions);
            this.groupBoxFieldMappings.Controls.Add(this.comboBoxConncectionOptions);
            this.groupBoxFieldMappings.Controls.Add(this.comboBoxConnectionMethod);
            this.groupBoxFieldMappings.Controls.Add(this.labelConnectionMethod);
            this.groupBoxFieldMappings.Location = new System.Drawing.Point(7, 7);
            this.groupBoxFieldMappings.Name = "groupBoxFieldMappings";
            this.groupBoxFieldMappings.Size = new System.Drawing.Size(746, 578);
            this.groupBoxFieldMappings.TabIndex = 0;
            this.groupBoxFieldMappings.TabStop = false;
            this.groupBoxFieldMappings.Text = "Settings";
            // 
            // checkBoxEnableBuiltinRDP
            // 
            this.checkBoxEnableBuiltinRDP.AutoSize = true;
            this.checkBoxEnableBuiltinRDP.Location = new System.Drawing.Point(232, 162);
            this.checkBoxEnableBuiltinRDP.Name = "checkBoxEnableBuiltinRDP";
            this.checkBoxEnableBuiltinRDP.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEnableBuiltinRDP.TabIndex = 12;
            this.checkBoxEnableBuiltinRDP.UseVisualStyleBackColor = true;
            // 
            // labelEnableBuiltinRDP
            // 
            this.labelEnableBuiltinRDP.AutoSize = false;
            this.labelEnableBuiltinRDP.Location = new System.Drawing.Point(7, 162);
            this.labelEnableBuiltinRDP.Name = "labelEnableBuiltinRDP";
            this.labelEnableBuiltinRDP.Size = new System.Drawing.Size(206, 13);
            this.labelEnableBuiltinRDP.TabIndex = 11;
            this.labelEnableBuiltinRDP.Text = "Enable built-in RDP support";
            // 
            // labelMainSeparator1
            // 
            this.labelMainSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMainSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMainSeparator1.Location = new System.Drawing.Point(7, 146);
            this.labelMainSeparator1.Name = "labelMainSeparator1";
            this.labelMainSeparator1.Size = new System.Drawing.Size(733, 2);
            this.labelMainSeparator1.TabIndex = 10;
            // 
            // labelConncectionOptions
            // 
            this.labelConncectionOptions.AutoSize = false;
            this.labelConncectionOptions.Location = new System.Drawing.Point(7, 55);
            this.labelConncectionOptions.Name = "labelConncectionOptions";
            this.labelConncectionOptions.Size = new System.Drawing.Size(180, 13);
            this.labelConncectionOptions.TabIndex = 4;
            this.labelConncectionOptions.Text = "Connection options field";
            // 
            // comboBoxConncectionOptions
            // 
            this.comboBoxConncectionOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxConncectionOptions.FormattingEnabled = true;
            this.comboBoxConncectionOptions.Location = new System.Drawing.Point(232, 52);
            this.comboBoxConncectionOptions.Name = "comboBoxConncectionOptions";
            this.comboBoxConncectionOptions.Size = new System.Drawing.Size(508, 21);
            this.comboBoxConncectionOptions.TabIndex = 5;
            this.comboBoxConncectionOptions.Text = "Keepass Field";
            // 
            // comboBoxConnectionMethod
            // 
            this.comboBoxConnectionMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxConnectionMethod.FormattingEnabled = true;
            this.comboBoxConnectionMethod.Location = new System.Drawing.Point(232, 19);
            this.comboBoxConnectionMethod.Name = "comboBoxConnectionMethod";
            this.comboBoxConnectionMethod.Size = new System.Drawing.Size(508, 21);
            this.comboBoxConnectionMethod.TabIndex = 3;
            this.comboBoxConnectionMethod.Text = "Keepass Field";
            // 
            // labelConnectionMethod
            // 
            this.labelConnectionMethod.AutoSize = false;
            this.labelConnectionMethod.Location = new System.Drawing.Point(6, 22);
            this.labelConnectionMethod.Name = "labelConnectionMethod";
            this.labelConnectionMethod.Size = new System.Drawing.Size(182, 13);
            this.labelConnectionMethod.TabIndex = 2;
            this.labelConnectionMethod.Text = "Connection method field";
            // 
            // tabPageApplications
            // 
            this.tabPageApplications.Controls.Add(this.groupBoxApplications);
            this.tabPageApplications.Location = new System.Drawing.Point(4, 22);
            this.tabPageApplications.Name = "tabPageApplications";
            this.tabPageApplications.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageApplications.Size = new System.Drawing.Size(765, 597);
            this.tabPageApplications.TabIndex = 2;
            this.tabPageApplications.Text = "Applications";
            this.tabPageApplications.UseVisualStyleBackColor = true;
            // 
            // groupBoxApplications
            // 
            this.groupBoxApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxApplications.Controls.Add(this.buttonApplicationRemove);
            this.groupBoxApplications.Controls.Add(this.buttonApplicationAdd);
            this.groupBoxApplications.Controls.Add(this.dataGridViewApplications);
            this.groupBoxApplications.Location = new System.Drawing.Point(7, 7);
            this.groupBoxApplications.Name = "groupBoxApplications";
            this.groupBoxApplications.Size = new System.Drawing.Size(746, 578);
            this.groupBoxApplications.TabIndex = 0;
            this.groupBoxApplications.TabStop = false;
            this.groupBoxApplications.Text = "Applications";
            // 
            // buttonApplicationRemove
            // 
            this.buttonApplicationRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplicationRemove.Location = new System.Drawing.Point(624, 544);
            this.buttonApplicationRemove.Name = "buttonApplicationRemove";
            this.buttonApplicationRemove.Size = new System.Drawing.Size(116, 28);
            this.buttonApplicationRemove.TabIndex = 2;
            this.buttonApplicationRemove.Text = "Remove";
            this.buttonApplicationRemove.UseVisualStyleBackColor = true;
            this.buttonApplicationRemove.Click += new System.EventHandler(this.buttonApplicationRemove_Click);
            // 
            // buttonApplicationAdd
            // 
            this.buttonApplicationAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplicationAdd.Location = new System.Drawing.Point(624, 510);
            this.buttonApplicationAdd.Name = "buttonApplicationAdd";
            this.buttonApplicationAdd.Size = new System.Drawing.Size(116, 28);
            this.buttonApplicationAdd.TabIndex = 0;
            this.buttonApplicationAdd.Text = "Add";
            this.buttonApplicationAdd.UseVisualStyleBackColor = true;
            this.buttonApplicationAdd.Click += new System.EventHandler(this.buttonApplicationAdd_Click);
            // 
            // dataGridViewApplications
            // 
            this.dataGridViewApplications.AllowUserToAddRows = false;
            this.dataGridViewApplications.AllowUserToDeleteRows = false;
            this.dataGridViewApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewApplications.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.dataGridViewApplications.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewApplications.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewApplications.Name = "dataGridViewApplications";
            this.dataGridViewApplications.RowHeadersVisible = false;
            this.dataGridViewApplications.Size = new System.Drawing.Size(734, 485);
            this.dataGridViewApplications.TabIndex = 0;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(728, 682);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(647, 682);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(566, 682);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelRDPConncectionAddress
            // 
            this.labelRDPConncectionAddress.AutoSize = false;
            this.labelRDPConncectionAddress.Location = new System.Drawing.Point(7, 191);
            this.labelRDPConncectionAddress.Name = "labelRDPConncectionAddress";
            this.labelRDPConncectionAddress.Size = new System.Drawing.Size(185, 13);
            this.labelRDPConncectionAddress.TabIndex = 13;
            this.labelRDPConncectionAddress.Text = "Connection address field";
            // 
            // comboBoxRDPConncectionAddress
            // 
            this.comboBoxRDPConncectionAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRDPConncectionAddress.FormattingEnabled = true;
            this.comboBoxRDPConncectionAddress.Location = new System.Drawing.Point(232, 188);
            this.comboBoxRDPConncectionAddress.Name = "comboBoxRDPConncectionAddress";
            this.comboBoxRDPConncectionAddress.Size = new System.Drawing.Size(508, 21);
            this.comboBoxRDPConncectionAddress.TabIndex = 14;
            this.comboBoxRDPConncectionAddress.Text = "Keepass Field";
            // 
            // textBoxRDPCustomParameter
            // 
            this.textBoxRDPCustomParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRDPCustomParameter.Location = new System.Drawing.Point(232, 241);
            this.textBoxRDPCustomParameter.Name = "textBoxRDPCustomParameter";
            this.textBoxRDPCustomParameter.Size = new System.Drawing.Size(508, 20);
            this.textBoxRDPCustomParameter.TabIndex = 18;
            // 
            // labelRDPConncectionMethod
            // 
            this.labelRDPConncectionMethod.AutoSize = false;
            this.labelRDPConncectionMethod.Location = new System.Drawing.Point(7, 218);
            this.labelRDPConncectionMethod.Name = "labelRDPConncectionMethod";
            this.labelRDPConncectionMethod.Size = new System.Drawing.Size(150, 13);
            this.labelRDPConncectionMethod.TabIndex = 15;
            this.labelRDPConncectionMethod.Text = "Connection Method";
            // 
            // textBoxRDPConnectionMethod
            // 
            this.textBoxRDPConnectionMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRDPConnectionMethod.Location = new System.Drawing.Point(232, 215);
            this.textBoxRDPConnectionMethod.Name = "textBoxRDPConnectionMethod";
            this.textBoxRDPConnectionMethod.Size = new System.Drawing.Size(508, 20);
            this.textBoxRDPConnectionMethod.TabIndex = 16;
            // 
            // labelRDPConnectionParameter
            // 
            this.labelRDPConnectionParameter.AutoSize = false;
            this.labelRDPConnectionParameter.Location = new System.Drawing.Point(7, 244);
            this.labelRDPConnectionParameter.Name = "labelRDPConnectionParameter";
            this.labelRDPConnectionParameter.Size = new System.Drawing.Size(195, 13);
            this.labelRDPConnectionParameter.TabIndex = 17;
            this.labelRDPConnectionParameter.Text = "Additional RDP Parameter";
            // 
            // labelRDPInfo
            // 
            this.labelRDPInfo.AutoSize = false;
            this.labelRDPInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRDPInfo.Location = new System.Drawing.Point(7, 276);
            this.labelRDPInfo.Name = "labelRDPInfo";
            this.labelRDPInfo.Size = new System.Drawing.Size(285, 13);
            this.labelRDPInfo.TabIndex = 19;
            this.labelRDPInfo.Text = "Windows operating systems only";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(7, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(733, 2);
            this.label1.TabIndex = 20;
            // 
            // labelSuppressUnresolvedFieldWarning
            // 
            this.labelSuppressUnresolvedFieldWarning.AutoSize = false;
            this.labelSuppressUnresolvedFieldWarning.Location = new System.Drawing.Point(7, 87);
            this.labelSuppressUnresolvedFieldWarning.Name = "labelSuppressUnresolvedFieldWarning";
            this.labelSuppressUnresolvedFieldWarning.Size = new System.Drawing.Size(216, 13);
            this.labelSuppressUnresolvedFieldWarning.TabIndex = 6;
            this.labelSuppressUnresolvedFieldWarning.Text = "No unresolved field warning";
            // 
            // checkBoxSuppressUnresolvedFieldWarning
            // 
            this.checkBoxSuppressUnresolvedFieldWarning.AutoSize = true;
            this.checkBoxSuppressUnresolvedFieldWarning.Location = new System.Drawing.Point(232, 87);
            this.checkBoxSuppressUnresolvedFieldWarning.Name = "checkBoxSuppressUnresolvedFieldWarning";
            this.checkBoxSuppressUnresolvedFieldWarning.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSuppressUnresolvedFieldWarning.TabIndex = 7;
            this.checkBoxSuppressUnresolvedFieldWarning.UseVisualStyleBackColor = true;
            // 
            // labelRDPCredentialCleanupDelay
            // 
            this.labelRDPCredentialCleanupDelay.AutoSize = false;
            this.labelRDPCredentialCleanupDelay.Location = new System.Drawing.Point(7, 117);
            this.labelRDPCredentialCleanupDelay.Name = "labelRDPCredentialCleanupDelay";
            this.labelRDPCredentialCleanupDelay.Size = new System.Drawing.Size(216, 13);
            this.labelRDPCredentialCleanupDelay.TabIndex = 8;
            this.labelRDPCredentialCleanupDelay.Text = "RDP credential cleanup (ms)";
            // 
            // textBoxRDPCredentialCleanupDelay
            // 
            this.textBoxRDPCredentialCleanupDelay.Location = new System.Drawing.Point(232, 114);
            this.textBoxRDPCredentialCleanupDelay.Name = "textBoxRDPCredentialCleanupDelay";
            this.textBoxRDPCredentialCleanupDelay.Size = new System.Drawing.Size(120, 20);
            this.textBoxRDPCredentialCleanupDelay.TabIndex = 9;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 717);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(839, 776);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tabControl.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.groupBoxFieldMappings.ResumeLayout(false);
            this.groupBoxFieldMappings.PerformLayout();
            this.tabPageApplications.ResumeLayout(false);
            this.groupBoxApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewApplications)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.GroupBox groupBoxFieldMappings;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelConncectionOptions;
        private System.Windows.Forms.ComboBox comboBoxConncectionOptions;
        private System.Windows.Forms.ComboBox comboBoxConnectionMethod;
        private System.Windows.Forms.Label labelConnectionMethod;
        private System.Windows.Forms.TabPage tabPageApplications;
        private System.Windows.Forms.GroupBox groupBoxApplications;
        private System.Windows.Forms.DataGridView dataGridViewApplications;
        private System.Windows.Forms.Button buttonApplicationRemove;
        private System.Windows.Forms.Button buttonApplicationAdd;
        private System.Windows.Forms.Label labelMainSeparator1;
        private System.Windows.Forms.CheckBox checkBoxEnableBuiltinRDP;
        private System.Windows.Forms.Label labelEnableBuiltinRDP;
        private System.Windows.Forms.Label labelRDPConncectionAddress;
        private System.Windows.Forms.ComboBox comboBoxRDPConncectionAddress;
        private System.Windows.Forms.TextBox textBoxRDPCustomParameter;
        private System.Windows.Forms.TextBox textBoxRDPConnectionMethod;
        private System.Windows.Forms.Label labelRDPConncectionMethod;
        private System.Windows.Forms.Label labelRDPConnectionParameter;
        private System.Windows.Forms.Label labelRDPInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSuppressUnresolvedFieldWarning;
        private System.Windows.Forms.Label labelSuppressUnresolvedFieldWarning;
        private System.Windows.Forms.Label labelRDPCredentialCleanupDelay;
        private System.Windows.Forms.TextBox textBoxRDPCredentialCleanupDelay;
    }
}