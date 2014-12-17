namespace Web.Controllers

open System.Web.Http
open PhoneCat.Domain

[<RoutePrefix("api/promotions")>]
type PromotionsController
    (
        getPromotions : seq<PhoneIndex> -> seq<PromotionPhone>,
        phoneIndexes: seq<PhoneIndex>
    ) =
    inherit ApiController()

    [<Route("")>]
    member this.Get () = 
        getPromotions phoneIndexes
