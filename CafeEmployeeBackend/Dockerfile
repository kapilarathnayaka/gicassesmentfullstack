# Use the official .NET image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CafeEmployee.API/CafeEmployee.API.csproj", "CafeEmployee.API/"]
COPY ["CafeEmployee.Application/CafeEmployee.Application.csproj", "CafeEmployee.Application/"]
COPY ["CafeEmployee.Infrastructure/CafeEmployee.Infrastructure.csproj", "CafeEmployee.Infrastructure/"]
COPY ["CafeEmployee.Domain/CafeEmployee.Domain.csproj", "CafeEmployee.Domain/"]
COPY ["CafeEmployee.Dtos/CafeEmployee.Dtos.csproj", "CafeEmployee.Dtos/"]
RUN dotnet restore "CafeEmployee.API/CafeEmployee.API.csproj"
COPY . .
WORKDIR "/src/CafeEmployee.API"
RUN dotnet build "CafeEmployee.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CafeEmployee.API.csproj" -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CafeEmployee.API.dll"]
