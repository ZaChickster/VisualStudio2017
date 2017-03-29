using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VisualStudio2017.Domain.DataAccess
{
    public class HttpSessionDataAccess
    {
		public const string ITEMS_KEY = "_session-Items-k3y";

		private readonly ISessionWrapper _session;

		public HttpSessionDataAccess(ISessionWrapper session)
		{
			_session = session;
		}

		public List<WorkItem> GetAll()
		{
			List<WorkItem> items = new List<WorkItem>();
			string raw = _session.Get(ITEMS_KEY);

			if (!string.IsNullOrEmpty(raw))
			{
				List<WorkItem> converted = JsonConvert.DeserializeObject<List<WorkItem>>(raw);
				items.AddRange(converted);
			}

			return items;
		}

		public WorkItem GetOne(int id)
		{
			List<WorkItem> items = GetAll();

			return items.Find(i => i.Id == id);
		}

		public WorkItem Add(WorkItem item)
		{
			List<WorkItem> items = GetAll();

			int newId = items.Max(i => i.Id) + 1;
			item.Id = newId;

			items.Add(item);

			return item;
		}
	}
}
