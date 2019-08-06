
```bash

# Make a folder for project #
mkdir avalier.todo
cd avalier.todo

# Setup source control #
git init

# Create a src folder #
mkdir src
cd src

# Create solution #
dotnet new sln -n Avalier.Todo

# Create a web application project #
dotnet new webapi -n Avalier.Todo.Host

# Add project to solution #
dotnet sln add ./Avalier.Todo.Host/Avalier.Todo.Host.csproj

# Run to make sure everything is ok #
dotnet run --project ./Avalier.Todo.Host/Avalier.Todo.Host.csproj
```

