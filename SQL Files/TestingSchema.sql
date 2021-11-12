--RestaurantDetails - Table
select * from RestaurantDetails

--Insert data into RestaurantDetails tables
exec RestaurantDetails_I 'Sagar1','1-Block E, PalmGreens, Reliance Green', '8777899445'

--Update data of RestaurantDetails table
exec RestaurantDetails_U 19,'Saffronn','6-Sec Reliance Greens', '8886543291'

--Delete data from RestaurantDetails table
exec RestaurantDetails_D 15,'RASOI1','6-Sec Reliance Greens', '8886543219'


--Cuisine Table
select * from Cuisine

--Insert/Update data into Cuisine table
exec Cuisine_IU NULL,'20','Thai' --Insert
exec Cuisine_IU 8,'20','Italian' --Update

--Delete data from Cuisine table
exec Cuisine_D 1015,'20','Italian'
exec Cuisine_I '19','Chinese'

--RestaurantMenuItem
select * from RestaurantMenuItem

--Insert data into RestaurantMenuItem table
exec RestaurantMenuItem_I 11,'PavBhajir Dosa',320

--Update data into RestaurantMenuItem table
exec RestaurantMenuItem_U 41,1,'Malaii Kofta',275

--Delete data from RestaurantMenuItem table
exec RestaurantMenuItem_D 51,7,'Chhola Naan',285


--DiningTable
select * from DiningTable

----Insert data into DiningTable table
exec DiningTable_I 20,'Table No 11'

--Update data into DiningTable table
exec DiningTable_U 1,19,'Table No-1'

--Delete data from DiningTable 
exec DiningTable_D 19


--Order Table
select * from OrderInfo

----Insert data into OrderInfo table
exec OrderInfo_I '2021-10-24',19,51,1,100,17

--Update data into OrderInfo table
exec OrderInfo_U 43,'2021-10-21',19,42,4,100,3

--Delete data from OrderInfo table
exec OrderInfo_D 43,'2021-10-21',19,42,4,100,3


--Customer Table
select * from Customer

--Insert data into Customer table
exec Customer_I 20,'Neha Jain','7457768000'

--Update data into Customer table
exec Customer_U 38,19,'Dhyana Nimmavat','9865327845'

--Delete data from Customer table
exec Customer_D 47,20,'Divya Poduval','9457568000'


--Bills Table
select * from Bills

--Insert data into Bills table
exec Bills_I 49,42

--Update data into Bills table
--Update won't be possible in Bills Table

--Delete from Bills table
exec Bills_D 18 



--Queries / Reports
--1) Create procedure to get the list of all currently vacant tables in restaurant. Procedure should accept RestaurantID as input parameter.
exec UFDTableStatus 20

--2) Create procedure to get the list of all customers, Order Details and Table they used for dining. Procedure should be written dynamically so that filters can be applied.
--a. Procedure should accept @FilterBy & @OrderBy variable to filter and order the data dynamically.
exec USPDisplayCustomerDetails 'where O.RestuarantID=19','Order By C.CustomerName Desc'
--exec USPDisplayCustomerDetails 'where O.RestuarantID=20','Order By C.CustomerName Asc'
--exec USPDisplayCustomerDetails null,'Order By C.CustomerName Asc'
--exec USPDisplayCustomerDetails 'where O.RestuarantID=20',null
--exec USPDisplayCustomerDetails null,null


--3) Create a procedure to display Restaurantwise, Yearwise total order amount.
exec USPYearTotalAmount 19
exec USPYearTotalAmount NULL

--4) Create procedure to list daywise, tablewise total order amount.
exec USPDayWiseAmount NULL
exec USPDayWiseAmount 19

--5) Create view to list cuisinewise item details.
--View is available in the view's folder
select * from Vw_CuisineDetails