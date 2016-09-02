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

namespace CAS.CommServer.ProtocolHub.Communication
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
