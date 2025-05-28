# 2025-05-28    Day - 18    FluentAPI, Repository Pattern, Dependency Injection

## Topics

- FluentAPI

- One-Many Relationship Configuration using FluentAPI

- Primary Key, Foreign Key declaration using FluentAPI

- Dependency Injection
    - Singleton
    - Scoped
    - Transient

- async - await

- IRepository and Abstract class Repository [Generic] implementation with DbContext

- DTO (Data Transfer Objects)

- Data Manipulation using DbContext object

- Repository - Service Implementation

## Short Notes


**FluentAPI**

Created DbContext 
- Can perform add, remove, update without specifying DbSet property(db table object) based on class of the object
- But for getting the records we should specify DbSet property.

from DbContext. When we call _clinicContext.Add(item):
* EF Core examines the object’s runtime type (e.g., Patient, Doctor, etc.).
* It figures out which DbSet<T> it belongs to by using metadata from your ClinicContext, like this:

``` c#
public DbSet<Patient> patients { get; set; }
```

So even though we don’t specify DbSet<Patient> manually, EF Core uses reflection to match the type of item to the correct DbSet behind the scenes.
✅ That's why Add works in your generic base class.


Why are Get and GetAll abstract?
- Because you can't safely query by key or access DbSet<T> without knowing the type and key.

Why are Get and GetAll abstract and implemented in the derived class?
Because EF Core doesn't let you query by key or get all items generically unless:
1. You know which DbSet<T> to query.
2. You know how to access Id or the primary key — but in generic T, there's no guarantee that an Id property exists.


**OnModelCreating — Fluent API Configuration**
This is where you define complex mappings, keys, relationships, and constraints that can't easily be done using attributes.



Repository - is only for crud


**Dependency Injection**

Dependency Injection (DI) is a design pattern used to:
- Automatically provide instances of classes (dependencies) that other classes need.
- Avoid new keyword all over your code.
- Make our app easier to test, maintain, and extend.

``` c#
builder.Services.AddTransient<IRepository<int, Doctor>, DoctorRepository>();
```
This line tells the framework:

"Whenever a class asks for IRepository<int, Doctor>, give them a new instance of DoctorRepository."


In ASP.NET Core, builder.Services is used to register services with the built-in DI container.
AddSingleTon - object create once when server is started
``` c#
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
```

AddScoped - object created for every request
``` c#
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```
AddTransient - object created for every injection
``` c#
builder.Services.AddTransient<IEmailService, EmailService>();
```

**Best Practices for DI**
- Use Scoped for most services in ASP.NET Core web APIs (especially with DbContext).

- Use Transient when services are stateless and lightweight.

- Use Singleton only if the service is thread-safe and doesn't rely on per-request data.

| Lifetime    | Scope                             | Pros                                          | Cons                                             | Use Case                          |
| ----------- | --------------------------------- | --------------------------------------------- | ------------------------------------------------ | --------------------------------- |
| `Transient` | New instance **every time**       | Stateless, clean, no side effects             | Can be inefficient if service is heavy           | Lightweight services              |
| `Scoped`    | One instance **per request**      | Consistent within a request, safe for EF Core | Can’t use in background jobs                     | Business logic, EF DbContext      |
| `Singleton` | One instance **for app lifetime** | High performance, good for caching            | Risky with shared state, memory leaks if misused | Logging, caching, config services |

## Links

- https://github.com/gayat19/PresidioMay25/tree/main/Day18/FirstAPI