namespace Web

open System
open System.Net.Http
open System.Web
open System.Web.Http
open System.Web.Mvc
open System.Web.Routing
open System.Web.Optimization
open System.Web.Http.Dispatcher
open Web.Controllers
open System.Web.Http.Controllers
open PhoneCat.DataAccess

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

type CompositionRoot() =
    interface IHttpControllerActivator with
        member this.Create(request, controllerDescriptor, controllerType) =
            if controllerType = typeof<PromotionsController> then
                let promotionsController = new PromotionsController(PhoneCat.DataAccess.Promotions.getPromotions, GitHubRepository.phoneIndexes)
                promotionsController :> IHttpController            
            else
                raise <| ArgumentException((sprintf "Unknown controller type requested: %A" controllerType))

type Global() =
    inherit System.Web.HttpApplication() 

    static member RegisterWebApi(config: HttpConfiguration) =
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
        config.Services.Replace(typeof<IHttpControllerActivator>, CompositionRoot())

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
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
        Global.RegisterFilters(GlobalFilters.Filters)
        Global.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles BundleTable.Bundles