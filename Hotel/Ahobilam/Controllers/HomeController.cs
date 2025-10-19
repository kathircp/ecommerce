using Ahobilam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Ahobilam.Controllers
{
    public class HomeController : Controller
    {
        
        private SQLUtil _sqlUtil = new SQLUtil();
        

        public HomeController()
        {
          

            string connString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString; 
            _sqlUtil.ConnString = connString;

        }
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult BookNow()
        {            
            //Available rooms
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            
            //Available rooms
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            
            //Available rooms
            return View();
        }
        [HttpGet]
        public ActionResult Rooms()
        {
            
            //Available rooms
            return View();
        }

        [HttpGet]
        public ActionResult Room_details()
        {
            
            //Available rooms
            return View();
        }
        [HttpGet]
        public ActionResult TempleHistory()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PhotoGraphy()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewBooking()
        {            
            if (TempData["CustID"] != null)
            {
                if (!string.IsNullOrEmpty(TempData["CustID"].ToString()))
                {
                    FileBookingDetails(TempData["CustID"].ToString());
                }
            }
            return View();
        }
        [ActionName("ViewBooking")]
        [HttpPost]
        public ActionResult ViewBooking_post(string searchName)
        {            
            FileBookingDetails(searchName);
            return View();

        }   


        [HttpPost]
        //public JsonResult CheckAvailability(DateTime CheckInDate, DateTime CheckOutDate, string Guests, string Rooms)
        public JsonResult CheckAvailability(BookingViewModel check)
        {
            int result = 0;
            try
            {
          

                string sqlQuery = string.Format(@"select Count(*) from tblRooms where RoomType = '{0}' and RoomNo not in (select RoomNo from tblAvailability where BookedDate between '{1}' and '{2}')", check.RoomType,check.CheckInDate.ToString("dd-MMM-yy"), check.CheckOutDate.ToString("dd-MMM-yy"));
                
                result = _sqlUtil.GetScalarValue(sqlQuery);
                if (result >= Convert.ToInt32(check.Rooms))
                    return Json("SUCCESS");
                else
                    return Json("Sorry...Rooms(s) not available.. Please check for another date...");
            }
            catch (Exception ex)
            {
                throw new JsonException(ex.Message);
            }
            
        }
        [HttpPost]
        public JsonResult SaveData(BookNowModel check)
        {
            int result = 0;
            try
            {
                TempData["CustID"] = "";
                  

                //FInd Rooms
                int roomindex = 0;               
                List<int> lstRooms = new List<int>();
                string sqlQuery = string.Format(@"select RoomNo, RoomType from tblRooms where RoomType = '{0}' and RoomNo not in ( select RoomNo from tblAvailability where BookedDate between '{1}' and '{2}')", check.RoomType, check.CheckInDate.ToString("dd-MMM-yy"), check.CheckOutDate.ToString("dd-MMM-yy"));
                List<KeyValuePair<int,string>> rooms = _sqlUtil.GetPairInfo(sqlQuery);
                if (rooms.Count > 0)
                {
                    //need to check
                    foreach (KeyValuePair<int, string> row in rooms)
                    {
                        roomindex++;
                        lstRooms.Add(row.Key);
                        //insert into tblReservation 
                        string sqlQueryReservation = string.Format("insert into tblReservation values ({0}, '{1}','{2}',{3})", row.Key, check.CheckInDate.ToString("dd-MMM-yy"), check.CheckOutDate.ToString("dd-MMM-yy"), check.Guests);
                        result = _sqlUtil.ExecuteQuery(sqlQueryReservation);
                        if (roomindex == Convert.ToInt32(check.Rooms))
                            break;
                    }
                    //getting customer id
                    string sqlQueryCustID = "select Max(CustID) from tblReservation";
                    int custID = _sqlUtil.GetScalarValue(sqlQueryCustID);
                    //insert into tblCustomers
                    int noofpersons = Convert.ToInt32(check.Guests) * Convert.ToInt32(check.Rooms);
                    //string[] arr = new string[noofpersons];
                    //insert into tblAvailability
                    for (int i = 0; i < noofpersons; i++)
                    {
                        sqlQuery = string.Format("insert into tblCustomers values({0},'{1}','{2}','{3}', {4},'{5}')", custID, check.Names[i], check.Addresses[i], check.Phones[i], check.Ages[i], check.Sexs[i]);
                        result = _sqlUtil.ExecuteQuery(sqlQuery);

                        if (result <= 0)
                            return Json("Sorry...Could not insert customers info.");
                    }
                    //list.CopyTo(arr);
                    //string allQueries = string.Join(";", arr);               
                    //StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < lstRooms.Count; i++)
                    {
                        foreach (DateTime day in EachDay(check.CheckInDate, check.CheckOutDate))
                        {
                            sqlQuery = string.Format("insert into tblAvailability values ({0},'{1}')", lstRooms[i], day.ToString("dd-MMM-yy"));
                            result = _sqlUtil.ExecuteQuery(sqlQuery);
                            if (result <= 0)
                                return Json("Sorry...Could not insert customers info.");
                        }
                    }
                    TempData["CustID"]=custID;
                    //return RedirectToAction("ViewBooking/"+ custID);
                    return Json(custID);
                }
                else
                {
                    return Json("Sorry...Rooms(s) not available.. Please check for another date...");
                }
            }
            catch (Exception ex)
            {
                throw new JsonException(ex.Message);
            }

        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        private void FileBookingDetails(string searchName)
        {
            ViewBag.bookingdetails = null;
            ViewBag.customerdetails = null;
            ViewBag.Msg = "";
            if (!string.IsNullOrEmpty(searchName))
            {
                // Pass your list out to your view              
            
                //View Bookings
                string sqlQuery = "select T1.CustID as CustID,T1.CheckInDate as InDate, T1.CheckOutDate as OutDate, T1.NoOfAdults as NoofAdults,  T1.RoomNo as RoomNo, T2.RoomType as RoomType, T2.Price as Price from tblReservation T1 inner join tblRooms T2 on T1.RoomNo = T2.RoomNo where CustID =" + searchName.Trim();
                List<ViewBookingsModel> listBookings = _sqlUtil.GetViewBookings(sqlQuery);
                //var data = list.ToList();
                ViewBag.bookingdetails = listBookings;
                string sqlQuery2 = "select [Name], Age, Sex, PhoneNo, [Address] from tblCustomers where CustID = " + searchName.Trim();
                List<GuestDetailModel> listCusts = _sqlUtil.GetGuestDetails(sqlQuery2);
                ViewBag.customerdetails = listCusts;

                if ((listBookings.Count == 0) && (listCusts.Count == 0))
                {
                    ViewBag.Msg = "Invalid Customer ID... Please Enter the valid Customer ID..";
                }

            }
        }
    }
}
