namespace NetworkConfig.HMI
{
  partial class ProgresReport
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
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point( 27, 53 );
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size( 250, 23 );
      this.progressBar1.TabIndex = 0;
      this.progressBar1.UseWaitCursor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 24, 23 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 0, 13 );
      this.label1.TabIndex = 1;
      this.label1.UseWaitCursor = true;
      // 
      // ProgresReport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 311, 108 );
      this.Controls.Add( this.label1 );
      this.Controls.Add( this.progressBar1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "ProgresReport";
      this.ShowIcon = false;
      this.Text = "Opreration in progres";
      this.UseWaitCursor = true;
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label label1;
  }
}