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
    public class EfUserDataDal : NetMovieContext
    {
        public List<UserData> GetAll(Expression<Func<UserData, bool>> filter = null)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return filter == null ? context.Set<UserData>().ToList() : context.Set<UserData>().Where(filter).ToList();
            }
        }

        public List<MovieDetail> GetMoviesByUserID(Expression<Func<MovieDetail, bool>> filter = null)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return filter == null ? context.Set<MovieDetail>().ToList() : context.Set<MovieDetail>().Where(filter).ToList();
            }
        }

        public UserData Get(Expression<Func<UserData, bool>> filter)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return context.Set<UserData>().SingleOrDefault(filter);
            }
        }

        public void Add(UserData UserData)
        {
            // Burada using kullanılmasının sebebi new lenen şeyin işi bittikten sonra direk bellekten atılması.
            // Daha performanslı kullanım için. Nesneyi direk normal new leyebilirsin
            using (NetMovieContext context = new NetMovieContext())
            {
                var addedEntity = context.Entry(UserData);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(UserData UserData)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var updatedEntity = context.Entry(UserData);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(UserData UserData)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var deletedEntity = context.Entry(UserData);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
