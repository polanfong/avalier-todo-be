## Build ##
FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Avalier.Todo.Host/*.csproj ./Avalier.Todo.Host/
COPY Avalier.Todo.Host.Tests/*.csproj ./Avalier.Todo.Host.Tests/
RUN dotnet restore

# copy everything else and build app
COPY Avalier.Todo.Host/. ./Avalier.Todo.Host/
COPY Avalier.Todo.Host.Tests/. ./Avalier.Todo.Host.Tests/
WORKDIR /app/Avalier.Todo.Host
RUN dotnet publish -c Release -o out -r linux-musl-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true

## Test ##
FROM build AS test
WORKDIR /app/
RUN dotnet test --test-adapter-path:. --logger:xunit --results-directory /test
#RUN dotnet test --results-directory /test --logger "trx;LogFileName=test_results.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=/test/coverage/ /p:Exclude="[xunit.*]*%2c[StackExchange.*]*"
#RUN dotnet tool install --global dotnet-reportgenerator-globaltool
RUN ls -al /test
LABEL test=true

#export id=$(docker images --filter "label=test=true" -q | head -1)
#docker create --name testcontainer $id
#docker cp testcontainer:/test ./test
#docker run --rm -it $(docker images --filter "label=test=true" -q | head -1) /bin/sh

## Run ##
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine3.9 AS runtime
RUN apk upgrade -U
WORKDIR /app
COPY --from=build /app/Avalier.Todo.Host/out/ /app/
ENTRYPOINT /app/Avalier.Todo.Host