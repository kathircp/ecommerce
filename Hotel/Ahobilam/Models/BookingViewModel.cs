using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ahobilam.Models
{
    public class BookingViewModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Guests { get; set; }
        public string Rooms { get; set; }
        public string RoomType { get; set; }

    }
}
