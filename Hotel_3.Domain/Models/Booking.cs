using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_3.Domain.Models
{
    public class Booking : EntityObject
    {
        public DateTime EnterDate { get; set; }
        public DateTime ExitDate { get; set; }
        public virtual Room Room { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
    }
}
