FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copier uniquement le csproj
COPY src/RailFlow.NotificationService/RailFlow.NotificationService.csproj src/RailFlow.NotificationService/

# 2. Restore (cacheable)
RUN dotnet restore src/RailFlow.NotificationService/RailFlow.NotificationService.csproj

# 3. Copier le reste du code
COPY src/RailFlow.NotificationService/ src/RailFlow.NotificationService/

# 4. Build
RUN dotnet publish src/RailFlow.NotificationService/RailFlow.NotificationService.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RailFlow.NotificationService.dll"]
