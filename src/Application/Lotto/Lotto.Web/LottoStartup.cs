// <copyright file="LottoStartup.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Web.Http;
using Lotto.Web.Common;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Lotto.Web.LottoStartup))]

namespace Lotto.Web
{
    public class LottoStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var lottoBootstrapper = new LottoWebBootstrapper();
            lottoBootstrapper.Initialize();

            var httpConfig = new HttpConfiguration();
            httpConfig.Routes.MapHttpRoute("Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            httpConfig.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpConfig.DependencyResolver = lottoBootstrapper.GetDependencyResolver();
            app.UseWebApi(httpConfig);

            app.UseNancy();
        }
    }
}
