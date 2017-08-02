namespace CAS.CommServer.DA.Server.ConfigTool
{
    partial class RegisterServerDlg
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
            this.ButtonsPN = new System.Windows.Forms.Panel();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.OkBTN = new System.Windows.Forms.Button();
            this.MainPN = new System.Windows.Forms.Panel();
            this.ParamtersGB = new System.Windows.Forms.GroupBox();
            this.ParametersCTRL = new ParameterListControl();
            this.ProgIdLB = new System.Windows.Forms.Label();
            this.BrowseBTN = new System.Windows.Forms.Button();
            this.ProgIdTB = new System.Windows.Forms.TextBox();
            this.DotNetServerCB = new System.Windows.Forms.ComboBox();
            this.DescriptionLB = new System.Windows.Forms.Label();
            this.DotNetServerLB = new System.Windows.Forms.Label();
            this.DescriptionTB = new System.Windows.Forms.TextBox();
            this.WrapperCB = new System.Windows.Forms.ComboBox();
            this.ClsidLB = new System.Windows.Forms.Label();
            this.WrapperLB = new System.Windows.Forms.Label();
            this.ClsidTB = new System.Windows.Forms.TextBox();
            this.NewClsidBTN = new System.Windows.Forms.Button();
            this.SpecificationsLB = new System.Windows.Forms.Label();
            this.SpecificationsTB = new System.Windows.Forms.TextBox();
            this.ButtonsPN.SuspendLayout();
            this.MainPN.SuspendLayout();
            this.ParamtersGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonsPN
            // 
            this.ButtonsPN.Controls.Add(this.CancelBTN);
            this.ButtonsPN.Controls.Add(this.OkBTN);
            this.ButtonsPN.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPN.Location = new System.Drawing.Point(0, 324);
            this.ButtonsPN.Name = "ButtonsPN";
            this.ButtonsPN.Size = new System.Drawing.Size(487, 28);
            this.ButtonsPN.TabIndex = 0;
            // 
            // CancelBTN
            // 
            this.CancelBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBTN.Location = new System.Drawing.Point(409, 3);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 1;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            // 
            // OkBTN
            // 
            this.OkBTN.Location = new System.Drawing.Point(3, 3);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 0;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // MainPN
            // 
            this.MainPN.Controls.Add(this.SpecificationsTB);
            this.MainPN.Controls.Add(this.SpecificationsLB);
            this.MainPN.Controls.Add(this.ParamtersGB);
            this.MainPN.Controls.Add(this.ProgIdLB);
            this.MainPN.Controls.Add(this.BrowseBTN);
            this.MainPN.Controls.Add(this.ProgIdTB);
            this.MainPN.Controls.Add(this.DotNetServerCB);
            this.MainPN.Controls.Add(this.DescriptionLB);
            this.MainPN.Controls.Add(this.DotNetServerLB);
            this.MainPN.Controls.Add(this.DescriptionTB);
            this.MainPN.Controls.Add(this.WrapperCB);
            this.MainPN.Controls.Add(this.ClsidLB);
            this.MainPN.Controls.Add(this.WrapperLB);
            this.MainPN.Controls.Add(this.ClsidTB);
            this.MainPN.Controls.Add(this.NewClsidBTN);
            this.MainPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPN.Location = new System.Drawing.Point(0, 0);
            this.MainPN.Name = "MainPN";
            this.MainPN.Size = new System.Drawing.Size(487, 324);
            this.MainPN.TabIndex = 1;
            // 
            // ParamtersGB
            // 
            this.ParamtersGB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ParamtersGB.Controls.Add(this.ParametersCTRL);
            this.ParamtersGB.Location = new System.Drawing.Point(7, 150);
            this.ParamtersGB.Name = "ParamtersGB";
            this.ParamtersGB.Size = new System.Drawing.Size(475, 171);
            this.ParamtersGB.TabIndex = 12;
            this.ParamtersGB.TabStop = false;
            this.ParamtersGB.Text = "Parameters";
            // 
            // ParametersCTRL
            // 
            this.ParametersCTRL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParametersCTRL.EnableDragging = false;
            this.ParametersCTRL.Instructions = "Right click to add parameters.";
            this.ParametersCTRL.Location = new System.Drawing.Point(3, 16);
            this.ParametersCTRL.Name = "ParametersCTRL";
            this.ParametersCTRL.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ParametersCTRL.PrependItems = false;
            this.ParametersCTRL.Size = new System.Drawing.Size(469, 152);
            this.ParametersCTRL.TabIndex = 0;
            // 
            // ProgIdLB
            // 
            this.ProgIdLB.AutoSize = true;
            this.ProgIdLB.Location = new System.Drawing.Point(4, 80);
            this.ProgIdLB.Name = "ProgIdLB";
            this.ProgIdLB.Size = new System.Drawing.Size(43, 13);
            this.ProgIdLB.TabIndex = 8;
            this.ProgIdLB.Text = "Prog ID";
            this.ProgIdLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BrowseBTN
            // 
            this.BrowseBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseBTN.Location = new System.Drawing.Point(422, 3);
            this.BrowseBTN.Name = "BrowseBTN";
            this.BrowseBTN.Size = new System.Drawing.Size(62, 23);
            this.BrowseBTN.TabIndex = 2;
            this.BrowseBTN.Text = "Browse...";
            this.BrowseBTN.UseVisualStyleBackColor = true;
            this.BrowseBTN.Click += new System.EventHandler(this.BrowseBTN_Click);
            // 
            // ProgIdTB
            // 
            this.ProgIdTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgIdTB.Location = new System.Drawing.Point(96, 76);
            this.ProgIdTB.Name = "ProgIdTB";
            this.ProgIdTB.Size = new System.Drawing.Size(322, 20);
            this.ProgIdTB.TabIndex = 9;
            // 
            // DotNetServerCB
            // 
            this.DotNetServerCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DotNetServerCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DotNetServerCB.FormattingEnabled = true;
            this.DotNetServerCB.Location = new System.Drawing.Point(96, 4);
            this.DotNetServerCB.Name = "DotNetServerCB";
            this.DotNetServerCB.Size = new System.Drawing.Size(322, 21);
            this.DotNetServerCB.TabIndex = 1;
            this.DotNetServerCB.SelectedIndexChanged += new System.EventHandler(this.DotNetServerCB_SelectedIndexChanged);
            // 
            // DescriptionLB
            // 
            this.DescriptionLB.AutoSize = true;
            this.DescriptionLB.Location = new System.Drawing.Point(4, 104);
            this.DescriptionLB.Name = "DescriptionLB";
            this.DescriptionLB.Size = new System.Drawing.Size(60, 13);
            this.DescriptionLB.TabIndex = 10;
            this.DescriptionLB.Text = "Description";
            this.DescriptionLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DotNetServerLB
            // 
            this.DotNetServerLB.AutoSize = true;
            this.DotNetServerLB.Location = new System.Drawing.Point(4, 7);
            this.DotNetServerLB.Name = "DotNetServerLB";
            this.DotNetServerLB.Size = new System.Drawing.Size(91, 13);
            this.DotNetServerLB.TabIndex = 0;
            this.DotNetServerLB.Text = ".NET OPC Server";
            this.DotNetServerLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DescriptionTB
            // 
            this.DescriptionTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionTB.Location = new System.Drawing.Point(96, 100);
            this.DescriptionTB.Name = "DescriptionTB";
            this.DescriptionTB.Size = new System.Drawing.Size(322, 20);
            this.DescriptionTB.TabIndex = 11;
            // 
            // WrapperCB
            // 
            this.WrapperCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WrapperCB.FormattingEnabled = true;
            this.WrapperCB.Location = new System.Drawing.Point(96, 28);
            this.WrapperCB.Name = "WrapperCB";
            this.WrapperCB.Size = new System.Drawing.Size(322, 21);
            this.WrapperCB.TabIndex = 4;
            // 
            // ClsidLB
            // 
            this.ClsidLB.AutoSize = true;
            this.ClsidLB.Location = new System.Drawing.Point(4, 56);
            this.ClsidLB.Name = "ClsidLB";
            this.ClsidLB.Size = new System.Drawing.Size(38, 13);
            this.ClsidLB.TabIndex = 5;
            this.ClsidLB.Text = "CLSID";
            this.ClsidLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WrapperLB
            // 
            this.WrapperLB.AutoSize = true;
            this.WrapperLB.Location = new System.Drawing.Point(4, 32);
            this.WrapperLB.Name = "WrapperLB";
            this.WrapperLB.Size = new System.Drawing.Size(89, 13);
            this.WrapperLB.TabIndex = 3;
            this.WrapperLB.Text = "Wrapper Process";
            this.WrapperLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ClsidTB
            // 
            this.ClsidTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ClsidTB.Location = new System.Drawing.Point(96, 52);
            this.ClsidTB.Name = "ClsidTB";
            this.ClsidTB.Size = new System.Drawing.Size(322, 20);
            this.ClsidTB.TabIndex = 6;
            // 
            // NewClsidBTN
            // 
            this.NewClsidBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewClsidBTN.Location = new System.Drawing.Point(422, 51);
            this.NewClsidBTN.Name = "NewClsidBTN";
            this.NewClsidBTN.Size = new System.Drawing.Size(62, 23);
            this.NewClsidBTN.TabIndex = 7;
            this.NewClsidBTN.Text = "New";
            this.NewClsidBTN.UseVisualStyleBackColor = true;
            this.NewClsidBTN.Click += new System.EventHandler(this.NewClsidBTN_Click);
            // 
            // SpecificationsLB
            // 
            this.SpecificationsLB.AutoSize = true;
            this.SpecificationsLB.Location = new System.Drawing.Point(4, 128);
            this.SpecificationsLB.Name = "SpecificationsLB";
            this.SpecificationsLB.Size = new System.Drawing.Size(73, 13);
            this.SpecificationsLB.TabIndex = 13;
            this.SpecificationsLB.Text = "Specifications";
            this.SpecificationsLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpecificationsTB
            // 
            this.SpecificationsTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SpecificationsTB.BackColor = System.Drawing.SystemColors.Window;
            this.SpecificationsTB.Location = new System.Drawing.Point(96, 124);
            this.SpecificationsTB.Name = "SpecificationsTB";
            this.SpecificationsTB.ReadOnly = true;
            this.SpecificationsTB.Size = new System.Drawing.Size(99, 20);
            this.SpecificationsTB.TabIndex = 14;
            // 
            // RegisterServerDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 352);
            this.Controls.Add(this.MainPN);
            this.Controls.Add(this.ButtonsPN);
            this.Name = "RegisterServerDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Register .NET OPC Server";
            this.ButtonsPN.ResumeLayout(false);
            this.MainPN.ResumeLayout(false);
            this.MainPN.PerformLayout();
            this.ParamtersGB.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ButtonsPN;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.Panel MainPN;
        private System.Windows.Forms.GroupBox ParamtersGB;
        private System.Windows.Forms.Label ProgIdLB;
        private System.Windows.Forms.Button BrowseBTN;
        private System.Windows.Forms.TextBox ProgIdTB;
        private System.Windows.Forms.ComboBox DotNetServerCB;
        private System.Windows.Forms.Label DescriptionLB;
        private System.Windows.Forms.Label DotNetServerLB;
        private System.Windows.Forms.TextBox DescriptionTB;
        private System.Windows.Forms.ComboBox WrapperCB;
        private System.Windows.Forms.Label ClsidLB;
        private System.Windows.Forms.Label WrapperLB;
        private System.Windows.Forms.TextBox ClsidTB;
        private System.Windows.Forms.Button NewClsidBTN;
        private ParameterListControl ParametersCTRL;
        private System.Windows.Forms.TextBox SpecificationsTB;
        private System.Windows.Forms.Label SpecificationsLB;
    }
}