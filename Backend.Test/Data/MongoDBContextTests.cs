using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisualStudio2017.Backend.Data;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.Backend.Test.Data
{
	[TestClass]
    public class MongoDBContextTests
    {
        [TestMethod]
        public void Should_GetCount_FromMongoDB()
        {
			MongoDBContext ctx = new MongoDBContext();

			Assert.IsTrue(ctx.GetCount().Result > 0);
        }

		[TestMethod]
		public void Should_GetRestaurants_FromMongoDB_FirstPage()
		{
			MongoDBContext ctx = new MongoDBContext();
			List<Restaurant> found = ctx.GetRestaurants(25, 0).Result;

			Assert.IsTrue(found.Count == 25);
		}

		[TestMethod]
		public void Should_GetRestaurants_FromMongoDB_LaterPage()
		{
			MongoDBContext ctx = new MongoDBContext();
			List<Restaurant> found = ctx.GetRestaurants(50, 19).Result;

			Assert.IsTrue(found.Count == 50);
		}

		[TestMethod]
		public void Should_GetRestaurant_FromMongoDB()
		{
			MongoDBContext ctx = new MongoDBContext();
			Restaurant found = ctx.GetRestaurant("40698892").Result;

			Assert.IsNotNull(found);
		}

		[TestMethod]
		public void Should_UpdateRestaurant_ViaMongoDB()
		{
			MongoDBContext ctx = new MongoDBContext();
			Restaurant initial = ctx.GetRestaurant("40698892").Result;
			string originalName = initial.name;

			initial.name = "Hi; I'm Updated";
			ModificationResult changeName = ctx.UpdateRestaurant(initial).Result;
			Restaurant updated = ctx.GetRestaurant("40698892").Result;
			string updatedName = updated.name;

			updated.name = originalName;
			ModificationResult revert = ctx.UpdateRestaurant(updated).Result;
			Restaurant reverted = ctx.GetRestaurant("40698892").Result;

			Assert.IsTrue(changeName.ModifiedCount == 1);
			Assert.IsTrue(updatedName == "Hi; I'm Updated");
			Assert.IsTrue(revert.ModifiedCount == 1);
			Assert.IsTrue(originalName == reverted.name);

		}

		[TestMethod]
		public void Should_AddAndRemove_ViaMongoDB()
		{
			MongoDBContext ctx = new MongoDBContext();
			ModificationResult add = ctx.AddRestaurant(new Restaurant { name = "for test", cuisine = "for test", borough = "for test" }).Result;
			Restaurant found = ctx.GetRestaurant(add.InstanceId).Result;
			ModificationResult remove = ctx.DeleteRestaurant(found).Result;
			Restaurant notfound = ctx.GetRestaurant(add.InstanceId).Result;

			Assert.IsTrue(add.ModifiedCount == 1);
			Assert.IsTrue(found != null);
			Assert.IsTrue(remove.ModifiedCount == 1);
			Assert.IsTrue(notfound == null);
		}
	}
}
