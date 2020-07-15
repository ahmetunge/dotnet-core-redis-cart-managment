# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Api/*.csproj ./Api/
COPY Entities/*.csproj ./Entities/
COPY Core/*.csproj ./Core/
COPY DataAccess/*.csproj ./DataAccess/
COPY Business/*.csproj ./Business/
COPY Business.Tests/*.csproj ./Business.Tests/
RUN dotnet restore

# copy everything else and build app
COPY Api/. ./Api/
COPY Entities/. ./Entities/
COPY Core/. ./Core/
COPY DataAccess/. ./DataAccess/
COPY Business/. ./Business/
COPY Business.Tests/. ./Business.Tests/
WORKDIR /source/Api
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Api.dll"]