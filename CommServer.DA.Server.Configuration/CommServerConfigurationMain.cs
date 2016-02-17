//<summary>
//  Title   : CommServer configuration management main entry point.
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2009, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CAS.NetworkConfigLib.Properties;
using CAS.Lib.CodeProtect;

namespace CAS.NetworkConfigLib
{
  /// <summary>
  /// CommServer configuration management main entry point.
  /// </summary>
  public partial class CommServerConfigurationMain: Component
  {
    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="CommServerConfigurationMain"/> class that is a main 
    /// entry point to use and manage the configuration file.
    /// </summary>
    public CommServerConfigurationMain()
    {
      InitializeComponent();
      InitializeThis();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="CommServerConfigurationMain"/> class that is a main 
    /// entry point to use and manage the configuration file.
    /// </summary>
    /// <param name="container">The container to add thic component.</param>
    public CommServerConfigurationMain( IContainer container )
      : this()
    {
      container.Add( this );
    }
    #endregion

    #region public
    /// <summary>
    /// Gets the configuartion.
    /// </summary>
    /// <value>The configuartion <see cref="ComunicationNet"/>.</value>
    public ComunicationNet Configuartion { get { return m_CcomunicationNet; } }
    /// <summary>
    /// Gets the menu.
    /// </summary>
    /// <value>The menu <see cref="ContextMenuStrip"/>.</value>
    public ToolStripItem[] Menu
    {
      get
      {
        ToolStripItem[] ret = new ToolStripItem[ 4 ];
        ret[ 0 ] = new ToolStripMenuItem
          ( m_TSMI_New.Text, m_TSMI_New.Image, new EventHandler( m_TSMI_New_Click ), "Configuration New" ) { ToolTipText = m_TSMI_New.ToolTipText };
        ret[ 1 ] = new ToolStripMenuItem
          ( m_TSMI_Open.Text, m_TSMI_Open.Image, new EventHandler( OnOpen_Click ), "Configuration Open" ) { ToolTipText = m_TSMI_Open.ToolTipText };
        ret[ 2 ] = new ToolStripMenuItem
          ( m_TSMI_Save.Text, m_TSMI_Save.Image, new EventHandler( OnSave_Click ), "Configuration Save" ) { ToolTipText = m_TSMI_Save.ToolTipText };
        ret[ 3 ] = new ToolStripMenuItem
          ( m_TSMI_SaveAs.Text, m_TSMI_SaveAs.Image, new EventHandler( OnSaveAs_Click ), "Configuration Save As" ) { ToolTipText = m_TSMI_SaveAs.ToolTipText };
        return ret;
      }
    }
    /// <summary>
    /// Specialized Event Argument <see cref="EventArgs"/> sent as parameter to events
    /// </summary>
    public class ConfigurationEventArg: EventArgs
    {
      /// <summary>
      /// Gets or sets the configuration.
      /// </summary>
      /// <value>The configuration.</value>
      public ComunicationNet Configuration { get; private set; }
      /// <summary>
      /// Initializes a new instance of the <see cref="ConfigurationEventArg"/> class.
      /// </summary>
      /// <param name="config">The config.</param>
      public ConfigurationEventArg( ComunicationNet config )
      {
        Configuration = config;
      }
    }
    /// <summary>
    /// Occurs when configuration has been chnged.
    /// </summary>
    public event EventHandler<ConfigurationEventArg> ConfigurationChnged;
    /// <summary>
    /// Is called before configuration saving.
    /// </summary>
    public EventHandler<ConfigurationEventArg> ConfigurationSaving;
    /// <summary>
    /// Gets or sets the default name of the file.
    /// </summary>
    /// <value>The default name of the file.</value>
    public string DefaultFileName
    {
      set
      {
        m_OpenFileDialog.FileName = value;
        m_Empty = true;
      }
      get { return m_OpenFileDialog.FileName; }
    }
    /// <summary>
    /// Read the CommServer configuration from an external dictionary file. If file name not set it opens the file open
    /// dialog to choose the file.
    /// </summary>
    /// <returns></returns>
    public bool Open()
    {
      return Open( string.Empty );
    }
    /// <summary>
    /// Opens the specified file name and reads the CommServer configuration from an external dictionary file. 
    /// If <paramref name="FileName"/> parameter is empty, it opens the file dialog to choose the file
    /// </summary>
    /// <param name="FileName">Name of the file with the CommServer configuration.</param>
    /// <returns><c>true</c> if successfully accomplished</returns>
    public bool Open( string FileName )
    {
      if ( !string.IsNullOrEmpty( FileName ) )
      {
        m_OpenFileDialog.FileName = FileName;
      }
      else
      {
        if ( m_OpenFileDialog.ShowDialog() != DialogResult.OK )
          return false;
      }
      Cursor myPreviousCursor = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        Application.UseWaitCursor = true;
        ReadConfiguration( m_OpenFileDialog.FileName );
        m_Empty = false;
        UpdateCurrentDirectoryInConfigurationFile( m_OpenFileDialog );
        return true;
      }
      catch ( Exception ex )
      {
        m_OpenFileDialog.FileName = "";
        MessageBox.Show
          ( ex.Message, Properties.Resources.SessionFileOpenError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        return false;
      }
      finally
      {
        Application.UseWaitCursor = false;
        Cursor.Current = myPreviousCursor;
      }
    }
    /// <summary>
    /// Reads the configuration.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the file, or the relative file name.</param>
    /// <exception cref="FileNotFoundException">The exception that is thrown when an attempt to access a file 
    /// that does not exist or read operation fails.
    /// </exception>
    /// <exception cref="System.Security.SecurityException">System.Security.Permissions.FileIOPermission is not 
    /// set to System.Security.Permissions.FileIOPermissionAccess.Read.</exception>
    /// <exception cref="System.Data.ConstraintException">One or more constraints cannot be enforced.</exception>
    public void ReadConfiguration( string fileName )
    {
      FileInfo info = new FileInfo( fileName );
      if ( !info.Exists )
        throw new FileNotFoundException( fileName );
      m_CcomunicationNet.Clear();
      m_CcomunicationNet.EnforceConstraints = false;
      m_CcomunicationNet.ReadXml( fileName, System.Data.XmlReadMode.IgnoreSchema );
      m_CcomunicationNet.EnforceConstraints = true;
      m_SaveFileDialog.FileName = m_OpenFileDialog.FileName = fileName;
      RaiseConfigurationChnged();
    }
    /// <summary>
    /// Save the CommServer configuration file in an external dictionary file. 
    /// </summary>
    /// <remarks>It calls the ConfigurationSaving delegate to allow user to update the configuration and flush all changes 
    /// just before file saving.</remarks>
    /// <param name="prompt">If set to <c>true</c> show prompt to enter a file name.</param>
    /// <returns></returns>
    public bool Save( bool prompt )
    {
      if ( ConfigurationSaving != null )
        ConfigurationSaving( this, new ConfigurationEventArg( m_CcomunicationNet ) );
      prompt = m_Empty || prompt;
      if ( prompt )
      {
        m_SaveFileDialog.FileName = string.IsNullOrEmpty( DefaultFileName ) ? "CommServerConfiguration" : DefaultFileName;
        if ( m_SaveFileDialog.ShowDialog() != DialogResult.OK )
          return false;
        m_Empty = false;
      }
      Cursor myPreviousCursor = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        Application.UseWaitCursor = true;
        m_CcomunicationNet.WriteXml( m_SaveFileDialog.FileName, System.Data.XmlWriteMode.WriteSchema );
      }
      catch ( Exception ex )
      {
        MessageBox.Show
          ( ex.Message, Properties.Resources.SessionFileSaveError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
      }
      finally
      {
        Application.UseWaitCursor = false;
        Cursor.Current = myPreviousCursor;
        UpdateCurrentDirectoryInConfigurationFile( m_SaveFileDialog );
      }
      return true;
    }
    /// <summary>
    /// Gets the open file dialog - can be used to copy all properties.
    /// </summary>
    /// <value>The open file dialog.</value>
    public OpenFileDialog OpenFileDialog { get { return m_OpenFileDialog; } }
    /// <summary>
    /// Returns a <see cref="T:System.String"/> containing the name of the <see cref="T:System.ComponentModel.Component"/>, if any. This method should not be overridden.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> containing the name of the <see cref="T:System.ComponentModel.Component"/>, if any, or null if the <see cref="T:System.ComponentModel.Component"/> is unnamed.
    /// </returns>
    public override string ToString()
    {
      return "CommServer configuration management";
    }
    #endregion

    #region private
    private void InitializeThis()
    {
      m_TSMI_Open.Click += new EventHandler( OnOpen_Click );
      m_TSMI_Save.Click += new EventHandler( OnSave_Click );
      m_TSMI_SaveAs.Click += new EventHandler( OnSaveAs_Click );
      m_TSMI_New.Click += new EventHandler( m_TSMI_New_Click );
      if ( string.IsNullOrEmpty( Settings.Default.InitialDirectory ) )
        Settings.Default.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
      m_OpenFileDialog.InitialDirectory = InstallContextNames.ApplicationDataPath;
      m_SaveFileDialog.InitialDirectory = InstallContextNames.ApplicationDataPath;
    }
    private bool m_Empty = true;
    private void UpdateCurrentDirectoryInConfigurationFile( FileDialog fileDialog )
    {
      try
      {
        Settings.Default.InitialDirectory = fileDialog.InitialDirectory;
        if ( !string.IsNullOrEmpty( fileDialog.FileName ) )
        {
          FileInfo fi = new FileInfo( fileDialog.FileName );
          if ( fi.Exists )
          {
            Settings.Default.InitialDirectory = fi.DirectoryName;
            m_OpenFileDialog.FileName = fi.FullName;
            m_SaveFileDialog.FileName = fi.FullName;
          }
        }
        m_OpenFileDialog.InitialDirectory = InstallContextNames.ApplicationDataPath;
        m_SaveFileDialog.InitialDirectory = InstallContextNames.ApplicationDataPath;
      }
      catch { }
    }
    private void RaiseConfigurationChnged()
    {
      if ( ConfigurationChnged != null )
        ConfigurationChnged( this, new ConfigurationEventArg( m_CcomunicationNet ) );
    }

    #region private menu handlers
    private void m_TSMI_New_Click( object sender, EventArgs e )
    {
      m_CcomunicationNet.Clear();
      m_Empty = true;
      RaiseConfigurationChnged();
    }
    /// <summary>
    /// Called when SaveAs was clicked].
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void OnSaveAs_Click( object sender, EventArgs e )
    {
      Save( true );
    }
    /// <summary>
    /// Called when Save was clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void OnSave_Click( object sender, EventArgs e )
    {
      Save( false );
    }
    /// <summary>
    /// Called when Open was clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void OnOpen_Click( object sender, EventArgs e )
    {
      Open();
    }
    #endregion

    #endregion

  }
}
