using VisualStudio2017.Backend.Data;

namespace VisualStudio2017.Backend.Test.Data
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
