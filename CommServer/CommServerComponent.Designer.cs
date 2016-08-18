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
//      Utworzy³em Component z klasy MainForm w pliku Main.cs
//    MPostol - 28-10-2006
//      removed Form reference, used reflection instead
//    Maciej Zbrzezny - 12-04-2006
//      usunieto okno aplickacji !!
//    Mariusz Postol - 11-03-04
//      zsnchronizowalem dostêp do obiektu przez threds'y wywoluj¹ce events do zmiany stanu.
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

namespace CAS.Lib.CommServer
{
  partial class CommServerComponent
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.m_CommonBusControl = new CAS.Lib.CommonBus.CommonBusControl( this.components );

    }
    #endregion
    internal CAS.Lib.CommonBus.CommonBusControl m_CommonBusControl;
  }
}
