# 2025-06-04    Day - 23 OAuth using Google, Moq Unit Testing

## Topics

- OAuth - using Google

- Unit Testing

- Moq Unit Testing (Used for Service Testing (Mocking repos))

- Log4net

- Filters

- Custom Exception Filter



## Short Notes

Moq 
- creates an object of mock class which is inherent of class
- Liskov Substitution principle
- Overrides the existing methods for testing
- Injection
- Used for checking dependency


``` sh
dotnet add package Microsoft.Extensions.Logging.Log4Net.AspNetCore --version 8.0.0
dotnet add package Moq --version 4.20.72
```

Log4net config  
Destination : `FirstAPI/bin/Debug/net9.0/main.log`
``` conf
<log4net>
	<root>
		<level value="ALL" />
		<appender-ref ref="RollingFile" />
	</root>

	<appender name="RollingFile" type="log4net.Appender.FileAppender">
		<file value="main.log" />
	
		<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%date [%thread] %level %logger - %message%newline" />
		</layout>
	</appender>
</log4net>

```
## Links
- https://chatgpt.com/share/68409c7d-3a3c-800a-bd8e-7d603d53e264
- https://console.cloud.google.com/auth/overview?inv=1&invt=AbzNMQ&project=fir-7d9ee
- https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-9.0
- https://docs.nunit.org/articles/nunit/writing-tests/constraints/Constraints.html
- https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-9.0
- https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-9.0
- https://jwt.io/
- https://github.com/devlooped/moq
- https://github.com/gayat19/PresidioMay25/blob/main/Day18/FirstAPI/