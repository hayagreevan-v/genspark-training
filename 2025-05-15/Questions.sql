CREATE EXTENSION IF NOT EXISTS pgcrypto;

--  1. Create a stored procedure to encrypt a given text
-- Task: Write a stored procedure sp_encrypt_text that takes a plain text input (e.g., email or mobile number) 
-- and returns an encrypted version using PostgreSQL's pgcrypto extension.

--  Use pgp_sym_encrypt(text, key) from pgcrypto.

CREATE OR REPLACE FUNCTION fn_encrypt_text(data text)
RETURNS BYTEA AS
$$
	BEGIN
		return pgp_sym_encrypt(data, 'encrypt');
	END;
$$
LANGUAGE PLPGSQL;


CREATE OR REPLACE PROCEDURE proc_encrypt_text(data text, OUT enc_text text)
AS
$$
	BEGIN
		enc_text:= pgp_sym_encrypt(data, 'encrypt');
	END;
$$
LANGUAGE PLPGSQL;



SELECT fn_encrypt_text::bytea FROM fn_encrypt_text('Hello World');

SELECT pgp_sym_decrypt(fn_encrypt_text::bytea,'encrypt') FROM fn_encrypt_text('Hello World');

DO $$
DECLARE output TEXT;
BEGIN
	CALL proc_encrypt_text('Hello World!',output);
	RAISE NOTICE '%',OUTPUT;
END;
$$;

-- 2. Create a stored procedure to compare two encrypted texts
-- Task: Write a procedure sp_compare_encrypted that takes two encrypted values and checks if they decrypt to the same plain text.


CREATE OR REPLACE PROCEDURE proc_compare_encrypted(e_txt1 bytea, e_txt2 bytea)
AS $$
BEGIN
	IF pgp_sym_decrypt(e_txt1, 'encrypt') = pgp_sym_decrypt(e_txt2, 'encrypt') THEN
		RAISE NOTICE 'Both are same';
	ELSE
		RAISE NOTICE 'Both are not same';
	END IF;
EXCEPTION WHEN OTHERS THEN
	RAISE NOTICE 'Error : %',SQLERRM;
END;
$$
LANGUAGE PLPGSQL;

CALL proc_compare_encrypted(fn_encrypt_text('Hello'),fn_encrypt_text('Hello'));

--  3. Create a stored procedure to partially mask a given text
-- Task: Write a procedure sp_mask_text that:

-- Shows only the first 2 and last 2 characters of the input string

-- Masks the rest with *

-- E.g., input: 'john.doe@example.com' → output: 'jo***************om'

SELECT SUBSTRING('john.doe@example.com' FOR 2) || REPEAT('*',LENGTH('john.doe@example.com')-4) || RIGHT('john.doe@example.com',2);
SELECT SUBSTRING('john.doe.com' FOR 2) || REPEAT('*',LENGTH('john.doe.com')-4) || RIGHT('john.doe.com',2);

CREATE OR REPLACE PROCEDURE proc_mask_text(IN txt TEXT,OUT masked_text TEXT)
AS $$
BEGIN
IF LENGTH(txt)>4 THEN
	SELECT SUBSTRING(txt FOR 2) || REPEAT('*',LENGTH(txt)-4) || RIGHT(txt,2) INTO masked_text;
ELSE
	masked_text := txt;
END IF;
END;
$$
LANGUAGE PLPGSQL;

DO $$
DECLARE masked_text TEXT;
BEGIN
	CALL proc_mask_text('john.doe@example.com',masked_text);
	RAISE NOTICE '%',masked_text;
END;
$$;

-- 4. Create a procedure to insert into customer with encrypted email and masked name
-- Task:

-- Call sp_encrypt_text for email

-- Call sp_mask_text for first_name

-- Insert masked and encrypted values into the customer table

-- Use any valid address_id and store_id to satisfy FK constraints.

CREATE TABLE customer (customer_id SERIAL PRIMARY KEY, name TEXT, email BYTEA, address_id INT, store_id INT);


CREATE OR REPLACE PROCEDURE proc_InsertCustomer ( name VARCHAR(100), email VARCHAR(100),address_id INT,store_id INT)
AS $$
DECLARE masked_name TEXT;
BEGIN
	CALL proc_mask_text(name::TEXT,masked_name);
	INSERT INTO customer(name,email,address_id,store_id) VALUES(masked_name,fn_encrypt_text(email::TEXT),address_id,store_id);
END;
$$
LANGUAGE PLPGSQL;

CALL proc_InsertCustomer('Hayagreevan','hv@gmail.com',1,1);
SELECT * FROM customer;

-- 5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
-- Task:
-- Write sp_read_customer_masked() that:

-- Loops through all rows

-- Decrypts email

-- Displays customer_id, masked first name, and decrypted email

CREATE OR REPLACE FUNCTION fn_read_Customer()
RETURNS TABLE(customer_id INT, name TEXT, email TEXT)
AS 
$$
BEGIN
	return QUERY SELECT c.customer_id, c.name, pgp_sym_decrypt(c.email,'encrypt') as email FROM customer c;
END;
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_read_Customer();


CREATE OR REPLACE PROCEDURE proc_read_Customer()
AS 
$$
DECLARE 
	rec RECORD;
	cur CURSOR FOR SELECT * FROM customer;
BEGIN
	OPEN cur;
	LOOP
		FETCH NEXT FROM cur INTO rec;
		EXIT WHEN NOT FOUND;
		RAISE NOTICE 'ID : %, Name : %, Email : %',rec.customer_id, rec.name, pgp_sym_decrypt(rec.email,'encrypt');
	END LOOP;
END;
$$
LANGUAGE PLPGSQL;

CALL proc_read_Customer();
	