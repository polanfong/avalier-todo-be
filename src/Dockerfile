FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Avalier.Todo.Host/*.csproj ./Avalier.Todo.Host/
RUN dotnet restore

# copy everything else and build app
COPY Avalier.Todo.Host/. ./Avalier.Todo.Host/
WORKDIR /app/Avalier.Todo.Host
RUN dotnet publish -c Release -o out -r linux-musl-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine3.9 AS runtime

# Fix Vulnerability (To be revoved as of Alpine 3.10): CVE-2019-14697 
RUN apk upgrade -U

WORKDIR /app
COPY --from=build /app/Avalier.Todo.Host/out/ /app/
ENTRYPOINT /app/Avalier.Todo.Host