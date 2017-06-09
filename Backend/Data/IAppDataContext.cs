using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisualStudio2017.Backend.Data
{
	public interface IAppDataContext
    {
		Task<int> GetCount();
		Task<List<Restaurant>> GetRestaurants(int pageSize, int pageNumber);
		Task<Restaurant> GetRestaurant(string id);
		Task<ModificationResult> UpdateRestaurant(Restaurant data);
		Task<ModificationResult> AddRestaurant(Restaurant data);
		Task<ModificationResult> DeleteRestaurant(Restaurant data);
	}
}
