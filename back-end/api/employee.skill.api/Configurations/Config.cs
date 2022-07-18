using System;
using System.Reflection;
using AspNetCoreRateLimit;
using doc.imagination.services.Skills;
using employee.skill.common.infrastructure.Exceptions.Repositories;
using employee.skill.common.infrastructure.PropertyMappings;
using employee.skill.common.infrastructure.PropertyMappings.TypeHelpers;
using employee.skill.common.infrastructure.Serializers;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.Employees.contracts.Employees;
using employees.skills.api.Helpers;
using employees.skills.contracts.Employees;
using employees.skills.contracts.Skills;
using employees.skills.contracts.V1;
using employees.skills.repository.Mappings.Employees;
using employees.skills.repository.NhUnitOfWork;
using employees.skills.repository.Repositories;
using employees.skills.services.Employees;
using employees.skills.services.Skills;
using employees.skills.services.V1;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Spatial.Dialect;
using NHibernate.Spatial.Mapping;
using NHibernate.Spatial.Metadata;
using Skills.skills.services.V1;

namespace employees.skills.api.Configurations
{

    /// <summary>
    /// Class : Config
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// ConfigureRepositories
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                    .ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            services.AddSingleton<IPropertyMappingService, PropertyMappingService>();
            services.AddSingleton<ITypeHelperService, TypeHelperService>();

            services.AddSingleton<IJsonSerializer, JsonSerializer>();

            services.AddScoped<IInquiryEmployeeProcessor, InquiryEmployeeProcessor>();
            services.AddScoped<IInquiryAllEmployeesProcessor, InquiryAllEmployeesProcessor>();
            services.AddScoped<ICreateEmployeeProcessor, CreateEmployeeProcessor>();
            services.AddScoped<IUpdateEmployeeProcessor, UpdateEmployeeProcessor>();
            services.AddScoped<IDeleteEmployeeProcessor, DeleteEmployeeProcessor>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeesControllerDependencyBlock, EmployeesControllerDependencyBlock>();
            
            services.AddScoped<IInquirySkillProcessor, InquirySkillProcessor>();
            services.AddScoped<IInquiryAllSkillsProcessor, InquiryAllSkillsProcessor>();
            services.AddScoped<ICreateSkillProcessor, CreateSkillProcessor>();
            services.AddScoped<IUpdateSkillProcessor, UpdateSkillProcessor>();
            services.AddScoped<IDeleteSkillProcessor, DeleteSkillProcessor>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillsControllerDependencyBlock, SkillsControllerDependencyBlock>();
        }

        /// <summary>
        /// Method : ConfigureAutoMapper
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddSingleton<IAutoMapper, AutoMapperAdapter>();
        }

        /// <summary>
        /// Method : ConfigureNHibernate
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <exception cref="NHibernateInitializationException"></exception>
        public static void ConfigureNHibernate(IServiceCollection services, string connectionString)
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            try
            {
                var cfg = Fluently.Configure()
                    .Database(PostgreSQLConfiguration.PostgreSQL82
                        .ConnectionString(connectionString)
                        .Driver<NpgsqlDriver>()
                        .Dialect<PostGis20Dialect>()
                        .MaxFetchDepth(5)
                        .FormatSql()
                        .Raw("transaction.use_connection_on_system_prepare", "true")
                        .AdoNetBatchSize(100)
                    )
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
                    .Cache(c => c.UseSecondLevelCache().UseQueryCache()
                        .ProviderClass(typeof(NHibernate.Caches.RtMemoryCache.RtMemoryCacheProvider)
                            .AssemblyQualifiedName)
                    )
                    .CurrentSessionContext("web")
                    .BuildConfiguration();

                cfg.AddAssembly(Assembly.GetExecutingAssembly());
                cfg.AddAuxiliaryDatabaseObject(new SpatialAuxiliaryDatabaseObject(cfg));
                Metadata.AddMapping(cfg, MetadataClass.GeometryColumn);
                Metadata.AddMapping(cfg, MetadataClass.SpatialReferenceSystem);

                var sessionFactory = cfg.BuildSessionFactory();

                services.AddSingleton<ISessionFactory>(sessionFactory);

                services.AddScoped<ISession>((ctx) =>
                {
                    var sf = ctx.GetRequiredService<ISessionFactory>();

                    return sf.OpenSession();

                });

                services.AddScoped<IUnitOfWork, NhUnitOfWork>();
            }
            catch (Exception ex)
            {
                //Todo : Log
                throw new NHibernateInitializationException(ex.Message, ex.InnerException?.Message);
            }
        }
    }
}