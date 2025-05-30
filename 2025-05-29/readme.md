# 2025-05-29    Day - 19 Transaction Control, MVC, Repository with DbContext

## Topics

- DbContext

- Execution and Storage of Result from Sql query

- Execution of Stored Procedure using DbContext

- Transaction Control with DbContext

- Mappers

- JSON Data Handling in AddController

- Eager loading while retrieving the objects from DB

## Short Notes

**For parsing JSON** (Two ways)   
- Adding controller configuration
- Newtonsoft Json Package

**TO call stored procedure from DB** (Two ways)
- dbContext
	- create dbset for fn return table type and add it to dbcontext and call fn
	- `clinicContext.Set<T>().FromSqlInterpoled("sql").ToListAsync();`
- ADO .Net



**Eagel loading to get respective class objects**
``` c#
var transaction = await _bankContext.Transactions
                                            .Include(t => t.FromUser)
                                            .Include(t => t.ToUser)
                                            .FirstOrDefaultAsync(t => t.Id == id);
```

Service should not contain Context Injection
All Context Usage (Crud Operations) should be done in Repository

## Link
- https://github.com/gayat19/PresidioMay25/tree/main/Day18/FirstAPI