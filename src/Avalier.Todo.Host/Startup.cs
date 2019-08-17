using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Avalier.Todo.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public string Title => this.GetType().Assembly.GetName().Name;

        public int MajorVersion => this.GetType().Assembly.GetName().Version.Major;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                //c.RoutePrefix = "";
                c.SwaggerDoc($"v{MajorVersion}", new OpenApiInfo { Title = Title, Version = $"v{MajorVersion}" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // wire up using autofac specific APIs here
            
            builder.RegisterModule<DefaultModule>();
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            // OpenApi / Swagger //
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint($"/swagger/v{MajorVersion}/swagger.json", $"{Title} V{MajorVersion}");
            }); 
        }
    }
}
