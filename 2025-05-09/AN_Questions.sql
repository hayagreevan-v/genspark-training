-- -----------------------------------------------------
-- Cursor-Based Questions (5)
-- Write a cursor that loops through all films and prints titles longer than 120 minutes.

CREATE OR REPLACE FUNCTION filterFilm ()
RETURNS TABLE(title VARCHAR(100))
AS
$$
DECLARE film_cursor CURSOR FOR SELECT * FROM film; rec RECORD;
BEGIN
	OPEN film_cursor;
	CREATE TABLE IF NOT EXISTS temp_table(title VARCHAR(100));
	TRUNCATE TABLE temp_table;
	
	LOOP
		FETCH film_cursor INTO rec;
		EXIT WHEN NOT FOUND;
		IF rec.length>120 THEN
			INSERT INTO temp_table VALUES(REC.TITLE);
		END IF;
	END LOOP;
	RETURN QUERY SELECT * FROM temp_table;
	END;

$$
LANGUAGE PLPGSQL;

SELECT * FROM filterfilm();

SELECT * FROM film WHERE length >120;


-- Create a cursor that iterates through all customers and counts how many rentals each made.

DROP TABLE customer_rentals_count;
CREATE OR REPLACE FUNCTION fn_CustomerRentalsCount ()
RETURNS TABLE(ID INT, C_COUNT INT)
AS $$
	DECLARE
		customer_cursor CURSOR FOR SELECT distinct customer_id FROM customer;
		rec RECORD;
		count INT := 0;
	BEGIN
		CREATE TABLE IF NOT EXISTS customer_rentals_count (C_ID INT PRIMARY KEY, c_COUNT INT);
		TRUNCATE TABLE customer_rentals_count;
		OPEN customer_cursor;
		LOOP
			FETCH NEXT FROM customer_cursor INTO rec;
			EXIT WHEN NOT FOUND; 
			INSERT INTO customer_rentals_count(C_ID,C_COUNT) VALUES(rec.customer_id, (select(count(*)) FROM rental r where r.customer_id = rec.customer_id)); 
		END LOOP;
		RETURN QUERY SELECT c.C_id,c.c_count FROM customer_rentals_count c;
	END;
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_CustomerRentalsCount();


-- Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.

CREATE OR REPLACE FUNCTION fn_UpdateRentalRate()
RETURNS VOID AS
$$
DECLARE film_cursor CURSOR FOR SELECT f.film_id, count(*) FROM film f 
						JOIN inventory i ON i.film_id = f.film_id
						JOIN rental r on i.inventory_id = r.inventory_id 
						GROUP BY f.film_id
						HAVING COUNT(*)< 5;
		rec RECORD;
BEGIN
	OPEN film_cursor;
	
	LOOP
		FETCH NEXT FROM film_cursor INTO rec;
		EXIT WHEN NOT FOUND;
		UPDATE film SET rental_rate = rental_rate+1 where  film_id = rec.film_id;
	END LOOP;
END;
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_UpdateRentalRate();
-- For viewing those records
SELECT FILM_ID, rental_rate from film where film_id in (SELECT f.film_id FROM film f 
JOIN inventory i ON i.film_id = f.film_id
JOIN rental r on i.inventory_id = r.inventory_id 
GROUP BY f.film_id
HAVING COUNT(*)< 5);
--


-- Create a function using a cursor that collects titles of all films from a particular category.

CREATE OR REPLACE FUNCTION fn_CategoryTitles(category INT)
RETURNS TABLE(category_id INT, title VARCHAR(100))
AS $$
DECLARE film_cursor CURSOR FOR SELECT film_id FROM film_category fc WHERE fc.category_id = category;
		rec RECORD;
BEGIN
	CREATE TABLE IF NOT EXISTS categoryTitle(category_id INT, title VARCHAR(100));
	TRUNCATE TABLE categoryTitle;
	OPEN film_cursor;
	LOOP
		FETCH NEXT FROM film_cursor INTO rec;
		EXIT WHEN NOT FOUND;
		INSERT INTO categoryTitle VALUES (category,(SELECT f.TITLE from film f where film_id = rec.film_id));
	END LOOP;
	RETURN QUERY SELECT * FROM categoryTitle;
END;
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_CategoryTitles(2);


-- Loop through all stores and count how many distinct films are available in each store using a cursor.

CREATE FUNCTION fn_StoreFilmCount()
RETURNS TABLE(storeid INT,film_count INT)
AS $$
DECLARE store_cursor CURSOR FOR SELECT DISTINCT store_id FROM store;
		rec RECORD;
		count INT :=0;
BEGIN
	CREATE TABLE IF NOT EXISTS storeFilmCount(storeid INT, film_COUNT INT);
	TRUNCATE TABLE storeFilmCount;
	OPEN store_cursor;
	LOOP
		FETCH NEXT FROM store_cursor INTO rec;
		EXIT WHEN NOT FOUND;
		
		SELECT count(*) INTO count FROM store s LEFT JOIN inventory i on i.store_id = s.store_id
		LEFT JOIN film f on f.film_id = i.film_id WHERE s.store_id = rec.store_id ;
		INSERT INTO storeFilmCount VALUES (rec.store_id,count);
	END LOOP;
	RETURN QUERY SELECT * FROM storeFilmCount;
END;
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_StoreFilmCount();

SELECT s.store_id, count(*) FROM store s LEFT JOIN inventory i on i.store_id = s.store_id
LEFT JOIN film f on f.film_id = i.film_id group by s.store_id ;

--------------------------------------------------------------------------------------------------------------------------------
-- Trigger-Based Questions (5)
-- Write a trigger that logs whenever a new customer is inserted.

CREATE OR REPLACE FUNCTION fn_Customer_AfterInsert()
RETURNS TRIGGER AS $$
BEGIN
	CREATE TABLE IF NOT EXISTS CustomerInsertionLog(id SERIAL, customer_id INT, created_At TIMESTAMP DEFAULT CURRENT_TIMESTAMP);
	INSERT INTO CustomerInsertionLog(customer_id) VALUES(NEW.customer_id);
	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE OR REPLACE TRIGGER fn_Customer_AfterInsert
AFTER INSERT ON Customer
FOR EACH ROW
EXECUTE PROCEDURE fn_Customer_AfterInsert();

INSERT INTO CUSTOMER(first_name,last_name,STORE_ID,ADDRESS_ID) VALUES ('Hex','WORLD',1,1);
SELECT * FROM CustomerInsertionLog;


-- Create a trigger that prevents inserting a payment of amount 0.

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


-- Set up a trigger to automatically set last_update on the film table before update.

CREATE OR REPLACE FUNCTION fn_Film_BeforeUpdate()
RETURNS TRIGGER AS
$$
	BEGIN
		NEW.last_update = CURRENT_TIMESTAMP ;
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


-- Create a trigger to log changes in the inventory table (insert/delete).


CREATE OR REPLACE FUNCTION fn_Inventory_AfterInsertDelete()
RETURNS TRIGGER AS $$
BEGIN
	CREATE TABLE IF NOT EXISTS InventoryChangesLog(id SERIAL, inventory_id INT,modificationType VARCHAR(10), modified_At TIMESTAMP DEFAULT CURRENT_TIMESTAMP);
	IF TG_OP ='INSERT' then
		INSERT INTO InventoryChangesLog(INVENTORY_ID,modificationType) VALUES(NEW.inventory_id, TG_OP);
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO InventoryChangesLog(Inventory_id,modificationType) VALUES(OLD.inventory_id, TG_OP);
	END IF;
	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE OR REPLACE TRIGGER tr_Inventory_AfterInsertDelete
AFTER INSERT OR DELETE ON Inventory
FOR EACH ROW
EXECUTE PROCEDURE fn_Inventory_AfterInsertDelete();


SELECT * FROM INVENTORY;
INSERT INTO INVENTORY(FILM_ID,STORE_ID) VALUES (134,2);
delete from inventory where inventory_id = 4587;
SELECT * FROM InventoryChangesLog;


-- Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.

CREATE FUNCTION fn_Rental_BeforeInsert()
RETURNS TRIGGER AS 
$$
DECLARE owes INT :=0;
BEGIN
	SELECT sum(rental_rate) into owes  from RENTAL r 
	JOIN Inventory i on r.inventory_id = i.inventory_id
	JOIN FILM f ON f.film_id = i.film_id
	WHERE rental_id not in (Select rental_id from payment p where p.rental_id= r.rental_id)
	AND r.customer_id = NEW.customer_id;

	RAISE NOTICE 'Customer owes %', owes;

	if owes > 50 then
		RAISE EXCEPTION 'Customer cant rent any more, He owes more than $50';
	end if;

	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_Rental_BeforeInsert
BEFORE INSERT ON Rental
FOR EACH ROW
EXECUTE PROCEDURE fn_Rental_BeforeInsert();



INSERT INTO RENTAL(rental_date,inventory_id,customer_id,staff_id) 
values(CURRENT_TIMESTAMP, 1, 293,1);

SELECT c.Customer_id, sum(rental_rate) as Owes  from RENTAL r JOIN Customer C on c.customer_id = r.customer_id
JOIN Inventory i on r.inventory_id = i.inventory_id
JOIN FILM f ON f.film_id = i.film_id
WHERE rental_id not in (Select rental_id from payment p where p.rental_id= r.rental_id)
GROUP BY C.customer_id
ORDER BY Owes DESC;


----------------------------------------------------------------------------------------------------------------------------------------------
-- Transaction-Based Questions (5)
-- Write a transaction that inserts a customer and an initial rental in one atomic operation.

SELECT * FROM customer;
SELECT * FROM RENTAL ORDER BY rental_date DESC;

BEGIN TRANSACTION;
INSERT INTO CUSTOMER(first_name,last_name,STORE_ID,ADDRESS_ID) VALUES ('Hex','Welcome',1,1);
INSERT INTO RENTAL(rental_date,inventory_id, customer_id,staff_id) VALUES (CURRENT_TIMESTAMP,602,1,1);
COMMIT;

-- Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.

SELECT * FROM FILM where film_id = 133;
SELECT * FROM INVENTORY where film_id =133;


BEGIN TRANSACTION;
UPDATE film SET TITLE = 'Hex' WHERE FILM_ID = 133;
INSERT INTO inventory(film_id,store_id) VALUES (133,1);
ROLLBACK;

-- Create a transaction that transfers an inventory item from one store to another.
SELECT * FROM INVENTORY;

BEGIN;
UPDATE inventory SET store_id =1 where INVENTORY_ID = 5;
COMMIT;

-- Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.
SELECT * FROM PAYMENT WHERE payment_id in (17503, 17504);

BEGIN;
UPDATE payment SET amount = 2.99 WHERE payment_id = 17504;
SAVEPOINT paymentSavePoint;
UPDATE payment SET amount = 2.99 WHERE payment_id = 17503;
ROLLBACK TO paymentSavePoint;
COMMIT;


-- Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
SELECT * FROM CUSTOMER WHERE customer_id =524;
SELECT * FROM Rental WHERE customer_id =524;
SELECT * FROM Payment WHERE customer_id =524;

BEGIN;
DELETE FROM CUSTOMER WHERE customer_id = 524;
DELETE FROM RENTAL WHERE customer_id = 524;
DELETE FROM payment WHERE customer_id = 524;
COMMIT;