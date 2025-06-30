using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Models.TMDB
{
    internal class ReleaseCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public enum CategoryType
    {
        NowPlaying = 1,
        NewReleaseShows = 2,
        TopTrending = 3,
        Popular = 4
    }
}
