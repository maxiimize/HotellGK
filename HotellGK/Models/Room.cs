using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; } // Single, Double
        public bool HasExtraBeds { get; set; }
        public bool IsAvailable { get; set; }
    }
}
