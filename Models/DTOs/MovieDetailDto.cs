using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class MovieDetailDto
    {
        public string Category { get; set; }
        public MovieDetail Movie { get; set; }
    }
}
