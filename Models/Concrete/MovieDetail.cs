using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class MovieDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string ReleaseDate { get; set; }
        public string VideoUrl { get; set; }
        public string PictureUrl { get; set; }
        public string Rated { get; set; }
        public string Duration { get; set; }
        public decimal Imdb { get; set; }
        public string Detail { get; set; }
        public string TrailerUrl { get; set; }
        public string Starring { get; set; }
        public DateTime Created { get; set; }
    }
}

