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
    public class EfCategoryDal: NetMovieContext
    {
        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return filter == null ? context.Set<Category>().ToList() : context.Set<Category>().Where(filter).ToList();
            }
        }

        public Category Get(Expression<Func<Category, bool>> filter)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return context.Set<Category>().SingleOrDefault(filter);
            }
        }

        public void Add(Category category)
        {
            // Burada using kullanılmasının sebebi new lenen şeyin işi bittikten sonra direk bellekten atılması.
            // Daha performanslı kullanım için. Nesneyi direk normal new leyebilirsin
            using (NetMovieContext context = new NetMovieContext())
            {
                var addedEntity = context.Entry(category);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Category category)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var updatedEntity = context.Entry(category);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Category category)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                var deletedEntity = context.Entry(category);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


    }
}
