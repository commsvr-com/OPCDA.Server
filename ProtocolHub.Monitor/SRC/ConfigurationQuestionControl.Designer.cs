namespace CAS.CommServerConsole
{
  partial class ConfigurationQuestionControl
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
      this.radioButton_primary = new System.Windows.Forms.RadioButton();
      this.radioButton_alternative = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.label_primary = new System.Windows.Forms.Label();
      this.label_alternative = new System.Windows.Forms.Label();
      this.checkBox_askquestion = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // radioButton_primary
      // 
      this.radioButton_primary.AutoSize = true;
      this.radioButton_primary.Location = new System.Drawing.Point( 9, 14 );
      this.radioButton_primary.Name = "radioButton_primary";
      this.radioButton_primary.Size = new System.Drawing.Size( 59, 17 );
      this.radioButton_primary.TabIndex = 0;
      this.radioButton_primary.TabStop = true;
      this.radioButton_primary.Text = "Primary";
      this.radioButton_primary.UseVisualStyleBackColor = true;
      // 
      // radioButton_alternative
      // 
      this.radioButton_alternative.AutoSize = true;
      this.radioButton_alternative.Location = new System.Drawing.Point( 9, 51 );
      this.radioButton_alternative.Name = "radioButton_alternative";
      this.radioButton_alternative.Size = new System.Drawing.Size( 78, 17 );
      this.radioButton_alternative.TabIndex = 1;
      this.radioButton_alternative.TabStop = true;
      this.radioButton_alternative.Text = "Alternative ";
      this.radioButton_alternative.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.label1.Location = new System.Drawing.Point( 8, 18 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 320, 13 );
      this.label1.TabIndex = 2;
      this.label1.Text = "What configuration do you want to use for connection?";
      // 
      // label_primary
      // 
      this.label_primary.AutoSize = true;
      this.label_primary.Location = new System.Drawing.Point( 11, 35 );
      this.label_primary.Name = "label_primary";
      this.label_primary.Size = new System.Drawing.Size( 68, 13 );
      this.label_primary.TabIndex = 3;
      this.label_primary.Text = "tcp://primary";
      // 
      // label_alternative
      // 
      this.label_alternative.AutoSize = true;
      this.label_alternative.Location = new System.Drawing.Point( 11, 71 );
      this.label_alternative.Name = "label_alternative";
      this.label_alternative.Size = new System.Drawing.Size( 84, 13 );
      this.label_alternative.TabIndex = 3;
      this.label_alternative.Text = "tcp://alternative";
      // 
      // checkBox_askquestion
      // 
      this.checkBox_askquestion.AutoSize = true;
      this.checkBox_askquestion.Location = new System.Drawing.Point( 11, 180 );
      this.checkBox_askquestion.Name = "checkBox_askquestion";
      this.checkBox_askquestion.Size = new System.Drawing.Size( 172, 17 );
      this.checkBox_askquestion.TabIndex = 4;
      this.checkBox_askquestion.Text = "Display this question at startup.";
      this.checkBox_askquestion.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.label2.Location = new System.Drawing.Point( 125, 16 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 244, 13 );
      this.label2.TabIndex = 5;
      this.label2.Text = " (Default for CommServer OPC DA Server)";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.label3.Location = new System.Drawing.Point( 82, 51 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size( 287, 13 );
      this.label3.TabIndex = 6;
      this.label3.Text = "(Default for CommServer as OPC UA DataSource)";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add( this.label3 );
      this.groupBox1.Controls.Add( this.label2 );
      this.groupBox1.Controls.Add( this.label_alternative );
      this.groupBox1.Controls.Add( this.label_primary );
      this.groupBox1.Controls.Add( this.radioButton_alternative );
      this.groupBox1.Controls.Add( this.radioButton_primary );
      this.groupBox1.Location = new System.Drawing.Point( 11, 54 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size( 373, 95 );
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      // 
      // ConfigurationQuestionControl
      // 
      this.Controls.Add( this.groupBox1 );
      this.Controls.Add( this.checkBox_askquestion );
      this.Controls.Add( this.label1 );
      this.Name = "ConfigurationQuestionControl";
      this.Size = new System.Drawing.Size( 395, 209 );
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton radioButton_primary;
    private System.Windows.Forms.RadioButton radioButton_alternative;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label_primary;
    private System.Windows.Forms.Label label_alternative;
    private System.Windows.Forms.CheckBox checkBox_askquestion;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox1;
  }
}
