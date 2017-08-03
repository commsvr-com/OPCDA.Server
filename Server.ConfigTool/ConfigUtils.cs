//_______________________________________________________________
//  Title   : ConfigUtilities
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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  /// <summary>
  /// Exposes WIN32 and COM API functions.
  /// </summary>
  public static class CommonDefinitions
  {
    #region NetApi Function Declarations
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct SERVER_INFO_100
    {
      public uint sv100_platform_id;
      [MarshalAs(UnmanagedType.LPWStr)]
      public string sv100_name;
    }
    private const uint LEVEL_SERVER_INFO_100 = 100;
    private const uint LEVEL_SERVER_INFO_101 = 101;
    private const int MAX_PREFERRED_LENGTH = -1;
    private const uint SV_TYPE_WORKSTATION = 0x00000001;
    private const uint SV_TYPE_SERVER = 0x00000002;

    [DllImport("Netapi32.dll")]
    private static extern int NetServerEnum(
        IntPtr servername,
        uint level,
        out IntPtr bufptr,
        int prefmaxlen,
        out int entriesread,
        out int totalentries,
        uint servertype,
        IntPtr domain,
        IntPtr resume_handle);

    /// <summary>
    /// The NetApiBufferFree function frees the memory that the NetApiBufferAllocate function allocates. Applications should also call NetApiBufferFree to free the memory that other network management functions use internally to return information.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>System.Int32.</returns>
    /// TODO Edit XML Comment Template for NetApiBufferFree
    [DllImport("Netapi32.dll")]
    private static extern int NetApiBufferFree(IntPtr buffer);
    /// <summary>
    /// Enumerates computers on the local network.
    /// </summary>
    public static string[] EnumComputers()
    {
      IntPtr _pInfo;
      int _entriesRead = 0;
      int _totalEntries = 0;
      int result = NetServerEnum(
          IntPtr.Zero,
          LEVEL_SERVER_INFO_100,
          out _pInfo,
          MAX_PREFERRED_LENGTH,
          out _entriesRead,
          out _totalEntries,
          SV_TYPE_WORKSTATION | SV_TYPE_SERVER,
          IntPtr.Zero,
          IntPtr.Zero);
      if (result != 0)
        throw new ApplicationException("NetApi Error = " + String.Format("0x{0:X8}", result));
      string[] computers = new string[_entriesRead];
      IntPtr pos = _pInfo;
      for (int ii = 0; ii < _entriesRead; ii++)
      {
        SERVER_INFO_100 info = (SERVER_INFO_100)Marshal.PtrToStructure(pos, typeof(SERVER_INFO_100));
        computers[ii] = info.sv100_name;
        pos = (IntPtr)(pos.ToInt32() + Marshal.SizeOf(typeof(SERVER_INFO_100)));
      }
      NetApiBufferFree(_pInfo);
      return computers;
    }
    #endregion

    #region Categories
    public const string ConfigToolSchemaUri = "http://opcfoundation.org/ConfigTool";
    public static readonly Guid CATID_DotNetOpcServers = new Guid("E633C3F5-7692-476c-9A35-8BEE25E5BA9D");  //it is not used
    public static readonly Guid CATID_DotNetOpcServerWrappers = new Guid("132B3E2B-0E92-4816-972B-E42AA9532529");
    public static readonly Guid CATID_RegisteredDotNetOpcServers = new Guid("62C8FE65-4EBB-45e7-B440-6E39B2CDBF29"); //It is added by regasm and registered at HKEY_CLASSES_ROOT\Component Categories\
    #endregion

    #region Extension

    /// <summary>
    /// Displays the contents of an exception.
    /// </summary>
    public static void HandleException(this Exception e, string caption, MethodBase method)
    {
      StringBuilder message = new StringBuilder();
      BuildMessage(message, e);
      MessageBox.Show(message.ToString(), caption);
    }
    /// <summary>
    /// Return true if the array contains no elements.
    /// </summary>
    public static bool IsEmpty(this Array array)
    {
      return (array == null || array.Length == 0);
    }
    #endregion

    #region private
    /// <summary>
    /// Builds a message showing the exception trace.
    /// </summary>
    private static void BuildMessage(StringBuilder message, Exception e)
    {
      if (e.InnerException != null)
        BuildMessage(message, e.InnerException);
      if (message.Length > 0)
        message.Append("\r\n\r\n");
      message.Append(">>> ");
      message.Append(e.Message);
      if (e.StackTrace != null)
      {
        string[] trace = e.StackTrace.Split(new char[] { '\r', '\n' });
        for (int ii = 0; ii < trace.Length; ii++)
          if (trace[ii] != null && trace[ii].Length > 0)
            message.AppendFormat("\r\n--- {0}", trace[ii]);
      }
    }
    #endregion

  }
}

