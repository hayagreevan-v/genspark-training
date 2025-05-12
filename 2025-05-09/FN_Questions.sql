-- SELECT Queries
-- List all films with their length and rental rate, sorted by length descending.
-- Columns: title, length, rental_rate

SELECT film_id, title, length, rental_rate FROM film ORDER BY LENGTH DESC;

-- Find the top 5 customers who have rented the most films.
-- Hint: Use the rental and customer tables.

SELECT r.customer_id, first_name||' '|| last_name as Name, count(*) AS total 
FROM rental r JOIN customer c ON c.customer_id=r.customer_id
GROUP BY r.customer_id, c.first_name, last_name
ORDER BY total DESC
LIMIT 5;


-- Display all films that have never been rented.
-- Hint: Use LEFT JOIN between film and inventory → rental.

SELECT f.film_id, title FROM film f 
LEFT JOIN  inventory i on i.film_id = f.film_id 
LEFT JOIN rental r on r.inventory_id = i.inventory_id
WHERE r.rental_id is null
ORDER BY f.film_id;

-- JOIN Queries
-- List all actors who appeared in the film ‘Academy Dinosaur’.
-- Tables: film, film_actor, actor

SELECT a.actor_id, first_name ||' '|| last_name as name 
FROM film_actor fa JOIN actor a ON a.actor_id = fa.actor_id
WHERE fa.film_id = (SELECT film_id FROM film WHERE title= 'Academy Dinosaur');

-- List each customer along with the total number of rentals they made and the total amount paid.
-- Tables: customer, rental, payment

SELECT c.customer_id, first_name||' '||last_name as Name, COUNT(rental_id) as Total_Rentals ,SUM(amount) as Total_AMount 
FROM customer c JOIN payment p on p.customer_id = c.customer_id
GROUP BY c.customer_id,first_name, last_name
ORDER BY Total_Rentals desc;

-- CTE-Based Queries
-- Using a CTE, show the top 3 rented movies by number of rentals.
-- Columns: title, rental_count

WITH cte_Top3RentalMovies AS(
	SELECT r.rental_id, r.inventory_id, count(*) as Total_Rented 
	FROM payment p JOIN rental r ON p.rental_id = r.rental_id
	GROUP BY r.rental_id, r.inventory_id
	ORDER BY Total_Rented DESC
	LIMIT 3
)
SELECT TITLE, total_rented FROM cte_Top3RentalMovies c 
JOIN inventory i on c.inventory_id = i.inventory_id
JOIN film f on f.film_id = i.film_id 
ORDER BY Total_Rented DESC, TITLE;

-- Find customers who have rented more than the average number of films.
-- Use a CTE to compute the average rentals per customer, then filter.

WITH cte_MoreThanAvgNoOfMovies AS(
	SELECT r.customer_id, count(*) as Total_Rented 
	FROM payment p JOIN rental r ON p.rental_id = r.rental_id
	GROUP BY r.customer_id
	ORDER BY Total_Rented DESC
)
SELECT customer_id, SUM(TOTAL_RENTED) AS RENTALS FROM cte_MoreThanAvgNoOfMovies c 
GROUP BY customer_id
HAVING SUM(total_rented) > (SELECT AVG(TOTAL_RENTED) FROM  cte_MoreThanAvgNoOfMovies C2)
ORDER BY RENTALS;

--  Function Questions
-- Write a function that returns the total number of rentals for a given customer ID.
-- Function: get_total_rentals(customer_id INT)

CREATE FUNCTION fn_TotalRentalOfCustomer (c_id INT)
RETURNS INT
AS
$$
	SELECT COUNT(*) FROM rental r WHERE customer_id = C_id
$$
LANGUAGE SQL;

SELECT * FROM fn_TotalRentalOfCustomer (2);

-- Stored Procedure Questions
-- Write a stored procedure that updates the rental rate of a film by film ID and new rate.
-- Procedure: update_rental_rate(film_id INT, new_rate NUMERIC)

CREATE PROCEDURE proc_UpdateFilmRentalRate (id INT, new_rate DECIMAL)
AS
$$
	UPDATE film
	SET rental_rate = new_rate
	WHERE film_id =id
$$
LANGUAGE SQL

SELECT film_id, rental_rate FROM film ORDER BY film_id;

CALL proc_UpdateFilmRentalRate (1, 2);

-- Write a procedure to list overdue rentals (return date is NULL and rental date older than 7 days).
-- Procedure: get_overdue_rentals() that selects relevant columns.

SELECT * FROM RENTAL;

CREATE OR REPLACE PROCEDURE get_overdue_rentals()
AS
$$
	BEGIN
	CREATE TABLE IF NOT EXISTS outputTable(rental_id INT, rental_date timestamp, customer_id int);
	TRUNCATE TABLE outputTable;
	INSERT INTO outputTable (SELECT rental_id, rental_date, customer_id FROM rental WHERE return_date IS NULL) ;

	RAISE NOTICE 'Over Dues are saved in OutputTable\n';
	END
$$
LANGUAGE PLPGSQL;

CALL get_overdue_rentals();
SELECT * FROM outputTable;




DROP FUNCTION fn_get_overdue_rentals();

CREATE OR REPLACE FUNCTION fn_get_overdue_rentals()
RETURNS TABLE(rental_id INT, rental_date timestamp, customer_id smallint)
AS 
$$
	BEGIN
	RETURN QUERY
	SELECT r.rental_id, r.rental_date, r.customer_id FROM rental r WHERE r.return_date IS NULL;
	END
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_get_overdue_rentals();