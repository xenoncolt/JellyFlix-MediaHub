using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Data.Handlers
{
    internal class CategoryRepository
    {
        public static int EnsureCategoryExists(CategoryType categoryType)
        {
            try
            {
                // Map enum to existing database CategoryName
                string dbCategoryName = MapCategoryTypeToDbName(categoryType);

                Console.WriteLine($"Looking for existing category: {dbCategoryName}");

                var checkParams = new Dictionary<string, object>
                {
                    { "CategoryName", dbCategoryName }
                };

                object result = DBOperations.ExecuteOperation(
                    DatabaseOperation.SELECT,
                    "ReleaseCategories",
                    checkParams,
                    "CategoryName = @CategoryName",
                    "Id");

                DataTable resultTable = (DataTable)result;

                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    int existingId = Convert.ToInt32(resultTable.Rows[0]["Id"]);
                    Console.WriteLine($"Found existing category '{dbCategoryName}' with ID: {existingId}");
                    return existingId;
                }
                else
                {
                    // Create new category if it doesn't exist (for categories like TopRated, Upcoming, etc.)
                    string displayName = GetCategoryName(categoryType);
                    string description = GetCategoryDescription(categoryType);

                    Console.WriteLine($"Creating new category: {dbCategoryName}");

                    var insertParams = new Dictionary<string, object>
                    {
                        { "CategoryName", dbCategoryName },
                        { "Name", displayName },
                        { "Description", description }
                    };

                    DBOperations.ExecuteOperation(
                        DatabaseOperation.INSERT,
                        "ReleaseCategories",
                        insertParams);

                    // Get the new ID
                    result = DBOperations.ExecuteOperation(
                        DatabaseOperation.SELECT,
                        "ReleaseCategories",
                        checkParams,
                        "CategoryName = @CategoryName",
                        "Id");

                    resultTable = (DataTable)result;
                    if (resultTable != null && resultTable.Rows.Count > 0)
                    {
                        int newId = Convert.ToInt32(resultTable.Rows[0]["Id"]);
                        Console.WriteLine($"Created new category '{dbCategoryName}' with ID: {newId}");
                        return newId;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring category exists for {categoryType}: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        private static string MapCategoryTypeToDbName(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.NowPlaying:
                    return "NewReleaseMovies"; // Maps to existing ID 1
                case CategoryType.Popular:
                    return "TopTrending"; // Maps to existing ID 3
                case CategoryType.TopRated:
                    return "TopRated"; // Will create new if needed
                case CategoryType.Upcoming:
                    return "Upcoming"; // Will create new if needed  
                case CategoryType.Favorite:
                    return "Favorites"; // Will create new if needed
                case CategoryType.Watchlist:
                    return "Watchlist"; // Will create new if needed
                default:
                    return categoryType.ToString();
            }
        }

        public static string GetCategoryName(CategoryType categoryType)
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

        public static string GetCategoryDescription(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Popular:
                    return "Most popular content";
                case CategoryType.TopRated:
                    return "Highest rated content";
                case CategoryType.Upcoming:
                    return "Upcoming releases";
                case CategoryType.NowPlaying:
                    return "Currently available content";
                case CategoryType.Favorite:
                    return "User favorite content";
                case CategoryType.Watchlist:
                    return "User watchlist content";
                default:
                    return $"Content in {categoryType} category";
            }
        }
    }
}