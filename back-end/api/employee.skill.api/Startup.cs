using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Autofac;
using employees.skills.api.Configurations;
using employees.skills.api.Configurations.AutoMappingProfiles.Employees;
using employees.skills.api.Configurations.AutoMappingProfiles.Skills;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;

namespace employees.skills.api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Ctor : Startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostEnv"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnv)
        {
            this.Configuration = configuration;
            this.HostEnv = hostEnv;
        }

        private const string CorsPolicyName = "AllowSpecificOrigins";

        /// <summary>
        /// Property : Configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Property : HostEnv
        /// </summary>
        private IWebHostEnvironment HostEnv { get; }

        /// <summary>
        /// Property : ApplicationContainer
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// Method : ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builderCors => { builderCors.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var name = Assembly.GetExecutingAssembly().GetName();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("employee.skills", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Revision", $"{name.Version}")
                .WriteTo.Debug(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{HttpContext} {NewLine}{Exception}")
                .WriteTo.RollingFile(this.HostEnv.WebRootPath + "\\logs.txt", Serilog.Events.LogEventLevel.Information,
                    retainedFileCountLimit: 7)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
                loggingBuilder
                    .AddSerilog(dispose: true));

            services.AddMvc(
                    options =>
                    {
                        options.EnableEndpointRouting = false;
                        options.RespectBrowserAcceptHeader = true;
                        options.ReturnHttpNotAcceptable = true;
                    })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation();

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            ;

            services.AddResponseCaching();

            services.AddHttpCacheHeaders(
                (expirationModelOptions)
                    =>
                {
                    expirationModelOptions.MaxAge = 60;
                    expirationModelOptions.SharedMaxAge = 30;
                },
                (validationModelOptions)
                    =>
                {
                    validationModelOptions.MustRevalidate = true;
                    validationModelOptions.ProxyRevalidate = true;
                });

            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>((options) =>
            {
                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>()
                {
                    new RateLimitRule()
                    {
                        Endpoint = "*",
                        Limit = 1000,
                        Period = "5m"
                    },
                    new RateLimitRule()
                    {
                        Endpoint = "*",
                        Limit = 200,
                        Period = "10s"
                    }
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "employees.skills.api - HTTP API",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API for employees.skills.api service",
                });
            });

            Config.ConfigureRepositories(services);
            Config.ConfigureAutoMapper(services);
            Config.ConfigureNHibernate(services, this.Configuration.GetConnectionString("PostgreSqlDatabase"));

            services.AddHealthChecks()
                .AddNpgSql(this.Configuration.GetConnectionString("PostgreSqlDatabase"),
                    failureStatus: HealthStatus.Unhealthy,
                    name: "PostgreSQL database", tags: new[] { "ready" });

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builderCors => { builderCors.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); });
            });
        }

        /// <summary>
        /// Method : Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="lifetime"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHealthChecks("/health/ready",
                new HealthCheckOptions
                {

                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    },

                    Predicate = (check) => check.Tags.Contains("ready"),
                    AllowCachingResponses = false,
                    ResponseWriter = WriteHealthCheckReadyResponse
                });

            app.UseHealthChecks("/health/live",
                new HealthCheckOptions
                {
                    Predicate = (check) => !check.Tags.Contains("live"),
                    ResponseWriter = WriteHealthCheckLiveResponse,
                    AllowCachingResponses = false
                });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
                AllowCachingResponses = false
            });

            app.UseSwagger()
                .UseSwaggerUI(
                    c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "employees.skills.api - API V1"); });

            app.UseCors(CorsPolicyName);
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseApiVersioning();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<SkillEntityToSkillUiAutoMapperProfile>();
                cfg.AddProfile<EmployeeEntityToEmployeeUiAutoMapperProfile>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private Task WriteHealthCheckLiveResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("OverallStatus", result.Status.ToString()),
                new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00"))
            );

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }

        private Task WriteHealthCheckReadyResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("OverallStatus", result.Status.ToString()),
                new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00")),
                new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(dicItem =>
                    new JProperty(dicItem.Key, new JObject(
                        new JProperty("Status", dicItem.Value.Status.ToString()),
                        new JProperty("Duration", dicItem.Value.Duration.TotalSeconds.ToString("0:0.00")),
                        new JProperty("Exception", dicItem.Value.Exception?.Message),
                        new JProperty("Data", new JObject(dicItem.Value.Data.Select(dicData =>
                            new JProperty(dicData.Key, dicData.Value))))
                    ))
                )))
            );

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}