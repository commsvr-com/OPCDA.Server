using System;
using System.Collections.Generic;
using System.Text;

namespace CAS.CommServerConsole
{
  public partial class MainForm: System.Windows.Forms.Form
  {
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label labelRxDBPlus;
    private System.Windows.Forms.Label labelRxDBPlusVal;
    private System.Windows.Forms.Label labelRxDBMin;
    private System.Windows.Forms.Label labelTxDBPlusVal;
    private System.Windows.Forms.Label labelTxDBPlus;
    private System.Windows.Forms.Label labelTxDBMinVal;
    private System.Windows.Forms.Label labelTxDBMin;
    private System.Windows.Forms.Label labelTestTimeLab;
    private System.Windows.Forms.Label labelTestTime;
    private System.Windows.Forms.TabPage tabPageInterface;
    private System.Windows.Forms.ListView InterfaceListView;
    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.Label labelRxDBMinusVal;
    private System.Windows.Forms.Label labelCharGapLab;
    private System.Windows.Forms.Label labelnaklAB;
    private System.Windows.Forms.Label labelMaxResponseTime;
    private System.Windows.Forms.Label labelMaxResTime;
    private System.Windows.Forms.Label labelTimeOutCnt;
    private System.Windows.Forms.Label labelTimeOutCountLab;
    private System.Windows.Forms.Label labelIncompleteCnt;
    private System.Windows.Forms.Label labelIncompleteLab;
    private System.Windows.Forms.Label labelCRCErrorsCnt;
    private System.Windows.Forms.Label labelCRCErrorLob;
    private System.Windows.Forms.Label labelTXFramesCount;
    private System.Windows.Forms.Label labelRXFramesLab;
    private System.Windows.Forms.Label labelTXFramesLab;
    private System.Windows.Forms.Label labelRXFramesCount;
    private System.Windows.Forms.Label labelProgramName;
    private System.Windows.Forms.Label labelIntState;
    private System.Windows.Forms.Label labelIntStateVal;
    private System.Windows.Forms.Label labelIntActiveTimeVal;
    private System.Windows.Forms.Label labelIntActiveTime;
    private System.Windows.Forms.Label labelIntFailTimeVal;
    private System.Windows.Forms.Label labelIntFailTime;
    private System.Windows.Forms.Label labelIntStandByVal;
    private System.Windows.Forms.Label labelIntStandBy;
    private System.Windows.Forms.GroupBox groupBoxCurrInt;
    private System.Windows.Forms.TabPage tabPageProtocol;
    private System.Windows.Forms.TabPage tabPageSegments;
    private System.Windows.Forms.ListView SegmentsListView;
    private System.Windows.Forms.Label labelSegAciveVal;
    private System.Windows.Forms.Label labelSegAcive;
    private System.Windows.Forms.Label labelSegStateVal;
    private System.Windows.Forms.Label labelSegState;
    private System.Windows.Forms.Label labelSegWriteDelayVal;
    private System.Windows.Forms.Label labelSegWriteDelay;
    private System.Windows.Forms.Label labelSegReadDelayVal;
    private System.Windows.Forms.Label labelSegReadDelay;
    private System.Windows.Forms.Label labelSegDataOvertimeVal;
    private System.Windows.Forms.Label labelSegDataOvertime;
    private System.Windows.Forms.Label labelSegConnectionsVal;
    private System.Windows.Forms.Label labelSegConnections;
    private System.Windows.Forms.Label labelSegConnFailVal;
    private System.Windows.Forms.Label labelSegConnFail;
    private System.Windows.Forms.Label labeProtlMaxCharGapTime;
    private System.Windows.Forms.Label labelProtNAKVal;
    private System.Windows.Forms.Label labelProtocolnvalidVVal;
    private System.Windows.Forms.Label labelProtocolnvalidV;
    private System.Windows.Forms.Label LabelProtocolRxSynchErrorVal;
    private System.Windows.Forms.Label LabelProtocolRxSynchError;
    private System.Windows.Forms.TabPage tabStation;
    private System.Windows.Forms.ListView StationsListView;
    private System.Windows.Forms.MenuItem menu_MENU;
    private System.Windows.Forms.MenuItem menu_hide;
    private System.Windows.Forms.MenuItem menu_configurator;
    private System.Windows.Forms.MenuItem menu_report;
    private System.Windows.Forms.MenuItem menu_help;
    private System.Windows.Forms.NotifyIcon notifyIcon_trayicon;
    private System.Windows.Forms.ListBox listBoxProtocol;
    private System.Windows.Forms.Timer RefreshFormTimer;
    private System.Windows.Forms.ContextMenu TrayIconMenu;
    private System.Windows.Forms.MainMenu MainFormMenu;
    private System.Windows.Forms.MenuItem menuItem2;
    private System.Windows.Forms.MenuItem menuItem4;
    private System.Windows.Forms.MenuItem menu_About;
    private System.Windows.Forms.MenuItem menu_Exit;
    private System.Windows.Forms.MenuItem CMItem_Exit;
    private System.Windows.Forms.ColumnHeader SegmentColumn;
    private System.Windows.Forms.ColumnHeader InterfaceColumn;
    private System.Windows.Forms.ColumnHeader StationHeader;
    private System.Windows.Forms.Label labelSegConnectMMAVal;
    private System.Windows.Forms.Label labelSegConnectMMA;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.ImageList StationImageList;
    private System.Windows.Forms.ImageList SegmentsImageList;
    private System.Windows.Forms.ImageList InterfaceImageList;
    private System.Windows.Forms.GroupBox groupBoxProtStat;
    private System.Windows.Forms.GroupBox groupBoxSegmentCurr;
    private System.Windows.Forms.GroupBox groupBoxCurrStation;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label_StationCurrState;
    private System.Windows.Forms.Label labelRelease;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TabControl tabControlInterface;
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if ( disposing )
      {
        if ( components != null )
        {
          components.Dispose();
        }
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
      this.tabControlInterface = new System.Windows.Forms.TabControl();
      this.tabStation = new System.Windows.Forms.TabPage();
      this.groupBoxCurrStation = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label_StationCurrState = new System.Windows.Forms.Label();
      this.StationsListView = new System.Windows.Forms.ListView();
      this.StationHeader = new System.Windows.Forms.ColumnHeader();
      this.TrayIconMenu = new System.Windows.Forms.ContextMenu();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.CMItem_Exit = new System.Windows.Forms.MenuItem();
      this.menuItem4 = new System.Windows.Forms.MenuItem();
      this.StationImageList = new System.Windows.Forms.ImageList( this.components );
      this.tabPageInterface = new System.Windows.Forms.TabPage();
      this.groupBoxCurrInt = new System.Windows.Forms.GroupBox();
      this.labelIntStandByVal = new System.Windows.Forms.Label();
      this.labelIntStandBy = new System.Windows.Forms.Label();
      this.labelIntFailTimeVal = new System.Windows.Forms.Label();
      this.labelIntFailTime = new System.Windows.Forms.Label();
      this.labelIntActiveTimeVal = new System.Windows.Forms.Label();
      this.labelIntActiveTime = new System.Windows.Forms.Label();
      this.labelIntStateVal = new System.Windows.Forms.Label();
      this.labelIntState = new System.Windows.Forms.Label();
      this.InterfaceListView = new System.Windows.Forms.ListView();
      this.InterfaceColumn = new System.Windows.Forms.ColumnHeader();
      this.InterfaceImageList = new System.Windows.Forms.ImageList( this.components );
      this.tabPageSegments = new System.Windows.Forms.TabPage();
      this.groupBoxSegmentCurr = new System.Windows.Forms.GroupBox();
      this.labelSegConnectMMAVal = new System.Windows.Forms.Label();
      this.labelSegConnectMMA = new System.Windows.Forms.Label();
      this.labelSegConnFailVal = new System.Windows.Forms.Label();
      this.labelSegConnFail = new System.Windows.Forms.Label();
      this.labelSegConnectionsVal = new System.Windows.Forms.Label();
      this.labelSegConnections = new System.Windows.Forms.Label();
      this.labelSegDataOvertimeVal = new System.Windows.Forms.Label();
      this.labelSegDataOvertime = new System.Windows.Forms.Label();
      this.labelSegReadDelayVal = new System.Windows.Forms.Label();
      this.labelSegReadDelay = new System.Windows.Forms.Label();
      this.labelSegWriteDelayVal = new System.Windows.Forms.Label();
      this.labelSegWriteDelay = new System.Windows.Forms.Label();
      this.labelSegAciveVal = new System.Windows.Forms.Label();
      this.labelSegAcive = new System.Windows.Forms.Label();
      this.labelSegStateVal = new System.Windows.Forms.Label();
      this.labelSegState = new System.Windows.Forms.Label();
      this.SegmentsListView = new System.Windows.Forms.ListView();
      this.SegmentColumn = new System.Windows.Forms.ColumnHeader();
      this.SegmentsImageList = new System.Windows.Forms.ImageList( this.components );
      this.tabPageProtocol = new System.Windows.Forms.TabPage();
      this.listBoxProtocol = new System.Windows.Forms.ListBox();
      this.groupBoxProtStat = new System.Windows.Forms.GroupBox();
      this.textBox_protpar = new System.Windows.Forms.TextBox();
      this.LabelProtocolRxSynchErrorVal = new System.Windows.Forms.Label();
      this.LabelProtocolRxSynchError = new System.Windows.Forms.Label();
      this.labelProtocolnvalidVVal = new System.Windows.Forms.Label();
      this.labelProtocolnvalidV = new System.Windows.Forms.Label();
      this.labeProtlMaxCharGapTime = new System.Windows.Forms.Label();
      this.labelCharGapLab = new System.Windows.Forms.Label();
      this.labelProtNAKVal = new System.Windows.Forms.Label();
      this.labelnaklAB = new System.Windows.Forms.Label();
      this.labelMaxResponseTime = new System.Windows.Forms.Label();
      this.labelMaxResTime = new System.Windows.Forms.Label();
      this.labelTimeOutCnt = new System.Windows.Forms.Label();
      this.labelTimeOutCountLab = new System.Windows.Forms.Label();
      this.labelIncompleteCnt = new System.Windows.Forms.Label();
      this.labelIncompleteLab = new System.Windows.Forms.Label();
      this.labelCRCErrorsCnt = new System.Windows.Forms.Label();
      this.labelCRCErrorLob = new System.Windows.Forms.Label();
      this.labelTXFramesCount = new System.Windows.Forms.Label();
      this.labelRXFramesLab = new System.Windows.Forms.Label();
      this.labelTXFramesLab = new System.Windows.Forms.Label();
      this.labelRXFramesCount = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.labelTxDBMinVal = new System.Windows.Forms.Label();
      this.labelTxDBMin = new System.Windows.Forms.Label();
      this.labelTxDBPlusVal = new System.Windows.Forms.Label();
      this.labelTxDBPlus = new System.Windows.Forms.Label();
      this.labelRxDBMinusVal = new System.Windows.Forms.Label();
      this.labelRxDBMin = new System.Windows.Forms.Label();
      this.labelRxDBPlusVal = new System.Windows.Forms.Label();
      this.labelRxDBPlus = new System.Windows.Forms.Label();
      this.labelTestTimeLab = new System.Windows.Forms.Label();
      this.labelTestTime = new System.Windows.Forms.Label();
      this.labelProgramName = new System.Windows.Forms.Label();
      this.MainFormMenu = new System.Windows.Forms.MainMenu( this.components );
      this.menu_MENU = new System.Windows.Forms.MenuItem();
      this.menu_report = new System.Windows.Forms.MenuItem();
      this.menu_Exit = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.menu_hide = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.menu_configurator = new System.Windows.Forms.MenuItem();
      this.menuItem_DCOM_configuration = new System.Windows.Forms.MenuItem();
      this.menuItem_XBUS_Measure = new System.Windows.Forms.MenuItem();
      this.menuItem5 = new System.Windows.Forms.MenuItem();
      this.menuItem_options = new System.Windows.Forms.MenuItem();
      this.menuItem_connectionTypeOptions = new System.Windows.Forms.MenuItem();
      this.menu_help = new System.Windows.Forms.MenuItem();
      this.menu_About = new System.Windows.Forms.MenuItem();
      this.menuItem6 = new System.Windows.Forms.MenuItem();
      this.notifyIcon_trayicon = new System.Windows.Forms.NotifyIcon( this.components );
      this.RefreshFormTimer = new System.Windows.Forms.Timer( this.components );
      this.labelRelease = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label_connected_to = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.menuItem7 = new System.Windows.Forms.MenuItem();
      this.tabControlInterface.SuspendLayout();
      this.tabStation.SuspendLayout();
      this.groupBoxCurrStation.SuspendLayout();
      this.tabPageInterface.SuspendLayout();
      this.groupBoxCurrInt.SuspendLayout();
      this.tabPageSegments.SuspendLayout();
      this.groupBoxSegmentCurr.SuspendLayout();
      this.tabPageProtocol.SuspendLayout();
      this.groupBoxProtStat.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControlInterface
      // 
      this.tabControlInterface.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tabControlInterface.Controls.Add( this.tabStation );
      this.tabControlInterface.Controls.Add( this.tabPageInterface );
      this.tabControlInterface.Controls.Add( this.tabPageSegments );
      this.tabControlInterface.Controls.Add( this.tabPageProtocol );
      this.tabControlInterface.Location = new System.Drawing.Point( 8, 100 );
      this.tabControlInterface.Name = "tabControlInterface";
      this.tabControlInterface.SelectedIndex = 0;
      this.tabControlInterface.ShowToolTips = true;
      this.tabControlInterface.Size = new System.Drawing.Size( 560, 377 );
      this.tabControlInterface.TabIndex = 0;
      // 
      // tabStation
      // 
      this.tabStation.BackColor = System.Drawing.SystemColors.Control;
      this.tabStation.Controls.Add( this.groupBoxCurrStation );
      this.tabStation.Controls.Add( this.StationsListView );
      this.tabStation.Location = new System.Drawing.Point( 4, 22 );
      this.tabStation.Name = "tabStation";
      this.tabStation.Size = new System.Drawing.Size( 552, 351 );
      this.tabStation.TabIndex = 3;
      this.tabStation.Text = "Station";
      // 
      // groupBoxCurrStation
      // 
      this.groupBoxCurrStation.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.groupBoxCurrStation.Controls.Add( this.label1 );
      this.groupBoxCurrStation.Controls.Add( this.label_StationCurrState );
      this.groupBoxCurrStation.Location = new System.Drawing.Point( 248, 16 );
      this.groupBoxCurrStation.Name = "groupBoxCurrStation";
      this.groupBoxCurrStation.Size = new System.Drawing.Size( 296, 64 );
      this.groupBoxCurrStation.TabIndex = 1;
      this.groupBoxCurrStation.TabStop = false;
      this.groupBoxCurrStation.Text = "Station Information";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point( 16, 37 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 88, 16 );
      this.label1.TabIndex = 78;
      this.label1.Text = "State";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label_StationCurrState
      // 
      this.label_StationCurrState.BackColor = System.Drawing.SystemColors.Info;
      this.label_StationCurrState.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label_StationCurrState.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.label_StationCurrState.Location = new System.Drawing.Point( 120, 37 );
      this.label_StationCurrState.Name = "label_StationCurrState";
      this.label_StationCurrState.Size = new System.Drawing.Size( 160, 16 );
      this.label_StationCurrState.TabIndex = 77;
      this.label_StationCurrState.Text = "no select";
      this.label_StationCurrState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // StationsListView
      // 
      this.StationsListView.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.StationsListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.StationHeader} );
      this.StationsListView.ContextMenu = this.TrayIconMenu;
      this.StationsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.StationsListView.Location = new System.Drawing.Point( 16, 16 );
      this.StationsListView.MultiSelect = false;
      this.StationsListView.Name = "StationsListView";
      this.StationsListView.Size = new System.Drawing.Size( 224, 321 );
      this.StationsListView.SmallImageList = this.StationImageList;
      this.StationsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.StationsListView.TabIndex = 0;
      this.StationsListView.UseCompatibleStateImageBehavior = false;
      this.StationsListView.View = System.Windows.Forms.View.Details;
      // 
      // StationHeader
      // 
      this.StationHeader.Text = "Stations";
      this.StationHeader.Width = 220;
      // 
      // TrayIconMenu
      // 
      this.TrayIconMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.CMItem_Exit,
            this.menuItem4} );
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 0;
      this.menuItem2.Text = "Hide";
      this.menuItem2.Click += new System.EventHandler( this.menu_Hide_Click );
      // 
      // CMItem_Exit
      // 
      this.CMItem_Exit.Index = 1;
      this.CMItem_Exit.Text = "Exit";
      this.CMItem_Exit.Click += new System.EventHandler( this.menu_Exit_Click );
      // 
      // menuItem4
      // 
      this.menuItem4.Index = 2;
      this.menuItem4.Text = "Show";
      this.menuItem4.Click += new System.EventHandler( this.CM_Show_click );
      // 
      // StationImageList
      // 
      this.StationImageList.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "StationImageList.ImageStream" ) ) );
      this.StationImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.StationImageList.Images.SetKeyName( 0, "stacja_fail_48_full.ico" );
      this.StationImageList.Images.SetKeyName( 1, "stacja_48_full.ico" );
      this.StationImageList.Images.SetKeyName( 2, "stacja_spec_48_full.ico" );
      this.StationImageList.Images.SetKeyName( 3, "stacja_spec_48_full.ico" );
      // 
      // tabPageInterface
      // 
      this.tabPageInterface.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageInterface.Controls.Add( this.groupBoxCurrInt );
      this.tabPageInterface.Controls.Add( this.InterfaceListView );
      this.tabPageInterface.Location = new System.Drawing.Point( 4, 22 );
      this.tabPageInterface.Name = "tabPageInterface";
      this.tabPageInterface.Size = new System.Drawing.Size( 552, 351 );
      this.tabPageInterface.TabIndex = 0;
      this.tabPageInterface.Text = "Port";
      // 
      // groupBoxCurrInt
      // 
      this.groupBoxCurrInt.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.groupBoxCurrInt.Controls.Add( this.labelIntStandByVal );
      this.groupBoxCurrInt.Controls.Add( this.labelIntStandBy );
      this.groupBoxCurrInt.Controls.Add( this.labelIntFailTimeVal );
      this.groupBoxCurrInt.Controls.Add( this.labelIntFailTime );
      this.groupBoxCurrInt.Controls.Add( this.labelIntActiveTimeVal );
      this.groupBoxCurrInt.Controls.Add( this.labelIntActiveTime );
      this.groupBoxCurrInt.Controls.Add( this.labelIntStateVal );
      this.groupBoxCurrInt.Controls.Add( this.labelIntState );
      this.groupBoxCurrInt.Location = new System.Drawing.Point( 312, 16 );
      this.groupBoxCurrInt.Name = "groupBoxCurrInt";
      this.groupBoxCurrInt.Size = new System.Drawing.Size( 232, 146 );
      this.groupBoxCurrInt.TabIndex = 4;
      this.groupBoxCurrInt.TabStop = false;
      // 
      // labelIntStandByVal
      // 
      this.labelIntStandByVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelIntStandByVal.Location = new System.Drawing.Point( 120, 117 );
      this.labelIntStandByVal.Name = "labelIntStandByVal";
      this.labelIntStandByVal.Size = new System.Drawing.Size( 96, 16 );
      this.labelIntStandByVal.TabIndex = 7;
      this.labelIntStandByVal.Text = "----";
      this.labelIntStandByVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIntStandBy
      // 
      this.labelIntStandBy.Location = new System.Drawing.Point( 20, 117 );
      this.labelIntStandBy.Name = "labelIntStandBy";
      this.labelIntStandBy.Size = new System.Drawing.Size( 80, 16 );
      this.labelIntStandBy.TabIndex = 6;
      this.labelIntStandBy.Text = "Standby time";
      this.labelIntStandBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIntFailTimeVal
      // 
      this.labelIntFailTimeVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelIntFailTimeVal.Location = new System.Drawing.Point( 120, 93 );
      this.labelIntFailTimeVal.Name = "labelIntFailTimeVal";
      this.labelIntFailTimeVal.Size = new System.Drawing.Size( 96, 16 );
      this.labelIntFailTimeVal.TabIndex = 5;
      this.labelIntFailTimeVal.Text = "----";
      this.labelIntFailTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIntFailTime
      // 
      this.labelIntFailTime.Location = new System.Drawing.Point( 20, 93 );
      this.labelIntFailTime.Name = "labelIntFailTime";
      this.labelIntFailTime.Size = new System.Drawing.Size( 80, 16 );
      this.labelIntFailTime.TabIndex = 4;
      this.labelIntFailTime.Text = "Fail time";
      this.labelIntFailTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIntActiveTimeVal
      // 
      this.labelIntActiveTimeVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelIntActiveTimeVal.Location = new System.Drawing.Point( 120, 69 );
      this.labelIntActiveTimeVal.Name = "labelIntActiveTimeVal";
      this.labelIntActiveTimeVal.Size = new System.Drawing.Size( 96, 16 );
      this.labelIntActiveTimeVal.TabIndex = 3;
      this.labelIntActiveTimeVal.Text = "----";
      this.labelIntActiveTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIntActiveTime
      // 
      this.labelIntActiveTime.Location = new System.Drawing.Point( 20, 69 );
      this.labelIntActiveTime.Name = "labelIntActiveTime";
      this.labelIntActiveTime.Size = new System.Drawing.Size( 80, 16 );
      this.labelIntActiveTime.TabIndex = 2;
      this.labelIntActiveTime.Text = "Active time";
      this.labelIntActiveTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIntStateVal
      // 
      this.labelIntStateVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelIntStateVal.Location = new System.Drawing.Point( 120, 45 );
      this.labelIntStateVal.Name = "labelIntStateVal";
      this.labelIntStateVal.Size = new System.Drawing.Size( 96, 16 );
      this.labelIntStateVal.TabIndex = 1;
      this.labelIntStateVal.Text = "----";
      this.labelIntStateVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIntState
      // 
      this.labelIntState.Location = new System.Drawing.Point( 20, 45 );
      this.labelIntState.Name = "labelIntState";
      this.labelIntState.Size = new System.Drawing.Size( 80, 16 );
      this.labelIntState.TabIndex = 0;
      this.labelIntState.Text = "Interface State";
      this.labelIntState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // InterfaceListView
      // 
      this.InterfaceListView.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.InterfaceListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.InterfaceColumn} );
      this.InterfaceListView.Location = new System.Drawing.Point( 8, 16 );
      this.InterfaceListView.Name = "InterfaceListView";
      this.InterfaceListView.Size = new System.Drawing.Size( 296, 284 );
      this.InterfaceListView.SmallImageList = this.InterfaceImageList;
      this.InterfaceListView.TabIndex = 1;
      this.InterfaceListView.UseCompatibleStateImageBehavior = false;
      this.InterfaceListView.View = System.Windows.Forms.View.Details;
      // 
      // InterfaceColumn
      // 
      this.InterfaceColumn.Text = "Interfaces";
      this.InterfaceColumn.Width = 284;
      // 
      // InterfaceImageList
      // 
      this.InterfaceImageList.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "InterfaceImageList.ImageStream" ) ) );
      this.InterfaceImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.InterfaceImageList.Images.SetKeyName( 0, "port_48_full.ico" );
      this.InterfaceImageList.Images.SetKeyName( 1, "port_fail_48_full.ico" );
      this.InterfaceImageList.Images.SetKeyName( 2, "port_sb_48_full.ico" );
      this.InterfaceImageList.Images.SetKeyName( 3, "port_spec_48full.ico" );
      // 
      // tabPageSegments
      // 
      this.tabPageSegments.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageSegments.Controls.Add( this.groupBoxSegmentCurr );
      this.tabPageSegments.Controls.Add( this.SegmentsListView );
      this.tabPageSegments.Location = new System.Drawing.Point( 4, 22 );
      this.tabPageSegments.Name = "tabPageSegments";
      this.tabPageSegments.Size = new System.Drawing.Size( 552, 351 );
      this.tabPageSegments.TabIndex = 2;
      this.tabPageSegments.Text = "Segments";
      // 
      // groupBoxSegmentCurr
      // 
      this.groupBoxSegmentCurr.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegConnectMMAVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegConnectMMA );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegConnFailVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegConnFail );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegConnectionsVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegConnections );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegDataOvertimeVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegDataOvertime );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegReadDelayVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegReadDelay );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegWriteDelayVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegWriteDelay );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegAciveVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegAcive );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegStateVal );
      this.groupBoxSegmentCurr.Controls.Add( this.labelSegState );
      this.groupBoxSegmentCurr.Location = new System.Drawing.Point( 192, 16 );
      this.groupBoxSegmentCurr.Name = "groupBoxSegmentCurr";
      this.groupBoxSegmentCurr.Size = new System.Drawing.Size( 344, 329 );
      this.groupBoxSegmentCurr.TabIndex = 6;
      this.groupBoxSegmentCurr.TabStop = false;
      // 
      // labelSegConnectMMAVal
      // 
      this.labelSegConnectMMAVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegConnectMMAVal.Location = new System.Drawing.Point( 176, 252 );
      this.labelSegConnectMMAVal.Name = "labelSegConnectMMAVal";
      this.labelSegConnectMMAVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegConnectMMAVal.TabIndex = 15;
      this.labelSegConnectMMAVal.Text = "----";
      this.labelSegConnectMMAVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegConnectMMA
      // 
      this.labelSegConnectMMA.Location = new System.Drawing.Point( 8, 256 );
      this.labelSegConnectMMA.Name = "labelSegConnectMMA";
      this.labelSegConnectMMA.Size = new System.Drawing.Size( 168, 16 );
      this.labelSegConnectMMA.TabIndex = 14;
      this.labelSegConnectMMA.Text = "Connect time[ms]";
      this.labelSegConnectMMA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegConnFailVal
      // 
      this.labelSegConnFailVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegConnFailVal.Location = new System.Drawing.Point( 176, 150 );
      this.labelSegConnFailVal.Name = "labelSegConnFailVal";
      this.labelSegConnFailVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegConnFailVal.TabIndex = 13;
      this.labelSegConnFailVal.Text = "----";
      this.labelSegConnFailVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegConnFail
      // 
      this.labelSegConnFail.Location = new System.Drawing.Point( 8, 154 );
      this.labelSegConnFail.Name = "labelSegConnFail";
      this.labelSegConnFail.Size = new System.Drawing.Size( 168, 16 );
      this.labelSegConnFail.TabIndex = 12;
      this.labelSegConnFail.Text = "Number of connections failed";
      this.labelSegConnFail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegConnectionsVal
      // 
      this.labelSegConnectionsVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegConnectionsVal.Location = new System.Drawing.Point( 176, 116 );
      this.labelSegConnectionsVal.Name = "labelSegConnectionsVal";
      this.labelSegConnectionsVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegConnectionsVal.TabIndex = 11;
      this.labelSegConnectionsVal.Text = "----";
      this.labelSegConnectionsVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegConnections
      // 
      this.labelSegConnections.Location = new System.Drawing.Point( -17, 120 );
      this.labelSegConnections.Name = "labelSegConnections";
      this.labelSegConnections.Size = new System.Drawing.Size( 194, 21 );
      this.labelSegConnections.TabIndex = 10;
      this.labelSegConnections.Text = "Number of successful connections";
      this.labelSegConnections.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegDataOvertimeVal
      // 
      this.labelSegDataOvertimeVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegDataOvertimeVal.Location = new System.Drawing.Point( 176, 286 );
      this.labelSegDataOvertimeVal.Name = "labelSegDataOvertimeVal";
      this.labelSegDataOvertimeVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegDataOvertimeVal.TabIndex = 9;
      this.labelSegDataOvertimeVal.Text = "----";
      this.labelSegDataOvertimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegDataOvertime
      // 
      this.labelSegDataOvertime.Location = new System.Drawing.Point( 8, 290 );
      this.labelSegDataOvertime.Name = "labelSegDataOvertime";
      this.labelSegDataOvertime.Size = new System.Drawing.Size( 168, 16 );
      this.labelSegDataOvertime.TabIndex = 8;
      this.labelSegDataOvertime.Text = "Data overtime (Min/Avr/Max) [%]";
      this.labelSegDataOvertime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegReadDelayVal
      // 
      this.labelSegReadDelayVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegReadDelayVal.Location = new System.Drawing.Point( 176, 218 );
      this.labelSegReadDelayVal.Name = "labelSegReadDelayVal";
      this.labelSegReadDelayVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegReadDelayVal.TabIndex = 7;
      this.labelSegReadDelayVal.Text = "----";
      this.labelSegReadDelayVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegReadDelay
      // 
      this.labelSegReadDelay.Location = new System.Drawing.Point( 8, 222 );
      this.labelSegReadDelay.Name = "labelSegReadDelay";
      this.labelSegReadDelay.Size = new System.Drawing.Size( 168, 16 );
      this.labelSegReadDelay.TabIndex = 6;
      this.labelSegReadDelay.Text = "Read delay time[ms]";
      this.labelSegReadDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegWriteDelayVal
      // 
      this.labelSegWriteDelayVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegWriteDelayVal.Location = new System.Drawing.Point( 176, 184 );
      this.labelSegWriteDelayVal.Name = "labelSegWriteDelayVal";
      this.labelSegWriteDelayVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegWriteDelayVal.TabIndex = 5;
      this.labelSegWriteDelayVal.Text = "----";
      this.labelSegWriteDelayVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegWriteDelay
      // 
      this.labelSegWriteDelay.Location = new System.Drawing.Point( 8, 188 );
      this.labelSegWriteDelay.Name = "labelSegWriteDelay";
      this.labelSegWriteDelay.Size = new System.Drawing.Size( 168, 16 );
      this.labelSegWriteDelay.TabIndex = 4;
      this.labelSegWriteDelay.Text = "Write delay time[ms]";
      this.labelSegWriteDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegAciveVal
      // 
      this.labelSegAciveVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegAciveVal.Location = new System.Drawing.Point( 176, 82 );
      this.labelSegAciveVal.Name = "labelSegAciveVal";
      this.labelSegAciveVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegAciveVal.TabIndex = 3;
      this.labelSegAciveVal.Text = "----";
      this.labelSegAciveVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegAcive
      // 
      this.labelSegAcive.Location = new System.Drawing.Point( 8, 86 );
      this.labelSegAcive.Name = "labelSegAcive";
      this.labelSegAcive.Size = new System.Drawing.Size( 168, 16 );
      this.labelSegAcive.TabIndex = 2;
      this.labelSegAcive.Text = "Total connect time [s]";
      this.labelSegAcive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegStateVal
      // 
      this.labelSegStateVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelSegStateVal.Location = new System.Drawing.Point( 176, 48 );
      this.labelSegStateVal.Name = "labelSegStateVal";
      this.labelSegStateVal.Size = new System.Drawing.Size( 160, 25 );
      this.labelSegStateVal.TabIndex = 1;
      this.labelSegStateVal.Text = "----";
      this.labelSegStateVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelSegState
      // 
      this.labelSegState.Location = new System.Drawing.Point( 8, 52 );
      this.labelSegState.Name = "labelSegState";
      this.labelSegState.Size = new System.Drawing.Size( 168, 16 );
      this.labelSegState.TabIndex = 0;
      this.labelSegState.Text = "Segment state";
      this.labelSegState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // SegmentsListView
      // 
      this.SegmentsListView.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.SegmentsListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.SegmentColumn} );
      this.SegmentsListView.ContextMenu = this.TrayIconMenu;
      this.SegmentsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.SegmentsListView.Location = new System.Drawing.Point( 8, 16 );
      this.SegmentsListView.MultiSelect = false;
      this.SegmentsListView.Name = "SegmentsListView";
      this.SegmentsListView.Size = new System.Drawing.Size( 176, 329 );
      this.SegmentsListView.SmallImageList = this.SegmentsImageList;
      this.SegmentsListView.TabIndex = 0;
      this.SegmentsListView.UseCompatibleStateImageBehavior = false;
      this.SegmentsListView.View = System.Windows.Forms.View.Details;
      // 
      // SegmentColumn
      // 
      this.SegmentColumn.Text = "Segments";
      this.SegmentColumn.Width = 170;
      // 
      // SegmentsImageList
      // 
      this.SegmentsImageList.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "SegmentsImageList.ImageStream" ) ) );
      this.SegmentsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.SegmentsImageList.Images.SetKeyName( 0, "segment_fail_48_full.ico" );
      this.SegmentsImageList.Images.SetKeyName( 1, "segment_48_full.ico" );
      this.SegmentsImageList.Images.SetKeyName( 2, "segment_spec_48_full.ico" );
      this.SegmentsImageList.Images.SetKeyName( 3, "segment_spec_48_full.ico" );
      // 
      // tabPageProtocol
      // 
      this.tabPageProtocol.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageProtocol.Controls.Add( this.listBoxProtocol );
      this.tabPageProtocol.Controls.Add( this.groupBoxProtStat );
      this.tabPageProtocol.Controls.Add( this.groupBox1 );
      this.tabPageProtocol.Location = new System.Drawing.Point( 4, 22 );
      this.tabPageProtocol.Name = "tabPageProtocol";
      this.tabPageProtocol.Size = new System.Drawing.Size( 552, 351 );
      this.tabPageProtocol.TabIndex = 1;
      this.tabPageProtocol.Text = "Protocol";
      // 
      // listBoxProtocol
      // 
      this.listBoxProtocol.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.listBoxProtocol.Location = new System.Drawing.Point( 16, 18 );
      this.listBoxProtocol.Name = "listBoxProtocol";
      this.listBoxProtocol.Size = new System.Drawing.Size( 168, 173 );
      this.listBoxProtocol.TabIndex = 80;
      // 
      // groupBoxProtStat
      // 
      this.groupBoxProtStat.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.groupBoxProtStat.Controls.Add( this.textBox_protpar );
      this.groupBoxProtStat.Controls.Add( this.LabelProtocolRxSynchErrorVal );
      this.groupBoxProtStat.Controls.Add( this.LabelProtocolRxSynchError );
      this.groupBoxProtStat.Controls.Add( this.labelProtocolnvalidVVal );
      this.groupBoxProtStat.Controls.Add( this.labelProtocolnvalidV );
      this.groupBoxProtStat.Controls.Add( this.labeProtlMaxCharGapTime );
      this.groupBoxProtStat.Controls.Add( this.labelCharGapLab );
      this.groupBoxProtStat.Controls.Add( this.labelProtNAKVal );
      this.groupBoxProtStat.Controls.Add( this.labelnaklAB );
      this.groupBoxProtStat.Controls.Add( this.labelMaxResponseTime );
      this.groupBoxProtStat.Controls.Add( this.labelMaxResTime );
      this.groupBoxProtStat.Controls.Add( this.labelTimeOutCnt );
      this.groupBoxProtStat.Controls.Add( this.labelTimeOutCountLab );
      this.groupBoxProtStat.Controls.Add( this.labelIncompleteCnt );
      this.groupBoxProtStat.Controls.Add( this.labelIncompleteLab );
      this.groupBoxProtStat.Controls.Add( this.labelCRCErrorsCnt );
      this.groupBoxProtStat.Controls.Add( this.labelCRCErrorLob );
      this.groupBoxProtStat.Controls.Add( this.labelTXFramesCount );
      this.groupBoxProtStat.Controls.Add( this.labelRXFramesLab );
      this.groupBoxProtStat.Controls.Add( this.labelTXFramesLab );
      this.groupBoxProtStat.Controls.Add( this.labelRXFramesCount );
      this.groupBoxProtStat.Location = new System.Drawing.Point( 190, 18 );
      this.groupBoxProtStat.Name = "groupBoxProtStat";
      this.groupBoxProtStat.Size = new System.Drawing.Size( 346, 318 );
      this.groupBoxProtStat.TabIndex = 60;
      this.groupBoxProtStat.TabStop = false;
      this.groupBoxProtStat.Text = "Data Provider Information";
      // 
      // textBox_protpar
      // 
      this.textBox_protpar.BackColor = System.Drawing.SystemColors.Info;
      this.textBox_protpar.Location = new System.Drawing.Point( 10, 23 );
      this.textBox_protpar.Multiline = true;
      this.textBox_protpar.Name = "textBox_protpar";
      this.textBox_protpar.ReadOnly = true;
      this.textBox_protpar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox_protpar.Size = new System.Drawing.Size( 320, 72 );
      this.textBox_protpar.TabIndex = 81;
      // 
      // LabelProtocolRxSynchErrorVal
      // 
      this.LabelProtocolRxSynchErrorVal.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.LabelProtocolRxSynchErrorVal.BackColor = System.Drawing.SystemColors.Info;
      this.LabelProtocolRxSynchErrorVal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.LabelProtocolRxSynchErrorVal.Location = new System.Drawing.Point( 170, 219 );
      this.LabelProtocolRxSynchErrorVal.Name = "LabelProtocolRxSynchErrorVal";
      this.LabelProtocolRxSynchErrorVal.Size = new System.Drawing.Size( 64, 16 );
      this.LabelProtocolRxSynchErrorVal.TabIndex = 75;
      this.LabelProtocolRxSynchErrorVal.Text = "0";
      this.LabelProtocolRxSynchErrorVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // LabelProtocolRxSynchError
      // 
      this.LabelProtocolRxSynchError.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.LabelProtocolRxSynchError.Location = new System.Drawing.Point( 42, 219 );
      this.LabelProtocolRxSynchError.Name = "LabelProtocolRxSynchError";
      this.LabelProtocolRxSynchError.Size = new System.Drawing.Size( 120, 16 );
      this.LabelProtocolRxSynchError.TabIndex = 74;
      this.LabelProtocolRxSynchError.Text = "Rx synchro errors";
      this.LabelProtocolRxSynchError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelProtocolnvalidVVal
      // 
      this.labelProtocolnvalidVVal.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelProtocolnvalidVVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelProtocolnvalidVVal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelProtocolnvalidVVal.Location = new System.Drawing.Point( 170, 195 );
      this.labelProtocolnvalidVVal.Name = "labelProtocolnvalidVVal";
      this.labelProtocolnvalidVVal.Size = new System.Drawing.Size( 64, 16 );
      this.labelProtocolnvalidVVal.TabIndex = 73;
      this.labelProtocolnvalidVVal.Text = "0";
      this.labelProtocolnvalidVVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelProtocolnvalidV
      // 
      this.labelProtocolnvalidV.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelProtocolnvalidV.Location = new System.Drawing.Point( 42, 195 );
      this.labelProtocolnvalidV.Name = "labelProtocolnvalidV";
      this.labelProtocolnvalidV.Size = new System.Drawing.Size( 120, 16 );
      this.labelProtocolnvalidV.TabIndex = 72;
      this.labelProtocolnvalidV.Text = "Invalid rx frames";
      this.labelProtocolnvalidV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labeProtlMaxCharGapTime
      // 
      this.labeProtlMaxCharGapTime.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labeProtlMaxCharGapTime.BackColor = System.Drawing.SystemColors.Info;
      this.labeProtlMaxCharGapTime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labeProtlMaxCharGapTime.Location = new System.Drawing.Point( 170, 291 );
      this.labeProtlMaxCharGapTime.Name = "labeProtlMaxCharGapTime";
      this.labeProtlMaxCharGapTime.Size = new System.Drawing.Size( 160, 16 );
      this.labeProtlMaxCharGapTime.TabIndex = 71;
      this.labeProtlMaxCharGapTime.Text = "0";
      this.labeProtlMaxCharGapTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCharGapLab
      // 
      this.labelCharGapLab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelCharGapLab.Location = new System.Drawing.Point( 42, 291 );
      this.labelCharGapLab.Name = "labelCharGapLab";
      this.labelCharGapLab.Size = new System.Drawing.Size( 120, 16 );
      this.labelCharGapLab.TabIndex = 70;
      this.labelCharGapLab.Text = "Max char gap [us]";
      this.labelCharGapLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelProtNAKVal
      // 
      this.labelProtNAKVal.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelProtNAKVal.BackColor = System.Drawing.SystemColors.Info;
      this.labelProtNAKVal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelProtNAKVal.Location = new System.Drawing.Point( 170, 243 );
      this.labelProtNAKVal.Name = "labelProtNAKVal";
      this.labelProtNAKVal.Size = new System.Drawing.Size( 64, 16 );
      this.labelProtNAKVal.TabIndex = 69;
      this.labelProtNAKVal.Text = "0";
      this.labelProtNAKVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelnaklAB
      // 
      this.labelnaklAB.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelnaklAB.Location = new System.Drawing.Point( 42, 243 );
      this.labelnaklAB.Name = "labelnaklAB";
      this.labelnaklAB.Size = new System.Drawing.Size( 120, 16 );
      this.labelnaklAB.TabIndex = 68;
      this.labelnaklAB.Text = "NAK counter";
      this.labelnaklAB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelMaxResponseTime
      // 
      this.labelMaxResponseTime.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelMaxResponseTime.BackColor = System.Drawing.SystemColors.Info;
      this.labelMaxResponseTime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelMaxResponseTime.Location = new System.Drawing.Point( 170, 267 );
      this.labelMaxResponseTime.Name = "labelMaxResponseTime";
      this.labelMaxResponseTime.Size = new System.Drawing.Size( 160, 16 );
      this.labelMaxResponseTime.TabIndex = 67;
      this.labelMaxResponseTime.Text = "0";
      this.labelMaxResponseTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelMaxResTime
      // 
      this.labelMaxResTime.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelMaxResTime.Location = new System.Drawing.Point( 42, 267 );
      this.labelMaxResTime.Name = "labelMaxResTime";
      this.labelMaxResTime.Size = new System.Drawing.Size( 120, 16 );
      this.labelMaxResTime.TabIndex = 66;
      this.labelMaxResTime.Text = "Max response [ms]";
      this.labelMaxResTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelTimeOutCnt
      // 
      this.labelTimeOutCnt.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelTimeOutCnt.BackColor = System.Drawing.SystemColors.Info;
      this.labelTimeOutCnt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelTimeOutCnt.Location = new System.Drawing.Point( 170, 171 );
      this.labelTimeOutCnt.Name = "labelTimeOutCnt";
      this.labelTimeOutCnt.Size = new System.Drawing.Size( 64, 16 );
      this.labelTimeOutCnt.TabIndex = 65;
      this.labelTimeOutCnt.Text = "0";
      this.labelTimeOutCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelTimeOutCountLab
      // 
      this.labelTimeOutCountLab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelTimeOutCountLab.Location = new System.Drawing.Point( 42, 171 );
      this.labelTimeOutCountLab.Name = "labelTimeOutCountLab";
      this.labelTimeOutCountLab.Size = new System.Drawing.Size( 120, 16 );
      this.labelTimeOutCountLab.TabIndex = 64;
      this.labelTimeOutCountLab.Text = "Time-out count";
      this.labelTimeOutCountLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIncompleteCnt
      // 
      this.labelIncompleteCnt.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelIncompleteCnt.BackColor = System.Drawing.SystemColors.Info;
      this.labelIncompleteCnt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelIncompleteCnt.Location = new System.Drawing.Point( 170, 147 );
      this.labelIncompleteCnt.Name = "labelIncompleteCnt";
      this.labelIncompleteCnt.Size = new System.Drawing.Size( 64, 16 );
      this.labelIncompleteCnt.TabIndex = 63;
      this.labelIncompleteCnt.Text = "0";
      this.labelIncompleteCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelIncompleteLab
      // 
      this.labelIncompleteLab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelIncompleteLab.Location = new System.Drawing.Point( 42, 147 );
      this.labelIncompleteLab.Name = "labelIncompleteLab";
      this.labelIncompleteLab.Size = new System.Drawing.Size( 120, 16 );
      this.labelIncompleteLab.TabIndex = 62;
      this.labelIncompleteLab.Text = "Incomplete frames";
      this.labelIncompleteLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCRCErrorsCnt
      // 
      this.labelCRCErrorsCnt.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelCRCErrorsCnt.BackColor = System.Drawing.SystemColors.Info;
      this.labelCRCErrorsCnt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelCRCErrorsCnt.Location = new System.Drawing.Point( 170, 123 );
      this.labelCRCErrorsCnt.Name = "labelCRCErrorsCnt";
      this.labelCRCErrorsCnt.Size = new System.Drawing.Size( 64, 16 );
      this.labelCRCErrorsCnt.TabIndex = 61;
      this.labelCRCErrorsCnt.Text = "0";
      this.labelCRCErrorsCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelCRCErrorLob
      // 
      this.labelCRCErrorLob.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelCRCErrorLob.Location = new System.Drawing.Point( 42, 123 );
      this.labelCRCErrorLob.Name = "labelCRCErrorLob";
      this.labelCRCErrorLob.Size = new System.Drawing.Size( 120, 16 );
      this.labelCRCErrorLob.TabIndex = 60;
      this.labelCRCErrorLob.Text = "CRC errors";
      this.labelCRCErrorLob.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelTXFramesCount
      // 
      this.labelTXFramesCount.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelTXFramesCount.BackColor = System.Drawing.SystemColors.Info;
      this.labelTXFramesCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelTXFramesCount.Location = new System.Drawing.Point( 170, 99 );
      this.labelTXFramesCount.Name = "labelTXFramesCount";
      this.labelTXFramesCount.Size = new System.Drawing.Size( 64, 16 );
      this.labelTXFramesCount.TabIndex = 59;
      this.labelTXFramesCount.Text = "0";
      this.labelTXFramesCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelRXFramesLab
      // 
      this.labelRXFramesLab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelRXFramesLab.Location = new System.Drawing.Point( 42, 75 );
      this.labelRXFramesLab.Name = "labelRXFramesLab";
      this.labelRXFramesLab.Size = new System.Drawing.Size( 120, 16 );
      this.labelRXFramesLab.TabIndex = 19;
      this.labelRXFramesLab.Text = "RX frames";
      this.labelRXFramesLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelTXFramesLab
      // 
      this.labelTXFramesLab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelTXFramesLab.Location = new System.Drawing.Point( 42, 99 );
      this.labelTXFramesLab.Name = "labelTXFramesLab";
      this.labelTXFramesLab.Size = new System.Drawing.Size( 120, 16 );
      this.labelTXFramesLab.TabIndex = 18;
      this.labelTXFramesLab.Text = "TXframes";
      this.labelTXFramesLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelRXFramesCount
      // 
      this.labelRXFramesCount.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelRXFramesCount.BackColor = System.Drawing.SystemColors.Info;
      this.labelRXFramesCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelRXFramesCount.Location = new System.Drawing.Point( 170, 75 );
      this.labelRXFramesCount.Name = "labelRXFramesCount";
      this.labelRXFramesCount.Size = new System.Drawing.Size( 64, 16 );
      this.labelRXFramesCount.TabIndex = 58;
      this.labelRXFramesCount.Text = "0";
      this.labelRXFramesCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.groupBox1.Controls.Add( this.labelTxDBMinVal );
      this.groupBox1.Controls.Add( this.labelTxDBMin );
      this.groupBox1.Controls.Add( this.labelTxDBPlusVal );
      this.groupBox1.Controls.Add( this.labelTxDBPlus );
      this.groupBox1.Controls.Add( this.labelRxDBMinusVal );
      this.groupBox1.Controls.Add( this.labelRxDBMin );
      this.groupBox1.Controls.Add( this.labelRxDBPlusVal );
      this.groupBox1.Controls.Add( this.labelRxDBPlus );
      this.groupBox1.Location = new System.Drawing.Point( 16, 209 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size( 168, 128 );
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Protocol";
      // 
      // labelTxDBMinVal
      // 
      this.labelTxDBMinVal.BackColor = System.Drawing.SystemColors.HotTrack;
      this.labelTxDBMinVal.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.labelTxDBMinVal.Location = new System.Drawing.Point( 88, 96 );
      this.labelTxDBMinVal.Name = "labelTxDBMinVal";
      this.labelTxDBMinVal.Size = new System.Drawing.Size( 64, 16 );
      this.labelTxDBMinVal.TabIndex = 7;
      this.labelTxDBMinVal.Text = "0";
      this.labelTxDBMinVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelTxDBMin
      // 
      this.labelTxDBMin.Location = new System.Drawing.Point( 16, 96 );
      this.labelTxDBMin.Name = "labelTxDBMin";
      this.labelTxDBMin.Size = new System.Drawing.Size( 64, 16 );
      this.labelTxDBMin.TabIndex = 6;
      this.labelTxDBMin.Text = "Tx data -";
      this.labelTxDBMin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // labelTxDBPlusVal
      // 
      this.labelTxDBPlusVal.BackColor = System.Drawing.SystemColors.HotTrack;
      this.labelTxDBPlusVal.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.labelTxDBPlusVal.Location = new System.Drawing.Point( 88, 72 );
      this.labelTxDBPlusVal.Name = "labelTxDBPlusVal";
      this.labelTxDBPlusVal.Size = new System.Drawing.Size( 64, 16 );
      this.labelTxDBPlusVal.TabIndex = 5;
      this.labelTxDBPlusVal.Text = "0";
      this.labelTxDBPlusVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelTxDBPlus
      // 
      this.labelTxDBPlus.Location = new System.Drawing.Point( 16, 72 );
      this.labelTxDBPlus.Name = "labelTxDBPlus";
      this.labelTxDBPlus.Size = new System.Drawing.Size( 64, 16 );
      this.labelTxDBPlus.TabIndex = 4;
      this.labelTxDBPlus.Text = "Tx data +";
      this.labelTxDBPlus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // labelRxDBMinusVal
      // 
      this.labelRxDBMinusVal.BackColor = System.Drawing.SystemColors.HotTrack;
      this.labelRxDBMinusVal.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.labelRxDBMinusVal.Location = new System.Drawing.Point( 88, 48 );
      this.labelRxDBMinusVal.Name = "labelRxDBMinusVal";
      this.labelRxDBMinusVal.Size = new System.Drawing.Size( 64, 16 );
      this.labelRxDBMinusVal.TabIndex = 3;
      this.labelRxDBMinusVal.Text = "0";
      this.labelRxDBMinusVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelRxDBMin
      // 
      this.labelRxDBMin.Location = new System.Drawing.Point( 16, 48 );
      this.labelRxDBMin.Name = "labelRxDBMin";
      this.labelRxDBMin.Size = new System.Drawing.Size( 64, 16 );
      this.labelRxDBMin.TabIndex = 2;
      this.labelRxDBMin.Text = "Rx data -";
      this.labelRxDBMin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // labelRxDBPlusVal
      // 
      this.labelRxDBPlusVal.BackColor = System.Drawing.SystemColors.HotTrack;
      this.labelRxDBPlusVal.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.labelRxDBPlusVal.Location = new System.Drawing.Point( 88, 24 );
      this.labelRxDBPlusVal.Name = "labelRxDBPlusVal";
      this.labelRxDBPlusVal.Size = new System.Drawing.Size( 64, 16 );
      this.labelRxDBPlusVal.TabIndex = 1;
      this.labelRxDBPlusVal.Text = "0";
      this.labelRxDBPlusVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelRxDBPlus
      // 
      this.labelRxDBPlus.Location = new System.Drawing.Point( 16, 24 );
      this.labelRxDBPlus.Name = "labelRxDBPlus";
      this.labelRxDBPlus.Size = new System.Drawing.Size( 64, 16 );
      this.labelRxDBPlus.TabIndex = 0;
      this.labelRxDBPlus.Text = "Rx data +";
      this.labelRxDBPlus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // labelTestTimeLab
      // 
      this.labelTestTimeLab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelTestTimeLab.Location = new System.Drawing.Point( 202, 16 );
      this.labelTestTimeLab.Name = "labelTestTimeLab";
      this.labelTestTimeLab.Size = new System.Drawing.Size( 54, 16 );
      this.labelTestTimeLab.TabIndex = 78;
      this.labelTestTimeLab.Text = "Run time:";
      this.labelTestTimeLab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelTestTime
      // 
      this.labelTestTime.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelTestTime.BackColor = System.Drawing.SystemColors.Info;
      this.labelTestTime.ForeColor = System.Drawing.SystemColors.ControlText;
      this.labelTestTime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.labelTestTime.Location = new System.Drawing.Point( 262, 16 );
      this.labelTestTime.Name = "labelTestTime";
      this.labelTestTime.Size = new System.Drawing.Size( 94, 16 );
      this.labelTestTime.TabIndex = 77;
      this.labelTestTime.Text = "0";
      this.labelTestTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelProgramName
      // 
      this.labelProgramName.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelProgramName.AutoEllipsis = true;
      this.labelProgramName.Location = new System.Drawing.Point( 9, 18 );
      this.labelProgramName.Name = "labelProgramName";
      this.labelProgramName.Size = new System.Drawing.Size( 169, 14 );
      this.labelProgramName.TabIndex = 81;
      this.labelProgramName.Text = "Name";
      this.labelProgramName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // MainFormMenu
      // 
      this.MainFormMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menu_MENU,
            this.menuItem1,
            this.menuItem3,
            this.menu_help} );
      // 
      // menu_MENU
      // 
      this.menu_MENU.Index = 0;
      this.menu_MENU.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menu_report,
            this.menu_Exit} );
      this.menu_MENU.Text = "File";
      // 
      // menu_report
      // 
      this.menu_report.Index = 0;
      this.menu_report.Text = "Report";
      this.menu_report.Click += new System.EventHandler( this.menu_Report_Click );
      // 
      // menu_Exit
      // 
      this.menu_Exit.Index = 1;
      this.menu_Exit.Text = "Exit";
      this.menu_Exit.Click += new System.EventHandler( this.menu_Exit_Click );
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 1;
      this.menuItem1.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menu_hide} );
      this.menuItem1.Text = "View";
      // 
      // menu_hide
      // 
      this.menu_hide.Index = 0;
      this.menu_hide.Text = "Hide";
      this.menu_hide.Click += new System.EventHandler( this.menu_Hide_Click );
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 2;
      this.menuItem3.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menu_configurator,
            this.menuItem_DCOM_configuration,
            this.menuItem_XBUS_Measure,
            this.menuItem5,
            this.menuItem_options,
            this.menuItem_connectionTypeOptions} );
      this.menuItem3.Text = "Tools";
      // 
      // menu_configurator
      // 
      this.menu_configurator.Index = 0;
      this.menu_configurator.Text = "Configurator";
      this.menu_configurator.Click += new System.EventHandler( this.menu_Configurator_Click );
      // 
      // menuItem_DCOM_configuration
      // 
      this.menuItem_DCOM_configuration.Index = 1;
      this.menuItem_DCOM_configuration.Text = "DCOM Configuration";
      this.menuItem_DCOM_configuration.Click += new System.EventHandler( this.menuItem_DCOM_configuration_Click );
      // 
      // menuItem_XBUS_Measure
      // 
      this.menuItem_XBUS_Measure.Index = 2;
      this.menuItem_XBUS_Measure.Text = "XBUS Measure";
      this.menuItem_XBUS_Measure.Click += new System.EventHandler( this.menuItem_XBUS_Measure_Click );
      // 
      // menuItem5
      // 
      this.menuItem5.Index = 3;
      this.menuItem5.Text = "-";
      // 
      // menuItem_options
      // 
      this.menuItem_options.Index = 4;
      this.menuItem_options.Text = "Options";
      this.menuItem_options.Click += new System.EventHandler( this.menuItem_options_Click );
      // 
      // menuItem_connectionTypeOptions
      // 
      this.menuItem_connectionTypeOptions.Index = 5;
      this.menuItem_connectionTypeOptions.Text = "Connection Type Options";
      this.menuItem_connectionTypeOptions.Click += new System.EventHandler( this.menuItem_connectionTypeOptions_Click );
      // 
      // menu_help
      // 
      this.menu_help.Index = 3;
      this.menu_help.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menu_About,
            this.menuItem6,
            this.menuItem7} );
      this.menu_help.Text = "Help";
      // 
      // menu_About
      // 
      this.menu_About.Index = 0;
      this.menu_About.Shortcut = System.Windows.Forms.Shortcut.F2;
      this.menu_About.Text = "About CommServer";
      this.menu_About.Click += new System.EventHandler( this.menu_About_Click );
      // 
      // menuItem6
      // 
      this.menuItem6.Index = 1;
      this.menuItem6.Text = "License information";
      this.menuItem6.Click += new System.EventHandler( this.menuItem6_Click );
      // 
      // notifyIcon_trayicon
      // 
      this.notifyIcon_trayicon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
      this.notifyIcon_trayicon.BalloonTipText = "CommServer – click the icon to enlarge the window.";
      this.notifyIcon_trayicon.BalloonTipTitle = "CommServer";
      this.notifyIcon_trayicon.ContextMenu = this.TrayIconMenu;
      this.notifyIcon_trayicon.Icon = ( (System.Drawing.Icon)( resources.GetObject( "notifyIcon_trayicon.Icon" ) ) );
      this.notifyIcon_trayicon.Text = "CommServer";
      this.notifyIcon_trayicon.Visible = true;
      this.notifyIcon_trayicon.DoubleClick += new System.EventHandler( this.notifyIcon_trayicon_DoubleClick );
      // 
      // RefreshFormTimer
      // 
      this.RefreshFormTimer.Interval = 1000;
      this.RefreshFormTimer.Tick += new System.EventHandler( this.TimerTick );
      // 
      // labelRelease
      // 
      this.labelRelease.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.labelRelease.AutoEllipsis = true;
      this.labelRelease.Location = new System.Drawing.Point( 9, 37 );
      this.labelRelease.Name = "labelRelease";
      this.labelRelease.Size = new System.Drawing.Size( 169, 14 );
      this.labelRelease.TabIndex = 82;
      this.labelRelease.Text = "Release";
      this.labelRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
      this.groupBox2.Controls.Add( this.labelProgramName );
      this.groupBox2.Controls.Add( this.labelRelease );
      this.groupBox2.Controls.Add( this.label_connected_to );
      this.groupBox2.Controls.Add( this.label2 );
      this.groupBox2.Controls.Add( this.labelTestTime );
      this.groupBox2.Controls.Add( this.labelTestTimeLab );
      this.groupBox2.Location = new System.Drawing.Point( 199, 4 );
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size( 369, 82 );
      this.groupBox2.TabIndex = 83;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Connected server";
      // 
      // label_connected_to
      // 
      this.label_connected_to.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.label_connected_to.BackColor = System.Drawing.SystemColors.Info;
      this.label_connected_to.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label_connected_to.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.label_connected_to.Location = new System.Drawing.Point( 84, 55 );
      this.label_connected_to.Name = "label_connected_to";
      this.label_connected_to.Size = new System.Drawing.Size( 272, 16 );
      this.label_connected_to.TabIndex = 77;
      this.label_connected_to.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.label2.Location = new System.Drawing.Point( 9, 55 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 76, 16 );
      this.label2.TabIndex = 78;
      this.label2.Text = "Connected to:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // menuItem7
      // 
      this.menuItem7.Index = 2;
      this.menuItem7.Text = "Enter the unlock code";
      this.menuItem7.Click += new System.EventHandler( this.menuItem7_Click );
      // 
      // MainForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size( 576, 490 );
      this.ContextMenu = this.TrayIconMenu;
      this.Controls.Add( this.groupBox2 );
      this.Controls.Add( this.tabControlInterface );
      this.HelpButton = true;
      this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
      this.Menu = this.MainFormMenu;
      this.Name = "MainForm";
      this.Text = "CommServer Monitor";
      this.Load += new System.EventHandler( this.MainForm_Load );
      this.Closing += new System.ComponentModel.CancelEventHandler( this.EMainForm_Closing );
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.MainForm_FormClosing );
      this.Resize += new System.EventHandler( this.MainForm_Resize );
      this.tabControlInterface.ResumeLayout( false );
      this.tabStation.ResumeLayout( false );
      this.groupBoxCurrStation.ResumeLayout( false );
      this.tabPageInterface.ResumeLayout( false );
      this.groupBoxCurrInt.ResumeLayout( false );
      this.tabPageSegments.ResumeLayout( false );
      this.groupBoxSegmentCurr.ResumeLayout( false );
      this.tabPageProtocol.ResumeLayout( false );
      this.groupBoxProtStat.ResumeLayout( false );
      this.groupBoxProtStat.PerformLayout();
      this.groupBox1.ResumeLayout( false );
      this.groupBox2.ResumeLayout( false );
      this.ResumeLayout( false );

    }
    #endregion

    private System.Windows.Forms.TextBox textBox_protpar;
    private System.Windows.Forms.Label label_connected_to;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.MenuItem menuItem3;
    private System.Windows.Forms.MenuItem menuItem_options;
    private System.Windows.Forms.MenuItem menuItem_DCOM_configuration;
    private System.Windows.Forms.MenuItem menuItem5;
    private System.Windows.Forms.MenuItem menuItem_XBUS_Measure;
    private System.Windows.Forms.MenuItem menuItem_connectionTypeOptions;
    private System.Windows.Forms.MenuItem menuItem6;
    private System.Windows.Forms.MenuItem menuItem7;
  }
}
