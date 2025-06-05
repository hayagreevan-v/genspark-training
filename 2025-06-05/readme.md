# 2025-06-05    Day - 24 File Handling in Controller, SignalR - WebSocket

## Topics

- File Upload and Download to and from server (refer FileHandleController.cs & Notify - DocumentController.cs)

- SignalR - Websocket

- CORS (Cross Origin Resource Sharing)

- Custom Validation (refer NameValidation.cs)

- SignalR Frontend Setup (refer Frontend/*.html)



## Short Notes

File get/post  
Custom validation  

Notification - SignalR
- Web Socket
- Pub-sub design pattern
- Observer pattern

CORS Setup


#### While having FileHandling and WebSocket connection simultaneously, **Store the file outside the project directories,** 
#### or else server (web socket) will restart for each file upload as there is a change project directory.

------
Standard Nuget Packages  

``` sh
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.4
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.5
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.5
```

## Links
- SignalR - https://medium.com/@BasuraRatnayake/c-tutorial-signalr-b7a7b76901be
- SignalR - https://learn.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-9.0&tabs=visual-studio-code
- SignalR CDN - https://cdnjs.com/libraries/microsoft-signalr
- SignalR Nuget Package - https://www.nuget.org/packages/Microsoft.AspNetCore.SignalR#readme-body-tab
-Download file - https://learn.microsoft.com/en-us/answers/questions/1033258/download-file-in-c-net-core
- Upload file - https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-9.0#upload-small-files-with-buffered-model-binding-to-physical-storage

