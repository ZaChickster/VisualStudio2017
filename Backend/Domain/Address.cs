using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VisualStudio2017.Backend
{
	public class Address
	{
		[ScaffoldColumn(false)]
		public string building { get; set; }

		[ScaffoldColumn(false)]
		public List<double> coord { get; set; }

		[ScaffoldColumn(false)]
		public string street { get; set; }

		[ScaffoldColumn(false)]
		public string zipcode { get; set; }
	}
}
