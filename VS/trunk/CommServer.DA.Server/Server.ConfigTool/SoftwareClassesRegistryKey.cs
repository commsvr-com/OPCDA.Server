
using Microsoft.Win32;
using System;

namespace CAS.CommServer.DA.Server.ConfigTool
{
  internal class SoftwareClassesRegistryKey : IDisposable
  {
    public SoftwareClassesRegistryKey(Guid clsid)
    {
      KeyName = GetKeyName(clsid);
      foreach (RegistryView _view in Enum.GetValues(typeof(RegistryView)))
      {
        if (_view == RegistryView.Default)
          continue;
        RegistryView = _view;
        m_RegistryKey = OpenSubKey(_view);
        if (m_RegistryKey != null)
          return;
      }
      throw new ArgumentOutOfRangeException(nameof(clsid), $"Cannot find entry of {clsid} in the system registry");
    }
    public SoftwareClassesRegistryKey(Guid clsid, RegistryView view)
    {
      KeyName = GetKeyName(clsid);
      RegistryView = view;
      m_RegistryKey = OpenSubKey(RegistryView);
      if (m_RegistryKey != null)
        return;
      throw new ArgumentOutOfRangeException(nameof(clsid), $"Cannot find entry of {clsid} in the system registry");
    }
    internal string ProgIDFromCLSID()
    {
      using (RegistryKey _ProgIdKey = m_RegistryKey.OpenSubKey("ProgId", false))
        return _ProgIdKey == null ? "ProgId not set" : (string)_ProgIdKey.GetValue("");
    }
    internal Tuple<string, ServerType>  GetExecutablePath()
    {
      using (RegistryKey _key = m_RegistryKey.OpenSubKey("LocalServer32", false))
        if (_key != null)
          return new Tuple<string, ServerType> ((string)_key.GetValue("Codebase"), ServerType.LocalServer32);
      using (RegistryKey _key = m_RegistryKey.OpenSubKey("InprocServer32", false))
        if (_key != null)
          return new Tuple<string, ServerType>((string)_key.GetValue("Codebase"), ServerType.InprocServer32);
      return null;
    }
    internal string KeyName { get; private set; }
    internal RegistryView RegistryView { get; private set; }
    internal enum  ServerType{ LocalServer32, InprocServer32 }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
          m_RegistryKey.Close();
        disposedValue = true;
      }
    }
    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion

    #region private
    private RegistryKey m_RegistryKey;
    private string GetKeyName(Guid clsid)
    {
      return $@"Software\Classes\CLSID\{{{clsid}}}";
    }
    private RegistryKey OpenSubKey(RegistryView view)
    {
      using (RegistryKey _localMachineKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
        return _localMachineKey.OpenSubKey(KeyName, false);
    }
    #endregion
  }
}
