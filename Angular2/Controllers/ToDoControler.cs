using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VisualStudio2017.Domain;
using VisualStudio2017.Domain.DataAccess;
using Microsoft.AspNetCore.Http;
using VisualStudio2017.Angular2.Models;

namespace VisualStudio2017.Angular2.Controllers
{
    [Route("api")]
    public class ToDoController : Controller
    {
		private readonly IAppDataAccess _dataAccess;

		public ToDoController(IAppDataAccess da)
		{
			_dataAccess = da;
		}

		private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("ToDos")]
        public List<WorkItem> GetAll()
        {
			return _dataAccess.GetAll();
		}

		[HttpGet("ToDo")]
		public WorkItemModel GetOne(int? id)
		{
			WorkItemModel model = new WorkItemModel();

			if (id != null)
			{
				model.Data = _dataAccess.GetOne(id.Value);
			}
			else
			{
				model.Data = new WorkItem();
			}

			return model;
		}

		[HttpPost("ToDo")]
		public WorkItemModel Update([FromBody] WorkItem item)
		{
			WorkItemModel model = new WorkItemModel();
			model.Data = _dataAccess.Update(item);
			model.Message = "ToDo Updated";
			return model;
		}

		[HttpPut("ToDo")]
		public WorkItemModel Insert([FromBody] WorkItem item)
		{
			WorkItemModel model = new WorkItemModel();
			model.Data = _dataAccess.Add(item);
			model.Message = "ToDo Added";
			return model;
		}

		[HttpDelete("ToDo")]
		public WorkItemModel Remove([FromBody] WorkItem item)
		{
			WorkItemModel model = new WorkItemModel();
			model.Data = _dataAccess.Delete(item.Id);
			model.Message = "ToDo Deleted";
			return model;
		}
	}
}
