using System;
using System.Text;
using Library.Application.Extensions;
using Library.Domain.Options;
using Library.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using WebUI.Filters;

namespace WebUI
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
            services.AddMongo(Configuration);
            services.AddRepositories();
            services.AddSettings(Configuration);
            services.AddApplication();
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MultipartHeadersCountLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.AddControllersWithViews(options => 
            { 
                options.Filters.Add<UnhandledExceptionFilterAttribute>();
            });
            var settings = Configuration.GetSection("Settings").Get<Settings>();
            services.AddAuthentication("Bearer")
                .AddJwtBearer(opts =>
            {
                opts.RequireHttpsMetadata = false;
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //.AddJwtBearer("docksite", opts => 
            //{
            //    opts.Authority = settings.DocksiteUrl;
            //    opts.RequireHttpsMetadata = false;

            //});
            //services.AddAuthorization(opts =>
            //{
            //    opts.DefaultPolicy = new AuthorizationPolicyBuilder()
            //            .RequireAuthenticatedUser()
            //            .AddAuthenticationSchemes(new "local", "docksite")
            //            .Build();
            //});
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title =  "CMM Forms",
                    Version = "v1",
                    Description = "API"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()

                    }
                });
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMM Forms API V1");
            });
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) => 
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = long.MaxValue;
                await next.Invoke();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}