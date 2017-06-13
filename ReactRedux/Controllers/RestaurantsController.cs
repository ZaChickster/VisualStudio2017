using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VisualStudio2017.Backend.Data;
using VisualStudio2017.Backend.Domain;
using VisualStudio2017.ReactRedux.Models;

namespace VisualStudio2017.ReactRedux.Controllers
{
    [Route("api")]
    public class RestaurantsController : Controller
    {
		private readonly IAppDataContext _mongo;

		public RestaurantsController(IAppDataContext ctx)
		{
			_mongo = ctx;
		}		

		[HttpGet("Restaurants")]
		public async Task<RestaurantsIndex> Restaurants(int? page)
		{
			return new RestaurantsIndex
			{
				TotalRestaurants = await _mongo.GetCount(),
				PageInstances = await _mongo.GetRestaurants(15, page ?? 0),
				CurrentPage = page ?? 0,
				PageSize = 15
			};
		}

		[HttpGet("Restaurant")]		
		public async Task<Restaurant> Restaurant(string id)
		{
			Restaurant found = await _mongo.GetRestaurant(id);
			return found;
		}

		[HttpPost("Restaurant")]
		public async Task<Restaurant> Update([FromBody] Restaurant instance)
		{
			ModificationResult updateResult = await _mongo.UpdateRestaurant(instance);
			Restaurant found = await _mongo.GetRestaurant(instance.restaurant_id);
			return found;
		}

		[HttpPut("Restaurant")]
		public async Task<Restaurant> Add([FromBody] Restaurant instance)
		{
			ModificationResult added = await _mongo.AddRestaurant(instance);
			Restaurant found = await _mongo.GetRestaurant(instance.restaurant_id);
			return found;
		}

		[HttpDelete("Restaurant")]
		public async Task<string> Delete([FromBody] Restaurant instance)
		{
			ModificationResult deleted = await _mongo.DeleteRestaurant(instance);
			return instance.restaurant_id;
		}
	}
}
