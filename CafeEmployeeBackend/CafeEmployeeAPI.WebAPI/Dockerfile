# Use the official .NET 9 SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory inside the container
WORKDIR /app

# Copy solution and restore dependencies
COPY CafeEmployeeAPI.sln . 
COPY CafeEmployeeAPI.Application/*.csproj ./CafeEmployeeAPI.Application/
COPY CafeEmployeeAPI.Domain/*.csproj ./CafeEmployeeAPI.Domain/
COPY CafeEmployeeAPI.Infrastructure/*.csproj ./CafeEmployeeAPI.Infrastructure/
COPY CafeEmployeeAPI.WebAPI/*.csproj ./CafeEmployeeAPI.WebAPI/

RUN dotnet restore

# Copy everything and build the application
COPY . .
RUN dotnet publish -c Release -o /out

# Use a smaller .NET 9 runtime image for final container
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Expose the API port
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "CafeEmployeeAPI.WebAPI.dll"]
