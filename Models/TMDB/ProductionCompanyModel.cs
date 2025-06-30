using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models.TMDB
{
    internal class ProductionCompanyModel
    {
        public int Id { get; set; }
        public int TMDBId { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string OriginCountry { get; set; }
    }
}
