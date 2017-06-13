namespace VisualStudio2017.Domain.DataAccess
{
	public interface ISessionWrapper
	{
		void Set(string key, string json);
		string Get(string key);
	}
}
