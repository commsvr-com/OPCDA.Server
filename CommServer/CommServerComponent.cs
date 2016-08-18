//<summary>
//  Title   : CommServer main component
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20080625: mzbrzezny: EventLogMonitor is used instead of System.Diagnostics.EventLog.
//                         The advantage is that EventLogMonitor can save messages through .NET trace
//                         so events can be stored in the log file automatically (depends on the app.config)
//    MPostol - 11-02-2007:
//      Utworzy�em Component z klasy MainForm w pliku Main.cs
//    MPostol - 28-10-2006
//      removed Form reference, used reflection instead
//    Maciej Zbrzezny - 12-04-2006
//      usunieto okno aplickacji !!
//    Mariusz Postol - 11-03-04
//      zsnchronizowalem dost�p do obiektu przez threds'y wywoluj�ce events do zmiany stanu.
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using CAS.Lib.CodeProtect;
using CAS.Lib.CodeProtect.LicenseDsc;
using CAS.Lib.RTLib.Processes;
using CAS.Lib.CodeProtect.Properties;

namespace CAS.Lib.CommServer
{
  /// <summary>
  /// CommServer main component - must be used as singleton
  /// </summary>
  [LicenseProvider( typeof( CodeProtectLP ) )]
  [GuidAttribute( "0F87D35C-B978-4d6c-BACF-DE0566A0DC51" )]
  public partial class CommServerComponent: Component
  {
    #region private
    private static bool m_isCreated = false;
    private static bool m_isInitialized = false;
    private static CAS.Lib.RTLib.Processes.Stopwatch m_RuntimeStopWatch = new CAS.Lib.RTLib.Processes.Stopwatch();
    private static System.Timers.Timer m_RunTimeout;
    private void m_RunTimeout_Elapsed( object sender, System.Timers.ElapsedEventArgs e )
    {
      EventLogMonitor.WriteToEventLog
       ( "Runtime expired � server entered demo mode � no data will be read. ",
       System.Diagnostics.EventLogEntryType.Warning, (int)CAS.Lib.RTLib.Processes.Error.CommServer_CommServerComponent, 72
       );
      CAS.Lib.CommServer.Segment.DemoMode = true;
    }
    private static TraceEvent m_traceevent_internal = new TraceEvent( "CAS.Lib.CommServer" );
    #region IDisposable
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
      m_traceevent_internal.TraceEventClose();
      base.Dispose( disposing );
    }
    #endregion
    #endregion
    #region public
    /// <summary>
    /// Gets the tracer.
    /// </summary>
    /// <value>The tracer.</value>
    internal static TraceEvent Tracer
    {
      get
      {
        return m_traceevent_internal;
      }
    }
    /// <summary>
    /// Provides time the server is up in seconds
    /// </summary>
    internal static uint RunTime
    {
      get
      {
        return CAS.Lib.RTLib.Processes.Stopwatch.ConvertTo_s( m_RuntimeStopWatch.Read );
      }
    }
    /// <summary>
    /// Provides name of the source to be used while instaling to register it the EventLog engine.
    /// </summary>
    internal static string Source
    {
      get { return Assembly.GetExecutingAssembly().GetName().Name; }
    }
    /// <summary>
    /// Adds components to components containter.
    /// </summary>
    /// <param name="cCmpnt">Components to be added.</param>
    internal void AddComponent( Component cCmpnt ) { components.Add( cCmpnt ); }
    /// <summary>
    /// CommServer main component creator
    /// </summary>
    public CommServerComponent()
    {
      InitializeComponent();
      if ( m_isCreated )
        throw new Exception( "Only one instance of CommServerComponent is allowed." );
      m_isCreated = true;
    }
    /// <summary>
    /// Initializes the Main CommServer Component using specified configuration file name.
    /// </summary>
    /// <param name="ConfigurationFileName">The configuration file name.</param>
    public void Initialize( string ConfigurationFileName )
    {
      if ( m_isInitialized )
        throw new Exception( "Only one initialisation of CommServerComponent is allowed." );
      m_isInitialized = true;
      int cEventID = (int)CAS.Lib.RTLib.Processes.Error.CommServer_CommServerComponent;
      bool m_DemoVer = true;
      int cRTConstrain = 2;
      int cVConstrain = 15;
      License lic = null;
      LicenseManager.IsValid( this.GetType(), this, out lic );
      LicenseFile m_license = lic as LicenseFile;
      if ( m_license == null )
        EventLogMonitor.WriteToEventLog( Resources.Tx_LicNoFileErr, System.Diagnostics.EventLogEntryType.Error, cEventID, 93 );
      else
        using ( lic )
        {
          MaintenanceControlComponent mcc = new MaintenanceControlComponent();
          if ( mcc.Warning != null )
            Tracer.TraceWarning( 143, this.GetType().Name, "The following warning(s) appeared during loading the license: " + mcc.Warning );
          if ( m_license.FailureReason != String.Empty )
            EventLogMonitor.WriteToEventLog( m_license.FailureReason, System.Diagnostics.EventLogEntryType.Error, cEventID, 95 );
          else
          {
            m_DemoVer = false;
            EventLogMonitor.WriteToEventLog
              ( "Opened the license: " + m_license.ToString(), System.Diagnostics.EventLogEntryType.Information, cEventID, 98 );
            cRTConstrain = m_license.RunTimeConstrain;
            if ( m_license.VolumeConstrain < 0 )
              cVConstrain = int.MaxValue;
            else
              cVConstrain = m_license.VolumeConstrain;
          }
        }
      if ( m_DemoVer )
        EventLogMonitor.WriteToEventLog( Resources.Tx_LicDemoModeInfo, System.Diagnostics.EventLogEntryType.Information, cEventID, 98 );
      string cProductName;
      string cProductVersion;
      string cFullName;
      cProductName = Assembly.GetExecutingAssembly().GetName().Name;
      cProductVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      cFullName = Assembly.GetExecutingAssembly().GetName().FullName;
      ulong vd = m_RuntimeStopWatch.Start;
      int cVcounter = cVConstrain;
      EventLogMonitor.WriteToEventLog
        ( "Communication server started - product name:" + cFullName,
        System.Diagnostics.EventLogEntryType.Information, (int)CAS.Lib.RTLib.Processes.Error.CommServer_CommServerComponent, 130 );
      BaseStation.Initialization.InitServer( this, m_DemoVer, ref cVcounter, ConfigurationFileName );
      BaseStation.ConsoleIterface.Start( cProductName, cProductVersion );
      if ( cVcounter <= 0 )
        EventLogMonitor.WriteToEventLog
        ( "Some tags have not been added due to license limitation � the volume constrain have been reached",
           System.Diagnostics.EventLogEntryType.Warning, (int)CAS.Lib.RTLib.Processes.Error.CommServer_CommServerComponent, 134 );
      else
      {
        string msg = string.Format
          ( "Initiated {0} tags, The license allows you to add {1} more tags. ", cVConstrain - cVcounter, cVcounter );
        EventLogMonitor.WriteToEventLog
         ( msg, System.Diagnostics.EventLogEntryType.Information, (int)CAS.Lib.RTLib.Processes.Error.CommServer_CommServerComponent, 139 );
      }
      if ( cRTConstrain > 0 )
      {
        string msg = string.Format( "Runtime of the product is constrained up to {0} hours.", cRTConstrain );
        EventLogMonitor.WriteToEventLog
         ( msg, System.Diagnostics.EventLogEntryType.Warning, (int)CAS.Lib.RTLib.Processes.Error.CommServer_CommServerComponent, 145 );
        m_RunTimeout = new System.Timers.Timer( cRTConstrain * 60 * 60 * 1000 );
        m_RunTimeout.Start();
        m_RunTimeout.Elapsed += new System.Timers.ElapsedEventHandler( m_RunTimeout_Elapsed );
      }
    }
    /// <summary>
    /// CommServer main component creator
    /// </summary>
    /// <param name="container">Container of the parent component if any.</param>
    public CommServerComponent( IContainer container )
      : this()
    {
      container.Add( this );
    }
    #endregion
  }
}
