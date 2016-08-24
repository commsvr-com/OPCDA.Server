//_______________________________________________________________
//  Title   : ConfigurationQuestionControl
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using CAS.CommServerConsole.Properties;
using System;
using System.Windows.Forms;

namespace CAS.CommServerConsole
{
  /// <summary>
  /// Class ConfigurationQuestionControl.
  /// </summary>
  /// <seealso cref="System.Windows.Forms.UserControl" />
  public partial class ConfigurationQuestionControl: UserControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationQuestionControl"/> class.
    /// </summary>
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
