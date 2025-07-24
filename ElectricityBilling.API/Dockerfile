# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy .csproj files and restore as distinct layers
COPY ["ElectricityBilling.API/ElectricityBilling.API.csproj", "ElectricityBilling.API/"]
COPY ["ElectricityBilling.Application/ElectricityBilling.Application.csproj", "ElectricityBilling.Application/"]
COPY ["ElectricityBilling.Domain/ElectricityBilling.Domain.csproj", "ElectricityBilling.Domain/"]
COPY ["ElectricityBilling.Infrastructure/ElectricityBilling.Infrastructure.csproj", "ElectricityBilling.Infrastructure/"]
RUN dotnet restore "./ElectricityBilling.API/ElectricityBilling.API.csproj"

# Copy all source files
COPY . .

# Build the project
WORKDIR "/src/ElectricityBilling.API"
RUN dotnet build "./ElectricityBilling.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ElectricityBilling.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
ARG APP_UID=1000
WORKDIR /app
COPY --from=publish /app/publish .
COPY wait-for-it.sh .
RUN chmod +x wait-for-it.sh
USER $APP_UID
ENTRYPOINT ["dotnet", "ElectricityBilling.API.dll"]
