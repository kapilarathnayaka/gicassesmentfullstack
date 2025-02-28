# Build Stage: Use .NET SDK to compile the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project files (without unnecessary files like bin, obj)
COPY ["CafeEmployeeAPI.sln", "."]
COPY ["CafeEmployeeAPI.WebAPI/CafeEmployeeAPI.WebAPI.csproj", "CafeEmployeeAPI.WebAPI/"]
COPY ["CafeEmployeeAPI.Application/CafeEmployeeAPI.Application.csproj", "CafeEmployeeAPI.Application/"]
COPY ["CafeEmployeeAPI.Infrastructure/CafeEmployeeAPI.Infrastructure.csproj", "CafeEmployeeAPI.Infrastructure/"]

# Restore dependencies
WORKDIR /src/CafeEmployeeAPI.WebAPI
RUN dotnet restore CafeEmployeeAPI.WebAPI.csproj

# Copy the entire source code after restoring
COPY . .

# Build and publish the application
#RUN dotnet publish -c Release -o /app/publish
RUN dotnet publish CafeEmployeeAPI.WebAPI/CafeEmployeeAPI.WebAPI.csproj -c Release -o /app/publish


# Runtime Stage: Use lightweight .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/publish .

# Expose the port
EXPOSE 80
ENV ASPNETCORE_URLS="http://0.0.0.0:80"

# Set entry point
ENTRYPOINT ["dotnet", "CafeEmployeeAPI.WebAPI.dll"]
