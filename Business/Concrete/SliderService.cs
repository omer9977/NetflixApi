using DataAccess.Concrete.EntityFramework;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    //Bu class Home Page deki sliderlar için hazırlanmıştır.
    public class SliderService
    {
        EfWatchingHistoryDal wh;
        EfMovieDetailDal md;
        EfMovieCategoryDal mc;
        EfCategoryDal c;

        public SliderService()
        {
            wh = new EfWatchingHistoryDal();
            md = new EfMovieDetailDal();
            mc = new EfMovieCategoryDal();
            c = new EfCategoryDal();
        }

        public List<MovieDetail> GetMovieListFromWatchingHistory(string userId)
        {
            List<WatchingHistory> watchingHistory = wh.GetAll(udata => udata.UserId == userId);
            watchingHistory = watchingHistory.OrderByDescending(x => x.Updated).ToList();
            List<MovieDetail> movies = new List<MovieDetail>();
            foreach (var item in watchingHistory)
            {
                movies.Add(md.GetMovieById(movie => movie.Id == item.MovieId));
            }
            return movies;
        }

        public List<MovieDetail> GetRecentlyAddeds()
        {
            List<MovieDetail> movies = md.GetAll();
            movies = movies.OrderByDescending(movies => movies.Created).ToList();
            if (movies.Count > 10)
            {
                movies = movies.GetRange(0, 10);
            }
            return movies;
        }

        public List<MovieDetail> GetMostViewed()
        {
            List<WatchingHistory> whatchingMovies = wh.GetAll();
            var query = whatchingMovies.GroupBy(x => x.MovieId)
    .Select(group => new { MovieId = group.Key, Count = group.Count() })
    .OrderByDescending(x => x.Count).Distinct().ToList();
            List<MovieDetail> movieList = new List<MovieDetail>();
            foreach (var item in query)
            {
                movieList.Add(md.GetMovieById(movie => movie.Id == item.MovieId));

            }
            if (movieList.Count > 10)
            {
                movieList = movieList.GetRange(0, 10);
            }
            return movieList;

        }

        public List<List<MovieDetail>> GetMoviesByCategories()
        {
            List<Category> categories = c.GetAll();
            categories = categories.OrderBy(category => category.Name).ToList();
            List<List<MovieDetail>> allMoviesByCategories = new List<List<MovieDetail>>();
            foreach (var category in categories)
            {
                List<MovieCategory> movieCategoryList = mc.GetAll(movie => movie.CategoryId == category.Id);
                List<MovieDetail> movies = new List<MovieDetail>();
                int count = 0;
                foreach (var item in movieCategoryList)
                {
                    movies.Add(md.GetMovieById(movie => movie.Id == item.MovieId));
                    count++;
                    if (count == 10)
                    {
                        break;
                    }
                }
                allMoviesByCategories.Add(movies);
            }
            return allMoviesByCategories;
        }

    }
}
