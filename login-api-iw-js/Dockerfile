# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY *.sln .
COPY login-api-iw-js/*.csproj ./login-api-iw-js/
RUN dotnet restore

COPY login-api-iw-js/. ./login-api-iw-js/
WORKDIR /app/login-api-iw-js
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/login-api-iw-js/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "login-api-iw-js.dll"]
