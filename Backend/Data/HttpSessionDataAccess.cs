﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.Backend.Data
{
	public class HttpSessionDataAccess : IAppDataAccess
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

			return items
				.OrderBy(wi => wi.WhenDue)
				.ToList();
		}

		public WorkItem GetOne(int id)
		{
			List<WorkItem> items = GetAll();

			return GetOne(id, GetAll());
		}

		public WorkItem GetOne(int id, List<WorkItem> items)
		{
			return items.Find(i => i.Id == id);
		}

		public WorkItem Add(WorkItem item)
		{
			List<WorkItem> items = GetAll();
			int newId = 1; 

			if (items.Count > 0)
			{
				newId = items.Max(i => i.Id) + 1;
			}

			item.Id = newId;
			items.Add(item);
			_session.Set(ITEMS_KEY, JsonConvert.SerializeObject(items));

			return item;
		}

		public WorkItem Update(WorkItem changed)
		{
			List<WorkItem> items = GetAll();
			WorkItem item = GetOne(changed.Id, items);

			if (item != null)
			{
				items.Remove(item);
				items.Add(changed);
				_session.Set(ITEMS_KEY, JsonConvert.SerializeObject(items));
			}

			return changed;
		}

		public WorkItem Delete(int id)
		{
			List<WorkItem> items = GetAll();
			WorkItem item = GetOne(id, items);

			if (item != null)
			{
				items.Remove(item);
				_session.Set(ITEMS_KEY, JsonConvert.SerializeObject(items));
			}

			return item;
		}
	}
}
