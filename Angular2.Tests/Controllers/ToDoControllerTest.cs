using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VisualStudio2017.Angular2.Controllers;
using VisualStudio2017.Domain;
using VisualStudio2017.Domain.DataAccess;

namespace VisualStudio2017.Angular2.Tests.Controllers
{
	[TestClass]
	public class ToDoControllerTest
    {
		private Mock<IAppDataAccess> _dataAccess;
		private ToDoController _controller;

		[TestInitialize]
		public void Startup()
		{
			_dataAccess = new Mock<IAppDataAccess>();
			_controller = new ToDoController(_dataAccess.Object);
		}

		[TestMethod]
		public void ShouldCreateNewWorkItem_When_IdIsNull()
		{
			WorkItem item = _controller.GetOne(null);

			Assert.IsTrue(item != null);
			Assert.IsTrue(item.Id == 0);
		}

		[TestMethod]
		public void ShouldGetExistingWorkItem_When_GivenId()
		{
			_dataAccess.Setup(da => da.GetOne(15)).Returns(new WorkItem { Id = 15, Name = "Fifteen" });

			WorkItem item = _controller.GetOne(15);

			Assert.IsTrue(item != null);
			Assert.IsTrue(item.Id == 15);
			Assert.IsTrue(item.Name == "Fifteen");
		}
	}
}
