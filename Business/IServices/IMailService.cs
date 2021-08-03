using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IMailService
    {
        Task<string> SendMail(MailClass oMailClass);
        string GetMailBody(UserInfo oLoginInfo);
        string GetMailBodyForLostPassword(UserInfo oLoginInfo);
        MailClass GetMailObject(UserInfo user);
        MailClass GetMailObjectForLostPassword(UserInfo user);
    }
}
