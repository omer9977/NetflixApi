using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        SliderService ss;
        MovieBusiness mb;
        public SliderController()
        {
            ss = new SliderService();
            mb = new MovieBusiness();
        }

        [HttpGet]
        public ActionResult GetMovieListFromWatchingHistory(string userId)
        {
            return Ok(ss.GetMovieListFromWatchingHistory(userId));
        }

        [HttpGet]
        public IActionResult GetRecentlyAddeds()
        {
            return Ok(ss.GetRecentlyAddeds());
        }

        [HttpGet]
        public IActionResult GetMostViewed()
        {
            return Ok(ss.GetMostViewed());
        }

        [HttpGet]
        public IActionResult GetMoviesByCategories()
        {
            return Ok(ss.GetMoviesByCategories());
        }
    }
}
