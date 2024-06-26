#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081
#install node

FROM node:latest as node_base
RUN echo "Node.js version:" && node -v
RUN echo "NPM version:" && npm -v

USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

#make nodejs available
COPY --from=node_base . .


ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["shop-front/nuget.config", "shop-front/"]
COPY ["shop-back.Server/shop-back.Server.csproj", "shop-back.Server/"]
COPY ["shop-front/shop-front.esproj", "shop-front/"]
RUN dotnet restore "./shop-back.Server/./shop-back.Server.csproj"
COPY . .
WORKDIR "/src/shop-back.Server"
RUN dotnet build "./shop-back.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./shop-back.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shop-back.Server.dll"]