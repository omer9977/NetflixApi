using Microsoft.EntityFrameworkCore;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class NetMovieContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=omar\SQLEXPRESS;Database=NetMovie;Trusted_Connection=True");
        }

        public DbSet<Category> Category { get; set; } //İsimlere dikkat et. Db deki tablolarla aynı olacak

        public DbSet<MovieDetail> MovieDetail { get; set; }

        public DbSet<User> UserInfo { get; set; }

        public DbSet<MovieCategory> MovieCategory { get; set; }

        public DbSet<UserData> UserData { get; set; }

        public DbSet<WatchingHistory> WatchingHistory { get; set; }
    }
}
