using System;

namespace VisualStudio2017.Domain
{
    public class WorkItem
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Due { get; set; }
		public DateTime? Completed { get; set; }
	}
}
