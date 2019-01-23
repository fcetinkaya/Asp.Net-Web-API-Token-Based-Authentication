using Microsoft.Owin;
using Microsoft.Owin.Security.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using AspNetWebAPIOAuth.OAuth.Providers;

[assembly: OwinStartup(typeof(AspNetWebAPIOAuth.OAuth.Startup))]
namespace AspNetWebAPIOAuth.OAuth
{
    // Servis çalışmaya başlarken Owin pipeline'ını ayağa kaldırabilmek için Startup'u yazıyoruz.
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(appBuilder);
            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"), // Get token path
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp=true,
              Provider= new SimpleAuthorizationServerProvider()
            };

            // AppBuilder be product authorization settings
            appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);

            // Beraer token OAuth 2.0 token type
            // Beraer securtiy SSL
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}