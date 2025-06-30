using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models.TMDB
{
    internal class GenreModel
    {
        public int Id { get; set; }
        public int TMDBId { get; set; }
        public string Name { get; set; }
    }
}
