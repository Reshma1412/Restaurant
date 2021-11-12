using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Restaurant.BLL.Models
{
    public class CuisineBLL
    {
        public int CuisineID { get; set; }
    
        [Required(ErrorMessage = "Restaurant ID is required.")]
        public int RestaurantID { get; set; }
        

        [Required(ErrorMessage = "Cuisine Name is required.")]
        public string CuisineName { get; set; }

        public static void SelectData()
        {
            string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConnectionString);

            //select query
            string displayQuery = "select * from cuisine";
            SqlCommand displaycommand = new SqlCommand(displayQuery, con);
            con.Open();
            SqlDataReader dataReader = displaycommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine("CuisineID: " + dataReader.GetValue(0).ToString());
                Console.WriteLine("RestuarantID: " + dataReader.GetValue(1).ToString());
                Console.WriteLine("CuisineName: " + dataReader.GetValue(2).ToString());
            }
            dataReader.Close();
            con.Close();
        }

        public List<CuisineBLL> SelectCuisineData()
        {
            string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConnectionString);
            var com = new SqlCommand("SELECT * FROM Cuisine", con);
            com.CommandType = System.Data.CommandType.Text;
            var sqlDataAdapter = new SqlDataAdapter(com);
            var dt = new DataTable();



            con.Open();
            sqlDataAdapter.Fill(dt);
            con.Close();

            var resultList = new List<CuisineBLL>();

            foreach (DataRow dr in dt.Rows)
            {
                var res = new CuisineBLL
                {
                    CuisineID = Convert.ToInt32(dr["CuisineID"]),
                    RestaurantID = Convert.ToInt32(dr["RestuarantID"]),
                    CuisineName = Convert.ToString(dr["CuisineName"]),
                    
                };
                resultList.Add(res);
            }

            return resultList;
        }


        public bool InsertCuisine(CuisineBLL cuisine)
        {
            try
            {

                string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
                string SQL = "Cuisine_I";

                // Create ADO.NET objects.

                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                //param = cmd.Parameters.AddWithValue("@CuisineID", cuisine.CuisineID);
                param = cmd.Parameters.AddWithValue("@RestuarantID", cuisine.RestaurantID);
                param = cmd.Parameters.AddWithValue("@CuisineName", cuisine.CuisineName);

                // Execute the command.
                cmd.Connection = con;
                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();
                

                if (rowsAffected > 0)
                    return true;
                else
                    return false;

                
            }
            catch (Exception)
            {
               
                return false;
               
            }

        }


        //To view cuisine details with generic list     
        public List<CuisineBLL> GetAllCuisines()
        {
            string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
            string SQL = "Select * from Cuisine";

            SqlConnection con = new SqlConnection(ConnectionString);
           
            List<CuisineBLL> CuisineList = new List<CuisineBLL>();


            SqlCommand cmd = new SqlCommand(SQL, con);
           // cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                CuisineList.Add(

                    new CuisineBLL 
                    {

                        CuisineID = Convert.ToInt32(dr["CuisineId"]),
                        RestaurantID  = Convert.ToInt32(dr["RestuarantID"]),
                        CuisineName  = Convert.ToString(dr["CuisineName"])

                    }
                    );
            }

            return CuisineList;
        }

        public bool UpdateCuisine(CuisineBLL cuisine)
        {
            try
            {

                string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
                string SQL = "Cuisine_U";

                // Create ADO.NET objects.

                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = cmd.Parameters.AddWithValue("@CuisineID", cuisine.CuisineID);
                param = cmd.Parameters.AddWithValue("@RestuarantID", cuisine.RestaurantID);
                param = cmd.Parameters.AddWithValue("@CuisineName", cuisine.CuisineName);

                // Execute the command.
                cmd.Connection = con;
                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();


                con.Close();
               
                if (rowsAffected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                
                return false;
            }

        }

        public bool DeleteCuisine(CuisineBLL cuisine)
        {
            try
            {

                string ConnectionString = "Data Source=.;Initial Catalog=Restaurant;Integrated Security=True";
                string SQL = "Cuisine_D";

                // Create ADO.NET objects.

                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = cmd.Parameters.AddWithValue("@CuisineID", cuisine.CuisineID);
                param = cmd.Parameters.AddWithValue("@RestuarantID", cuisine.RestaurantID);
                param = cmd.Parameters.AddWithValue("@CuisineName", cuisine.CuisineName);
                

                // Execute the command.
                cmd.Connection = con;
                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();


                con.Close();

              

                if (rowsAffected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                
                return false;
            }

        }
    }
}
