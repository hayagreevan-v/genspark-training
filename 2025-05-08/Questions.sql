use northwind;


-- 1) List all orders with the customer name and the employee who handled the order.
-- (Join Orders, Customers, and Employees)

select o.OrderID, contactname as customer, firstname+' '+lastname as employee from 
orders o join customers c on c.customerid = o.customerid
join employees e on e.employeeid = o.employeeid;


-- 2) Get a list of products along with their category and supplier name.
-- (Join Products, Categories, and Suppliers)

SELECT P.ProductID,p.ProductName, CategoryName as Category, CompanyName as Supplier FROM PRODUCTS P 
JOIN CATEGORIES C ON C.CATEGORYID = P.CATEGORYID
JOIN SUPPLIERS S ON P.SUPPLIERID = S.SUPPLIERID;


-- 3) Show all orders and the products included in each order with quantity and unit price.
-- (Join Orders, Order Details, Products)

SELECT o.OrderID, p.ProductName,Quantity, p.UnitPrice FROM Orders o 
JOIN [Order Details] od ON o.OrderID = od.OrderID
JOIN Products p ON p.ProductID = od.ProductID
ORDER BY OrderID, ProductName;


-- 4) List employees who report to other employees (manager-subordinate relationship).
-- (Self join on Employees)

SELECT e1.EmployeeID,e1.FirstName+' ' + e1.LastName as Name, e2.FirstName+' '+e2.LastName as ReportsTo FROM Employees e1 JOIN Employees e2 ON e1.ReportsTo = e2.EmployeeID;


-- 5) Display each customer and their total order count.
-- (Join Customers and Orders, then GROUP BY)

SELECT ContactName as Customer, Count(*) AS Total_Orders FROM Customers c JOIN Orders o ON o.CustomerID = c.CustomerID group by ContactName;

-- 6) Find the average unit price of products per category.
-- Use AVG() with GROUP BY

SELECT c.CategoryID, CategoryName, AVG(UnitPrice) as Average_Price FROM Products p JOIN Categories c on p.CategoryID = c.CategoryID 
GROUP BY c.CategoryID, CategoryName;


-- 7) List customers where the contact title starts with 'Owner'.
-- Use LIKE or LEFT(ContactTitle, 5)

SELECT * FROM Customers WHERE ContactTitle LIKE 'Owner%';


-- 8) Show the top 5 most expensive products.
-- Use ORDER BY UnitPrice DESC and TOP 5

SELECT TOP 5 * FROM Products ORDER BY UnitPrice DESC;


-- 9) Return the total sales amount (quantity � unit price) per order.
-- Use SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) and GROUP BY

SELECT OrderID, SUM(Quantity*UnitPrice) AS Total_Sales_Amount FROM [Order Details] 
GROUP BY OrderID;


-- 10) Create a stored procedure that returns all orders for a given customer ID.
-- Input: @CustomerID

CREATE PROCEDURE proc_CustomerOrders (@CustomerID NVARCHAR(10))
AS
BEGIN
	SELECT * FROM Orders WHERE CustomerID = @CustomerID;
END;

SELECT * FROM CUSTOMERS;
EXEC proc_CustomerOrders 'ANATR';


-- 11) Write a stored procedure that inserts a new product.
-- Inputs: ProductName, SupplierID, CategoryID, UnitPrice, etc.

SELECT * FROM Products;

CREATE PROCEDURE proc_InsertProduct (@productName NVARCHAR(50), @supplierID INT, @categoryID INT, @unitPrice FLOAT)
AS
BEGIN
	INSERT INTO Products(ProductName, SupplierID, CategoryID, UnitPrice)
	VALUES (@productName,@supplierID,@categoryID,@unitPrice);
END

EXEC proc_InsertProduct 'Coffee',2,3,20;


-- 12) Create a stored procedure that returns total sales per employee.
-- Join Orders, Order Details, and Employees

CREATE PROCEDURE proc_TotalSalesPerEmployee 
AS BEGIN
	SELECT e.EmployeeID, SUM(Quantity*UnitPrice) as Total_Sales_Per_Employee
	FROM Employees e 
	JOIN Orders o ON o.EmployeeID = e.EmployeeID
	JOIN [Order Details] od ON od.OrderID = o.OrderID
	GROUP BY e.EmployeeID;
END;

EXEC proc_TotalSalesPerEmployee;


-- 13) Use a CTE to rank products by unit price within each category.
-- Use ROW_NUMBER() or RANK() with PARTITION BY CategoryID

With ProductRanking as (
	SELECT ProductID,CategoryID,UnitPrice, ROW_NUMBER() OVER(PARTITION BY CategoryID ORDER BY UnitPrice) as RANK FROM Products
)
SELECT * FROM ProductRanking;


-- 14) Create a CTE to calculate total revenue per product and filter products with revenue > 10,000.

WITH TotalRevenuePerProduct as (
	SELECT ProductID, SUM(UnitPrice*Quantity) AS Total_Revenue FROM [Order Details] 
	GROUP BY ProductID
)

SELECT * FROM TotalRevenuePerProduct WHERE Total_Revenue > 10000 ORDER BY Total_Revenue DESC;


-- 15) Use a CTE with recursion to display employee hierarchy.
-- Start from top-level employee (ReportsTo IS NULL) and drill down

WITH EmployeeHierarchy AS (
	SELECT EmployeeID, FirstName+' '+LastName as Name, ReportsTo, 1 as HierarchyLevel FROM Employees WHERE ReportsTo IS NULL
	UNION ALL
	SELECT E1.EmployeeID, E1.FirstName+' '+E1.LastName as Name, E1.ReportsTo, E2.HierarchyLevel+1 FROM Employees e1 JOIN EmployeeHierarchy e2 ON e1.ReportsTo = E2.EmployeeID
)
SELECT * FROM EmployeeHierarchy;
