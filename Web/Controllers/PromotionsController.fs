namespace Web.Controllers

open System.Web.Http
open PhoneCat.Domain

[<RoutePrefix("api/promotions")>]
type PromotionsController
    (
        getPromotions : seq<PhoneIndexTypeProvider.Root> -> seq<PromotionPhone>,
        phoneIndexes: seq<PhoneIndexTypeProvider.Root>
    ) =
    inherit ApiController()

    [<Route("")>]
    member this.Get () = 
        getPromotions phoneIndexes
