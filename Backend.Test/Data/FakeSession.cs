using VisualStudio2017.Domain.DataAccess;

namespace VisualStudio2017.Domain.Tests.DataAccess
{
	public class FakeSession : ISessionWrapper
	{
		private string _storage;

		public string Get(string key)
		{
			return _storage; ;
		}

		public void Set(string key, string json)
		{
			_storage = json;
		}
	}
}
