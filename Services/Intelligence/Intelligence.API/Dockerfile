#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Services/Intelligence/Intelligence.API/Intelligence.API.csproj", "Services/Intelligence/Intelligence.API/"]
RUN dotnet restore "Services/Intelligence/Intelligence.API/Intelligence.API.csproj"
COPY . .
WORKDIR "/src/Services/Intelligence/Intelligence.API"
RUN dotnet build "Intelligence.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Intelligence.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Intelligence.API.dll"]