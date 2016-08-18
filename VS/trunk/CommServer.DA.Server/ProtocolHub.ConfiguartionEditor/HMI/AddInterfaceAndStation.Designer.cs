namespace NetworkConfig.HMI
{
  partial class AddInterfaceAndStation
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
      this.pgInterface = new System.Windows.Forms.PropertyGrid();
      this.cmbStations = new System.Windows.Forms.ComboBox();
      this.btnNewSerial = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.btnAdd = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // pgInterface
      // 
      this.pgInterface.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.pgInterface.Location = new System.Drawing.Point( 2, 1 );
      this.pgInterface.Name = "pgInterface";
      this.pgInterface.Size = new System.Drawing.Size( 237, 261 );
      this.pgInterface.TabIndex = 0;
      // 
      // cmbStations
      // 
      this.cmbStations.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmbStations.FormattingEnabled = true;
      this.cmbStations.Location = new System.Drawing.Point( 12, 289 );
      this.cmbStations.Name = "cmbStations";
      this.cmbStations.Size = new System.Drawing.Size( 101, 21 );
      this.cmbStations.TabIndex = 1;
      this.cmbStations.SelectedIndexChanged += new System.EventHandler( this.cmbStations_SelectedIndexChanged );
      // 
      // btnNewSerial
      // 
      this.btnNewSerial.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.btnNewSerial.Location = new System.Drawing.Point( 130, 288 );
      this.btnNewSerial.Name = "btnNewSerial";
      this.btnNewSerial.Size = new System.Drawing.Size( 100, 23 );
      this.btnNewSerial.TabIndex = 2;
      this.btnNewSerial.Text = "New station";
      this.btnNewSerial.UseVisualStyleBackColor = true;
      this.btnNewSerial.Click += new System.EventHandler( this.btnNewSerial_Click );
      // 
      // label1
      // 
      this.label1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 12, 273 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 74, 13 );
      this.label1.TabIndex = 3;
      this.label1.Text = "Select station:";
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.btnAdd.Location = new System.Drawing.Point( 12, 329 );
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size( 100, 23 );
      this.btnAdd.TabIndex = 5;
      this.btnAdd.Text = "Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler( this.btnAdd_Click );
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.btnCancel.Location = new System.Drawing.Point( 130, 329 );
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size( 100, 23 );
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
      // 
      // AddInterfaceAndStation
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 242, 366 );
      this.Controls.Add( this.btnCancel );
      this.Controls.Add( this.btnAdd );
      this.Controls.Add( this.label1 );
      this.Controls.Add( this.btnNewSerial );
      this.Controls.Add( this.cmbStations );
      this.Controls.Add( this.pgInterface );
      this.MinimumSize = new System.Drawing.Size( 250, 400 );
      this.Name = "AddInterfaceAndStation";
      this.ShowIcon = false;
      this.Text = "Add new interface and station";
      this.Load += new System.EventHandler( this.AddInterfaceAndStation_Load );
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PropertyGrid pgInterface;
    private System.Windows.Forms.ComboBox cmbStations;
    private System.Windows.Forms.Button btnNewSerial;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnCancel;
  }
}