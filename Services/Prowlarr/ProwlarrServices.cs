using JellyFlix_MediaHub.Models.Prowlarr;
using JellyFlix_MediaHub.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Services.Prowlarr
{
    internal class ProwlarrService
    {
        private readonly string baseUrl;
        private readonly string apiKey;
        private static readonly HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };

        public ProwlarrService(string baseUrl, string apiKey)
        {
            this.baseUrl = baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/";
            this.apiKey = apiKey;
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                string testUrl = $"{baseUrl}api/v1/system/status";
                var request = new HttpRequestMessage(HttpMethod.Get, testUrl);
                request.Headers.Add("X-Api-Key", apiKey);
                request.Headers.Add("User-Agent", "JellyFlix-MediaHub/1.0");

                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    var response = await httpClient.SendAsync(request, cts.Token);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Prowlarr connection test failed: {ex.Message}");
                return false;
            }
        }

        public async Task<List<ProwlarrSearchResult>> SearchAsync(string query, int? tmdbId = null, string category = null)
        {
            try
            {
                Console.WriteLine($"Testing Prowlarr connection first...");
                if (!await TestConnectionAsync())
                {
                    throw new Exception("Cannot connect to Prowlarr server. Please check if Prowlarr is running and accessible.");
                }

                string searchUrl = $"{baseUrl}api/v1/search";
                string queryParam = $"?query={Uri.EscapeDataString(query)}";

                if (tmdbId.HasValue)
                {
                    queryParam += $"&tmdbId={tmdbId.Value}";
                }

                if (!string.IsNullOrEmpty(category))
                {
                    queryParam += $"&categories={category}";
                }

                string fullUrl = searchUrl + queryParam;
                Console.WriteLine($"Searching Prowlarr: {fullUrl}");

                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("X-Api-Key", apiKey);
                request.Headers.Add("User-Agent", "JellyFlix-MediaHub/1.0");

                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(45)))
                {
                    var response = await httpClient.SendAsync(request, cts.Token);
                    Console.WriteLine($"Prowlarr response status: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Prowlarr response length: {content.Length}");

                        if (string.IsNullOrEmpty(content) || content.Trim() == "[]")
                        {
                            Console.WriteLine("No results from Prowlarr");
                            return new List<ProwlarrSearchResult>();
                        }

                        var results = JsonConvert.DeserializeObject<List<ProwlarrSearchResult>>(content);
                        Console.WriteLine($"Found {results?.Count ?? 0} results from Prowlarr");
                        return results ?? new List<ProwlarrSearchResult>();
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Prowlarr API error: {response.StatusCode} - {errorContent}");
                        throw new Exception($"Prowlarr API returned {response.StatusCode}: {response.ReasonPhrase}");
                    }
                }
            }
            catch (TaskCanceledException ex) when (ex.CancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"Prowlarr search was cancelled/timed out: {ex.Message}");
                throw new Exception("Search timed out. Prowlarr may be slow or overloaded. Try again in a moment.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Prowlarr connection error: {ex.Message}");
                throw new Exception($"Cannot connect to Prowlarr. Check if the server is running at {baseUrl}");
            }
            catch (Exception ex) when (!(ex is TaskCanceledException))
            {
                Console.WriteLine($"Error searching Prowlarr: {ex.Message}");
                throw;
            }
        }

        public ProwlarrSearchResult GetBestMatch(List<ProwlarrSearchResult> results, string title, int? tmdbId = null)
        {
            if (results == null || results.Count == 0)
                return null;

            var seedableResults = results.Where(r => r.Seeders.HasValue && r.Seeders.Value > 0).ToList();

            if (seedableResults.Count == 0)
                return null;

            var tmdbMatches = new List<ProwlarrSearchResult>();
            if (tmdbId.HasValue)
            {
                tmdbMatches = seedableResults.Where(r =>
                    r.Title != null &&
                    (r.Title.Contains(tmdbId.Value.ToString()) ||
                     IsCloseMatch(r.Title, title))).ToList();
            }

            if (tmdbMatches.Count == 0)
            {
                tmdbMatches = seedableResults.Where(r => IsCloseMatch(r.Title, title)).ToList();
            }

            if (tmdbMatches.Count == 0)
            {
                tmdbMatches = seedableResults;
            }

            return tmdbMatches.OrderByDescending(r => r.Seeders.Value).First();
        }

        private bool IsCloseMatch(string resultTitle, string searchTitle)
        {
            if (string.IsNullOrEmpty(resultTitle) || string.IsNullOrEmpty(searchTitle))
                return false;

            string cleanResult = CleanTitle(resultTitle);
            string cleanSearch = CleanTitle(searchTitle);

            return cleanResult.Contains(cleanSearch) || cleanSearch.Contains(cleanResult);
        }

        private string CleanTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return string.Empty;

            return title.ToLowerInvariant()
                       .Replace(".", " ")
                       .Replace("-", " ")
                       .Replace("_", " ")
                       .Replace("  ", " ")
                       .Trim();
        }
    }
}