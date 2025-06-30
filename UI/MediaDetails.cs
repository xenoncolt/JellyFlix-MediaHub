using Guna.UI2.WinForms;
using JellyFlix_MediaHub.Models;
using JellyFlix_MediaHub.Models.Prowlarr;
using JellyFlix_MediaHub.Models.TMDB;
using JellyFlix_MediaHub.Services.Prowlarr;
using JellyFlix_MediaHub.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JellyFlix_MediaHub.UI
{
    internal partial class MediaDetails : Form
    {
        private readonly MovieModel movie;
        private readonly TVShowModel tvShow;
        private readonly User current_user;
        private readonly string mediaType;
        private List<object> cached_movies;
        private List<object> cached_series;
        private List<object> cached_trending;

        public MediaDetails()
        {
            InitializeComponent();
        }

        public MediaDetails(MovieModel movieModel, User user = null) : this()
        {
            movie = movieModel;
            current_user = user;
            mediaType = "movie";
            LoadMediaDetails();
        }

        public MediaDetails(MovieModel movieModel, User user, List<object> movies, List<object> series, List<object> trending) : this()
        {
            movie = movieModel;
            current_user = user;
            mediaType = "movie";

            // Store the data for future use
            this.cached_movies = movies;
            this.cached_series = series;
            this.cached_trending = trending;

            LoadMediaDetails();
        }

        public MediaDetails(TVShowModel tvShowModel, User user, List<object> movies, List<object> series, List<object> trending) : this()
        {
            tvShow = tvShowModel;
            current_user = user;
            mediaType = "tvshow";

            // Store the data for future use
            this.cached_movies = movies;
            this.cached_series = series;
            this.cached_trending = trending;

            LoadMediaDetails();
        }


        public MediaDetails(TVShowModel tvShowModel, User user = null) : this()
        {
            tvShow = tvShowModel;
            current_user = user;
            mediaType = "tvshow";
            LoadMediaDetails();
        }

        private void LoadMediaDetails()
        {
            try
            {
                if (mediaType == "movie" && movie != null)
                {
                    LoadMovieDetails();
                }
                else if (mediaType == "tvshow" && tvShow != null)
                {
                    LoadTVShowDetails();
                }

                DownloadButton.Click += DownloadButton_ClickAsync;
                BackButton.Click += BackButton_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading media details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMovieDetails()
        {
            MovieTitleLabel.Text = movie.Title ?? "Unknown Title";

            DescriptionsLabel.Text = string.IsNullOrEmpty(movie.Overview)? "No overview available for this movie." : movie.Overview;

            if (movie.VoteAverage.HasValue)
            {
                MovieRatingStar.Value = (float)(movie.VoteAverage.Value / 2.0m);
                MovieRatingStar.ReadOnly = true;
                MovieRatingStar.RatingColor = Color.Gold;
            }
            else
            {
                MovieRatingStar.Value = 0;
            }

            string releaseDate = movie.ReleaseDate?.ToString("MMM dd, yyyy") ?? "Unknown";
            string runtime = movie.Runtime.HasValue ? $"{movie.Runtime} minutes" : "Unknown";
            string rating = movie.VoteAverage.HasValue ? $"{movie.VoteAverage.Value:F1}/10" : "No rating";
            string status = movie.Status ?? "Unknown";

            OthersAboutMovieDetails.Text = $"Release Date: {releaseDate}\n" +
                                         $"Runtime: {runtime}\n" +
                                         $"Rating: {rating}\n" +
                                         $"Status: {status}";

            LoadPosterImage(movie.PosterPath, movie.Title);
            LoadBackdropImage(movie.BackdropPath, movie.PosterPath, movie.Title);
        }

        private void LoadTVShowDetails()
        {
            MovieTitleLabel.Text = tvShow.Name ?? "Unknown Title";

            DescriptionsLabel.Text = string.IsNullOrEmpty(tvShow.Overview)
                ? "No overview available for this TV show."
                : tvShow.Overview;

            if (tvShow.VoteAverage.HasValue)
            {
                MovieRatingStar.Value = (float)(tvShow.VoteAverage.Value / 2.0m);
                MovieRatingStar.ReadOnly = true;
                MovieRatingStar.RatingColor = Color.Gold;
            }
            else
            {
                MovieRatingStar.Value = 0;
            }

            string firstAirDate = tvShow.FirstAirDate?.ToString("MMM dd, yyyy") ?? "Unknown";
            string lastAirDate = tvShow.LastAirDate?.ToString("MMM dd, yyyy") ?? "Unknown";
            string seasons = tvShow.NumberOfSeasons?.ToString() ?? "Unknown";
            string episodes = tvShow.NumberOfEpisodes?.ToString() ?? "Unknown";
            string rating = tvShow.VoteAverage.HasValue ? $"{tvShow.VoteAverage.Value:F1}/10" : "No rating";
            string status = tvShow.Status ?? "Unknown";

            OthersAboutMovieDetails.Text = $"First Air Date: {firstAirDate}\n" +
                                         $"Last Air Date: {lastAirDate}\n" +
                                         $"Seasons: {seasons} | Episodes: {episodes}\n" +
                                         $"Rating: {rating}\n" +
                                         $"Status: {status}";

            LoadPosterImage(tvShow.PosterPath, tvShow.Name);
            LoadBackdropImage(tvShow.BackdropPath, tvShow.PosterPath, tvShow.Name);
        }

        private void LoadPosterImage(string posterPath, string title)
        {
            CreatePlaceholderImage(PosterPicture, "Loading...");

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
                                    PosterPicture.Image = poster;
                                    PosterPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                                });
                            }
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            CreatePlaceholderImage(PosterPicture, title);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading poster for '{title}': {ex.Message}");
                    this.Invoke((MethodInvoker)delegate
                    {
                        CreatePlaceholderImage(PosterPicture, title);
                    });
                }
            });
        }

        private void LoadBackdropImage(string backdropPath, string posterPath, string title)
        {
            CreatePlaceholderImage(BackdropPicture, "Loading Backdrop...");

            Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(backdropPath))
                    {
                        string imageUrl = $"https://image.tmdb.org/t/p/w1280{backdropPath}";

                        using (WebClient client = new WebClient())
                        {
                            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                            byte[] imageData = client.DownloadData(imageUrl);
                            using (MemoryStream stream = new MemoryStream(imageData))
                            {
                                Image backdrop = Image.FromStream(stream);
                                this.Invoke((MethodInvoker)delegate {
                                    BackdropPicture.Image = backdrop;
                                    BackdropPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                                });
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(posterPath))
                    {
                        string imageUrl = $"https://image.tmdb.org/t/p/w1280{posterPath}";

                        using (WebClient client = new WebClient())
                        {
                            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                            byte[] imageData = client.DownloadData(imageUrl);
                            using (MemoryStream stream = new MemoryStream(imageData))
                            {
                                Image backdrop = Image.FromStream(stream);
                                this.Invoke((MethodInvoker)delegate {
                                    BackdropPicture.Image = backdrop;
                                    BackdropPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                                });
                            }
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            CreatePlaceholderImage(BackdropPicture, "No Backdrop Available");
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading backdrop for '{title}': {ex.Message}");
                    this.Invoke((MethodInvoker)delegate
                    {
                        CreatePlaceholderImage(BackdropPicture, "No Backdrop Available");
                    });
                }
            });
        }

        private void CreatePlaceholderImage(Guna2PictureBox imageBox, string text)
        {
            try
            {
                imageBox.Image = Properties.Resources.movie_placeholder;
                imageBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                Bitmap placeholder = new Bitmap(imageBox.Width, imageBox.Height);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.FromArgb(60, 60, 60));
                    using (Brush brush = new SolidBrush(Color.FromArgb(100, 100, 100)))
                    {
                        g.FillRectangle(brush, 0, 0, placeholder.Width, placeholder.Height);

                        using (Font font = new Font("Segoe UI", Math.Max(10, imageBox.Width / 20)))
                        {
                            SizeF textSize = g.MeasureString(text, font);
                            float textX = (placeholder.Width - textSize.Width) / 2;
                            float textY = (placeholder.Height - textSize.Height) / 2;
                            g.DrawString(text, font, Brushes.White, textX, textY);
                        }
                    }
                }
                imageBox.Image = placeholder;
                imageBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private async void DownloadButton_ClickAsync(object sender, EventArgs e)
        {
            string itemTitle = mediaType == "movie" ? movie?.Title : tvShow?.Name;
            int? tmdbId = mediaType == "movie" ? movie?.TMDBId : tvShow?.TMDBId;

            try
            {
                var config = ConfigManager.LoadConfig();

                if (string.IsNullOrEmpty(config.ProwlarrBaseUrl) || string.IsNullOrEmpty(config.ProwlarrApiKey))
                {
                    MsgBox.Caption = "Configuration Required";
                    MsgBox.Text = "Prowlarr configuration is required. Please configure Prowlarr settings in the admin panel.";
                    MsgBox.Icon = MessageDialogIcon.Warning;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    return;
                }

                DownloadButton.Text = "Connecting...";
                DownloadButton.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var prowlarrService = new ProwlarrService(config.ProwlarrBaseUrl, config.ProwlarrApiKey);

                if (!await prowlarrService.TestConnectionAsync())
                {
                    MsgBox.Caption = "Connection Failed";
                    MsgBox.Text = $"Cannot connect to Prowlarr server at:\n{config.ProwlarrBaseUrl}\n\nPlease check:\n• Prowlarr is running\n• URL is correct\n• API key is valid\n• No firewall blocking connection";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    return;
                }

                DownloadButton.Text = "Searching...";
                string category = mediaType == "movie" ? "2000" : "5000";

                Console.WriteLine($"Searching for: {itemTitle} (TMDB ID: {tmdbId})");
                var searchResults = await prowlarrService.SearchAsync(itemTitle, tmdbId, category);

                if (searchResults == null || searchResults.Count == 0)
                {
                    DownloadButton.Text = "Trying simplified search...";
                    searchResults = await prowlarrService.SearchAsync(itemTitle, null, category);
                }

                if (searchResults == null || searchResults.Count == 0)
                {
                    MsgBox.Caption = "No Results Found";
                    MsgBox.Text = $"No torrents found for: {itemTitle}\n\nThis could mean:\n• No torrents available for this title\n• Prowlarr indexers are not configured\n• Title name doesn't match torrent names";
                    MsgBox.Icon = MessageDialogIcon.Information;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    return;
                }

                var bestMatch = prowlarrService.GetBestMatch(searchResults, itemTitle, tmdbId);

                if (bestMatch == null)
                {
                    MsgBox.Caption = "No Suitable Torrent";
                    MsgBox.Text = $"Found {searchResults.Count} torrents but none have seeders.\n\nTitle: {itemTitle}";
                    MsgBox.Icon = MessageDialogIcon.Information;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    return;
                }

                string confirmMessage = $"Found torrent: {bestMatch.Title}\n\n" +
                                      $"Size: {bestMatch.FormattedSize}\n" +
                                      $"Seeders: {bestMatch.Seeders}\n" +
                                      $"Indexer: {bestMatch.Indexer}\n\n" +
                                      $"Download this torrent?";

                MsgBox.Caption = "Torrent Found";
                MsgBox.Text = confirmMessage;
                MsgBox.Icon = MessageDialogIcon.Question;
                MsgBox.Buttons = MessageDialogButtons.YesNo;

                if (MsgBox.Show() == DialogResult.Yes)
                {
                    DownloadButton.Text = "Downloading...";
                    await DownloadTorrentFile(bestMatch, itemTitle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in download process: {ex.Message}");

                string errorMessage = "An error occurred while searching for torrents.";

                if (ex.Message.Contains("timeout") || ex.Message.Contains("timed out"))
                {
                    errorMessage = "Search timed out. Prowlarr may be slow or overloaded.\n\nTry again in a moment or check your Prowlarr server.";
                }
                else if (ex.Message.Contains("connect"))
                {
                    errorMessage = "Cannot connect to Prowlarr server.\n\nCheck if Prowlarr is running and accessible.";
                }
                else
                {
                    errorMessage = $"Error: {ex.Message}";
                }

                MsgBox.Caption = "Search Error";
                MsgBox.Text = errorMessage;
                MsgBox.Icon = MessageDialogIcon.Error;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
            }
            finally
            {
                DownloadButton.Text = "Download";
                DownloadButton.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async Task DownloadTorrentFile(ProwlarrSearchResult torrent, string mediaTitle)
        {
            try
            {
                if (string.IsNullOrEmpty(torrent.DownloadUrl))
                {
                    MsgBox.Caption = "Download Error";
                    MsgBox.Text = "No download URL available for this torrent.";
                    MsgBox.Icon = MessageDialogIcon.Error;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    return;
                }

                Console.WriteLine($"Download URL: {torrent.DownloadUrl}");

                if (torrent.DownloadUrl.StartsWith("magnet:", StringComparison.OrdinalIgnoreCase))
                {
                    HandleMagnetLink(torrent.DownloadUrl, torrent.Title);
                    return;
                }

                if (!torrent.DownloadUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    MsgBox.Caption = "Unsupported Download";
                    MsgBox.Text = $"Unsupported download URL format: {torrent.DownloadUrl.Substring(0, Math.Min(50, torrent.DownloadUrl.Length))}...";
                    MsgBox.Icon = MessageDialogIcon.Warning;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                    return;
                }

                Console.WriteLine("Attempting to download torrent file...");

                var handler = new HttpClientHandler()
                {
                    AllowAutoRedirect = false
                };

                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                    string currentUrl = torrent.DownloadUrl;
                    int redirectCount = 0;
                    const int maxRedirects = 10;

                    while (redirectCount < maxRedirects)
                    {
                        Console.WriteLine($"Requesting URL (redirect #{redirectCount}): {currentUrl}");

                        var response = await httpClient.GetAsync(currentUrl);
                        Console.WriteLine($"Response Status: {response.StatusCode}");

                        if (response.StatusCode == System.Net.HttpStatusCode.MovedPermanently ||
                            response.StatusCode == System.Net.HttpStatusCode.Found ||
                            response.StatusCode == System.Net.HttpStatusCode.SeeOther ||
                            response.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                        {
                            string locationHeader = response.Headers.Location?.ToString();
                            Console.WriteLine($"Redirect location: {locationHeader}");

                            if (string.IsNullOrEmpty(locationHeader))
                            {
                                throw new Exception("Redirect response missing Location header");
                            }

                            if (locationHeader.StartsWith("magnet:", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Redirect points to magnet link");
                                HandleMagnetLink(locationHeader, torrent.Title);
                                return;
                            }

                            if (!locationHeader.StartsWith("http"))
                            {
                                var baseUri = new Uri(currentUrl);
                                currentUrl = new Uri(baseUri, locationHeader).ToString();
                            }
                            else
                            {
                                currentUrl = locationHeader;
                            }

                            redirectCount++;
                            continue;
                        }

                        if (response.IsSuccessStatusCode)
                        {
                            string contentType = response.Content.Headers.ContentType?.MediaType ?? "";
                            Console.WriteLine($"Final Content-Type: {contentType}");

                            var content = await response.Content.ReadAsByteArrayAsync();
                            Console.WriteLine($"Downloaded content size: {content.Length} bytes");

                            if (content.Length == 0)
                            {
                                throw new Exception("Downloaded file is empty");
                            }

                            if (contentType.Contains("text/html") || (content.Length > 10 && System.Text.Encoding.UTF8.GetString(content, 0, Math.Min(100, content.Length)).Contains("<html")))
                            {
                                Console.WriteLine("Content appears to be HTML, searching for magnet/torrent links...");
                                string htmlContent = System.Text.Encoding.UTF8.GetString(content);

                                var magnetMatch = System.Text.RegularExpressions.Regex.Match(htmlContent, @"magnet:\?[^""'\s>]+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                                if (magnetMatch.Success)
                                {
                                    Console.WriteLine($"Found magnet link in HTML");
                                    HandleMagnetLink(magnetMatch.Value, torrent.Title);
                                    return;
                                }

                                var torrentLinkMatch = System.Text.RegularExpressions.Regex.Match(htmlContent, @"href=[""']([^""']*\.torrent[^""']*)[""']", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                                if (torrentLinkMatch.Success)
                                {
                                    string torrentUrl = torrentLinkMatch.Groups[1].Value;
                                    if (!torrentUrl.StartsWith("http"))
                                    {
                                        var baseUri = new Uri(currentUrl);
                                        torrentUrl = new Uri(baseUri, torrentUrl).ToString();
                                    }

                                    Console.WriteLine($"Found torrent link in HTML: {torrentUrl}");
                                    currentUrl = torrentUrl;
                                    redirectCount++;
                                    continue;
                                }
                                else
                                {
                                    throw new Exception("The download link leads to a webpage instead of a torrent file. This indexer may require authentication or the link has expired.");
                                }
                            }

                            string contentStr = System.Text.Encoding.UTF8.GetString(content, 0, Math.Min(100, content.Length));
                            if (contentStr.StartsWith("magnet:", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Content is a magnet link");
                                HandleMagnetLink(System.Text.Encoding.UTF8.GetString(content).Trim(), torrent.Title);
                                return;
                            }

                            if (!IsTorrentFile(content))
                            {
                                Console.WriteLine("Content is not a valid torrent file");
                                throw new Exception("Downloaded content is not a valid torrent file");
                            }

                            if (FindQbit.IsQBittorrentInstalled())
                            {
                                Console.WriteLine("qBittorrent detected, attempting to add torrent directly...");
                                if (await TryAddTorrentToQBittorrent(content, torrent.Title))
                                {
                                    MsgBox.Caption = "Added to qBittorrent";
                                    MsgBox.Text = $"Torrent has been successfully added to qBittorrent:\n\n{torrent.Title}";
                                    MsgBox.Icon = MessageDialogIcon.Information;
                                    MsgBox.Buttons = MessageDialogButtons.OK;
                                    MsgBox.Show();
                                    return;
                                }
                            }

                            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            string safeFileName = CreateSafeFileName(torrent.Title) + ".torrent";
                            string tempFilePath = Path.Combine(desktopPath, safeFileName);

                            File.WriteAllBytes(tempFilePath, content);
                            Console.WriteLine($"Torrent file saved to: {tempFilePath}");

                            System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{tempFilePath}\"");

                            MsgBox.Caption = "Download Complete";
                            MsgBox.Text = $"Torrent file downloaded to desktop: {safeFileName}\n\nWindows Explorer has been opened for you to locate the file.";
                            MsgBox.Icon = MessageDialogIcon.Information;
                            MsgBox.Buttons = MessageDialogButtons.OK;
                            MsgBox.Show();
                            return;
                        }
                        else
                        {
                            throw new Exception($"HTTP {response.StatusCode}: {response.ReasonPhrase}");
                        }
                    }

                    throw new Exception($"Too many redirects (>{maxRedirects})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading torrent file: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                MsgBox.Caption = "Download Error";
                MsgBox.Text = $"Failed to download torrent file: {ex.Message}";
                MsgBox.Icon = MessageDialogIcon.Error;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
            }
        }

        private async Task<bool> TryAddTorrentToQBittorrent(byte[] torrentData, string torrentTitle)
        {
            try
            {
                string tempPath = Path.GetTempPath();
                string tempTorrentFile = Path.Combine(tempPath, $"{Guid.NewGuid()}.torrent");

                File.WriteAllBytes(tempTorrentFile, torrentData);

                string qbittorrentPath = FindQbit.GetQBittorrentPath();
                if (string.IsNullOrEmpty(qbittorrentPath))
                    return false;

                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = qbittorrentPath,
                    Arguments = $"\"{tempTorrentFile}\"",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                var process = System.Diagnostics.Process.Start(startInfo);

                await Task.Delay(2000);

                try
                {
                    File.Delete(tempTorrentFile);
                }
                catch
                {
                }

                return process != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding torrent to qBittorrent: {ex.Message}");
                return false;
            }
        }

        private bool IsTorrentFile(byte[] content)
        {
            if (content == null || content.Length < 10)
                return false;

            string header = System.Text.Encoding.ASCII.GetString(content, 0, Math.Min(20, content.Length));
            return header.StartsWith("d") && content.Contains((byte)'e') &&
                   (header.Contains("announce") || header.Contains("info"));
        }

        private string CreateSafeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "torrent_file";

            char[] invalidChars = Path.GetInvalidFileNameChars();
            string safeName = fileName;

            foreach (char c in invalidChars)
            {
                safeName = safeName.Replace(c, '_');
            }

            if (safeName.Length > 100)
            {
                safeName = safeName.Substring(0, 100);
            }

            return safeName;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenu main_menu = new MainMenu(current_user);
            App.Show(main_menu, this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void HandleMagnetLink(string magnetUrl, string torrentTitle)
        {
            try
            {
                Console.WriteLine($"Handling magnet link for: {torrentTitle}");
                Console.WriteLine($"Magnet URL length: {magnetUrl.Length}");

                if (FindQbit.IsQBittorrentInstalled())
                {
                    Console.WriteLine("qBittorrent found, opening magnet link directly...");
                    if (FindQbit.TryImportToQBittorrent(magnetUrl, torrentTitle))
                    {
                        MsgBox.Caption = "Added to qBittorrent";
                        MsgBox.Text = $"Magnet link has been successfully added to qBittorrent:\n\n{torrentTitle}";
                        MsgBox.Icon = MessageDialogIcon.Information;
                        MsgBox.Buttons = MessageDialogButtons.OK;
                        MsgBox.Show();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Failed to import to qBittorrent, trying alternative method...");

                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = magnetUrl,
                                UseShellExecute = true
                            });

                            MsgBox.Caption = "Magnet Link Opened";
                            MsgBox.Text = $"Magnet link opened with default torrent client:\n\n{torrentTitle}";
                            MsgBox.Icon = MessageDialogIcon.Information;
                            MsgBox.Buttons = MessageDialogButtons.OK;
                            MsgBox.Show();
                            return;
                        }
                        catch (Exception processEx)
                        {
                            Console.WriteLine($"Failed to open magnet with shell execute: {processEx.Message}");
                        }
                    }
                }

                Console.WriteLine("qBittorrent not found or failed to open, using fallback methods...");

                string safeFileName = CreateSafeFileName(torrentTitle) + "_magnet.txt";
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string magnetFilePath = Path.Combine(desktopPath, safeFileName);

                File.WriteAllText(magnetFilePath, magnetUrl);

                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = magnetUrl,
                        UseShellExecute = true
                    });

                    MsgBox.Caption = "Magnet Link Opened";
                    MsgBox.Text = $"Magnet link opened with default torrent client.\n\nThe magnet link has also been saved to: {safeFileName}";
                    MsgBox.Icon = MessageDialogIcon.Information;
                    MsgBox.Buttons = MessageDialogButtons.OK;
                    MsgBox.Show();
                }
                catch
                {
                    try
                    {
                        Clipboard.SetText(magnetUrl);
                        MsgBox.Caption = "Magnet Link Copied";
                        MsgBox.Text = $"Magnet link copied to clipboard and saved to desktop:\n\n{safeFileName}\n\nPaste it into your torrent client.";
                        MsgBox.Icon = MessageDialogIcon.Information;
                        MsgBox.Buttons = MessageDialogButtons.OK;
                        MsgBox.Show();
                    }
                    catch
                    {
                        System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{magnetFilePath}\"");

                        MsgBox.Caption = "Magnet Link Saved";
                        MsgBox.Text = $"Magnet link saved to desktop: {safeFileName}\n\nCopy the magnet link and paste it into your torrent client.";
                        MsgBox.Icon = MessageDialogIcon.Information;
                        MsgBox.Buttons = MessageDialogButtons.OK;
                        MsgBox.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling magnet link: {ex.Message}");
                MsgBox.Caption = "Error";
                MsgBox.Text = $"Failed to handle magnet link: {ex.Message}";
                MsgBox.Icon = MessageDialogIcon.Error;
                MsgBox.Buttons = MessageDialogButtons.OK;
                MsgBox.Show();
            }
        }
    }
}