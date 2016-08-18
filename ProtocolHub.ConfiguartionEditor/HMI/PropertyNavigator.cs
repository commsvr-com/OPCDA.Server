//<summary>
//  Title   : Property Navigator
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//    20081203: mzbrzezny: undoredomanager is switched off
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Windows.Forms;
using CAS.NetworkConfigLib;

namespace NetworkConfig.HMI
{
  internal partial class PropertyNavigator: UserControl
  {
    #region private
    private ClipboardOperation clipboard = new ClipboardOperation();
    private IAction GetSelectedAction { get { return (IAction)cn_TreeView.SelectedNode.Tag; } }
    #endregion
    #region creator
    internal PropertyNavigator()
    {
      InitializeComponent();
      //this.cn_TreeView.P += new System.EventHandler( TreeView_ParentChanged );
    }
    #endregion
    #region cNode.Parent
    public void Refresh( ComunicationNet myConfig )
    {
      myConfig.AcceptChanges();
      TreeBuilder.CreateTree( myConfig, cn_TreeView );
      base.Refresh();
    }
    internal void Delate()
    {
      clipboard.Clear();
      TreeNode cNode = cn_TreeView.SelectedNode as TreeNode;
      if ( cNode == null )
        return;
      IAction currAct = cn_TreeView.SelectedNode.Tag as IAction;
      System.Diagnostics.Debug.Assert( currAct.CanBeDeleted );
      DialogResult result;
      result = MessageBox.Show
        ( "Selected node will be deleted. Do you want to delete selected node ?", "Delete selected object", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
      if ( result == DialogResult.Yes )
      {
#if UNDOREDO
       RTLib.DataBase.UndoRedo.UndoRedoMenager.BeginTransaction();
#endif
        currAct.DeleteObject();
#if UNDOREDO
        RTLib.DataBase.UndoRedo.UndoRedoMenager.EndTransaction();
#endif
      }
      //( (IAction)cNode.Parent.Tag ).CreateNodes( cNode.Parent );
    }
    internal bool CanBePasted()
    {
      if ( clipboard.ClipboardData != null )
        return GetSelectedAction.CanBePastedAsChild( clipboard.ClipboardData.Tag as IAction );
      else
        return false;
    }
    internal string GetPasteString { get { return clipboard.ToString(); } }
    #endregion
    #region Clipboard
    internal void Copy()
    {
      TreeNode tn = cn_TreeView.SelectedNode;
      if ( ( tn == null ) || !( (IAction)tn.Tag ).CanBeCopied )
        return;
      clipboard.SetDataObject( tn, ClipboardOperationType.Copy );
      DataObject clipboardData = new DataObject();
      clipboardData.SetData( DataFormats.Text, true, ( (IAction)tn.Tag ).ToString() );
      Clipboard.SetDataObject( clipboardData );
    }
    internal void Cut()
    {
      TreeNode tn = cn_TreeView.SelectedNode;
      if ( ( tn == null ) || !( (IAction)tn.Tag ).CanBeMoved() )
        return;
      clipboard.SetDataObject( tn, ClipboardOperationType.Cut );
      DataObject clipboardData = new DataObject();
      clipboardData.SetData( DataFormats.Text, true, ( (IAction)tn.Tag ).ToString() );
      Clipboard.SetDataObject( clipboardData );
    }
    internal void Paste()
    {
#if UNDOREDO
      RTLib.DataBase.UndoRedo.UndoRedoMenager.BeginTransaction();
#endif
      clipboard.Paste( cn_TreeView.SelectedNode );
#if UNDOREDO
        RTLib.DataBase.UndoRedo.UndoRedoMenager.EndTransaction();
#endif
    }
    #endregion
    #region Event handlers
    private void PropertyNavigator_Load( object sender, EventArgs e )
    {
    }
    private void mTreeView_AfterSelect( object sender, TreeViewEventArgs e )
    {
      m_PropertyGrid.SelectedObject = ( (TreeView)sender ).SelectedNode.Tag;
    }
    private void m_PropertyGrid_PropertyValueChanged( object s, PropertyValueChangedEventArgs e )
    {
      ( (IAction)m_PropertyGrid.SelectedObject ).HasChanged();
    }
    #endregion
  }
}
