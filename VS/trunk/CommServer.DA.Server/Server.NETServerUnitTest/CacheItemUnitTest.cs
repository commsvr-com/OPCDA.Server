
using CAS.OpcSvr.Da.NETServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Server.Server.NETServerUnitTest
{
  [TestClass]
  public class CacheItemUnitTest
  {

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorEmptyItemIDTest()
    {
      CacheItem _newCacheItem = new CacheItem(string.Empty, 0, null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorNullDeviceTest()
    {
      CacheItem _newCacheItem = new CacheItem("itemID", 0, null);
    }

  }
}
