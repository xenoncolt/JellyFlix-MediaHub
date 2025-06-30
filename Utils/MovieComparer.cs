using JellyFlix_MediaHub.Models.TMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Utils
{
    internal class MovieComparer : IEqualityComparer<MovieModel>
    {
        public bool Equals(MovieModel x, MovieModel y)
        {
            return x.TMDBId == y.TMDBId;
        }

        public int GetHashCode(MovieModel obj)
        {
            return obj.TMDBId.GetHashCode();
        }
    }
}
