#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/Stock.API/Stock.API.csproj", "Services/Stock.API/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/Stock.API/Stock.API.csproj"
COPY . .
WORKDIR "/src/Services/Stock.API"
RUN dotnet build "Stock.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stock.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stock.API.dll"]