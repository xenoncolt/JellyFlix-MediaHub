using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models.TMDB
{
    internal class CastModel
    {
        public int Id { get; set; }
        public int TMDBId { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
        public int? Gender { get; set; }
        public string Character { get; set; }
        public string CreditId { get; set; }
        public int? OrderIndex { get; set; }
    }
}
