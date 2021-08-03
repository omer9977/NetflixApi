using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        MovieBusiness mb;
        public HomeController()
        {
            mb = new MovieBusiness();
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(mb.GetCategories());
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(mb.GetMovies());
        }

        [HttpGet]
        public IActionResult GetMoviebyId(int id)
        {
            return Ok(mb.GetMoviebyId(id));
        }

        [HttpGet]
        public IActionResult GetMovieCategory(int categoryId)
        {
            return Ok(mb.GetMoviesByCategory(categoryId));
        }

        [HttpGet]
        public IActionResult GetTokenData()
        {
            var data = (string)RouteData.DataTokens["Name"];
            return Ok(data);
        }

        [HttpPost]
        public void AddMovie(MovieDetail movie)
        {
            movie.Created = DateTime.Now;
            mb.AddMovie(movie);
        }
    }
}
