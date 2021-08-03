using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IUserAuthService
    {
        Task<UserInfo> SignUp(UserInfo oLoginInfo);
        Task<string> ConfirmEmail(string userId);
        public Task<UserInfo> LostPassword(UserInfo oLoginInfo);
        Task<string> CreatePassword(UserInfo userIdPassword);
    }
}
