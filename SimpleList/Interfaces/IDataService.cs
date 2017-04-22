using System;
using System.Threading.Tasks;

namespace SimpleList
{
	public interface IDataService<T> where T : class, new()
	{
		Task<T> GetAsync(string url, string extraParam = "");
	}
}
