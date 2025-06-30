using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Utils
{
    internal class FindQbit
    {
        private static string _cachedPath = null;

        public static bool IsQBittorrentInstalled()
        {
            return !string.IsNullOrEmpty(GetQBittorrentPath());
        }

        public static string GetQBittorrentPath()
        {
            if (!string.IsNullOrEmpty(_cachedPath) && File.Exists(_cachedPath))
                return _cachedPath;

            _cachedPath = FindQBittorrentExecutable();
            return _cachedPath;
        }

        public static bool TryImportToQBittorrent(string magnetUrl, string torrentTitle)
        {
            try
            {
                Console.WriteLine($"Attempting to import magnet link to qBittorrent: {torrentTitle}");

                string qbittorrentPath = GetQBittorrentPath();

                if (string.IsNullOrEmpty(qbittorrentPath))
                {
                    Console.WriteLine("qBittorrent executable not found");
                    return false;
                }

                Console.WriteLine($"Found qBittorrent at: {qbittorrentPath}");

                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = qbittorrentPath,
                    Arguments = $"\"{magnetUrl}\"",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                var process = System.Diagnostics.Process.Start(startInfo);

                if (process != null)
                {
                    Console.WriteLine($"Successfully launched qBittorrent with magnet link for: {torrentTitle}");
                    return true;
                }

                Console.WriteLine("Failed to start qBittorrent process");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing to qBittorrent: {ex.Message}");
                return false;
            }
        }

        private static string FindQBittorrentExecutable()
        {
            try
            {
                Console.WriteLine("Searching for qBittorrent executable...");

                string[] possiblePaths = {
                    @"C:\Program Files\qBittorrent\qbittorrent.exe",
                    @"C:\Program Files (x86)\qBittorrent\qbittorrent.exe",
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Programs\qBittorrent\qbittorrent.exe"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"qBittorrent\qbittorrent.exe"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"qBittorrent\qbittorrent.exe")
                };

                foreach (string path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        Console.WriteLine($"Found qBittorrent executable: {path}");
                        return path;
                    }
                }

                string registryPath = GetQBittorrentPathFromRegistry();
                if (!string.IsNullOrEmpty(registryPath) && File.Exists(registryPath))
                {
                    Console.WriteLine($"Found qBittorrent via registry: {registryPath}");
                    return registryPath;
                }

                string envPath = GetQBittorrentFromEnvironmentPath();
                if (!string.IsNullOrEmpty(envPath))
                {
                    Console.WriteLine($"Found qBittorrent in PATH: {envPath}");
                    return envPath;
                }

                Console.WriteLine("qBittorrent executable not found in any common locations");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding qBittorrent: {ex.Message}");
                return null;
            }
        }

        private static string GetQBittorrentFromEnvironmentPath()
        {
            try
            {
                string pathEnv = Environment.GetEnvironmentVariable("PATH");
                if (string.IsNullOrEmpty(pathEnv))
                    return null;

                string[] paths = pathEnv.Split(';');
                foreach (string path in paths)
                {
                    try
                    {
                        string qbPath = Path.Combine(path.Trim(), "qbittorrent.exe");
                        if (File.Exists(qbPath))
                        {
                            return qbPath;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching PATH for qBittorrent: {ex.Message}");
            }

            return null;
        }

        private static string GetQBittorrentPathFromRegistry()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    if (key != null)
                    {
                        foreach (string subKeyName in key.GetSubKeyNames())
                        {
                            try
                            {
                                using (var subKey = key.OpenSubKey(subKeyName))
                                {
                                    var displayName = subKey?.GetValue("DisplayName")?.ToString();
                                    if (!string.IsNullOrEmpty(displayName) && displayName.ToLower().Contains("qbittorrent"))
                                    {
                                        var installLocation = subKey.GetValue("InstallLocation")?.ToString();
                                        if (!string.IsNullOrEmpty(installLocation))
                                        {
                                            string exePath = Path.Combine(installLocation, "qbittorrent.exe");
                                            if (File.Exists(exePath))
                                            {
                                                return exePath;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }

                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Classes\magnet\shell\open\command"))
                {
                    var command = key?.GetValue("")?.ToString();
                    if (!string.IsNullOrEmpty(command) && command.ToLower().Contains("qbittorrent"))
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(command, @"""([^""]*qbittorrent[^""]*)""", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            string path = match.Groups[1].Value;
                            if (File.Exists(path))
                            {
                                return path;
                            }
                        }
                    }
                }

                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\magnet\shell\open\command"))
                {
                    var command = key?.GetValue("")?.ToString();
                    if (!string.IsNullOrEmpty(command) && command.ToLower().Contains("qbittorrent"))
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(command, @"""([^""]*qbittorrent[^""]*)""", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            string path = match.Groups[1].Value;
                            if (File.Exists(path))
                            {
                                return path;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking registry for qBittorrent: {ex.Message}");
            }

            return null;
        }
    }
}