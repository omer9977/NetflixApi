using Microsoft.EntityFrameworkCore;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMovieCategoryDal : NetMovieContext
    {
        public List<MovieCategory> GetAll(Expression<Func<MovieCategory, bool>> filter = null)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return filter == null ? context.Set<MovieCategory>().ToList() : context.Set<MovieCategory>().Where(filter).ToList();
            }
        }

        public MovieCategory Get(Expression<Func<MovieCategory, bool>> filter)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return context.Set<MovieCategory>().SingleOrDefault(filter);
            }
        }
    }
}
