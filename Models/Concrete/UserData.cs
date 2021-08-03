using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class UserData
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
    }
}
