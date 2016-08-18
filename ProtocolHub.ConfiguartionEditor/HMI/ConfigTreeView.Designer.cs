//<summary>
//  Title   : Config tree view for Network Config
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

//#define advancetools

namespace NetworkConfig.HMI
{
  internal partial class ConfigTreeView
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }
    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.m_PNavigator = new PropertyNavigator();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ConfigTreeView ) );
      System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
      System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      System.Windows.Forms.ToolStrip toolStrip1;
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip( this.components );
      this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.showAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton_refresh = new System.Windows.Forms.ToolStripButton();
      this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.tsbUndo = new System.Windows.Forms.ToolStripButton();
      this.tsbRedo = new System.Windows.Forms.ToolStripButton();
      this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
      this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.stationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.channelsAndStationsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.m_helpToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.mStatusStrip = new System.Windows.Forms.StatusStrip();
      this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
      this.toolStripStatusLabel_tagamount = new System.Windows.Forms.ToolStripStatusLabel();
      this.mToolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.undoCtrlZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redoCtrlYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.cutCtrlXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyCtrlCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteCtrlVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteDelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.treeTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.channelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.stationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.channelsAndStationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem_Tools = new System.Windows.Forms.ToolStripMenuItem();
      this.importSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sBusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tagbloksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tagBitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.scanSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tagsForSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.advanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xBUSMeasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dCOMConfiguratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_HelpCcontentsTSMI = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.licenseInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.oToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.enterTheUnlockCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.backForwardTreViewToolStrip1 = new CAS.Lib.ControlLibrary.BackForwardTreViewToolStrip();
      this.searchTreeViewToolStrip1 = new CAS.Lib.ControlLibrary.SearchTreeViewToolStrip();
      this.bwSave = new System.ComponentModel.BackgroundWorker();
      this.m_helpProvider = new System.Windows.Forms.HelpProvider();
      this.cm_commonBusControl = new CAS.Lib.CommonBus.CommonBusControl( this.components );
      this.importToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.contextMenuStrip1.SuspendLayout();
      toolStrip1.SuspendLayout();
      this.mStatusStrip.SuspendLayout();
      this.mToolStripContainer.ContentPanel.SuspendLayout();
      this.mToolStripContainer.TopToolStripPanel.SuspendLayout();
      this.mToolStripContainer.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_PNavigator
      // 
      this.m_PNavigator.ContextMenuStrip = this.contextMenuStrip1;
      this.m_PNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_PNavigator.Location = new System.Drawing.Point( 0, 0 );
      this.m_PNavigator.Name = "m_PNavigator";
      this.m_PNavigator.Size = new System.Drawing.Size( 862, 325 );
      this.m_PNavigator.TabIndex = 1;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.showAllToolStripMenuItem,
            this.toolStripSeparator5,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem} );
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size( 130, 142 );
      // 
      // addToolStripMenuItem
      // 
      this.addToolStripMenuItem.Name = "addToolStripMenuItem";
      this.addToolStripMenuItem.Size = new System.Drawing.Size( 129, 22 );
      this.addToolStripMenuItem.Text = "Add";
      this.addToolStripMenuItem.Click += new System.EventHandler( this.addToolStripMenuItem_Click );
      // 
      // showAllToolStripMenuItem
      // 
      this.showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
      this.showAllToolStripMenuItem.Size = new System.Drawing.Size( 129, 22 );
      this.showAllToolStripMenuItem.Text = "Expand All";
      this.showAllToolStripMenuItem.Click += new System.EventHandler( this.showAllToolStripMenuItem_Click );
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size( 126, 6 );
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "cutToolStripMenuItem.Image" ) ) );
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.Size = new System.Drawing.Size( 129, 22 );
      this.cutToolStripMenuItem.Text = "Cut";
      this.cutToolStripMenuItem.Click += new System.EventHandler( this.cutToolStripMenuItem_Click );
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "copyToolStripMenuItem.Image" ) ) );
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size( 129, 22 );
      this.copyToolStripMenuItem.Text = "Copy";
      this.copyToolStripMenuItem.Click += new System.EventHandler( this.copyToolStripMenuItem_Click );
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "pasteToolStripMenuItem.Image" ) ) );
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size( 129, 22 );
      this.pasteToolStripMenuItem.Text = "Paste";
      this.pasteToolStripMenuItem.Click += new System.EventHandler( this.pasteToolStripMenuItem_Click );
      // 
      // deleteToolStripMenuItem
      // 
      this.deleteToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "deleteToolStripMenuItem.Image" ) ) );
      this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      this.deleteToolStripMenuItem.Size = new System.Drawing.Size( 129, 22 );
      this.deleteToolStripMenuItem.Text = "Delete";
      this.deleteToolStripMenuItem.Click += new System.EventHandler( this.deleteToolStripMenuItem_Click );
      // 
      // toolStripSeparator4
      // 
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new System.Drawing.Size( 6, 25 );
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new System.Drawing.Size( 6, 25 );
      toolStripSeparator1.Visible = false;
      // 
      // toolStrip1
      // 
      toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripButton_refresh,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            toolStripSeparator4,
            this.tsbUndo,
            this.tsbRedo,
            toolStripSeparator1,
            this.toolStripSplitButton1,
            this.m_helpToolStripButton} );
      toolStrip1.Location = new System.Drawing.Point( 3, 24 );
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Size = new System.Drawing.Size( 234, 25 );
      toolStrip1.TabIndex = 0;
      toolStrip1.Text = "toolStrip1";
      // 
      // newToolStripButton
      // 
      this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "newToolStripButton.Image" ) ) );
      this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newToolStripButton.Name = "newToolStripButton";
      this.newToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.newToolStripButton.Text = "&New";
      this.newToolStripButton.Click += new System.EventHandler( this.newToolStripButton_Click );
      // 
      // openToolStripButton
      // 
      this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "openToolStripButton.Image" ) ) );
      this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripButton.Name = "openToolStripButton";
      this.openToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.openToolStripButton.Text = "&Open";
      this.openToolStripButton.Click += new System.EventHandler( this.openToolStripButton_Click );
      // 
      // saveToolStripButton
      // 
      this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "saveToolStripButton.Image" ) ) );
      this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripButton.Name = "saveToolStripButton";
      this.saveToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.saveToolStripButton.Text = "&Save";
      this.saveToolStripButton.Click += new System.EventHandler( this.saveToolStripButton_Click );
      // 
      // printToolStripButton
      // 
      this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.printToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "printToolStripButton.Image" ) ) );
      this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printToolStripButton.Name = "printToolStripButton";
      this.printToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.printToolStripButton.Text = "&Print";
      this.printToolStripButton.Visible = false;
      // 
      // toolStripButton_refresh
      // 
      this.toolStripButton_refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton_refresh.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButton_refresh.Image" ) ) );
      this.toolStripButton_refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton_refresh.Name = "toolStripButton_refresh";
      this.toolStripButton_refresh.Size = new System.Drawing.Size( 23, 22 );
      this.toolStripButton_refresh.Text = "Refresh";
      this.toolStripButton_refresh.Click += new System.EventHandler( this.toolStripButton_refresh_Click );
      // 
      // cutToolStripButton
      // 
      this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.cutToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "cutToolStripButton.Image" ) ) );
      this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutToolStripButton.Name = "cutToolStripButton";
      this.cutToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.cutToolStripButton.Text = "C&ut";
      this.cutToolStripButton.Click += new System.EventHandler( this.cutToolStripButton_Click );
      // 
      // copyToolStripButton
      // 
      this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "copyToolStripButton.Image" ) ) );
      this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyToolStripButton.Name = "copyToolStripButton";
      this.copyToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.copyToolStripButton.Text = "&Copy";
      this.copyToolStripButton.Click += new System.EventHandler( this.copyToolStripButton_Click );
      // 
      // pasteToolStripButton
      // 
      this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.pasteToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "pasteToolStripButton.Image" ) ) );
      this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteToolStripButton.Name = "pasteToolStripButton";
      this.pasteToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.pasteToolStripButton.Text = "&Paste";
      this.pasteToolStripButton.Click += new System.EventHandler( this.pasteToolStripButton_Click );
      // 
      // tsbUndo
      // 
      this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbUndo.Enabled = false;
      this.tsbUndo.Image = ( (System.Drawing.Image)( resources.GetObject( "tsbUndo.Image" ) ) );
      this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbUndo.Name = "tsbUndo";
      this.tsbUndo.Size = new System.Drawing.Size( 23, 22 );
      this.tsbUndo.Text = "Undo";
      this.tsbUndo.Visible = false;
      this.tsbUndo.Click += new System.EventHandler( this.tsbUndo_Click );
      // 
      // tsbRedo
      // 
      this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbRedo.Enabled = false;
      this.tsbRedo.Image = ( (System.Drawing.Image)( resources.GetObject( "tsbRedo.Image" ) ) );
      this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbRedo.Name = "tsbRedo";
      this.tsbRedo.Size = new System.Drawing.Size( 23, 22 );
      this.tsbRedo.Text = "Redo";
      this.tsbRedo.Visible = false;
      this.tsbRedo.Click += new System.EventHandler( this.tsbRedo_Click );
      // 
      // toolStripSplitButton1
      // 
      this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripSplitButton1.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.channelsToolStripMenuItem,
            this.stationsToolStripMenuItem,
            this.channelsAndStationsToolStripMenuItem1} );
      this.toolStripSplitButton1.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripSplitButton1.Image" ) ) );
      this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripSplitButton1.Name = "toolStripSplitButton1";
      this.toolStripSplitButton1.Size = new System.Drawing.Size( 32, 22 );
      this.toolStripSplitButton1.Text = "TreeView Type";
      // 
      // channelsToolStripMenuItem
      // 
      this.channelsToolStripMenuItem.Name = "channelsToolStripMenuItem";
      this.channelsToolStripMenuItem.Size = new System.Drawing.Size( 190, 22 );
      this.channelsToolStripMenuItem.Text = "Channels";
      this.channelsToolStripMenuItem.Click += new System.EventHandler( this.channelsToolStripMenuItem_Click );
      // 
      // stationsToolStripMenuItem
      // 
      this.stationsToolStripMenuItem.Name = "stationsToolStripMenuItem";
      this.stationsToolStripMenuItem.Size = new System.Drawing.Size( 190, 22 );
      this.stationsToolStripMenuItem.Text = "Stations";
      this.stationsToolStripMenuItem.Click += new System.EventHandler( this.stationsToolStripMenuItem_Click );
      // 
      // channelsAndStationsToolStripMenuItem1
      // 
      this.channelsAndStationsToolStripMenuItem1.Name = "channelsAndStationsToolStripMenuItem1";
      this.channelsAndStationsToolStripMenuItem1.Size = new System.Drawing.Size( 190, 22 );
      this.channelsAndStationsToolStripMenuItem1.Text = "Channels and stations";
      this.channelsAndStationsToolStripMenuItem1.Click += new System.EventHandler( this.channelsAndStationsToolStripMenuItem1_Click );
      // 
      // m_helpToolStripButton
      // 
      this.m_helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_helpToolStripButton.Image = ( (System.Drawing.Image)( resources.GetObject( "m_helpToolStripButton.Image" ) ) );
      this.m_helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_helpToolStripButton.Name = "m_helpToolStripButton";
      this.m_helpToolStripButton.Size = new System.Drawing.Size( 23, 22 );
      this.m_helpToolStripButton.Text = "He&lp";
      this.m_helpToolStripButton.Click += new System.EventHandler( this.m_helpToolStripButton_Click );
      // 
      // mStatusStrip
      // 
      this.mStatusStrip.ContextMenuStrip = this.contextMenuStrip1;
      this.mStatusStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel_tagamount} );
      this.mStatusStrip.Location = new System.Drawing.Point( 0, 325 );
      this.mStatusStrip.Name = "mStatusStrip";
      this.mStatusStrip.Size = new System.Drawing.Size( 862, 22 );
      this.mStatusStrip.TabIndex = 2;
      this.mStatusStrip.Text = "MyStatusStrip";
      // 
      // toolStripProgressBar1
      // 
      this.toolStripProgressBar1.Name = "toolStripProgressBar1";
      this.toolStripProgressBar1.Size = new System.Drawing.Size( 100, 16 );
      // 
      // toolStripStatusLabel_tagamount
      // 
      this.toolStripStatusLabel_tagamount.Name = "toolStripStatusLabel_tagamount";
      this.toolStripStatusLabel_tagamount.Size = new System.Drawing.Size( 71, 17 );
      this.toolStripStatusLabel_tagamount.Text = "No. of Tags:";
      // 
      // mToolStripContainer
      // 
      // 
      // mToolStripContainer.ContentPanel
      // 
      this.mToolStripContainer.ContentPanel.Controls.Add( this.m_PNavigator );
      this.mToolStripContainer.ContentPanel.Controls.Add( this.mStatusStrip );
      this.mToolStripContainer.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
      this.mToolStripContainer.ContentPanel.Size = new System.Drawing.Size( 862, 347 );
      this.mToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mToolStripContainer.Location = new System.Drawing.Point( 0, 0 );
      this.mToolStripContainer.Name = "mToolStripContainer";
      this.mToolStripContainer.Size = new System.Drawing.Size( 862, 446 );
      this.mToolStripContainer.TabIndex = 4;
      this.mToolStripContainer.Text = "toolStripContainer2";
      // 
      // mToolStripContainer.TopToolStripPanel
      // 
      this.mToolStripContainer.TopToolStripPanel.Controls.Add( this.menuStrip1 );
      this.mToolStripContainer.TopToolStripPanel.Controls.Add( toolStrip1 );
      this.mToolStripContainer.TopToolStripPanel.Controls.Add( this.backForwardTreViewToolStrip1 );
      this.mToolStripContainer.TopToolStripPanel.Controls.Add( this.searchTreeViewToolStrip1 );
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem_Tools,
            this.helpToolStripMenuItem} );
      this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size( 862, 24 );
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem} );
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size( 37, 20 );
      this.toolStripMenuItem1.Text = "File";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "openToolStripMenuItem.Image" ) ) );
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size( 148, 22 );
      this.openToolStripMenuItem.Text = "Open   Ctrl+O";
      this.openToolStripMenuItem.Click += new System.EventHandler( this.openToolStripMenuItem_Click );
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "saveToolStripMenuItem.Image" ) ) );
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.Size = new System.Drawing.Size( 148, 22 );
      this.saveToolStripMenuItem.Text = "Save   Ctrl+S";
      this.saveToolStripMenuItem.Click += new System.EventHandler( this.saveToolStripMenuItem_Click );
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size( 148, 22 );
      this.saveAsToolStripMenuItem.Text = "Save As...";
      this.saveAsToolStripMenuItem.Click += new System.EventHandler( this.saveAsToolStripMenuItem_Click );
      // 
      // clearToolStripMenuItem
      // 
      this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
      this.clearToolStripMenuItem.Size = new System.Drawing.Size( 148, 22 );
      this.clearToolStripMenuItem.Text = "Clear";
      this.clearToolStripMenuItem.Click += new System.EventHandler( this.clearToolStripMenuItem_Click );
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size( 145, 6 );
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size( 148, 22 );
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler( this.exitToolStripMenuItem_Click );
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.undoCtrlZToolStripMenuItem,
            this.redoCtrlYToolStripMenuItem,
            this.toolStripSeparator2,
            this.cutCtrlXToolStripMenuItem,
            this.copyCtrlCToolStripMenuItem,
            this.pasteCtrlVToolStripMenuItem,
            this.deleteDelToolStripMenuItem} );
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size( 39, 20 );
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // undoCtrlZToolStripMenuItem
      // 
      this.undoCtrlZToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "undoCtrlZToolStripMenuItem.Image" ) ) );
      this.undoCtrlZToolStripMenuItem.Name = "undoCtrlZToolStripMenuItem";
      this.undoCtrlZToolStripMenuItem.Size = new System.Drawing.Size( 146, 22 );
      this.undoCtrlZToolStripMenuItem.Text = "Undo   Ctrl+Z";
      this.undoCtrlZToolStripMenuItem.Click += new System.EventHandler( this.undoCtrlZToolStripMenuItem_Click );
      // 
      // redoCtrlYToolStripMenuItem
      // 
      this.redoCtrlYToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "redoCtrlYToolStripMenuItem.Image" ) ) );
      this.redoCtrlYToolStripMenuItem.Name = "redoCtrlYToolStripMenuItem";
      this.redoCtrlYToolStripMenuItem.Size = new System.Drawing.Size( 146, 22 );
      this.redoCtrlYToolStripMenuItem.Text = "Redo   Ctrl+Y";
      this.redoCtrlYToolStripMenuItem.Click += new System.EventHandler( this.redoCtrlYToolStripMenuItem_Click );
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size( 143, 6 );
      // 
      // cutCtrlXToolStripMenuItem
      // 
      this.cutCtrlXToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "cutCtrlXToolStripMenuItem.Image" ) ) );
      this.cutCtrlXToolStripMenuItem.Name = "cutCtrlXToolStripMenuItem";
      this.cutCtrlXToolStripMenuItem.Size = new System.Drawing.Size( 146, 22 );
      this.cutCtrlXToolStripMenuItem.Text = "Cut   Ctrl+X";
      this.cutCtrlXToolStripMenuItem.Click += new System.EventHandler( this.cutCtrlXToolStripMenuItem_Click );
      // 
      // copyCtrlCToolStripMenuItem
      // 
      this.copyCtrlCToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "copyCtrlCToolStripMenuItem.Image" ) ) );
      this.copyCtrlCToolStripMenuItem.Name = "copyCtrlCToolStripMenuItem";
      this.copyCtrlCToolStripMenuItem.Size = new System.Drawing.Size( 146, 22 );
      this.copyCtrlCToolStripMenuItem.Text = "Copy   Ctrl+C";
      this.copyCtrlCToolStripMenuItem.Click += new System.EventHandler( this.copyCtrlCToolStripMenuItem_Click );
      // 
      // pasteCtrlVToolStripMenuItem
      // 
      this.pasteCtrlVToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "pasteCtrlVToolStripMenuItem.Image" ) ) );
      this.pasteCtrlVToolStripMenuItem.Name = "pasteCtrlVToolStripMenuItem";
      this.pasteCtrlVToolStripMenuItem.Size = new System.Drawing.Size( 146, 22 );
      this.pasteCtrlVToolStripMenuItem.Text = "Paste   Ctrl+V";
      this.pasteCtrlVToolStripMenuItem.Click += new System.EventHandler( this.pasteCtrlVToolStripMenuItem_Click );
      // 
      // deleteDelToolStripMenuItem
      // 
      this.deleteDelToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "deleteDelToolStripMenuItem.Image" ) ) );
      this.deleteDelToolStripMenuItem.Name = "deleteDelToolStripMenuItem";
      this.deleteDelToolStripMenuItem.Size = new System.Drawing.Size( 146, 22 );
      this.deleteDelToolStripMenuItem.Text = "Delete   Del";
      this.deleteDelToolStripMenuItem.Click += new System.EventHandler( this.deleteDelToolStripMenuItem_Click );
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.treeTypeToolStripMenuItem,
            this.refreshToolStripMenuItem1} );
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size( 44, 20 );
      this.viewToolStripMenuItem.Text = "View";
      // 
      // treeTypeToolStripMenuItem
      // 
      this.treeTypeToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.channelToolStripMenuItem,
            this.stationToolStripMenuItem,
            this.channelsAndStationsToolStripMenuItem} );
      this.treeTypeToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "treeTypeToolStripMenuItem.Image" ) ) );
      this.treeTypeToolStripMenuItem.Name = "treeTypeToolStripMenuItem";
      this.treeTypeToolStripMenuItem.Size = new System.Drawing.Size( 123, 22 );
      this.treeTypeToolStripMenuItem.Text = "Tree type";
      // 
      // channelToolStripMenuItem
      // 
      this.channelToolStripMenuItem.Name = "channelToolStripMenuItem";
      this.channelToolStripMenuItem.Size = new System.Drawing.Size( 190, 22 );
      this.channelToolStripMenuItem.Text = "Channels";
      this.channelToolStripMenuItem.Click += new System.EventHandler( this.cannelToolStripMenuItem_Click );
      // 
      // stationToolStripMenuItem
      // 
      this.stationToolStripMenuItem.Name = "stationToolStripMenuItem";
      this.stationToolStripMenuItem.Size = new System.Drawing.Size( 190, 22 );
      this.stationToolStripMenuItem.Text = "Stations";
      this.stationToolStripMenuItem.Click += new System.EventHandler( this.stationToolStripMenuItem_Click );
      // 
      // channelsAndStationsToolStripMenuItem
      // 
      this.channelsAndStationsToolStripMenuItem.Name = "channelsAndStationsToolStripMenuItem";
      this.channelsAndStationsToolStripMenuItem.Size = new System.Drawing.Size( 190, 22 );
      this.channelsAndStationsToolStripMenuItem.Text = "Channels and stations";
      this.channelsAndStationsToolStripMenuItem.Click += new System.EventHandler( this.channelsAndStationsToolStripMenuItem_Click );
      // 
      // refreshToolStripMenuItem1
      // 
      this.refreshToolStripMenuItem1.Image = ( (System.Drawing.Image)( resources.GetObject( "refreshToolStripMenuItem1.Image" ) ) );
      this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
      this.refreshToolStripMenuItem1.Size = new System.Drawing.Size( 123, 22 );
      this.refreshToolStripMenuItem1.Text = "Refresh";
      this.refreshToolStripMenuItem1.Click += new System.EventHandler( this.refreshToolStripMenuItem1_Click );
      // 
      // toolsToolStripMenuItem_Tools
      // 
      this.toolsToolStripMenuItem_Tools.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.importSettingsToolStripMenuItem,
            this.advanceToolStripMenuItem,
            this.xBUSMeasureToolStripMenuItem,
            this.dCOMConfiguratorToolStripMenuItem} );
      this.toolsToolStripMenuItem_Tools.Name = "toolsToolStripMenuItem_Tools";
      this.toolsToolStripMenuItem_Tools.Size = new System.Drawing.Size( 48, 20 );
      this.toolsToolStripMenuItem_Tools.Text = "Tools";
      // 
      // importSettingsToolStripMenuItem
      // 
      this.importSettingsToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.sBusToolStripMenuItem,
            this.tagbloksToolStripMenuItem,
            this.tagBitToolStripMenuItem,
            this.tagToolStripMenuItem,
            this.scanSettingsToolStripMenuItem,
            this.tagsForSimulationToolStripMenuItem} );
      this.importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
      this.importSettingsToolStripMenuItem.Size = new System.Drawing.Size( 181, 22 );
      this.importSettingsToolStripMenuItem.Text = "Import Settings";
      // 
      // sBusToolStripMenuItem
      // 
      this.sBusToolStripMenuItem.Name = "sBusToolStripMenuItem";
      this.sBusToolStripMenuItem.Size = new System.Drawing.Size( 182, 22 );
      this.sBusToolStripMenuItem.Text = "Blocks from BLS file";
      this.sBusToolStripMenuItem.Click += new System.EventHandler( this.sBLSToolStripMenuItem_Click_1 );
      // 
      // tagbloksToolStripMenuItem
      // 
      this.tagbloksToolStripMenuItem.Name = "tagbloksToolStripMenuItem";
      this.tagbloksToolStripMenuItem.Size = new System.Drawing.Size( 182, 22 );
      this.tagbloksToolStripMenuItem.Text = "Blocks of tags (CSV)";
      this.tagbloksToolStripMenuItem.Click += new System.EventHandler( this.tagbloksToolStripMenuItem_Click_1 );
      // 
      // tagBitToolStripMenuItem
      // 
      this.tagBitToolStripMenuItem.Name = "tagBitToolStripMenuItem";
      this.tagBitToolStripMenuItem.Size = new System.Drawing.Size( 182, 22 );
      this.tagBitToolStripMenuItem.Text = "TagBits (CSV)";
      this.tagBitToolStripMenuItem.Click += new System.EventHandler( this.tagBitToolStripMenuItem_Click_1 );
      // 
      // tagToolStripMenuItem
      // 
      this.tagToolStripMenuItem.Name = "tagToolStripMenuItem";
      this.tagToolStripMenuItem.Size = new System.Drawing.Size( 182, 22 );
      this.tagToolStripMenuItem.Text = "Tag Mappings (CSV)";
      this.tagToolStripMenuItem.Click += new System.EventHandler( this.tagToolStripMenuItem_Click_1 );
      // 
      // scanSettingsToolStripMenuItem
      // 
      this.scanSettingsToolStripMenuItem.Name = "scanSettingsToolStripMenuItem";
      this.scanSettingsToolStripMenuItem.Size = new System.Drawing.Size( 182, 22 );
      this.scanSettingsToolStripMenuItem.Text = "Scan settings (CSV)";
      this.scanSettingsToolStripMenuItem.Click += new System.EventHandler( this.scanSettingsToolStripMenuItem_Click_1 );
      // 
      // tagsForSimulationToolStripMenuItem
      // 
      this.tagsForSimulationToolStripMenuItem.Name = "tagsForSimulationToolStripMenuItem";
      this.tagsForSimulationToolStripMenuItem.Size = new System.Drawing.Size( 182, 22 );
      this.tagsForSimulationToolStripMenuItem.Text = "Tags for Simulation";
      this.tagsForSimulationToolStripMenuItem.Click += new System.EventHandler( this.tagsForSimulationToolStripMenuItem_Click_1 );
      // 
      // advanceToolStripMenuItem
      // 
      this.advanceToolStripMenuItem.Name = "advanceToolStripMenuItem";
      this.advanceToolStripMenuItem.Size = new System.Drawing.Size( 181, 22 );
      this.advanceToolStripMenuItem.Text = "Advance";
      this.advanceToolStripMenuItem.Click += new System.EventHandler( this.advanceToolStripMenuItem_Click );
      // 
      // xBUSMeasureToolStripMenuItem
      // 
      this.xBUSMeasureToolStripMenuItem.Name = "xBUSMeasureToolStripMenuItem";
      this.xBUSMeasureToolStripMenuItem.Size = new System.Drawing.Size( 181, 22 );
      this.xBUSMeasureToolStripMenuItem.Text = "XBUS Measure";
      this.xBUSMeasureToolStripMenuItem.Click += new System.EventHandler( this.xBUSMeasureToolStripMenuItem_Click );
      // 
      // dCOMConfiguratorToolStripMenuItem
      // 
      this.dCOMConfiguratorToolStripMenuItem.Name = "dCOMConfiguratorToolStripMenuItem";
      this.dCOMConfiguratorToolStripMenuItem.Size = new System.Drawing.Size( 181, 22 );
      this.dCOMConfiguratorToolStripMenuItem.Text = "DCOM Configurator";
      this.dCOMConfiguratorToolStripMenuItem.Click += new System.EventHandler( this.dCOMConfiguratorToolStripMenuItem_Click );
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.m_HelpCcontentsTSMI,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem,
            this.licenseInformationToolStripMenuItem,
            this.oToolStripMenuItem,
            this.enterTheUnlockCodeToolStripMenuItem} );
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size( 44, 20 );
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // m_HelpCcontentsTSMI
      // 
      this.m_HelpCcontentsTSMI.Image = ( (System.Drawing.Image)( resources.GetObject( "m_HelpCcontentsTSMI.Image" ) ) );
      this.m_HelpCcontentsTSMI.Name = "m_HelpCcontentsTSMI";
      this.m_HelpCcontentsTSMI.Size = new System.Drawing.Size( 226, 22 );
      this.m_HelpCcontentsTSMI.Text = "Help";
      this.m_HelpCcontentsTSMI.Click += new System.EventHandler( this.m_helpToolStripButton_Click );
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size( 223, 6 );
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "aboutToolStripMenuItem.Image" ) ) );
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size( 226, 22 );
      this.aboutToolStripMenuItem.Text = "About Network Config";
      this.aboutToolStripMenuItem.Click += new System.EventHandler( this.aboutToolStripMenuItem_Click );
      // 
      // licenseInformationToolStripMenuItem
      // 
      this.licenseInformationToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "licenseInformationToolStripMenuItem.Image" ) ) );
      this.licenseInformationToolStripMenuItem.Name = "licenseInformationToolStripMenuItem";
      this.licenseInformationToolStripMenuItem.Size = new System.Drawing.Size( 226, 22 );
      this.licenseInformationToolStripMenuItem.Text = "License information";
      this.licenseInformationToolStripMenuItem.Click += new System.EventHandler( this.licenseInformationToolStripMenuItem_Click );
      // 
      // oToolStripMenuItem
      // 
      this.oToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "oToolStripMenuItem.Image" ) ) );
      this.oToolStripMenuItem.Name = "oToolStripMenuItem";
      this.oToolStripMenuItem.Size = new System.Drawing.Size( 226, 22 );
      this.oToolStripMenuItem.Text = "Open logs containing folder";
      this.oToolStripMenuItem.Click += new System.EventHandler( this.oToolStripMenuItem_Click );
      // 
      // enterTheUnlockCodeToolStripMenuItem
      // 
      this.enterTheUnlockCodeToolStripMenuItem.Image = ( (System.Drawing.Image)( resources.GetObject( "enterTheUnlockCodeToolStripMenuItem.Image" ) ) );
      this.enterTheUnlockCodeToolStripMenuItem.Name = "enterTheUnlockCodeToolStripMenuItem";
      this.enterTheUnlockCodeToolStripMenuItem.ShortcutKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.K ) ) );
      this.enterTheUnlockCodeToolStripMenuItem.Size = new System.Drawing.Size( 226, 22 );
      this.enterTheUnlockCodeToolStripMenuItem.Text = "Enter the unlock code";
      this.enterTheUnlockCodeToolStripMenuItem.Click += new System.EventHandler( this.enterTheUnlockCodeToolStripMenuItem_Click );
      // 
      // backForwardTreViewToolStrip1
      // 
      this.backForwardTreViewToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.backForwardTreViewToolStrip1.Location = new System.Drawing.Point( 30, 49 );
      this.backForwardTreViewToolStrip1.Name = "backForwardTreViewToolStrip1";
      this.backForwardTreViewToolStrip1.NumberOfPreviousNodesInTheTooltip = 5;
      this.backForwardTreViewToolStrip1.Size = new System.Drawing.Size( 120, 25 );
      this.backForwardTreViewToolStrip1.TabIndex = 2;
      this.backForwardTreViewToolStrip1.Text = "Navigate: ";
      this.backForwardTreViewToolStrip1.ToolStripBackwardImage = null;
      this.backForwardTreViewToolStrip1.ToolStripForwardImage = null;
      this.backForwardTreViewToolStrip1.TreeView = null;
      // 
      // searchTreeViewToolStrip1
      // 
      this.searchTreeViewToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.searchTreeViewToolStrip1.InformationCannotBeFound = "Searched element cannot be found";
      this.searchTreeViewToolStrip1.InformationEndIsPassed = "End of the tree is passed";
      this.searchTreeViewToolStrip1.InformationStartIsPassed = "Start of the tree is passed";
      this.searchTreeViewToolStrip1.Location = new System.Drawing.Point( 3, 74 );
      this.searchTreeViewToolStrip1.Name = "searchTreeViewToolStrip1";
      this.searchTreeViewToolStrip1.Size = new System.Drawing.Size( 328, 25 );
      this.searchTreeViewToolStrip1.TabIndex = 3;
      this.searchTreeViewToolStrip1.Text = "Search:";
      this.searchTreeViewToolStrip1.ToolStripBackwardImage = null;
      this.searchTreeViewToolStrip1.ToolStripForwardImage = null;
      this.searchTreeViewToolStrip1.TreeView = null;
      // 
      // bwSave
      // 
      this.bwSave.DoWork += new System.ComponentModel.DoWorkEventHandler( this.bwSave_DoWork );
      this.bwSave.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler( this.bwSave_RunWorkerCompleted );
      this.bwSave.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler( this.bwSave_ProgressChanged );
      // 
      // m_helpProvider
      // 
      this.m_helpProvider.HelpNamespace = "C:\\Documents and Settings\\mpostol.HQ\\My Documents\\Visual Studio 2005\\Projects\\tru" +
          "nk2\\PR21-CommServer\\CHM\\CommServer.chm";
      // 
      // importToolStripMenuItem1
      // 
      this.importToolStripMenuItem1.Name = "importToolStripMenuItem1";
      this.importToolStripMenuItem1.Size = new System.Drawing.Size( 32, 19 );
      // 
      // ConfigTreeView
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.ClientSize = new System.Drawing.Size( 862, 446 );
      this.Controls.Add( this.mToolStripContainer );
      this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "ConfigTreeView";
      this.Text = "NetworkConfig";
      this.contextMenuStrip1.ResumeLayout( false );
      toolStrip1.ResumeLayout( false );
      toolStrip1.PerformLayout();
      this.mStatusStrip.ResumeLayout( false );
      this.mStatusStrip.PerformLayout();
      this.mToolStripContainer.ContentPanel.ResumeLayout( false );
      this.mToolStripContainer.ContentPanel.PerformLayout();
      this.mToolStripContainer.TopToolStripPanel.ResumeLayout( false );
      this.mToolStripContainer.TopToolStripPanel.PerformLayout();
      this.mToolStripContainer.ResumeLayout( false );
      this.mToolStripContainer.PerformLayout();
      this.menuStrip1.ResumeLayout( false );
      this.menuStrip1.PerformLayout();
      this.ResumeLayout( false );

    }
    #endregion
    private PropertyNavigator m_PNavigator;
    private System.Windows.Forms.StatusStrip mStatusStrip;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton openToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripButton printToolStripButton;
    private System.Windows.Forms.ToolStripButton cutToolStripButton;
    private System.Windows.Forms.ToolStripButton copyToolStripButton;
    private System.Windows.Forms.ToolStripButton pasteToolStripButton;
    private System.Windows.Forms.ToolStripButton m_helpToolStripButton;
    private System.Windows.Forms.ToolStripContainer mToolStripContainer;
    private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
#if advancetools
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
#endif
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem treeTypeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem channelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem stationToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton toolStripButton_refresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton tsbUndo;
    private System.Windows.Forms.ToolStripButton tsbRedo;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.ComponentModel.BackgroundWorker bwSave;
    private System.Windows.Forms.ToolStripMenuItem channelsAndStationsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_HelpCcontentsTSMI;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem undoCtrlZToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoCtrlYToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem cutCtrlXToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyCtrlCToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteCtrlVToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteDelToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    private System.Windows.Forms.ToolStripMenuItem stationsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem channelsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem channelsAndStationsToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.HelpProvider m_helpProvider;
    private CAS.Lib.CommonBus.CommonBusControl cm_commonBusControl;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem_Tools;
    private System.Windows.Forms.ToolStripMenuItem advanceToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem importSettingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sBusToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tagbloksToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tagBitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tagToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem scanSettingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tagsForSimulationToolStripMenuItem;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_tagamount;
    private System.Windows.Forms.ToolStripMenuItem xBUSMeasureToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem dCOMConfiguratorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem licenseInformationToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem oToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem enterTheUnlockCodeToolStripMenuItem;
    private CAS.Lib.ControlLibrary.BackForwardTreViewToolStrip backForwardTreViewToolStrip1;
    private CAS.Lib.ControlLibrary.SearchTreeViewToolStrip searchTreeViewToolStrip1;
  }
}
