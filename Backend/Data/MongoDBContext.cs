﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.Backend.Data
{
    public class MongoDBContext : IAppDataContext
	{
		private readonly IMongoCollection<Restaurant> _collection;
		private readonly FilterDefinition<Restaurant> _filter;

		public MongoDBContext()
		{
			IMongoClient client = new MongoClient();
			IMongoDatabase database = client.GetDatabase("test");
			_collection = database.GetCollection<Restaurant>("restaurants");
			_filter = Builders<Restaurant>.Filter.Ne("name", BsonString.Empty);
		}

		public async Task<int> GetCount()
		{
			long count = await _collection.CountAsync(_filter);

			return (int)count;
		}

		public async Task<List<Restaurant>> GetRestaurants(int pageSize, int pageNumber)
		{
			List<Restaurant> results = new List<Restaurant>();
			FindOptions<Restaurant> options = new FindOptions<Restaurant>()
			{
				Sort = Builders<Restaurant>.Sort.Ascending("name"),
				Skip = pageNumber * pageSize,
				Limit = pageSize,
				Projection = Builders<Restaurant>.Projection
					.Include(r => r.restaurant_id)
					.Include(r => r.name)
					.Include(r => r.borough)
					.Include(r => r.cuisine)
			};

			IAsyncCursor<Restaurant> found = await _collection.FindAsync(_filter, options);
			results.AddRange(found.ToList());

			return results;
		}

		public async Task<Restaurant> GetRestaurant(string id)
		{
			FilterDefinition<Restaurant> filter = Builders<Restaurant>.Filter.Eq("restaurant_id", id);

			List<Restaurant> found = await _collection.Find(filter)
				.SortBy(x => x.name)
				.Limit(1)
				.ToListAsync();

			return found.FirstOrDefault();
		}

		public async Task<ModificationResult> UpdateRestaurant(Restaurant data)
		{
			FilterDefinition<Restaurant> filter = Builders<Restaurant>.Filter.Eq("restaurant_id", data.restaurant_id);
			UpdateDefinition<Restaurant> update = Builders<Restaurant>.Update
				.Set(s => s.name, data.name)
				.Set(s => s.cuisine, data.cuisine)
				.Set(s => s.borough, data.borough);

			UpdateResult operation = await _collection.UpdateOneAsync(filter, update);
			ModificationResult result = new ModificationResult { ModifiedCount = operation.ModifiedCount, MatchedCount = operation.MatchedCount, InstanceId = data.restaurant_id };

			return result;
		}

		public async Task<ModificationResult> AddRestaurant(Restaurant data)
		{
			data.restaurant_id = Guid.NewGuid().ToString();

			await _collection.InsertOneAsync(data);

			return new ModificationResult { ModifiedCount = 1, MatchedCount = 1, InstanceId = data.restaurant_id };
		}

		public async Task<ModificationResult> DeleteRestaurant(Restaurant data)
		{
			FilterDefinition<Restaurant> filter = Builders<Restaurant>.Filter.Eq("restaurant_id", data.restaurant_id);
			DeleteResult result = await _collection.DeleteOneAsync<Restaurant>(r => r.restaurant_id == data.restaurant_id);

			return new ModificationResult { ModifiedCount = result.DeletedCount, MatchedCount = result.DeletedCount, InstanceId = data.restaurant_id };
		}
	}
}
