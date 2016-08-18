namespace SchemaGenerator
{
    partial class GraphGUIMainForm
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphGUIMainForm));
          this.menuStrip1 = new System.Windows.Forms.MenuStrip();
          this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.openCfgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.openSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.saveSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.updateCfgFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.refreshViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.regenerateViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.opcjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.programSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
          this.schemaPanel = new System.Windows.Forms.Panel();
          this.webBrowser1 = new System.Windows.Forms.WebBrowser();
          this.label1 = new System.Windows.Forms.Label();
          this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
          this.menuStrip1.SuspendLayout();
          this.schemaPanel.SuspendLayout();
          this.SuspendLayout();
          // 
          // menuStrip1
          // 
          this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
          this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
          this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
          this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.opcjeToolStripMenuItem,
            this.infoToolStripMenuItem});
          this.menuStrip1.Location = new System.Drawing.Point(0, 0);
          this.menuStrip1.Name = "menuStrip1";
          this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
          this.menuStrip1.Size = new System.Drawing.Size(792, 24);
          this.menuStrip1.TabIndex = 0;
          this.menuStrip1.Text = "menuStrip1";
          // 
          // plikToolStripMenuItem
          // 
          this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCfgToolStripMenuItem,
            this.openSchemaToolStripMenuItem,
            this.saveSchemaToolStripMenuItem,
            this.updateCfgFileToolStripMenuItem,
            this.refreshViewToolStripMenuItem,
            this.regenerateViewToolStripMenuItem,
            this.exitToolStripMenuItem});
          this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
          this.plikToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
          this.plikToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
          this.plikToolStripMenuItem.Text = "Plik";
          // 
          // openCfgToolStripMenuItem
          // 
          this.openCfgToolStripMenuItem.Name = "openCfgToolStripMenuItem";
          this.openCfgToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
          this.openCfgToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
          this.openCfgToolStripMenuItem.Text = "Otwórz plik konfiguracji";
          this.openCfgToolStripMenuItem.Click += new System.EventHandler(this.openCfgToolStripMenuItem_Click_1);
          // 
          // openSchemaToolStripMenuItem
          // 
          this.openSchemaToolStripMenuItem.Name = "openSchemaToolStripMenuItem";
          this.openSchemaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
          this.openSchemaToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
          this.openSchemaToolStripMenuItem.Text = "Otwórz plik schematu";
          // 
          // saveSchemaToolStripMenuItem
          // 
          this.saveSchemaToolStripMenuItem.Enabled = false;
          this.saveSchemaToolStripMenuItem.Name = "saveSchemaToolStripMenuItem";
          this.saveSchemaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
          this.saveSchemaToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
          this.saveSchemaToolStripMenuItem.Text = "Zapisz schemat";
          // 
          // updateCfgFileToolStripMenuItem
          // 
          this.updateCfgFileToolStripMenuItem.Enabled = false;
          this.updateCfgFileToolStripMenuItem.Name = "updateCfgFileToolStripMenuItem";
          this.updateCfgFileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
          this.updateCfgFileToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
          this.updateCfgFileToolStripMenuItem.Text = "Aktualizuj plik konfiguracji";
          // 
          // refreshViewToolStripMenuItem
          // 
          this.refreshViewToolStripMenuItem.Enabled = false;
          this.refreshViewToolStripMenuItem.Name = "refreshViewToolStripMenuItem";
          this.refreshViewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
          this.refreshViewToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
          this.refreshViewToolStripMenuItem.Text = "Odśwież widok";
          // 
          // regenerateViewToolStripMenuItem
          // 
          this.regenerateViewToolStripMenuItem.Enabled = false;
          this.regenerateViewToolStripMenuItem.Name = "regenerateViewToolStripMenuItem";
          this.regenerateViewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
          this.regenerateViewToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
          this.regenerateViewToolStripMenuItem.Text = "Generuj schemat";
          // 
          // exitToolStripMenuItem
          // 
          this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
          this.exitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
          this.exitToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
          this.exitToolStripMenuItem.Text = "Zakończ program";
          this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
          // 
          // opcjeToolStripMenuItem
          // 
          this.opcjeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programSettingsToolStripMenuItem});
          this.opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
          this.opcjeToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
          this.opcjeToolStripMenuItem.Text = "Opcje";
          // 
          // programSettingsToolStripMenuItem
          // 
          this.programSettingsToolStripMenuItem.Name = "programSettingsToolStripMenuItem";
          this.programSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
          this.programSettingsToolStripMenuItem.Text = "Ustawienia programu";
          // 
          // infoToolStripMenuItem
          // 
          this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
          this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
          this.infoToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
          this.infoToolStripMenuItem.Text = "Info";
          // 
          // helpToolStripMenuItem
          // 
          this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
          this.helpToolStripMenuItem.ShortcutKeyDisplayString = "";
          this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
          this.helpToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
          this.helpToolStripMenuItem.Text = "Pomoc";
          // 
          // aboutToolStripMenuItem
          // 
          this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
          this.aboutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
          this.aboutToolStripMenuItem.Text = "O programie";
          // 
          // openFileDialog1
          // 
          this.openFileDialog1.FileName = "openFileDialog1";
          // 
          // schemaPanel
          // 
          this.schemaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.schemaPanel.BackColor = System.Drawing.SystemColors.ControlLight;
          this.schemaPanel.Controls.Add(this.webBrowser1);
          this.schemaPanel.Controls.Add(this.label1);
          this.schemaPanel.Location = new System.Drawing.Point(0, 24);
          this.schemaPanel.Name = "schemaPanel";
          this.schemaPanel.Size = new System.Drawing.Size(792, 539);
          this.schemaPanel.TabIndex = 9;
          // 
          // webBrowser1
          // 
          this.webBrowser1.AllowNavigation = false;
          this.webBrowser1.AllowWebBrowserDrop = false;
          this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.webBrowser1.Location = new System.Drawing.Point(491, 364);
          this.webBrowser1.Margin = new System.Windows.Forms.Padding(1);
          this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
          this.webBrowser1.Name = "webBrowser1";
          this.webBrowser1.Size = new System.Drawing.Size(300, 175);
          this.webBrowser1.TabIndex = 0;
          // 
          // label1
          // 
          this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
          this.label1.Location = new System.Drawing.Point(488, 361);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(304, 178);
          this.label1.TabIndex = 1;
          // 
          // GraphGUIMainForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.ClientSize = new System.Drawing.Size(792, 563);
          this.Controls.Add(this.schemaPanel);
          this.Controls.Add(this.menuStrip1);
          this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MainMenuStrip = this.menuStrip1;
          this.MinimumSize = new System.Drawing.Size(800, 590);
          this.Name = "GraphGUIMainForm";
          this.Text = "SchemaGenerator 2007";
          this.menuStrip1.ResumeLayout(false);
          this.menuStrip1.PerformLayout();
          this.schemaPanel.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCfgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel schemaPanel;
        private System.Windows.Forms.ToolStripMenuItem regenerateViewToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem openSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshViewToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programSettingsToolStripMenuItem;
        public System.Windows.Forms.WebBrowser webBrowser1;
        public System.Windows.Forms.ToolStripMenuItem updateCfgFileToolStripMenuItem;
    }
}

