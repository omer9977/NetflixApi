using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Common
{
    //Bu class kullanıcı authantication işlemleri için kullanıcıya bilgilendirme amaçlıdır
    public static class Message
    {
        public static string Success = "Success"; 
        public static string ErrorFound = "Error Found"; 
        public static string UserAlreadyCreated = "Kullanıcı Zaten kayıtlı."; 
        public static string VerifyMail = "Kullanıcı Zaten Kayıtlı, Lütfen Email Adresinizi Doğrulayın."; 
        public static string InvalidUser = "Geçersiz Kullanıcı. Lütfen Yeni Bir Hesap Oluşturunuz."; 
        public static string MailSent = "Mail Gönderildi"; 
        public static string UserCreatedVerifyMail = "Kullanıcı Oluşturuldu, Mail Adresinizi Kontrol Ediniz. Linke Tıklayın ve Doğrulayınız.";
    }
}
