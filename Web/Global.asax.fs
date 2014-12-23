namespace Web

open System
open System.Net.Http
open System.Web
open System.Web.Http
open System.Web.Mvc
open System.Web.Routing
open System.Web.Optimization
open System.Web.Http.Dispatcher
open PhoneCat.Web.Controllers
open System.Web.Http.Controllers
open PhoneCat.DataAccess
open PhoneCat.Web
open PhoneCat.Web.Infrastructure

type BundleConfig() =
    static member RegisterBundles (bundles:BundleCollection) =
        bundles.Add(ScriptBundle("~/bundles/jquery")
            .Include([|"~/Scripts/jquery-{version}.js"|]))

        bundles.Add(ScriptBundle("~/bundles/knockout")
            .Include([|"~/Scripts/knockout-3.2.0.js"|]))

        bundles.Add(ScriptBundle("~/bundles/modernizr")
            .Include([|"~/Scripts/modernizr-*"|]))

        bundles.Add(ScriptBundle("~/bundles/bootstrap")
            .Include("~/Scripts/bootstrap.js", 
                        "~/Scripts/respond.js"))

        bundles.Add(StyleBundle("~/Content/css")
            .Include("~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/shop-homepage.css"))

/// Route for ASP.NET MVC applications
type Route = { 
    controller : string
    action : string
    id : UrlParameter }

type HttpRoute = {
    controller : string
    id : RouteParameter }



type Global() =
    inherit System.Web.HttpApplication() 

    static member RegisterWebApi(config: HttpConfiguration, phones) =
        // Configure routing
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            "DefaultApi", // Route name
            "api/{controller}/{id}", // URL with parameters
            { controller = "{controller}"; id = RouteParameter.Optional } // Parameter defaults
        ) |> ignore

        // Configure serialization
        config.Formatters.XmlFormatter.UseXmlSerializer <- true
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver <- Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()

        // Additional Web API settings        
        let phoneIndexes = GitHubRepository.getPhoneIndexes()
        config.Services.Replace(typeof<IHttpControllerActivator>, CompositionRoot(phones, phoneIndexes))

    static member RegisterFilters(filters: GlobalFilterCollection) =
        filters.Add(new HandleErrorAttribute())

    static member RegisterRoutes(routes:RouteCollection) =
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.MapRoute(
            "Default", // Route name
            "{controller}/{action}/{id}", // URL with parameters
            { controller = "Home"; action = "Index"; id = UrlParameter.Optional } // Parameter defaults
        ) |> ignore

    member x.Application_Start() =
      let phones = GitHubRepository.getPhones()
      AreaRegistration.RegisterAllAreas()
      GlobalConfiguration.Configure(new Action<HttpConfiguration>(fun config -> Global.RegisterWebApi(config, phones)))
      Global.RegisterFilters(GlobalFilters.Filters)
      Global.RegisterRoutes(RouteTable.Routes)
      ControllerBuilder.Current.SetControllerFactory(MvcInfrastructure.CompositionRoot(phones))
      BundleConfig.RegisterBundles BundleTable.Bundles