#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Presentation/BlogPlatform.UI/BlogPlatform.UI.csproj", "Presentation/BlogPlatform.UI/"]
RUN dotnet restore "Presentation/BlogPlatform.UI/BlogPlatform.UI.csproj"
COPY . .
WORKDIR "/src/Presentation/BlogPlatform.UI"
RUN dotnet build "BlogPlatform.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogPlatform.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogPlatform.UI.dll"]
