using Microsoft.Win32;
using System;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  internal static class Win64RegistryUtilities
  {
    internal static Tuple<string, RegistryView> ProgIDFromCLSID(this Guid clsid)
    {
      string _keyName = $@"Software\Classes\CLSID\{{{clsid}}}";
      foreach (RegistryView _view in Enum.GetValues(typeof(RegistryView)))
      {
        if (_view == RegistryView.Default)
          continue;
        using (RegistryKey _localMachineKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, _view))
        {
          using (RegistryKey _CLSIDKey = _localMachineKey.OpenSubKey(_keyName, false))
          {
            if (_CLSIDKey == null)
              continue;
            using (RegistryKey _ProgIdKey = _CLSIDKey.OpenSubKey("ProgId", false))
            {
              string _progId = _ProgIdKey == null ? "ProgId not set" : (string)_ProgIdKey.GetValue("");
              return new Tuple<string, RegistryView>(_progId, _view);
            }
          }
        }
      }
      return null; ;
    }
    /// <summary>
    /// Returns the location of the COM server executable.
    /// </summary>
    internal static string GetExecutablePath(this Guid clsid)
    {
      RegistryKey key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\LocalServer32", clsid));
      if (key == null)
      {
        key = Registry.ClassesRoot.OpenSubKey(String.Format(@"CLSID\{{{0}}}\InprocServer32", clsid));
      }

      if (key != null)
      {
        try
        {
          string codebase = key.GetValue("Codebase") as string;

          if (codebase == null)
          {
            return key.GetValue(null) as string;
          }

          return codebase;
        }
        finally
        {
          key.Close();
        }
      }

      return null;
    }

  }
}
