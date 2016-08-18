namespace NetworkConfig.HMI.Editors
{
  partial class TagsCollection
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.ImageList cn_ButonsImages;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( TagsCollection ) );
      this.cn_PropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.cn_listBox = new System.Windows.Forms.ListBox();
      this.cn_ButtonUp = new System.Windows.Forms.Button();
      this.cn_ButtonAdd = new System.Windows.Forms.Button();
      this.cn_ButtonDelete = new System.Windows.Forms.Button();
      this.cn_ButtonDown = new System.Windows.Forms.Button();
      this.cn_ButtonOK = new System.Windows.Forms.Button();
      this.cn_ButtonCANCEL = new System.Windows.Forms.Button();
      cn_ButonsImages = new System.Windows.Forms.ImageList( this.components );
      this.SuspendLayout();
      // 
      // cn_ButonsImages
      // 
      cn_ButonsImages.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "cn_ButonsImages.ImageStream" ) ) );
      cn_ButonsImages.TransparentColor = System.Drawing.Color.Transparent;
      cn_ButonsImages.Images.SetKeyName( 0, "EditionAdd32.png" );
      cn_ButonsImages.Images.SetKeyName( 1, "EditionArrowDown32.png" );
      cn_ButonsImages.Images.SetKeyName( 2, "EditionArrowUp32.png" );
      cn_ButonsImages.Images.SetKeyName( 3, "EditionDelete32.png" );
      // 
      // cn_PropertyGrid
      // 
      this.cn_PropertyGrid.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cn_PropertyGrid.Location = new System.Drawing.Point( 270, 12 );
      this.cn_PropertyGrid.Name = "cn_PropertyGrid";
      this.cn_PropertyGrid.Size = new System.Drawing.Size( 206, 248 );
      this.cn_PropertyGrid.TabIndex = 0;
      // 
      // cn_listBox
      // 
      this.cn_listBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.cn_listBox.FormattingEnabled = true;
      this.cn_listBox.Location = new System.Drawing.Point( 12, 12 );
      this.cn_listBox.Name = "cn_listBox";
      this.cn_listBox.Size = new System.Drawing.Size( 206, 238 );
      this.cn_listBox.TabIndex = 1;
      this.cn_listBox.SelectedIndexChanged += new System.EventHandler( this.cn_listBox_SelectedIndexChanged );
      // 
      // cn_ButtonUp
      // 
      this.cn_ButtonUp.ImageIndex = 2;
      this.cn_ButtonUp.ImageList = cn_ButonsImages;
      this.cn_ButtonUp.Location = new System.Drawing.Point( 224, 12 );
      this.cn_ButtonUp.Name = "cn_ButtonUp";
      this.cn_ButtonUp.Size = new System.Drawing.Size( 40, 40 );
      this.cn_ButtonUp.TabIndex = 2;
      this.cn_ButtonUp.UseVisualStyleBackColor = true;
      this.cn_ButtonUp.Click += new System.EventHandler( this.cn_ButtonUp_Click );
      // 
      // cn_ButtonAdd
      // 
      this.cn_ButtonAdd.ImageIndex = 0;
      this.cn_ButtonAdd.ImageList = cn_ButonsImages;
      this.cn_ButtonAdd.Location = new System.Drawing.Point( 224, 60 );
      this.cn_ButtonAdd.Name = "cn_ButtonAdd";
      this.cn_ButtonAdd.Size = new System.Drawing.Size( 40, 40 );
      this.cn_ButtonAdd.TabIndex = 3;
      this.cn_ButtonAdd.UseVisualStyleBackColor = true;
      this.cn_ButtonAdd.Click += new System.EventHandler( this.cn_ButtonAdd_Click );
      // 
      // cn_ButtonDelete
      // 
      this.cn_ButtonDelete.ImageIndex = 3;
      this.cn_ButtonDelete.ImageList = cn_ButonsImages;
      this.cn_ButtonDelete.Location = new System.Drawing.Point( 224, 108 );
      this.cn_ButtonDelete.Name = "cn_ButtonDelete";
      this.cn_ButtonDelete.Size = new System.Drawing.Size( 40, 40 );
      this.cn_ButtonDelete.TabIndex = 4;
      this.cn_ButtonDelete.UseVisualStyleBackColor = true;
      this.cn_ButtonDelete.Click += new System.EventHandler( this.cn_ButtonDelete_Click );
      // 
      // cn_ButtonDown
      // 
      this.cn_ButtonDown.ImageIndex = 1;
      this.cn_ButtonDown.ImageList = cn_ButonsImages;
      this.cn_ButtonDown.Location = new System.Drawing.Point( 224, 156 );
      this.cn_ButtonDown.Name = "cn_ButtonDown";
      this.cn_ButtonDown.Size = new System.Drawing.Size( 40, 40 );
      this.cn_ButtonDown.TabIndex = 5;
      this.cn_ButtonDown.UseVisualStyleBackColor = true;
      this.cn_ButtonDown.Click += new System.EventHandler( this.cn_ButtonDown_Click );
      // 
      // cn_ButtonOK
      // 
      this.cn_ButtonOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.cn_ButtonOK.Location = new System.Drawing.Point( 12, 266 );
      this.cn_ButtonOK.Name = "cn_ButtonOK";
      this.cn_ButtonOK.Size = new System.Drawing.Size( 206, 40 );
      this.cn_ButtonOK.TabIndex = 6;
      this.cn_ButtonOK.Text = "OK";
      this.cn_ButtonOK.UseVisualStyleBackColor = true;
      this.cn_ButtonOK.Click += new System.EventHandler( this.cn_ButtonOK_Click );
      // 
      // cn_ButtonCANCEL
      // 
      this.cn_ButtonCANCEL.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cn_ButtonCANCEL.Location = new System.Drawing.Point( 270, 266 );
      this.cn_ButtonCANCEL.Name = "cn_ButtonCANCEL";
      this.cn_ButtonCANCEL.Size = new System.Drawing.Size( 206, 40 );
      this.cn_ButtonCANCEL.TabIndex = 7;
      this.cn_ButtonCANCEL.Text = "CANCEL";
      this.cn_ButtonCANCEL.UseVisualStyleBackColor = true;
      this.cn_ButtonCANCEL.Click += new System.EventHandler( this.cn_ButtonCANCEL_Click );
      // 
      // TagsCollection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 492, 316 );
      this.Controls.Add( this.cn_ButtonCANCEL );
      this.Controls.Add( this.cn_ButtonOK );
      this.Controls.Add( this.cn_ButtonDown );
      this.Controls.Add( this.cn_ButtonDelete );
      this.Controls.Add( this.cn_ButtonAdd );
      this.Controls.Add( this.cn_ButtonUp );
      this.Controls.Add( this.cn_listBox );
      this.Controls.Add( this.cn_PropertyGrid );
      this.MinimumSize = new System.Drawing.Size( 500, 350 );
      this.Name = "TagsCollection";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "TagsCollection";
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.PropertyGrid cn_PropertyGrid;
    private System.Windows.Forms.ListBox cn_listBox;
    private System.Windows.Forms.Button cn_ButtonUp;
    private System.Windows.Forms.Button cn_ButtonAdd;
    private System.Windows.Forms.Button cn_ButtonDelete;
    private System.Windows.Forms.Button cn_ButtonDown;
    private System.Windows.Forms.Button cn_ButtonOK;
    private System.Windows.Forms.Button cn_ButtonCANCEL;
  }
}