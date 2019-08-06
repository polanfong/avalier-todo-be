
## Setup Dependency Injection (Autofac)

Reference...   
https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#quick-start-without-configurecontainer

Add the Add the Autofac package(s) to the project...

```bash
cd Avalier.Todo.Host
dotnet add package Autofac.Extensions.DependencyInjection
```

Add a DefaultModule.cs file to the webapi project...

```cs
using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Avalier.Todo.Host
{
    public class DefaultModule : Autofac.Module
    {
        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            // Register services by convention... //
            var assembly = Assembly.GetEntryAssembly();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith(assembly.GetName().Name.Split(".").FirstOrDefault()))
                .Union(new[] {assembly})
                .ToArray();
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().AsSelf();
        }  
    } 
}
```

In Startup.cs, add the following using statements... 

```cs
using Autofac;
using Autofac.Extensions.DependencyInjection;
```

In Startup.cs, add the following property below the Configuration property...

```cs
public IContainer Container { get; private set; }
```

In Startup.cs, change...

```cs
public void ConfigureServices(IServiceCollection services)
```

to...

```cs
public IServiceProvider ConfigureServices(IServiceCollection services)
```

In Startup.cs, add the following code below services.AddMvc()...

```cs
    // Create the container builder.
    var builder = new ContainerBuilder();

    // Register dependencies, populate the services from
    // the collection, and build the container.
    //
    // Note that Populate is basically a foreach to add things
    // into Autofac that are in the collection. If you register
    // things in Autofac BEFORE Populate then the stuff in the
    // ServiceCollection can override those things; if you register
    // AFTER Populate those registrations can override things
    // in the ServiceCollection. Mix and match as needed.
    builder.Populate(services);
    builder.RegisterModule<DefaultModule>();
    this.Container = builder.Build();

    // Create the IServiceProvider based on the container.
    return new AutofacServiceProvider(this.Container);
```






