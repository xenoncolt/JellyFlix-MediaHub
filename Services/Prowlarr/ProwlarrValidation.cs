using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Services.Prowlarr
{
    internal class ProwlarrValidation
    {
        public static async Task<bool> ValidateApiKey(string base_url, string api_key)
        {
            try
            {
                if (!base_url.EndsWith("/"))
                {
                    base_url += "/";
                }

                using (HttpClient client = new HttpClient())
                {
                    string url = $"{base_url}api/v1/system/status";

                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("X-Api-Key", api_key);

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return !string.IsNullOrEmpty(content) && content.Contains("version");
                    }

                    return false;
                }
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
