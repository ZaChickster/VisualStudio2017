using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
		public void GetAll_ShouldReturnOrderedList_ByWhenDue()
		{
			string hydrated = @"[
				{
					Id: 1,
					Name: 'n1',
					WhenDue: '2017-04-01'
				},
				{
					Id: 2,
					Name: 'n3',
					WhenDue: '2017-04-03'
				},
				{
					Id: 3,
					Name: 'n2',
					WhenDue: '2017-04-02'	
				}
			]";

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(hydrated);

			Assert.IsTrue(_dataAccess.GetAll() != null);
			Assert.IsTrue(_dataAccess.GetAll().Count == 3);
			Assert.IsTrue(_dataAccess.GetAll()[0].Name == "n1");
			Assert.IsTrue(_dataAccess.GetAll()[1].Name == "n2");
			Assert.IsTrue(_dataAccess.GetAll()[2].Name == "n3");
		}

		[TestMethod]
		public void GetOne_ShouldReturnItem_WhenItemExists()
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
		public void GetOne_ShouldReturnNull_WhenItemMissing()
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

		[TestMethod]
		public void Add_ShouldUseMaxId_WhenAdding()
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
			WorkItem item = _dataAccess.Add(new WorkItem { Name = "n4" });

			Assert.IsTrue(item.Id == 4);
		}

		[TestMethod]
		public void Add_ShouldAddToList_WhenAddingFirst()
		{
			string hydrated = @"[]";

			FakeSession fake = new FakeSession();
			fake.Set(HttpSessionDataAccess.ITEMS_KEY, hydrated);
			_dataAccess = new HttpSessionDataAccess(fake);

			WorkItem item = _dataAccess.Add(new WorkItem { Name = "n1" });

			Assert.IsTrue(_dataAccess.GetAll().Count == 1);
			Assert.IsTrue(_dataAccess.GetOne(1).Name == "n1");
		}

		[TestMethod]
		public void Add_ShouldAddToList_WhenAdding()
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

			FakeSession fake = new FakeSession();
			fake.Set(HttpSessionDataAccess.ITEMS_KEY, hydrated);
			_dataAccess = new HttpSessionDataAccess(fake);

			WorkItem item = _dataAccess.Add(new WorkItem { Name = "n4" });

			Assert.IsTrue(_dataAccess.GetAll().Count == 4);
			Assert.IsTrue(_dataAccess.GetOne(4).Name == "n4");
		}

		[TestMethod]
		public void Add_ShouldUseMaxId_WhenAddingOutOfSync()
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
					Id: 14,
					Name: 'n14'	
				}
			]";

			_session.Setup(s => s.Get(HttpSessionDataAccess.ITEMS_KEY)).Returns(hydrated);
			WorkItem item = _dataAccess.Add(new WorkItem { Name = "n15" });

			Assert.IsTrue(item.Id == 15);
		}

		[TestMethod]
		public void Update_ShouldReplaceItem_WhenCalled()
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

			FakeSession fake = new FakeSession();
			fake.Set(HttpSessionDataAccess.ITEMS_KEY, hydrated);
			_dataAccess = new HttpSessionDataAccess(fake);

			WorkItem item = _dataAccess.Update(new WorkItem { Id = 2, Name = "Hello, I've changed" });

			Assert.IsTrue(_dataAccess.GetAll().Count == 3);
			Assert.IsTrue(_dataAccess.GetOne(2).Name == "Hello, I've changed");
		}

		[TestMethod]
		public void Delete_ShouldRemoveItem_WhenCalled()
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

			FakeSession fake = new FakeSession();
			fake.Set(HttpSessionDataAccess.ITEMS_KEY, hydrated);
			_dataAccess = new HttpSessionDataAccess(fake);

			WorkItem item = _dataAccess.Delete(2);

			Assert.IsTrue(_dataAccess.GetAll().Count == 2);
		}
	}
}
