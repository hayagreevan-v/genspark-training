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

