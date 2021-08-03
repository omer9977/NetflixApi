using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Common
{
    //Bu class Startupta connectionstring ve domainname ayarı içindir
    public static class Global
    {
        public static string ConnectionString { get; set; }
        public static string DomainName { get; set; }
    }
}
