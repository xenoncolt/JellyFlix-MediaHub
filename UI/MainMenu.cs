using Guna.UI2.WinForms;
using JellyFlix_MediaHub.Data.Handlers;
using JellyFlix_MediaHub.Models;
using JellyFlix_MediaHub.Models.TMDB;
using JellyFlix_MediaHub.Services;
using JellyFlix_MediaHub.Services.Prowlarr;
using JellyFlix_MediaHub.Services.TMDB;
using JellyFlix_MediaHub.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JellyFlix_MediaHub.UI
{
    public partial class MainMenu : Form
    {
        private readonly User currentUser;
        private readonly AvatarImage avatar_img;
        private List<User> all_users;
        private TMDBServiceProvider TMDB_provider;

        private List<MovieModel> loaded_movies;
        private List<TVShowModel> loaded_series;
        private List<object> loaded_trending;
        private int displayed_movies = 0;
        private int displayed_series = 0;
        private int displayed_trending = 0;

        private int total_movies_fetched = 50;
        private int total_series_fetched = 50;
        private int total_trending_fetched = 50;

        private int MAX_INITIAL_ITEMS = 15;
        private const int GRID_CARD_WIDTH = 160;
        private const int GRID_CARD_HEIGHT = 280;
        private const int GRID_CARD_MARGIN = 12;

        private Guna2ProgressBar fetchProgressBar;
        private Guna2HtmlLabel progressLabel;
        private bool isSearchMode = false;
        private List<object> currentSearchResults;
        private string lastSearchQuery = "";
        private List<object> originalMovies;
        private List<object> originalSeries;
        private List<object> originalTrending;

        public MainMenu(User user)
        {
            InitializeComponent();
            currentUser = user;
            avatar_img = new AvatarImage(currentUser.Username);
            this.SizeChanged += MainMenu_SizeChanged;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                if (currentUser.Role == "user")
                {
                    NavMenu.TabPages.Remove(InvitesPage);
                    NavMenu.TabPages.Remove(UsersTab);
                }
                else if (currentUser.Role == "premium")
                {
                    NavMenu.TabPages.Remove(UsersTab);
                }
            }

            if (avatar_img.LoadUserAvatar() != null)
            {
                ProfileBox.Image = avatar_img.LoadUserAvatar();
            }

            SearchBox.KeyDown += SearchBox_KeyDown;

            LoadAllUsers();
            LoadApiConfig();
            TMDB_provider = TMDBServiceProvider.Instance;
            TMDB_provider.Initialize();

            NavMenu.SelectedIndexChanged += async (s, args) =>
            {
                UpdateSearchBox();

                if (isSearchMode && !string.IsNullOrEmpty(lastSearchQuery))
                {
                    await HandleSearchAsync(lastSearchQuery);
                    return;
                }

                if (NavMenu.SelectedTab == MovieTab && loaded_movies == null)
                {
                    await LoadMoviesDirectly();
                }
                else if (NavMenu.SelectedTab == MovieTab && loaded_movies != null)
                {
                    DisplayMoviesGrid();
                }
                else if (NavMenu.SelectedTab == SeriesTab && loaded_series == null)
                {
                    await LoadSeriesDirectly();
                }
                else if (NavMenu.SelectedTab == SeriesTab && loaded_series != null)
                {
                    DisplaySeriesGrid();
                }
                else if (NavMenu.SelectedTab == TrendingTab && loaded_trending == null)
                {
                    await LoadTrendingDirectly();
                }
                else if (NavMenu.SelectedTab == TrendingTab && loaded_trending != null)
                {
                    DisplayTrendingGrid();
                }
            };

            if (NavMenu.SelectedTab == MovieTab)
            {
                _ = LoadMoviesDirectly();
            }

            if (currentUser.Role == "admin" && !AreApiKeysConfigured())
            {
                MsgBox.Caption = "API Configuration Required";
                MsgBox.Text = "Please configure the TMDB and Prowlarr API keys in the Server Config section to enable full functionality...";
                MsgBox.Icon = MessageDialogIcon.Information;
                MsgBox.Buttons = MessageDialogButtons.OK;

                if (MsgBox.Show() == DialogResult.OK)
                {
                    NavMenu.SelectedTab = SettingsTab;
                    SettingsMenu.SelectedTab = ServerSettingsPage;
                }
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchQuery = SearchBox.Text.Trim();
                if (string.IsNullOrEmpty(searchQuery))
                {
                    RestoreOriginalContent();
                    return;
                }
                _ = HandleSearchAsync(searchQuery);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                RestoreOriginalContent();
                SearchBox.Clear();
            }
        }

        private void UpdateSearchBox()
        {
            if (NavMenu.SelectedTab == MovieTab || NavMenu.SelectedTab == SeriesTab || NavMenu.SelectedTab == TrendingTab)
            {
                SearchBox.Visible = true;
                SearchBox.BringToFront();
            }
            else
            {
                SearchBox.Visible = false;
            }
        }

        private async Task HandleSearchAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                isSearchMode = false;
                currentSearchResults = null;
                lastSearchQuery = "";
                RestoreOriginalContent();
                return;
            }

            if (TMDB_provider == null || !TMDB_provider.Initialize())
            {
                MessageBox.Show("TMDB service not initialized. Please configure TMDB API key first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!isSearchMode)
                {
                    StoreOriginalContent();
                }

                Cursor = Cursors.WaitCursor;
                isSearchMode = true;
                lastSearchQuery = searchQuery;

                ShowSearchingProgress(NavMenu.SelectedTab, $"Searching for '{searchQuery}'...");

                var tmdbService = TMDB_provider.GetTMDBService();
                List<object> searchResults = new List<object>();

                if (NavMenu.SelectedTab == MovieTab)
                {
                    var movies = await SearchMovies(tmdbService, searchQuery);
                    searchResults = movies.Cast<object>().ToList();
                    loaded_movies = movies;
                }
                else if (NavMenu.SelectedTab == SeriesTab)
                {
                    var series = await SearchTVShows(tmdbService, searchQuery);
                    searchResults = series.Cast<object>().ToList();
                    loaded_series = series;
                }
                else if (NavMenu.SelectedTab == TrendingTab)
                {
                    var movies = await SearchMovies(tmdbService, searchQuery);
                    var series = await SearchTVShows(tmdbService, searchQuery);
                    searchResults.AddRange(movies.Cast<object>());
                    searchResults.AddRange(series.Cast<object>());
                    loaded_trending = searchResults;
                }

                currentSearchResults = searchResults;
                DisplaySearchResults(NavMenu.SelectedTab, searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search failed: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Search error: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task<List<MovieModel>> SearchMovies(TMDBService tmdbService, string query)
        {
            var movies = new List<MovieModel>();

            try
            {
                var config = ConfigManager.LoadConfig();
                string apiKey = config.TmdbApiKey;

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1";
                    var response = await httpClient.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<TMDBMovieResponse>(response);

                    if (result?.Results != null)
                    {
                        int count = 0;
                        foreach (var movieData in result.Results.Take(20))
                        {
                            try
                            {
                                var movie = await tmdbService.GetMovieDetailsAsync(movieData.Id);
                                if (movie != null)
                                {
                                    movies.Add(movie);
                                    count++;
                                    UpdateSearchProgress(count, Math.Min(20, result.Results.Count), $"Found movie: {movie.Title}");
                                    await Task.Delay(100);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error fetching movie details: {ex.Message}");
                            }
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

        private async Task<List<TVShowModel>> SearchTVShows(TMDBService tmdbService, string query)
        {
            var series = new List<TVShowModel>();

            try
            {
                var config = ConfigManager.LoadConfig();
                string apiKey = config.TmdbApiKey;

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var url = $"https://api.themoviedb.org/3/search/tv?api_key={apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1";
                    var response = await httpClient.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<TMDBTVResponse>(response);

                    if (result?.Results != null)
                    {
                        int count = 0;
                        foreach (var tvData in result.Results.Take(20))
                        {
                            try
                            {
                                var tvShow = await tmdbService.GetTVShowDetailsAsync(tvData.Id);
                                if (tvShow != null)
                                {
                                    series.Add(tvShow);
                                    count++;
                                    UpdateSearchProgress(count, Math.Min(20, result.Results.Count), $"Found series: {tvShow.Name}");
                                    await Task.Delay(100);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error fetching TV show details: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching TV shows: {ex.Message}");
            }

            return series;
        }

        private void StoreOriginalContent()
        {
            if (NavMenu.SelectedTab == MovieTab && loaded_movies != null)
                originalMovies = loaded_movies.Cast<object>().ToList();
            else if (NavMenu.SelectedTab == SeriesTab && loaded_series != null)
                originalSeries = loaded_series.Cast<object>().ToList();
            else if (NavMenu.SelectedTab == TrendingTab && loaded_trending != null)
                originalTrending = loaded_trending.ToList();
        }

        private void RestoreOriginalContent()
        {
            if (!isSearchMode) return;

            isSearchMode = false;
            lastSearchQuery = "";
            SearchBox.Clear();

            if (NavMenu.SelectedTab == MovieTab && originalMovies != null)
            {
                loaded_movies = originalMovies.Cast<MovieModel>().ToList();
                DisplayMoviesGrid();
            }
            else if (NavMenu.SelectedTab == SeriesTab && originalSeries != null)
            {
                loaded_series = originalSeries.Cast<TVShowModel>().ToList();
                DisplaySeriesGrid();
            }
            else if (NavMenu.SelectedTab == TrendingTab && originalTrending != null)
            {
                loaded_trending = originalTrending.ToList();
                DisplayTrendingGrid();
            }

            Console.WriteLine("Restored original content");
        }

        private void ShowSearchingProgress(TabPage tab, string message)
        {
            tab.Controls.Clear();

            Guna2HtmlLabel searchingLabel = new Guna2HtmlLabel
            {
                Text = message,
                Location = new Point(50, 60),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Comic Sans MS", 14, FontStyle.Bold)
            };

            fetchProgressBar = new Guna2ProgressBar
            {
                Location = new Point(50, 110),
                Size = new Size(400, 20),
                ProgressColor = Color.FromArgb(255, 193, 7),
                ProgressColor2 = Color.FromArgb(255, 152, 0),
                BorderRadius = 10,
                Value = 0
            };

            progressLabel = new Guna2HtmlLabel
            {
                Text = "Initializing search...",
                Location = new Point(50, 140),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Comic Sans MS", 10)
            };

            tab.Controls.Add(searchingLabel);
            tab.Controls.Add(fetchProgressBar);
            tab.Controls.Add(progressLabel);
            tab.Refresh();
        }

        private void UpdateSearchProgress(int current, int total, string message)
        {
            if (fetchProgressBar != null && progressLabel != null)
            {
                int percentage = total > 0 ? (int)((double)current / total * 100) : 0;
                fetchProgressBar.Value = percentage;
                progressLabel.Text = message;
                Application.DoEvents();
            }
        }

        private void DisplaySearchResults(TabPage tab, List<object> results)
        {
            tab.Controls.Clear();

            if (results == null || results.Count == 0)
            {
                ShowNoSearchResults(tab, lastSearchQuery);
                return;
            }

            Guna2Panel scrollPanel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                FillColor = Color.Transparent
            };

            tab.Controls.Add(scrollPanel);

            Guna2HtmlLabel searchHeader = new Guna2HtmlLabel
            {
                Text = $"Search Results for '{lastSearchQuery}' ({results.Count} found)",
                Location = new Point(20, 50),
                AutoSize = true,
                ForeColor = Color.FromArgb(255, 193, 7),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            scrollPanel.Controls.Add(searchHeader);

            int availableWidth = tab.ClientSize.Width - 40;
            int itemsPerRow = Math.Max(4, availableWidth / (GRID_CARD_WIDTH + GRID_CARD_MARGIN));

            for (int i = 0; i < results.Count; i++)
            {
                int row = i / itemsPerRow;
                int col = i % itemsPerRow;
                int x = GRID_CARD_MARGIN + col * (GRID_CARD_WIDTH + GRID_CARD_MARGIN);
                int y = 80 + row * (GRID_CARD_HEIGHT + GRID_CARD_MARGIN);

                if (tab == MovieTab)
                    CreateMovieGridCard(results[i], x, y);
                else if (tab == SeriesTab)
                    CreateSeriesGridCard(results[i], x, y);
                else if (tab == TrendingTab)
                    CreateTrendingGridCard(results[i], x, y);
            }
        }

        private void ShowNoSearchResults(TabPage tab, string query)
        {
            Guna2HtmlLabel noResultsLabel = new Guna2HtmlLabel
            {
                Text = $"No results found for '{query}'",
                Location = new Point(50, 60),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Comic Sans MS", 12, FontStyle.Bold)
            };

            Guna2Button backButton = new Guna2Button
            {
                Text = "Back to Browse",
                Location = new Point(50, 110),
                Size = new Size(150, 40),
                BorderRadius = 10,
                FillColor = Color.FromArgb(94, 148, 255),
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            backButton.Click += (s, e) => {
                RestoreOriginalContent();
            };

            tab.Controls.Add(noResultsLabel);
            tab.Controls.Add(backButton);
            SearchBox.BringToFront();
        }

        private void DisplayMoviesGrid()
        {
            DisplayGridContent(MovieTab, loaded_movies.Cast<object>().ToList(), ref displayed_movies, "movies", CreateMovieGridCard);
        }

        private void DisplaySeriesGrid()
        {
            DisplayGridContent(GetSeriesTab(), loaded_series.Cast<object>().ToList(), ref displayed_series, "series", CreateSeriesGridCard);
        }

        private void DisplayTrendingGrid()
        {
            DisplayGridContent(GetTrendingTab(), loaded_trending, ref displayed_trending, "trending items", CreateTrendingGridCard);
        }

        private void DisplayGridContent(TabPage tab, List<object> items, ref int displayedCount, string itemType, Action<object, int, int> createCardMethod)
        {
            Console.WriteLine($"DisplayGridContent called with {items.Count} {itemType}");

            tab.Controls.Clear();
            displayedCount = 0;

            if (items == null || items.Count == 0)
            {
                ShowNoContentMessage(tab, $"No {itemType} available.");
                return;
            }

            Guna2Panel scrollPanel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                FillColor = Color.Transparent
            };

            tab.Controls.Add(scrollPanel);

            int availableWidth = tab.ClientSize.Width - 40;
            int itemsPerRow = Math.Max(4, availableWidth / (GRID_CARD_WIDTH + GRID_CARD_MARGIN));

            int initialItemsToShow = Math.Min(MAX_INITIAL_ITEMS, items.Count);
            Console.WriteLine($"Will display {initialItemsToShow} {itemType} initially");

            int startY = 50;

            for (int i = 0; i < initialItemsToShow; i++)
            {
                int row = i / itemsPerRow;
                int col = i % itemsPerRow;
                int x = GRID_CARD_MARGIN + col * (GRID_CARD_WIDTH + GRID_CARD_MARGIN);
                int y = startY + row * (GRID_CARD_HEIGHT + GRID_CARD_MARGIN);

                createCardMethod(items[i], x, y);
                displayedCount++;
            }

            if (displayedCount < items.Count || CanFetchMoreFromTMDB(tab))
            {
                int totalRows = (int)Math.Ceiling((double)initialItemsToShow / itemsPerRow);
                int loadMoreY = startY + totalRows * (GRID_CARD_HEIGHT + GRID_CARD_MARGIN) + 40;
                CreateLoadMoreButton(scrollPanel, loadMoreY, availableWidth, itemType, items, displayedCount, createCardMethod, tab);
            }

            Console.WriteLine($"DisplayGridContent completed. Total displayed: {displayedCount}");
        }

        private bool CanFetchMoreFromTMDB(TabPage tab)
        {
            if (isSearchMode) return false;

            if (tab == MovieTab)
                return total_movies_fetched < 500;
            else if (tab == SeriesTab)
                return total_series_fetched < 500;
            else if (tab == TrendingTab)
                return total_trending_fetched < 500;

            return false;
        }

        private void CreateMovieGridCard(object item, int x, int y)
        {
            var movie = item as MovieModel;
            if (movie == null) return;

            CreateGridCard(movie.Title, movie.PosterPath, movie, x, y, MovieTab);
        }

        private void CreateSeriesGridCard(object item, int x, int y)
        {
            var series = item as TVShowModel;
            if (series == null) return;

            CreateGridCard(series.Name, series.PosterPath, series, x, y, GetSeriesTab());
        }

        private void CreateTrendingGridCard(object item, int x, int y)
        {
            string title = "";
            string posterPath = "";

            if (item is MovieModel movie)
            {
                title = movie.Title;
                posterPath = movie.PosterPath;
            }
            else if (item is TVShowModel series)
            {
                title = series.Name;
                posterPath = series.PosterPath;
            }

            CreateGridCard(title, posterPath, item, x, y, GetTrendingTab());
        }

        private void CreateGridCard(string title, string posterPath, object mediaItem, int x, int y, TabPage parentTab)
        {
            Guna2Panel cardPanel = new Guna2Panel
            {
                Size = new Size(GRID_CARD_WIDTH, GRID_CARD_HEIGHT),
                Location = new Point(x, y),
                BorderRadius = 10,
                FillColor = Color.FromArgb(30, 30, 30),
                BorderColor = Color.FromArgb(60, 60, 60),
                BorderThickness = 1,
                Cursor = Cursors.Hand,
                Tag = mediaItem
            };

            Guna2PictureBox posterBox = new Guna2PictureBox
            {
                Size = new Size(GRID_CARD_WIDTH - 10, 200),
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderRadius = 8,
                BackColor = Color.FromArgb(50, 50, 50)
            };

            Guna2HtmlLabel titleLabel = new Guna2HtmlLabel
            {
                Text = TruncateText(title, 20),
                Size = new Size(GRID_CARD_WIDTH - 10, 40),
                Location = new Point(5, 210),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.TopCenter
            };

            decimal rating = 0;
            if (mediaItem is MovieModel mov && mov.VoteAverage.HasValue)
                rating = (decimal)(mov.VoteAverage.Value / 2.0m);
            else if (mediaItem is TVShowModel tv && tv.VoteAverage.HasValue)
                rating = (decimal)(tv.VoteAverage.Value / 2.0m);

            Guna2RatingStar ratingStar = new Guna2RatingStar
            {
                Size = new Size(100, 20),
                Location = new Point((GRID_CARD_WIDTH - 100) / 2, 250),
                Value = (float)rating,
                RatingColor = Color.Gold,
                ReadOnly = true
            };

            LoadGridCardImage(posterBox, posterPath, title);

            cardPanel.Controls.Add(posterBox);
            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(ratingStar);

            cardPanel.Click += (s, e) => OpenMediaDetails(mediaItem);
            posterBox.Click += (s, e) => OpenMediaDetails(mediaItem);
            titleLabel.Click += (s, e) => OpenMediaDetails(mediaItem);

            cardPanel.MouseEnter += (s, e) => {
                cardPanel.FillColor = Color.FromArgb(45, 45, 45);
                cardPanel.BorderColor = Color.FromArgb(94, 148, 255);
            };
            cardPanel.MouseLeave += (s, e) => {
                cardPanel.FillColor = Color.FromArgb(30, 30, 30);
                cardPanel.BorderColor = Color.FromArgb(60, 60, 60);
            };

            Guna2Panel scrollPanel = null;
            foreach (Control control in parentTab.Controls)
            {
                if (control is Guna2Panel panel && panel.AutoScroll)
                {
                    scrollPanel = panel;
                    break;
                }
            }

            if (scrollPanel != null)
            {
                scrollPanel.Controls.Add(cardPanel);
            }
            else
            {
                parentTab.Controls.Add(cardPanel);
            }
        }

        private void OpenMediaDetails(object mediaItem)
        {
            MediaDetails mediaDetails;

            if (mediaItem is MovieModel movie)
            {
                mediaDetails = new MediaDetails(movie, currentUser, loaded_movies?.Cast<object>().ToList(), loaded_series?.Cast<object>().ToList(), loaded_trending);
            }
            else if (mediaItem is TVShowModel series)
            {
                mediaDetails = new MediaDetails(series, currentUser, loaded_movies?.Cast<object>().ToList(), loaded_series?.Cast<object>().ToList(), loaded_trending);
            }
            else
            {
                return;
            }

            mediaDetails.FormClosed += (s, e) =>
            {
                Console.WriteLine("MediaDetails closed, data preserved");
            };

            App.Show(mediaDetails, this);
        }

        private void CreateLoadMoreButton(Guna2Panel parentPanel, int y, int availableWidth, string itemType, List<object> items, int displayedCount, Action<object, int, int> createCardMethod, TabPage currentTab)
        {
            Guna2Button loadMoreButton = new Guna2Button
            {
                Text = "Load More",
                Size = new Size(200, 50),
                Location = new Point((availableWidth - 200) / 2 + 20, y),
                BorderRadius = 18,
                FillColor = Color.FromArgb(19, 64, 116),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            loadMoreButton.Click += async (sender, e) => {
                await LoadMoreGridItems(parentPanel, items, displayedCount, createCardMethod, loadMoreButton, itemType, currentTab);
            };

            parentPanel.Controls.Add(loadMoreButton);
        }

        private async Task LoadMoreGridItems(Guna2Panel parentPanel, List<object> items, int currentDisplayed, Action<object, int, int> createCardMethod, Guna2Button loadMoreButton, string itemType, TabPage currentTab)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                loadMoreButton.Text = "Loading...";
                loadMoreButton.Enabled = false;
                const int DISPLAY_BATCH_SIZE = 15;
                const int TMDB_FETCH_SIZE = 50;
                const int FETCH_THRESHOLD = 10;

                int newTotalToDisplay = currentDisplayed + DISPLAY_BATCH_SIZE;
                bool needsMoreData = (newTotalToDisplay + FETCH_THRESHOLD) > items.Count;

                if (needsMoreData && CanFetchMoreFromTMDB(currentTab))
                {
                    int currentTotal = GetCurrentTotalFetched(currentTab);
                    int targetFetchCount = ((newTotalToDisplay + FETCH_THRESHOLD - 1) / TMDB_FETCH_SIZE + 1) * TMDB_FETCH_SIZE;

                    if (targetFetchCount > currentTotal)
                    {
                        Console.WriteLine($"Fetching more data from TMDB:");
                        Console.WriteLine($"  - Currently displaying: {currentDisplayed}");
                        Console.WriteLine($"  - Will display: {newTotalToDisplay}");
                        Console.WriteLine($"  - Available items: {items.Count}");
                        Console.WriteLine($"  - Total fetched from TMDB: {currentTotal}");
                        Console.WriteLine($"  - Target fetch count: {targetFetchCount}");

                        await FetchMoreDataFromTMDB(currentTab, itemType, targetFetchCount);
                        if (currentTab == MovieTab)
                            items = loaded_movies.Cast<object>().ToList();
                        else if (currentTab == SeriesTab)
                            items = loaded_series.Cast<object>().ToList();
                        else if (currentTab == TrendingTab)
                            items = loaded_trending;

                        Console.WriteLine($"  - Items available after fetch: {items.Count}");
                    }
                }
                parentPanel.Controls.Clear();

                int availableWidth = parentPanel.Parent.ClientSize.Width - 40;
                int itemsPerRow = Math.Max(4, availableWidth / (GRID_CARD_WIDTH + GRID_CARD_MARGIN));
                int actualItemsToShow = Math.Min(newTotalToDisplay, items.Count);

                Console.WriteLine($"Displaying {actualItemsToShow} items (requested: {newTotalToDisplay}, available: {items.Count})");

                int startY = SearchBox.Visible ? 50 : 20;

                for (int i = 0; i < actualItemsToShow; i++)
                {
                    int row = i / itemsPerRow;
                    int col = i % itemsPerRow;
                    int x = GRID_CARD_MARGIN + col * (GRID_CARD_WIDTH + GRID_CARD_MARGIN);
                    int y = startY + row * (GRID_CARD_HEIGHT + GRID_CARD_MARGIN);

                    createCardMethod(items[i], x, y);
                }

                if (currentTab == MovieTab)
                    displayed_movies = actualItemsToShow;
                else if (currentTab == SeriesTab)
                    displayed_series = actualItemsToShow;
                else if (currentTab == TrendingTab)
                    displayed_trending = actualItemsToShow;

                bool hasMoreItemsToDisplay = actualItemsToShow < items.Count;
                bool canFetchMoreFromTMDB = CanFetchMoreFromTMDB(currentTab);

                if (hasMoreItemsToDisplay || canFetchMoreFromTMDB)
                {
                    int totalRows = (int)Math.Ceiling((double)actualItemsToShow / itemsPerRow);
                    int newY = startY + totalRows * (GRID_CARD_HEIGHT + GRID_CARD_MARGIN) + 40;

                    int remainingItems = Math.Max(0, items.Count - actualItemsToShow);
                    int nextBatchSize = Math.Min(DISPLAY_BATCH_SIZE, remainingItems);

                    if (nextBatchSize > 0)
                    {
                        loadMoreButton.Text = $"Load More ({nextBatchSize} items)";
                    }
                    else if (canFetchMoreFromTMDB)
                    {
                        loadMoreButton.Text = $"Load More ({DISPLAY_BATCH_SIZE} items)";
                    }
                    else
                    {
                        loadMoreButton.Text = "Load More";
                    }

                    loadMoreButton.Enabled = true;
                    loadMoreButton.Location = new Point((availableWidth - 140) / 2 + 20, newY);
                    parentPanel.Controls.Add(loadMoreButton);
                }
                else
                {
                    int totalRows = (int)Math.Ceiling((double)actualItemsToShow / itemsPerRow);
                    int messageY = startY + totalRows * (GRID_CARD_HEIGHT + GRID_CARD_MARGIN) + 40;

                    Guna2HtmlLabel limitReachedLabel = new Guna2HtmlLabel
                    {
                        Text = $"All available content loaded ({actualItemsToShow} items)",
                        Location = new Point((availableWidth - 250) / 2 + 20, messageY),
                        AutoSize = true,
                        ForeColor = Color.FromArgb(150, 150, 150),
                        BackColor = Color.Transparent,
                        Font = new Font("Segoe UI", 9, FontStyle.Italic)
                    };

                    parentPanel.Controls.Add(limitReachedLabel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading more items: {ex.Message}");
                MessageBox.Show($"Error loading more items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                loadMoreButton.Text = "Load More";
                loadMoreButton.Enabled = true;
                parentPanel.Controls.Add(loadMoreButton);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private int GetCurrentTotalFetched(TabPage tab)
        {
            if (tab == MovieTab)
                return total_movies_fetched;
            else if (tab == SeriesTab)
                return total_series_fetched;
            else if (tab == TrendingTab)
                return total_trending_fetched;

            return 0;
        }

        private async Task FetchMoreDataFromTMDB(TabPage currentTab, string itemType, int targetFetchCount)
        {
            if (TMDB_provider == null || !TMDB_provider.Initialize())
                return;

            try
            {
                var tmdbService = TMDB_provider.GetTMDBService();

                if (currentTab == MovieTab)
                {
                    Console.WriteLine($"Fetching more movies from TMDB, target: {targetFetchCount}");
                    var allMovies = await tmdbService.GetNewReleaseMoviesAsync(targetFetchCount);

                    if (allMovies != null && allMovies.Count > loaded_movies.Count)
                    {
                        var newMovies = allMovies.Skip(loaded_movies.Count).ToList();
                        loaded_movies.AddRange(newMovies);
                        total_movies_fetched = targetFetchCount;
                        Console.WriteLine($"Added {newMovies.Count} new movies. Total: {loaded_movies.Count}");
                    }
                }
                else if (currentTab == SeriesTab)
                {
                    Console.WriteLine($"Fetching more series from TMDB, target: {targetFetchCount}");
                    var allSeries = await tmdbService.GetNewReleaseTVShowsAsync(targetFetchCount);

                    if (allSeries != null && allSeries.Count > loaded_series.Count)
                    {
                        var newSeries = allSeries.Skip(loaded_series.Count).ToList();
                        loaded_series.AddRange(newSeries);
                        total_series_fetched = targetFetchCount;
                        Console.WriteLine($"Added {newSeries.Count} new series. Total: {loaded_series.Count}");
                    }
                }
                else if (currentTab == TrendingTab)
                {
                    Console.WriteLine($"Fetching more trending items from TMDB, target: {targetFetchCount}");
                    var allTrending = await tmdbService.GetTrendingAsync(targetFetchCount);

                    if (allTrending != null && allTrending.Count > loaded_trending.Count)
                    {
                        var newTrending = allTrending.Skip(loaded_trending.Count).ToList();
                        loaded_trending.AddRange(newTrending);
                        total_trending_fetched = targetFetchCount;
                        Console.WriteLine($"Added {newTrending.Count} new trending items. Total: {loaded_trending.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching more {itemType} from TMDB: {ex.Message}");
            }
        }

        private void LoadGridCardImage(Guna2PictureBox imageBox, string posterPath, string title)
        {
            CreatePlaceholderImage(imageBox, GRID_CARD_WIDTH - 10, "Loading...");

            Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(posterPath))
                    {
                        string imageUrl = $"https://image.tmdb.org/t/p/w342{posterPath}";

                        using (WebClient client = new WebClient())
                        {
                            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                            byte[] imageData = client.DownloadData(imageUrl);
                            using (MemoryStream stream = new MemoryStream(imageData))
                            {
                                Image poster = Image.FromStream(stream);
                                this.Invoke((MethodInvoker)delegate {
                                    imageBox.Image = poster;
                                });
                            }
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            CreatePlaceholderImage(imageBox, GRID_CARD_WIDTH - 10, title);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image for '{title}': {ex.Message}");
                    this.Invoke((MethodInvoker)delegate
                    {
                        CreatePlaceholderImage(imageBox, GRID_CARD_WIDTH - 10, title);
                    });
                }
            });
        }

        private void CreatePlaceholderImage(Guna2PictureBox imageBox, int width, string title = "No Image")
        {
            try
            {
                imageBox.Image = Properties.Resources.movie_placeholder;
            }
            catch
            {
                int height = (int)(width * 1.5);
                Bitmap placeholder = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.FromArgb(60, 60, 60));
                    using (Brush brush = new SolidBrush(Color.FromArgb(100, 100, 100)))
                    {
                        g.FillRectangle(brush, 0, 0, placeholder.Width, placeholder.Height);

                        using (Font font = new Font("Segoe UI", Math.Max(8, width / 20)))
                        {
                            string text = string.IsNullOrEmpty(title) ? "No Image" : title;
                            SizeF textSize = g.MeasureString(text, font);
                            float textX = (placeholder.Width - textSize.Width) / 2;
                            float textY = (placeholder.Height - textSize.Height) / 2;
                            g.DrawString(text, font, Brushes.White, textX, textY);
                        }
                    }
                }
                imageBox.Image = placeholder;
            }
        }

        private async Task LoadMoviesDirectly()
        {
            try
            {
                if (TMDB_provider == null || !TMDB_provider.Initialize())
                {
                    ShowNoContentMessage(MovieTab, "TMDB service not initialized. Please configure TMDB API key first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;
                ShowLoadingWithProgress(MovieTab, "Fetching movies from TMDB...");

                var tmdbService = TMDB_provider.GetTMDBService();
                loaded_movies = await FetchMoviesWithProgress(tmdbService, 50);
                total_movies_fetched = 50;

                Console.WriteLine($"Loaded {loaded_movies.Count} movies directly from TMDB");
                DisplayMoviesGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading movies: {ex.Message}");
                ShowNoContentMessage(MovieTab, $"Error loading movies: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadSeriesDirectly()
        {
            try
            {
                if (TMDB_provider == null || !TMDB_provider.Initialize())
                {
                    ShowNoContentMessage(GetSeriesTab(), "TMDB service not initialized. Please configure TMDB API key first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;
                ShowLoadingWithProgress(GetSeriesTab(), "Fetching TV series from TMDB...");

                var tmdbService = TMDB_provider.GetTMDBService();
                loaded_series = await FetchSeriesWithProgress(tmdbService, 50);
                total_series_fetched = 50;

                Console.WriteLine($"Loaded {loaded_series.Count} series directly from TMDB");
                DisplaySeriesGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading series: {ex.Message}");
                ShowNoContentMessage(GetSeriesTab(), $"Error loading series: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadTrendingDirectly()
        {
            try
            {
                if (TMDB_provider == null || !TMDB_provider.Initialize())
                {
                    ShowNoContentMessage(GetTrendingTab(), "TMDB service not initialized. Please configure TMDB API key first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;
                ShowLoadingWithProgress(GetTrendingTab(), "Fetching trending content from TMDB...");

                var tmdbService = TMDB_provider.GetTMDBService();
                loaded_trending = await FetchTrendingWithProgress(tmdbService, 50);
                total_trending_fetched = 50;

                Console.WriteLine($"Loaded {loaded_trending.Count} trending items directly from TMDB");
                DisplayTrendingGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading trending content: {ex.Message}");
                ShowNoContentMessage(GetTrendingTab(), $"Error loading trending content: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task<List<MovieModel>> FetchMoviesWithProgress(TMDBService tmdbService, int totalMovies)
        {
            var movies = new List<MovieModel>();
            var allMovies = await tmdbService.GetNewReleaseMoviesAsync(totalMovies);

            if (allMovies == null || allMovies.Count == 0)
            {
                Console.WriteLine("No movies returned from TMDB service");
                return movies;
            }

            for (int i = 0; i < allMovies.Count; i++)
            {
                if (allMovies[i] != null)
                {
                    movies.Add(allMovies[i]);
                    UpdateProgress(i + 1, Math.Max(allMovies.Count, 1), $"Loading movie {i + 1} of {allMovies.Count}: {allMovies[i].Title}");
                    await Task.Delay(10);
                }
            }

            return movies;
        }

        private async Task<List<TVShowModel>> FetchSeriesWithProgress(TMDBService tmdbService, int totalSeries)
        {
            var series = new List<TVShowModel>();
            var allSeries = await tmdbService.GetNewReleaseTVShowsAsync(totalSeries);

            if (allSeries == null || allSeries.Count == 0)
            {
                Console.WriteLine("No TV series returned from TMDB service");
                return series;
            }

            for (int i = 0; i < allSeries.Count; i++)
            {
                if (allSeries[i] != null)
                {
                    series.Add(allSeries[i]);
                    UpdateProgress(i + 1, Math.Max(allSeries.Count, 1), $"Loading series {i + 1} of {allSeries.Count}: {allSeries[i].Name}");
                    await Task.Delay(10);
                }
            }

            return series;
        }

        private async Task<List<object>> FetchTrendingWithProgress(TMDBService tmdbService, int totalItems)
        {
            var trending = new List<object>();
            var allTrending = await tmdbService.GetTrendingAsync(totalItems);

            if (allTrending == null || allTrending.Count == 0)
            {
                Console.WriteLine("No trending items returned from TMDB service");
                return trending;
            }

            for (int i = 0; i < allTrending.Count; i++)
            {
                if (allTrending[i] != null)
                {
                    trending.Add(allTrending[i]);
                    string itemName = allTrending[i] is MovieModel movie ? movie.Title :
                                     allTrending[i] is TVShowModel tvShow ? tvShow.Name : "Unknown";
                    UpdateProgress(i + 1, Math.Max(allTrending.Count, 1), $"Loading trending {i + 1} of {allTrending.Count}: {itemName}");
                    await Task.Delay(10);
                }
            }

            return trending;
        }

        private void ShowLoadingWithProgress(TabPage tab, string message)
        {
            tab.Controls.Clear();

            Guna2HtmlLabel loadingLabel = new Guna2HtmlLabel
            {
                Text = message,
                Location = new Point(50, 60),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Comic Sans MS", 14, FontStyle.Bold)
            };

            fetchProgressBar = new Guna2ProgressBar
            {
                Location = new Point(50, 110),
                Size = new Size(400, 20),
                ProgressColor = Color.FromArgb(94, 148, 255),
                ProgressColor2 = Color.FromArgb(51, 102, 255),
                BorderRadius = 10,
                TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault,
                Value = 0
            };

            progressLabel = new Guna2HtmlLabel
            {
                Text = "Starting...",
                Location = new Point(50, 140),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Comic Sans MS", 10)
            };

            tab.Controls.Add(loadingLabel);
            tab.Controls.Add(fetchProgressBar);
            tab.Controls.Add(progressLabel);
            tab.Refresh();
        }

        private void ShowNoContentMessage(TabPage tab, string message)
        {
            tab.Controls.Clear();

            Guna2HtmlLabel noContentLabel = new Guna2HtmlLabel
            {
                Text = message,
                Location = new Point(50, 60),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Comic Sans MS", 12, FontStyle.Bold)
            };

            if (AreApiKeysConfigured())
            {
                Guna2Button retryButton = new Guna2Button
                {
                    Text = "Retry",
                    Location = new Point(50, 110),
                    Size = new Size(150, 40),
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(94, 148, 255),
                    Font = new Font("Comic Sans MS", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand
                };

                retryButton.Click += async (s, e) => {
                    if (tab == MovieTab)
                        await LoadMoviesDirectly();
                    else if (tab == SeriesTab)
                        await LoadSeriesDirectly();
                    else if (tab == TrendingTab)
                        await LoadTrendingDirectly();
                };

                tab.Controls.Add(retryButton);
            }
            else
            {
                Guna2HtmlLabel configLabel = new Guna2HtmlLabel
                {
                    Text = "Please configure TMDB API key in Settings.",
                    Location = new Point(50, 110),
                    AutoSize = true,
                    ForeColor = Color.FromArgb(255, 193, 7),
                    BackColor = Color.Transparent,
                    Font = new Font("Comic Sans MS", 10, FontStyle.Regular)
                };
                tab.Controls.Add(configLabel);
            }

            tab.Controls.Add(noContentLabel);
        }

        private void UpdateProgress(int current, int total, string message)
        {
            if (fetchProgressBar != null && progressLabel != null)
            {
                int percentage = (int)((double)current / total * 100);
                fetchProgressBar.Value = percentage;
                progressLabel.Text = message;
                Application.DoEvents();
            }
        }

        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength - 3) + "...";
        }

        private TabPage GetSeriesTab()
        {
            return SeriesTab;
        }

        private TabPage GetTrendingTab()
        {
            return TrendingTab;
        }

        private void LoadAllUsers()
        {
            try
            {
                Console.WriteLine("Loading all users...");
                all_users = UserHandle.GetAllUsers();
                Console.WriteLine($"Loaded {all_users.Count} users.");
                PopulateUserPanels();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading users: {e.Message}");
                MessageBox.Show($"Error loading users: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                all_users = new List<User>();
            }
        }

        private void PopulateUserPanels()
        {
            var panels_to_remove = UsersTab.Controls.OfType<Control>()
                .Where(p => p is Guna2GradientPanel && p != UserListPanel && p != TableTitlePanel)
                .ToList();

            foreach (var panel in panels_to_remove)
            {
                UsersTab.Controls.Remove(panel);
                panel.Dispose();
            }

            UserListPanel.Visible = false;
            int y_position = TableTitlePanel.Bottom + 10;

            foreach (var user in all_users)
            {
                CreateUserPanel(user, y_position);
                y_position += 59;
            }
        }

        private void CreateUserPanel(User user, int yPosition)
        {
            Guna2GradientPanel userPanel = new Guna2GradientPanel();
            userPanel.Size = UserListPanel.Size;
            userPanel.Location = new Point(UserListPanel.Location.X, yPosition);
            userPanel.FillColor = UserListPanel.FillColor;
            userPanel.FillColor2 = UserListPanel.FillColor2;
            userPanel.GradientMode = UserListPanel.GradientMode;
            userPanel.AutoRoundedCorners = true;
            userPanel.Dock = DockStyle.Top;
            userPanel.Tag = user;

            Guna2HtmlLabel usernameLabel = new Guna2HtmlLabel();
            usernameLabel.Text = user.Username;
            usernameLabel.Font = UsernameColumn.Font;
            usernameLabel.ForeColor = UsernameColumn.ForeColor;
            usernameLabel.BackColor = Color.Transparent;
            usernameLabel.Location = new Point(23, 11);
            usernameLabel.AutoSize = true;

            Guna2HtmlLabel emailLabel = new Guna2HtmlLabel();
            emailLabel.Text = user.Email;
            emailLabel.Font = EmailColumn.Font;
            emailLabel.ForeColor = EmailColumn.ForeColor;
            emailLabel.BackColor = Color.Transparent;
            emailLabel.Location = new Point(260, 11);
            emailLabel.AutoSize = true;

            Guna2ComboBox roleComboBox = new Guna2ComboBox();
            roleComboBox.Size = UserTypeColumn.Size;
            roleComboBox.Location = new Point(610, 9);
            roleComboBox.FillColor = UserTypeColumn.FillColor;
            roleComboBox.Font = UserTypeColumn.Font;
            roleComboBox.ForeColor = UserTypeColumn.ForeColor;
            roleComboBox.AutoRoundedCorners = true;
            roleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            roleComboBox.Items.AddRange(new string[] { "User", "Premium", "Admin" });
            roleComboBox.SelectedItem = char.ToUpper(user.Role[0]) + user.Role.Substring(1);
            roleComboBox.Tag = user;
            roleComboBox.SelectedIndexChanged += UserTypeColumn_SelectedIndexChanged;

            Guna2HtmlLabel createdDateLabel = new Guna2HtmlLabel();
            createdDateLabel.Text = user.CreatedDate.ToString("dd/MM/yyyy - HH:mm");
            createdDateLabel.Font = CreatedTimeColumn.Font;
            createdDateLabel.ForeColor = CreatedTimeColumn.ForeColor;
            createdDateLabel.BackColor = Color.Transparent;
            createdDateLabel.Location = new Point(857, 11);
            createdDateLabel.AutoSize = true;

            userPanel.Controls.Add(usernameLabel);
            userPanel.Controls.Add(emailLabel);
            userPanel.Controls.Add(roleComboBox);
            userPanel.Controls.Add(createdDateLabel);

            userPanel.MouseDoubleClick += UserListPanel_MouseDoubleClick;
            usernameLabel.MouseDoubleClick += UserListPanel_MouseDoubleClick;
            emailLabel.MouseDoubleClick += UserListPanel_MouseDoubleClick;
            createdDateLabel.MouseDoubleClick += UserListPanel_MouseDoubleClick;

            UsersTab.Controls.Add(userPanel);
            userPanel.BringToFront();
        }

        private void LoadApiConfig()
        {
            var config = ConfigManager.LoadConfig();

            TMDBApiTextBox.Text = config.TmdbApiKey ?? "";
            ProwlarrApiTextBox.Text = config.ProwlarrApiKey ?? "";
            ProwlarrUrlTextBox.Text = config.ProwlarrBaseUrl ?? "";
            SMTPHostTextBox.Text = config.SmtpHost ?? "";
            SMTPPortTextBox.Text = config.SmtpPort?.ToString() ?? "";
            SMTPUsernameTextBox.Text = config.SmtpUsername ?? "";
            SMTPPassTextBox.Text = config.SmtpPassword ?? "";
        }

        private bool AreApiKeysConfigured()
        {
            var config = Utils.ConfigManager.LoadConfig();
            return !string.IsNullOrEmpty(config.TmdbApiKey) &&
                   !string.IsNullOrEmpty(config.ProwlarrApiKey) &&
                   !string.IsNullOrEmpty(config.ProwlarrBaseUrl);
        }

        private void ProfileBox_Click(object sender, EventArgs e)
        {
            UserProfile user_profile = new UserProfile(currentUser);
            App.Show(user_profile, this);
        }

        private void NavMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NavMenu.SelectedTab == UsersTab)
            {
                LoadAllUsers();
            }
        }

        private void UserTypeColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is Guna2ComboBox combo_box && combo_box.Tag is User user)
            {
                string new_role = combo_box.SelectedItem.ToString().ToLower();

                if (user.UserId == currentUser.UserId)
                {
                    MsgBox.Caption = "Permission Denied";
                    MsgBox.Text = "You cannot change your own role.";
                    MsgBox.Icon = MessageDialogIcon.Warning;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    combo_box.SelectedItem = char.ToUpper(user.Role[0]) + user.Role.Substring(1);
                    return;
                }

                if (UserHandle.UpdateUserRole(user.UserId, new_role))
                {
                    user.Role = new_role;
                    MsgBox.Caption = "Success";
                    MsgBox.Text = "User role updated successfully.";
                    MsgBox.Icon = MessageDialogIcon.Information;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                }
                else
                {
                    MsgBox.Caption = "Error";
                    MsgBox.Text = "Failed to update user role.";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    combo_box.SelectedItem = char.ToUpper(user.Role[0]) + user.Role.Substring(1);
                }
            }
        }

        private void UserListPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            User user = null;

            if (sender is Guna2GradientPanel panel) user = panel.Tag as User;
            else if (sender is Control control && control.Parent is Guna2GradientPanel parentPanel) user = parentPanel.Tag as User;

            if (user == null) return;

            if (user.UserId == currentUser.UserId)
            {
                MsgBox.Caption = "Permission Denied";
                MsgBox.Text = "You can't delete your own account";
                MsgBox.Icon = MessageDialogIcon.Warning;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
                return;
            }

            MsgBox.Caption = "Confirm User Deletion";
            MsgBox.Text = $"Are you sure you want to delete the user '{user.Username}'?\n\nThis action cannot be undone!";
            MsgBox.Icon = MessageDialogIcon.Question;
            MsgBox.Buttons = MessageDialogButtons.YesNo;
            DialogResult result = MsgBox.Show();

            if (result == DialogResult.Yes)
            {
                if (UserHandle.DeleteUser(user.UserId))
                {
                    MsgBox.Caption = "Success";
                    MsgBox.Text = $"User '{user.Username}' has been deleted!";
                    MsgBox.Icon = MessageDialogIcon.Information;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    LoadAllUsers();
                }
                else
                {
                    MsgBox.Caption = "Error";
                    MsgBox.Text = "Failed to delete user.";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                }
            }
        }

        private void ProwlarrUrlTextBox_Leave(object sender, EventArgs e)
        {
            string url = ProwlarrUrlTextBox.Text.Trim();

            try
            {
                Uri uri = new Uri(url);
                ProwlarrUrlErrorMsg.Visible = false;
            }
            catch (UriFormatException)
            {
                ProwlarrUrlErrorMsg.Text = "Invalid URL format";
                ProwlarrUrlErrorMsg.ForeColor = Color.Red;
                ProwlarrUrlErrorMsg.Visible = true;
            }
        }

        private async void TMDBApiTextBox_Leave(object sender, EventArgs e)
        {
            string api_key = TMDBApiTextBox.Text.Trim();
            if (string.IsNullOrEmpty(api_key)) return;

            TMDBErrorMsg.Text = "Checking API Key...";
            TMDBErrorMsg.Visible = true;
            TMDBErrorMsg.ForeColor = Color.Yellow;

            if (await TMDBValidator.ValidApiKey(api_key))
            {
                TMDBErrorMsg.Text = "Connection Successful";
                TMDBErrorMsg.ForeColor = Color.Green;
            }
            else
            {
                TMDBErrorMsg.Text = "Invalid API key";
                TMDBErrorMsg.ForeColor = Color.Red;
            }
        }

        private async void ProwlarrApiTextBox_Leave(object sender, EventArgs e)
        {
            string api_key = ProwlarrApiTextBox.Text.Trim();
            string base_url = ProwlarrUrlTextBox.Text.Trim();

            if (string.IsNullOrEmpty(base_url))
            {
                ProwlarrErrorMsg.Text = "Please enter Prowlarr URL first";
                ProwlarrErrorMsg.ForeColor = Color.Red;
                ProwlarrErrorMsg.Visible = true;
                return;
            }

            ProwlarrErrorMsg.Text = "Checking API Key...";
            ProwlarrErrorMsg.Visible = true;
            ProwlarrErrorMsg.ForeColor = Color.Yellow;

            if (await ProwlarrValidation.ValidateApiKey(base_url, api_key))
            {
                ProwlarrErrorMsg.Text = "Connection successful";
                ProwlarrErrorMsg.ForeColor = Color.Green;
            }
            else
            {
                ProwlarrErrorMsg.Text = "Invalid API key or URL";
                ProwlarrErrorMsg.ForeColor = Color.Red;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            int? smtp_port = null;
            if (!string.IsNullOrEmpty(SMTPPortTextBox.Text) && int.TryParse(SMTPPortTextBox.Text, out int port))
            {
                smtp_port = port;
            }
            var config = new AppConfig
            {
                TmdbApiKey = TMDBApiTextBox.Text.Trim(),
                ProwlarrApiKey = ProwlarrApiTextBox.Text.Trim(),
                ProwlarrBaseUrl = ProwlarrUrlTextBox.Text.Trim(),
                SmtpHost = SMTPHostTextBox.Text.Trim(),
                SmtpPort = smtp_port,
                SmtpUsername = SMTPUsernameTextBox.Text.Trim(),
                SmtpPassword = SMTPPassTextBox.Text.Trim()
            };

            bool hasSmtpField = !string.IsNullOrEmpty(config.SmtpHost) ||
                        !string.IsNullOrEmpty(config.SmtpUsername) ||
                        !string.IsNullOrEmpty(config.SmtpPassword) ||
                        config.SmtpPort.HasValue;

            bool allSmtpFieldsFilled = !string.IsNullOrEmpty(config.SmtpHost) &&
                                       !string.IsNullOrEmpty(config.SmtpUsername) &&
                                       !string.IsNullOrEmpty(config.SmtpPassword) &&
                                       config.SmtpPort.HasValue;

            if (hasSmtpField && !allSmtpFieldsFilled)
            {
                SMTPHostErrorMsg.Visible = true;
                SMTPPassErrorMsg.Visible = true;
                SMTPUsernameErrorMsg.Visible = true;
                SMTPPortErrorMsg.Visible = true;
                SMTPEmailErrorMsg.Visible = true;
            }

            if (String.IsNullOrEmpty(config.TmdbApiKey)) return;
            if (String.IsNullOrEmpty(config.ProwlarrApiKey) && String.IsNullOrEmpty(config.ProwlarrBaseUrl)) return;

            if (ConfigManager.SaveConfig(config))
            {
                MsgBox.Caption = "Success";
                MsgBox.Text = "API configuration saved successfully.";
                MsgBox.Icon = MessageDialogIcon.Information;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
            }
            else
            {
                MsgBox.Caption = "Error";
                MsgBox.Text = "Failed to save API configuration.";
                MsgBox.Icon = MessageDialogIcon.Error;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
            }
        }

        private void InviteEmailTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(InviteEmailTextBox.Text, @"^[^@\s]+@gmail\.com$"))
            {
                InviteEmailErrorMsg.Visible = true;
            }
            else
            {
                InviteEmailErrorMsg.Visible = false;
            }
        }

        private async void CheckMarkBox_Click(object sender, EventArgs e)
        {
            var config = ConfigManager.LoadConfig();
            if (!config.SmtpConfigured)
            {
                MsgBox.Caption = "SMTP Not Configured";
                MsgBox.Text = "Please configure SMTP settings in the Server Config section first or Ask an Admin to Setup first.";
                MsgBox.Icon = MessageDialogIcon.Warning;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
                return;
            }

            string invite_code = SMTPService.GenerateInviteCode();

            CheckMarkBox.Enabled = false;
            InviteEmailTextBox.Enabled = false;
            CheckMarkBox.Cursor = Cursors.WaitCursor;

            try
            {
                SMTPService smtp_service = new SMTPService();

                if (await smtp_service.SendInvitationEmail(InviteEmailTextBox.Text.Trim(), invite_code))
                {
                    CheckMarkBox.Image = Properties.Resources.green_check_solid;
                    Timer green_timer = new Timer();
                    green_timer.Interval = 60 * 1000;
                    green_timer.Tick += (s, args) =>
                    {
                        CheckMarkBox.Image = Properties.Resources.check_solid;
                        green_timer.Stop();
                        green_timer.Dispose();
                    };
                    green_timer.Start();
                    InviteEmailTextBox.Clear();
                }
                else
                {
                    MsgBox.Caption = "Error";
                    MsgBox.Text = "Failed to send invitation email. Please check your SMTP settings and try again.";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending invitation email: {ex.Message}");
            }
            finally
            {
                CheckMarkBox.Enabled = true;
                InviteEmailTextBox.Enabled = true;
                CheckMarkBox.Cursor = Cursors.Default;
            }
        }

        private void MainMenu_SizeChanged(object sender, EventArgs e)
        {
            if (NavMenu.SelectedTab == MovieTab && loaded_movies != null && loaded_movies.Count > 0)
            {
                DisplayMoviesGrid();
            }
            else if (NavMenu.SelectedTab == SeriesTab && loaded_series != null && loaded_series.Count > 0)
            {
                DisplaySeriesGrid();
            }
            else if (NavMenu.SelectedTab == TrendingTab && loaded_trending != null && loaded_trending.Count > 0)
            {
                DisplayTrendingGrid();
            }
        }

        private void SettingsTab_Click(object sender, EventArgs e) { }
        private void TrendingTab_Click(object sender, EventArgs e) { }
        private void MovieTab_Click(object sender, EventArgs e) { }

        private void SearchBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as Guna2TextBox;
            if (textBox != null)
            {
                _ = HandleSearchAsync(textBox.Text.Trim());
            }
        }
    }
}