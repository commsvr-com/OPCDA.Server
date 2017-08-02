namespace CAS.CommServer.DA.Server.ConfigTool
{
  partial class ComServerListDlg
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
      this.MainPN = new System.Windows.Forms.Panel();
      this.SelectorPN = new System.Windows.Forms.Panel();
      this.RegisteredServersRB = new System.Windows.Forms.RadioButton();
      this.WrappersRB = new System.Windows.Forms.RadioButton();
      this.DotNetServersRB = new System.Windows.Forms.RadioButton();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.TasksMI = new System.Windows.Forms.ToolStripMenuItem();
      this.RegisterServerMI = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.ExitMI = new System.Windows.Forms.ToolStripMenuItem();
      this.ServersCTRL = new ComServerListUserControl();
      this.ExportMI = new System.Windows.Forms.ToolStripMenuItem();
      this.MainPN.SuspendLayout();
      this.SelectorPN.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPN
      // 
      this.MainPN.Controls.Add(this.ServersCTRL);
      this.MainPN.Controls.Add(this.SelectorPN);
      this.MainPN.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPN.Location = new System.Drawing.Point(0, 24);
      this.MainPN.Name = "MainPN";
      this.MainPN.Padding = new System.Windows.Forms.Padding(3);
      this.MainPN.Size = new System.Drawing.Size(681, 249);
      this.MainPN.TabIndex = 12;
      // 
      // SelectorPN
      // 
      this.SelectorPN.Controls.Add(this.RegisteredServersRB);
      this.SelectorPN.Controls.Add(this.WrappersRB);
      this.SelectorPN.Controls.Add(this.DotNetServersRB);
      this.SelectorPN.Dock = System.Windows.Forms.DockStyle.Top;
      this.SelectorPN.Location = new System.Drawing.Point(3, 3);
      this.SelectorPN.Name = "SelectorPN";
      this.SelectorPN.Size = new System.Drawing.Size(675, 27);
      this.SelectorPN.TabIndex = 4;
      // 
      // RegisteredServersRB
      // 
      this.RegisteredServersRB.AutoSize = true;
      this.RegisteredServersRB.Location = new System.Drawing.Point(4, 4);
      this.RegisteredServersRB.Name = "RegisteredServersRB";
      this.RegisteredServersRB.Size = new System.Drawing.Size(115, 17);
      this.RegisteredServersRB.TabIndex = 0;
      this.RegisteredServersRB.TabStop = true;
      this.RegisteredServersRB.Text = "Registered Servers";
      this.RegisteredServersRB.UseVisualStyleBackColor = true;
      this.RegisteredServersRB.CheckedChanged += new System.EventHandler(this.RegisteredServersRB_CheckedChanged);
      // 
      // WrappersRB
      // 
      this.WrappersRB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
      this.WrappersRB.AutoSize = true;
      this.WrappersRB.Location = new System.Drawing.Point(278, 4);
      this.WrappersRB.Name = "WrappersRB";
      this.WrappersRB.Size = new System.Drawing.Size(118, 17);
      this.WrappersRB.TabIndex = 1;
      this.WrappersRB.TabStop = true;
      this.WrappersRB.Text = "Wrapper Processes";
      this.WrappersRB.UseVisualStyleBackColor = true;
      this.WrappersRB.CheckedChanged += new System.EventHandler(this.WrappersRB_CheckedChanged);
      // 
      // DotNetServersRB
      // 
      this.DotNetServersRB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.DotNetServersRB.AutoSize = true;
      this.DotNetServersRB.Location = new System.Drawing.Point(558, 4);
      this.DotNetServersRB.Name = "DotNetServersRB";
      this.DotNetServersRB.Size = new System.Drawing.Size(114, 17);
      this.DotNetServersRB.TabIndex = 2;
      this.DotNetServersRB.TabStop = true;
      this.DotNetServersRB.Text = ".NET OPC Servers";
      this.DotNetServersRB.UseVisualStyleBackColor = true;
      this.DotNetServersRB.CheckedChanged += new System.EventHandler(this.DotNetServersRB_CheckedChanged);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TasksMI});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(681, 24);
      this.menuStrip1.TabIndex = 14;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // TasksMI
      // 
      this.TasksMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RegisterServerMI,
            this.ExportMI,
            this.toolStripMenuItem1,
            this.ExitMI});
      this.TasksMI.Name = "TasksMI";
      this.TasksMI.Size = new System.Drawing.Size(46, 20);
      this.TasksMI.Text = "Tasks";
      // 
      // RegisterServerMI
      // 
      this.RegisterServerMI.Name = "RegisterServerMI";
      this.RegisterServerMI.Size = new System.Drawing.Size(213, 22);
      this.RegisterServerMI.Text = "Register Server...";
      this.RegisterServerMI.Click += new System.EventHandler(this.RegisterServerMI_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 6);
      // 
      // ExitMI
      // 
      this.ExitMI.Name = "ExitMI";
      this.ExitMI.Size = new System.Drawing.Size(213, 22);
      this.ExitMI.Text = "Exit";
      this.ExitMI.Click += new System.EventHandler(this.ExitMI_Click);
      // 
      // ServersCTRL
      // 
      this.ServersCTRL.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ServersCTRL.EnableDragging = false;
      this.ServersCTRL.Instructions = null;
      this.ServersCTRL.Location = new System.Drawing.Point(3, 30);
      this.ServersCTRL.Name = "ServersCTRL";
      this.ServersCTRL.PrependItems = false;
      this.ServersCTRL.Size = new System.Drawing.Size(675, 216);
      this.ServersCTRL.TabIndex = 3;
      // 
      // ExportMI
      // 
      this.ExportMI.Name = "ExportMI";
      this.ExportMI.Size = new System.Drawing.Size(213, 22);
      this.ExportMI.Text = "Export Registered Servers...";
      this.ExportMI.Click += new System.EventHandler(this.ExportMI_Click);
      // 
      // ComServerListDlg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(681, 273);
      this.Controls.Add(this.MainPN);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "ComServerListDlg";
      this.Text = ".NET OPC Server Configuration Tool";
      this.MainPN.ResumeLayout(false);
      this.SelectorPN.ResumeLayout(false);
      this.SelectorPN.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel MainPN;
    private System.Windows.Forms.RadioButton DotNetServersRB;
    private System.Windows.Forms.RadioButton WrappersRB;
    private System.Windows.Forms.RadioButton RegisteredServersRB;
    private ComServerListUserControl ServersCTRL;
    private System.Windows.Forms.Panel SelectorPN;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem TasksMI;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem ExitMI;
    private System.Windows.Forms.ToolStripMenuItem RegisterServerMI;
    private System.Windows.Forms.ToolStripMenuItem ExportMI;
  }
}