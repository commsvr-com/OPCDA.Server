//_______________________________________________________________
//  Title   : Unit tests for retry filters
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

using CAS.CommServer.ProtocolHub.Communication.BaseStation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CAS.CommServer.ProtocolHub.CommunicationUnitTests
{
  [TestClass]
  public class TestRetryFilter
  {
    private RetryFilter retryFilter = new RetryFilter(5);

    [TestMethod]
    public void RetryFilterTestMethod()
    {
      Assert.AreEqual(retryFilter.Retry, 5, "Starting value check");
      Assert.AreEqual(retryFilter.Quality, 100.0);
      for (int i = 0; i < 10; i++)
        retryFilter.MarkSuccess();
      Assert.AreEqual(retryFilter.Retry, 5, "Starting value check");
      Assert.AreEqual(retryFilter.Quality, 100.0);
      for (int i = 0; i < 10; i++)
        retryFilter.MarkFail();
      Assert.AreEqual(1, retryFilter.Retry, "Fail value check");
      Assert.IsTrue(retryFilter.Quality <= 10.0);
      Assert.IsTrue(retryFilter.Quality >= 0.0);
      Debug.WriteLine("Quality fail = {0}", retryFilter.Quality);
      for (int i = 0; i < 10; i++)
      {
        retryFilter.MarkFail();
        retryFilter.MarkSuccess();
      }
      Assert.AreEqual(5, retryFilter.Retry, "Success value check");
      Assert.IsTrue(retryFilter.Quality <= 60.0);
      Assert.IsTrue(retryFilter.Quality >= 40.0);
      Debug.WriteLine("Quality poor = {0}", retryFilter.Quality);

    }
  }
}
