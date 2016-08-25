//_______________________________________________________________
//  Title   : Configuration management utilities
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


using CAS.Lib.RTLib.Database;
using CAS.Lib.RTLib.Processes;
using CAS.NetworkConfigLib;
using System;

namespace NetworkConfig
{
  /// <summary>
  /// Opens and reads OPC server configuration from XML file.
  /// </summary>
  internal class XMLManagement
  {

    #region PRIVATE
    private string filename;
    #endregion

    #region PUBLIC
    /// <summary>
    /// configuration 
    /// </summary>
    public readonly ComunicationNet configuration;
    /// <summary>
    /// Initializes a new instance of the <see cref="XMLManagement"/> class.
    /// </summary>
    /// <param name="ConfigurationFilename">The configuration filename.</param>
    public XMLManagement(string ConfigurationFilename)
      : this(ConfigurationFilename, false)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="XMLManagement"/> class.
    /// </summary>
    /// <param name="ConfigurationFilename">The configuration filename.</param>
    /// <param name="open_readonly">if set to <c>true</c> file is opened as readonly.</param>
    public XMLManagement(string ConfigurationFilename, bool open_readonly)
    {
      filename = ConfigurationFilename;
      try
      {
        configuration = new ComunicationNet();
        this.configuration.BeginInit();
        this.configuration.DataSetName = "ComunicationNet";
        this.configuration.Locale = new System.Globalization.CultureInfo("en-US");
        this.configuration.EndInit();
        XML2DataSetIO.readXMLFile(configuration, filename, open_readonly);
      }
      catch (System.IO.IOException ioex)
      {
        CAS.Lib.RTLib.Processes.EventLogMonitor.WriteToEventLogError(
          "Problem with CommServer configuration file:" + ioex.Message,
          (int)Error.CommServer_Configuration);
      }
      catch (Exception ex)
      {
        CAS.Lib.RTLib.Processes.EventLogMonitor.WriteToEventLogError(
          "Problem with CommServer XML configuration file :" + filename +
          " - in directory: " + CAS.Lib.RTLib.Win32API.Application.Path + " error: " + ex.ToString(),
          (int)Error.CommServer_Configuration);
      }
    }
    #endregion

  }
}
