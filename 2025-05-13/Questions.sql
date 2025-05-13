-- Cursors 
-- Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.

CREATE TABLE customer_rental_count(customer_id int, rentals_count int);
SELECT * FROM customer_rental_count;

DO $$
DECLARE 
	rec RECORD;
	count INT := 0;
	cur CURSOR FOR SELECT DISTINCT customer_id FROM customer;
BEGIN 
	OPEN cur;
	TRUNCATE customer_rental_count;
	LOOP
		FETCH NEXT FROM cur INTO rec;
		EXIT WHEN NOT FOUND;
		SELECT count(*) into count FROM rental WHERE customer_id = rec.customer_id;
		INSERT INTO customer_rental_count VALUES(rec.customer_id, count);
	END LOOP;
END;
$$;


-- Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.

SELECT fc.film_id FROM film_category fc JOIN category c
WHERE c.name = 'Comedy'
ON c.category_id = fc.category_id;

DO $$
DECLARE
	count INT := 0;
	film_name VARCHAR(100);
	rec RECORD;
	cur CURSOR FOR SELECT fc.film_id FROM film_category fc JOIN category c
		ON c.category_id = fc.category_id
		WHERE c.name = 'Comedy';
BEGIN
	OPEN cur;
	LOOP
		FETCH NEXT FROM cur INTO rec;
		SELECT COUNT(*) INTO count FROM rental r 
		JOIN inventory i ON i.inventory_id = r.inventory_id
		JOIN film f on f.film_id = i.film_id 
		WHERE f.film_id = rec.film_id;
		
		SELECT title INTO film_name FROM film WHERE film_id = rec.film_id;
		
		IF count > 10 THEN
			RAISE NOTICE '% rented % times',film_name, count;
		END IF;
	END LOOP;

	CLOSE cur;
END;
$$;

-- Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.

CREATE TABLE store_films_count (store_id INT, film_count INT);
SELECT * FROM store_films_count;

DO $$
DECLARE
	count INT :=0;
	rec RECORD;
	cur CURSOR FOR SELECT DISTINCT store_id FROM store;
BEGIN
	OPEN cur;

	LOOP
		FETCH NEXT FROM cur INTO rec;
		EXIT WHEN NOT FOUND;
		SELECT DISTINCT COUNT(film_id) into count FROM inventory WHERE store_id = rec.store_id;
		INSERT INTO store_films_count VALUES (rec.store_id, count);
	END LOOP;
	CLOSE cur;
END;
$$;

-- Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.

CREATE TABLE inactive_customers (customer_id INT, name VARCHAR(100));
SELECT * FROM inactive_customers;

DO $$
DECLARE 
	rec RECORD;
	cur CURSOR FOR SELECT DISTINCT * FROM customer;
	max_date TIMESTAMP;
BEGIN
	OPEN cur;

	LOOP
		FETCH NEXT FROM cur INTO rec;
		EXIT WHEN NOT FOUND;
		SELECT MAX(rental_date) into max_date FROM rental WHERE customer_id = rec.customer_id;
		IF(MAX_DATE < CURRENT_TIMESTAMP - INTERVAL '6 MONTH') THEN
			INSERT INTO inactive_customers VALUES (rec.customer_id, rec.first_name ||' '||rec.last_name);
		END IF;
	END LOOP;
	CLOSE cur;
END;
$$;

-- --------------------------------------------------------------------------
 
-- Transactions 
-- Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.
SELECT * FROM PAYMENT;

BEGIN TRANSACTION;
INSERT INTO CUSTOMER(first_name,last_name,STORE_ID,ADDRESS_ID) VALUES ('Hex','Welcome',1,1);
INSERT INTO RENTAL(rental_date,inventory_id, customer_id,staff_id) VALUES (CURRENT_TIMESTAMP,602,1,1);
INSERT INTO PAYMENT(customer_id, staff_id, rental_id, amount, payment_date)
VALUES (1,1,1,4,CURRENT_TIMESTAMP);
COMMIT;


-- Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.
SELECT * FROM rental;

BEGIN;
UPDATE rental
SET rental_id = 2
WHERE rental_id = 3;
ROLLBACK;
 
-- Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.

SELECT * FROM PAYMENT WHERE payment_id in (17503, 17504);

BEGIN;
UPDATE payment SET amount = 2.99 WHERE payment_id = 17504;
SAVEPOINT paymentSavePoint;
UPDATE payment SET amount = 2.99 WHERE payment_id = 17503;
ROLLBACK TO SAVEPOINT paymentSavePoint;
COMMIT;


 
-- Perform a transaction that transfers inventory from one store to another (delete + insert) safely.
SELECT * FROM INVENTORY;

BEGIN;
DELETE FROM inventory  where INVENTORY_ID = 5;
INSERT INTO inventory(inventory_id,film_id, store_id) VALUES (5,1,2);
COMMIT;

 
-- Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity.

SELECT * FROM CUSTOMER WHERE customer_id =524;
SELECT * FROM Rental WHERE customer_id =524;
SELECT * FROM Payment WHERE customer_id =524;

BEGIN;
DELETE FROM payment WHERE customer_id = 524;
DELETE FROM RENTAL WHERE customer_id = 524;
DELETE FROM CUSTOMER WHERE customer_id = 524;
COMMIT;

-- ----------------------------------------------------------------------------
 
-- Triggers
-- Create a trigger to prevent inserting payments of zero or negative amount.

 CREATE FUNCTION fn_Payment_BeforeInsert ()
RETURNS TRIGGER AS $$
BEGIN
	IF(new.amount <= 0) THEN 
		RAISE EXCEPTION 'Payment Amount should be greater than 0';
	END IF;
	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_Payment_BeforeInsert
BEFORE INSERT ON payment
FOR EACH ROW
EXECUTE PROCEDURE fn_Payment_BeforeInsert();

INSERT INTO PAYMENT(customer_id, staff_id, rental_id, amount,payment_date)
VALUES(133, 1,1,0,CURRENT_TIMESTAMP)

SELECT * FROM payment where amount = 0;

-- Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.


CREATE OR REPLACE FUNCTION fn_Film_BeforeUpdate()
RETURNS TRIGGER AS
$$
	BEGIN
		IF NEW.title IS DISTINCT FROM OLD.title or NEW.rental_rate IS DISTINCT FROM OLD.rental_rate THEN
			NEW.last_update = CURRENT_TIMESTAMP ;
		END IF;
		RETURN NEW;
	END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_Film_BeforeUpdate
BEFORE UPDATE ON Film
FOR EACH ROW
EXECUTE PROCEDURE fn_Film_BeforeUpdate();


SELECT * FROM FILM WHERE FILM_ID = 133;
UPDATE Film SET RENTAL_DURATION = 8 WHERE FILM_ID = 133;

 
-- Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week.
CREATE TABLE rental_log(film_id INT, film_title VARCHAR(100));

CREATE OR REPLACE FUNCTION fn_rentalAfter()
RETURNS TRIGGER AS 
$$
DECLARE count INT := 0;
		id INT;
		film_title VARCHAR(100);
BEGIN

	SELECT F.film_id into id FROM rental r
	JOIN inventory i ON i.inventory_id = r.inventory_id
	JOIN film f on f.film_id = i.film_id
	LIMIT 1;
	
	SELECT COUNT(*) INTO count FROM rental r 
	JOIN inventory i ON i.inventory_id = r.inventory_id
	JOIN film f on f.film_id = i.film_id 
	WHERE f.film_id = id AND
	rental_date > CURRENT_TIMESTAMP -  INTERVAL '6 DAYS';

	IF count>= 3 THEN
		SELECT title into film_title FROM film f WHERE f.film_id = id;
		INSERT INTO rental_log VALUES(id, film_title);
	END IF;

	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_rentalAfter
AFTER INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION fn_rentalAfter();


SELECT * FROM inventory;
SELECT * FROM rental;
SELECT * FROM rental_log;

INSERT INTO rental (rental_date, inventory_id,customer_id,staff_id) VALUES(CURRENT_TIMESTAMP, 1,1,1);
INSERT INTO rental (rental_date, inventory_id,customer_id,staff_id) VALUES(CURRENT_TIMESTAMP, 1,1,1);
INSERT INTO rental (rental_date, inventory_id,customer_id,staff_id) VALUES(CURRENT_TIMESTAMP, 1,1,1);