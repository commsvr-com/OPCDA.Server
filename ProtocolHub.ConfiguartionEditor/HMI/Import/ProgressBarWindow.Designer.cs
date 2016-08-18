namespace NetworkConfig.HMI.Import
{
  partial class ProgressBarWindow
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
      this.backgroundWorker_DoTheJob = new System.ComponentModel.BackgroundWorker();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.label_percent = new System.Windows.Forms.Label();
      this.label_info = new System.Windows.Forms.Label();
      this.button_cancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // backgroundWorker_DoTheJob
      // 
      this.backgroundWorker_DoTheJob.WorkerSupportsCancellation = true;
      this.backgroundWorker_DoTheJob.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler( this.backgroundWorker_DoTheJob_RunWorkerCompleted );
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point( 12, 12 );
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size( 239, 23 );
      this.progressBar1.TabIndex = 0;
      // 
      // label_percent
      // 
      this.label_percent.AutoSize = true;
      this.label_percent.Location = new System.Drawing.Point( 257, 17 );
      this.label_percent.Name = "label_percent";
      this.label_percent.Size = new System.Drawing.Size( 33, 13 );
      this.label_percent.TabIndex = 1;
      this.label_percent.Text = "100%";
      // 
      // label_info
      // 
      this.label_info.AutoSize = true;
      this.label_info.Location = new System.Drawing.Point( 15, 46 );
      this.label_info.Name = "label_info";
      this.label_info.Size = new System.Drawing.Size( 59, 13 );
      this.label_info.TabIndex = 2;
      this.label_info.Text = "Information";
      // 
      // button_cancel
      // 
      this.button_cancel.Location = new System.Drawing.Point( 229, 41 );
      this.button_cancel.Name = "button_cancel";
      this.button_cancel.Size = new System.Drawing.Size( 61, 23 );
      this.button_cancel.TabIndex = 3;
      this.button_cancel.Text = "Cancel";
      this.button_cancel.UseVisualStyleBackColor = true;
      this.button_cancel.Click += new System.EventHandler( this.button_cancel_Click );
      // 
      // ProgressBarWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 299, 71 );
      this.ControlBox = false;
      this.Controls.Add( this.button_cancel );
      this.Controls.Add( this.label_info );
      this.Controls.Add( this.label_percent );
      this.Controls.Add( this.progressBar1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "ProgressBarWindow";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "ProgressBarWindow";
      this.TopMost = true;
      this.Load += new System.EventHandler( this.ProgressBarWindow_Load );
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.ComponentModel.BackgroundWorker backgroundWorker_DoTheJob;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label label_percent;
    private System.Windows.Forms.Label label_info;
    private System.Windows.Forms.Button button_cancel;
  }
}