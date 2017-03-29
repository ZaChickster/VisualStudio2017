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

		[TestMethod]
		public void GetAll_ShouldReturnEmpty_WhenSessionBlank()
		{
			string blank = "";

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(blank);

			Assert.IsTrue(_dataAccess.GetAll() != null);
			Assert.IsTrue(_dataAccess.GetAll().Count == 0);
		}

		[TestMethod]
		public void GetAll_ShouldReturnEmpty_WhenSessionEmpty()
		{
			string empty = "[]";

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(empty);

			Assert.IsTrue(_dataAccess.GetAll() != null);
			Assert.IsTrue(_dataAccess.GetAll().Count == 0);
		}

		[TestMethod]
		public void GetAll_ShouldReturnList_WhenSessionValueExists()
		{
			string hydrated = @"[
				{
					Id: 1,
					Name: 'n1'	
				},
				{
					Id: 2,
					Name: 'n2'	
				},
				{
					Id: 3,
					Name: 'n3'	
				}
			]";

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(hydrated);

			Assert.IsTrue(_dataAccess.GetAll() != null);
			Assert.IsTrue(_dataAccess.GetAll().Count == 3);
		}

		[TestMethod]
		public void GetAll_ShouldReturnItem_WhenItemExists()
		{
			string hydrated = @"[
				{
					Id: 1,
					Name: 'n1'	
				},
				{
					Id: 2,
					Name: 'n2'	
				},
				{
					Id: 3,
					Name: 'n3'	
				}
			]";

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(hydrated);
			WorkItem item = _dataAccess.GetOne(2);

			Assert.IsTrue(item != null);
			Assert.IsTrue(item.Name == "n2");
		}

		[TestMethod]
		public void GetAll_ShouldReturnNull_WhenItemMissing()
		{
			string hydrated = @"[
				{
					Id: 1,
					Name: 'n1'	
				},
				{
					Id: 2,
					Name: 'n2'	
				},
				{
					Id: 3,
					Name: 'n3'	
				}
			]";

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(hydrated);

			Assert.IsTrue(_dataAccess.GetOne(4) == null);
		}
	}
}
