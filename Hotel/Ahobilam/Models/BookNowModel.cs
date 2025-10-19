using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ahobilam.Models
{
    public class BookNowModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Guests { get; set; }
        public string Rooms { get; set; }
        public string RoomType { get; set; }

        public string[] Names { get;set;}        
        public int[] Ages { get; set; }
        public string[] Sexs { get; set; }
        public string[] Phones { get; set; }
        public string[] Addresses { get; set; } 

    }
}
