using System;

namespace VisualStudio2017.Backend.Domain
{
    public class WorkItem
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime? WhenDue { get; set; }
		public DateTime? WhenCompleted { get; set; }
		public bool Completed { get; set; }
	}
}
