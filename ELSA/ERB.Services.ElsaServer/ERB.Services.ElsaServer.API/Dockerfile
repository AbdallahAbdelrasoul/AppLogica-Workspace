#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ERB.Services.ElsaServer.API/nuget.config", "ERB.Services.ElsaServer.API/"]
COPY ["ERB.Services.ElsaServer.API/ERB.Services.ElsaServer.API.csproj", "ERB.Services.ElsaServer.API/"]
COPY ["ERB.Services.ElsaServer.Domain/ERB.Services.ElsaServer.Domain.csproj", "ERB.Services.ElsaServer.Domain/"]
RUN dotnet restore "./ERB.Services.ElsaServer.API/ERB.Services.ElsaServer.API.csproj"
COPY . .
WORKDIR "/src/ERB.Services.ElsaServer.API"
RUN dotnet build "./ERB.Services.ElsaServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ERB.Services.ElsaServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERB.Services.ElsaServer.API.dll"]