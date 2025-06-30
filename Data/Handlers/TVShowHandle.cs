using JellyFlix_MediaHub.Models.TMDB;
using JellyFlix_MediaHub.Data.Handlers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Data.Handlers
{
    internal class TVShowHandle
    {
        public static int SaveTVShow(TVShowModel tvShow)
        {
            try
            {
                Console.WriteLine($"Attempting to save TV show: {tvShow.Name}");

                // First save/update the TV show details
                int tvShowId = SaveTVShowDetails(tvShow);

                if (tvShowId > 0)
                {
                    // Save related entities
                    SaveTVShowGenres(tvShowId, tvShow.Genres);
                    SaveTVShowProductionCompanies(tvShowId, tvShow.ProductionCompanies);
                    SaveTVShowCast(tvShowId, tvShow.Cast);

                    Console.WriteLine($"Successfully saved TV show with ID: {tvShowId}");
                }

                return tvShowId;
            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error Number: {e.Number}");
                Console.WriteLine($"SQL Error: {e.Message}");
                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while saving TV show: {e.GetType().Name}: {e.Message}");
                Console.WriteLine($"Stack trace: {e.StackTrace}");
                return -1;
            }
        }

        private static int SaveTVShowDetails(TVShowModel tvShow)
        {
            // Check if TV show exists by TMDBId
            var checkParams = new Dictionary<string, object>
            {
                { "TMDBId", tvShow.TMDBId }
            };

            object result = DBOperations.ExecuteOperation(
                DatabaseOperation.SELECT,
                "TVShows",
                checkParams,
                "TMDBId = @TMDBId",
                "Id");

            DataTable resultTable = (DataTable)result;

            if (resultTable != null && resultTable.Rows.Count > 0)
            {
                // TV show exists, update it
                int tvShowId = Convert.ToInt32(resultTable.Rows[0]["Id"]);

                var updateParams = new Dictionary<string, object>
                {
                    { "Name", tvShow.Name },
                    { "Overview", tvShow.Overview },
                    { "FirstAirDate", tvShow.FirstAirDate },
                    { "LastAirDate", tvShow.LastAirDate },
                    { "NumberOfEpisodes", tvShow.NumberOfEpisodes },
                    { "NumberOfSeasons", tvShow.NumberOfSeasons },
                    { "EpisodeRunTime", tvShow.EpisodeRunTime },
                    { "VoteAverage", tvShow.VoteAverage },
                    { "VoteCount", tvShow.VoteCount },
                    { "Popularity", tvShow.Popularity },
                    { "PosterPath", tvShow.PosterPath },
                    { "BackdropPath", tvShow.BackdropPath },
                    { "OriginalLanguage", tvShow.OriginalLanguage },
                    { "OriginalName", tvShow.OriginalName },
                    { "Status", tvShow.Status },
                    { "Type", tvShow.Type },
                    { "HomePage", tvShow.HomePage },
                    { "InProduction", tvShow.InProduction },
                    { "UpdatedAt", DateTime.Now }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.UPDATE,
                    "TVShows",
                    updateParams,
                    $"Id = {tvShowId}");

                return tvShowId;
            }
            else
            {
                // TV show doesn't exist, insert it
                var insertParams = new Dictionary<string, object>
                {
                    { "TMDBId", tvShow.TMDBId },
                    { "Name", tvShow.Name },
                    { "Overview", tvShow.Overview },
                    { "FirstAirDate", tvShow.FirstAirDate },
                    { "LastAirDate", tvShow.LastAirDate },
                    { "NumberOfEpisodes", tvShow.NumberOfEpisodes },
                    { "NumberOfSeasons", tvShow.NumberOfSeasons },
                    { "EpisodeRunTime", tvShow.EpisodeRunTime },
                    { "VoteAverage", tvShow.VoteAverage },
                    { "VoteCount", tvShow.VoteCount },
                    { "Popularity", tvShow.Popularity },
                    { "PosterPath", tvShow.PosterPath },
                    { "BackdropPath", tvShow.BackdropPath },
                    { "OriginalLanguage", tvShow.OriginalLanguage },
                    { "OriginalName", tvShow.OriginalName },
                    { "Status", tvShow.Status },
                    { "Type", tvShow.Type },
                    { "HomePage", tvShow.HomePage },
                    { "InProduction", tvShow.InProduction },
                    { "CreatedAt", DateTime.Now },
                    { "UpdatedAt", DateTime.Now }
                };

                // Insert the TV show
                DBOperations.ExecuteOperation(DatabaseOperation.INSERT, "TVShows", insertParams);

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
                    "TVShows",
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

        // Genre handling - reusing from MovieHandle since the logic is the same
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

        private static void SaveTVShowGenres(int tvShowId, List<GenreModel> genres)
        {
            try
            {
                // First delete existing associations for this specific TV show
                var deleteParams = new Dictionary<string, object>
                {
                    { "TVShowId", tvShowId }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.DELETE,
                    "TVShowGenres",
                    deleteParams,
                    "TVShowId = @TVShowId");

                // Then create new associations
                foreach (var genre in genres)
                {
                    int genreId = SaveGenre(genre);
                    if (genreId > 0)
                    {
                        // Check if this specific association already exists
                        var checkParams = new Dictionary<string, object>
                        {
                            { "TVShowId", tvShowId },
                            { "GenreId", genreId }
                        };

                        object checkResult = DBOperations.ExecuteOperation(
                            DatabaseOperation.SELECT,
                            "TVShowGenres",
                            checkParams,
                            "TVShowId = @TVShowId AND GenreId = @GenreId",
                            "TVShowId");

                        DataTable checkTable = (DataTable)checkResult;
                        if (checkTable == null || checkTable.Rows.Count == 0)
                        {
                            // Association doesn't exist, create it
                            var insertParams = new Dictionary<string, object>
                            {
                                { "TVShowId", tvShowId },
                                { "GenreId", genreId }
                            };

                            DBOperations.ExecuteOperation(
                                DatabaseOperation.INSERT,
                                "TVShowGenres",
                                insertParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving TV show genres for TVShowId {tvShowId}: {ex.Message}");
            }
        }

        // Production company handling - reusing from MovieHandle since the logic is the same
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

                DBOperations.ExecuteOperation(DatabaseOperation.INSERT, "ProductionCompanies", insertParams);

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

        private static void SaveTVShowProductionCompanies(int tvShowId, List<ProductionCompanyModel> companies)
        {
            try
            {
                // First delete existing associations for this specific TV show
                var deleteParams = new Dictionary<string, object>
                {
                    { "TVShowId", tvShowId }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.DELETE,
                    "TVShowProductionCompanies",
                    deleteParams,
                    "TVShowId = @TVShowId");

                // Then create new associations
                foreach (var company in companies)
                {
                    int companyId = SaveProductionCompany(company);
                    if (companyId > 0)
                    {
                        // Check if this specific association already exists
                        var checkParams = new Dictionary<string, object>
                        {
                            { "TVShowId", tvShowId },
                            { "ProductionCompanyId", companyId }
                        };

                        object checkResult = DBOperations.ExecuteOperation(
                            DatabaseOperation.SELECT,
                            "TVShowProductionCompanies",
                            checkParams,
                            "TVShowId = @TVShowId AND ProductionCompanyId = @ProductionCompanyId",
                            "TVShowId");

                        DataTable checkTable = (DataTable)checkResult;
                        if (checkTable == null || checkTable.Rows.Count == 0)
                        {
                            // Association doesn't exist, create it
                            var insertParams = new Dictionary<string, object>
                            {
                                { "TVShowId", tvShowId },
                                { "ProductionCompanyId", companyId }
                            };

                            DBOperations.ExecuteOperation(
                                DatabaseOperation.INSERT,
                                "TVShowProductionCompanies",
                                insertParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving TV show production companies for TVShowId {tvShowId}: {ex.Message}");
            }
        }

        // Cast handling - reusing from MovieHandle since the logic is the same
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

        private static void SaveTVShowCast(int tvShowId, List<CastModel> cast)
        {
            try
            {
                // First delete existing associations for this specific TV show
                var deleteParams = new Dictionary<string, object>
                {
                    { "TVShowId", tvShowId }
                };

                DBOperations.ExecuteOperation(
                    DatabaseOperation.DELETE,
                    "TVShowCast",
                    deleteParams,
                    "TVShowId = @TVShowId");

                // Then create new associations
                foreach (var castMember in cast)
                {
                    int castId = SaveCast(castMember);
                    if (castId > 0)
                    {
                        // Check if this specific association already exists
                        var checkParams = new Dictionary<string, object>
                        {
                            { "TVShowId", tvShowId },
                            { "CastId", castId }
                        };

                        object checkResult = DBOperations.ExecuteOperation(
                            DatabaseOperation.SELECT,
                            "TVShowCast",
                            checkParams,
                            "TVShowId = @TVShowId AND CastId = @CastId",
                            "TVShowId");

                        DataTable checkTable = (DataTable)checkResult;
                        if (checkTable == null || checkTable.Rows.Count == 0)
                        {
                            // Association doesn't exist, create it
                            var insertParams = new Dictionary<string, object>
                            {
                                { "TVShowId", tvShowId },
                                { "CastId", castId },
                                { "Character", castMember.Character },
                                { "CreditId", castMember.CreditId },
                                { "OrderIndex", castMember.OrderIndex }
                            };

                            DBOperations.ExecuteOperation(
                                DatabaseOperation.INSERT,
                                "TVShowCast",
                                insertParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving TV show cast for TVShowId {tvShowId}: {ex.Message}");
            }
        }

        public static bool AddTVShowToCategory(int tvShowId, CategoryType categoryType)
        {
            try
            {
                if (tvShowId <= 0)
                {
                    Console.WriteLine($"Invalid TV show ID: {tvShowId}. Cannot add to category.");
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
            { "TVShowId", tvShowId },
            { "CategoryId", categoryId }
        };

                object result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "TVShowCategories",
                    checkParams,
                    "TVShowId = @TVShowId AND CategoryId = @CategoryId",
                    "TVShowId"); // Just need to check if any row exists

                DataTable resultTable = (DataTable)result;

                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    // Relationship already exists
                    Console.WriteLine($"TV show ID {tvShowId} already in category '{categoryType}' (Category ID: {categoryId})");
                    return true; // Return true because the relationship exists
                }

                // Relationship doesn't exist, create it
                var insertParams = new Dictionary<string, object>
        {
            { "TVShowId", tvShowId },
            { "CategoryId", categoryId },
            { "AddedDate", DateTime.UtcNow }
        };

                Console.WriteLine($"Adding TV show ID {tvShowId} to category '{categoryType}' (Category ID: {categoryId})");

                DBOperations.ExecuteOperation(
                    DatabaseOperation.INSERT,
                    "TVShowCategories",
                    insertParams);

                Console.WriteLine($"Successfully added TV show to category '{categoryType}'");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding TV show ID {tvShowId} to category '{categoryType}': {e.Message}");
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
                    return "Most popular TV shows";
                case CategoryType.TopRated:
                    return "Highest rated TV shows";
                case CategoryType.Upcoming:
                    return "Upcoming TV show releases";
                case CategoryType.NowPlaying:
                    return "Currently airing TV shows";
                case CategoryType.Favorite:
                    return "User favorite TV shows";
                case CategoryType.Watchlist:
                    return "User watchlist TV shows";
                default:
                    return $"TV shows in {categoryType} category";
            }
        }

        public static List<TVShowModel> GetTVShowsByCategory(CategoryType categoryType)
        {
            try
            {
                List<TVShowModel> tvShows = new List<TVShowModel>();

                var parameters = new Dictionary<string, object>
                {
                    { "CategoryId", (int)categoryType }
                };

                string sql = @"SELECT t.* FROM TVShows t
                               INNER JOIN TVShowCategories tc ON t.Id = tc.TVShowId
                               WHERE tc.CategoryId = @CategoryId
                               ORDER BY tc.AddedDate DESC";

                // Using direct SQL since we need a JOIN which isn't directly supported by ExecuteOperation
                DataTable result = DBOperations.ExecuteQuery(sql, parameters);

                if (result != null)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        TVShowModel tvShow = new TVShowModel
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            TMDBId = Convert.ToInt32(row["TMDBId"]),
                            Name = row["Name"] as string,
                            Overview = row["Overview"] as string,
                            FirstAirDate = row["FirstAirDate"] as DateTime?,
                            LastAirDate = row["LastAirDate"] as DateTime?,
                            NumberOfEpisodes = row["NumberOfEpisodes"] != DBNull.Value ? (int?)Convert.ToInt32(row["NumberOfEpisodes"]) : null,
                            NumberOfSeasons = row["NumberOfSeasons"] != DBNull.Value ? (int?)Convert.ToInt32(row["NumberOfSeasons"]) : null,
                            EpisodeRunTime = row["EpisodeRunTime"] as string,
                            VoteAverage = row["VoteAverage"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["VoteAverage"]) : null,
                            VoteCount = row["VoteCount"] != DBNull.Value ? (int?)Convert.ToInt32(row["VoteCount"]) : null,
                            Popularity = row["Popularity"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["Popularity"]) : null,
                            PosterPath = row["PosterPath"] as string,
                            BackdropPath = row["BackdropPath"] as string,
                            OriginalLanguage = row["OriginalLanguage"] as string,
                            OriginalName = row["OriginalName"] as string,
                            Status = row["Status"] as string,
                            Type = row["Type"] as string,
                            HomePage = row["HomePage"] as string,
                            InProduction = Convert.ToBoolean(row["InProduction"]),
                            CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(row["UpdatedAt"])
                        };

                        // Load related genres, production companies, and cast
                        tvShow.Genres = GetTVShowGenres(tvShow.Id);
                        tvShow.ProductionCompanies = GetTVShowProductionCompanies(tvShow.Id);
                        tvShow.Cast = GetTVShowCast(tvShow.Id);

                        tvShows.Add(tvShow);
                    }
                }

                return tvShows;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting TV shows by category: {e.Message}");
                return new List<TVShowModel>();
            }
        }

        private static List<GenreModel> GetTVShowGenres(int tvShowId)
        {
            List<GenreModel> genres = new List<GenreModel>();

            string sql = @"SELECT g.* FROM Genres g
                          INNER JOIN TVShowGenres tg ON g.Id = tg.GenreId
                          WHERE tg.TVShowId = @TVShowId";

            var parameters = new Dictionary<string, object>
            {
                { "TVShowId", tvShowId }
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

        private static List<ProductionCompanyModel> GetTVShowProductionCompanies(int tvShowId)
        {
            List<ProductionCompanyModel> companies = new List<ProductionCompanyModel>();

            string sql = @"SELECT pc.* FROM ProductionCompanies pc
                          INNER JOIN TVShowProductionCompanies tpc ON pc.Id = tpc.ProductionCompanyId
                          WHERE tpc.TVShowId = @TVShowId";

            var parameters = new Dictionary<string, object>
            {
                { "TVShowId", tvShowId }
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

        private static List<CastModel> GetTVShowCast(int tvShowId)
        {
            List<CastModel> cast = new List<CastModel>();

            string sql = @"SELECT c.*, tc.Character, tc.CreditId, tc.OrderIndex FROM Cast c
                          INNER JOIN TVShowCast tc ON c.Id = tc.CastId
                          WHERE tc.TVShowId = @TVShowId
                          ORDER BY tc.OrderIndex";

            var parameters = new Dictionary<string, object>
            {
                { "TVShowId", tvShowId }
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
