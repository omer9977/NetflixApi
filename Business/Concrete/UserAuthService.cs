using Dapper;
using Models.Common;
using Business.IServices;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    //Bu class kullanıcının kayıt olma, giriş yapma ve parolamı unuttum operasyonları içindir
    public class UserAuthService : IUserAuthService
    {
        UserInfo _oUser = new UserInfo();

        public async Task<string> ConfirmEmail(string userId)
        {
            //try
            //{
            if (string.IsNullOrEmpty(userId.ToString())) return "Invalid User Id";

            UserInfo oUser = new UserInfo()
            {
                Id = userId
            };
            UserInfo user = await this.CheckRecordExistence(oUser);
            if (user == null)
            {
                return Message.InvalidUser;
            }
            else
            { // Burası DataAccess katmanına ait
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    var oUsers = await con.QueryAsync<UserInfo>("SP_UserInfo",
                        this.SetParameters(oUser, (int)OperationType.UpdateConfirmMail),
                        commandType: CommandType.StoredProcedure);
                    if (oUsers != null && oUsers.Count() > 0)
                    {
                        _oUser = oUsers.FirstOrDefault();
                    }
                    return "Mail Confirmed";
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }

        public async Task<UserInfo> SignUp(UserInfo oUser) // Kullanıcının kayıt olmasını sağlayan operasyon
        {
            _oUser = new UserInfo();
            // try
            // {
            UserInfo user = await this.CheckRecordExistence(oUser);
            if (user == null)
            {
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    var oUsers = await con.QueryAsync<UserInfo>("SP_UserInfo", this.SetParameters(oUser, (int)OperationType.Signup),
                        commandType: CommandType.StoredProcedure);

                    if (oUsers != null && oUsers.Count() > 0)
                    {
                        _oUser = oUsers.FirstOrDefault();
                    }
                    _oUser.Message = Message.Success;
                }
            }
            else
            {
                _oUser = user;
            }
            // }
            //catch (Exception ex)
            //{

            //      _oUser.Message = ex.Message;
            //}
            return _oUser;
        }


        private async Task<UserInfo> CheckRecordExistence(UserInfo oUser) // Kullanıcının veri tabanında olup olmadığını kontrol eder
        {
            UserInfo user = new UserInfo();
            if (!string.IsNullOrEmpty(oUser.Username))
            {
                user = await this.GetLoginUser(oUser);
                if (user != null)
                {
                    if (!user.Status)
                    {
                        user.Message = Message.VerifyMail;
                    }
                    else if (user.Status)
                    {
                        user.Message = Message.UserAlreadyCreated;
                    }
                }
            }
            return user;
        }

        private async Task<UserInfo> GetLoginUser(UserInfo user)//Kullanıcının giriş yapmasını sağlayan operasyondur
        {
            _oUser = new UserInfo();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string sSQL = "SELECT * FROM UserInfo WHERE 1=1 ";
                if (!string.IsNullOrEmpty(user.Username)) sSQL += " AND Username='" + user.Username + "' OR Email='" + user.Email + "'"; // Buraya email ayarı çekmem lazım onun için parametrenin UserInfo olması lazım
                var oUsers = (await con.QueryAsync<UserInfo>(sSQL)).ToList();
                if (oUsers != null && oUsers.Count > 0) _oUser = oUsers.SingleOrDefault();
                else return null;
            }
            return _oUser;
        }

        private DynamicParameters SetParameters(UserInfo oUser, int nOperationType)// Veritabanından verileri set etmeye yarayan operasyon
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", oUser.Id);
            parameters.Add("@Name", oUser.Name);// Burayı çıkartacağım
            parameters.Add("@Email", oUser.Email);
            parameters.Add("@UserName", oUser.Username);
            parameters.Add("@Password", oUser.Password);
            parameters.Add("@Status", oUser.Status);
            parameters.Add("@OperationType", nOperationType);
            return parameters;


        }


        #region Parolamı unuttum bölümü

        public async Task<UserInfo> LostPassword(UserInfo oUser)
        {
            _oUser = new UserInfo();
            //try
            //{
            UserInfo user = await this.CheckRecordExistenceForLostPassword(oUser);
            if (user == null)
            {

            }
            else
            {
                _oUser = user;
            }
            // }
            //catch (Exception ex)
            //{

            //    _oUser.Message = ex.Message;
            //}
            return _oUser;
        }

        private async Task<UserInfo> CheckRecordExistenceForLostPassword(UserInfo oUser)
        {
            UserInfo user = new UserInfo();
            if (!string.IsNullOrEmpty(oUser.Email))
            {
                user = await this.GetLoginUserByEmail(oUser.Email);
                if (user != null)
                {
                    if (user.Status)
                    {
                        user.Message = Message.Success;
                    }
                    else if (!user.Status)
                    {
                        user.Message = Message.UserAlreadyCreated;
                    }
                }
            }
            return user;
        }

        private async Task<UserInfo> GetLoginUserByEmail(string email)
        {
            _oUser = new UserInfo();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string sSQL = "SELECT * FROM UserInfo WHERE 1=1 ";
                if (!string.IsNullOrEmpty(email)) sSQL += " AND Email='" + email + "'";
                var oUsers = (await con.QueryAsync<UserInfo>(sSQL)).ToList();
                if (oUsers != null && oUsers.Count > 0) _oUser = oUsers.SingleOrDefault();
                else return null;
            }
            return _oUser;
        }
        public async Task<string> CreatePassword(UserInfo userIdPassword)
        {
            //try
            //{
            if (string.IsNullOrEmpty(userIdPassword.Email)) return "Invalid Email";

            UserInfo oUser = new UserInfo()
            {
                Username = userIdPassword.Username,
                Email = userIdPassword.Email
            };
            UserInfo user = await this.CheckRecordExistenceForLostPassword(oUser);
            if (user == null)
            {
                return Message.InvalidUser;
            }
            else
            {
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    oUser.Password = userIdPassword.Password;
                    if (con.State == ConnectionState.Closed) con.Open();

                    var oUsers = await con.QueryAsync<UserInfo>("SP_UserInfo",
                        this.SetParameters(oUser, (int)OperationType.CreatePassword),
                        commandType: CommandType.StoredProcedure);
                    if (oUsers != null && oUsers.Count() > 0)
                    {
                        _oUser = oUsers.FirstOrDefault();
                    }
                    return "Mail Confirmed";
                }
            }
            // }
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }
        #endregion



    }

}
