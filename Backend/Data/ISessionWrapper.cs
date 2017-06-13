namespace VisualStudio2017.Backend.Data
{
	public interface ISessionWrapper
	{
		void Set(string key, string json);
		string Get(string key);
	}
}
