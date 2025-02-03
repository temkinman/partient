#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SeedingInitialData/SeedingInitialData.csproj", "SeedingInitialData/"]
COPY ["Hospital.Domain/Hospital.Domain.csproj", "Hospital.Domain/"]
COPY ["Hospital.Infrastructure/Hospital.Infrastructure.csproj", "Hospital.Infrastructure/"]
COPY ["Hospital.Application/Hospital.Application.csproj", "Hospital.Application/"]
RUN dotnet restore "./SeedingInitialData/SeedingInitialData.csproj"
COPY . .
WORKDIR "/src/SeedingInitialData"
RUN dotnet build "./SeedingInitialData.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SeedingInitialData.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SeedingInitialData.dll"]