using Microsoft.EntityFrameworkCore;
using Models.Concrete;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMovieDetailDal: NetMovieContext
    {
        public List<MovieDetail> GetAll(Expression<Func<MovieDetail, bool>> filter = null)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return filter == null ? context.Set<MovieDetail>().ToList() : context.Set<MovieDetail>().Where(filter).ToList();
            }
        }

        public MovieDetail GetMovieById(Expression<Func<MovieDetail, bool>> filter)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return context.Set<MovieDetail>().SingleOrDefault(filter);
            }
        }

        public void Add(MovieDetail movie)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                context.Add(movie);
                context.SaveChanges();
            }
        }

        public void Update(MovieDetail movie)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var updatedEntity = context.Entry(movie);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(MovieDetail movie)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var deletedEntity = context.Entry(movie);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
