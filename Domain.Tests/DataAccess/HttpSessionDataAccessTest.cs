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
		private Mock<ISession> _session;

		[TestInitialize]
		public void Startup()
		{
			Mock<IHttpContextAccessor> accessor = new Mock<IHttpContextAccessor>();
			Mock<HttpContext> context = new Mock<HttpContext>();
			_session = new Mock<ISession>();

			accessor.Setup(a => a.HttpContext).Returns(() => context.Object);
			context.Setup(c => c.Session).Returns(() => _session.Object);

			_dataAccess = new HttpSessionDataAccess(accessor.Object);
		}

		[TestMethod]
		public void GetAll_ShouldReturnEmpty_WhenSessionNull()
		{
			string nu11 = null;

			_session.Setup(s => s.GetString(HttpSessionDataAccess.ITEMS_KEY)).Returns(nu11);

			Assert.IsTrue(_dataAccess.GetAll() == null);
		}
    }
}
