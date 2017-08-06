using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CAS.CommServer.DA.Server.NETServer.UnitTest
{
  [TestClass]
  [DeploymentItem("DefaultConfig.xml")]
  [DeploymentItem("item_dsc.xml")]
  public class Deployment
  {
    [Microsoft.VisualStudio.TestTools.UnitTesting.AssemblyInitialize]
    public static void AssemblyInitialize(TestContext _context)
    {
      if (!File.Exists("DefaultConfig.xml"))
        throw new ApplicationException("Cannot copy DefaultConfig.xml for ynit test");
      if (!File.Exists("item_dsc.xml"))
        throw new ApplicationException("Cannot copy item_dsc.xml for ynit test");
    }
  }
}
