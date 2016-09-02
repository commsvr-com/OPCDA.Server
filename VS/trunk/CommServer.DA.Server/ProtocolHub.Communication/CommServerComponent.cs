//<summary>
//_______________________________________________________________
//  Title   : CommServer main component
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

using CAS.CommServer.ProtocolHub.Communication.BaseStation;
using CAS.Lib.CodeProtect;
using CAS.Lib.CodeProtect.LicenseDsc;
using CAS.Lib.CodeProtect.Properties;
using CAS.Lib.RTLib.Processes;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CAS.CommServer.ProtocolHub.Communication
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
    private static Stopwatch m_RuntimeStopWatch = new Stopwatch();
    private static System.Timers.Timer m_RunTimeout;
    private void m_RunTimeout_Elapsed( object sender, System.Timers.ElapsedEventArgs e )
    {
      EventLogMonitor.WriteToEventLog
       ( "Runtime expired – server entered demo mode – no data will be read. ",
       System.Diagnostics.EventLogEntryType.Warning, (int)Error.CommServer_CommServerComponent, 72
       );
      Segment.DemoMode = true;
    }
    private static TraceEvent m_traceEvent_internal = new TraceEvent( "CAS.Lib.CommServer" );
    #endregion

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
      m_traceEvent_internal.TraceEventClose();
      base.Dispose( disposing );
    }
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
        return m_traceEvent_internal;
      }
    }
    /// <summary>
    /// Provides time the server is up in seconds
    /// </summary>
    internal static uint RunTime
    {
      get
      {
        return Stopwatch.ConvertTo_s( m_RuntimeStopWatch.Read );
      }
    }
    /// <summary>
    /// Provides name of the source to be used while instating to register it the EventLog engine.
    /// </summary>
    internal static string Source
    {
      get { return Assembly.GetExecutingAssembly().GetName().Name; }
    }
    /// <summary>
    /// Adds components to components container.
    /// </summary>
    /// <param name="component">Components to be added.</param>
    internal void AddComponent( Component component) { components.Add(component); }
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
    /// <param name="configurationFileName">The configuration file name.</param>
    public void Initialize( string configurationFileName )
    {
      if ( m_isInitialized )
        throw new Exception( "Only one initialization of CommServerComponent is allowed." );
      m_isInitialized = true;
      int cEventID = (int)Error.CommServer_CommServerComponent;
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
        System.Diagnostics.EventLogEntryType.Information, (int)Error.CommServer_CommServerComponent, 130 );
      Initialization.InitializeServer( this, m_DemoVer, ref cVcounter, configurationFileName );
      ConsoleIterface.Start( cProductName, cProductVersion );
      if ( cVcounter <= 0 )
        EventLogMonitor.WriteToEventLog
        ( "Some tags have not been added due to license limitation – the volume constrain have been reached",
           System.Diagnostics.EventLogEntryType.Warning, (int)Error.CommServer_CommServerComponent, 134 );
      else
      {
        string msg = string.Format
          ( "Initiated {0} tags, The license allows you to add {1} more tags. ", cVConstrain - cVcounter, cVcounter );
        EventLogMonitor.WriteToEventLog
         ( msg, System.Diagnostics.EventLogEntryType.Information, (int)Error.CommServer_CommServerComponent, 139 );
      }
      if ( cRTConstrain > 0 )
      {
        string msg = string.Format( "Runtime of the product is constrained up to {0} hours.", cRTConstrain );
        EventLogMonitor.WriteToEventLog
         ( msg, System.Diagnostics.EventLogEntryType.Warning, (int)Error.CommServer_CommServerComponent, 145 );
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
