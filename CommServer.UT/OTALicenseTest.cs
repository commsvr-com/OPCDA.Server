//<summary>
//  Title   : This is a test class for OTALicenseTest and is intended to contain all OTALicenseTest Unit Tests
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.CodeProtect;
using CAS.Lib.CommServer;
using CAS.Lib.CommServer.LicenseControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests
{
  /// <summary>
  ///This is a test class for OTALicenseTest and is intended
  ///to contain all OTALicenseTest Unit Tests
  ///</summary>
  [TestClass()]
  public class OTALicenseTest
  {
    private TestContext testContextInstance;
    private static CommServerComponent cs ;
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get { return testContextInstance; }
      set { testContextInstance = value; }
    }
    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    [ClassInitialize()]
    public static void MyClassInitialize( TestContext testContext )
    {
      cs = new CommServerComponent();
      cs.Initialize( "DefaultConfig.xml" );
      LibInstaller.InstalLicense();
    }
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    [TestCleanup()]
    public void MyTestCleanup()
    {
      cs.Dispose();
    }
    //
    #endregion
    /// <summary>
    ///A test for OTALicense Constructor
    ///</summary>
    [TestMethod()]
    [DeploymentItem( "CR.UT.CommServer\\CAS.License.lic" )]
    public void OTALicenseConstructorTest()
    {
      Assert.IsTrue( OTALicense.License.Licensed, "License error - see log" );
    }
    [TestMethod()]
    [ExpectedExceptionAttribute( typeof( System.ComponentModel.LicenseException ) )]
    public void MultichannelTest()
    {
      Assert.AreEqual( Multichannel.License.Volumen, 5 );
      Assert.AreEqual( Multichannel.License.RunTime.Value.TotalHours, 0 );
      for ( int i = 0; i < 5; i++ )
        Multichannel.NextChannnel();
      Multichannel.NextChannnel();
    }
    [TestMethod()]
    [ExpectedExceptionAttribute( typeof( System.ComponentModel.LicenseException ) )]
    public void RedundancyTest()
    {
      Assert.IsTrue( Redundancy.License.Licensed );
      Assert.AreEqual( Redundancy.License.Volumen, 2 );
      Redundancy.CheckIfAllowed( 2, "test name" );
    }
    [TestMethod()]
    public void ASALicenseTest()
    {
      Assert.IsTrue( ASALicense.License.Licensed, "License error - see log" );
    }
  }
}
