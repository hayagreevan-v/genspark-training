# 2025-05-27    Day - 17 : Entity Framework

## Topics

- Entity FrameWork

- Nuget Packages

- DbContext

- Attributes usage in DbContext

- Database Connection

- Configuration Setup

- Connecting Database to Program using DbContext

## Short Notes

Package Installation Commands
``` sh 
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.4
dotnet tool install --global dotnet-ef --version 9.0.5
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.5
```

pg Connection string : `User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;`


In DbContext :

- In class - proprty containing id will be considered as Primary Key
- Attributes like [Key],[ForeignKey("")] can be used for Key identification
- class Attributes are not considered for schema
- while declaring DbSet properties for table declaraction follow smaller cases (SQL is case insensitive)


------------------------------------------------

### Question

You have been hired to build the backend for a Twitter-like application using .NET 8, Entity Framework Core (EF Core) with PostgreSQL as the database.   
The application supports basic social media features such as user registration, posting tweets, liking tweets, using hashtags, and following users.  
Your goal is to model and implement the database layer only using EF Core with code-first approach, focusing on data design, relationships,  migrations, and PostgreSQL-specific features.

--------------------------

Users :   
- UserId, Name,Age, Email, ContactNo, Status  
- Navigation Properties : 
    - Tweets, Following, FollowedBy

Tweets :  
- TweetId, PostedByUserId, Content, Status  
- Navigation Properties : 
    - Likes, HashTags

Likes :  
- LikeId, TweetId, LikedByUserId  

Follows :  
- FollowId, FollowerUserId, FollowedUserId  

HashTags :  
- HashTagId, HashTagContent  

Hashtag_Tweets :  
- HashTageTweetId, HashTagId, TweetId  

## Links 

- https://www.nuget.org/packages/dotnet-ef
- https://www.nuget.org/packages/microsoft.entityframeworkcore.design/
- https://www.nuget.org/packages/npgsql.entityframeworkcore.postgresql
- https://community.render.com/t/hot-to-connect-to-pgsql-from-asp-net-core-app/13632