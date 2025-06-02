# Imagen base para la etapa de build (.NET 9 SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar archivos de solución y proyecto
COPY *.sln .
COPY login-api-iw-js/*.csproj ./login-api-iw-js/

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código
COPY login-api-iw-js/. ./login-api-iw-js/
WORKDIR /app/login-api-iw-js

# Publicar la aplicación
RUN dotnet publish -c Release -o out

# Imagen base para el runtime (.NET 9 ASP.NET)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar los archivos publicados desde la etapa de build
COPY --from=build /app/login-api-iw-js/out ./

# Exponer el puerto (para Render)
EXPOSE 10000

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "login-api-iw-js.dll"]
