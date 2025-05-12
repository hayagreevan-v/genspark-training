# 2025-05-12   Day - 6 PostgreSQL: Transactions & Concurrency Control

# Topics
**PostgreSQL**
- Transaction
- Concurrency Control
- Errors caused due to lack of Concurrency Control
    - Dirty Read
    - Non Repeatable read
    - Phantom Read
    - Lost Update
- Concurrency Handling
    - MVCC (Multi-Versioning Concurrency Control)
    - Isolation Levels
        - Read Uncommitted
        - Read Committed
        - Repeatable Read
        - Serializable


## Short Notes
**Solutions to Avoid Lost Updates:**

**1. Pessimistic Locking (Explicit Locks)**

    Lock the record when someone reads it, so no one else can read or write until the lock is released.
    Example: SELECT ... FOR UPDATE in SQL.
    Prevents concurrency but can reduce performance due to blocking.

**2. Optimistic Locking (Versioning)**

    Common and scalable solution.
    Each record has a version number or timestamp.
    When updating, you check that the version hasn’t changed since you read it.
    If it changed, you reject the update (user must retry).

    Example:
    UPDATE products
    SET price = 100, version = version + 1
    WHERE id = 1 AND version = 3; --3

**3. Serializable Isolation Level**
    In database transactions, using the highest isolation level (SERIALIZABLE) can prevent lost updates.
    But it's heavier and can cause performance issues (due to more locks or transaction retries).

**Which Solution is Best?**
- For web apps and APIs: Optimistic locking is often the best balance (fast reads + safe writes).
- For critical financial systems: Pessimistic locking may be safer.

## Links :
- https://www.geeksforgeeks.org/what-is-multi-version-concurrency-control-mvcc-in-dbms/
- https://www.geeksforgeeks.org/transaction-isolation-levels-dbms/