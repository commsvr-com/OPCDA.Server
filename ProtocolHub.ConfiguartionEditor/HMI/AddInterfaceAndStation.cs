//<summary>
//  Title   : Adding new Station and Interface couple.
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20080905: mzbrzezny: this object is responsible for automatically assign interface number
//    TSiwecki Created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.CommServer.ProtocolHub.Communication.BaseStation;
using CAS.Lib.ControlLibrary;
using CAS.NetworkConfigLib;
using System;
using System.Data;
using System.Windows.Forms;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Control allowing to add new Interface and station couple.
  /// </summary>
  internal partial class AddInterfaceAndStation: Form
  {
    
    #region private
    private ComunicationNet.InterfacesDataTable m_InterfacesDataTable;
    private ComunicationNet.StationDataTable StationTab
    {
      set
      {
        foreach ( ComunicationNet.StationRow sr in value )
        {
          if ( sr.RowState == DataRowState.Deleted )
            break;
          if ( sr.GetInterfacesRows().Length < 2 )
            cmbStations.Items.Add( new StationRowWrapper( sr, null ) );
        }
      }
    }
    #endregion

    #region Constructor
    public AddInterfaceAndStation
      ( ComunicationNet pDB, IAction pNewInteface )
    {
      InitializeComponent();
      m_InterfacesDataTable = pDB.Interfaces;
      StationTab = pDB.Station;
      pgInterface.SelectedObject = pNewInteface;
    }
    #endregion

    #region Event handlers
    private void btnAdd_Click( object sender, EventArgs e )
    {
      try
      {
        //musimy sprawdzic czy para - interface num, station juz jest w tabeli
        InterfacesRowWrapper currentInterfacesRowWrapper = (InterfacesRowWrapper)pgInterface.SelectedObject as InterfacesRowWrapper;
        if ( this.cmbStations.SelectedItem == null )
          throw new Exception( Properties.Resources.tx_AddInterfaceAndStation_pleaseselectstationfirst );
        if ( currentInterfacesRowWrapper.StationId < 0 )
          try
          {
            currentInterfacesRowWrapper.StationId = ( (StationRowWrapper)( this.cmbStations.SelectedItem ) ).StationID;
          }
          catch ( Exception )
          {
            throw new Exception( Properties.Resources.tx_AddInterfaceAndStation_pleaseselectstationfirst );
          }
        ulong InterfaceNum = 0;
        bool InterfaceNumIsValid = false;
        while ( !InterfaceNumIsValid )
        {
          InterfaceNumIsValid = true;
          foreach ( ComunicationNet.InterfacesRow interface_row in
            (ComunicationNet.InterfacesDataTable)( ( currentInterfacesRowWrapper ).DataRow.Table ) )
          {
            if ( interface_row.RowState != DataRowState.Detached
              && interface_row.RowState != DataRowState.Deleted
              && interface_row.StationId == currentInterfacesRowWrapper.StationId &&
              interface_row.InterfaceNum == InterfaceNum )
            {
              InterfaceNumIsValid = false;
              InterfaceNum++;
              break;
            }
          }
        }
        if ( InterfaceNum > Interface.Parameters.InterfaceNumberMaxValue )
          throw new Exception( "You cannot assign more ports to this station.\n\rThis port (interface) is not added." );
        currentInterfacesRowWrapper.InterfaceNum = InterfaceNum;
        //sprawdzmy teraz nazwe:
        foreach ( ComunicationNet.InterfacesRow interface_row in
          (ComunicationNet.InterfacesDataTable)( ( currentInterfacesRowWrapper ).DataRow.Table ) )
        {
          if ( interface_row.RowState != System.Data.DataRowState.Deleted &&
            interface_row.StationId == currentInterfacesRowWrapper.StationId &&
            interface_row.Name == currentInterfacesRowWrapper.Name )
          {
            currentInterfacesRowWrapper.Name += InterfaceNum.ToString();
            break;
          }
        }
        currentInterfacesRowWrapper.AddObjectToTable();
      }
      catch ( Exception ex )
      {
        string cMsg = "I cannot add new object to the configuration because: ";
        string cCAption = "Error while creating new object";
        MessageBox.Show( cMsg + ex.Message, cCAption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        return;
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    private void btnCancel_Click( object sender, EventArgs e )
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
    private void AddInterfaceAndStation_Load( object sender, EventArgs e ) { }
    private void btnNewSerial_Click( object sender, EventArgs e )
    {
      IAction currAct = (IAction)pgInterface.SelectedObject;
      IAction newAct = currAct.CreateNewChildObject();
      AddObject<IAction> fm_AddObj = new AddObject<IAction>();
      fm_AddObj.Text = "Add new station";
      fm_AddObj.Object = newAct;
      bool ok;
      do
      {
        ok = true;
        if ( fm_AddObj.ShowDialog( this ) != DialogResult.OK )
          return;
        try { newAct.AddObjectToTable(); }
        catch ( Exception ex )
        {
          MessageBox.Show
            ( "I cannot add new object to the configuration because: " + ex.Message, "Error while creating new object", MessageBoxButtons.OK, MessageBoxIcon.Error );
          ok = false;
        }
      }
      while ( !ok );
      cmbStations.Items.Add( (StationRowWrapper)newAct );
      cmbStations.SelectedItem = (StationRowWrapper)newAct;
    }
    private void cmbStations_SelectedIndexChanged( object sender, EventArgs e )
    {
      ( (InterfacesRowWrapper)pgInterface.SelectedObject ).StationId = ( (StationRowWrapper)( (ComboBox)sender ).SelectedItem ).StationID;
    }
    #endregion

  }
}