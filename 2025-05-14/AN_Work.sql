CREATE TABLE rental_log (
    log_id SERIAL PRIMARY KEY,
    rental_time TIMESTAMP,
    customer_id INT,
    film_id INT,
    amount NUMERIC,
    logged_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE PROCEDURE proc_add_rental_log(c_id INT, f_id INT, amt NUMERIC)
AS $$
BEGIN
	INSERT INTO rental_log(rental_time,customer_id,film_id, amount) 
	VALUES(CURRENT_TIMESTAMP,c_id,f_id,amt);
EXCEPTION WHEN others THEN
	RAISE NOTICE 'Error Occurred : %',SQLERRM;
END;
$$
LANGUAGE PLPGSQL;

CALL proc_add_rental_log(2,2,20);

SELECT * FROM rental_log;

CREATE FUNCTION fn_rental_log_log()
RETURNS TRIGGER AS 
$$
BEGIN
	RAISE NOTICE '% on %',TG_OP,NEW.log_id;

	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_rental_log_Before
BEFORE INSERT OR UPDATE OR DELETE
ON rental_log
FOR EACH ROW
EXECUTE PROCEDURE fn_rental_log_log();



CREATE FUNCTION fn_rental_log()
RETURNS TRIGGER AS 
$$
DECLARE film INT;
BEGIN
	SELECT film_id INTO film FROM INVENTORY where inventory_id = NEW.inventory_id;
	CALL proc_add_rental_log(NEW.customer_id,film,NEW.amount);
	RAISE NOTICE '% on %',TG_OP,NEW.log_id;

	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_rental_log_Before
BEFORE INSERT OR UPDATE OR DELETE
ON rental_log
FOR EACH ROW
EXECUTE PROCEDURE fn_rental_log_log();