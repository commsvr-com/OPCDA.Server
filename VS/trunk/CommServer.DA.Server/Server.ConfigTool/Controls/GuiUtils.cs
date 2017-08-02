//_______________________________________________________________
//  Title   : GuiUtils
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate:  $
//  $Rev: $
//  $LastChangedBy: $
//  $URL: $
//  $Id:  $
//
//  Copyright (C) 2017, CAS LODZ POLAND.
//  TEL: +48 608 61 98 99 
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using System;
using System.Windows.Forms;
using System.Reflection;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  /// <summary>
  /// A class that provide various common utility functions and shared resources.
  /// </summary>
  public partial class GuiUtils : UserControl
  {
    public GuiUtils()
    {
      InitializeComponent();
    }
    /// <summary>
    /// Displays the details of an exception.
    /// </summary>
    public static void HandleException(string caption, MethodBase method, Exception e)
    {
      if (String.IsNullOrEmpty(caption))
        caption = method.Name;
      new ExceptionDlg().ShowDialog(caption, e);
    }
  }
}
