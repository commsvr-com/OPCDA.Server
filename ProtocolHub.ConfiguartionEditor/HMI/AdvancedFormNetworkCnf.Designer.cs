//_______________________________________________________________
//  Title   : AdvancedFormNetworkConfig
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________


using CAS.NetworkConfigLib;
using CAS.Windows.Forms;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Summary description for Form MainInterface.
  /// </summary>
  public partial class AdvancedFormNetworkConfig
  {
    private System.Windows.Forms.DataGrid dataGrid2;
    private System.Windows.Forms.DataGrid dataGrid3;
    private System.Windows.Forms.TabControl TabNetworkStructure;
    private System.Windows.Forms.TabPage tabData;
    private System.Windows.Forms.TabPage tabData2;
    private System.Windows.Forms.DataGrid dataGrid1;
    private System.Windows.Forms.DataGrid dataGridSerial;
    private System.Windows.Forms.DataGrid dataGridProtocol;
    private System.Windows.Forms.OpenFileDialog openFileDialogXMLFile;
    private System.Windows.Forms.SaveFileDialog saveXMLFileDialog;
    private System.Diagnostics.EventLog eventLog1;
    private System.Windows.Forms.TabPage tabPageCommStruc;
    private System.Windows.Forms.TabPage tabPageProtocol;
    internal CAS.NetworkConfigLib.ComunicationNet configDataBase;
    private System.Windows.Forms.DataGrid dataGridChannels;
    private System.Windows.Forms.DataGridTableStyle ProtocolsTableStyle;
    private System.Windows.Forms.DataGridTextBoxColumn Protocols_ProtocolIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Protocols_NameColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Protocols_ChannelIDColumn;
    private DataGridComboBoxColumn Protocols_ChannelIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Protocols_ResponseTimeOutColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Protocols_FrameTimeOutColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Protocols_CharacterTimeOutColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Protocols_InterframeGapColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Protocols_MaxNumberColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Protocols_ProtocolTypeColumn;
    private DataGridComboBoxColumn Protocols_ProtocolTypeColumn;
    private System.Windows.Forms.DataGridTableStyle SerialportsTableSyle;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_SerialNumColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Serialports_ProtocolIDColumn;
    private DataGridComboBoxColumn Serialports_ProtocolIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_BaudRateColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_ParityColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_DataBitsColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_StopBitsColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_TXFlowCTSColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_TXFlowDSRColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_TxFlowXColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_WhenRxColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_RxGateDSRColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_RxFlowXColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_UseRTSColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_UseDTRColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_XonCharColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_XoffCharColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_rxHighWaterColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_rxLowWaterColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_sendTimeoutMultiplier;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_sendTimeoutConstantColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_rxQueueColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_txQueueColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_autoReopenColumn;
    private System.Windows.Forms.DataGridBoolColumn Serialports_checkAllSendsColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Serialports_SerialTypeColumn;
    private System.Windows.Forms.DataGridTableStyle SegmentsTableStyle;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_NameColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_SegmentIDColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Segments_ChannelIDColumn;
    private DataGridComboBoxColumn Segments_ChannelIDColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Segments_ProtocolIDColumn;
    private DataGridComboBoxColumn Segments_ProtocolIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_AddressColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_TimeScanColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_KeepConnectColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_PickupConnColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_timeKeepConnColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_TimeReconnectColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Segments_TimeIdleKeepConn;
    private System.Windows.Forms.DataGridTableStyle InterfacesTableStyle;
    private System.Windows.Forms.DataGridTextBoxColumn Interfaces_NameColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Interfaces_SegmentIDColumn;
    private DataGridComboBoxColumn Interfaces_SegmentIDColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Interfaces_ChannelIDColumn;
    private DataGridComboBoxColumn Interfaces_ChannelIDColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Interfaces_StationIDColumn;
    private DataGridComboBoxColumn Interfaces_StationIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Interfaces_AddressColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Interfaces_InactTimeColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Interfaces_InactTimeAFailureColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Interfaces_InterfaceNumColumn;
    private System.Windows.Forms.DataGridTableStyle GroupsTableStyle;
    private System.Windows.Forms.DataGridTextBoxColumn Groups_NameColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Groups_StationIDColumn;
    private DataGridComboBoxColumn Groups_StationIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Groups_GroupIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Groups_TimeScanColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Groups_TimeOutColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Groups_TimeScanFastColumn;
    private System.Windows.Forms.DataGridTextBoxColumn Groups_TimeOutFastColumn;
    private System.Windows.Forms.DataGridTableStyle DataBlocksTableStyle;
    private System.Windows.Forms.DataGridTextBoxColumn DataBlocks_NameColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn DataBlocks_StationIDColumn;
    private DataGridComboBoxColumn DataBlocks_StationIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn DataBlocks_AddressColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn DataBlocks_GroupIDColumn;
    private DataGridComboBoxColumn DataBlocks_GroupIDColumn;
    private System.Windows.Forms.DataGridTextBoxColumn DataBlocks_DataTypeColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn Tags_StationIDColumn;
    //private System.Windows.Forms.DataGridTextBoxColumn TagBit_StationIDColumn;

    private DataGridComboBoxColumn Tags_StationIDColumn = new DataGridComboBoxColumn();
    private DataGridComboBoxColumn TagBit_StationIDColumn = new DataGridComboBoxColumn();
    private DataGridComboBoxColumn TagBit_TagIDColumn = new DataGridComboBoxColumn();
    private DataGridComboBoxColumn Tags_DataTypeConversionColumn = new DataGridComboBoxColumn();

    //private System.Windows.Forms.DataGridTextBoxColumn TagBit_TagIDColumn;
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem menuItem3;
    private System.Windows.Forms.MenuItem menuItem4;
    private System.Windows.Forms.MenuItem menuItem6;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuFile_exit;
    private System.Windows.Forms.MenuItem menuFile_Open;
    private System.Windows.Forms.MenuItem menuFile_Clear_All;
    private System.Windows.Forms.MenuItem menuFile_Save;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.DataGrid dataGrid_Groups;
    private System.Windows.Forms.DataGrid dataGrid_datablocks;
    private BindingSource protocolBindingSource;
    private BindingSource channelsBindingSource;
    private BindingSource protocolsBindingSource;
    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel4;
    private TableLayoutPanel tableLayoutPanel5;
    private DataGrid dataGrid_tagbits;
    private DataGrid dataGrid_tags;
    private DataGrid dataGrid8;
    private MenuItem menuItemSBUS_bls;
    private MenuItem menuItemTagblosks_csv;
    private MenuItem menuItemTagBit_csv;
    private MenuItem menuItemTagMappings_csv;
    private MenuItem menuItemScanSettings_txt;
    private MenuItem menuItem10;
    private MenuItem menuItemMODBUS_bls;
    private MenuItem menuItemTags_for_Simulation;
    private IContainer components;
    private AdvancedFormNetworkConfig()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
      InitConfigDataBase( true );
      InitializeAdvanceComponent();
    }
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
    #region ComboBoxColumn
    internal class DataGridComboBoxColumn: DataGridTextBoxColumn
    {
      // Hosted combobox control
      private ComboBox comboBox;
      private CurrencyManager cm;
      private int iCurrentRow;
      // Constructor - create combobox, 
      // register selection change event handler,
      // register lose focus event handler
      public DataGridComboBoxColumn()
      {
        this.cm = null;

        // Create combobox and force DropDownList style
        this.comboBox = new ComboBox();
        this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        // Add event handler for notification when combobox loses focus
        this.comboBox.Leave += new EventHandler( comboBox_Leave );
      }
      // Property to provide access to combobox 
      public ComboBox ComboBox
      {
        get { return comboBox; }
      }

      // On edit, add scroll event handler, and display combobox
      protected override void Edit( System.Windows.Forms.CurrencyManager
          source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly,
          string instantText, bool cellIsVisible )
      {
        base.Edit( source, rowNum, bounds, readOnly, instantText,
            cellIsVisible );

        if ( !readOnly && cellIsVisible )
        {
          // Save current row in the DataGrid and currency manager 
          // associated with the data source for the DataGrid
          this.iCurrentRow = rowNum;
          this.cm = source;

          // Add event handler for DataGrid scroll notification
          this.DataGridTableStyle.DataGrid.Scroll
              += new EventHandler( DataGrid_Scroll );

          // Site the combobox control within the current cell
          this.comboBox.Parent = this.TextBox.Parent;
          Rectangle rect =
              this.DataGridTableStyle.DataGrid.GetCurrentCellBounds();
          this.comboBox.Location = rect.Location;
          this.comboBox.Size =
              new Size( this.TextBox.Size.Width,
              this.comboBox.Size.Height );

          // Set combobox selection to given text
          this.comboBox.SelectedIndex =
              this.comboBox.FindStringExact( this.TextBox.Text );

          // Make the combobox visible and place on top textbox control
          this.comboBox.Show();
          this.comboBox.BringToFront();
          this.comboBox.Focus();
        }

      }
      // Given a row, get the value member associated with a row.  Use the
      // value member to find the associated display member by iterating 
      // over bound data source
      protected override object
          GetColumnValueAtRow( System.Windows.Forms.CurrencyManager source,
          int rowNum )
      {
        // Given a row number in the DataGrid, get the display member
        object obj = base.GetColumnValueAtRow( source, rowNum );

        // Iterate through the data source bound to the ColumnComboBox
        CurrencyManager cm = (CurrencyManager)
            ( this.DataGridTableStyle.DataGrid.BindingContext[ this.comboBox.DataSource ] );
        // Assumes the associated DataGrid is bound to a DataView or 
        // DataTable 
        DataView dataview = ( (DataView)cm.List );

        int i;

        for ( i = 0; i < dataview.Count; i++ )
        {
          if ( obj.Equals( dataview[ i ][ this.comboBox.ValueMember ] ) )
            break;
        }

        if ( i < dataview.Count )
          return dataview[ i ][ this.comboBox.DisplayMember ];

        return DBNull.Value;
      }
      // Given a row and a display member, iterate over bound data source to 
      // find the associated value member.  Set this value member.
      protected override void
          SetColumnValueAtRow( System.Windows.Forms.CurrencyManager source,
          int rowNum, object value )
      {
        object s = value;

        // Iterate through the data source bound to the ColumnComboBox
        CurrencyManager cm = (CurrencyManager)
            ( this.DataGridTableStyle.DataGrid.BindingContext[ this.comboBox.DataSource ] );
        // Assumes the associated DataGrid is bound to a DataView or 
        // DataTable 
        DataView dataview = ( (DataView)cm.List );
        int i;

        for ( i = 0; i < dataview.Count; i++ )
        {
          if ( s.Equals( dataview[ i ][ this.comboBox.DisplayMember ] ) )
            break;
        }

        // If set item was found return corresponding value, 
        // otherwise return DbNull.Value
        if ( i < dataview.Count )
          s = dataview[ i ][ this.comboBox.ValueMember ];
        else
          s = DBNull.Value;

        int oldpos = source.Position;
        if ( source.Position != rowNum )
        {
          source.Position = rowNum;
        }
        base.SetColumnValueAtRow( source, rowNum, s );
        if ( source.Position != oldpos )
        {
          source.Position = oldpos;
        }
      }
      // On DataGrid scroll, hide the combobox
      private void DataGrid_Scroll( object sender, EventArgs e )
      {
        this.comboBox.Hide();
      }
      // On combobox losing focus, set the column value, hide the combobox,
      // and unregister scroll event handler
      private void comboBox_Leave( object sender, EventArgs e )
      {
        DataRowView rowView = (DataRowView)this.comboBox.SelectedItem;
        //try
        if ( this.comboBox.SelectedValue != null )
        {
          object s = rowView.Row[ this.comboBox.DisplayMember ];

          int oldpos = this.cm.Position;
          if ( this.cm.Position != this.iCurrentRow )
          {
            this.cm.Position = this.iCurrentRow;
          }
          SetColumnValueAtRow( this.cm, this.iCurrentRow, s );
          if ( this.cm.Position != oldpos )
          {
            this.cm.Position = oldpos;
          }
          Invalidate();
        }
        //catch (Exception er)
        /*else
        {
            //MessageBox.Show(er.ToString());
            MessageBox.Show("blah");
        }*/
        this.comboBox.Hide();
        this.DataGridTableStyle.DataGrid.Scroll -=
            new EventHandler( DataGrid_Scroll );
      }
    }

    #endregion
    private void InitializeAdvanceComponent()
    {
      //comboboxy w datagridach
      this.Protocols_ChannelIDColumn.Format = "";
      this.Protocols_ChannelIDColumn.FormatInfo = null;
      this.Protocols_ChannelIDColumn.HeaderText = "ChannelID";
      this.Protocols_ChannelIDColumn.MappingName = "ChannelID";
      this.Protocols_ChannelIDColumn.Width = 75;
      this.Protocols_ChannelIDColumn.ComboBox.DataSource = this.configDataBase.Channels;
      this.Protocols_ChannelIDColumn.ComboBox.DisplayMember = "Name";
      this.Protocols_ChannelIDColumn.ComboBox.ValueMember = "ChannelID";


      this.Protocols_ProtocolTypeColumn.Format = "";
      this.Protocols_ProtocolTypeColumn.FormatInfo = null;
      this.Protocols_ProtocolTypeColumn.HeaderText = "ProtocolType";
      this.Protocols_ProtocolTypeColumn.MappingName = "ProtocolType";
      this.Protocols_ProtocolTypeColumn.Width = 100;
      //this.Protocols_ProtocolTypeColumn.ComboBox.DataSource = this.Data_fromXML.Protocols;
      //this.Protocols_ProtocolTypeColumn.ComboBox.DisplayMember = "TypeName";
      //this.Protocols_ProtocolTypeColumn.ComboBox.ValueMember = "TypeID";

      this.Serialports_ProtocolIDColumn.Format = "";
      this.Serialports_ProtocolIDColumn.FormatInfo = null;
      this.Serialports_ProtocolIDColumn.HeaderText = "ProtocolID";
      this.Serialports_ProtocolIDColumn.MappingName = "ProtocolID";
      this.Serialports_ProtocolIDColumn.Width = 75;
      this.Serialports_ProtocolIDColumn.ComboBox.DataSource = this.configDataBase.Protocol;
      this.Serialports_ProtocolIDColumn.ComboBox.DisplayMember = "Name";
      this.Serialports_ProtocolIDColumn.ComboBox.ValueMember = "ProtocolID";

      this.Segments_ChannelIDColumn.Format = "";
      this.Segments_ChannelIDColumn.FormatInfo = null;
      this.Segments_ChannelIDColumn.HeaderText = "ChannelID";
      this.Segments_ChannelIDColumn.MappingName = "ChannelID";
      this.Segments_ChannelIDColumn.Width = 75;
      this.Segments_ChannelIDColumn.ComboBox.DataSource = this.configDataBase.Channels;
      this.Segments_ChannelIDColumn.ComboBox.DisplayMember = "Name";
      this.Segments_ChannelIDColumn.ComboBox.ValueMember = "ChannelID";

      this.Segments_ProtocolIDColumn.Format = "";
      this.Segments_ProtocolIDColumn.FormatInfo = null;
      this.Segments_ProtocolIDColumn.HeaderText = "ProtocolID";
      this.Segments_ProtocolIDColumn.MappingName = "ProtocolID";
      this.Segments_ProtocolIDColumn.Width = 75;
      this.Segments_ProtocolIDColumn.ComboBox.DataSource = this.configDataBase.Protocol;
      this.Segments_ProtocolIDColumn.ComboBox.DisplayMember = "Name";
      this.Segments_ProtocolIDColumn.ComboBox.ValueMember = "ProtocolID";

      this.Interfaces_SegmentIDColumn.Format = "";
      this.Interfaces_SegmentIDColumn.FormatInfo = null;
      this.Interfaces_SegmentIDColumn.HeaderText = "SegmentID";
      this.Interfaces_SegmentIDColumn.MappingName = "SegmentID";
      this.Interfaces_SegmentIDColumn.Width = 75;
      this.Interfaces_SegmentIDColumn.ComboBox.DataSource = this.configDataBase.Segments;
      this.Interfaces_SegmentIDColumn.ComboBox.DisplayMember = "Name";
      this.Interfaces_SegmentIDColumn.ComboBox.ValueMember = "SegmentID";

      this.Interfaces_ChannelIDColumn.Format = "";
      this.Interfaces_ChannelIDColumn.FormatInfo = null;
      this.Interfaces_ChannelIDColumn.HeaderText = "ChannelID";
      this.Interfaces_ChannelIDColumn.MappingName = "ChannelID";
      this.Interfaces_ChannelIDColumn.Width = 75;
      this.Interfaces_ChannelIDColumn.ComboBox.DataSource = this.configDataBase.Channels;
      this.Interfaces_ChannelIDColumn.ComboBox.DisplayMember = "Name";
      this.Interfaces_ChannelIDColumn.ComboBox.ValueMember = "ChannelID";

      this.Interfaces_StationIDColumn.Format = "";
      this.Interfaces_StationIDColumn.FormatInfo = null;
      this.Interfaces_StationIDColumn.HeaderText = "StationID";
      this.Interfaces_StationIDColumn.MappingName = "StationID";
      this.Interfaces_StationIDColumn.Width = 75;
      this.Interfaces_StationIDColumn.ComboBox.DataSource = this.configDataBase.Station;
      this.Interfaces_StationIDColumn.ComboBox.DisplayMember = "Name";
      this.Interfaces_StationIDColumn.ComboBox.ValueMember = "StationID";

      this.Groups_StationIDColumn.Format = "";
      this.Groups_StationIDColumn.FormatInfo = null;
      this.Groups_StationIDColumn.HeaderText = "StationID";
      this.Groups_StationIDColumn.MappingName = "StationID";
      this.Groups_StationIDColumn.Width = 75;
      this.Groups_StationIDColumn.ComboBox.DataSource = this.configDataBase.Station;
      this.Groups_StationIDColumn.ComboBox.DisplayMember = "Name";
      this.Groups_StationIDColumn.ComboBox.ValueMember = "StationID";

      this.DataBlocks_StationIDColumn.Format = "";
      this.DataBlocks_StationIDColumn.FormatInfo = null;
      this.DataBlocks_StationIDColumn.HeaderText = "StationID";
      this.DataBlocks_StationIDColumn.MappingName = "SatationID";
      this.DataBlocks_StationIDColumn.Width = 75;
      this.DataBlocks_StationIDColumn.ComboBox.DataSource = this.configDataBase.Station;
      this.DataBlocks_StationIDColumn.ComboBox.DisplayMember = "Name";
      this.DataBlocks_StationIDColumn.ComboBox.ValueMember = "StationID";

      this.DataBlocks_GroupIDColumn.Format = "";
      this.DataBlocks_GroupIDColumn.FormatInfo = null;
      this.DataBlocks_GroupIDColumn.HeaderText = "GroupID";
      this.DataBlocks_GroupIDColumn.MappingName = "GroupID";
      this.DataBlocks_GroupIDColumn.Width = 75;
      this.DataBlocks_GroupIDColumn.ComboBox.DataSource = this.configDataBase.Groups;
      this.DataBlocks_GroupIDColumn.ComboBox.DisplayMember = "Name";
      this.DataBlocks_GroupIDColumn.ComboBox.ValueMember = "GroupID";

      this.Tags_StationIDColumn.Format = "";
      this.Tags_StationIDColumn.FormatInfo = null;
      this.Tags_StationIDColumn.HeaderText = "StationID";
      this.Tags_StationIDColumn.MappingName = "StationID";
      this.Tags_StationIDColumn.Width = 75;
      this.Tags_StationIDColumn.ComboBox.DataSource = this.configDataBase.Station;
      this.Tags_StationIDColumn.ComboBox.DisplayMember = "Name";
      this.Tags_StationIDColumn.ComboBox.ValueMember = "StationID";

      this.TagBit_StationIDColumn.Format = "";
      this.TagBit_StationIDColumn.FormatInfo = null;
      this.TagBit_StationIDColumn.HeaderText = "StationID";
      this.TagBit_StationIDColumn.MappingName = "StationID";
      this.TagBit_StationIDColumn.Width = 75;
      this.TagBit_StationIDColumn.ComboBox.DataSource = this.configDataBase.Station;
      this.TagBit_StationIDColumn.ComboBox.DisplayMember = "Name";
      this.TagBit_StationIDColumn.ComboBox.ValueMember = "StationID";

      this.TagBit_TagIDColumn.Format = "";
      this.TagBit_TagIDColumn.FormatInfo = null;
      this.TagBit_TagIDColumn.HeaderText = "TagID";
      this.TagBit_TagIDColumn.MappingName = "TagID";
      this.TagBit_TagIDColumn.Width = 50;
      this.TagBit_TagIDColumn.ComboBox.DataSource = this.configDataBase.Tags;
      this.TagBit_TagIDColumn.ComboBox.DisplayMember = "Name";
      this.TagBit_TagIDColumn.ComboBox.ValueMember = "TagID";

      this.Tags_DataTypeConversionColumn.Format = "";
      this.Tags_DataTypeConversionColumn.FormatInfo = null;
      this.Tags_DataTypeConversionColumn.HeaderText = "DataTypeConversion";
      this.Tags_DataTypeConversionColumn.MappingName = "DataTypeConversion";
      this.Tags_DataTypeConversionColumn.Width = 75;
      //this.Tags_DataTypeConversionColumn.ComboBox.DataSource = this.Data_fromXML.OPCTypes;
      //this.Tags_DataTypeConversionColumn.ComboBox.DisplayMember = "TypeName";
      //this.Tags_DataTypeConversionColumn.ComboBox.ValueMember = "TypeID";

      //      this.Protocols_ProtocolTypeColumn.Format = "";
      //      this.Protocols_ProtocolTypeColumn.FormatInfo = null;
      //      this.Protocols_ProtocolTypeColumn.HeaderText = "ProtocolType";
      //      this.Protocols_ProtocolTypeColumn.MappingName = "ProtocolType";
      //      this.Protocols_ProtocolTypeColumn.Width = 100;
      //      this.Protocols_ProtocolTypeColumn.ComboBox.DataSource=this.prot1.Protocols;
      //      this.Protocols_ProtocolTypeColumn.ComboBox.DisplayMember="TypeName";
      //      this.Protocols_ProtocolTypeColumn.ComboBox.ValueMember="TypeID";

    }
    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    internal void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.TabNetworkStructure = new System.Windows.Forms.TabControl();
      this.tabPageProtocol = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGridSerial = new System.Windows.Forms.DataGrid();
      this.SerialportsTableSyle = new System.Windows.Forms.DataGridTableStyle();
      this.Serialports_SerialNumColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_ProtocolIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Serialports_BaudRateColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_ParityColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_DataBitsColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_StopBitsColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_TXFlowCTSColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_TXFlowDSRColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_TxFlowXColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_WhenRxColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_RxGateDSRColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_RxFlowXColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_UseRTSColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_UseDTRColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_XonCharColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_XoffCharColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_rxHighWaterColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_rxLowWaterColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_sendTimeoutMultiplier = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_sendTimeoutConstantColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_rxQueueColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_txQueueColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Serialports_autoReopenColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_checkAllSendsColumn = new System.Windows.Forms.DataGridBoolColumn();
      this.Serialports_SerialTypeColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGridProtocol = new System.Windows.Forms.DataGrid();
      this.ProtocolsTableStyle = new System.Windows.Forms.DataGridTableStyle();
      this.Protocols_ProtocolIDColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Protocols_NameColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Protocols_ChannelIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Protocols_ResponseTimeOutColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Protocols_FrameTimeOutColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Protocols_CharacterTimeOutColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Protocols_InterframeGapColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Protocols_MaxNumberColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Protocols_ProtocolTypeColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.dataGridChannels = new System.Windows.Forms.DataGrid();
      this.channelsBindingSource = new System.Windows.Forms.BindingSource( this.components );
      this.tabPageCommStruc = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGrid3 = new System.Windows.Forms.DataGrid();
      this.InterfacesTableStyle = new System.Windows.Forms.DataGridTableStyle();
      this.Interfaces_NameColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Interfaces_SegmentIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Interfaces_ChannelIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Interfaces_StationIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Interfaces_AddressColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Interfaces_InactTimeColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Interfaces_InactTimeAFailureColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Interfaces_InterfaceNumColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGrid2 = new System.Windows.Forms.DataGrid();
      this.SegmentsTableStyle = new System.Windows.Forms.DataGridTableStyle();
      this.Segments_NameColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_SegmentIDColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_ChannelIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Segments_ProtocolIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Segments_AddressColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_TimeScanColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_KeepConnectColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_PickupConnColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_timeKeepConnColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_TimeReconnectColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Segments_TimeIdleKeepConn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGrid8 = new System.Windows.Forms.DataGrid();
      this.dataGrid1 = new System.Windows.Forms.DataGrid();
      this.tabData = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGrid_datablocks = new System.Windows.Forms.DataGrid();
      this.DataBlocksTableStyle = new System.Windows.Forms.DataGridTableStyle();
      this.DataBlocks_NameColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.DataBlocks_StationIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.DataBlocks_AddressColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.DataBlocks_GroupIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.DataBlocks_DataTypeColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGrid_Groups = new System.Windows.Forms.DataGrid();
      this.GroupsTableStyle = new System.Windows.Forms.DataGridTableStyle();
      this.Groups_NameColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Groups_StationIDColumn = new NetworkConfig.HMI.AdvancedFormNetworkConfig.DataGridComboBoxColumn();
      this.Groups_GroupIDColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Groups_TimeScanColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Groups_TimeOutColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Groups_TimeScanFastColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.Groups_TimeOutFastColumn = new System.Windows.Forms.DataGridTextBoxColumn();
      this.tabData2 = new System.Windows.Forms.TabPage();
      this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGrid_tagbits = new System.Windows.Forms.DataGrid();
      this.dataGrid_tags = new System.Windows.Forms.DataGrid();
      this.openFileDialogXMLFile = new System.Windows.Forms.OpenFileDialog();
      this.saveXMLFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.eventLog1 = new System.Diagnostics.EventLog();
      this.mainMenu1 = new System.Windows.Forms.MainMenu( this.components );
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuFile_Open = new System.Windows.Forms.MenuItem();
      this.menuFile_Clear_All = new System.Windows.Forms.MenuItem();
      this.menuFile_Save = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.menuFile_exit = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.menuItem6 = new System.Windows.Forms.MenuItem();
      this.menuItem4 = new System.Windows.Forms.MenuItem();
      this.menuItemSBUS_bls = new System.Windows.Forms.MenuItem();
      this.menuItemTagblosks_csv = new System.Windows.Forms.MenuItem();
      this.menuItemTagBit_csv = new System.Windows.Forms.MenuItem();
      this.menuItemTagMappings_csv = new System.Windows.Forms.MenuItem();
      this.menuItemScanSettings_txt = new System.Windows.Forms.MenuItem();
      this.menuItem10 = new System.Windows.Forms.MenuItem();
      this.menuItemMODBUS_bls = new System.Windows.Forms.MenuItem();
      this.menuItemTags_for_Simulation = new System.Windows.Forms.MenuItem();
      this.protocolsBindingSource = new System.Windows.Forms.BindingSource( this.components );
      this.protocolBindingSource = new System.Windows.Forms.BindingSource( this.components );
      this.comunicationNetBindingSource = new System.Windows.Forms.BindingSource( this.components );
      this.dataBlocksBindingSource = new System.Windows.Forms.BindingSource( this.components );
      this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
      this.TabNetworkStructure.SuspendLayout();
      this.tabPageProtocol.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGridSerial ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGridProtocol ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGridChannels ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.channelsBindingSource ) ).BeginInit();
      this.tabPageCommStruc.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid3 ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid2 ) ).BeginInit();
      this.tableLayoutPanel4.SuspendLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid8 ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid1 ) ).BeginInit();
      this.tabData.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_datablocks ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_Groups ) ).BeginInit();
      this.tabData2.SuspendLayout();
      this.tableLayoutPanel5.SuspendLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_tagbits ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_tags ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.eventLog1 ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.protocolsBindingSource ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.protocolBindingSource ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.comunicationNetBindingSource ) ).BeginInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataBlocksBindingSource ) ).BeginInit();
      this.SuspendLayout();
      // 
      // TabNetworkStructure
      // 
      this.TabNetworkStructure.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.TabNetworkStructure.Controls.Add( this.tabPageProtocol );
      this.TabNetworkStructure.Controls.Add( this.tabPageCommStruc );
      this.TabNetworkStructure.Controls.Add( this.tabData );
      this.TabNetworkStructure.Controls.Add( this.tabData2 );
      this.TabNetworkStructure.Location = new System.Drawing.Point( 12, 8 );
      this.TabNetworkStructure.Name = "TabNetworkStructure";
      this.TabNetworkStructure.SelectedIndex = 0;
      this.TabNetworkStructure.ShowToolTips = true;
      this.TabNetworkStructure.Size = new System.Drawing.Size( 903, 253 );
      this.TabNetworkStructure.TabIndex = 8;
      // 
      // tabPageProtocol
      // 
      this.tabPageProtocol.Controls.Add( this.tableLayoutPanel1 );
      this.tabPageProtocol.Location = new System.Drawing.Point( 4, 22 );
      this.tabPageProtocol.Name = "tabPageProtocol";
      this.tabPageProtocol.Size = new System.Drawing.Size( 895, 227 );
      this.tabPageProtocol.TabIndex = 2;
      this.tabPageProtocol.Text = "Protocols";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
      this.tableLayoutPanel1.Controls.Add( this.dataGridSerial, 0, 2 );
      this.tableLayoutPanel1.Controls.Add( this.dataGridProtocol, 0, 1 );
      this.tableLayoutPanel1.Controls.Add( this.dataGridChannels, 0, 0 );
      this.tableLayoutPanel1.Location = new System.Drawing.Point( 3, 3 );
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33F ) );
      this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33F ) );
      this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 34F ) );
      this.tableLayoutPanel1.Size = new System.Drawing.Size( 889, 221 );
      this.tableLayoutPanel1.TabIndex = 4;
      // 
      // dataGridSerial
      // 
      this.dataGridSerial.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGridSerial.CaptionText = "Serial ports";
      this.dataGridSerial.DataMember = "";
      this.dataGridSerial.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGridSerial.Location = new System.Drawing.Point( 3, 147 );
      this.dataGridSerial.Name = "dataGridSerial";
      this.dataGridSerial.Size = new System.Drawing.Size( 883, 71 );
      this.dataGridSerial.TabIndex = 1;
      this.dataGridSerial.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
            this.SerialportsTableSyle} );
      this.dataGridSerial.Navigate += new System.Windows.Forms.NavigateEventHandler( this.dataGrid10_Navigate );
      // 
      // SerialportsTableSyle
      // 
      this.SerialportsTableSyle.DataGrid = this.dataGridSerial;
      this.SerialportsTableSyle.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
            this.Serialports_SerialNumColumn,
            this.Serialports_ProtocolIDColumn,
            this.Serialports_BaudRateColumn,
            this.Serialports_ParityColumn,
            this.Serialports_DataBitsColumn,
            this.Serialports_StopBitsColumn,
            this.Serialports_TXFlowCTSColumn,
            this.Serialports_TXFlowDSRColumn,
            this.Serialports_TxFlowXColumn,
            this.Serialports_WhenRxColumn,
            this.Serialports_RxGateDSRColumn,
            this.Serialports_RxFlowXColumn,
            this.Serialports_UseRTSColumn,
            this.Serialports_UseDTRColumn,
            this.Serialports_XonCharColumn,
            this.Serialports_XoffCharColumn,
            this.Serialports_rxHighWaterColumn,
            this.Serialports_rxLowWaterColumn,
            this.Serialports_sendTimeoutMultiplier,
            this.Serialports_sendTimeoutConstantColumn,
            this.Serialports_rxQueueColumn,
            this.Serialports_txQueueColumn,
            this.Serialports_autoReopenColumn,
            this.Serialports_checkAllSendsColumn,
            this.Serialports_SerialTypeColumn} );
      this.SerialportsTableSyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.SerialportsTableSyle.MappingName = "SerialSetings";
      // 
      // Serialports_SerialNumColumn
      // 
      this.Serialports_SerialNumColumn.Format = "";
      this.Serialports_SerialNumColumn.FormatInfo = null;
      this.Serialports_SerialNumColumn.HeaderText = "SerialNum";
      this.Serialports_SerialNumColumn.MappingName = "SerialNum";
      this.Serialports_SerialNumColumn.Width = 75;
      // 
      // Serialports_ProtocolIDColumn
      // 
      this.Serialports_ProtocolIDColumn.Format = "";
      this.Serialports_ProtocolIDColumn.FormatInfo = null;
      this.Serialports_ProtocolIDColumn.Width = 75;
      // 
      // Serialports_BaudRateColumn
      // 
      this.Serialports_BaudRateColumn.Format = "";
      this.Serialports_BaudRateColumn.FormatInfo = null;
      this.Serialports_BaudRateColumn.HeaderText = "BaudRate";
      this.Serialports_BaudRateColumn.MappingName = "BaudRate";
      this.Serialports_BaudRateColumn.Width = 75;
      // 
      // Serialports_ParityColumn
      // 
      this.Serialports_ParityColumn.Format = "";
      this.Serialports_ParityColumn.FormatInfo = null;
      this.Serialports_ParityColumn.HeaderText = "Parity";
      this.Serialports_ParityColumn.MappingName = "Parity";
      this.Serialports_ParityColumn.Width = 75;
      // 
      // Serialports_DataBitsColumn
      // 
      this.Serialports_DataBitsColumn.Format = "";
      this.Serialports_DataBitsColumn.FormatInfo = null;
      this.Serialports_DataBitsColumn.HeaderText = "DataBits";
      this.Serialports_DataBitsColumn.MappingName = "DataBits";
      this.Serialports_DataBitsColumn.Width = 75;
      // 
      // Serialports_StopBitsColumn
      // 
      this.Serialports_StopBitsColumn.Format = "";
      this.Serialports_StopBitsColumn.FormatInfo = null;
      this.Serialports_StopBitsColumn.HeaderText = "StopBits";
      this.Serialports_StopBitsColumn.MappingName = "StopBits";
      this.Serialports_StopBitsColumn.Width = 75;
      // 
      // Serialports_TXFlowCTSColumn
      // 
      this.Serialports_TXFlowCTSColumn.HeaderText = "TxFlowCTS";
      this.Serialports_TXFlowCTSColumn.MappingName = "TxFlowCTS";
      this.Serialports_TXFlowCTSColumn.Width = 75;
      // 
      // Serialports_TXFlowDSRColumn
      // 
      this.Serialports_TXFlowDSRColumn.HeaderText = "TxFlowDSR";
      this.Serialports_TXFlowDSRColumn.MappingName = "TxFlowDSR";
      this.Serialports_TXFlowDSRColumn.Width = 75;
      // 
      // Serialports_TxFlowXColumn
      // 
      this.Serialports_TxFlowXColumn.HeaderText = "TxFlowX";
      this.Serialports_TxFlowXColumn.MappingName = "TxFlowX";
      this.Serialports_TxFlowXColumn.Width = 75;
      // 
      // Serialports_WhenRxColumn
      // 
      this.Serialports_WhenRxColumn.HeaderText = "TxWhenRxXoff";
      this.Serialports_WhenRxColumn.MappingName = "TxWhenRxXoff";
      this.Serialports_WhenRxColumn.Width = 75;
      // 
      // Serialports_RxGateDSRColumn
      // 
      this.Serialports_RxGateDSRColumn.HeaderText = "RxGateDSR";
      this.Serialports_RxGateDSRColumn.MappingName = "RxGateDSR";
      this.Serialports_RxGateDSRColumn.Width = 75;
      // 
      // Serialports_RxFlowXColumn
      // 
      this.Serialports_RxFlowXColumn.HeaderText = "RxFlowX";
      this.Serialports_RxFlowXColumn.MappingName = "RxFlowX";
      this.Serialports_RxFlowXColumn.Width = 75;
      // 
      // Serialports_UseRTSColumn
      // 
      this.Serialports_UseRTSColumn.Format = "";
      this.Serialports_UseRTSColumn.FormatInfo = null;
      this.Serialports_UseRTSColumn.HeaderText = "UseRTS";
      this.Serialports_UseRTSColumn.MappingName = "UseRTS";
      this.Serialports_UseRTSColumn.Width = 75;
      // 
      // Serialports_UseDTRColumn
      // 
      this.Serialports_UseDTRColumn.Format = "";
      this.Serialports_UseDTRColumn.FormatInfo = null;
      this.Serialports_UseDTRColumn.HeaderText = "UseDTR";
      this.Serialports_UseDTRColumn.MappingName = "UseDTR";
      this.Serialports_UseDTRColumn.Width = 75;
      // 
      // Serialports_XonCharColumn
      // 
      this.Serialports_XonCharColumn.Format = "";
      this.Serialports_XonCharColumn.FormatInfo = null;
      this.Serialports_XonCharColumn.HeaderText = "XonChar";
      this.Serialports_XonCharColumn.MappingName = "XonChar";
      this.Serialports_XonCharColumn.Width = 75;
      // 
      // Serialports_XoffCharColumn
      // 
      this.Serialports_XoffCharColumn.Format = "";
      this.Serialports_XoffCharColumn.FormatInfo = null;
      this.Serialports_XoffCharColumn.HeaderText = "XoffChar";
      this.Serialports_XoffCharColumn.MappingName = "XoffChar";
      this.Serialports_XoffCharColumn.Width = 75;
      // 
      // Serialports_rxHighWaterColumn
      // 
      this.Serialports_rxHighWaterColumn.Format = "";
      this.Serialports_rxHighWaterColumn.FormatInfo = null;
      this.Serialports_rxHighWaterColumn.HeaderText = "rxHighWater";
      this.Serialports_rxHighWaterColumn.MappingName = "rxHighWater";
      this.Serialports_rxHighWaterColumn.Width = 75;
      // 
      // Serialports_rxLowWaterColumn
      // 
      this.Serialports_rxLowWaterColumn.Format = "";
      this.Serialports_rxLowWaterColumn.FormatInfo = null;
      this.Serialports_rxLowWaterColumn.HeaderText = "rxLowWater";
      this.Serialports_rxLowWaterColumn.MappingName = "rxLowWater";
      this.Serialports_rxLowWaterColumn.Width = 75;
      // 
      // Serialports_sendTimeoutMultiplier
      // 
      this.Serialports_sendTimeoutMultiplier.Format = "";
      this.Serialports_sendTimeoutMultiplier.FormatInfo = null;
      this.Serialports_sendTimeoutMultiplier.HeaderText = "sendTimeoutMultiplier";
      this.Serialports_sendTimeoutMultiplier.MappingName = "sendTimeoutMultiplier";
      this.Serialports_sendTimeoutMultiplier.Width = 75;
      // 
      // Serialports_sendTimeoutConstantColumn
      // 
      this.Serialports_sendTimeoutConstantColumn.Format = "";
      this.Serialports_sendTimeoutConstantColumn.FormatInfo = null;
      this.Serialports_sendTimeoutConstantColumn.HeaderText = "sendTimeoutConstant";
      this.Serialports_sendTimeoutConstantColumn.MappingName = "sendTimeoutConstant";
      this.Serialports_sendTimeoutConstantColumn.Width = 75;
      // 
      // Serialports_rxQueueColumn
      // 
      this.Serialports_rxQueueColumn.Format = "";
      this.Serialports_rxQueueColumn.FormatInfo = null;
      this.Serialports_rxQueueColumn.HeaderText = "rxQueue";
      this.Serialports_rxQueueColumn.MappingName = "rxQueue";
      this.Serialports_rxQueueColumn.Width = 75;
      // 
      // Serialports_txQueueColumn
      // 
      this.Serialports_txQueueColumn.Format = "";
      this.Serialports_txQueueColumn.FormatInfo = null;
      this.Serialports_txQueueColumn.HeaderText = "txQueue";
      this.Serialports_txQueueColumn.MappingName = "txQueue";
      this.Serialports_txQueueColumn.Width = 75;
      // 
      // Serialports_autoReopenColumn
      // 
      this.Serialports_autoReopenColumn.HeaderText = "autoReopen";
      this.Serialports_autoReopenColumn.MappingName = "autoReopen";
      this.Serialports_autoReopenColumn.Width = 75;
      // 
      // Serialports_checkAllSendsColumn
      // 
      this.Serialports_checkAllSendsColumn.HeaderText = "checkAllSends";
      this.Serialports_checkAllSendsColumn.MappingName = "checkAllSends";
      this.Serialports_checkAllSendsColumn.Width = 75;
      // 
      // Serialports_SerialTypeColumn
      // 
      this.Serialports_SerialTypeColumn.Format = "";
      this.Serialports_SerialTypeColumn.FormatInfo = null;
      this.Serialports_SerialTypeColumn.HeaderText = "SerialType";
      this.Serialports_SerialTypeColumn.MappingName = "SerialType";
      this.Serialports_SerialTypeColumn.Width = 75;
      // 
      // dataGridProtocol
      // 
      this.dataGridProtocol.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGridProtocol.CaptionText = "Protocols";
      this.dataGridProtocol.DataMember = "";
      this.dataGridProtocol.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGridProtocol.Location = new System.Drawing.Point( 3, 75 );
      this.dataGridProtocol.Name = "dataGridProtocol";
      this.dataGridProtocol.Size = new System.Drawing.Size( 883, 66 );
      this.dataGridProtocol.TabIndex = 0;
      this.dataGridProtocol.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
            this.ProtocolsTableStyle} );
      // 
      // ProtocolsTableStyle
      // 
      this.ProtocolsTableStyle.DataGrid = this.dataGridProtocol;
      this.ProtocolsTableStyle.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
            this.Protocols_ProtocolIDColumn,
            this.Protocols_NameColumn,
            this.Protocols_ChannelIDColumn,
            this.Protocols_ResponseTimeOutColumn,
            this.Protocols_FrameTimeOutColumn,
            this.Protocols_CharacterTimeOutColumn,
            this.Protocols_InterframeGapColumn,
            this.Protocols_MaxNumberColumn,
            this.Protocols_ProtocolTypeColumn} );
      this.ProtocolsTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      // 
      // Protocols_ProtocolIDColumn
      // 
      this.Protocols_ProtocolIDColumn.Format = "";
      this.Protocols_ProtocolIDColumn.FormatInfo = null;
      this.Protocols_ProtocolIDColumn.HeaderText = "ProtocolID";
      this.Protocols_ProtocolIDColumn.MappingName = "ProtocolID";
      this.Protocols_ProtocolIDColumn.Width = 75;
      // 
      // Protocols_NameColumn
      // 
      this.Protocols_NameColumn.Format = "";
      this.Protocols_NameColumn.FormatInfo = null;
      this.Protocols_NameColumn.HeaderText = "Name";
      this.Protocols_NameColumn.MappingName = "Name";
      this.Protocols_NameColumn.Width = 75;
      // 
      // Protocols_ChannelIDColumn
      // 
      this.Protocols_ChannelIDColumn.Format = "";
      this.Protocols_ChannelIDColumn.FormatInfo = null;
      this.Protocols_ChannelIDColumn.MappingName = "ChannelID";
      this.Protocols_ChannelIDColumn.Width = 75;
      // 
      // Protocols_ResponseTimeOutColumn
      // 
      this.Protocols_ResponseTimeOutColumn.Format = "";
      this.Protocols_ResponseTimeOutColumn.FormatInfo = null;
      this.Protocols_ResponseTimeOutColumn.HeaderText = "ResponseTimeOut";
      this.Protocols_ResponseTimeOutColumn.MappingName = "ResponseTimeOut";
      this.Protocols_ResponseTimeOutColumn.Width = 110;
      // 
      // Protocols_FrameTimeOutColumn
      // 
      this.Protocols_FrameTimeOutColumn.Format = "";
      this.Protocols_FrameTimeOutColumn.FormatInfo = null;
      this.Protocols_FrameTimeOutColumn.HeaderText = "FrameTimeOut";
      this.Protocols_FrameTimeOutColumn.MappingName = "FrameTimeOut";
      this.Protocols_FrameTimeOutColumn.Width = 110;
      // 
      // Protocols_CharacterTimeOutColumn
      // 
      this.Protocols_CharacterTimeOutColumn.Format = "";
      this.Protocols_CharacterTimeOutColumn.FormatInfo = null;
      this.Protocols_CharacterTimeOutColumn.HeaderText = "CharacterTimeOut";
      this.Protocols_CharacterTimeOutColumn.MappingName = "CharacterTimeOut";
      this.Protocols_CharacterTimeOutColumn.Width = 110;
      // 
      // Protocols_InterframeGapColumn
      // 
      this.Protocols_InterframeGapColumn.Format = "";
      this.Protocols_InterframeGapColumn.FormatInfo = null;
      this.Protocols_InterframeGapColumn.HeaderText = "InterframeGap";
      this.Protocols_InterframeGapColumn.MappingName = "InterfarameGap";
      this.Protocols_InterframeGapColumn.Width = 110;
      // 
      // Protocols_MaxNumberColumn
      // 
      this.Protocols_MaxNumberColumn.Format = "";
      this.Protocols_MaxNumberColumn.FormatInfo = null;
      this.Protocols_MaxNumberColumn.HeaderText = "MaxNumberOfRetries";
      this.Protocols_MaxNumberColumn.MappingName = "MaxNumberOfRetries";
      this.Protocols_MaxNumberColumn.Width = 120;
      // 
      // Protocols_ProtocolTypeColumn
      // 
      this.Protocols_ProtocolTypeColumn.Format = "";
      this.Protocols_ProtocolTypeColumn.FormatInfo = null;
      this.Protocols_ProtocolTypeColumn.Width = 75;
      // 
      // dataGridChannels
      // 
      this.dataGridChannels.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGridChannels.CaptionText = "Channels";
      this.dataGridChannels.DataMember = "";
      this.dataGridChannels.DataSource = this.channelsBindingSource;
      this.dataGridChannels.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGridChannels.Location = new System.Drawing.Point( 3, 3 );
      this.dataGridChannels.Name = "dataGridChannels";
      this.dataGridChannels.Size = new System.Drawing.Size( 883, 66 );
      this.dataGridChannels.TabIndex = 2;
      // 
      // channelsBindingSource
      // 
      this.channelsBindingSource.DataMember = "Channels";
      this.channelsBindingSource.DataSource = typeof( CAS.NetworkConfigLib.ComunicationNet );
      // 
      // tabPageCommStruc
      // 
      this.tabPageCommStruc.Controls.Add( this.tableLayoutPanel3 );
      this.tabPageCommStruc.Location = new System.Drawing.Point( 4, 22 );
      this.tabPageCommStruc.Name = "tabPageCommStruc";
      this.tabPageCommStruc.Size = new System.Drawing.Size( 895, 227 );
      this.tabPageCommStruc.TabIndex = 0;
      this.tabPageCommStruc.Text = "Com structure";
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tableLayoutPanel3.ColumnCount = 1;
      this.tableLayoutPanel3.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
      this.tableLayoutPanel3.Controls.Add( this.dataGrid3, 0, 2 );
      this.tableLayoutPanel3.Controls.Add( this.dataGrid2, 0, 1 );
      this.tableLayoutPanel3.Controls.Add( this.tableLayoutPanel4, 0, 0 );
      this.tableLayoutPanel3.Location = new System.Drawing.Point( 3, 3 );
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 3;
      this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 34F ) );
      this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33F ) );
      this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33F ) );
      this.tableLayoutPanel3.Size = new System.Drawing.Size( 889, 221 );
      this.tableLayoutPanel3.TabIndex = 6;
      // 
      // dataGrid3
      // 
      this.dataGrid3.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid3.CaptionText = "Interfaces";
      this.dataGrid3.DataMember = "";
      this.dataGrid3.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGrid3.Location = new System.Drawing.Point( 3, 150 );
      this.dataGrid3.Name = "dataGrid3";
      this.dataGrid3.Size = new System.Drawing.Size( 883, 68 );
      this.dataGrid3.TabIndex = 3;
      this.dataGrid3.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
            this.InterfacesTableStyle} );
      // 
      // InterfacesTableStyle
      // 
      this.InterfacesTableStyle.DataGrid = this.dataGrid3;
      this.InterfacesTableStyle.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
            this.Interfaces_NameColumn,
            this.Interfaces_SegmentIDColumn,
            this.Interfaces_ChannelIDColumn,
            this.Interfaces_StationIDColumn,
            this.Interfaces_AddressColumn,
            this.Interfaces_InactTimeColumn,
            this.Interfaces_InactTimeAFailureColumn,
            this.Interfaces_InterfaceNumColumn} );
      this.InterfacesTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.InterfacesTableStyle.MappingName = "Interfaces";
      // 
      // Interfaces_NameColumn
      // 
      this.Interfaces_NameColumn.Format = "";
      this.Interfaces_NameColumn.FormatInfo = null;
      this.Interfaces_NameColumn.HeaderText = "Name";
      this.Interfaces_NameColumn.MappingName = "Name";
      this.Interfaces_NameColumn.Width = 75;
      // 
      // Interfaces_SegmentIDColumn
      // 
      this.Interfaces_SegmentIDColumn.Format = "";
      this.Interfaces_SegmentIDColumn.FormatInfo = null;
      this.Interfaces_SegmentIDColumn.Width = 75;
      // 
      // Interfaces_ChannelIDColumn
      // 
      this.Interfaces_ChannelIDColumn.Format = "";
      this.Interfaces_ChannelIDColumn.FormatInfo = null;
      this.Interfaces_ChannelIDColumn.Width = 75;
      // 
      // Interfaces_StationIDColumn
      // 
      this.Interfaces_StationIDColumn.Format = "";
      this.Interfaces_StationIDColumn.FormatInfo = null;
      this.Interfaces_StationIDColumn.Width = 75;
      // 
      // Interfaces_AddressColumn
      // 
      this.Interfaces_AddressColumn.Format = "";
      this.Interfaces_AddressColumn.FormatInfo = null;
      this.Interfaces_AddressColumn.HeaderText = "Address";
      this.Interfaces_AddressColumn.MappingName = "Address";
      this.Interfaces_AddressColumn.Width = 75;
      // 
      // Interfaces_InactTimeColumn
      // 
      this.Interfaces_InactTimeColumn.Format = "";
      this.Interfaces_InactTimeColumn.FormatInfo = null;
      this.Interfaces_InactTimeColumn.HeaderText = "InactTime";
      this.Interfaces_InactTimeColumn.MappingName = "InactTime";
      this.Interfaces_InactTimeColumn.Width = 75;
      // 
      // Interfaces_InactTimeAFailureColumn
      // 
      this.Interfaces_InactTimeAFailureColumn.Format = "";
      this.Interfaces_InactTimeAFailureColumn.FormatInfo = null;
      this.Interfaces_InactTimeAFailureColumn.HeaderText = "InactTimeAFailure";
      this.Interfaces_InactTimeAFailureColumn.MappingName = "InactTimeAFailure";
      this.Interfaces_InactTimeAFailureColumn.Width = 110;
      // 
      // Interfaces_InterfaceNumColumn
      // 
      this.Interfaces_InterfaceNumColumn.Format = "";
      this.Interfaces_InterfaceNumColumn.FormatInfo = null;
      this.Interfaces_InterfaceNumColumn.HeaderText = "InterfaceNum";
      this.Interfaces_InterfaceNumColumn.MappingName = "InterfaceNum";
      this.Interfaces_InterfaceNumColumn.Width = 75;
      // 
      // dataGrid2
      // 
      this.dataGrid2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid2.CaptionText = "Segments";
      this.dataGrid2.DataMember = "";
      this.dataGrid2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGrid2.Location = new System.Drawing.Point( 3, 78 );
      this.dataGrid2.Name = "dataGrid2";
      this.dataGrid2.Size = new System.Drawing.Size( 883, 66 );
      this.dataGrid2.TabIndex = 2;
      this.dataGrid2.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
            this.SegmentsTableStyle} );
      // 
      // SegmentsTableStyle
      // 
      this.SegmentsTableStyle.DataGrid = this.dataGrid2;
      this.SegmentsTableStyle.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
            this.Segments_NameColumn,
            this.Segments_SegmentIDColumn,
            this.Segments_ChannelIDColumn,
            this.Segments_ProtocolIDColumn,
            this.Segments_AddressColumn,
            this.Segments_TimeScanColumn,
            this.Segments_KeepConnectColumn,
            this.Segments_PickupConnColumn,
            this.Segments_timeKeepConnColumn,
            this.Segments_TimeReconnectColumn,
            this.Segments_TimeIdleKeepConn} );
      this.SegmentsTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.SegmentsTableStyle.MappingName = "Segments";
      // 
      // Segments_NameColumn
      // 
      this.Segments_NameColumn.Format = "";
      this.Segments_NameColumn.FormatInfo = null;
      this.Segments_NameColumn.HeaderText = "Name";
      this.Segments_NameColumn.MappingName = "Name";
      this.Segments_NameColumn.Width = 75;
      // 
      // Segments_SegmentIDColumn
      // 
      this.Segments_SegmentIDColumn.Format = "";
      this.Segments_SegmentIDColumn.FormatInfo = null;
      this.Segments_SegmentIDColumn.HeaderText = "SegmentID";
      this.Segments_SegmentIDColumn.MappingName = "SegmentID";
      this.Segments_SegmentIDColumn.Width = 75;
      // 
      // Segments_ChannelIDColumn
      // 
      this.Segments_ChannelIDColumn.Format = "";
      this.Segments_ChannelIDColumn.FormatInfo = null;
      this.Segments_ChannelIDColumn.Width = 75;
      // 
      // Segments_ProtocolIDColumn
      // 
      this.Segments_ProtocolIDColumn.Format = "";
      this.Segments_ProtocolIDColumn.FormatInfo = null;
      this.Segments_ProtocolIDColumn.Width = 75;
      // 
      // Segments_AddressColumn
      // 
      this.Segments_AddressColumn.Format = "";
      this.Segments_AddressColumn.FormatInfo = null;
      this.Segments_AddressColumn.HeaderText = "Address";
      this.Segments_AddressColumn.MappingName = "Address";
      this.Segments_AddressColumn.Width = 75;
      // 
      // Segments_TimeScanColumn
      // 
      this.Segments_TimeScanColumn.Format = "";
      this.Segments_TimeScanColumn.FormatInfo = null;
      this.Segments_TimeScanColumn.HeaderText = "TimeScan";
      this.Segments_TimeScanColumn.MappingName = "TimeScan";
      this.Segments_TimeScanColumn.Width = 75;
      // 
      // Segments_KeepConnectColumn
      // 
      this.Segments_KeepConnectColumn.Format = "";
      this.Segments_KeepConnectColumn.FormatInfo = null;
      this.Segments_KeepConnectColumn.HeaderText = "KeepConnect";
      this.Segments_KeepConnectColumn.MappingName = "KeepConnect";
      this.Segments_KeepConnectColumn.Width = 75;
      // 
      // Segments_PickupConnColumn
      // 
      this.Segments_PickupConnColumn.Format = "";
      this.Segments_PickupConnColumn.FormatInfo = null;
      this.Segments_PickupConnColumn.HeaderText = "PickupConn";
      this.Segments_PickupConnColumn.MappingName = "PickupConn";
      this.Segments_PickupConnColumn.Width = 75;
      // 
      // Segments_timeKeepConnColumn
      // 
      this.Segments_timeKeepConnColumn.Format = "";
      this.Segments_timeKeepConnColumn.FormatInfo = null;
      this.Segments_timeKeepConnColumn.HeaderText = "timeKeepConn";
      this.Segments_timeKeepConnColumn.MappingName = "timeKeepConn";
      this.Segments_timeKeepConnColumn.Width = 95;
      // 
      // Segments_TimeReconnectColumn
      // 
      this.Segments_TimeReconnectColumn.Format = "";
      this.Segments_TimeReconnectColumn.FormatInfo = null;
      this.Segments_TimeReconnectColumn.HeaderText = "TimeReconnect";
      this.Segments_TimeReconnectColumn.MappingName = "TimeReconnect";
      this.Segments_TimeReconnectColumn.Width = 75;
      // 
      // Segments_TimeIdleKeepConn
      // 
      this.Segments_TimeIdleKeepConn.Format = "";
      this.Segments_TimeIdleKeepConn.FormatInfo = null;
      this.Segments_TimeIdleKeepConn.HeaderText = "TimeIdleKeepConn";
      this.Segments_TimeIdleKeepConn.MappingName = "TimeIdleKeepConn";
      this.Segments_TimeIdleKeepConn.Width = 110;
      // 
      // tableLayoutPanel4
      // 
      this.tableLayoutPanel4.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tableLayoutPanel4.ColumnCount = 2;
      this.tableLayoutPanel4.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel4.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel4.Controls.Add( this.dataGrid8, 0, 0 );
      this.tableLayoutPanel4.Controls.Add( this.dataGrid1, 1, 0 );
      this.tableLayoutPanel4.Location = new System.Drawing.Point( 3, 3 );
      this.tableLayoutPanel4.Name = "tableLayoutPanel4";
      this.tableLayoutPanel4.RowCount = 1;
      this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel4.Size = new System.Drawing.Size( 883, 69 );
      this.tableLayoutPanel4.TabIndex = 4;
      // 
      // dataGrid8
      // 
      this.dataGrid8.AlternatingBackColor = System.Drawing.Color.Lavender;
      this.dataGrid8.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.dataGrid8.BackgroundColor = System.Drawing.Color.LightGray;
      this.dataGrid8.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dataGrid8.CaptionBackColor = System.Drawing.Color.LightSteelBlue;
      this.dataGrid8.CaptionFont = new System.Drawing.Font( "Microsoft Sans Serif", 8F );
      this.dataGrid8.CaptionForeColor = System.Drawing.Color.MidnightBlue;
      this.dataGrid8.CaptionText = "Channels";
      this.dataGrid8.DataMember = "";
      this.dataGrid8.FlatMode = true;
      this.dataGrid8.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8F );
      this.dataGrid8.ForeColor = System.Drawing.Color.MidnightBlue;
      this.dataGrid8.GridLineColor = System.Drawing.Color.Gainsboro;
      this.dataGrid8.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
      this.dataGrid8.HeaderBackColor = System.Drawing.Color.MidnightBlue;
      this.dataGrid8.HeaderFont = new System.Drawing.Font( "Microsoft Sans Serif", 8F );
      this.dataGrid8.HeaderForeColor = System.Drawing.Color.WhiteSmoke;
      this.dataGrid8.LinkColor = System.Drawing.Color.Teal;
      this.dataGrid8.Location = new System.Drawing.Point( 3, 3 );
      this.dataGrid8.Name = "dataGrid8";
      this.dataGrid8.ParentRowsBackColor = System.Drawing.Color.Gainsboro;
      this.dataGrid8.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
      this.dataGrid8.SelectionBackColor = System.Drawing.Color.CadetBlue;
      this.dataGrid8.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
      this.dataGrid8.Size = new System.Drawing.Size( 435, 63 );
      this.dataGrid8.TabIndex = 6;
      // 
      // dataGrid1
      // 
      this.dataGrid1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid1.CaptionText = "Stations";
      this.dataGrid1.DataMember = "";
      this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGrid1.Location = new System.Drawing.Point( 444, 3 );
      this.dataGrid1.Name = "dataGrid1";
      this.dataGrid1.Size = new System.Drawing.Size( 436, 63 );
      this.dataGrid1.TabIndex = 5;
      // 
      // tabData
      // 
      this.tabData.Controls.Add( this.tableLayoutPanel2 );
      this.tabData.Location = new System.Drawing.Point( 4, 22 );
      this.tabData.Name = "tabData";
      this.tabData.Size = new System.Drawing.Size( 895, 227 );
      this.tabData.TabIndex = 1;
      this.tabData.Text = "Data Groups/Blocks";
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
      this.tableLayoutPanel2.Controls.Add( this.dataGrid_datablocks, 0, 1 );
      this.tableLayoutPanel2.Controls.Add( this.dataGrid_Groups, 0, 0 );
      this.tableLayoutPanel2.Location = new System.Drawing.Point( 3, 4 );
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
      this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
      this.tableLayoutPanel2.Size = new System.Drawing.Size( 889, 220 );
      this.tableLayoutPanel2.TabIndex = 10;
      // 
      // dataGrid_datablocks
      // 
      this.dataGrid_datablocks.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid_datablocks.CaptionText = "Data bloks";
      this.dataGrid_datablocks.DataMember = "";
      this.dataGrid_datablocks.DataSource = this.dataBlocksBindingSource;
      this.dataGrid_datablocks.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGrid_datablocks.Location = new System.Drawing.Point( 3, 113 );
      this.dataGrid_datablocks.Name = "dataGrid_datablocks";
      this.dataGrid_datablocks.Size = new System.Drawing.Size( 883, 104 );
      this.dataGrid_datablocks.TabIndex = 7;
      this.dataGrid_datablocks.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
            this.DataBlocksTableStyle} );
      // 
      // DataBlocksTableStyle
      // 
      this.DataBlocksTableStyle.DataGrid = this.dataGrid_datablocks;
      this.DataBlocksTableStyle.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
            this.DataBlocks_NameColumn,
            this.DataBlocks_StationIDColumn,
            this.DataBlocks_AddressColumn,
            this.DataBlocks_GroupIDColumn,
            this.DataBlocks_DataTypeColumn,
            this.dataGridTextBoxColumn1} );
      this.DataBlocksTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.DataBlocksTableStyle.MappingName = "DataBlocks";
      // 
      // DataBlocks_NameColumn
      // 
      this.DataBlocks_NameColumn.Format = "";
      this.DataBlocks_NameColumn.FormatInfo = null;
      this.DataBlocks_NameColumn.HeaderText = "Name";
      this.DataBlocks_NameColumn.MappingName = "Name";
      this.DataBlocks_NameColumn.Width = 75;
      // 
      // DataBlocks_StationIDColumn
      // 
      this.DataBlocks_StationIDColumn.Format = "";
      this.DataBlocks_StationIDColumn.FormatInfo = null;
      this.DataBlocks_StationIDColumn.Width = 75;
      // 
      // DataBlocks_AddressColumn
      // 
      this.DataBlocks_AddressColumn.Format = "";
      this.DataBlocks_AddressColumn.FormatInfo = null;
      this.DataBlocks_AddressColumn.HeaderText = "Address";
      this.DataBlocks_AddressColumn.MappingName = "Address";
      this.DataBlocks_AddressColumn.Width = 75;
      // 
      // DataBlocks_GroupIDColumn
      // 
      this.DataBlocks_GroupIDColumn.Format = "";
      this.DataBlocks_GroupIDColumn.FormatInfo = null;
      this.DataBlocks_GroupIDColumn.Width = 75;
      // 
      // DataBlocks_DataTypeColumn
      // 
      this.DataBlocks_DataTypeColumn.Format = "";
      this.DataBlocks_DataTypeColumn.FormatInfo = null;
      this.DataBlocks_DataTypeColumn.HeaderText = "DataType";
      this.DataBlocks_DataTypeColumn.MappingName = "DataType";
      this.DataBlocks_DataTypeColumn.Width = 75;
      // 
      // dataGrid_Groups
      // 
      this.dataGrid_Groups.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid_Groups.CaptionText = "Tag groups";
      this.dataGrid_Groups.DataMember = "";
      this.dataGrid_Groups.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGrid_Groups.Location = new System.Drawing.Point( 3, 3 );
      this.dataGrid_Groups.Name = "dataGrid_Groups";
      this.dataGrid_Groups.Size = new System.Drawing.Size( 883, 104 );
      this.dataGrid_Groups.TabIndex = 5;
      this.dataGrid_Groups.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
            this.GroupsTableStyle} );
      // 
      // GroupsTableStyle
      // 
      this.GroupsTableStyle.DataGrid = this.dataGrid_Groups;
      this.GroupsTableStyle.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
            this.Groups_NameColumn,
            this.Groups_StationIDColumn,
            this.Groups_GroupIDColumn,
            this.Groups_TimeScanColumn,
            this.Groups_TimeOutColumn,
            this.Groups_TimeScanFastColumn,
            this.Groups_TimeOutFastColumn} );
      this.GroupsTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.GroupsTableStyle.MappingName = "Groups";
      // 
      // Groups_NameColumn
      // 
      this.Groups_NameColumn.Format = "";
      this.Groups_NameColumn.FormatInfo = null;
      this.Groups_NameColumn.HeaderText = "Name";
      this.Groups_NameColumn.MappingName = "Name";
      this.Groups_NameColumn.Width = 75;
      // 
      // Groups_StationIDColumn
      // 
      this.Groups_StationIDColumn.Format = "";
      this.Groups_StationIDColumn.FormatInfo = null;
      this.Groups_StationIDColumn.Width = 75;
      // 
      // Groups_GroupIDColumn
      // 
      this.Groups_GroupIDColumn.Format = "";
      this.Groups_GroupIDColumn.FormatInfo = null;
      this.Groups_GroupIDColumn.HeaderText = "GroupID";
      this.Groups_GroupIDColumn.MappingName = "GroupID";
      this.Groups_GroupIDColumn.Width = 75;
      // 
      // Groups_TimeScanColumn
      // 
      this.Groups_TimeScanColumn.Format = "";
      this.Groups_TimeScanColumn.FormatInfo = null;
      this.Groups_TimeScanColumn.HeaderText = "TimeScan";
      this.Groups_TimeScanColumn.MappingName = "TimeScan";
      this.Groups_TimeScanColumn.Width = 75;
      // 
      // Groups_TimeOutColumn
      // 
      this.Groups_TimeOutColumn.Format = "";
      this.Groups_TimeOutColumn.FormatInfo = null;
      this.Groups_TimeOutColumn.HeaderText = "TimeOut";
      this.Groups_TimeOutColumn.MappingName = "TimeOut";
      this.Groups_TimeOutColumn.Width = 70;
      // 
      // Groups_TimeScanFastColumn
      // 
      this.Groups_TimeScanFastColumn.Format = "";
      this.Groups_TimeScanFastColumn.FormatInfo = null;
      this.Groups_TimeScanFastColumn.HeaderText = "TimeScanFast";
      this.Groups_TimeScanFastColumn.MappingName = "TimeScanFast";
      this.Groups_TimeScanFastColumn.Width = 90;
      // 
      // Groups_TimeOutFastColumn
      // 
      this.Groups_TimeOutFastColumn.Format = "";
      this.Groups_TimeOutFastColumn.FormatInfo = null;
      this.Groups_TimeOutFastColumn.HeaderText = "TimeOutFast";
      this.Groups_TimeOutFastColumn.MappingName = "TimeOutFast";
      this.Groups_TimeOutFastColumn.Width = 75;
      // 
      // tabData2
      // 
      this.tabData2.Controls.Add( this.tableLayoutPanel5 );
      this.tabData2.Location = new System.Drawing.Point( 4, 22 );
      this.tabData2.Name = "tabData2";
      this.tabData2.Size = new System.Drawing.Size( 895, 227 );
      this.tabData2.TabIndex = 3;
      this.tabData2.Text = "DataTags";
      // 
      // tableLayoutPanel5
      // 
      this.tableLayoutPanel5.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tableLayoutPanel5.ColumnCount = 1;
      this.tableLayoutPanel5.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel5.Controls.Add( this.dataGrid_tagbits, 0, 1 );
      this.tableLayoutPanel5.Controls.Add( this.dataGrid_tags, 0, 0 );
      this.tableLayoutPanel5.Location = new System.Drawing.Point( 3, 3 );
      this.tableLayoutPanel5.Name = "tableLayoutPanel5";
      this.tableLayoutPanel5.RowCount = 2;
      this.tableLayoutPanel5.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel5.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
      this.tableLayoutPanel5.Size = new System.Drawing.Size( 889, 221 );
      this.tableLayoutPanel5.TabIndex = 11;
      // 
      // dataGrid_tagbits
      // 
      this.dataGrid_tagbits.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid_tagbits.CaptionText = "Bits definition";
      this.dataGrid_tagbits.DataMember = "";
      this.dataGrid_tagbits.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGrid_tagbits.Location = new System.Drawing.Point( 3, 113 );
      this.dataGrid_tagbits.Name = "dataGrid_tagbits";
      this.dataGrid_tagbits.Size = new System.Drawing.Size( 883, 105 );
      this.dataGrid_tagbits.TabIndex = 12;
      // 
      // dataGrid_tags
      // 
      this.dataGrid_tags.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dataGrid_tags.CaptionText = "Tags definition";
      this.dataGrid_tags.DataMember = "";
      this.dataGrid_tags.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dataGrid_tags.Location = new System.Drawing.Point( 3, 3 );
      this.dataGrid_tags.Name = "dataGrid_tags";
      this.dataGrid_tags.Size = new System.Drawing.Size( 883, 104 );
      this.dataGrid_tags.TabIndex = 11;
      // 
      // openFileDialogXMLFile
      // 
      this.openFileDialogXMLFile.Filter = "XML files (*.XML)|*.XML";
      this.openFileDialogXMLFile.Title = "XML source file";
      // 
      // saveXMLFileDialog
      // 
      this.saveXMLFileDialog.DefaultExt = "XML";
      this.saveXMLFileDialog.Filter = "XML files (*.XML)|*.XML";
      this.saveXMLFileDialog.Title = "Save as XML file";
      // 
      // eventLog1
      // 
      this.eventLog1.Log = "Application";
      this.eventLog1.MachineName = "cas_002";
      this.eventLog1.SynchronizingObject = this;
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menuFile,
            this.menuItem3,
            this.menuItem4} );
      // 
      // menuFile
      // 
      this.menuFile.Index = 0;
      this.menuFile.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menuFile_Open,
            this.menuFile_Clear_All,
            this.menuFile_Save,
            this.menuItem1,
            this.menuFile_exit} );
      this.menuFile.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
      this.menuFile.Text = "File";
      // 
      // menuFile_Open
      // 
      this.menuFile_Open.Index = 0;
      this.menuFile_Open.Text = "Open";
      this.menuFile_Open.Click += new System.EventHandler( this.ReadXML_Click );
      // 
      // menuFile_Clear_All
      // 
      this.menuFile_Clear_All.Index = 1;
      this.menuFile_Clear_All.Text = "Clear_All";
      this.menuFile_Clear_All.Click += new System.EventHandler( this.menuFile_Clear_All_Click );
      // 
      // menuFile_Save
      // 
      this.menuFile_Save.Index = 2;
      this.menuFile_Save.Text = "Save";
      this.menuFile_Save.Click += new System.EventHandler( this.Button_SaveXML_Click );
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 3;
      this.menuItem1.Text = "-";
      // 
      // menuFile_exit
      // 
      this.menuFile_exit.Index = 4;
      this.menuFile_exit.Text = "Exit";
      this.menuFile_exit.Click += new System.EventHandler( this.menuItem_exit_Click );
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 1;
      this.menuItem3.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menuItem6} );
      this.menuItem3.Text = "Edit";
      // 
      // menuItem6
      // 
      this.menuItem6.Index = 0;
      this.menuItem6.Text = "Clear config";
      this.menuItem6.Click += new System.EventHandler( this.clearButton_Click );
      // 
      // menuItem4
      // 
      this.menuItem4.Index = 2;
      this.menuItem4.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menuItemSBUS_bls,
            this.menuItemTagblosks_csv,
            this.menuItemTagBit_csv,
            this.menuItemTagMappings_csv,
            this.menuItemScanSettings_txt,
            this.menuItem10,
            this.menuItemMODBUS_bls,
            this.menuItemTags_for_Simulation} );
      this.menuItem4.Text = "Import";
      // 
      // menuItemSBUS_bls
      // 
      this.menuItemSBUS_bls.Index = 0;
      this.menuItemSBUS_bls.Text = "SBUS.bls";
      this.menuItemSBUS_bls.Click += new System.EventHandler( this.menuItemSBUS_bls_Click );
      // 
      // menuItemTagblosks_csv
      // 
      this.menuItemTagblosks_csv.Index = 1;
      this.menuItemTagblosks_csv.Text = "Tagblosks.csv";
      this.menuItemTagblosks_csv.Click += new System.EventHandler( this.menuItemTagblosks_csv_Click );
      // 
      // menuItemTagBit_csv
      // 
      this.menuItemTagBit_csv.Index = 2;
      this.menuItemTagBit_csv.Text = "TagBit.csv";
      this.menuItemTagBit_csv.Click += new System.EventHandler( this.menuItemTagBit_csv_Click );
      // 
      // menuItemTagMappings_csv
      // 
      this.menuItemTagMappings_csv.Index = 3;
      this.menuItemTagMappings_csv.Text = "TagMappings.csv";
      this.menuItemTagMappings_csv.Click += new System.EventHandler( this.menuItemTagMappings_csv_Click );
      // 
      // menuItemScanSettings_txt
      // 
      this.menuItemScanSettings_txt.Index = 4;
      this.menuItemScanSettings_txt.Text = " ScanSettings.txt";
      this.menuItemScanSettings_txt.Click += new System.EventHandler( this.menuItemScanSettings_txt_Click );
      // 
      // menuItem10
      // 
      this.menuItem10.Index = 5;
      this.menuItem10.Text = "-";
      // 
      // menuItemMODBUS_bls
      // 
      this.menuItemMODBUS_bls.Index = 6;
      this.menuItemMODBUS_bls.Text = "MODBUS.bls";
      this.menuItemMODBUS_bls.Click += new System.EventHandler( this.menuItemMODBUS_bls_Click );
      // 
      // menuItemTags_for_Simulation
      // 
      this.menuItemTags_for_Simulation.Index = 7;
      this.menuItemTags_for_Simulation.Text = "Tags for Simulation";
      this.menuItemTags_for_Simulation.Click += new System.EventHandler( this.menuItemTags_for_Simulation_Click );
      // 
      // protocolBindingSource
      // 
      this.protocolBindingSource.DataMember = "Protocol";
      // 
      // comunicationNetBindingSource
      // 
      this.comunicationNetBindingSource.DataSource = typeof( CAS.NetworkConfigLib.ComunicationNet );
      this.comunicationNetBindingSource.Position = 0;
      // 
      // dataBlocksBindingSource
      // 
      this.dataBlocksBindingSource.DataMember = "DataBlocks";
      this.dataBlocksBindingSource.DataSource = this.comunicationNetBindingSource;
      // 
      // dataGridTextBoxColumn1
      // 
      this.dataGridTextBoxColumn1.Format = "";
      this.dataGridTextBoxColumn1.FormatInfo = null;
      this.dataGridTextBoxColumn1.HeaderText = "DatBlockID";
      this.dataGridTextBoxColumn1.MappingName = "DatBlockID";
      this.dataGridTextBoxColumn1.Width = 75;
      // 
      // AdvancedFormNetworkConfig
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.ClientSize = new System.Drawing.Size( 927, 273 );
      this.Controls.Add( this.TabNetworkStructure );
      this.Menu = this.mainMenu1;
      this.MinimumSize = new System.Drawing.Size( 700, 60 );
      this.Name = "AdvancedFormNetworkConfig";
      this.Text = "Network configuration";
      this.TabNetworkStructure.ResumeLayout( false );
      this.tabPageProtocol.ResumeLayout( false );
      this.tableLayoutPanel1.ResumeLayout( false );
      ( (System.ComponentModel.ISupportInitialize)( this.dataGridSerial ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGridProtocol ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGridChannels ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.channelsBindingSource ) ).EndInit();
      this.tabPageCommStruc.ResumeLayout( false );
      this.tableLayoutPanel3.ResumeLayout( false );
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid3 ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid2 ) ).EndInit();
      this.tableLayoutPanel4.ResumeLayout( false );
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid8 ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid1 ) ).EndInit();
      this.tabData.ResumeLayout( false );
      this.tableLayoutPanel2.ResumeLayout( false );
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_datablocks ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_Groups ) ).EndInit();
      this.tabData2.ResumeLayout( false );
      this.tableLayoutPanel5.ResumeLayout( false );
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_tagbits ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataGrid_tags ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.eventLog1 ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.protocolsBindingSource ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.protocolBindingSource ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.comunicationNetBindingSource ) ).EndInit();
      ( (System.ComponentModel.ISupportInitialize)( this.dataBlocksBindingSource ) ).EndInit();
      this.ResumeLayout( false );

    }
    private void InitConfigDataBase( bool CreateNewconfigDatabase )
    {
      // 
      // configDataBase
      // 
      if ( CreateNewconfigDatabase )
        this.configDataBase = new ComunicationNet();
      ( (System.ComponentModel.ISupportInitialize)( this.configDataBase ) ).BeginInit();
      this.configDataBase.DataSetName = "ComunicationNet";
      this.configDataBase.Locale = new System.Globalization.CultureInfo( "en-US" );
      this.configDataBase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
      this.dataGridChannels.DataSource = this.configDataBase.Channels;
      this.dataGridSerial.DataSource = this.configDataBase.SerialSetings;
      this.dataGridProtocol.DataSource = this.configDataBase.Protocol;
      this.dataGrid1.DataSource = this.configDataBase.Station;
      this.dataGrid3.DataSource = this.configDataBase.Interfaces;
      this.dataGrid2.DataSource = this.configDataBase.Segments;
      this.dataGrid8.DataSource = this.configDataBase.Channels;
      this.dataGrid_tagbits.DataSource = this.configDataBase.TagBit;
      this.dataGrid_tags.DataSource = this.configDataBase.Tags;
      this.dataGrid_datablocks.DataSource = this.configDataBase.DataBlocks;
      this.dataGrid_Groups.DataSource = this.configDataBase.Groups;
      this.protocolBindingSource.DataSource = this.configDataBase;
      this.channelsBindingSource.DataSource = this.configDataBase;
      ( (System.ComponentModel.ISupportInitialize)( this.configDataBase ) ).EndInit();

    }
    #endregion
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      AdvancedFormNetworkConfig myForm = new AdvancedFormNetworkConfig();
      //XMLManagement myConfig = new XMLManagement();
      //myForm.ReadXML.Enabled = true;
      Application.Run( myForm );
      //myConfig.writeXMLFile(myForm.comunicationNet1);
    }
    private void dataGrid10_Navigate( object sender, System.Windows.Forms.NavigateEventArgs ne )
    {

    }
    //private void buttonStationTree_Click(object sender, System.EventArgs e)
    //{
    //    //new HMI.ConfigTreeView(configDataBase).ShowDialog(this);
    //}
    private void clearButton_Click( object sender, System.EventArgs e )
    {
      if ( MessageBox.Show( this, "Clear all datagrids???", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
      {
        fileclear( this );
        //configDataBase.SerialSetings.Clear();
        //configDataBase.Interfaces.Clear();
        //configDataBase.Tags.Clear();
        //configDataBase.TagBit.Clear();
        //configDataBase.DataBlocks.Clear();
        //configDataBase.Groups.Clear();
        //configDataBase.Segments.Clear();
        //configDataBase.Station.Clear();
        //configDataBase.Protocol.Clear();
        //configDataBase.Channels.Clear();
      }
    }
    private void button_exit_Click( object sender, System.EventArgs e )
    {
      this.Close();
    }
    private void menuItem_exit_Click( object sender, System.EventArgs e )
    {
      ConfigurationManagement.SaveProc( this );
      this.Close();
    }
    private void button1_Click( object sender, System.EventArgs e )
    {
      ImportFunctionRootClass importer = new NetworkConfig.HMI.Import.ImportTagsForSimulation( configDataBase, this );
      importer.Import();
    }
    private void menuFile_Clear_All_Click( object sender, EventArgs e )
    {
      if ( MessageBox.Show( this, "Clear all datagrids???", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
      {
        fileclear( this );
      }
    }
    private void menuItemSBUS_bls_Click( object sender, EventArgs e )
    {
      new NetworkConfig.HMI.Import.ImportBLS( configDataBase, this ).Import();
    }
    private void menuItemTagblosks_csv_Click( object sender, EventArgs e )
    {
      new NetworkConfig.HMI.Import.ImportBlockCSV( configDataBase, this ).Import();
    }
    private void menuItemTagBit_csv_Click( object sender, EventArgs e )
    {
      new NetworkConfig.HMI.Import.ImportTagBits( configDataBase, this ).Import();
    }
    private void menuItemTagMappings_csv_Click( object sender, EventArgs e )
    {
      new NetworkConfig.HMI.Import.ImportTagMappings( configDataBase, this ).Import();
    }
    private void menuItemScanSettings_txt_Click( object sender, EventArgs e )
    {
      new NetworkConfig.HMI.Import.ImportScanSettings( configDataBase, this ).Import();
    }
    private void menuItemMODBUS_bls_Click( object sender, EventArgs e )
    {
      new NetworkConfig.HMI.Import.ImportBLS( configDataBase, this ).Import();
    }
    private void menuItemTags_for_Simulation_Click( object sender, EventArgs e )
    {
      new NetworkConfig.HMI.Import.ImportTagsForSimulation( configDataBase, this ).Import();
    }

    private BindingSource dataBlocksBindingSource;
    private BindingSource comunicationNetBindingSource;
    private DataGridTextBoxColumn dataGridTextBoxColumn1;
  } //Form1
}
