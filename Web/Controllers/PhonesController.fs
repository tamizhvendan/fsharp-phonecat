namespace Web.Controllers

open System.Web.Http
open PhoneCat.Domain

[<RoutePrefix("api/phones")>]
type PhonesController
    (
        getTopSellingPhones : int -> seq<Phone> -> seq<Phone>,
        phones : seq<Phone>              
    ) = 
    inherit ApiController()

    [<Route("topselling")>]
    member this.GetTopSelling () =
        getTopSellingPhones 3 phones
