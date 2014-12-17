namespace Web.Controllers

open System.Web.Http
open PhoneCat.Domain

type ManufacturerViewModel =
    {
        Name : string
    }


[<RoutePrefix("api/manufacturers")>]
type ManufacturersController
    (
        getManufacturers : seq<Phone> -> seq<Manufacturer>,
        phones : seq<Phone>              
    ) = 
    inherit ApiController()

    [<Route("")>]
    member this.Get () =
        getManufacturers phones
        |> Seq.map (fun p -> {Name = ManufacturerName.ToString p.Name})