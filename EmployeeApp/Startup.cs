using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EmployeeApp
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adding services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // configure database settings here 

            var dbConfigSection = Configuration.GetSection("DbConfig");
            services.Configure<DbConfig>(dbConfigSection);

            services.AddSingleton<IDbConfig>(sp => sp.GetRequiredService<IOptions<DbConfig>>().Value);

            // Configure AppSettings Information
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
            // Register the Swagger generator, defining 1 or more Swagger documents
            // Swagger contains definitions for authorization as well
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employee API",
                    Description = "Employee API specification",
                });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Enter your token details as follows : Bearer followed by your token name",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0Config:Domain"]}/";
                options.Audience = Configuration["Auth0Config:ApiIdentifier"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddAuthorization();
            // Adding singleton service
            services.Add(new ServiceDescriptor(typeof(IEmployeeService), typeof(EmployeeService), ServiceLifetime.Singleton));
        }
        

        /// <summary>
        /// Setting up the request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            //specifying the Swagger JSON endpoint.
           app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API v.1");

                // To serve the Swagger UI at the app's root
                c.RoutePrefix = string.Empty;
           });
           
            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
