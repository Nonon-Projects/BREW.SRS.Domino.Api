#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BREW.SRS.Domino.Api/BREW.SRS.Domino.Api.csproj", "src/BREW.SRS.Domino.Api/"]
RUN dotnet restore "src/BREW.SRS.Domino.Api/BREW.SRS.Domino.Api.csproj"
COPY . .
WORKDIR "/src/src/BREW.SRS.Domino.Api"
RUN dotnet build "BREW.SRS.Domino.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BREW.SRS.Domino.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BREW.SRS.Domino.Api.dll"]