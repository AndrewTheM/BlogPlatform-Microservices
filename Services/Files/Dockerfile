#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Services/Files/Files.API.csproj", "Services/Files/"]
RUN dotnet restore "Services/Files/Files.API.csproj"
COPY . .
WORKDIR "/src/Services/Files"
RUN dotnet build "Files.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Files.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Files.API.dll"]