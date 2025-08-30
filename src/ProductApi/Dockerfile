# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ProductApi.csproj ./
RUN dotnet restore ./ProductApi.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish ProductApi.csproj -c Release -o /app/publish

# Use the official ASP.NET runtime image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "ProductApi.dll"]