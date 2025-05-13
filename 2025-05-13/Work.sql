create table audit_log
(audit_id serial primary key,
table_name text,
field_name text,
old_value text,
new_value text,
updated_date Timestamp default current_Timestamp)

create or replace function Update_Audit_log()
returns trigger 
as $$
begin
	Insert into audit_log(table_name,field_name,old_value,new_value) 
	values('customer','email',OLD.email,NEW.email);
	return new;
end;
$$ language plpgsql


CREATE OR REPLACE FUNCTION Update_AUdit_log()
RETURNS TRIGGER AS 
$$
DECLARE 
	col_name TEXT := TG_ARGV[0];
	table_name TEXT := TG_ARGV[1];
	old_value TEXT;
	new_value TEXT;
BEGIN
	EXECUTE FORMAT('SELECT $1.%I::TEXT',col_name) into old_value USING OLD;
	EXECUTE FORMAT('SELECT $1.%I::TEXT',col_name) into NEW_value USING NEW;
	IF old_value is distinct from new_value then
		Insert into audit_log(table_name,field_name,old_value,new_value) 
		values(table_name,col_name,old_value,new_value);
	END IF;

	RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;


create or replace trigger trg_log_customer_email_Change
before update
on customer
for each row
execute function Update_Audit_log('email','customer');

drop trigger trg_log_customer_email_Change on customer
drop table audit_log;
select * from customer order by customer_id

select * from audit_log;
update customer set email = 'mary.smith@sakilacustomer.com' where customer_id = 1


do $$
declare
    rental_record record;
    rental_cursor cursor for
        select r.rental_id, c.first_name, c.last_name, r.rental_date
        from rental r
        join customer c on r.customer_id = c.customer_id
        order by r.rental_id;
begin
    open rental_cursor;

    loop
        fetch rental_cursor into rental_record;
        exit when not found;

        raise notice 'rental id: %, customer: % %, date: %',
                     rental_record.rental_id,
                     rental_record.first_name,
                     rental_record.last_name,
                     rental_record.rental_date;
    end loop;

    close rental_cursor;
end;
$$;




create table rental_tax_log (
    rental_id int,
    customer_name text,
    rental_date timestamp,
    amount numeric,
    tax numeric
);

select * from rental_tax_log;
TRUNCATE rental_tax_log;


DO $$
DECLARE 
	rec RECORD;
	cur CURSOR FOR 
		select r.rental_id, 
               c.first_name || ' ' || c.last_name as customer_name,
               r.rental_date,
               p.amount
        from rental r
        join payment p on r.rental_id = p.rental_id
        join customer c on r.customer_id = c.customer_id;

BEGIN
	OPEN cur;

	LOOP
		FETCH cur INTO rec;
		EXIT WHEN NOT FOUND;
		INSERT INTO rental_tax_log (rental_id, customer_name, rental_date, amount, tax)
        VALUES (
            rec.rental_id,
            rec.customer_name,
            rec.rental_date,
            rec.amount,
            rec.amount * 0.10
        );
	END LOOP;
	CLOSE cur;
END;
$$;