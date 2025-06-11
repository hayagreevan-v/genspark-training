CREATE OR REPLACE FUNCTION fn_usersLogs()
RETURNS TRIGGER AS 
$$
DECLARE 
	oldval TEXT :=''; 
	newval TEXT:='';
BEGIN
	IF TG_OP = 'INSERT' THEN
	INSERT INTO users_table_logs("ModifiedUserId","ModificationType","OldValue","NewValue","ModifiedByUserId","ModifiedAt")
	VALUES(NEW."Id",TG_OP,null,null,NEW."LastUpdatedByUserId",NEW."LastUpdatedAt");
	
	ELSIF TG_OP = 'UPDATE' THEN
		IF OLD."Name" IS DISTINCT FROM NEW."Name" THEN
			oldval := oldval || 'Name=' || OLD."Name"|| ';';
			newval := newval || 'Name=' || new."Name"|| ';';
		END IF;
		IF OLD."Role" IS DISTINCT FROM NEW."Role" THEN
			oldval := oldval || 'Role=' || OLD."Role" || ';';
			newval := newval || 'Role=' || new."Role" || ';';
		END IF;
		IF OLD."Email" IS DISTINCT FROM NEW."Email" THEN
			oldval := oldval || 'Email=' || OLD."Email" || ';';
			newval := newval || 'Email=' || new."Email" || ';';
		END IF;
		IF OLD."Password" IS DISTINCT FROM NEW."Password" THEN
			oldval := oldval || 'Password' || ';';
			newval := newval || 'Password' || ';';
		END IF;
		IF OLD."IsDeleted" IS DISTINCT FROM NEW."IsDeleted" THEN
			oldval := oldval || 'IsDeleted=' || OLD."IsDeleted" || ';';
			newval := newval || 'IsDeleted=' || new."IsDeleted" || ';';
		END IF;
		
		INSERT INTO users_table_logs("ModifiedUserId","ModificationType","OldValue","NewValue","ModifiedByUserId","ModifiedAt")
		VALUES(NEW."Id",TG_OP,oldval,newval,NEW."LastUpdatedByUserId",NEW."LastUpdatedAt");
	END IF;
RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_usersLogs
AFTER INSERT OR UPDATE
ON users 
FOR EACH ROW
EXECUTE FUNCTION fn_usersLogs();



INSERT INTO users VALUES
('922cbc0b-cee7-4f40-90fd-61f3fcfe9a02','Hex','Admin','hex@mail.com','password','active','922cbc0b-cee7-4f40-90fd-61f3fcfe9a02',CURRENT_TIMESTAMP,'922cbc0b-cee7-4f40-90fd-61f3fcfe9a02');

SELECT * FROM USERS;
SELECT * FROM USERS_table_logs;
