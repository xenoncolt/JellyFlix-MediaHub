using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models
{
    internal class AppConfig
    {
        public string TmdbApiKey { get; set; }
        public string ProwlarrApiKey { get; set; }
        public string ProwlarrBaseUrl { get; set; }
        public string SmtpHost { get; set; }
        public int? SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public bool SmtpConfigured => !string.IsNullOrEmpty(SmtpHost) &&
                             SmtpPort.HasValue &&
                             !string.IsNullOrEmpty(SmtpUsername) &&
                             !string.IsNullOrEmpty(SmtpPassword);
    }
}
