using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Services.Prowlarr
{
    internal static class ProwlarrValidation
    {
        private static readonly HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(15) };

        public static async Task<bool> ValidateApiKey(string baseUrl, string apiKey)
        {
            try
            {
                if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(apiKey))
                    return false;

                string url = baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/";
                url += "api/v1/system/status";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("X-Api-Key", apiKey);
                request.Headers.Add("User-Agent", "JellyFlix-MediaHub/1.0");

                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    var response = await httpClient.SendAsync(request, cts.Token);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Prowlarr validation timeout");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Prowlarr validation error: {ex.Message}");
                return false;
            }
        }
    }
}