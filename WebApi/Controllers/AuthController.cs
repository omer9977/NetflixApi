using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Bu controller giriş yapan kullanıcı ile ilgili işlemler içindir.
        AuthBusiness ab;
        public AuthController()
        {
            ab = new AuthBusiness();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            User userGot = ab.GetUser(user);
            if (userGot != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySuperSecretKey@77"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:4000/deneme",
                    audience: "https://localhost:4000/deneme",
                    claims: new List<Claim>(),
                    //expires: DateTime.Now.AddMinutes(),
                    signingCredentials: signingCredentials

                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString, userGot.Id });
            }
            return Unauthorized();

        }

        [HttpGet]
        public ActionResult GetUserById(string userId)
        {
            return Ok(ab.GetUserById(userId));
        }

        [HttpGet]
        public ActionResult GetUserDataByUserId(string userID)
        {
            return Ok(ab.GetUserDataByUserId(userID));
        }


        [HttpPost]
        public ActionResult UpdateUserData(UserData userData)
        {
            return Ok(ab.UpdateUserData(userData));
        }

        [HttpGet]
        public ActionResult GetMovieOfUser(string userId, int movieId)
        {
            return Ok(ab.GetMovieOfUser(userId, movieId));
        }

        [HttpGet]
        public ActionResult GetUserWatchlist(string userID)
        {
            return Ok(ab.GetUserWatchlist(userID));
        }

        [HttpPost]
        public ActionResult AddMovieToWatchlist(UserData userData)
        {
            return Ok(ab.AddMovieToWatchlist(userData));
        }

        [HttpPost]
        public ActionResult RemoveFromWatchlist(UserData userData)
        {
            return Ok(ab.RemoveFromWatchlist(userData));
        }

        [HttpPost]
        public ActionResult AddMovieToWatchingHistory(WatchingHistory userData)
        {
            return Ok(ab.AddMovieToWatchingHistory(userData));
        }


        [HttpPost]
        public ActionResult GetMovieFromWatchingHistory(WatchingHistory watchingHistory)
        {
            return Ok(ab.GetMovieFromWatchingHistory(watchingHistory));
        }

        [HttpPost]
        public void UpdateWatchingHistory(WatchingHistory watchingHistory)
        {
            ab.UpdateWatchingHistory(watchingHistory);
        }
    }
}
