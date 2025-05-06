# 2025-05-05     Day 1 - SQL

Create a plan for the database(Tables and columns) for the following requirement
It is for a shop that takes orders for custom tailoring
It allows people to place order with measurement and gives a delivery date for the order
The fabric will be picked up from the customer's place
The pick-up date will be specified by the application
Once the order is billed, we can see the billing details
Bills have to be maintained
Once fabric is picked up no cancellation is accepted( before that customer can cancel the order)

CustomerStatusMaster
 Id, StatusMessage(Example - {id-1,StatusMessage-"Available"})

OrderStatusMaster
 Id, StatusMessage(Example - {id-1,StatusMessage-"Order Confirmed"})

 
CityMaster
 Id, CityName, StateId

StateMaster
 Id, StateName

TypeMaster
 Id, Name, status

Items_Mater
 Id, Name

FabricMater
  Id, Name

ITems_Fabric
  Id, Fabric_Id, Item_Id, Price

Address
  Id, Doornumber, Street/Apartmnet Name, Area, Zip code, CityId

Customer_Address
  Id, Name, Address_Id, Customer_Id
 
Customer
 Id, Name, Phone, Email,  Status_Id 


SizeChart
 Id, Measurement(JSON)

Order
 OrerNumber, OrderDate, Customer_Id,  Amount, PicupId, Order_Status_Id, Remarks, Patmnet_Id

Refund_Details
 Refund_id, Order_Numebr, Refund_Initiated_Date, Refund_Date, Amount 

Payment_Datails
  Id, Order_Number, Type, Amount, Status

Order_Details
  Order_Details_Number, Order_Number, Item_Fabric_Id, quantity, Price,  Size_Id

PickUp_And_Delivery_Details
  Id, Date, Order_Numebr, Status, actual_Date, ScheduledDate, Address_Id, Item_Fabric_id
---------------------------------------------------------------------------------------------------------
 
Tables and columns for the following

Case 1: A Simple Case
•	A video store rents movies to members.
•	Each movie in the store has a title and is identified by a unique movie number.
•	A movie can be in VHS, VCD, or DVD format.
•	Each movie belongs to one of a given set of categories (action, adventure, comedy, ... )
•	The store has a name and a (unique) phone number for each member.
•	Each member may provide a favorite movie category (used for marketing purposes).
•	There are two types of members: 
	o	Golden Members:
	o	Bronze Members:
•	Using  their credit cards gold members can rent one or more movies and bronze members max. of one movie.  
•	A member may have a number of dependents (with known names).
•	Each dependent is allowed to rent one (1) movie at a time.
---------------------------------------------------------------------------------------------------------------------
1) https://www.hackerrank.com/challenges/plus-minus/problem?isFullScreen=true

2) https://www.hackerrank.com/challenges/staircase/problem?isFullScreen=true
 
3) https://www.hackerrank.com/challenges/mini-max-sum/problem?isFullScreen=true
 
4) https://www.hackerrank.com/challenges/birthday-cake-candles/problem?isFullScreen=true
 
5) https://www.hackerrank.com/challenges/time-conversion/problem?isFullScreen=true
 
6) https://www.hackerrank.com/challenges/grading/problem?isFullScreen=true
 
7) https://www.hackerrank.com/challenges/apple-and-orange/problem?isFullScreen=true
 
8) https://www.hackerrank.com/challenges/kangaroo/problem?isFullScreen=true
 
9) https://www.hackerrank.com/challenges/between-two-sets/problem?isFullScreen=true
 
10) https://www.hackerrank.com/challenges/breaking-best-and-worst-records/problem?isFullScreen=true

---------------------------------------------------
 
11) https://www.hackerrank.com/challenges/the-birthday-bar/problem?isFullScreen=true
 
12) https://www.hackerrank.com/challenges/divisible-sum-pairs/problem?isFullScreen=true
 
13) https://www.hackerrank.com/challenges/migratory-birds/problem?isFullScreen=true
 
14) https://www.hackerrank.com/challenges/day-of-the-programmer/problem?isFullScreen=true
 
15) https://www.hackerrank.com/challenges/bon-appetit/problem?isFullScreen=true
