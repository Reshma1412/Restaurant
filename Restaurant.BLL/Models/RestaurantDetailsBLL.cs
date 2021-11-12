using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace Restaurant.BLL.Models
{
    public class RestaurantDetailsBLL
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }

        //public void SelectData()
        public static void SelectData()
        {
            try
            {
                string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
                SqlConnection con = new SqlConnection(ConnectionString);

                //select query
                string displayQuery = "select * from RestaurantDetails";
                SqlCommand displaycommand = new SqlCommand(displayQuery, con);
                con.Open();
                SqlDataReader dataReader = displaycommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine("ID: " + dataReader.GetValue(0).ToString());
                    Console.WriteLine("Name: " + dataReader.GetValue(1).ToString());
                    Console.WriteLine("Address: " + dataReader.GetValue(2).ToString());
                    Console.WriteLine("Mobile Number: " + dataReader.GetValue(3).ToString());
                }
                dataReader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("oops, something went wrong.\n" + ex.ToString());
            }
        }

        public List<RestaurantDetailsBLL> SelectRestaurantData()
        {
            string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConnectionString);
            var com = new SqlCommand("SELECT * FROM RestaurantDetails", con);
            com.CommandType = System.Data.CommandType.Text; 
            var sqlDataAdapter = new SqlDataAdapter(com);
            var dt = new DataTable();



            con.Open();
            sqlDataAdapter.Fill(dt);
            con.Close();

            var resultList = new List<RestaurantDetailsBLL>();

            foreach (DataRow dr in dt.Rows)
            {
                var res = new RestaurantDetailsBLL
                {
                    ID = Convert.ToInt32(dr["RestuarantID"]),
                    Name = Convert.ToString(dr["RestuarantName"]),
                    Address = Convert.ToString(dr["RestaurantAddress"]),
                    MobileNo = Convert.ToString(dr["MobileNo"])
                };
                resultList.Add(res);
            }

            return resultList;
        }


        public string InsertRestaurant(RestaurantDetailsBLL restaurant)
        {
            try
            {

                string connectionstring = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
                string sql = "RestaurantDetails_I";
                //string sql="insert into restaurantdetails (restuarantname,restaurantaddress,mobileno) values('"+restaurant.name+"','"+restaurant.address+"','"+restaurant.mobileno+"')";


                // create ado.net objects.

                SqlConnection con = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = cmd.Parameters.AddWithValue("@restaurantname", restaurant.Name);
                param = cmd.Parameters.AddWithValue("@restaurantaddress", restaurant.Address);
                param = cmd.Parameters.AddWithValue("@mobileno", restaurant.MobileNo);

                
                // execute the command.
                cmd.Connection = con;
                con.Open();

                //int rowsaffected = cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();

                con.Close();
               
                //if (rowsaffected > 0)
                //    return true;
                //else
                //    return false;
                return "Record added successfully";

            }
            catch (Exception ex)
            {

               
                return ex.Message;
            }

        }

        //public static string UpdateRestaurant(RestaurantDetailsBLL restaurant)
        public string UpdateRestaurant(RestaurantDetailsBLL restaurant)
        {
            try
            {

                string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
                //string SQL = "Update RestaurantDetails set (RestuarantName,RestaurantAddress,MobileNo) Values('" + restaurant.Name + "','" + restaurant.Address + "','" + restaurant.MobileNo + "') where RestaurantID='"+restaurant.ID+"'";
                string SQL = "RestaurantDetails_U";

                // Create ADO.NET objects.

                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = cmd.Parameters.AddWithValue("@RestuarantID", restaurant.ID);
                param = cmd.Parameters.AddWithValue("@RestaurantName", restaurant.Name);
                param = cmd.Parameters.AddWithValue("@RestaurantAddress", restaurant.Address);
                param = cmd.Parameters.AddWithValue("@MobileNo", restaurant.MobileNo);

                // Execute the command.
                cmd.Connection = con;
                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();


                con.Close();
                

                //if (rowsAffected > 0)
                //    return true;
                //else
                //    return false;

                return "Record updates succesfully";
            }
            catch (Exception ex)
            {
                
                return ex.Message;
            }

        }

        public string DeleteRestaurant(RestaurantDetailsBLL restaurant)
        {
            try
            {

                string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
                string SQL = "RestaurantDetails_D";

                // Create ADO.NET objects.

                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = cmd.Parameters.AddWithValue("@RestuarantID", restaurant.ID);
                param = cmd.Parameters.AddWithValue("@RestaurantName", restaurant.Name);
                param = cmd.Parameters.AddWithValue("@RestaurantAddress", restaurant.Address);
                param = cmd.Parameters.AddWithValue("@MobileNo", restaurant.MobileNo);

                // Execute the command.
                cmd.Connection = con;
                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();


                con.Close();
                
                //if (rowsAffected > 0)
                //    return true;
                //else
                //    return false;
                return "Record was deleted successfully";
            }
            catch (Exception ex)
            {
               
                return ex.Message;
            }

        }

    }
}
