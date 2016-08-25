using System;
using System.Text;
using System.Windows.Forms;
namespace CAS.CommServerConsole
{
  class Program
  {
    [MTAThread]
    public static void Main()
    {
      try
      {
        Application.Run( new MainForm() );
      }
      catch
      {
        MessageBox.Show
          ( Properties.Resources.Tx_InitCommError, Properties.Resources.Tx_InitFailedCap, MessageBoxButtons.OK, MessageBoxIcon.Stop );
      }
    }
  }
}
