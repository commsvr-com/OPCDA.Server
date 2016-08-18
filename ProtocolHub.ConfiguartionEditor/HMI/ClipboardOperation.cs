//<summary>
//  Title   : ClipboardOperation
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    Tomek Siwecki - February 2007 - Created 
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@eu
//  http://www.cas.eu
//</summary>

using System;
using System.Drawing;
using System.Windows.Forms;
using NetworkConfig.HMI;
using System.Data;

namespace NetworkConfig
{
  /// <summary>
  /// Class responsible for storing treeview node in buffor and operation type
  /// </summary>
  public class ClipboardOperation
  {
    #region private
    /// <summary>
    /// Operation type
    /// </summary>
    private ClipboardOperationType operationType;
    /// <summary>
    /// Treeview node stored in buffor
    /// </summary>
    private TreeNode data;
    private void RemoveCutMark()
    {
      if ( ( data != null ) && ( operationType == ClipboardOperationType.Cut ) )
        data.ForeColor = Color.Black;
    }
    #endregion
    #region Properties
    /// <summary>
    /// Gets or sets the operation type
    /// </summary>
    public ClipboardOperationType OpertationType { get { return operationType; } }
    /// <summary>
    /// Gets or sets data to be stored in buffor
    /// </summary>
    public TreeNode ClipboardData { get { return ( data == null ) || ( data.TreeView == null ) ? null : data; } }
    #endregion
    #region Methods
    /// <summary>
    /// Sets the data object.
    /// </summary>
    /// <param name="pData">The parent data.</param>
    /// <param name="pOperation">The parent operation.</param>
    public void SetDataObject( TreeNode pData, ClipboardOperationType pOperation )
    {
      RemoveCutMark();
      data = pData;
      operationType = pOperation;
      if ( pOperation == ClipboardOperationType.Cut )
        data.ForeColor = Color.LightGray;
    }
    /// <summary>
    /// Pastes buffor data under specified parent Treeview node.
    /// </summary>
    /// <remarks>
    /// If its copy opertion, executes PasteChildObject from specified parent wrapper
    /// If its cut opertaion, executes Move operation and change the operation type to copy 
    /// </remarks>
    /// <param name="pDestination"><see cref="TreeNode"/> to paste</param>
    public void Paste( TreeNode pDestination )
    {
      if ( pDestination != null )
      {
        IAction cDestination = pDestination.Tag as IAction;
        IAction cSource = data.Tag as IAction;
        switch ( operationType )
        {
          case ClipboardOperationType.Copy:
            cDestination.PasteChildObject( cSource );
            break;
          case ClipboardOperationType.Cut:
            cDestination.MoveChildObject( cSource );
            ( (IAction)data.Parent.Tag ).CreateNodes();
            data.Expand();
            break;
        }
        cDestination.CreateNodes();
        pDestination.Expand();
      }
    }
    #endregion
    #region Override
    /// <summary>
    /// Returns ToString() value from the wrapper
    /// </summary>
    /// <returns>ToString() value form the wrapper</returns>
    public override string ToString()
    {
      return ( data.Tag as IAction ).ToString();
    }
    /// <summary>
    /// Changes node color to default (black)
    /// </summary>
    public void Clear()
    {
      RemoveCutMark();
      data = null;
    }
    #endregion
  }
}