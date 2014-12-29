namespace PhoneCat.Web

open Microsoft.AspNet.SignalR
open System.Web
open System.Web.Routing

type RecommendationHub (httpContext : HttpContext) =
  inherit Hub ()
  member this.GetUrl(phoneId : string) =
    let routeValueDictionary = new RouteValueDictionary()
    routeValueDictionary.Add("controller", "Phone")
    routeValueDictionary.Add("action", "Show")
    routeValueDictionary.Add("id", phoneId)
    let requestContext = new RequestContext(new HttpContextWrapper(httpContext), new RouteData());
    let virtualPathData = RouteTable.Routes.GetVirtualPath(requestContext, routeValueDictionary);
    virtualPathData.VirtualPath

