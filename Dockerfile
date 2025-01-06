#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AUTHIO.API/AUTHIO.API.csproj", "src/AUTHIO.API/"]
COPY ["src/ATHIO.APPLICATION/AUTHIO.APPLICATION.csproj", "src/ATHIO.APPLICATION/"]
RUN dotnet restore "./src/AUTHIO.API/AUTHIO.API.csproj"
COPY . .
WORKDIR "/src/src/AUTHIO.API"
RUN dotnet build "./AUTHIO.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AUTHIO.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AUTHIO.API.dll"]
