using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisualStudio2017.Domain.DataAccess
{
	public class MySessionWrapper : ISessionWrapper
    {
		private readonly ISession _session;

		public MySessionWrapper(IHttpContextAccessor session)
		{
			_session = session.HttpContext.Session;
		}

		public void Set(string key, string json)
		{
			_session.SetString(key, json);
		}

		public string Get(string key)
		{
			return _session.GetString(key);
		}
	}
}
