using JellyFlix_MediaHub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Services.TMDB
{
    internal class TMDBServiceProvider
    {
        private static TMDBServiceProvider _instance;
        private static readonly object _lock = new object();

        private TMDBService _tmdbService;
        private TMDBDataService _tmdbDataService;
        private string _apiKey;
        private bool _isInitialized;

        public static TMDBServiceProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TMDBServiceProvider();
                        }
                    }
                }
                return _instance;
            }
        }

        private TMDBServiceProvider()
        {
            // Private constructor for singleton pattern
        }

        /// <summary>
        /// Initializes the provider using the API key from configuration.
        /// </summary>
        /// <returns>True if initialization was successful, false otherwise.</returns>
        public bool Initialize()
        {
            try
            {
                var config = ConfigManager.LoadConfig();

                if (string.IsNullOrEmpty(config.TmdbApiKey))
                {
                    Console.WriteLine("TMDB API key not found in configuration.");
                    return false;
                }

                _apiKey = config.TmdbApiKey;
                _tmdbService = new TMDBService(_apiKey);
                _tmdbDataService = new TMDBDataService(_apiKey);
                _isInitialized = true;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing TMDB service provider: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Validates the stored API key.
        /// </summary>
        /// <returns>True if the API key is valid, false otherwise.</returns>
        public async Task<bool> ValidateApiKey()
        {
            if (!_isInitialized && !Initialize())
            {
                return false;
            }

            return await TMDBValidator.ValidApiKey(_apiKey);
        }

        /// <summary>
        /// Gets the TMDBService instance using the stored API key.
        /// </summary>
        public TMDBService GetTMDBService()
        {
            if (!_isInitialized && !Initialize())
            {
                throw new InvalidOperationException("TMDB Service Provider is not initialized. Call Initialize() first.");
            }

            return _tmdbService;
        }

        /// <summary>
        /// Gets the TMDBDataService instance using the stored API key.
        /// </summary>
        public TMDBDataService GetTMDBDataService()
        {
            if (!_isInitialized && !Initialize())
            {
                throw new InvalidOperationException("TMDB Service Provider is not initialized. Call Initialize() first.");
            }

            return _tmdbDataService;
        }

        /// <summary>
        /// Disposes of all services.
        /// </summary>
        public void Dispose()
        {
            _tmdbService?.Dispose();
            _tmdbDataService?.Dispose();
        }
    }
}
