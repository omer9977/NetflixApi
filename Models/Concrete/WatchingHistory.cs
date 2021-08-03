using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class WatchingHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public Int16 PassingTime { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
