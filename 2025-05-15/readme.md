# 2025-05-15   Day - 9 PostgreSQL: pgcrypto, Roles and Permissions

# Topics
**PostgreSQL**
- pgcrypto
- Encryption
- Descrytion
- Authentication
- Authorization
- Roles and Permissions
- GRANT
- REVOKE

## Short Notes

Refer "C:\Softwares\PostgreSQL\17\data\pg_hba.conf" for Client Authentication

Permission Cmds:

``` sql
grant connect on database dbSample to readonly; -- readonly is a role

grant select to all tables in schema public to readonly

revoke connect on database dbSample from readonly;
```


## Links 
- https://www.postgresql.org/docs/current/pgcrypto.html
- https://www.postgresql.org/docs/current/client-authentication.html
- https://www.postgresql.org/docs/current/auth-methods.html
- https://www.postgresql.org/docs/current/sql-grant.html
- https://www.postgresql.org/docs/current/functions-string.html
