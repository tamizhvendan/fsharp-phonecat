namespace Web.Controllers


open System.Web.Http
open PhoneCat.Domain

[<RoutePrefix("api/promotions")>]
type PromotionsController
    (
        getPromotions : unit -> seq<PromotionItem>
    ) =
    inherit ApiController()

    [<Route("")>]
    member this.Get () = 
        getPromotions ()

