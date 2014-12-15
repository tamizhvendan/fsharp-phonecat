namespace Web.Controllers

open System.Web.Http
open PhoneCat.Domain

[<RoutePrefix("api/manufacturers")>]
type ManufacturersController
    (
        getManufacturers : seq<PhoneTypeProvider.Root> -> seq<Manufacturer>,
        phones : seq<PhoneTypeProvider.Root>              
    ) = 
    inherit ApiController()

    [<Route("")>]
    member this.Get () =
        getManufacturers phones
