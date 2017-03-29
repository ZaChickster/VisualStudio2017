using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
	}
}
