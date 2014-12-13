namespace Web.Controllers

open System.Web.Http
open PhoneCat.Domain
open PhoneCat.DataAccess

[<RoutePrefix("api/promotions")>]
type PromotionsController
    (
        getPromotions : seq<PhoneIndexTypeProvider.Root> -> seq<PromotionItem>,
        phoneIndexes: seq<PhoneIndexTypeProvider.Root>
    ) =
    inherit ApiController()

    [<Route("")>]
    member this.Get () = 
        getPromotions phoneIndexes

