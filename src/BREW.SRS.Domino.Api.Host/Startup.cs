using AutoMapper;
using BREW.SRS.Domino.Application.Entities;
using BREW.SRS.Domino.Host.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace BREW.SRS.Domino.Api
{
    public class Startup
    {
        private readonly string _environment;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            var con = Configuration.GetConnectionString("DominoDbConnection");

            string connectionString = "";
            //Database
            if (_environment == "Local")
                connectionString = Configuration[@"Database:ConnectionString"];
            else
                connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

            services.AddDbContextPool<DominoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DominoDbConnection")));
            services.AddControllers();

            services.AddServiceBindings();

            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "SRS  Project Domino API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                 
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Bearer token part should be prepended with 'Bearer '. Eg: Bearer [token]"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {{
                        new OpenApiSecurityScheme() { Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }},
                        new string[] {}
                    }});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Domino API V1");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // run migration backend migration
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var apiContext = serviceScope.ServiceProvider.GetRequiredService<DominoDbContext>();
                apiContext.Database.Migrate();
            }
        }
    }
}
