using Models.Common;
using Business.IServices;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Business.Services
{
    //Bu class mail işlemleri ile ilgilidir
    public class MailService : IMailService
    {
        public string GetMailBody(UserInfo oLoginInfo)
        {
            string url = "http://localhost:4200/login/" + oLoginInfo.Id;
            return string.Format(@"< div style='text-align:center;'> 
                                <h1>Welcome to our Web Site</hl> 
                                <h3>Click below button for verify your Email Id</h3> 
                                <form method='post' action='{0}' style='display: inline;'> 
                                <button type='submit' style='display: block; text-align: center; font-weight: bold; 
                                  background-color: #008CBA; 
                                  font-size: 16px; border-radius: 10px; color:#ffffff; cursor:pointer; width:100%; padding:10px;' 
            Confirm Mail </ button > </ form > </ div > ", url, oLoginInfo.Id);
        }

        public string GetMailBodyForLostPassword(UserInfo oLoginInfo)
        {
            string url = "http://localhost:4200/createPassword/" + oLoginInfo.Id;
            return string.Format(@"< div style='text-align:center;'> 
                                <h1>For new password click the button</hl> 
                                <h3>Click below button for verify your Email Id</h3> 
                                <form method='post' action='{0}' style='display: inline;'> 
                                <button type='submit' style='display: block; text-align: center; font-weight: bold; 
                                  background-color: #008CBA; 
                                  font-size: 16px; border-radius: 10px; color:#ffffff; cursor:pointer; width:100%; padding:10px;' 
            Confirm Mail </ button > </ form > </ div > ", url, oLoginInfo.Id);
        }

        public MailClass GetMailObject(UserInfo user)
        {
            MailClass oMailClass = new MailClass();
            oMailClass.Subject = "Mail Confirmation";
            oMailClass.Body = GetMailBody(user);
            oMailClass.ToMailIds = new List<string>() { user.Email };
            return oMailClass;
        }

        public async Task<string> SendMail(MailClass oMailClass)
        {
            //try
            //{
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(oMailClass.FromMailId);
                oMailClass.ToMailIds.ForEach(x =>
                {
                    mail.To.Add(x);
                });

                mail.Subject = oMailClass.Subject;
                mail.Body = oMailClass.Body;
                mail.IsBodyHtml = oMailClass.IsBodyHtml;
                oMailClass.Attachments.ForEach(x =>
                {
                    mail.Attachments.Add(new Attachment(x));
                });
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(oMailClass.FromMailId, oMailClass.FromMailIdPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                    return Message.MailSent;
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}

        }

        public MailClass GetMailObjectForLostPassword(UserInfo user)
        {
            MailClass oMailClass = new MailClass();
            oMailClass.Subject = "Mail Confirmation";
            oMailClass.Body = GetMailBodyForLostPassword(user);
            oMailClass.ToMailIds = new List<string>() { user.Email };
            return oMailClass;
        }
    }
}
