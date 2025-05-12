CREATE TABLE tbl_bank_accounts
(
account_id SERIAL PRIMARY KEY,
account_name VARCHAR(100),
balance NUMERIC(10, 2)
);

INSERT INTO tbl_bank_accounts
(account_name, balance)
VALUES
('Alice', 5000.00),
('Bob', 3000.00);

ABORT;

SELECT * FROM tbl_bank_accounts;

-- Raising Notice
BEGIN TRANSACTION;

DO $$ 
DECLARE
  current_balance NUMERIC :=0;
BEGIN

  SELECT balance INTO current_balance
  FROM tbl_bank_accounts
  WHERE account_name = 'Alice';

  RAISE NOTICE 'Current Balance : %', current_balance;


  IF current_balance IS NULL OR current_balance = 0 THEN
    RAISE EXCEPTION 'Invalid Sender';
  END IF;

  IF current_balance > 4500 THEN
    UPDATE tbl_bank_accounts 
    SET balance = balance - 4500 
    WHERE account_name = 'Alice';

    SAVEPOINT debit_alice;

	BEGIN
      UPDATE tbl_bank_accounts 
      SET balance = balance + 4500 
      WHERE account_name = 'Hex';

      EXCEPTION WHEN no_data_found THEN
        ROLLBACK TO SAVEPOINT debit_alice;
        INSERT INTO tbl_bank_accounts (account_name, balance) 
        VALUES ('Hex', 4500);
      END ;

  ELSE
    RAISE NOTICE 'Insufficient Funds!';
	ROLLBACK;
  END IF;
END;
$$;

COMMIT;