using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_3.Domain.Models
{
    public class Room : EntityObject
    {
        public int Floor { get; set; }
        public int Number { get; set; }
        public virtual RoomCategory RoomCategory { get; set; } = null!;
        public virtual RoomStatus RoomStatus { get; set; } = null!;
    }
}
