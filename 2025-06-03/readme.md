# 2025-06-03    Day - 22 Authorization, Unit Testing

## Topics

- Authorization

    - Role based Authorization

    - Policy based Authorization

- JWT data parsing

- Custom policy for Authorization - AuthorizationRequirement, AuthorizationHandler

- Unit Testing



## Short Notes

Authentication - validating credentials  
Authorisation - permission to access  

Unit Testing (3 steps)
- Arrange
- Action
- Asset

Commands to create Unit Test :

``` sh
dotnet new nunit -n FirstAPI.Test

cd FirstAPI.Test

dotnet add reference ../FirstAPI.csproj
```
## Links
- https://chatgpt.com/share/683f2030-b154-800a-a6d7-8d9f787dd104
- https://www.red-gate.com/simple-talk/development/dotnet-development/policy-based-authorization-in-asp-net-core-a-deep-dive/
- https://jwt.io/
- https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-9.0
- https://github.com/gayat19/PresidioMay25/blob/main/Day18/FirstAPI/