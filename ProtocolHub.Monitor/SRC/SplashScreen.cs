//<summary>
//  Title   : Splash scree window
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    2006 Mzbrzezny - created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>


namespace CAS.CommServerConsole
{
  /// <summary>
  /// Summary description for SplashScreen.
  /// </summary>
  public class SplashScreen: System.Windows.Forms.Form
  {
    private System.Windows.Forms.PictureBox pictureBox1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;
    internal SplashScreen()
    {
      InitializeComponent();
    }
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if ( disposing )
      {
        if ( components != null )
        {
          components.Dispose();
        }
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( SplashScreen ) );
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ( (System.Drawing.Image)( resources.GetObject( "pictureBox1.Image" ) ) );
      this.pictureBox1.Location = new System.Drawing.Point( 0, 0 );
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size( 448, 317 );
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // SplashScreen
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.ClientSize = new System.Drawing.Size( 448, 317 );
      this.Controls.Add( this.pictureBox1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "SplashScreen";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SplashScreen";
      ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
      this.ResumeLayout( false );

    }
    #endregion
  }
}
