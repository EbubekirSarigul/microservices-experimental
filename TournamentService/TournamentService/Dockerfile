#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TournamentService/TournamentService.csproj", "TournamentService/"]
COPY ["Tournament.Core/Tournament.Core.csproj", "Tournament.Core/"]
COPY ["Tournament.Data/Tournament.Data.csproj", "Tournament.Data/"]
RUN dotnet nuget locals all --clear
RUN dotnet restore "TournamentService/TournamentService.csproj"
COPY . .
WORKDIR "/src/TournamentService"
RUN dotnet build "TournamentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TournamentService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TournamentService.dll"]