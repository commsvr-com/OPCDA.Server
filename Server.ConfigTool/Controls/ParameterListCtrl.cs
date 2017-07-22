using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Opc.ConfigTool
{
    public partial class ParameterListCtrl : Opc.ConfigTool.BaseListCtrl
    {
        public ParameterListCtrl()
        {
            InitializeComponent();                        
			SetColumns(m_ColumnNames); 
        }

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
                foreach (KeyValuePair<string,string> entry in server.Parameters)
                {
                    AddItem(entry);
                }
            }

            AdjustColumns();
        }

        /// <summary>
        /// Returns the parameters in the control.
        /// </summary>
        public Dictionary<string,string> GetParameters()
        {
            Dictionary<string,string> parameters = new Dictionary<string,string>();

            foreach (ListViewItem item in ItemsLV.Items)
            {
                if (item.Tag is KeyValuePair<string,string>)
                {
                    KeyValuePair<string,string> entry = (KeyValuePair<string,string>)item.Tag;
                    parameters[entry.Key] = entry.Value;
                }
            }

            return parameters;
        }
		#endregion
                
        #region Overridden Methods
        /// <see cref="BaseListCtrl.EnableMenuItems" />
		protected override void EnableMenuItems(ListViewItem clickedItem)
		{
            NewMI.Enabled    = true;
            EditMI.Enabled   = ItemsLV.SelectedItems.Count == 1;
            DeleteMI.Enabled = ItemsLV.SelectedItems.Count > 0;
		}
        
        /// <see cref="BaseListCtrl.PickItems" />
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

        /// <see cref="BaseListCtrl.UpdateItem" />
        protected override void UpdateItem(ListViewItem listItem, object item)
        {
			if (!(item is KeyValuePair<string,string>))
			{
				base.UpdateItem(listItem, item);
				return;
			}

			KeyValuePair<string,string> entry = (KeyValuePair<string,string>)item;
            
            listItem.SubItems[0].Text = entry.Key;
            listItem.SubItems[1].Text = entry.Value;

			listItem.Tag = item;
            listItem.ImageKey = "Property";
        }
        #endregion
        
        #region Event Handlers
        private void NewMI_Click(object sender, EventArgs e)
        {
            try
            {
                KeyValuePair<string,string> parameter = new ParameterEditDlg().ShowDialog(new KeyValuePair<string,string>());
                
                if (String.IsNullOrEmpty(parameter.Key))
                {
                    return;
                }
                    
                AddItem(parameter);
                AdjustColumns();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void EditMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemsLV.SelectedItems.Count != 1)
                {
                    return;
                }

                KeyValuePair<string,string> parameter = new ParameterEditDlg().ShowDialog((KeyValuePair<string,string>)ItemsLV.SelectedItems[0].Tag);
                
                if (String.IsNullOrEmpty(parameter.Key))
                {
                    return;
                }

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
                List<ListViewItem> itemsToDelete = new List<ListViewItem>();

                foreach (ListViewItem item in ItemsLV.SelectedItems)
                {
                    itemsToDelete.Add(item);
                }
                
                foreach (ListViewItem item in itemsToDelete)
                {
                    item.Remove();
                }

                AdjustColumns();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}

