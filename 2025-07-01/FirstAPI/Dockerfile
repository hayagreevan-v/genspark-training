# Base image used for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Install openssl to generate certificates
RUN apt-get update && apt-get install -y openssl

# Generate self-signed certificate
RUN mkdir /https && \
    openssl req -x509 -nodes -days 365 \
      -newkey rsa:2048 \
      -keyout /https/aspnetcore.key \
      -out /https/aspnetcore.crt \
      -subj "/CN=localhost" && \
    openssl pkcs12 -export \
      -out /https/aspnetcore.pfx \
      -inkey /https/aspnetcore.key \
      -in /https/aspnetcore.crt \
      -passout pass:password

EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /App




# Copy everything
COPY . /App
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:b4bea3a52a0a77317fa93c5bbdb076623f81e3e2f201078d89914da71318b5d8
FROM base AS final
WORKDIR /App
COPY --from=build /App/out .
COPY .env /App/.env
# Environment variable for Development to enable Swagger
ENV ASPNETCORE_ENVIRONMENT=Development

# Configure Kestrel URLs
ENV ASPNETCORE_URLS="http://+:8080;https://+:8081"

ENTRYPOINT ["dotnet", "FirstAPI.dll"]