
## Setup OpenApi (Swagger)

Reference...   
https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle

Add the Swashbuckle package to the project...

```bash
# Remove --version 5.0.0-rc2 if 5 is no longer in beta #
dotnet add package Swashbuckle.AspNetCore --version 5.0.0-rc2
```

In Startup.cs, add the following using statment...

```cs
using Microsoft.OpenApi.Models;
```

In Startup.cs, add the following code below the Configuration property...

```cs
        public string Title => this.GetType().Assembly.GetName().Name;

        public int MajorVersion => this.GetType().Assembly.GetName().Version.Major;
```
In Startup.cs, add the following code below the services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); statement...

```cs
// Register the Swagger generator, defining 1 or more Swagger documents
services.AddSwaggerGen(c =>
{
    c.RoutePrefix = "";
    c.SwaggerDoc($"v{MajorVersion}", new OpenApiInfo { Title = Title, Version = $"v{MajorVersion}" });
});
```

In Startup.cs, add the following code to the beginning of the Configure method...

```cs
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/swagger/v{MajorVersion}/swagger.json", $"{Title} V{MajorVersion}");
});
```

In Properties/launchSettings.json change each launchUrl from "api/values" to ""...

```json
"launchUrl": "",
```
