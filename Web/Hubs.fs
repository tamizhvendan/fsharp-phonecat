namespace PhoneCat.Web

open Microsoft.AspNet.SignalR
open System.Web
open System.Web.Routing
open System
open PhoneCat.Domain
open System.Dynamic
open ImpromptuInterface.FSharp
open System.Web.Security
open System.Reflection
open PhoneViewTracker
open PhoneCat.DataAccess
open PhoneCat.Domain.UserNavigationHistory

module Hubs =     

  let private getUrl (phoneId : string) httpContext =
    let routeValueDictionary = new RouteValueDictionary()
    routeValueDictionary.Add("controller", "Phone")
    routeValueDictionary.Add("action", "Show")
    routeValueDictionary.Add("id", phoneId)
    let requestContext = new RequestContext(new HttpContextWrapper(httpContext), new RouteData());
    let virtualPathData = RouteTable.Routes.GetVirtualPath(requestContext, routeValueDictionary);
    virtualPathData.VirtualPath

  // copied from http://stackoverflow.com/questions/2481037/how-do-you-get-anonymousid-from-cookie-aspxanonymous
  let private decode encodedAnonymousId =
    let decodeMethod = 
      typeof<AnonymousIdentificationModule>
        .GetMethod("GetDecodedValue", BindingFlags.Static ||| BindingFlags.NonPublic)
    let anonymousIdData = decodeMethod.Invoke(null, [| encodedAnonymousId |]);
    let field = 
      anonymousIdData.GetType()
        .GetField("AnonymousId", BindingFlags.Instance ||| BindingFlags.NonPublic);
    field.GetValue(anonymousIdData) :?> string

  type RecommendationHub() =
    inherit Hub ()
    member this.GetRecommendation () =             
      let encodedAnonymousId = this.Context.Request.Cookies.[".ASPXANONYMOUS"].Value
      let anonymousId = decode encodedAnonymousId
      let connectionId = this.Context.ConnectionId
      StorageAgent.Post (GetRecommendation(anonymousId, connectionId))
      "Recommendation initiated"


  let notifyRecommendation httpContext phones (connectionId, recommendedPhoneId) =
    let phones' = phones |> Seq.map TypeProviders.ToPhone
    match recommendedPhoneId with 
    | Some phoneId ->
      let recommendedPhone = Phones.getPhoneById phones' phoneId
      let phoneUrl = getUrl phoneId httpContext
      let hubContext = GlobalHost.ConnectionManager.GetHubContext<RecommendationHub>()
      hubContext.Clients.Client(connectionId)?showRecommendation(recommendedPhone, phoneUrl) 
    | None -> ()