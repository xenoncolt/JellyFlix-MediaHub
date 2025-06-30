using JellyFlix_MediaHub.Data.Handlers;
using JellyFlix_MediaHub.Models.TMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Services.TMDB
{
    internal class TMDBDataService : IDisposable
    {
        private readonly TMDBService _tmdbService;

        public TMDBDataService(string tmdbApiKey)
        {
            _tmdbService = new TMDBService(tmdbApiKey);
        }

        public async Task SaveNewReleaseMoviesAsync(int totalMovies = 100)
        {
            try
            {
                Console.WriteLine($"Starting to fetch {totalMovies} new release movies...");
                var movies = await _tmdbService.GetNewReleaseMoviesAsync(totalMovies);

                Console.WriteLine($"Fetched {movies.Count} movies. Starting to save to database...");
                int savedCount = 0;
                int errorCount = 0;

                foreach (var movie in movies)
                {
                    try
                    {
                        Console.WriteLine($"Attempting to save movie: {movie.Title}");

                        // Save the movie first
                        int movieId = MovieHandle.SaveMovie(movie);

                        if (movieId > 0)
                        {
                            // Add movie to Now Playing category (maps to NewReleaseMovies)
                            bool addedToCategory = MovieHandle.AddMovieToCategory(movieId, Data.Handlers.CategoryType.NowPlaying);

                            if (addedToCategory)
                            {
                                savedCount++;
                                Console.WriteLine($"✓ Successfully saved movie {savedCount}/{movies.Count}: {movie.Title} (ID: {movieId})");
                            }
                            else
                            {
                                Console.WriteLine($"⚠ Movie saved but failed to add to category: {movie.Title}");
                                savedCount++; // Still count as saved since movie was saved
                            }
                        }
                        else
                        {
                            Console.WriteLine($"✗ Failed to save movie: {movie.Title}");
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error saving movie '{movie.Title}': {ex.Message}");
                        errorCount++;
                    }
                }

                Console.WriteLine($"\n=== SUMMARY ===");
                Console.WriteLine($"Total fetched: {movies.Count}");
                Console.WriteLine($"Successfully saved: {savedCount}");
                Console.WriteLine($"Errors: {errorCount}");
                Console.WriteLine($"Success rate: {(savedCount * 100.0 / movies.Count):F1}%");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SaveNewReleaseMoviesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task SaveNewReleaseTVShowsAsync(int totalShows = 100)
        {
            try
            {
                Console.WriteLine($"Starting to fetch {totalShows} new release TV shows...");
                var tvShows = await _tmdbService.GetNewReleaseTVShowsAsync(totalShows);

                Console.WriteLine($"Fetched {tvShows.Count} TV shows. Starting to save to database...");
                int savedCount = 0;
                int errorCount = 0;

                foreach (var tvShow in tvShows)
                {
                    try
                    {
                        // Save the TV show (assuming you have TVShowHandle.SaveTVShow method)
                        int tvShowId = TVShowHandle.SaveTVShow(tvShow);

                        if (tvShowId > 0)
                        {
                            // Add TV show to category - you can create a new category or use existing ones
                            bool addedToCategory = TVShowHandle.AddTVShowToCategory(tvShowId, Data.Handlers.CategoryType.NowPlaying);

                            if (addedToCategory)
                            {
                                savedCount++;
                                Console.WriteLine($"✓ Successfully saved TV show {savedCount}/{tvShows.Count}: {tvShow.Name}");
                            }
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error saving TV show '{tvShow.Name}': {ex.Message}");
                        errorCount++;
                    }
                }

                Console.WriteLine($"\n=== TV SHOWS SUMMARY ===");
                Console.WriteLine($"Successfully saved: {savedCount}, Errors: {errorCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving new release TV shows: {ex.Message}");
                throw;
            }
        }

        public async Task SaveTrendingContentAsync(int totalItems = 100)
        {
            try
            {
                Console.WriteLine($"Starting to fetch {totalItems} trending items...");
                var trendingItems = await _tmdbService.GetTrendingAsync(totalItems);
                int movieCount = 0, tvShowCount = 0;
                int movieErrors = 0, tvShowErrors = 0;

                foreach (var item in trendingItems)
                {
                    try
                    {
                        if (item is MovieModel movie)
                        {
                            Console.WriteLine($"Processing trending movie: {movie.Title}");
                            int movieId = MovieHandle.SaveMovie(movie);
                            if (movieId > 0)
                            {
                                bool addedToCategory = MovieHandle.AddMovieToCategory(movieId, Data.Handlers.CategoryType.Popular);
                                if (addedToCategory)
                                {
                                    movieCount++;
                                    Console.WriteLine($"✓ Successfully processed trending movie: {movie.Title}");
                                }
                                else
                                {
                                    Console.WriteLine($"⚠ Movie saved but category relationship failed: {movie.Title}");
                                    movieCount++; // Still count as success since movie was saved
                                }
                            }
                            else
                            {
                                Console.WriteLine($"✗ Failed to save movie: {movie.Title}");
                                movieErrors++;
                            }
                        }
                        else if (item is TVShowModel tvShow)
                        {
                            Console.WriteLine($"Processing trending TV show: {tvShow.Name}");
                            int tvShowId = TVShowHandle.SaveTVShow(tvShow);
                            if (tvShowId > 0)
                            {
                                bool addedToCategory = TVShowHandle.AddTVShowToCategory(tvShowId, Data.Handlers.CategoryType.Popular);
                                if (addedToCategory)
                                {
                                    tvShowCount++;
                                    Console.WriteLine($"✓ Successfully processed trending TV show: {tvShow.Name}");
                                }
                                else
                                {
                                    Console.WriteLine($"⚠ TV show saved but category relationship failed: {tvShow.Name}");
                                    tvShowCount++; // Still count as success since TV show was saved
                                }
                            }
                            else
                            {
                                Console.WriteLine($"✗ Failed to save TV show: {tvShow.Name}");
                                tvShowErrors++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error processing trending item: {ex.Message}");
                        // Log but continue processing
                        if (item is MovieModel)
                            movieErrors++;
                        else if (item is TVShowModel)
                            tvShowErrors++;
                    }
                }

                Console.WriteLine($"\n=== TRENDING CONTENT SUMMARY ===");
                Console.WriteLine($"Movies processed: {movieCount}, TV Shows processed: {tvShowCount}");
                if (movieErrors > 0 || tvShowErrors > 0)
                {
                    Console.WriteLine($"Errors: {movieErrors} movie errors, {tvShowErrors} TV show errors");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving trending content: {ex.Message}");
                throw;
            }
        }

        // Keep your existing methods...
        public async Task SaveAllCategoriesAsync()
        {
            Console.WriteLine("=== Starting to save all TMDB data ===");

            await SaveNewReleaseMoviesAsync(100);  // Fetch 100 movies
            await SaveNewReleaseTVShowsAsync(100); // Fetch 100 TV shows  
            await SaveTrendingContentAsync(100);   // Fetch 100 trending items

            Console.WriteLine("=== Completed saving all TMDB data ===");
        }

        public List<MovieModel> GetNewReleaseMovies()
        {
            return MovieHandle.GetMoviesByCategory(Data.Handlers.CategoryType.NowPlaying);
        }

        public List<TVShowModel> GetNewReleaseTVShows()
        {
            return TVShowHandle.GetTVShowsByCategory(Data.Handlers.CategoryType.NowPlaying);
        }

        public List<MovieModel> GetTrendingMovies()
        {
            return MovieHandle.GetMoviesByCategory(Data.Handlers.CategoryType.Popular);
        }

        public List<TVShowModel> GetTrendingTVShows()
        {
            return TVShowHandle.GetTVShowsByCategory(Data.Handlers.CategoryType.Popular);
        }

        public void Dispose()
        {
            _tmdbService?.Dispose();
        }
    }
}