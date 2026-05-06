FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copier uniquement les .csproj (cache)
COPY src/RailFlow.NotificationService/RailFlow.NotificationService.csproj RailFlow.NotificationService/
COPY src/RailFlow.Contracts/RailFlow.Contracts.csproj RailFlow.Contracts/

# 2. Restore
RUN dotnet restore RailFlow.NotificationService/RailFlow.NotificationService.csproj

# 3. Copier tout le code
COPY src/RailFlow.NotificationService/ RailFlow.NotificationService/
COPY src/RailFlow.Contracts/ RailFlow.Contracts/

# 4. Build / publish
WORKDIR /src/RailFlow.NotificationService
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RailFlow.NotificationService.dll"]