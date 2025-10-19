using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace Ahobilam.Models
{
    public class SQLUtil
    {
        public String ConnString { get; set; }
        public String ErrMsg { get; set; }

        public SQLUtil()
        {
            string errMsg = string.Empty;
            ConnString = "";
            ErrMsg = errMsg;
         
        }
        public List<KeyValuePair<int, string>> GetPairInfo(string sqlQuery)
        {
            ErrMsg = string.Empty;

            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlConnection openCon = new SqlConnection(ConnString))
                {

                    SqlCommand command = new SqlCommand(sqlQuery, openCon);
                    openCon.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            list.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1).Trim()));

                        }
                    }
                    reader.Close();
                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }

            return list;
        }
        public int ExecuteQuery(string allQueries)
        {
            int result = 0;
            ErrMsg = string.Empty;
            try
            {
                using (SqlConnection openCon = new SqlConnection(ConnString))
                {
                    SqlCommand cmd = new SqlCommand(allQueries, openCon);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    result = cmd.ExecuteNonQuery();

                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }

            return result;
        }
        public int GetScalarValue(string sqlQuery)
        {
            object result = null;
            int retVal = 0;
            ErrMsg = string.Empty;
            try
            {
                using (SqlConnection openCon = new SqlConnection(ConnString))
                {
                    SqlCommand command = new SqlCommand(sqlQuery, openCon);
                    openCon.Open();
                    result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                        retVal = Convert.ToInt32(result);
                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }
            return retVal;
        }
        public DataTable GetDataTable(string sqlQuery)
        {
            ErrMsg = string.Empty;

            DataTable dataTable = null;
            try
            {
                using (SqlConnection openCon = new SqlConnection(ConnString))
                {

                    SqlCommand command = new SqlCommand(sqlQuery, openCon);
                    openCon.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable = new DataTable();
                            dataTable.Load(reader);
                        }
                        reader.Close();
                    }
                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }

            return dataTable;
        }
        public List<ViewBookingsModel> GetViewBookings(string sqlQuery)
        {
           
            List <ViewBookingsModel> list = new List<ViewBookingsModel>();
            try
            {
                using (SqlConnection openCon = new SqlConnection(ConnString))
                {

                    SqlCommand command = new SqlCommand(sqlQuery, openCon);
                    openCon.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ViewBookingsModel bookings = new ViewBookingsModel();                           
                            bookings.CustID = Convert.ToInt32(reader[0]);
                            bookings.CheckInDate = reader[1].ToString();
                            bookings.CheckOutDate = reader[2].ToString();
                            bookings.NoOfAdults = Convert.ToInt32(reader[3]);
                            bookings.RoomNo = Convert.ToInt32(reader[4]);
                            bookings.RoomType = reader[5].ToString();
                            bookings.Price = Convert.ToInt32(reader[6]);
                            list.Add(bookings);
                        }
                        
                        reader.Close();
                    }
                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }

            return list;
        }
        public List<GuestDetailModel> GetGuestDetails(string sqlQuery)
        {

            List<GuestDetailModel> list = new List<GuestDetailModel>();
            try
            {
                using (SqlConnection openCon = new SqlConnection(ConnString))
                {

                    SqlCommand command = new SqlCommand(sqlQuery, openCon);
                    openCon.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GuestDetailModel guest = new GuestDetailModel();
                            guest.Name = reader[0].ToString();
                            guest.Age = Convert.ToInt32(reader[1]);
                            guest.Sex = reader[2].ToString();
                            guest.Phone = reader[3].ToString();
                            guest.Address = reader[4].ToString();
                            list.Add(guest);
                        }
                        reader.Close();
                    }
                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }

            return list;
        }
        //Access database query
        /*public DataTable GetDataTableAccess(string sqlQuery)
        {
            ErrMsg = string.Empty;

            DataTable dataTable = null;
            try
            {
                using (OleDbConnection openCon = new OleDbConnection(ConnString))
                {

                    OleDbCommand command = new OleDbCommand(sqlQuery, openCon);
                    openCon.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable = new DataTable();
                            dataTable.Load(reader);
                        }
                        reader.Close();
                    }
                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }

            return dataTable;
        }
        public int GetScalarValueAccess(string sqlQuery)
        {
            object result = null;
            int retVal = 0;
            ErrMsg = string.Empty;
            try
            {
                using (OleDbConnection openCon = new OleDbConnection(ConnString))
                {
                    OleDbCommand command = new OleDbCommand(sqlQuery, openCon);
                    openCon.Open();
                    result = command.ExecuteScalar();
                    if (result != null)
                        retVal = Convert.ToInt32(result);
                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }
            return retVal;
        }
        public int ExecuteQueryAccess(string allQueries)
        {
            int result = 0;
            ErrMsg = string.Empty;
            try
            {
                using (OleDbConnection openCon = new OleDbConnection(ConnString))
                {
                    OleDbCommand cmd = new OleDbCommand(allQueries, openCon);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    result = cmd.ExecuteNonQuery();

                    openCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DB Error:" + ex.Message);
            }

            return result;
        }*/
    }
}
