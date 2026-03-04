# ===== Build (SDK) =====
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# Copiar el script de entrada y darle permisos de ejecución
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

# Copiar los paquetes locales y NuGet.config
COPY packages/ /app/packages/
COPY NuGet.config /app/NuGet.config

# Copiá primero el csproj (mejora el cache)
COPY BussinessCore/SmartClickCore.csproj BussinessCore/
COPY DAL/DAL.csproj DAL/
RUN dotnet restore BussinessCore/SmartClickCore.csproj --configfile /app/NuGet.config

# Copiá el resto del código y publicá
COPY . .
# Eliminamos la línea RUN dotnet tool install --global dotnet-ef

RUN dotnet publish BussinessCore/SmartClickCore.csproj -c Debug -o /app/out --configfile /app/NuGet.config

# ===== Runtime (ASP.NET Core) =====
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app

# 1. Copiar las librerías publicadas
COPY --from=build /app/out ./

# 2. Copiar el script de entrada y darle permisos
COPY --from=build /app/entrypoint.sh ./
RUN chmod +x entrypoint.sh

# Ejecutar el script como punto de entrada
ENTRYPOINT ["./entrypoint.sh"]