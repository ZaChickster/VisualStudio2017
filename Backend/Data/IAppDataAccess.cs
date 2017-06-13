using System.Collections.Generic;

namespace VisualStudio2017.Domain.DataAccess
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
