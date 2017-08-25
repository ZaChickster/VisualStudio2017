using System.Collections.Generic;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.Angular4.Models
{
    public class RestaurantsIndex
    {
		public int TotalRestaurants { get; set; }
		public List<Restaurant> PageInstances { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int NumberPages
		{
			get
			{
				int rounded = TotalRestaurants / PageSize;
				
				if (rounded % PageSize == 0)
				{
					return rounded;
				}

				return rounded + 1;
			}
		}

		public int PreviousPage => CurrentPage - 1;
	    public bool ShowPreviousPage => PreviousPage >= 0;
	    public int NextPage => CurrentPage + 1;
	    public bool ShowNextPage => CurrentPage < NumberPages - 1;
    }
}
