using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_3.Domain.Models
{
    public class User : EntityObject
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
