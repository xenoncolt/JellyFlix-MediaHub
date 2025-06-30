using JellyFlix_MediaHub.Data.Handlers;
using JellyFlix_MediaHub.Models.TMDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Services.TMDB
{
    internal class TMDBService : IDisposable
    {
        public string _apiKey { get; private set; }
        public HttpClient _httpClient { get; private set; }
        private readonly string _baseUrl = "https://api.themoviedb.org/3";


        public TMDBService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<List<MovieModel>> SearchMoviesAsync(string query, int maxResults = 20)
        {
            var movies = new List<MovieModel>();

            try
            {
                var url = $"{_baseUrl}/search/movie?api_key={_apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1";
                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<TMDBMovieResponse>(response);

                if (result?.Results != null)
                {
                    var itemsToFetch = Math.Min(result.Results.Count, maxResults);
                    for (int i = 0; i < itemsToFetch; i++)
                    {
                        try
                        {
                            var movie = await GetMovieDetailsAsync(result.Results[i].Id);
                            if (movie != null)
                            {
                                movies.Add(movie);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching movie details for ID {result.Results[i].Id}: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching movies: {ex.Message}");
            }

            return movies;
        }

        public async Task<List<TVShowModel>> SearchTVShowsAsync(string query, int maxResults = 20)
        {
            var shows = new List<TVShowModel>();

            try
            {
                var url = $"{_baseUrl}/search/tv?api_key={_apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1";
                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<TMDBTVResponse>(response);

                if (result?.Results != null)
                {
                    var itemsToFetch = Math.Min(result.Results.Count, maxResults);
                    for (int i = 0; i < itemsToFetch; i++)
                    {
                        try
                        {
                            var show = await GetTVShowDetailsAsync(result.Results[i].Id);
                            if (show != null)
                            {
                                shows.Add(show);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching TV show details for ID {result.Results[i].Id}: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching TV shows: {ex.Message}");
            }

            return shows;
        }

        public async Task<List<object>> SearchAllAsync(string query, int maxResults = 20)
        {
            var allResults = new List<object>();

            try
            {
                var url = $"{_baseUrl}/search/multi?api_key={_apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1";
                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<TMDBTrendingResponse>(response);

                if (result?.Results != null)
                {
                    var itemsToFetch = Math.Min(result.Results.Count, maxResults);
                    for (int i = 0; i < itemsToFetch; i++)
                    {
                        try
                        {
                            if (result.Results[i].MediaType == "movie")
                            {
                                var movie = await GetMovieDetailsAsync(result.Results[i].Id);
                                if (movie != null)
                                {
                                    allResults.Add(movie);
                                }
                            }
                            else if (result.Results[i].MediaType == "tv")
                            {
                                var show = await GetTVShowDetailsAsync(result.Results[i].Id);
                                if (show != null)
                                {
                                    allResults.Add(show);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching details for ID {result.Results[i].Id}: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching all content: {ex.Message}");
            }

            return allResults;
        }

        public async Task<List<MovieModel>> GetNewReleaseMoviesAsync(int totalMovies = 100)
        {
            var allMovies = new List<MovieModel>();
            int moviesPerPage = 20; 
            int pagesToFetch = Math.Min((int)Math.Ceiling((double)totalMovies / moviesPerPage), 10); 

            Console.WriteLine($"Fetching {totalMovies} movies across {pagesToFetch} pages...");
            var random = new Random();
            var used_pages = new HashSet<int>();

            for (int i = 0; i < pagesToFetch && allMovies.Count < totalMovies; i++)
            {
                int page;
                do
                {
                    page = random.Next(1, 101); 
                } while (used_pages.Contains(page));
                used_pages.Add(page);
                try
                {
                    Console.WriteLine($"Fetching page {page}...");
                    var url = $"{_baseUrl}/movie/now_playing?api_key={_apiKey}&language=en-US&page={page}";
                    var response = await _httpClient.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<TMDBMovieResponse>(response);

                    if (result?.Results == null || result.Results.Count == 0)
                    {
                        Console.WriteLine($"No more results found at page {page}");
                        break;
                    }

                    foreach (var movieData in result.Results)
                    {
                        if (allMovies.Count >= totalMovies)
                            break;

                        try
                        {
                            var movie = await GetMovieDetailsAsync(movieData.Id);
                            if (movie != null)
                            {
                                allMovies.Add(movie);
                                Console.WriteLine($"Fetched movie {allMovies.Count}: {movie.Title}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching movie details for ID {movieData.Id}: {ex.Message}");
                        }
                    }
                    await Task.Delay(250);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching page {page}: {ex.Message}");
                }
            }

            Console.WriteLine($"Successfully fetched {allMovies.Count} movies");
            return allMovies;
        }

        public async Task<List<TVShowModel>> GetNewReleaseTVShowsAsync(int total_shows_wanted = 100)
        {
            var allShows = new List<TVShowModel>();
            int showsPerPage = 20; 
            int pagesToFetch = (int)Math.Ceiling((double)total_shows_wanted / showsPerPage);

            Console.WriteLine($"Fetching {total_shows_wanted} TV shows across {pagesToFetch} pages...");

            var random = new Random();
            var usedPages = new HashSet<int>();

            for (int i = 0; i < pagesToFetch && allShows.Count < total_shows_wanted; i++)
            {
                int page;
                do
                {
                    page = random.Next(1, 101);
                } while (usedPages.Contains(page));
                usedPages.Add(page);
                try
                {
                    Console.WriteLine($"Fetching TV shows page {page}...");
                    var url = $"{_baseUrl}/tv/on_the_air?api_key={_apiKey}&language=en-US&page={page}";
                    var response = await _httpClient.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<TMDBTVResponse>(response);

                    if (result?.Results == null || result.Results.Count == 0)
                    {
                        Console.WriteLine($"No more TV show results found at page {page}");
                        break;
                    }

                    foreach (var tvData in result.Results)
                    {
                        if (allShows.Count >= total_shows_wanted)
                            break;

                        try
                        {
                            var tvShow = await GetTVShowDetailsAsync(tvData.Id);
                            if (tvShow != null)
                            {
                                allShows.Add(tvShow);
                                Console.WriteLine($"Fetched TV show {allShows.Count}: {tvShow.Name}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching TV show details for ID {tvData.Id}: {ex.Message}");
                        }
                    }

                    await Task.Delay(250);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching TV shows page {page}: {ex.Message}");
                }
            }

            Console.WriteLine($"Successfully fetched {allShows.Count} TV shows");
            return allShows;
        }

        public async Task<List<object>> GetTrendingAsync(int total_items_wanted = 100)
        {
            var allItems = new List<object>();
            int itemsPerPage = 20;
            int pagesToFetch = (int)Math.Ceiling((double)total_items_wanted / itemsPerPage);

            Console.WriteLine($"Fetching {total_items_wanted} trending items across {pagesToFetch} pages...");

            var random = new Random();
            var usedPages = new HashSet<int>();

            for (int i = 0; i < pagesToFetch && allItems.Count < total_items_wanted; i++)
            {
                int page;
                do
                {
                    page = random.Next(1, 101);
                } while (usedPages.Contains(page));
                usedPages.Add(page);
                try
                {
                    Console.WriteLine($"Fetching trending page {page}...");
                    var url = $"{_baseUrl}/trending/all/day?api_key={_apiKey}&language=en-US&page={page}";
                    var response = await _httpClient.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<TMDBTrendingResponse>(response);

                    if (result?.Results == null || result.Results.Count == 0)
                    {
                        Console.WriteLine($"No more trending results found at page {page}");
                        break;
                    }

                    foreach (var item in result.Results)
                    {
                        if (allItems.Count >= total_items_wanted)
                            break;

                        try
                        {
                            if (item.MediaType == "movie")
                            {
                                var movie = await GetMovieDetailsAsync(item.Id);
                                if (movie != null)
                                {
                                    allItems.Add(movie);
                                    Console.WriteLine($"Fetched trending movie: {movie.Title}");
                                }
                            }
                            else if (item.MediaType == "tv")
                            {
                                var tvShow = await GetTVShowDetailsAsync(item.Id);
                                if (tvShow != null)
                                {
                                    allItems.Add(tvShow);
                                    Console.WriteLine($"Fetched trending TV show: {tvShow.Name}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching details for trending item ID {item.Id}: {ex.Message}");
                        }
                    }

                    await Task.Delay(250);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching trending page {page}: {ex.Message}");
                }
            }

            Console.WriteLine($"Successfully fetched {allItems.Count} trending items");
            return allItems;
        }

        public async Task<MovieModel> GetMovieDetailsAsync(int movieId)
        {
            try
            {
                var url = $"{_baseUrl}/movie/{movieId}?api_key={_apiKey}&language=en-US&append_to_response=credits";
                var response = await _httpClient.GetStringAsync(url);

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine($"Empty response for movie ID {movieId}");
                    return null;
                }

                var tmdbMovie = JsonConvert.DeserializeObject<TMDBMovieDetails>(response);

                if (tmdbMovie == null)
                {
                    Console.WriteLine($"Failed to deserialize movie data for ID {movieId}");
                    return null;
                }

                var movie = new MovieModel
                {
                    TMDBId = tmdbMovie.Id,
                    Title = tmdbMovie.Title ?? "Unknown Title",
                    Overview = tmdbMovie.Overview ?? "",
                    ReleaseDate = tmdbMovie.ReleaseDate,
                    Runtime = tmdbMovie.Runtime,
                    VoteAverage = (decimal?)tmdbMovie.VoteAverage,
                    VoteCount = tmdbMovie.VoteCount,
                    Popularity = (decimal?)tmdbMovie.Popularity,
                    PosterPath = tmdbMovie.PosterPath,
                    BackdropPath = tmdbMovie.BackdropPath,
                    OriginalLanguage = tmdbMovie.OriginalLanguage ?? "",
                    OriginalTitle = tmdbMovie.OriginalTitle ?? "",
                    Adult = tmdbMovie.Adult,
                    Budget = tmdbMovie.Budget,
                    Revenue = tmdbMovie.Revenue,
                    Status = tmdbMovie.Status ?? "",
                    Tagline = tmdbMovie.Tagline ?? "",
                    HomePage = tmdbMovie.Homepage ?? "",
                    ImdbId = tmdbMovie.ImdbId ?? "",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                Console.WriteLine($"Movie {movie.Title} - PosterPath: '{movie.PosterPath}'");
                return movie;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching movie details for ID {movieId}: {ex.Message}");
                return null;
            }
        }

        public async Task<TVShowModel> GetTVShowDetailsAsync(int tvShowId)
        {
            try
            {
                var url = $"{_baseUrl}/tv/{tvShowId}?api_key={_apiKey}&language=en-US&append_to_response=credits";
                var response = await _httpClient.GetStringAsync(url);

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine($"Empty response for TV show ID {tvShowId}");
                    return null;
                }

                var tmdbShow = JsonConvert.DeserializeObject<TMDBTVDetails>(response);

                if (tmdbShow == null)
                {
                    Console.WriteLine($"Failed to deserialize TV show data for ID {tvShowId}");
                    return null;
                }

                var tvShow = new TVShowModel
                {
                    TMDBId = tmdbShow.Id,
                    Name = tmdbShow.Name ?? "Unknown Name",
                    Overview = tmdbShow.Overview ?? "",
                    FirstAirDate = tmdbShow.FirstAirDate,
                    LastAirDate = tmdbShow.LastAirDate,
                    NumberOfEpisodes = tmdbShow.NumberOfEpisodes,
                    NumberOfSeasons = tmdbShow.NumberOfSeasons,
                    EpisodeRunTime = tmdbShow.EpisodeRunTime?.Count > 0 ? string.Join(",", tmdbShow.EpisodeRunTime) : null,
                    VoteAverage = (decimal?)tmdbShow.VoteAverage,
                    VoteCount = tmdbShow.VoteCount,
                    Popularity = (decimal?)tmdbShow.Popularity,
                    PosterPath = tmdbShow.PosterPath,
                    BackdropPath = tmdbShow.BackdropPath,
                    OriginalLanguage = tmdbShow.OriginalLanguage ?? "",
                    OriginalName = tmdbShow.OriginalName ?? "",
                    Status = tmdbShow.Status ?? "",
                    Type = tmdbShow.Type ?? "",
                    HomePage = tmdbShow.Homepage ?? "",
                    InProduction = tmdbShow.InProduction,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                Console.WriteLine($"TV Show {tvShow.Name} - PosterPath: '{tvShow.PosterPath}'");
                return tvShow;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching TV show details for ID {tvShowId}: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    // TMDB API Response Models
    public class TMDBMovieResponse
    {
        public List<TMDBMovieBasic> Results { get; set; }
    }

    public class TMDBTVResponse
    {
        public List<TMDBTVBasic> Results { get; set; }
    }

    public class TMDBTrendingResponse
    {
        public List<TMDBTrendingItem> Results { get; set; }
    }

    public class TMDBMovieBasic
    {
        public int Id { get; set; }
    }

    public class TMDBTVBasic
    {
        public int Id { get; set; }
    }

    public class TMDBTrendingItem
    {
        public int Id { get; set; }
        [JsonProperty("media_type")]
        public string MediaType { get; set; }
    }

    public class TMDBMovieDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }
        public int? Runtime { get; set; }
        [JsonProperty("vote_average")]
        public double? VoteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int? VoteCount { get; set; }
        public double? Popularity { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        public bool Adult { get; set; }
        public long? Budget { get; set; }
        public long? Revenue { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Homepage { get; set; }
        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }
        public List<TMDBGenre> Genres { get; set; }
        [JsonProperty("production_companies")]
        public List<TMDBProductionCompany> ProductionCompanies { get; set; }
        public TMDBCredits Credits { get; set; }
    }

    public class TMDBTVDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        [JsonProperty("first_air_date")]
        public DateTime? FirstAirDate { get; set; }
        [JsonProperty("last_air_date")]
        public DateTime? LastAirDate { get; set; }
        [JsonProperty("number_of_episodes")]
        public int? NumberOfEpisodes { get; set; }
        [JsonProperty("number_of_seasons")]
        public int? NumberOfSeasons { get; set; }
        [JsonProperty("episode_run_time")]
        public List<int> EpisodeRunTime { get; set; }
        [JsonProperty("vote_average")]
        public double? VoteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int? VoteCount { get; set; }
        public double? Popularity { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonProperty("original_name")]
        public string OriginalName { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Homepage { get; set; }
        [JsonProperty("in_production")]
        public bool InProduction { get; set; }
        public List<TMDBGenre> Genres { get; set; }
        [JsonProperty("production_companies")]
        public List<TMDBProductionCompany> ProductionCompanies { get; set; }
        public TMDBCredits Credits { get; set; }
    }

    public class TMDBGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TMDBProductionCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }
        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    public class TMDBCredits
    {
        public List<TMDBCast> Cast { get; set; }
    }

    public class TMDBCast
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
        public int? Gender { get; set; }
        public string Character { get; set; }
        [JsonProperty("credit_id")]
        public string CreditId { get; set; }
        public int? Order { get; set; }
    }
}