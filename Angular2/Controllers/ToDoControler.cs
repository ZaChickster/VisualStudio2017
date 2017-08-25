using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VisualStudio2017.Backend.Data;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.Angular4.Controllers
{
	[Route("api")]
    public class ToDoController : Controller
    {
		private readonly IAppDataAccess _dataAccess;

		public ToDoController(IAppDataAccess da)
		{
			_dataAccess = da;
		}

        [HttpGet("ToDos")]
        public List<WorkItem> GetAll()
        {
			return _dataAccess.GetAll();
		}

		[HttpGet("ToDo")]
		public WorkItem GetOne(int? id)
		{
			if (id != null)
			{
				return _dataAccess.GetOne(id.Value);
			}
			else
			{
				return new WorkItem();
			}
		}

		[HttpPost("ToDo")]
		public WorkItem Update([FromBody] WorkItem item)
		{
			return _dataAccess.Update(item);
		}

		[HttpPut("ToDo")]
		public WorkItem Insert([FromBody] WorkItem item)
		{
			return _dataAccess.Add(item);
		}

		[HttpDelete("ToDo")]
		public WorkItem Remove([FromBody] WorkItem item)
		{
			return _dataAccess.Delete(item.Id); ;
		}
	}
}
