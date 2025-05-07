``` sql
use pubs
go

select * from publishers

select * from titles

select title,pub_id from titles

select title, pub_name 
from titles join publishers
on titles.pub_id = publishers.pub_id

select title, pub_name, publishers.pub_id 
from titles join publishers 
on titles.pub_id = publishers.pub_id

select title, pub_name, p.pub_id 
from titles t join publishers p
on t.pub_id = p.pub_id

--print the publisher deatils of the publisher who has never published
select * from publishers where pub_id not in
(select distinct pub_id from titles)

select title, pub_name 
from titles right outer join publishers
on titles.pub_id = publishers.pub_id

--Select the author_id for all the books. Print the author_id and the book name
SELECT au_id, title 
FROM titleauthor ta
JOIN titles t
ON ta.title_id = t.title_id;

select * from authors

select concat(au_fname,' ',au_lname), title_id Author_Name from authors a
join titleauthor ta on a.au_id = ta.au_id
order by title_id
--or
select concat(au_fname,' ',au_lname), title_id Author_Name from authors a
join titleauthor ta on a.au_id = ta.au_id
order by 2

select concat(au_fname,' ',au_lname)  Author_Name, title Book_Name from authors a
join titleauthor ta on a.au_id = ta.au_id
join titles t on ta.title_id = t.title_id

select * from sales

--Print the publisher's name, book name and the order date of the  books
SELECT pub_name Publisher_Name, title Book_Name, ord_date Order_Date
FROM publishers p JOIN titles t ON p.pub_id = t.pub_id
JOIN sales s ON s.title_id = t.title_id
order by 3 desc

--Print the publisher name and the first book sale date for all the publishers
SELECT pub_name Publisher_Name, min(ord_date) First_Order_Date
FROM publishers p left outer JOIN titles t ON p.pub_id = t.pub_id
left outer JOIN sales s ON s.title_id = t.title_id
group by  pub_name
order by 2 desc

select * from stores

--print the bookname and teh store address of the sale
SELECT
    T.title AS Book_Name,
    CONCAT(S.stor_address,',', S.city, ',', S.state) AS Store_Address
FROM
    sales AS SL 
JOIN
    titles AS T ON SL.title_id = T.title_id
JOIN
    stores AS S ON SL.stor_id = S.stor_id
ORDER BY 1;

create procedure proc_FirstProcedure
as
begin
   print 'Hello World!'
end
Go
exec proc_FirstProcedure

create table Products
(id int identity(1,1) constraint pk_productId primary key,
name nvarchar(100) not null,
details nvarchar(max))
Go
create proc proc_InsertProduct(@pname nvarchar(100),@pdetails nvarchar(max))
as
begin
    insert into Products(name,details) values(@pname,@pdetails)
end
go
proc_InsertProduct 'Laptop','{"brand":"HP","spec":{"ram":"16GB","cpu":"i7"}}'
go
select * from Products

create or alter proc proc_InsertProduct(@pname nvarchar(100),@pdetails nvarchar(max))
as
begin
    insert into Products(name,details) values(@pname,@pdetails)
end

select JSON_QUERY(details, '$.spec') Product_Specification from products

create proc proc_UpdateProductSpec(@pid int,@newvalue varchar(20))
as
begin
   update products set details = JSON_MODIFY(details, '$.spec.ram',@newvalue) where id = @pid
end

proc_UpdateProductSpec 1, '24GB'

select id, name, JSON_VALUE(details, '$.brand') Brand_Name
from Products

  create table Posts
  (id int primary key,
  title nvarchar(100),
  user_id int,
  body nvarchar(max))
Go

  select * from Posts

  create proc proc_BulInsertPosts(@jsondata nvarchar(max))
  as
  begin
		insert into Posts(user_id,id,title,body)	
	  select userId,id,title,body from openjson(@jsondata)
	  with (userId int,id int, title varchar(100), body varchar(max))
  end

  delete from Posts

  proc_BulInsertPosts '
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
  }]'

  select * from products

  select * from products where 
  try_cast(json_value(details,'$.spec.cpu') as nvarchar(20)) ='i7'

  --create a procedure that brings post by taking the user_id as parameter

```