using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using NHibernate;
using PayCore_H3.Context;
using PayCore_H3.StartUpExtension;

namespace PayCore_H3
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

            var dbConfig = Configuration.GetConnectionString("DefaultConnection");
            services.AddNHibernatePosgreSql(dbConfig);

            //services.AddSingleton<NHibernate.ISessionFactory>(factory =>
            //{
            //    return Fluently.Configure()
            //        .Database(
            //            () =>
            //            {
            //                return FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard
            //                    .ShowSql()
            //                    .ConnectionString(dbConfig);
            //            }
            //        )
            //        .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
            //        ).BuildSessionFactory();
            //});

            //services.AddSingleton<ISession>(factory => factory.GetServices<ISessionFactory>().First().OpenSession());
            //services.AddScoped<IMapperSession, MapperSession>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayCore_H3", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayCore_H3 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
