namespace PhoneCat.Web

open Microsoft.AspNet.SignalR
open System.Web
open System.Web.Routing
open System
open PhoneCat.Domain
open System.Dynamic
open ImpromptuInterface.FSharp


module Hubs = 
  let private getUrl (phoneId : string) httpContext =
    let routeValueDictionary = new RouteValueDictionary()
    routeValueDictionary.Add("controller", "Phone")
    routeValueDictionary.Add("action", "Show")
    routeValueDictionary.Add("id", phoneId)
    let requestContext = new RequestContext(new HttpContextWrapper(httpContext), new RouteData());
    let virtualPathData = RouteTable.Routes.GetVirtualPath(requestContext, routeValueDictionary);
    virtualPathData.VirtualPath

  type RecommendationHub
    (
      httpContext : HttpContext,
      getPhone : string -> Phone
    ) =
    inherit Hub ()  

    interface IObserver<string> with
      member this.OnNext(recommendedPhoneId) = 
        let hubContext = GlobalHost.ConnectionManager.GetHubContext<RecommendationHub>()
        let phoneUrl = getUrl recommendedPhoneId httpContext
        let phone = getPhone recommendedPhoneId
        hubContext.Clients.All?showRecommendation(phone, phoneUrl)
      member this.OnError(err) = ()
      member this.OnCompleted() = ()

  

