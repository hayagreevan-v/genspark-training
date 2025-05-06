
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