using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Autofac;
using Autofac.Integration.WebApi;
using BookService.Infrastructure;
using BookService.Models;
using CacheCow.Server;
using CacheCow.Server.EntityTagStore.SqlServer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //
            // JSON formatter settings
            //
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //
            // Enable CORS
            //
            config.EnableCors();
            //
            // Set versioning
            //
            config.Services.Replace(typeof(IHttpControllerSelector), new ApiControllerSelector(config));
            //
            // Set caching
            //
            //SetupInMemoryCaching(config);
            SetupDbCaching(config);
            //
            // Set up DI
            //
            SetupIoC(config);

        }

        private static void SetupIoC(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            // Register controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            // Register dependencies
            builder.RegisterType<BookServiceContext>().AsSelf().InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void SetupDbCaching(HttpConfiguration config)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["cache"].ConnectionString;
            var cacheStore = new SqlServerEntityTagStore(connectionString);
            var cacheHandler = new CachingHandler(config, cacheStore)
            {
                AddLastModifiedHeader = false
            };

            config.MessageHandlers.Add(cacheHandler);
        }

        private static void SetupInMemoryCaching(HttpConfiguration config)
        {
            var cacheHandler = new CachingHandler(config);
            config.MessageHandlers.Add(cacheHandler);
        }
    }
}
