# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar archivos del proyecto y restaurar dependencias
COPY *.sln .
COPY login_api_iw_js/*.csproj ./login_api_iw_js/
RUN dotnet restore

# Copiar el resto del código y compilar
COPY login_api_iw_js/. ./login_api_iw_js/
WORKDIR /app/login_api_iw_js
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/login_api_iw_js/out ./

EXPOSE 80

ENTRYPOINT ["dotnet", "login_api_iw_js.dll"]
