using CAS.CommServerConsole;
using System;
using System.Windows.Forms;

namespace CAS.CommServer.DA.Server.Monitor
{
  class Program
  {
    [MTAThread]
    public static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        Application.Run( new MainForm() );
      }
      catch (Exception _ex)
      {
        string _message = $"{Properties.Resources.Tx_InitCommError}, An exception has been thrown: {_ex.Message}";
        MessageBox.Show(_message, Properties.Resources.Tx_InitFailedCap, MessageBoxButtons.OK, MessageBoxIcon.Stop );
      }
    }
  }
}
