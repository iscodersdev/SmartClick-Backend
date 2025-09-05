# ===== Build =====
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# Copiar los paquetes locales y NuGet.config
COPY packages/ /app/packages/
COPY NuGet.config /app/NuGet.config

# Copiá primero el csproj (mejora el cache)
COPY BussinessCore/SmartClickCore.csproj BussinessCore/
RUN dotnet restore BussinessCore/SmartClickCore.csproj --configfile /app/NuGet.config

# Copiá el resto del código y publicá
COPY . .
RUN dotnet publish BussinessCore/SmartClickCore.csproj -c Release -o /app/out --configfile /app/NuGet.config

# ===== Runtime (ASP.NET Core) =====
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
# Por defecto, la imagen aspnet:2.2 escucha en el puerto 80
ENTRYPOINT ["dotnet", "SmartClickCore.dll"]

