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