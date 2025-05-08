use pubs;

SELECT * FROM products;

 -- Procedure with Return parameter

CREATE PROCEDURE proc_CountCpu (@pcpu NVARCHAR(5), @pcount INT OUT)
AS
BEGIN
	set @pcount = (SELECT COUNT(*) FROM products 
				  WHERE TRY_CAST(JSON_VALUE(details,'$.specs.cpu') AS nvarchar(5)) = @pcpu)
END;


BEGIN
DECLARE @cnt INT;
EXEC proc_CountCpu 'i7', @cnt OUT;
PRINT @cnt;
END;





-- Bulk Insertion Procedure with Try-Catch

create table people
(id int primary key,
name nvarchar(20),
age int)

create table BulkInsertLog
(LogId int identity(1,1) primary key,
FilePath nvarchar(1000),
status nvarchar(50) constraint chk_status Check(status in('Success','Failed')),
Message nvarchar(1000),
InsertedOn DateTime default GetDate())


create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
  Begin try
	   declare @insertQuery nvarchar(max)

	   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
	   with(
	   FIRSTROW =2,
	   FIELDTERMINATOR='','',
	   ROWTERMINATOR = ''\n'')'
	   print @insertQuery
	   exec sp_executesql @insertQuery

	   insert into BulkInsertLog(filepath,status,message)
	   values(@filepath,'Success','Bulk insert completed')
  end try
  begin catch
		 insert into BulkInsertLog(filepath,status,message)
		 values(@filepath,'Failed',Error_Message())
  END Catch
end



SELECT * FROM PEOPLE;
SELECT * FROM BulkInsertLog;

EXEC proc_BulkInsert 'D:\\Genspark_Training\\2025-05-08\\Data.csv' ;






-- Common Table Expression (CTE)
with cteAuthors
as
(select au_id, concat(au_fname,' ',au_lname) author_name from authors)

select * from cteAuthors;


--Pagination CTE
DECLARE @page int =2, @pageSize int =10;
with PaginatedBooks as (
	SELECT title_id, title,price, ROW_NUMBER() OVER( ORDER BY PRICE desc) AS ROWNUM
	FROM titles
)
SELECT * FROM PaginatedBooks where ROWNUM between ((@page-1)*@pageSize+1) and (@page * @pageSize);


--create a sp that will take the page number and size as param and print the books
CREATE PROCEDURE proc_PaginateTitles (@page INT, @pageSize INT)
AS
BEGIN
	with PaginatedBooks as (
		SELECT title_id, title,price, ROW_NUMBER() OVER( ORDER BY PRICE desc) AS ROWNUM
		FROM titles
	)
	SELECT * FROM PaginatedBooks where ROWNUM between ((@page-1)*@pageSize+1) and (@page * @pageSize)
END

EXEC proc_PaginateTitles 2,5;


-- Pagination using OFFSET and FETCH  (LAtest Method)

SELECT title_id, title,price
FROM titles
ORDER BY price desc
OFFSET 5 ROWS FETCH NEXT 5 ROWS ONLY;


-- Functions

-- SCALAR VALUE FUNCTION - RETURNS A SINGLE VALUE
CREATE FUNCTION fn_CalculateTax (@baseprice INT, @tax INT)
RETURNS INT
AS
BEGIN
	return (@baseprice + (@baseprice * @tax/100));
END

SELECT dbo.fn_CalculateTax(1000,10);

SELECt title, dbo.fn_CalculateTax(price,12) FROM titles;


-- TABLE VALUE FUNCTION - RETURNS TABLE
-- DOES NOT support begin - end
CREATE function fn_tableSample (@minprice INT)
RETURNS TABLE
AS
return SELECT title, price FROM titles WHERE price >= @minprice;


SELECT * FROM dbo.fn_tableSample(10);

-- Older Method which supports Begin-end along with definition of table

CREATE OR ALTER function fn_tableSampleOld (@minprice INT)
RETURNS @Results TABLE (Book_Name NVARCHAR(100), PRICE FLOAT)
AS
BEGIN
	INSERT INTO @Results SELECT title, price FROM titles WHERE price >= @minprice;
	RETURN
END

SELECT * FROM dbo.fn_tableSample(10);
