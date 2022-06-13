#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Services/Posts/Posts.API/Posts.API.csproj", "Services/Posts/Posts.API/"]
COPY ["Services/Posts/Posts.DataAccess/Posts.DataAccess.csproj", "Services/Posts/Posts.DataAccess/"]
COPY ["Services/Posts/Posts.BusinessLogic/Posts.BusinessLogic.csproj", "Services/Posts/Posts.BusinessLogic/"]
RUN dotnet restore "Services/Posts/Posts.API/Posts.API.csproj"
COPY . .
WORKDIR "/src/Services/Posts/Posts.API"
RUN dotnet build "Posts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Posts.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Posts.API.dll"]