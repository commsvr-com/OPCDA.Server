//_______________________________________________________________
//  Title   : ComServerListUserControl
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate:  $
//  $Rev: $
//  $LastChangedBy: $
//  $URL: $
//  $Id:  $
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using CAS.CommServer.DA.Server.ConfigTool.ServersModel;
using OpcRcw;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  /// <summary>
  /// Class ComServerListUserControl.
  /// </summary>
  /// <seealso cref="BaseListUserControl" />
  public partial class ComServerListUserControl : BaseListUserControl
  {

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ComServerListUserControl"/> class.
    /// </summary>
    public ComServerListUserControl()
    {
      InitializeComponent();
      SetColumns(m_ColumnNames);
    }
    #endregion

    #region Public Interface
    /// <summary>
    /// Clears the contents of the control,
    /// </summary>
    public void Clear()
    {
      ItemsLV.Items.Clear();
      AdjustColumns();
    }
    /// <summary>
    /// Enumerates classes in category and populates this control.
    /// </summary>
    /// <param name="categoryId">The category identifier used byt the <see cref="ConfigUtilities.EnumClassesInCategory"/> to enumerate the servers.</param>
    public void Initialize(Guid categoryId)
    {
      Clear();
      m_CATID = categoryId;
      List<Guid> clsids = Utils.EnumClassesInCategories(categoryId);
      foreach (Guid clsid in clsids)
        AddItem(clsid);
      AdjustColumns();
    }
    #endregion

    #region Overridden Methods
    /// <see cref="BaseListUserControl.EnableMenuItems" />
    protected override void EnableMenuItems(ListViewItem clickedItem)
    {
      EditMI.Visible = m_CATID == ConfigUtilities.CATID_RegisteredDotNetOpcServers;
      EditMI.Enabled = ItemsLV.SelectedItems.Count == 1;
      DeleteMI.Enabled = ItemsLV.SelectedItems.Count > 0;
    }
    /// <see cref="BaseListUserControl.PickItems" />
    protected override void PickItems()
    {
      base.PickItems();
      if (ItemsLV.SelectedItems.Count == 1)
      {
        EditMI_Click(this, null);
        return;
      }
    }
    /// <summary>
    /// Updates a list item with the current contents of an object.
    /// </summary>
    /// <param name="listItem">The list item.</param>
    /// <param name="item">The item.</param>
    /// <see cref="BaseListUserControl.UpdateItem" />
    protected override void UpdateItem(ListViewItem listItem, object item)
    {
      if (!(item is Guid))
      {
        base.UpdateItem(listItem, item);
        return;
      }
      Guid _clsid = (Guid)item;
      listItem.SubItems[0].Text = Utils.ProgIDFromCLSID(_clsid);
      listItem.SubItems[1].Text = Utils.GetExecutablePath(_clsid);
      listItem.Tag = item;
      if (m_CATID == ConfigUtilities.CATID_DotNetOpcServerWrappers)
        listItem.ImageKey = "Folder";
      else if (m_CATID == ConfigUtilities.CATID_RegisteredDotNetOpcServers)
        listItem.ImageKey = "Method";
      else
        listItem.ImageKey = "Object";
    }
    #endregion

    #region Event Handlers
    private void EditMI_Click(object sender, EventArgs e)
    {
      try
      {
        if (ItemsLV.SelectedItems.Count != 1)
          return;
        if (m_CATID != ConfigUtilities.CATID_RegisteredDotNetOpcServers)
          return;
        Guid clsid = (Guid)ItemsLV.SelectedItems[0].Tag;
        RegisteredDotNetOpcServer server = new RegisterServerDlg().ShowDialog(new RegisteredDotNetOpcServer(clsid));
        if (server != null)
          UpdateItem(ItemsLV.SelectedItems[0], server.CLSID);
      }
      catch (Exception exception)
      {
        GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
      }
    }
    private void DeleteMI_Click(object sender, EventArgs e)
    {
      try
      {
        Guid[] _CLSIDS = base.GetSelectedItems(typeof(Guid)) as Guid[];
        if (_CLSIDS == null || _CLSIDS.Length == 0)
          return;
        for (int ii = 0; ii < _CLSIDS.Length; ii++)
          Utils.UnregisterComServer(_CLSIDS[ii]);
        List<ListViewItem> itemsToDelete = new List<ListViewItem>();
        foreach (ListViewItem item in ItemsLV.SelectedItems)
          itemsToDelete.Add(item);
        foreach (ListViewItem item in itemsToDelete)
          item.Remove();
      }
      catch (Exception exception)
      {
        GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
      }
    }
    #endregion

    #region Private Fields
    /// <summary>
    /// The columns to display in the control.
    /// </summary>
    private readonly object[][] m_ColumnNames = new object[][]
    {
       new object[] { "ProgId",   HorizontalAlignment.Left, null },
       new object[] { "Codebase", HorizontalAlignment.Left, null }
    };

    private Guid m_CATID;
    #endregion

  }
}

