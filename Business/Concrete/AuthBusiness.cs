using DataAccess.Concrete.EntityFramework;
using Models.Concrete;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    //Bu class siteye giriş yapan kullanıcının datalarıyla ilgili işlemler yapmasını sağlar
    public class AuthBusiness
    {
        EfUserDal u;
        EfUserDataDal ud;
        EfMovieDetailDal md;
        EfWatchingHistoryDal wh;

        public AuthBusiness()
        {
            u = new EfUserDal();
            ud = new EfUserDataDal();
            md = new EfMovieDetailDal();
            wh = new EfWatchingHistoryDal();
        }

        public List<User> GetAllUsers() //Tüm kullanıcıları çağır
        {
            return u.GetAll();
        }

        public User GetUser(User user) //Kullanıcı adı ve parolaya göre kullanıcı 
        {
            return u.Get(u => u.Username == user.Username && u.Password == user.Password && u.Status);
        }

        public User GetUserById(string userId) //User ın id sine göre getirilen kullanıcı
        {
            return u.Get(u => u.Id == userId);
        }

        public List<UserData> GetUserDataByUserId(string userId) //Kullanıcının izleme listesi
        {
            return ud.GetAll(userdata => userdata.UserId == userId);
        }

        public List<MovieDetail> GetUserWatchlist(string userId) //Kullanıcıya ait izleme listesi getir
        {
            List<UserData> userdata = ud.GetAll(udata => udata.UserId == userId);
            List<MovieDetail> watchlist = new List<MovieDetail>();
            foreach (UserData item in userdata)
            {
                watchlist.Add(md.GetMovieById(wlist => wlist.Id == item.MovieId));
            }
            return watchlist;
        }

        public UserData AddMovieToWatchlist(UserData userData) // İzleme listesine eklenen filmin id si ve user ın id si yani UserData
        {
            UserData oUserData = ud.Get(userdata => userdata.UserId == userData.UserId && userdata.MovieId == userData.MovieId);
            if (oUserData == null)
            {
                ud.Add(userData);
            }
            return userData;
        }

        public UserData RemoveFromWatchlist(UserData userData) // İzleme listesinden çıkarılan film ve onun bulunduğu userın id si
        {
            UserData _userdata = ud.Get(userdata => userdata.UserId == userData.UserId && userdata.MovieId == userData.MovieId);
            ud.Delete(_userdata);
            return _userdata;
        }

        public UserData GetMovieOfUser(string userId, int movieId)//Look...
        {
            UserData userdata = ud.Get(udata => udata.UserId == userId & udata.MovieId == movieId);
            return userdata;
        }

        public List<UserData> UpdateUserData(UserData userData)//Look...
        {
            ud.Update(userData);
            return ud.GetAll();
        }

        public WatchingHistory AddMovieToWatchingHistory(WatchingHistory userData)//Look...
        {
            WatchingHistory oWatchingHistory = wh.Get(wh => wh.UserId == userData.UserId && wh.MovieId == userData.MovieId);
            if (oWatchingHistory == null)
            {
                wh.Add(userData);
            }
            return userData;
        }

        public WatchingHistory GetMovieFromWatchingHistory(WatchingHistory watchingHistory)//Look...
        {
            //WatchingHistory watchingHistory = new WatchingHistory() { UserId = userId, MovieId = movieId };
            WatchingHistory _watchingHistory = wh.Get(wh => wh.UserId == watchingHistory.UserId && wh.MovieId == watchingHistory.MovieId);
            if (_watchingHistory == null)
            {
                AddMovieToWatchingHistory(watchingHistory);
                return wh.Get(wh => wh.UserId == watchingHistory.UserId && wh.MovieId == watchingHistory.MovieId);
            }
            else
            {
                return _watchingHistory;
            }
        }

        public void UpdateWatchingHistory(WatchingHistory watchingHistory)//Look...
        {
            WatchingHistory _watchingHistory = wh.Get(wh => wh.UserId == watchingHistory.UserId && wh.MovieId == watchingHistory.MovieId);
            _watchingHistory.PassingTime = watchingHistory.PassingTime;
            _watchingHistory.Updated = watchingHistory.Updated;
            wh.Update(_watchingHistory);
        }
    }
}
