use pubs;

 

SELECT au_id, title FROM titles t JOIN titleauthor ta ON t.title_id = ta.title_id;

SELECT a.au_id, a.au_fname+ ' '+ a.au_lname as Author,title 
FROM titles t JOIN titleauthor ta ON t.title_id = ta.title_id
JOIN authors a ON ta.au_id = a.au_id order by 2;

SELECT pub_name, title, ord_date 
FROM publishers p JOIN titles t ON p.pub_id = t.pub_id
JOIN sales s ON s.title_id = t.title_id;

SELECT pub_name as Publisher_Name,  MIN(ord_date) as First_Book_Sale
FROM publishers p LEFT JOIN titles t ON p.pub_id = t.pub_id
LEFT JOIN sales s ON s.title_id = t.title_id group by pub_name ;

SELECT title as Book_Name, stor_address as Store_Address
FROM titles t JOIN sales s ON s.title_id = t.title_id
JOIN stores st ON st.stor_id= s.stor_id
order by 2;


CREATE PROCEDURE proc_First
AS
BEGIN
	print 'Hello World!';
END;



CREATE PROCEDURE proc_Second
AS
BEGIN
	print 'Hello World!';
END;

EXEC proc_Second;


CREATE TABLE proc_demo (id INT PRIMARY KEY IDENTITY(1,1), name NVARCHAR(100));
SELECT * FROM proc_demo;

CREATE PROCEDURE proc_table_insert (@name NVARCHAR(100))
AS
BEGIN
	INSERT INTO proc_demo(name) VALUES(@name);
END;

EXEC proc_table_insert 'Hex';

CREATE TABLE products (id INT PRIMARY KEY IDENTITY(1,1), name NVARCHAR(100), details NVARCHAR(max));

CREATE PROCEDURE proc_insertProduct (@name NVARCHAR(100),@details NVARCHAR(MAX))
AS
BEGIN
	INSERT INTO products(name,details) VALUES(@name, @details);
END;

EXEC proc_insertProduct'Hex','{"brand":"dell","specs":{"ram":"16gb","cpu":"i7"}}';
EXEC proc_insertProduct'Hey','{"brand":"asus","specs":{"ram":"16gb","cpu":"i5"}}';

SELECT * FROM products;

SELECT name, JSON_VALUE(details,'$.brand') FROM products;

SELECT name, JSON_QUERY(details,'$.specs') FROM products;


CREATE PROCEDURE proc_updateProductRam (@id INT, @ram NVARCHAR(5))
AS
BEGIN
	UPDATE products SET details = JSON_MODIFY(details,'$.specs.ram',@ram) WHERE id = @id;
END;

EXEC proc_updateProductRam 1,'24gb';


-- Relational Operation on JSON Data
SELECT * FROM products
WHERE TRY_CAST(JSON_VALUE(details,'$.specs.cpu') as nvarchar(5)) = 'i7';

-- Inserting JSON Bulk Data

CREATE TABLE jsonTable (user_Id INT, id INT PRIMARY KEY, title NVARCHAR(100), body NVARCHAR(MAX));


declare @jsonData NVARCHAR(MAX) = '
[
  {
    "userId": 1,
    "id": 1,
    "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
    "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
  },
  {
    "userId": 1,
    "id": 2,
    "title": "qui est esse",
    "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
  }
]';
INSERT INTO jsonTable (user_Id, id, title, body)
SELECT userId, id, title, body FROM openjson(@jsonData)
WITH (userId INT, id INT, title NVARCHAR(100), body NVARCHAR(MAX));

-- ---------------------

-- create a procedure that brings post by taking the user_id as parameter
CREATE PROCEDURE proc_getPost(@id INT)
AS BEGIN
	SELECT * FROM jsonTable WHERE id = @id;
END;

EXEC proc_getPost 1;