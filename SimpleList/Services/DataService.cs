using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace SimpleList
{
	public class DataService<T> : IDataService<T> where T : class, new()
	{
		//https://gist.githubusercontent.com/keeguon/2310008/raw/bdc2ce1c1e3f28f9cab5b4393c7549f38361be4e/countries.json
		public async Task<T> GetAsync(string url, string extraParam = "")
		{
			try
			{
				if (CrossConnectivity.Current.IsConnected)
				{
					T result = default(T);

					using (var client = new HttpClient { BaseAddress = new Uri(url) })
					{
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

						//Basic Auth
						//request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "yourusername", "yourpwd"))));

						//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");

						//Authentication
						//var byteArray = Encoding.ASCII.GetBytes("my_client_id:my_client_secret");
						//var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
						//client.DefaultRequestHeaders.Authorization = header;


						var webApiUri = $"{url}";
						Debug.WriteLine("URLPath : " + webApiUri);
						var response = await client.GetAsync(webApiUri);
						response.EnsureSuccessStatusCode();
						var getResult = await response.Content.ReadAsStringAsync();
						if (getResult != null)
						{
							result = JsonConvert.DeserializeObject<T>(getResult);
						}
						Debug.WriteLine("Return payload : " + result);
					}
					return result;
				}
				return null;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("RestClient : " + ex.Message + " " + ex.StackTrace);
				//throw;
				return null;
			}
		}
	}
}
