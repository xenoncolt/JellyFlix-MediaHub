using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models.TMDB
{
    internal class TVShowModel
    {
        public int Id { get; set; }
        public int TMDBId { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime? FirstAirDate { get; set; }
        public DateTime? LastAirDate { get; set; }
        public int? NumberOfEpisodes { get; set; }
        public int? NumberOfSeasons { get; set; }
        public string EpisodeRunTime { get; set; } // uggghhhh remember JSON array of integers
        public decimal? VoteAverage { get; set; }
        public int? VoteCount { get; set; }
        public decimal? Popularity { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalName { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string HomePage { get; set; }
        public bool InProduction { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public List<ProductionCompanyModel> ProductionCompanies { get; set; } = new List<ProductionCompanyModel>();
        public List<CastModel> Cast { get; set; } = new List<CastModel>();
    }
}
