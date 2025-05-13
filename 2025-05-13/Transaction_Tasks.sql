SELECT * FROM tbl_bank_accounts;

-- 13 May 2025 - Task

-- 1. Try two concurrent updates to same row → see lock in action.
-- Transaction 1
BEGIN TRANSACTION;
UPDATE tbl_bank_accounts
SET balance = balance - 100
WHERE account_id=1;
COMMIT;

-- Transaction 2
BEGIN TRANSACTION;
UPDATE tbl_bank_accounts
SET balance = balance - 100
WHERE account_id=1;


-- 2. Write a query using SELECT...FOR UPDATE and check how it locks row.

-- Transaction 1
BEGIN;
LOCK TABLE tbl_bank_accounts
IN ROW SHARE MODE;
SELECT * FROM tbl_bank_accounts FOR UPDATE;
COMMIT;

-- Transaction 2
BEGIN TRANSACTION;
UPDATE tbl_bank_accounts
SET balance = balance - 100
WHERE account_id=1;
ROLLBACK;

-- 3. Intentionally create a deadlock and observe PostgreSQL cancel one transaction.

-- Transaction 1
BEGIN TRANSACTION;
UPDATE tbl_bank_accounts
SET balance = balance - 100
WHERE account_id=1;

UPDATE tbl_bank_accounts
SET balance = balance + 100
WHERE account_id=2;
ROLLBACK;

-- Transaction 2
BEGIN TRANSACTION;
UPDATE tbl_bank_accounts
SET balance = balance + 100
WHERE account_id=2;

UPDATE tbl_bank_accounts
SET balance = balance - 100
WHERE account_id=1;

/*
Transaction 2 Aborted!
ERROR:  deadlock detected
Process 9788 waits for ShareLock on transaction 1135; blocked by process 1504.
Process 1504 waits for ShareLock on transaction 1136; blocked by process 9788. 

SQL state: 40P01
Detail: Process 9788 waits for ShareLock on transaction 1135; blocked by process 1504.
Process 1504 waits for ShareLock on transaction 1136; blocked by process 9788.
Hint: See server log for query details.
Context: while updating tuple (0,1) in relation "tbl_bank_accounts"
*/

-- 4. Use pg_locks query to monitor active locks.
SELECT * FROM pg_locks;

-- 5. Explore about Lock Modes.
