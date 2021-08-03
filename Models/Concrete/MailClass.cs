using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class MailClass
    {
        public string FromMailId { get; set; } = "omerbilgin1999@gmail.com";
        public string FromMailIdPassword { get; set; } = "omermen77";
        public List<string> ToMailIds { get; set; } = new List<string>();
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public bool IsBodyHtml { get; set; } = true;
        public List<string> Attachments { get; set; } = new List<string>();

    }
}
