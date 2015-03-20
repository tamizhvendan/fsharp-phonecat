namespace PhoneCat.Web.Controllers

open System.Web.Http
open System.Web.SessionState
open PhoneCat.Domain.ShoppingCart

[<RoutePrefix("api/cart")>]
type ShoppingCartController 
  (
    cart : Cart,
    updateCart : Cart -> Cart
  ) =
  inherit ApiController () 

  [<Route("")>]
  member this.Get() = cart

  [<HttpPost>]
  [<Route("add")>]
  member this.AddItem([<FromBody>]productId : string) = 
    addItem cart productId |> updateCart