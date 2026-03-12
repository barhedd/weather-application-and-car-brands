# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar csproj
COPY ["CarBrands.Api/CarBrands.Api.csproj", "CarBrands.Api/"]
COPY ["CarBrands.Application/CarBrands.Application.csproj", "CarBrands.Application/"]
COPY ["CarBrands.Domain/CarBrands.Domain.csproj", "CarBrands.Domain/"]
COPY ["CarBrands.Infrastructure/CarBrands.Infrastructure.csproj", "CarBrands.Infrastructure/"]
COPY ["CarBrands.Tests/CarBrands.Tests.csproj", "CarBrands.Tests/"]
COPY ["CarBrands.Tests.Integration/CarBrands.Tests.Integration.csproj", "CarBrands.Tests.Integration/"]

# Restore dependencias
RUN dotnet restore "CarBrands.Api/CarBrands.Api.csproj"

# Copiar todo el código
COPY . .

# Ejecutar unit tests
RUN dotnet restore "CarBrands.Tests/CarBrands.Tests.csproj" && \
    dotnet test "CarBrands.Tests/CarBrands.Tests.csproj" -c $BUILD_CONFIGURATION --no-restore

# Integration tests
RUN dotnet restore "CarBrands.Tests.Integration/CarBrands.Tests.Integration.csproj" && \
    dotnet test "CarBrands.Tests.Integration/CarBrands.Tests.Integration.csproj" -c $BUILD_CONFIGURATION --no-restore

# Publicar API
WORKDIR "/src/CarBrands.Api"
RUN dotnet publish "CarBrands.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarBrands.Api.dll"]