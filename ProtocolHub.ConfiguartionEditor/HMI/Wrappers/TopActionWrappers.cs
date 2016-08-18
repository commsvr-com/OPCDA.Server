//<summary>
//  Title   : ApplicTop layer wrappers
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081203: mzbrzezny: undoredomanager is switched off
//    MPstol: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>

using System.Data;
using System.Windows.Forms;
using CAS.NetworkConfigLib;

namespace NetworkConfig.HMI.Wrappers
{
  #region TopWrapper class
  internal abstract class TopWrapper<TRow>: Action<TRow> where TRow: DataRow
  {
    protected ComunicationNet m_dataset;
    public TopWrapper( ComunicationNet dataset )
      : base( null )
    {
      m_dataset = dataset;
    }
    public override void DeleteObject() { }
    public override bool CanBeDeleted { get { return false; } }
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved under this object
    /// </summary>
    /// <returns>True if specified object can be moved hare</returns>
    public override bool CanBeMoved() { return false; }
  }
  #endregion
  #region SA_TopWrapper class (Station Action TopWrapper)
  /// <summary>
  /// SA - Station Action
  /// </summary>
  internal class SA_TopWrapper: TopWrapper<ComunicationNet.StationRow>
  {
    private SA_TopWrapper( ComunicationNet dataset )
      : base( dataset )
    { }
    public override bool CanBeCopied { get { return false; } }
    public override IAction CreateNewChildObject()
    {
      ComunicationNet.StationDataTable stationTable = m_dataset.Station;
#if UNDOREDO
      RTLib.DataBase.UndoRedo.UndoRedoMenager.SuspendLogging();
#endif
      ComunicationNet.StationRow cr = stationTable.NewStationRow( "<root>" );
#if UNDOREDO
      RTLib.DataBase.UndoRedo.UndoRedoMenager.ResumeLogging();
#endif
      return new StationRowWrapper( cr, null );
    }
    public override string ToString() { return "Stations"; }
    public override void CreateNodes()
    {
      base.CreateNodes();
      foreach ( ComunicationNet.StationRow cRow in m_dataset.Station )
        if ( cRow.RowState != DataRowState.Deleted )
        {
          StationRowWrapper newWrapper = new StationRowWrapper( cRow,null );
          newWrapper.AddActionTreeNode( m_Node, 13, 13 );
          newWrapper.CreateNodes();
        }
    }
    internal static TreeNode CreateNode( ComunicationNet dataset )
    {
      SA_TopWrapper newWrapper = new SA_TopWrapper( dataset );
      TreeNode cWTN = newWrapper.AddActionTreeNode( 7, 7 );
      newWrapper.CreateNodes();
      return cWTN;
    }
    public override void PasteChildObject( IAction objToPaste )
    {
      if ( objToPaste is StationRowWrapper )
      {
        StationRowWrapper wrapperToPaste = objToPaste as StationRowWrapper;
        m_dataset.Station.NewStationRow( wrapperToPaste.DataRow, false, "<root>" );
      }
    }
    public override bool CanBePastedAsChild( IAction objToPaste ) { return objToPaste is StationRowWrapper; }
    public override void MoveChildObject( IAction objToPaste ) { }
  }
  #endregion
  #region CA_TopWrapper class (Channel Action TopWrapper class)
  /// <summary>
  /// CA - Channel Action
  /// </summary>
  internal class CA_TopWrapper: TopWrapper<ComunicationNet.ChannelsRow>
  {
    private CA_TopWrapper( ComunicationNet dataset )
      : base( dataset )
    { }
    public override bool CanBeCopied { get { return false; } }
    public override IAction CreateNewChildObject()
    {
      return new ChannelsRowWrapper( m_dataset.Channels.NewChannelsRow( "<root>" ) );
    }
    public override string ToString() { return "Channels"; }
    public override void CreateNodes()
    {
      base.CreateNodes();
      foreach ( ComunicationNet.ChannelsRow currCh in m_dataset.Channels )
      {
        if ( currCh.RowState != DataRowState.Deleted )
        {
          ChannelsRowWrapper newWrapper = new ChannelsRowWrapper( currCh );
          newWrapper.AddActionTreeNode( m_Node, 1, 1 );
          newWrapper.CreateNodes();
        }
      }
    }
    internal static TreeNode CreateNode( ComunicationNet dataset )
    {
      CA_TopWrapper newWrapper = new CA_TopWrapper( dataset );
      TreeNode cWTN = newWrapper.AddActionTreeNode( 0, 0 );
      newWrapper.CreateNodes();
      return cWTN;
    }
    public override void PasteChildObject( IAction objToPaste )
    {
      if ( objToPaste is ChannelsRowWrapper )
      {
        ComunicationNet.ChannelsRow rowToPaste = ( (ChannelsRowWrapper)objToPaste ).DataRow;
        m_dataset.Channels.NewChannelsRow( rowToPaste, false, "CopyOf_" + rowToPaste.Name );
      }
    }
    public override bool CanBePastedAsChild( IAction objToPaste ) { return objToPaste is ChannelsRowWrapper; }
    public override void MoveChildObject( IAction objToPaste ) { }
  }
  #endregion
}