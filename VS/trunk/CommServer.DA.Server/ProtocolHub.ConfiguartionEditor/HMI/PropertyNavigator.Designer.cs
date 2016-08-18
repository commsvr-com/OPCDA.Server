namespace NetworkConfig.HMI
{
  internal partial class PropertyNavigator
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( PropertyNavigator ) );
      this.mSplitContainer = new System.Windows.Forms.SplitContainer();
      this.cn_TreeView = new System.Windows.Forms.TreeView();
      this.imglTreeview = new System.Windows.Forms.ImageList( this.components );
      this.m_PropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.mSplitContainer.Panel1.SuspendLayout();
      this.mSplitContainer.Panel2.SuspendLayout();
      this.mSplitContainer.SuspendLayout();
      this.SuspendLayout();
      // 
      // mSplitContainer
      // 
      this.mSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mSplitContainer.Location = new System.Drawing.Point( 0, 0 );
      this.mSplitContainer.Name = "mSplitContainer";
      // 
      // mSplitContainer.Panel1
      // 
      this.mSplitContainer.Panel1.Controls.Add( this.cn_TreeView );
      // 
      // mSplitContainer.Panel2
      // 
      this.mSplitContainer.Panel2.Controls.Add( this.m_PropertyGrid );
      this.mSplitContainer.Size = new System.Drawing.Size( 787, 347 );
      this.mSplitContainer.SplitterDistance = 262;
      this.mSplitContainer.SplitterWidth = 8;
      this.mSplitContainer.TabIndex = 2;
      // 
      // cn_TreeView
      // 
      this.cn_TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cn_TreeView.HideSelection = false;
      this.cn_TreeView.ImageIndex = 0;
      this.cn_TreeView.ImageList = this.imglTreeview;
      this.cn_TreeView.Location = new System.Drawing.Point( 0, 0 );
      this.cn_TreeView.Name = "cn_TreeView";
      this.cn_TreeView.SelectedImageIndex = 0;
      this.cn_TreeView.Size = new System.Drawing.Size( 262, 347 );
      this.cn_TreeView.TabIndex = 1;
      this.cn_TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.mTreeView_AfterSelect );
      // 
      // imglTreeview
      // 
      this.imglTreeview.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "imglTreeview.ImageStream" ) ) );
      this.imglTreeview.TransparentColor = System.Drawing.Color.Transparent;
      this.imglTreeview.Images.SetKeyName( 0, "kanaly 16.ico" );
      this.imglTreeview.Images.SetKeyName( 1, "kanal_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 2, "kanal_spec_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 3, "blok_danych_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 4, "blok_danych_spec_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 5, "grupa_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 6, "grupa_spec_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 7, "stacje 16.ico" );
      this.imglTreeview.Images.SetKeyName( 8, "stacje_spec 16.ico" );
      this.imglTreeview.Images.SetKeyName( 9, "port_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 10, "port_spec_48full.ico" );
      this.imglTreeview.Images.SetKeyName( 11, "protokol_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 12, "protokol_spec_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 13, "stacja_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 14, "stacja_spec_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 15, "tag_element_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 16, "tag_bitowy_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 17, "segment_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 18, "segment_spec_48_full.ico" );
      this.imglTreeview.Images.SetKeyName( 19, "kanaly_spec 16.ico" );
      // 
      // m_PropertyGrid
      // 
      this.m_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_PropertyGrid.Location = new System.Drawing.Point( 0, 0 );
      this.m_PropertyGrid.Name = "m_PropertyGrid";
      this.m_PropertyGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.m_PropertyGrid.Size = new System.Drawing.Size( 517, 347 );
      this.m_PropertyGrid.TabIndex = 2;
      this.m_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler( this.m_PropertyGrid_PropertyValueChanged );
      // 
      // PropertyNavigator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add( this.mSplitContainer );
      this.Name = "PropertyNavigator";
      this.Size = new System.Drawing.Size( 787, 347 );
      this.Load += new System.EventHandler( this.PropertyNavigator_Load );
      this.mSplitContainer.Panel1.ResumeLayout( false );
      this.mSplitContainer.Panel2.ResumeLayout( false );
      this.mSplitContainer.ResumeLayout( false );
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.SplitContainer mSplitContainer;
    internal System.Windows.Forms.TreeView cn_TreeView;
    private System.Windows.Forms.PropertyGrid m_PropertyGrid;
      protected internal System.Windows.Forms.ImageList imglTreeview;

  }
}
