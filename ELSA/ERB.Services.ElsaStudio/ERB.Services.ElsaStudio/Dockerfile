#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

ENV NUGET_PASSWORD=${NUGET_PASSWORD}
# Adds this source with basic authentication, other authentication types exist but I'm not sure if they are applicable here in Linux based container
RUN dotnet nuget add source https://pkgs.dev.azure.com/AppLogica/_packaging/AppLogica/nuget/v3/index.json --name Applogica --username ${NUGET_USERNAME} --store-password-in-clear-text --password ${NUGET_PASSWORD}

WORKDIR /src
COPY ["nuget.config", "."]
COPY ["ERB.Services.ElsaStudio.csproj", "."]
RUN dotnet restore "./ERB.Services.ElsaStudio.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ERB.Services.ElsaStudio.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ERB.Services.ElsaStudio.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERB.Services.ElsaStudio.dll"]