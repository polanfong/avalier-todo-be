
## Setup Docker

Reference:   
https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/Dockerfile.alpine-x64   
https://docs.microsoft.com/en-au/aspnet/core/host-and-deploy/docker/building-net-docker-images   
https://github.com/dotnet/dotnet-docker/blob/master/samples/dotnetapp/Dockerfile.alpine-x64   
https://www.hanselman.com/blog/MakingATinyNETCore30EntirelySelfcontainedSingleExecutable.aspx

Create a file 'Dockerfile' in the src folder...

```bash
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Avalier.Todo.Host/*.csproj ./Avalier.Todo.Host/
RUN dotnet restore

# copy everything else and build app
COPY Avalier.Todo.Host/. ./Avalier.Todo.Host/
WORKDIR /app/Avalier.Todo.Host
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runtime
WORKDIR /app
COPY --from=build /app/Avalier.Todo.Host/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "Avalier.Todo.Host.dll"]% 
```

Build and run the docker image...

```bash
# Create docker image #
docker build -t avalier/todo .

# Run docker image on port 8888 #
docker run --rm -p 8888:80 --name avalier-todo avalier/todo
```