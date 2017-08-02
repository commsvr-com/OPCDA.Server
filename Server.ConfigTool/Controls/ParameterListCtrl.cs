//_______________________________________________________________
//  Title   : ParameterListControl
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

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  /// <summary>
  /// Class ParameterListControl - user control supporting parameters list editing 
  /// </summary>
  /// <seealso cref="CAS.CommServer.DA.Server.ConfigTool.BaseListUserControl" />
  /// TODO Edit XML Comment Template for ParameterListControl
  public partial class ParameterListControl : BaseListUserControl
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterListControl"/> class.
    /// </summary>
    public ParameterListControl()
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
    /// Displays the parameters in the control.
    /// </summary>
    public void Initialize(RegisteredDotNetOpcServer server)
    {
      Clear();
      if (server != null)
      {
        foreach (KeyValuePair<string, string> entry in server.Parameters)
          AddItem(entry);
      }
      AdjustColumns();
    }
    /// <summary>
    /// Returns the parameters in the control.
    /// </summary>
    public Dictionary<string, string> GetParameters()
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      foreach (ListViewItem item in ItemsLV.Items)
        if (item.Tag is KeyValuePair<string, string>)
        {
          KeyValuePair<string, string> entry = (KeyValuePair<string, string>)item.Tag;
          parameters[entry.Key] = entry.Value;
        }
      return parameters;
    }
    #endregion

    #region Overridden Methods
    /// <summary>
    /// Enables the state of menu items.
    /// </summary>
    /// <param name="clickedItem">The clicked item.</param>
    /// <see cref="BaseListUserControl.EnableMenuItems" />
    protected override void EnableMenuItems(ListViewItem clickedItem)
    {
      NewMI.Enabled = true;
      EditMI.Enabled = ItemsLV.SelectedItems.Count == 1;
      DeleteMI.Enabled = ItemsLV.SelectedItems.Count > 0;
    }
    /// <summary>
    /// Sends notifications whenever items in the control are 'picked'.
    /// </summary>
    /// <see cref="BaseListUserControl.PickItems" />
    protected override void PickItems()
    {
      base.PickItems();
      if (ItemsLV.SelectedItems.Count == 1)
      {
        EditMI_Click(this, null);
        return;
      }
      if (ItemsLV.Items.Count == 0)
      {
        NewMI_Click(this, null);
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
      if (!(item is KeyValuePair<string, string>))
      {
        base.UpdateItem(listItem, item);
        return;
      }
      KeyValuePair<string, string> _entry = (KeyValuePair<string, string>)item;
      listItem.SubItems[0].Text = _entry.Key;
      listItem.SubItems[1].Text = _entry.Value;
      listItem.Tag = item;
      listItem.ImageKey = "Property";
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles the Click event of the NewMI control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void NewMI_Click(object sender, EventArgs e)
    {
      try
      {
        KeyValuePair<string, string> parameter = new ParameterEditDlg().ShowDialog(new KeyValuePair<string, string>());
        if (String.IsNullOrEmpty(parameter.Key))
          return;
        AddItem(parameter);
        AdjustColumns();
      }
      catch (Exception exception)
      {
        GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
      }
    }
    /// <summary>
    /// Handles the Click event of the EditMI control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    /// TODO Edit XML Comment Template for EditMI_Click
    private void EditMI_Click(object sender, EventArgs e)
    {
      try
      {
        if (ItemsLV.SelectedItems.Count != 1)
          return;
        KeyValuePair<string, string> parameter = new ParameterEditDlg().ShowDialog((KeyValuePair<string, string>)ItemsLV.SelectedItems[0].Tag);
        if (String.IsNullOrEmpty(parameter.Key))
          return;
        UpdateItem(ItemsLV.SelectedItems[0], parameter);
        AdjustColumns();
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
        List<ListViewItem> _itemsToDelete = new List<ListViewItem>();
        foreach (ListViewItem item in ItemsLV.SelectedItems)
          _itemsToDelete.Add(item);
        foreach (ListViewItem item in _itemsToDelete)
          item.Remove();
        AdjustColumns();
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
       new object[] { "Name",  HorizontalAlignment.Left, null },
       new object[] { "Value", HorizontalAlignment.Left, null }
    };
    #endregion
  }
}

