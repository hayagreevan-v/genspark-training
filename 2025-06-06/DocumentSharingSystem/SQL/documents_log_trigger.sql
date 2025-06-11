CREATE OR REPLACE FUNCTION fn_documentsLogs()
RETURNS TRIGGER AS 
$$
DECLARE 
	oldval TEXT :=''; 
	newval TEXT:='';
BEGIN
	IF TG_OP = 'INSERT' THEN
	INSERT INTO documents_table_logs("ModifiedDocumentId","ModificationType","OldValue","NewValue","ModifiedByUserId","ModifiedAt")
	VALUES(NEW."Id",TG_OP,null,null,NEW."LastUpdatedByUserId",NEW."LastUpdatedAt");
	
	ELSIF TG_OP = 'UPDATE' THEN
		IF OLD."StoredFileName" IS DISTINCT FROM NEW."StoredFileName" THEN
			oldval := oldval || 'StoredFileName=' || OLD."StoredFileName"|| ';';
			newval := newval || 'StoredFileName=' || new."StoredFileName"|| ';';
		END IF;
		IF OLD."OriginalFileName" IS DISTINCT FROM NEW."OriginalFileName" THEN
			oldval := oldval || 'OriginalFileName=' || OLD."OriginalFileName" || ';';
			newval := newval || 'OriginalFileName=' || new."OriginalFileName" || ';';
		END IF;
		IF OLD."IsDeleted" IS DISTINCT FROM NEW."IsDeleted" THEN
			oldval := oldval || 'IsDeleted=' || OLD."IsDeleted" || ';';
			newval := newval || 'IsDeleted=' || new."IsDeleted" || ';';
		END IF;
		
		INSERT INTO documents_table_logs("ModifiedDocumentId","ModificationType","OldValue","NewValue","ModifiedByUserId","ModifiedAt")
		VALUES(NEW."Id",TG_OP,oldval,newval,NEW."LastUpdatedByUserId",NEW."LastUpdatedAt");
	END IF;
RETURN NEW;
END;
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER tr_documentsLogs
AFTER INSERT OR UPDATE
ON documents 
FOR EACH ROW
EXECUTE FUNCTION fn_documentsLogs();

INSERT INTO users VALUES
('922cbc0b-cee7-4f40-90fd-61f3fcfe9a02','Hex','Admin','hex@mail.com','password','active','922cbc0b-cee7-4f40-90fd-61f3fcfe9a02',CURRENT_TIMESTAMP,'922cbc0b-cee7-4f40-90fd-61f3fcfe9a02');

INSERT INTO documents
values('7c89adfb-9a8b-4649-8645-a8d045dd5702','1','1','922cbc0b-cee7-4f40-90fd-61f3fcfe9a02',CURRENT_TIMESTAMP,
'922cbc0b-cee7-4f40-90fd-61f3fcfe9a02',CURRENT_TIMESTAMP,'ACTIVE');

UPDATE documents
SET "StoredFileName" = '2', "OriginalFileName"='2';

SELECT * FROM documents;
SELECT * FROM documents_table_logs;

