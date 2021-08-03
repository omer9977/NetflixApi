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
    public class EfWatchingHistoryDal: NetMovieContext
    {
        public List<WatchingHistory> GetAll(Expression<Func<WatchingHistory, bool>> filter = null)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return filter == null ? context.Set<WatchingHistory>().ToList() : context.Set<WatchingHistory>().Where(filter).ToList();
            }
        }

        public WatchingHistory Get(Expression<Func<WatchingHistory, bool>> filter)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return context.Set<WatchingHistory>().SingleOrDefault(filter);
            }
        }

        public void Add(WatchingHistory watchingHistory)
        {
            // Burada using kullanılmasının sebebi new lenen şeyin işi bittikten sonra direk bellekten atılması.
            // Daha performanslı kullanım için. Nesneyi direk normal new leyebilirsin
            using (NetMovieContext context = new NetMovieContext())
            {
                var addedEntity = context.Entry(watchingHistory);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(WatchingHistory watchingHistory)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var updatedEntity = context.Entry(watchingHistory);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(WatchingHistory watchingHistory)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var deletedEntity = context.Entry(watchingHistory);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
