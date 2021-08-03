using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Common
{
    //Veri tabanındaki kullanıcı işlemleri için kullanılıyor, verilen tipe göre stored procedure de ona göre işlem yapılıyor
    public enum OperationType
    {
        None = 0,
        Signup = 1,
        UpdateConfirmMail = 2,
        CreatePassword = 3
    }
}
