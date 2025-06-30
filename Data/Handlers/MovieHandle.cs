using JellyFlix_MediaHub.Models.TMDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace JellyFlix_MediaHub.Data.Handlers
{
    public enum CategoryType
    {
        Popular = 1,
        TopRated = 2,
        Upcoming = 3,
        NowPlaying = 4,
        Favorite = 5,
        Watchlist = 6
    }

    internal class MovieHandle
    {
        public static int SaveMovie(MovieModel movie)
        {
            try
            {
                Console.WriteLine($"Attempting to save movie: {movie.Title}");

                // First save/update the movie details
                int movieId = SaveMovieDetails(movie);

                if (movieId > 0)
                {
                    // Save related entities
                    SaveMovieGenres(movieId, movie.Genres);
                    SaveMovieProductionCompanies(movieId, movie.ProductionCompanies);
                    SaveMovieCast(movieId, movie.Cast);

                    Console.WriteLine($"Successfully saved movie with ID: {movieId}");
                }

                return movieId;
            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error Number: {e.Number}");
                Console.WriteLine($"SQL Error: {e.Message}");
                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while saving movie: {e.GetType().Name}: {e.Message}");
                Console.WriteLine($"Stack trace: {e.StackTrace}");
                return -1;
            }
        }

        private static int SaveMovieDetails(MovieModel movie)
        {
            // Check if movie exists by TMDBId
            var checkParams = new Dictionary<string, object>
            {
                { "TMDBId", movie.TMDBId }
            };

            object result = DBOperations.ExecuteOperation(
                DatabaseOperation.SELECT,
                "Movies",
                checkParams,
                "TMDBId = @TMDBId",
                "Id");

            DataTable resultTable = (DataTable)result;

            if (resultTable != null && resultTable.Rows.Count > 0)
            {
                // Movie exists, update it
                int movieId = Convert.ToInt32(resultTable.Rows[0]["Id"]);

                var updateParams = new Dictionary<string, object>
                {
                    { "Title", movie.Title },
                    { "Overview", movie.Overview },
                    { "ReleaseDate", movie.ReleaseDate },
                    { "Runtime", movie.Runtime },
                    { "VoteAverage", movie.VoteAverage },
                    { "VoteCount", movie.VoteCount },
                    { "Popularity", movie.Popularity },
                    { "PosterPath", movie.PosterPath },
                    { "BackdropPath", movie.BackdropPath },
                    { "OriginalLanguage", movie.OriginalLanguage },
                    { "OriginalTitle", movie.OriginalTitle },
                    { "Adult", movie.Adult },
                    { "Budget", movie.Budget },
                    { "Revenue", movie.Revenue },
                    { "Status", movie.Status },
                    { "Tagline", movie.Tagline },
                    { "HomePage", movie.HomePage },
                    { "ImdbId", movie.ImdbId },
                    { "UpdatedAt", DateTime.Now }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.UPDATE,
                    "Movies",
                    updateParams,
                    $"Id = {movieId}");

                return movieId;
            }
            else
            {
                // Movie doesn't exist, insert it
                var insertParams = new Dictionary<string, object>
                {
                    { "TMDBId", movie.TMDBId },
                    { "Title", movie.Title },
                    { "Overview", movie.Overview },
                    { "ReleaseDate", movie.ReleaseDate },
                    { "Runtime", movie.Runtime },
                    { "VoteAverage", movie.VoteAverage },
                    { "VoteCount", movie.VoteCount },
                    { "Popularity", movie.Popularity },
                    { "PosterPath", movie.PosterPath },
                    { "BackdropPath", movie.BackdropPath },
                    { "OriginalLanguage", movie.OriginalLanguage },
                    { "OriginalTitle", movie.OriginalTitle },
                    { "Adult", movie.Adult },
                    { "Budget", movie.Budget },
                    { "Revenue", movie.Revenue },
                    { "Status", movie.Status },
                    { "Tagline", movie.Tagline },
                    { "HomePage", movie.HomePage },
                    { "ImdbId", movie.ImdbId },
                    { "CreatedAt", DateTime.Now },
                    { "UpdatedAt", DateTime.Now }
                };

                // Insert the movie
                DBOperations.ExecuteOperation(DatabaseOperation.INSERT, "Movies", insertParams);

                // Get the inserted ID using SCOPE_IDENTITY()
                string identitySql = "SELECT SCOPE_IDENTITY() as LastId";
                DataTable identityTable = DBOperations.ExecuteQuery(identitySql);
                
                if (identityTable != null && identityTable.Rows.Count > 0 && identityTable.Rows[0]["LastId"] != DBNull.Value)
                {
                    return Convert.ToInt32(identityTable.Rows[0]["LastId"]);
                }

                // Fallback: Get by TMDBId
                result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "Movies",
                    checkParams,
                    "TMDBId = @TMDBId",
                    "Id");

                resultTable = (DataTable)result;
                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(resultTable.Rows[0]["Id"]);
                }
                return -1;
            }
        }

        private static int SaveGenre(GenreModel genre)
        {
            // Check if genre exists
            var checkParams = new Dictionary<string, object>
            {
                { "TMDBId", genre.TMDBId }
            };

            object result = DBOperations.ExecuteOperation(
                DatabaseOperation.SELECT,
                "Genres",
                checkParams,
                "TMDBId = @TMDBId",
                "Id");

            DataTable resultTable = (DataTable)result;

            if (resultTable != null && resultTable.Rows.Count > 0)
            {
                // Genre exists, update it
                int genreId = Convert.ToInt32(resultTable.Rows[0]["Id"]);

                var updateParams = new Dictionary<string, object>
                {
                    { "Name", genre.Name }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.UPDATE,
                    "Genres",
                    updateParams,
                    $"Id = {genreId}");

                return genreId;
            }
            else
            {
                // Genre doesn't exist, insert it
                var insertParams = new Dictionary<string, object>
                {
                    { "TMDBId", genre.TMDBId },
                    { "Name", genre.Name }
                };

                DBOperations.ExecuteOperation(DatabaseOperation.INSERT, "Genres", insertParams);

                // Get the inserted ID
                result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "Genres",
                    checkParams,
                    "TMDBId = @TMDBId",
                    "Id");

                resultTable = (DataTable)result;
                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(resultTable.Rows[0]["Id"]);
                }
                return -1;
            }
        }

        private static void SaveMovieGenres(int movieId, List<GenreModel> genres)
        {
            try
            {
                // First delete existing associations for this specific movie
                var deleteParams = new Dictionary<string, object>
                {
                    { "MovieId", movieId }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.DELETE,
                    "MovieGenres",
                    deleteParams,
                    "MovieId = @MovieId");

                // Then create new associations
                foreach (var genre in genres)
                {
                    int genreId = SaveGenre(genre);
                    if (genreId > 0)
                    {
                        // Check if this specific association already exists
                        var checkParams = new Dictionary<string, object>
                        {
                            { "MovieId", movieId },
                            { "GenreId", genreId }
                        };

                        object checkResult = DBOperations.ExecuteOperation(
                            DatabaseOperation.SELECT,
                            "MovieGenres",
                            checkParams,
                            "MovieId = @MovieId AND GenreId = @GenreId",
                            "MovieId");

                        DataTable checkTable = (DataTable)checkResult;
                        if (checkTable == null || checkTable.Rows.Count == 0)
                        {
                            // Association doesn't exist, create it
                            var insertParams = new Dictionary<string, object>
                            {
                                { "MovieId", movieId },
                                { "GenreId", genreId }
                            };

                            DBOperations.ExecuteOperation(
                                DatabaseOperation.INSERT,
                                "MovieGenres",
                                insertParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving movie genres for MovieId {movieId}: {ex.Message}");
            }
        }

        private static int SaveProductionCompany(ProductionCompanyModel company)
        {
            // Check if company exists
            var checkParams = new Dictionary<string, object>
            {
                { "TMDBId", company.TMDBId }
            };

            object result = DBOperations.ExecuteOperation(
                DatabaseOperation.SELECT,
                "ProductionCompanies",
                checkParams,
                "TMDBId = @TMDBId",
                "Id");

            DataTable resultTable = (DataTable)result;

            if (resultTable != null && resultTable.Rows.Count > 0)
            {
                // Company exists, update it
                int companyId = Convert.ToInt32(resultTable.Rows[0]["Id"]);

                var updateParams = new Dictionary<string, object>
                {
                    { "Name", company.Name },
                    { "LogoPath", company.LogoPath },
                    { "OriginCountry", company.OriginCountry }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.UPDATE,
                    "ProductionCompanies",
                    updateParams,
                    $"Id = {companyId}");

                return companyId;
            }
            else
            {
                // Company doesn't exist, insert it
                var insertParams = new Dictionary<string, object>
                {
                    { "TMDBId", company.TMDBId },
                    { "Name", company.Name },
                    { "LogoPath", company.LogoPath },
                    { "OriginCountry", company.OriginCountry }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.INSERT,
                    "ProductionCompanies",
                    insertParams);

                // Get the inserted ID
                result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "ProductionCompanies",
                    checkParams,
                    "TMDBId = @TMDBId",
                    "Id");

                resultTable = (DataTable)result;
                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(resultTable.Rows[0]["Id"]);
                }
                return -1;
            }
        }

        private static void SaveMovieProductionCompanies(int movieId, List<ProductionCompanyModel> companies)
        {
            try
            {
                // First delete existing associations for this specific movie
                var deleteParams = new Dictionary<string, object>
                {
                    { "MovieId", movieId }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.DELETE,
                    "MovieProductionCompanies",
                    deleteParams,
                    "MovieId = @MovieId");

                // Then create new associations
                foreach (var company in companies)
                {
                    int companyId = SaveProductionCompany(company);
                    if (companyId > 0)
                    {
                        // Check if this specific association already exists
                        var checkParams = new Dictionary<string, object>
                        {
                            { "MovieId", movieId },
                            { "ProductionCompanyId", companyId }
                        };

                        object checkResult = DBOperations.ExecuteOperation(
                            DatabaseOperation.SELECT,
                            "MovieProductionCompanies",
                            checkParams,
                            "MovieId = @MovieId AND ProductionCompanyId = @ProductionCompanyId",
                            "MovieId");

                        DataTable checkTable = (DataTable)checkResult;
                        if (checkTable == null || checkTable.Rows.Count == 0)
                        {
                            // Association doesn't exist, create it
                            var insertParams = new Dictionary<string, object>
                            {
                                { "MovieId", movieId },
                                { "ProductionCompanyId", companyId }
                            };

                            DBOperations.ExecuteOperation(
                                DatabaseOperation.INSERT,
                                "MovieProductionCompanies",
                                insertParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving movie production companies for MovieId {movieId}: {ex.Message}");
            }
        }

        private static int SaveCast(CastModel castMember)
        {
            // Check if cast member exists
            var checkParams = new Dictionary<string, object>
            {
                { "TMDBId", castMember.TMDBId }
            };

            object result = DBOperations.ExecuteOperation(
                DatabaseOperation.SELECT,
                "Cast",
                checkParams,
                "TMDBId = @TMDBId",
                "Id");

            DataTable resultTable = (DataTable)result;

            if (resultTable != null && resultTable.Rows.Count > 0)
            {
                // Cast member exists, update it
                int castId = Convert.ToInt32(resultTable.Rows[0]["Id"]);

                var updateParams = new Dictionary<string, object>
                {
                    { "Name", castMember.Name },
                    { "ProfilePath", castMember.ProfilePath },
                    { "Gender", castMember.Gender }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.UPDATE,
                    "Cast",
                    updateParams,
                    $"Id = {castId}");

                return castId;
            }
            else
            {
                // Cast member doesn't exist, insert it
                var insertParams = new Dictionary<string, object>
                {
                    { "TMDBId", castMember.TMDBId },
                    { "Name", castMember.Name },
                    { "ProfilePath", castMember.ProfilePath },
                    { "Gender", castMember.Gender }
                };

                DBOperations.ExecuteOperation(DatabaseOperation.INSERT, "Cast", insertParams);

                // Get the inserted ID
                result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "Cast",
                    checkParams,
                    "TMDBId = @TMDBId",
                    "Id");

                resultTable = (DataTable)result;
                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(resultTable.Rows[0]["Id"]);
                }
                return -1;
            }
        }

        private static void SaveMovieCast(int movieId, List<CastModel> cast)
        {
            try
            {
                // First delete existing associations for this specific movie
                var deleteParams = new Dictionary<string, object>
                {
                    { "MovieId", movieId }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.DELETE,
                    "MovieCast",
                    deleteParams,
                    "MovieId = @MovieId");

                // Then create new associations
                foreach (var castMember in cast)
                {
                    int castId = SaveCast(castMember);
                    if (castId > 0)
                    {
                        // Check if this specific association already exists
                        var checkParams = new Dictionary<string, object>
                        {
                            { "MovieId", movieId },
                            { "CastId", castId }
                        };

                        object checkResult = DBOperations.ExecuteOperation(
                            DatabaseOperation.SELECT,
                            "MovieCast",
                            checkParams,
                            "MovieId = @MovieId AND CastId = @CastId",
                            "MovieId");

                        DataTable checkTable = (DataTable)checkResult;
                        if (checkTable == null || checkTable.Rows.Count == 0)
                        {
                            // Association doesn't exist, create it
                            var insertParams = new Dictionary<string, object>
                            {
                                { "MovieId", movieId },
                                { "CastId", castId },
                                { "Character", castMember.Character },
                                { "CreditId", castMember.CreditId },
                                { "OrderIndex", castMember.OrderIndex }
                            };

                            DBOperations.ExecuteOperation(
                                DatabaseOperation.INSERT,
                                "MovieCast",
                                insertParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving movie cast for MovieId {movieId}: {ex.Message}");
            }
        }

        public static bool AddMovieToCategory(int movieId, CategoryType categoryType)
        {
            try
            {
                if (movieId <= 0)
                {
                    Console.WriteLine($"Invalid movie ID: {movieId}. Cannot add to category.");
                    return false;
                }

                int categoryId = CategoryRepository.EnsureCategoryExists(categoryType);
                if (categoryId <= 0)
                {
                    Console.WriteLine($"Failed to ensure category exists: {categoryType}");
                    return false;
                }

                // CHECK IF RELATIONSHIP ALREADY EXISTS BEFORE INSERTING
                var checkParams = new Dictionary<string, object>
        {
            { "MovieId", movieId },
            { "CategoryId", categoryId }
        };

                object result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "MovieCategories",
                    checkParams,
                    "MovieId = @MovieId AND CategoryId = @CategoryId",
                    "MovieId"); // Just need to check if any row exists

                DataTable resultTable = (DataTable)result;

                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    // Relationship already exists
                    Console.WriteLine($"Movie ID {movieId} already in category '{categoryType}' (Category ID: {categoryId})");
                    return true; // Return true because the relationship exists
                }

                // Relationship doesn't exist, create it
                var insertParams = new Dictionary<string, object>
        {
            { "MovieId", movieId },
            { "CategoryId", categoryId },
            { "AddedDate", DateTime.UtcNow }
        };

                Console.WriteLine($"Adding movie ID {movieId} to category '{categoryType}' (Category ID: {categoryId})");

                DBOperations.ExecuteOperation(
                    DatabaseOperation.INSERT,
                    "MovieCategories",
                    insertParams);

                Console.WriteLine($"Successfully added movie to category '{categoryType}'");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding movie ID {movieId} to category '{categoryType}': {e.Message}");
                return false;
            }
        }

        private static void EnsureCategoryExists(CategoryType categoryType)
        {
            try
            {
                // Check if category exists in ReleaseCategories
                var checkParams = new Dictionary<string, object>
                {
                    { "Id", (int)categoryType }
                };

                object result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "ReleaseCategories",
                    checkParams,
                    "Id = @Id",
                    "Id");

                DataTable resultTable = (DataTable)result;

                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    // Category doesn't exist, create it
                    var insertParams = new Dictionary<string, object>
                    {
                        { "Id", (int)categoryType },
                        { "Name", GetCategoryName(categoryType) },
                        { "Description", GetCategoryDescription(categoryType) }
                    };

                    DBOperations.ExecuteOperation(
                        DatabaseOperation.INSERT,
                        "ReleaseCategories",
                        insertParams);

                    Console.WriteLine($"Created category: {categoryType} with ID {(int)categoryType}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring category exists: {ex.Message}");
            }
        }

        private static string GetCategoryName(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Popular:
                    return "Popular";
                case CategoryType.TopRated:
                    return "Top Rated";
                case CategoryType.Upcoming:
                    return "Upcoming";
                case CategoryType.NowPlaying:
                    return "Now Playing";
                case CategoryType.Favorite:
                    return "Favorites";
                case CategoryType.Watchlist:
                    return "Watchlist";
                default:
                    return categoryType.ToString();
            }
        }

        private static string GetCategoryDescription(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Popular:
                    return "Most popular movies";
                case CategoryType.TopRated:
                    return "Highest rated movies";
                case CategoryType.Upcoming:
                    return "Upcoming movie releases";
                case CategoryType.NowPlaying:
                    return "Currently playing in theaters";
                case CategoryType.Favorite:
                    return "User favorite movies";
                case CategoryType.Watchlist:
                    return "User watchlist movies";
                default:
                    return $"Movies in {categoryType} category";
            }
        }

        public static List<MovieModel> GetMoviesByCategory(CategoryType categoryType)
        {
            try
            {
                List<MovieModel> movies = new List<MovieModel>();

                var parameters = new Dictionary<string, object>
                {
                    { "CategoryId", (int)categoryType }
                };

                string sql = @"SELECT m.* FROM Movies m
                               INNER JOIN MovieCategories mc ON m.Id = mc.MovieId
                               WHERE mc.CategoryId = @CategoryId
                               ORDER BY mc.AddedDate DESC";

                // Using direct SQL since we need a JOIN which isn't directly supported by ExecuteOperation
                DataTable result = DBOperations.ExecuteQuery(sql, parameters);

                if (result != null)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        MovieModel movie = new MovieModel
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            TMDBId = Convert.ToInt32(row["TMDBId"]),
                            Title = row["Title"] as string,
                            Overview = row["Overview"] as string,
                            ReleaseDate = row["ReleaseDate"] as DateTime?,
                            Runtime = row["Runtime"] != DBNull.Value ? (int?)Convert.ToInt32(row["Runtime"]) : null,
                            VoteAverage = row["VoteAverage"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["VoteAverage"]) : null,
                            VoteCount = row["VoteCount"] != DBNull.Value ? (int?)Convert.ToInt32(row["VoteCount"]) : null,
                            Popularity = row["Popularity"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["Popularity"]) : null,
                            PosterPath = row["PosterPath"] as string,
                            BackdropPath = row["BackdropPath"] as string,
                            OriginalLanguage = row["OriginalLanguage"] as string,
                            OriginalTitle = row["OriginalTitle"] as string,
                            Adult = Convert.ToBoolean(row["Adult"]),
                            Budget = row["Budget"] != DBNull.Value ? (long?)Convert.ToInt64(row["Budget"]) : null,
                            Revenue = row["Revenue"] != DBNull.Value ? (long?)Convert.ToInt64(row["Revenue"]) : null,
                            Status = row["Status"] as string,
                            Tagline = row["Tagline"] as string,
                            HomePage = row["HomePage"] as string,
                            ImdbId = row["ImdbId"] as string,
                            CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(row["UpdatedAt"])
                        };

                        // Load related genres, production companies, and cast
                        movie.Genres = GetMovieGenres(movie.Id);
                        movie.ProductionCompanies = GetMovieProductionCompanies(movie.Id);
                        movie.Cast = GetMovieCast(movie.Id);

                        movies.Add(movie);
                    }
                }

                return movies;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting movies by category: {e.Message}");
                return new List<MovieModel>();
            }
        }

        private static List<GenreModel> GetMovieGenres(int movieId)
        {
            List<GenreModel> genres = new List<GenreModel>();

            string sql = @"SELECT g.* FROM Genres g
                          INNER JOIN MovieGenres mg ON g.Id = mg.GenreId
                          WHERE mg.MovieId = @MovieId";

            var parameters = new Dictionary<string, object>
            {
                { "MovieId", movieId }
            };

            DataTable result = DBOperations.ExecuteQuery(sql, parameters);

            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    genres.Add(new GenreModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        TMDBId = Convert.ToInt32(row["TMDBId"]),
                        Name = row["Name"] as string
                    });
                }
            }

            return genres;
        }

        private static List<ProductionCompanyModel> GetMovieProductionCompanies(int movieId)
        {
            List<ProductionCompanyModel> companies = new List<ProductionCompanyModel>();

            string sql = @"SELECT pc.* FROM ProductionCompanies pc
                          INNER JOIN MovieProductionCompanies mpc ON pc.Id = mpc.ProductionCompanyId
                          WHERE mpc.MovieId = @MovieId";

            var parameters = new Dictionary<string, object>
            {
                { "MovieId", movieId }
            };

            DataTable result = DBOperations.ExecuteQuery(sql, parameters);

            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    companies.Add(new ProductionCompanyModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        TMDBId = Convert.ToInt32(row["TMDBId"]),
                        Name = row["Name"] as string,
                        LogoPath = row["LogoPath"] as string,
                        OriginCountry = row["OriginCountry"] as string
                    });
                }
            }

            return companies;
        }

        private static List<CastModel> GetMovieCast(int movieId)
        {
            List<CastModel> cast = new List<CastModel>();

            string sql = @"SELECT c.*, mc.Character, mc.CreditId, mc.OrderIndex FROM Cast c
                          INNER JOIN MovieCast mc ON c.Id = mc.CastId
                          WHERE mc.MovieId = @MovieId
                          ORDER BY mc.OrderIndex";

            var parameters = new Dictionary<string, object>
            {
                { "MovieId", movieId }
            };

            DataTable result = DBOperations.ExecuteQuery(sql, parameters);

            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    cast.Add(new CastModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        TMDBId = Convert.ToInt32(row["TMDBId"]),
                        Name = row["Name"] as string,
                        ProfilePath = row["ProfilePath"] as string,
                        Gender = row["Gender"] != DBNull.Value ? (int?)Convert.ToInt32(row["Gender"]) : null,
                        Character = row["Character"] as string,
                        CreditId = row["CreditId"] as string,
                        OrderIndex = row["OrderIndex"] != DBNull.Value ? (int?)Convert.ToInt32(row["OrderIndex"]) : null
                    });
                }
            }

            return cast;
        }
    }
}