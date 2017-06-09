using System;
using System.ComponentModel.DataAnnotations;

namespace VisualStudio2017.Backend
{
	public class Rating
	{
		[ScaffoldColumn(false)]
		public DateTime date { get; set; }

		[ScaffoldColumn(false)]
		public string grade { get; set; }

		[ScaffoldColumn(false)]
		public int? score { get; set; }
	}
}
