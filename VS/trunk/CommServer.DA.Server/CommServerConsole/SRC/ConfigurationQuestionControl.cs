using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CAS.CommServerConsole.Properties;

namespace CAS.CommServerConsole
{
  public partial class ConfigurationQuestionControl: UserControl
  {
    public ConfigurationQuestionControl()
    {
      InitializeComponent();
      this.checkBox_askquestion.Checked = Settings.Default.DisplayConfigurationQuestionAtStartup;
      this.radioButton_primary.Checked = !Settings.Default.UseAlternativeConfiguration;
      this.radioButton_alternative.Checked = Settings.Default.UseAlternativeConfiguration;
      this.label_primary.Text = String.Format( Settings.Default.CommServer_Connection_Template,
        Settings.Default.CommServer_Host_Primary, Settings.Default.CommServer_ListenPort_Primary );
      this.label_alternative.Text = String.Format( Settings.Default.CommServer_Connection_Template,
        Settings.Default.CommServer_Host_AlternativeConfiguration, Settings.Default.CommServer_ListenPort_AlternativeConfiguration );
    }
    internal bool UseAlternativeConfiguration
    {
      get
      {
        return this.radioButton_alternative.Checked;
      }
    }
    internal bool DisplayConfigurationQuestionAtStartup
    {
      get
      {
        return this.checkBox_askquestion.Checked;
      }
    }

  }
}
