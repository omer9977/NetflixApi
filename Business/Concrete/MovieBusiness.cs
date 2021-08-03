using DataAccess.Concrete.EntityFramework;
using Models.Concrete;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    //Bu class web sitesinin filmler ve kategoriler sekmeleri için hazırlanmıştır
    public class MovieBusiness
    {
        EfCategoryDal c;
        EfMovieDetailDal m;
        EfMovieCategoryDal mc;
        public List<MovieCategory> movieCategoryList;

        public MovieBusiness()
        {
            c = new EfCategoryDal();
            m = new EfMovieDetailDal();
            mc = new EfMovieCategoryDal();
            movieCategoryList = mc.GetAll();
        }

        public List<Category> GetCategories()//Tüm kategoriler
        {
            List<Category> categories = c.GetAll();
            categories = categories.OrderBy(category => category.Name).ToList();
            return categories;
        }


        public List<MovieDetail> GetMovies()//Tüm Filmler
        {
            return m.GetAll(); ;
        }

        public MovieDetail GetMoviebyId(int id)
        {
            return m.GetMovieById(m => m.Id == id);
        }

        public void AddMovie(MovieDetail movie)
        {
            m.Add(movie);
        }

        public List<MovieDetail> GetMoviesByCategory(int categoryId)
        {
            List<MovieDetail> movies = new List<MovieDetail>();
            foreach (var item in movieCategoryList)
            {
                if (item.CategoryId == categoryId)
                {
                    movies.Add(m.GetMovieById(m => m.Id == item.MovieId));
                }
            }
            return movies;
        }
    }
}
