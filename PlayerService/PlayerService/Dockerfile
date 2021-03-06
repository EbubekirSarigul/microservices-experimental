#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PlayerService/PlayerService.csproj", "PlayerService/"]
COPY ["Player.Core/Player.Core.csproj", "Player.Core/"]
COPY ["Player.Data/Player.Data.csproj", "Player.Data/"]
RUN dotnet nuget locals all --clear
RUN dotnet restore "PlayerService/PlayerService.csproj"
COPY . .
WORKDIR "/src/PlayerService"
RUN dotnet build "PlayerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayerService.dll"]