namespace Web.Controllers

open System.Web.Http
open PhoneCat.Domain

[<RoutePrefix("api/phones")>]
type PhonesController
    (
        getTopSellingPhones : seq<PhoneTypeProvider.Root> -> seq<Phone>,
        phones : seq<PhoneTypeProvider.Root>              
    ) = 
    inherit ApiController()

    [<Route("topselling")>]
    member this.GetTopSelling () =
        getTopSellingPhones phones
