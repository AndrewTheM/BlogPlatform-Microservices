#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Services/Accounts/Accounts.API/Accounts.API.csproj", "Services/Accounts/Accounts.API/"]
COPY ["Services/Accounts/Accounts.Infrastructure/Accounts.Infrastructure.csproj", "Services/Accounts/Accounts.Infrastructure/"]
COPY ["Services/Accounts/Accounts.Domain/Accounts.Domain.csproj", "Services/Accounts/Accounts.Domain/"]
COPY ["Services/Accounts/Accounts.Application/Accounts.Application.csproj", "Services/Accounts/Accounts.Application/"]
RUN dotnet restore "Services/Accounts/Accounts.API/Accounts.API.csproj"
COPY . .
WORKDIR "/src/Services/Accounts/Accounts.API"
RUN dotnet build "Accounts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.API.dll"]