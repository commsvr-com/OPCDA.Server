//<summary>
//  Title   : IAction
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081007: mzbrzezny: cleanup after unfinished add operation is added
//    Tomek Siwecki - from 12-2006 to 2-2007 - implementation
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;

namespace NetworkConfig.HMI
{
  /// <summary>
  /// Interface to support operation on the tree nodes
  /// </summary>
  interface IAction: IDisposable
  {
    #region Properties
    /// <summary>
    /// Get the boolean value indicated that this object can create child object. 
    /// </summary>
    bool CanCreateChild
    {
      get;
    }
    /// <summary>
    ///Get the boolean value indicated that this object can be deleted. 
    /// </summary>
    bool CanBeDeleted
    {
      get;
    }
    /// <summary>
    /// Get the boolean value indicated that this object can be copy or not. 
    /// </summary>
    bool CanBeCopied
    {
      get;
    }
    #endregion
    #region Methods
    /// <summary>
    /// Adds new row to the table
    /// </summary>
    void AddObjectToTable();
    /// <summary>
    /// cleanup after unfinished add operation
    /// </summary>
    void AddUnfinishedCleanup();
    /// <summary>
    /// Creates new child row in dataset
    /// </summary>
    /// <returns>IAction interface</returns>
    IAction CreateNewChildObject();
    /// <summary>
    /// Creates treeview nodes 
    /// </summary>
    void CreateNodes();
    /// <summary>
    /// Deletes row from the table
    /// </summary>
    void DeleteObject();
    /// <summary>
    /// Pastes specified IAction object under this node
    /// </summary>
    /// <param name="objToPaste">Object to paste</param>
    void PasteChildObject( IAction objToPaste );
    /// <summary>
    /// Checks if specified object can be pasted under this object 
    /// </summary>
    /// <param name="objToPaste">Object to paste</param>
    /// <returns>True if specified object can be pasted hare</returns>
    bool CanBePastedAsChild( IAction objToPaste );
    /// <summary>
    /// Checks if specified <see cref="IAction"/> object can be moved
    /// </summary>
    /// <returns>True if specified object can be moved</returns>
    bool CanBeMoved();
    /// <summary>
    /// Moves specified <see cref="IAction"/> object under this wrapper
    /// </summary>
    /// <param name="pObjToMove"><see cref="IAction"/> object to be moved</param>
    void MoveChildObject( IAction pObjToMove );
    /// <summary>
    /// Inform the object that some values have been changed.
    /// </summary>
    void HasChanged();
    #endregion
  }
}
