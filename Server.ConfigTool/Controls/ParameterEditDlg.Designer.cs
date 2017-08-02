namespace CAS.CommServer.DA.Server.ConfigTool
{
    partial class ParameterEditDlg
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
            this.ValueLB = new System.Windows.Forms.Label();
            this.ValueTB = new System.Windows.Forms.TextBox();
            this.NameLB = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.ButtonsPN.SuspendLayout();
            this.MainPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonsPN
            // 
            this.ButtonsPN.Controls.Add(this.CancelBTN);
            this.ButtonsPN.Controls.Add(this.OkBTN);
            this.ButtonsPN.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPN.Location = new System.Drawing.Point(0, 51);
            this.ButtonsPN.Name = "ButtonsPN";
            this.ButtonsPN.Size = new System.Drawing.Size(290, 28);
            this.ButtonsPN.TabIndex = 0;
            // 
            // CancelBTN
            // 
            this.CancelBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBTN.Location = new System.Drawing.Point(212, 3);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 0;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            // 
            // OkBTN
            // 
            this.OkBTN.Location = new System.Drawing.Point(3, 3);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 1;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // MainPN
            // 
            this.MainPN.Controls.Add(this.ValueLB);
            this.MainPN.Controls.Add(this.ValueTB);
            this.MainPN.Controls.Add(this.NameLB);
            this.MainPN.Controls.Add(this.NameTB);
            this.MainPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPN.Location = new System.Drawing.Point(0, 0);
            this.MainPN.Name = "MainPN";
            this.MainPN.Size = new System.Drawing.Size(290, 51);
            this.MainPN.TabIndex = 1;
            // 
            // ValueLB
            // 
            this.ValueLB.AutoSize = true;
            this.ValueLB.Location = new System.Drawing.Point(4, 32);
            this.ValueLB.Name = "ValueLB";
            this.ValueLB.Size = new System.Drawing.Size(34, 13);
            this.ValueLB.TabIndex = 2;
            this.ValueLB.Text = "Value";
            this.ValueLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ValueTB
            // 
            this.ValueTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTB.Location = new System.Drawing.Point(45, 28);
            this.ValueTB.Name = "ValueTB";
            this.ValueTB.Size = new System.Drawing.Size(242, 20);
            this.ValueTB.TabIndex = 3;
            // 
            // NameLB
            // 
            this.NameLB.AutoSize = true;
            this.NameLB.Location = new System.Drawing.Point(4, 8);
            this.NameLB.Name = "NameLB";
            this.NameLB.Size = new System.Drawing.Size(35, 13);
            this.NameLB.TabIndex = 0;
            this.NameLB.Text = "Name";
            this.NameLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NameTB
            // 
            this.NameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTB.Location = new System.Drawing.Point(45, 4);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(242, 20);
            this.NameTB.TabIndex = 1;
            // 
            // ParameterEditDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 79);
            this.Controls.Add(this.MainPN);
            this.Controls.Add(this.ButtonsPN);
            this.Name = "ParameterEditDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Parameter";
            this.ButtonsPN.ResumeLayout(false);
            this.MainPN.ResumeLayout(false);
            this.MainPN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ButtonsPN;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.Panel MainPN;
        private System.Windows.Forms.Label ValueLB;
        private System.Windows.Forms.TextBox ValueTB;
        private System.Windows.Forms.Label NameLB;
        private System.Windows.Forms.TextBox NameTB;
    }
}