using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.BLL.Models;

namespace RestaurantConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            RestaurantDetailsBLL.SelectData();
            Console.WriteLine("Select Action: ");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            int inputkey = Convert.ToInt32(Console.ReadLine());

            RestaurantDetailsBLL res = new RestaurantDetailsBLL();

            //get data from user
            //RestaurantDetails.Main(new string[] { });

            if (inputkey == 1)
            {
                try
                { 
                    Console.WriteLine("Restaurant Name:");
                    string RName = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Address:");
                    string Add = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Mobile Number:");
                    string MobNo = Convert.ToString(Console.ReadLine());

                    //var result = RestaurantDetails.InsertRestaurant(new RestaurantDetails() { Name = RName, Address = Add, MobileNo = MobNo });
                    RestaurantDetailsBLL obj = new RestaurantDetailsBLL();
                    obj.Name = RName;
                    obj.Address = Add;
                    obj.MobileNo = MobNo;
                    //var result = RestaurantDetailsBLL.InsertRestaurant(obj);
                    var result = obj.InsertRestaurant(obj);
                    Console.WriteLine(result);
                //    if (result == true)
                //    {
                //        Console.WriteLine("Record was successfully added");
                //    }
                //    else
                //    {
                //        Console.WriteLine("Record was not added");
                //    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                }
            }
            else if (inputkey == 2)
            {
                try
                {

                    Console.WriteLine("Restaurant Id:");
                    int RID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Restaurant Name:");
                    string RName = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Address:");
                    string Add = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Mobile Number:");
                    string MobNo = Convert.ToString(Console.ReadLine());

                    //var result = RestaurantDetailsBLL.UpdateRestaurant(new RestaurantDetailsBLL() { ID = RID, Name = RName, Address = Add, MobileNo = MobNo });


                    RestaurantDetailsBLL obj = new RestaurantDetailsBLL();
                    obj.ID = RID;
                    obj.Name = RName;
                    obj.Address = Add;
                    obj.MobileNo = MobNo;
                    //var result = RestaurantDetailsBLL.UpdateRestaurant(obj);
                    var result = obj.UpdateRestaurant(obj);
                    Console.WriteLine(result);
                    //if (result)
                    //{
                    //    Console.WriteLine("Record updated successfully");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Record was not updated");
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
            else if (inputkey == 3)
            {
                try
                {
                    Console.WriteLine("Enter Restaurant Id to delete:");
                    int RID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Restaurant Name:");
                    string RName = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Address:");
                    string Add = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Number:");
                    string MobNo = Convert.ToString(Console.ReadLine());


                    //var result = RestaurantDetailsBLL.DeleteRestaurant(new RestaurantDetailsBLL() { ID = RID, Name = RName, Address = Add, MobileNo = MobNo });

                    RestaurantDetailsBLL obj = new RestaurantDetailsBLL();
                    obj.ID = RID;
                    obj.Name = RName;
                    obj.Address = Add;
                    obj.MobileNo = MobNo;
                    var result = obj.DeleteRestaurant(obj);

                    Console.WriteLine(result);
                    //if (result)
                    //{
                    //    Console.WriteLine("Record deleted successfully");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Record was not deleted");
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
            else
            {
                Console.WriteLine("Select proper option");
            }
            Console.ReadKey();

        }
    }
}
