using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models.TMDB
{
    internal class MovieModel
    {
        public int Id { get; set; }
        public int TMDBId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Runtime { get; set; }
        public decimal? VoteAverage { get; set; }
        public int? VoteCount { get; set; }
        public decimal? Popularity { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public bool Adult { get; set; }
        public long? Budget { get; set; }
        public long? Revenue { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string HomePage { get; set; }
        public string ImdbId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public List<ProductionCompanyModel> ProductionCompanies { get; set; } = new List<ProductionCompanyModel>();
        public List<CastModel> Cast { get; set; } = new List<CastModel>();
    }
}
