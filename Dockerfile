# -------------------------
# 1. BUILD STAGE
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution and project files first
COPY People-Resource-Management.sln ./
COPY People-Specification.Api.csproj ./

COPY PeopleResourceManagement.Domain/PeopleResourceManagement.Domain.csproj \
     PeopleResourceManagement.Domain/

COPY PeopleResourceManagement.Application/PeopleResourceManagement.Application.csproj \
     PeopleResourceManagement.Application/

COPY PeopleResourceManagement.Infrastructure/PeopleResourceManagement.Infrastructure.csproj \
     PeopleResourceManagement.Infrastructure/

# Restore NuGet packages
RUN dotnet restore People-Specification.Api.csproj

# Copy the rest of the source code
COPY . .

# Publish the API
RUN dotnet publish People-Specification.Api.csproj \
    -c Release \
    -o /app/publish 


# -------------------------
# 2. RUNTIME STAGE
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "People-Specification.Api.dll"]