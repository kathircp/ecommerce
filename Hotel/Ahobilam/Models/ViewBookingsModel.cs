
namespace Ahobilam.Models
{
    public class ViewBookingsModel
    {
        public int CustID { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int NoOfAdults { get; set; }
        public int RoomNo { get; set; }      
        public string RoomType { get; set; }
        public int Price { get; set; }

    }
}
