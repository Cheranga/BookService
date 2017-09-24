using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using BookService.Infrastructure;
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
        }
    }
}
