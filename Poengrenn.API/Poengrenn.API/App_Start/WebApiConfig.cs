using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Poengrenn.API.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Poengrenn.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.EnableCors(new EnableCorsAttribute("*", "", ""));
            config.MessageHandlers.Add(new PreflightRequestHandler());


            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.Clear();
            
            var serializer = new JsonSerializerSettings {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var formatter = new JsonMediaTypeFormatter { Indent = true, SerializerSettings = serializer };
            config.Formatters.Add(formatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
