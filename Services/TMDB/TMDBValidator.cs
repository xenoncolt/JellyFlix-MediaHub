using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Services.TMDB
{
    internal class TMDBValidator
    {
        public static async Task<bool> ValidApiKey(string api_key)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://api.themoviedb.org/3/authentication?api_key={api_key}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        dynamic json_response = JsonConvert.DeserializeObject(content);

                        return json_response.success == true;
                    }
                }
                return false;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
