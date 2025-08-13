# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["FifaWorldCupBetting.Api/FifaWorldCupBetting.Api.csproj", "FifaWorldCupBetting.Api/"]
COPY ["FifaWorldCupBetting.Application/FifaWorldCupBetting.Application.csproj", "FifaWorldCupBetting.Application/"]
COPY ["FifaWorldCupBetting.Infrastructure/FifaWorldCupBetting.Infrastructure.csproj", "FifaWorldCupBetting.Infrastructure/"]
COPY ["FifaWorldCupBetting.Domain/FifaWorldCupBetting.Domain.csproj", "FifaWorldCupBetting.Domain/"]

RUN dotnet restore "FifaWorldCupBetting.Api/FifaWorldCupBetting.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/FifaWorldCupBetting.Api"
RUN dotnet build "FifaWorldCupBetting.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "FifaWorldCupBetting.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FifaWorldCupBetting.Api.dll"]
