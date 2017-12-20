using PhotoBrowser.Features.Core;

using MediatR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

using static PhotoBrowser.UnityConfiguration;
using PhotoBrowser.Features.Security;

namespace PhotoBrowser
{
    public class ApiConfiguration
    {
        public static void Install(HttpConfiguration config, IAppBuilder app)
        {
            WebApiUnityActionFilterProvider.RegisterFilterProviders(config);
            var container = GetContainer();

            app.UseCors(CorsOptions.AllowAll);

            app.MapSignalR();

            app.Use(typeof(StatusMiddleware));

            config.Filters.Add(new HandleErrorAttribute(container.Resolve<ILoggerFactory>()));
            
            config.SuppressHostPrincipal();

            var mediator = container.Resolve<IMediator>();

            config.EnableSwagger(c => {
                c.UseFullTypeNameInSchemaIds();
                c.SingleApiVersion("v1", "PhotoBrowser");
            })
            .EnableSwaggerUi();

            var authConfig = AuthConfiguration.Config;
            var lazyAuthConfig = container.Resolve<Lazy<IAuthConfiguration>>();
            app.UseOAuthAuthorizationServer(new OAuthOptions(lazyAuthConfig, mediator));

            app.UseJwtBearerAuthentication(new JwtOptions(authConfig.JwtAudience, authConfig.JwtIssuer, authConfig.JwtKey));

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());

            var jSettings = new JsonSerializerSettings();
            jSettings.Formatting = Formatting.Indented;
            jSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jSettings.ContractResolver = new SignalRContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings = jSettings;
            
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => JsonSerializer.Create(jSettings));

            config.MapHttpAttributeRoutes();
        }
    }

    public class JwtOptions : JwtBearerAuthenticationOptions
    {
        public JwtOptions(string jwtAudience, string jwtIssuer, string jwtKey)
        {

            AllowedAudiences = new[] { jwtAudience };
            IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(jwtIssuer, jwtKey)
            };
        }

        protected IAuthConfiguration _authConfiguration { get { return _lazyAuthConfiguration.Value; } }
        protected Lazy<IAuthConfiguration> _lazyAuthConfiguration;
    }

    public class SignalRContractResolver : IContractResolver
    {
        private readonly Assembly assembly;
        private readonly IContractResolver camelCaseContractResolver;
        private readonly IContractResolver defaultContractSerializer;

        public SignalRContractResolver()
        {
            defaultContractSerializer = new DefaultContractResolver();
            camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
            assembly = typeof(Connection).Assembly;
        }

        public JsonContract ResolveContract(Type type)
        {
            if (type.Assembly.Equals(assembly))
            {
                return defaultContractSerializer.ResolveContract(type);
            }
            return camelCaseContractResolver.ResolveContract(type);
        }
    }

    public class Constants
    {
        public static string VERSION = "1.0.0-alpha.0";
        public const int CacheOutputClientTimeSpan = 3600;
        public const int CacheOutputServerTimeSpan = 3600;
        public const int MaxStringLength = 255;
    }
}
