using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Livraria.Api.Middleware;
using Livraria.Domain.Handlers;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Infra.Data.DataContexts;
using Livraria.Infra.Data.Respositories;
using Livraria.Infra.Settings;
using LivrariaComMongo.Infra.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Livraria.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region AppSettings

            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton(appSettings);

            #endregion AppSettings

            #region DataContexts

            services.AddScoped<DataContext>();

            #endregion DataContexts

            #region Respositories

            services.AddTransient<ILivroRepository, LivroRepository>();

            #endregion Respositories

            #region Handlers

            services.AddTransient<LivroHandler, LivroHandler>();

            #endregion Handlers

            services.AddControllers();


            services.AddElmah();
            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/log";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Livraria.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/elmah", StringComparison.OrdinalIgnoreCase), appBuilder =>
            {
                appBuilder.Use(next =>
                {
                    return async ctx =>
                    {
                        ctx.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO = true;
                        await next(ctx);
                    };
                });
            });

            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
