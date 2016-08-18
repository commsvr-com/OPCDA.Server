//GraphGUIMainForm.cs - zawiera funkcje inicjalizacji i obsługi formularza
//Autor: Paweł Grącki
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Resources;
namespace SchemaGenerator
{
    /// <summary>
    /// Klasa ta zawiera całą funkcjonalnośc aplikacji
    /// </summary>
    public partial class GraphGUIMainForm : Form
    {
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public GraphGUIMainForm()
        {
            InitializeComponent();
            this.schemaPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(MouseUse.OnMouseClick);
            this.schemaPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseUse.OnMouseDown);
            this.schemaPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUse.OnMouseUp);
            this.schemaPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseUse.OnMouseMove);
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            this.openCfgToolStripMenuItem.Click += new System.EventHandler(this.OpenCfgToolStripMenuItem_Click);
            this.openSchemaToolStripMenuItem.Click += new System.EventHandler(this.OpenSchemaToolStripMenuItem_Click);
            this.saveSchemaToolStripMenuItem.Click += new System.EventHandler(this.SaveSchemaToolStripMenuItem_Click);
            this.regenerateViewToolStripMenuItem.Click += new System.EventHandler(this.RegenerateViewToolStripMenuItem_Click);
            this.refreshViewToolStripMenuItem.Click += new System.EventHandler(this.Form1_Refresh);
            this.updateCfgFileToolStripMenuItem.Click += new System.EventHandler(this.UpdateCfgFileToolStripMenuItem_Click);
            this.Resize += new System.EventHandler(this.Form1_Regenerate);
            this.Activated += new System.EventHandler(this.Form1_Refresh);
            this.ResizeEnd += new System.EventHandler(this.Form1_Regenerate);
            this.programSettingsToolStripMenuItem.Click += new System.EventHandler(this.ShowOptionsForm);
            Configuration.serverFrame.LineJoin = LineJoin.Bevel;
            Configuration.iconFrame.LineJoin = LineJoin.Bevel;
            Configuration.labelFrame.LineJoin = LineJoin.Bevel;
            resources = new ResourceManager(typeof(GraphGUIMainForm));
            // definiuję strukturę schemaDataBase
            this.webBrowser1.Navigate("about:blank");
            string blank = (string)resources.GetObject("Blank");
            this.webBrowser1.Document.Write(blank);
            this.webBrowser1.Refresh();
        }
        /// <summary>
        /// Wczytuje bazę danych z pliku XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCfgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "XML Files|*.xml";
                openFileDialog1.ShowDialog();
                configFile = openFileDialog1.FileName;
                // Włączam pozostałe opcje menu
                this.saveSchemaToolStripMenuItem.Enabled = true;
                this.regenerateViewToolStripMenuItem.Enabled = true;
                this.refreshViewToolStripMenuItem.Enabled = true;
                Data.LoadConfigFile(configFile);
                PrepareStaticTables();
                string blank = (string)resources.GetObject("Blank");
                this.webBrowser1.Document.Write(blank);
                this.webBrowser1.Refresh();
                GenerateSchema(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjątek : {0}", ex.ToString());
            }
        }
        /// <summary>
        /// Wczytuje schemat z pliku XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "XML Schema Data Files|*.xmlsd";
                openFileDialog1.ShowDialog();
                schemaFile = openFileDialog1.FileName;
                // Włączam pozostałe opcje menu
                this.saveSchemaToolStripMenuItem.Enabled = true;
                this.regenerateViewToolStripMenuItem.Enabled = true;
                this.refreshViewToolStripMenuItem.Enabled = true;
                Data.LoadSchemaFile(schemaFile);
                // Odczytuję i ustawiam rozmiar okna zapamiętany w schemaDataBase
                pH = int.Parse(Data.schemaDataSet1.panelSize.Rows[0]["panelHeight"].ToString());
                pW = int.Parse(Data.schemaDataSet1.panelSize.Rows[0]["panelWidth"].ToString());
                this.ClientSize = new Size(pW, pH);
                PrepareStaticTables();
                SetSchemaAttributes();
                Data.LoadSchemaData();
                // czyszczę okienko informacyjne
                string blank = (string)resources.GetObject("Blank");
                this.webBrowser1.Document.Write(blank);
                this.webBrowser1.Refresh();
                schemaFile = string.Empty; // ustawiam by umożliwić regeneracje schematu przy zmianie rozmiaru okna
                GenerateSchema(false); // odświeżenie widoku
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjątek : {0}", ex.ToString());
            }
        }
        /// <summary>
        /// Zapisuje plik z danymi schematu (schemaDataBase) do pliku XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Data.comunicationNet1.Channels.Rows.Count > 0) // zapobiega próbom generacji schematu przed wczytaniem bazy
            {
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "XML Schema Data Files|*.xmlsd";
                saveFileDialog1.ShowDialog();
                string url = saveFileDialog1.FileName;
                Data.SaveSchemaFile(url); // zapisuję plik schematu w wybranej lokacji
            }
        }
        /// <summary>
        /// Kończy działanie aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Otwiera plik pomocy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Data.comunicationNet12007.chm");
            Help.ShowHelp(this, path);
        }
        /// <summary>
        /// Otwiera okienko z informacjami o programie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphGUIAbout info = new GraphGUIAbout();
            info.ShowDialog();
        }
        /// <summary>
        /// Generuje schemat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegenerateViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateSchema(true);
        }
        /// <summary>
        /// Odświeża schemat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Refresh(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            GenerateSchema(false); // odświeżenie widoku
        }
        /// <summary>
        /// Generuje i wyświetla schemat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Regenerate(object sender, EventArgs e)
        {
            if (schemaFile == string.Empty) // jeśli wczytany został plik schematu (*.XMLSD) to nie regenerujemy schematu przy zmianie rozmiaru okna
            {
                this.Cursor = Cursors.Default;
                GenerateSchema(true); // odświeżenie widoku
            }
        }
        /// <summary>
        /// Zapisuje zaktualizowany (zmieniony) plik konfiguracji nadpisując stary plik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateCfgFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.comunicationNet1.WriteXml(configFile);
        }
        /// <summary>
        /// Wyświetla formularz z opcjami programu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOptionsForm(object sender, EventArgs e)
        {
            GraphGUISettings options = new GraphGUISettings();
            options.ShowDialog();
        }

      private void openCfgToolStripMenuItem_Click_1(object sender, EventArgs e)
      {

      }

      private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
      {

      }
    }
}