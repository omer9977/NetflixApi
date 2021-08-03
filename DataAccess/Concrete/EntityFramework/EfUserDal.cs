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
    public class EfUserDal : NetMovieContext
    {
        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return filter == null ? context.Set<User>().ToList() : context.Set<User>().Where(filter).ToList();
            }
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                return context.Set<User>().SingleOrDefault(filter);
            }
        }

        public void Register(UserInfo user)
        {
            using (NetMovieContext context = new NetMovieContext())
            {
                context.Add(user);
                context.SaveChanges();
            }
        }
    }

}

