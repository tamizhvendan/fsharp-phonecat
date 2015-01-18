namespace PhoneCat.Web.Controllers


open System.Web.Http
open PhoneCat.Domain
open System.Net.Http
open System.Net

type Error = { message : string }

[<RoutePrefix("api/phones")>]
type PhonesController
    (
        getTopSellingPhones : int -> seq<Phone> -> seq<Phone>,
        phones : seq<Phone>,
        catalogPhones : seq<Catalog.Phone>              
    ) = 
    inherit ApiController()

    [<Route("topselling")>]
    member this.GetTopSelling () =
      getTopSellingPhones 3 phones

    
    [<HttpGet>]
    [<Route("search")>]
    member this.SearchPhones (q : string) =
      let parserResult = SearchParser.parseFilter q
      match parserResult with
      | Choice1Of2 filters -> 
        let filteredPhones = Search.searchPhones catalogPhones filters
        base.Request.CreateResponse(HttpStatusCode.OK, filteredPhones)
      | Choice2Of2 errMsg ->
        base.Request.CreateResponse(HttpStatusCode.OK, { message = errMsg })
         

          
      
