using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VisualStudio2017.Domain.DataAccess;

namespace VisualStudio2017.Domain.Tests.DataAccess
{
	[TestClass]
	public class HttpSessionDataAccessTest
    {
		private HttpSessionDataAccess _dataAccess;
		private Mock<ISessionWrapper> _session;

		[TestInitialize]
		public void Startup()
		{
			_session = new Mock<ISessionWrapper>();
			_dataAccess = new HttpSessionDataAccess(_session.Object);
		}

		[TestMethod]
		public void GetAll_ShouldReturnEmpty_WhenSessionNull()
		{
			string nu11 = null;

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(nu11);

			Assert.IsTrue(_dataAccess.GetAll() != null);
			Assert.IsTrue(_dataAccess.GetAll().Count == 0);
		}
    }
}
