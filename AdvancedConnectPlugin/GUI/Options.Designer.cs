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
            this.tabControl.Size = new System.Drawing.Size(518, 424);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.groupBoxFieldMappings);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(510, 398);
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
            this.groupBoxFieldMappings.Size = new System.Drawing.Size(497, 385);
            this.groupBoxFieldMappings.TabIndex = 0;
            this.groupBoxFieldMappings.TabStop = false;
            this.groupBoxFieldMappings.Text = "Settings";
            // 
            // checkBoxEnableBuiltinRDP
            // 
            this.checkBoxEnableBuiltinRDP.AutoSize = true;
            this.checkBoxEnableBuiltinRDP.Location = new System.Drawing.Point(157, 102);
            this.checkBoxEnableBuiltinRDP.Name = "checkBoxEnableBuiltinRDP";
            this.checkBoxEnableBuiltinRDP.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEnableBuiltinRDP.TabIndex = 8;
            this.checkBoxEnableBuiltinRDP.UseVisualStyleBackColor = true;
            // 
            // labelEnableBuiltinRDP
            // 
            this.labelEnableBuiltinRDP.AutoSize = true;
            this.labelEnableBuiltinRDP.Location = new System.Drawing.Point(7, 102);
            this.labelEnableBuiltinRDP.Name = "labelEnableBuiltinRDP";
            this.labelEnableBuiltinRDP.Size = new System.Drawing.Size(137, 13);
            this.labelEnableBuiltinRDP.TabIndex = 7;
            this.labelEnableBuiltinRDP.Text = "Enable built-in RDP support";
            // 
            // labelMainSeparator1
            // 
            this.labelMainSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMainSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMainSeparator1.Location = new System.Drawing.Point(7, 86);
            this.labelMainSeparator1.Name = "labelMainSeparator1";
            this.labelMainSeparator1.Size = new System.Drawing.Size(484, 2);
            this.labelMainSeparator1.TabIndex = 6;
            // 
            // labelConncectionOptions
            // 
            this.labelConncectionOptions.AutoSize = true;
            this.labelConncectionOptions.Location = new System.Drawing.Point(7, 55);
            this.labelConncectionOptions.Name = "labelConncectionOptions";
            this.labelConncectionOptions.Size = new System.Drawing.Size(120, 13);
            this.labelConncectionOptions.TabIndex = 5;
            this.labelConncectionOptions.Text = "Connection options field";
            // 
            // comboBoxConncectionOptions
            // 
            this.comboBoxConncectionOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxConncectionOptions.FormattingEnabled = true;
            this.comboBoxConncectionOptions.Location = new System.Drawing.Point(157, 52);
            this.comboBoxConncectionOptions.Name = "comboBoxConncectionOptions";
            this.comboBoxConncectionOptions.Size = new System.Drawing.Size(334, 21);
            this.comboBoxConncectionOptions.TabIndex = 4;
            this.comboBoxConncectionOptions.Text = "Keepass Field";
            // 
            // comboBoxConnectionMethod
            // 
            this.comboBoxConnectionMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxConnectionMethod.FormattingEnabled = true;
            this.comboBoxConnectionMethod.Location = new System.Drawing.Point(157, 19);
            this.comboBoxConnectionMethod.Name = "comboBoxConnectionMethod";
            this.comboBoxConnectionMethod.Size = new System.Drawing.Size(334, 21);
            this.comboBoxConnectionMethod.TabIndex = 3;
            this.comboBoxConnectionMethod.Text = "Keepass Field";
            // 
            // labelConnectionMethod
            // 
            this.labelConnectionMethod.AutoSize = true;
            this.labelConnectionMethod.Location = new System.Drawing.Point(6, 22);
            this.labelConnectionMethod.Name = "labelConnectionMethod";
            this.labelConnectionMethod.Size = new System.Drawing.Size(121, 13);
            this.labelConnectionMethod.TabIndex = 2;
            this.labelConnectionMethod.Text = "Connection method field";
            // 
            // tabPageApplications
            // 
            this.tabPageApplications.Controls.Add(this.groupBoxApplications);
            this.tabPageApplications.Location = new System.Drawing.Point(4, 22);
            this.tabPageApplications.Name = "tabPageApplications";
            this.tabPageApplications.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageApplications.Size = new System.Drawing.Size(510, 398);
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
            this.groupBoxApplications.Size = new System.Drawing.Size(497, 385);
            this.groupBoxApplications.TabIndex = 0;
            this.groupBoxApplications.TabStop = false;
            this.groupBoxApplications.Text = "Applications";
            // 
            // buttonApplicationRemove
            // 
            this.buttonApplicationRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplicationRemove.Location = new System.Drawing.Point(375, 351);
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
            this.buttonApplicationAdd.Location = new System.Drawing.Point(375, 317);
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
            this.dataGridViewApplications.Size = new System.Drawing.Size(485, 292);
            this.dataGridViewApplications.TabIndex = 0;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(456, 443);
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
            this.buttonCancel.Location = new System.Drawing.Point(375, 443);
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
            this.buttonOK.Location = new System.Drawing.Point(294, 443);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelRDPConncectionAddress
            // 
            this.labelRDPConncectionAddress.AutoSize = true;
            this.labelRDPConncectionAddress.Location = new System.Drawing.Point(7, 131);
            this.labelRDPConncectionAddress.Name = "labelRDPConncectionAddress";
            this.labelRDPConncectionAddress.Size = new System.Drawing.Size(123, 13);
            this.labelRDPConncectionAddress.TabIndex = 9;
            this.labelRDPConncectionAddress.Text = "Connection address field";
            // 
            // comboBoxRDPConncectionAddress
            // 
            this.comboBoxRDPConncectionAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRDPConncectionAddress.FormattingEnabled = true;
            this.comboBoxRDPConncectionAddress.Location = new System.Drawing.Point(157, 128);
            this.comboBoxRDPConncectionAddress.Name = "comboBoxRDPConncectionAddress";
            this.comboBoxRDPConncectionAddress.Size = new System.Drawing.Size(334, 21);
            this.comboBoxRDPConncectionAddress.TabIndex = 10;
            this.comboBoxRDPConncectionAddress.Text = "Keepass Field";
            // 
            // textBoxRDPCustomParameter
            // 
            this.textBoxRDPCustomParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRDPCustomParameter.Location = new System.Drawing.Point(157, 181);
            this.textBoxRDPCustomParameter.Name = "textBoxRDPCustomParameter";
            this.textBoxRDPCustomParameter.Size = new System.Drawing.Size(334, 20);
            this.textBoxRDPCustomParameter.TabIndex = 11;
            // 
            // labelRDPConncectionMethod
            // 
            this.labelRDPConncectionMethod.AutoSize = true;
            this.labelRDPConncectionMethod.Location = new System.Drawing.Point(7, 158);
            this.labelRDPConncectionMethod.Name = "labelRDPConncectionMethod";
            this.labelRDPConncectionMethod.Size = new System.Drawing.Size(100, 13);
            this.labelRDPConncectionMethod.TabIndex = 12;
            this.labelRDPConncectionMethod.Text = "Connection Method";
            // 
            // textBoxRDPConnectionMethod
            // 
            this.textBoxRDPConnectionMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRDPConnectionMethod.Location = new System.Drawing.Point(157, 155);
            this.textBoxRDPConnectionMethod.Name = "textBoxRDPConnectionMethod";
            this.textBoxRDPConnectionMethod.Size = new System.Drawing.Size(334, 20);
            this.textBoxRDPConnectionMethod.TabIndex = 13;
            // 
            // labelRDPConnectionParameter
            // 
            this.labelRDPConnectionParameter.AutoSize = true;
            this.labelRDPConnectionParameter.Location = new System.Drawing.Point(7, 184);
            this.labelRDPConnectionParameter.Name = "labelRDPConnectionParameter";
            this.labelRDPConnectionParameter.Size = new System.Drawing.Size(130, 13);
            this.labelRDPConnectionParameter.TabIndex = 14;
            this.labelRDPConnectionParameter.Text = "Additional RDP Parameter";
            // 
            // labelRDPInfo
            // 
            this.labelRDPInfo.AutoSize = true;
            this.labelRDPInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRDPInfo.Location = new System.Drawing.Point(7, 216);
            this.labelRDPInfo.Name = "labelRDPInfo";
            this.labelRDPInfo.Size = new System.Drawing.Size(190, 13);
            this.labelRDPInfo.TabIndex = 15;
            this.labelRDPInfo.Text = "Windows operating systems only";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(7, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 2);
            this.label1.TabIndex = 16;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 478);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(559, 517);
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
    }
}