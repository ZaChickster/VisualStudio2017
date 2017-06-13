using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace VisualStudio2017.Backend.Domain
{
	public class Restaurant
	{
		[ScaffoldColumn(false)]
		public ObjectId Id { get; set; }

		[ScaffoldColumn(false)]
		public Address address { get; set; }

		[ScaffoldColumn(false)]
		public string borough { get; set; }

		[ScaffoldColumn(false)]
		public string cuisine { get; set; }

		[ScaffoldColumn(false)]
		public List<Rating> grades { get; set; }

		[ScaffoldColumn(false)]
		public string name { get; set; }

		[ScaffoldColumn(false)]
		public string restaurant_id { get; set; }
	}
}
