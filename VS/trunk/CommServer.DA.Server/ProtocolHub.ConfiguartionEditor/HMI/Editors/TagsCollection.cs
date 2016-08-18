//<summary>
//  Title   : Tag Collection Form
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081203: mzbrzezny: fixed issue with configuration disappearing when clicking on cancel in tag collection window
//    20081007: mzbrzezny: cleanup after unfinished add operation is added
//    20081006: mzbrzezny: if DataBlock row is detached (not connected to the table) no items ca be created (the message is displayed)
//    20081003 - mzbrzezny: changes related to sorted dictionary and item default settings implementation
//    20070610 - mpostol - created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Windows.Forms;
using CAS.Lib.ControlLibrary;
using CAS.NetworkConfigLib;

namespace NetworkConfig.HMI.Editors
{
  /// <summary>
  /// Form that allow to sort tags
  /// </summary>
  public partial class TagsCollection: Form
  {
    #region private
    DataBlocksRowWrapper dataBlockRowWrapper;
    private ComunicationNet.DataBlocksRow m_ParBlock;
    private ComunicationNet m_DB;
    private bool CanMoveDown()
    {
      return cn_listBox.SelectedIndex < cn_listBox.Items.Count - 1 && cn_listBox.SelectedIndex >= 0;
    }
    private bool CanMoveUp()
    {
      return cn_listBox.SelectedIndex > 0 && cn_listBox.SelectedIndex >= 0;
    }
    private void SetButtonsEnableProperty()
    {
      cn_ButtonDelete.Enabled = cn_listBox.Items.Count > 0 && cn_listBox.SelectedIndex >= 0;
      cn_ButtonUp.Enabled = CanMoveUp();
      cn_ButtonDown.Enabled = CanMoveDown();
    }
    private void FillLlist( int pSelIdx )
    {
      cn_listBox.Items.Clear();
      foreach ( TagsRowWrapper cr in dataBlockRowWrapper.SortedDictionaryOfTagsRowWrapper.Values )
        cn_listBox.Items.Add( cr );
      if ( cn_listBox.Items.Count > 0 )
        cn_listBox.SelectedIndex = pSelIdx;
      SetButtonsEnableProperty();
    }
    private TagsCollection()
    {
      InitializeComponent();
    }
    private void ExchangeRows( TagsRowWrapper cSelObj, TagsRowWrapper cAboveObj, int pSelIdx )
    {
      m_DB.EnforceConstraints = false;
      long cRowIdx = cSelObj.TagID;
      cSelObj.TagID = cAboveObj.TagID;
      cAboveObj.TagID = cRowIdx;
      m_DB.EnforceConstraints = true;
      FillLlist( pSelIdx );
    }
    #endregion
    /// <summary>
    /// Creator for the Tag collection Form
    /// </summary>
    /// <param name="pBlock">Block (<see cref="ComunicationNet.DataBlocksRow"/> to dispaly</param>
    /// <param name="DataBlockRowWrapper">The data block row wrapper.</param>
    internal TagsCollection( ComunicationNet.DataBlocksRow pBlock, DataBlocksRowWrapper DataBlockRowWrapper )
      : this()
    {
      dataBlockRowWrapper = DataBlockRowWrapper;
      m_ParBlock = pBlock;
      m_DB = pBlock.Table.DataSet as ComunicationNet;
      m_DB.Tags.AcceptChanges();
      m_DB.TagBit.AcceptChanges();
      m_DB.ItemPropertiesTable.AcceptChanges();
      FillLlist( 0 );
    }
    #region events handlers
    private void cn_ButtonOK_Click( object sender, EventArgs e )
    {
      this.DialogResult = DialogResult.OK;
      m_DB.Tags.AcceptChanges();
      m_DB.TagBit.AcceptChanges();
      m_DB.ItemPropertiesTable.AcceptChanges();
      this.Close();
    }
    private void cn_ButtonCANCEL_Click( object sender, EventArgs e )
    {
      this.DialogResult = DialogResult.Cancel;
      m_DB.Tags.RejectChanges();
      m_DB.TagBit.RejectChanges();
      m_DB.ItemPropertiesTable.RejectChanges();
      this.Close();
    }
    private void cn_ButtonUp_Click( object sender, EventArgs e )
    {
      if ( !CanMoveUp() )
        return;
      int cSelIdx = cn_listBox.SelectedIndex;
      TagsRowWrapper cSelObj = cn_listBox.SelectedItem as TagsRowWrapper;
      TagsRowWrapper cAboveObj = cn_listBox.Items[ cSelIdx - 1 ] as TagsRowWrapper;
      ExchangeRows( cSelObj, cAboveObj, cSelIdx - 1 );
    }
    private void cn_ButtonAdd_Click( object sender, EventArgs e )
    {
      try
      {
        if (m_ParBlock.RowState == System.Data.DataRowState.Detached)
        {
          MessageBox.Show(Properties.Resources.tx_TagsCollection_pleasecreatedatablockfirst);
          return;
        }
        m_DB.EnforceConstraints = false;
        ComunicationNet.TagsRow cNTG = m_DB.Tags.NewTagsRow(m_ParBlock.DatBlockID, m_ParBlock.Name);
        TagsRowWrapper cRW = new TagsRowWrapper(cNTG, dataBlockRowWrapper, true);
        using (AddObject<TagsRowWrapper> cDial = new AddObject<TagsRowWrapper>(cRW))
        {
          cDial.ShowDialog(this);
          if (cDial.DialogResult != DialogResult.OK)
          {
            cNTG.Delete();
            cRW.AddUnfinishedCleanup();
            return;
          }
        }
        cRW.AddObjectToTable();
        FillLlist(cn_listBox.Items.Count - 1);
      }
      catch (Exception)
      {
      }
      finally
      {
        m_DB.EnforceConstraints = true;
      }
    }
    private void cn_ButtonDelete_Click( object sender, EventArgs e )
    {
      ( (TagsRowWrapper)cn_listBox.SelectedItem ).DeleteObject();
      FillLlist( 0 );
    }
    private void cn_ButtonDown_Click( object sender, EventArgs e )
    {
      if ( !CanMoveDown() )
        return;
      int cSelIdx = cn_listBox.SelectedIndex;
      TagsRowWrapper cSelObj = cn_listBox.SelectedItem as TagsRowWrapper;
      TagsRowWrapper cAboveObj = cn_listBox.Items[ cSelIdx + 1 ] as TagsRowWrapper;
      ExchangeRows( cSelObj, cAboveObj, cSelIdx + 1 );
    }
    private void cn_listBox_SelectedIndexChanged( object sender, EventArgs e )
    {
      cn_PropertyGrid.SelectedObject = cn_listBox.SelectedItem;
      SetButtonsEnableProperty();
    }
    #endregion
  }
}