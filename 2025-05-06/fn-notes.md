# 2025-05-06 Day 2 - SQL

## Queries

1) Print all the titles names

``` sql
SELECT title FROM titles;
 ```

2) Print all the titles that have been published by 1389

``` sql
SELECT title FROM titles where pub_id = 1389;
 ```

3) Print the books that have price in range of 10 to 15

``` sql
SELECT * FROM titles where price between 10 and 15;
 ```

4) Print those books that have no price

``` sql
SELECT * FROM titles where price is null;
 ```

5) Print the book names that starts with 'The'

``` sql
SELECT * FROM titles where title like 'The%';
 ```

6) Print the book names that do not have 'v' in their name

``` sql
SELECT * FROM titles where title not like '%v%';
```

7) print the books sorted by the royalty

``` sql
SELECT * FROM titles order by royalty;
```

8) print the books sorted by publisher in descending then by types in ascending then by price in descending

``` sql
SELECT * FROM titles order by pub_id desc, type asc, price desc;
 ```

9) Print the average price of books in every type

``` sql
SELECT type, avg(price) FROM titles group by type;
 ```

10) print all the types in unique

``` sql
SELECT DISTINCT type FROM titles;
 ```

11) Print the first 2 costliest books

``` sql
SELECT TOP 2 * FROM titles order by price desc ;
 ```

12) Print books that are of type business and have price less than 20 which also have advance greater than 7000

``` sql
select * from titles where type = 'business' and price< 20 and advance>7000;
 ```


13) Select those publisher id and number of books which have price between 15 to 25 and have 'It' in its name. Print only those which have count greater than 2. Also sort the result in ascending order of count

``` sql
select pub_id, count(*) from titles where price between 15 and 25 and title LIKE '%It%' group by pub_id having count(*)>2 order by count(*);
```

 
14) Print the Authors who are from 'CA'

``` sql
SELECT * from authors where state='CA';
 ```

15) Print the count of authors from every state

``` sql
SELECT state, count(*) from authors group by state;
```


## Design Database
Design the database for a shop which sells products
Points for consideration
  1) One product can be supplied by many suppliers
  2) One supplier can supply many products
  3) All customers details have to present
  4) A customer can buy more than one product in every purchase
  5) Bill for every purchase has to be stored
  6) These are just details of one shop


### My Approach

- statemaster
	- id, state

- CityMaster
	- id, city, state_id

- OrderStatusMaster
	- id, status

- address
	- address_id, address line1, line2, city_id, pincode

- products
	- product_id, product_name, type

- suppliers
	- supplier_id, supplier_name, address_id

- Products-Suppliers
	- ps_id, product_id, supplier_id, quantity, unit_price

- customer_status
	- cs_id, status
- customers
	- customer_id, name, address_id, phoneNo, email, customer_status

- orders
	- order_id, customer_id, date, amount, order_status

- orderdetails
	- id, order_id, ps_id, quantity, unit_price

``` sql
CREATE DATABASE demo;

use demo;

CREATE TABLE state_master (
	state_id INT PRIMARY KEY,
	name VARCHAR(20)
);

exec sp_help state_master;

CREATE TABLE city_master (
	city_id INT PRIMARY KEY,
	name VARCHAR(20),
	state_id INT REFERENCES state_master(state_id)
);

exec sp_help city_master;

CREATE TABLE address (
	address_id INT PRIMARY KEY,
	line_1 VARCHAR(50),
	line_2 VARCHAR(50),
	city_id INT REFERENCES city_master(city_id),
	pincode NUMERIC(6)
);

CREATE TABLE products (
	product_id INT PRIMARY KEY,
	name VARCHAR(50),
	type VARCHAR(20)
);

CREATE TABLE suppliers (
	supplier_id INT PRIMARY KEY,
	name VARCHAR(50),
	address_id INT REFERENCES address(address_id)
);

CREATE TABLE products_suppliers (
	ps_id INT PRIMARY KEY,
	product_id INT REFERENCES products(product_id),
	supplier_id INT REFERENCES suppliers(supplier_id),
	quantity INT,
	price NUMERIC(6,2)
);

CREATE TABLE customer_status (
	cs_id INT PRIMARY KEY,
	status VARCHAR(10)
);

CREATE TABLE customers (
	customer_id INT PRIMARY KEY,
	name VARCHAR(50),
	address_id INT REFERENCES address(address_id),
	phone_no NUMERIC(10),
	email VARCHAR(30),
	status INT REFERENCES customer_status(cs_id)
);

CREATE TABLE order_status (
	os_id INT PRIMARY KEY,
	status VARCHAR(10)
);

CREATE TABLE orders (
	order_id INT PRIMARY KEY,
	customer_id INT REFERENCES customers(customer_id),
	purchased_date DATE,
	amount NUMERIC(8,2),
	status INT REFERENCES order_status(os_id)
);

CREATE TABLE order_details (
	od_id INT PRIMARY KEY,
	order_id INT REFERENCES orders(order_id),
	ps_id INT REFERENCES products_suppliers(ps_id),
	quantity INT,
	price INT
);
```



### Mam's Approach

Design the database for a shop which sells products
Points for consideration
  1) One product can be supplied by many suppliers
  2) One supplier can supply many products
  3) All customers details have to present
  4) A customer can buy more than one product in every purchase
  5) Bill for every purchase has to be stored
  6) These are just details of one shop
 
- categories
	- id, name, status
 
- country
	- id, name
 
- state
	- id, name, country_id
 
- City
	- id, name, state_id
 
- area
	- zipcode, name, city_id
 
- address
	- id, door_number, addressline1, zipcode
 
- supplier
	- id, name, contact_person, phone, email, address_id, status
 
- product
	- id, Name, unit_price, quantity, description, image
 
- product_supplier
	- transaction_id, product_id, supplier_id, date_of_supply, quantity,
 
- Customer
	- id, Name, Phone, age, address_id
 
- order
	- order_number, customer_id, Date_of_order, amount, order_status
 
- order_details
	- id, order_number, product_id, quantity, unit_price

``` sql

CREATE DATABASE DemoSolution;
USE DemoSolution;


CREATE TABLE status_master (
	status_id INT PRIMARY KEY,
	status VARCHAR(10)
);


ALTER TABLE status_master
ALTER COLUMN status_id INT NOT NULL;

ALTER TABLE status_master
ADD CONSTRAINT PK_status_id PRIMARY KEY CLUSTERED (status_id);

CREATE TABLE categories (
	category_id INT PRIMARY KEY,
	name VARCHAR(20),
	status INT REFERENCES status_master(status_id)
)

ALTER TABLE categories
ADD CONSTRAINT FK_categories_status_id FOREIGN KEY (status) REFERENCES status_master(status_id);

CREATE TABLE country_master (
	country_id INT PRIMARY KEY,
	name VARCHAR(20)
);

CREATE TABLE state_master (
	state_id INT PRIMARY KEY,
	name VARCHAR(20),
	country_id INT REFERENCES country_master(country_id)
);

exec sp_help state_master;

CREATE TABLE city_master (
	city_id INT PRIMARY KEY,
	name VARCHAR(20),
	state_id INT REFERENCES state_master(state_id)
);

exec sp_help city_master;

CREATE TABLE address (
	address_id INT PRIMARY KEY,
	line_1 VARCHAR(50),
	line_2 VARCHAR(50),
	city_id INT REFERENCES city_master(city_id),
	pincode NUMERIC(6)
);

CREATE TABLE products (
	product_id INT PRIMARY KEY,
	name VARCHAR(50),
	category_id INT REFERENCES categories(category_id),
	quantity INT,
	price NUMERIC(6,2),
	description VARCHAR(100),
	image IMAGE
);

CREATE TABLE suppliers (
	supplier_id INT PRIMARY KEY,
	name VARCHAR(50),
	contact_person VARCHAR(50),
	address_id INT REFERENCES address(address_id),
	phone_no NUMERIC(10),
	email VARCHAR(30),
	status INT REFERENCES status_master(status_id)
);

CREATE TABLE products_suppliers (
	ps_id INT PRIMARY KEY,
	product_id INT REFERENCES products(product_id),
	supplier_id INT REFERENCES suppliers(supplier_id),
	quantity INT,
	date_of_supply DATE
);


CREATE TABLE customers (
	customer_id INT PRIMARY KEY,
	name VARCHAR(50),
	age INT,
	address_id INT REFERENCES address(address_id),
	phone_no NUMERIC(10),
	email VARCHAR(30),
	status INT REFERENCES status_master(status_id)
);


CREATE TABLE orders (
	order_id INT PRIMARY KEY,
	customer_id INT REFERENCES customers(customer_id),
	purchased_date DATE,
	amount NUMERIC(8,2),
	order_status INT REFERENCES status_master(status_id)
);

CREATE TABLE order_details (
	od_id INT PRIMARY KEY,
	order_id INT REFERENCES orders(order_id),
	product_id INT REFERENCES products(product_id),
	quantity INT,
	selling_price INT
);
```
