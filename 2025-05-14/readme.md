# 2025-05-14   Day - 8 PostgreSQL: Replication, Backup, Standby Server

# Topics
**PostgreSQL**
- Replication of Server
- Creation of new Postgres Server
- Setting a server as basebackup (Physical Backup)
- Backup & Restore Database

## Short Notes

Creating a Replication Role

``` sh
psql -p 5433 -d postgres -c "CREATE ROLE replicator WITH REPLICATION LOGIN PASSWORD 'repl_pass';"
```

**Explanation:**

psql → PostgreSQL command-line tool.

-p 5433 → Connects to PostgreSQL running on port 5433 (default is 5432).

-d postgres → Connects to the postgres database.

-c "CREATE ROLE replicator WITH REPLICATION LOGIN PASSWORD 'repl_pass';"

Creates a role named replicator.

Grants it REPLICATION privileges (needed for streaming replication).

Allows LOGIN (so it can authenticate).

Sets the password to 'repl_pass'.

✅ Purpose: This creates a user (replicator) that can be used for streaming replication.

----------------------------------

Taking a Base Backup for Replication

``` sh
pg_basebackup -D d:\pg\sec -Fp -Xs -P -R -h 127.0.0.1 -U replicator -p 5433
```
**Explanation:**

pg_basebackup → A tool to take a physical backup of a PostgreSQL database.

-D d:\pg\sec → Stores the backup in the directory d:\pg\sec.

-Fp → Uses plain format (instead of tar).

-Xs → Includes WAL files for consistency.

-P → Shows progress during the backup.

-R → Creates a recovery configuration (standby.signal file) for replication.

-h 127.0.0.1 → Connects to the local PostgreSQL server.

-U replicator → Uses the replicator role for authentication.

-p 5433 → Connects to PostgreSQL running on port 5433.

✅ Purpose: This command creates a standby server by taking a backup of the primary database and preparing it for replication.




## Links 
- https://www.postgresql.org/docs/current/backup.html
- https://www.postgresql.org/docs/current/runtime-config-replication.html