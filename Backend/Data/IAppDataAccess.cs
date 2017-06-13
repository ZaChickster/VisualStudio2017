using System.Collections.Generic;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.Backend.Data
{
	public interface IAppDataAccess
	{
		List<WorkItem> GetAll();
		WorkItem GetOne(int id);
		WorkItem Add(WorkItem item);
		WorkItem Update(WorkItem changed);
		WorkItem Delete(int id);
	}
}
