using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models.Prowlarr
{
    internal class ProwlarrSearchResult
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonProperty("infoUrl")]
        public string InfoUrl { get; set; }

        [JsonProperty("indexer")]
        public string Indexer { get; set; }

        [JsonProperty("seeders")]
        public int? Seeders { get; set; }

        [JsonProperty("leechers")]
        public int? Leechers { get; set; }

        [JsonProperty("magnetUrl")]
        public string MagnetUrl { get; set; }

        public string FormattedSize => FormatByte(Size);

        private static string FormatByte(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number, 1) >= 1000) {
                number /= 1024;
                counter++;
            }
            return $"{number:n1} {suffixes[counter]}";
        }
    }
}
