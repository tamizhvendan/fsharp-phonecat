namespace PhoneCat.Web.Controllers

open System.Web.Http
open System.Web.SessionState
open PhoneCat.Domain.ShoppingCart

[<RoutePrefix("api/cart")>]
type ShoppingCartController (cart : ShoppingCart) =
  inherit ApiController ()
   
      //let cart = session.[]
      //()

  [<HttpPost>]
  [<Route("add")>]
  member this.AddItem(productId : string) = 
    productId
    

  static member GetShoppingCart (session : HttpSessionState) = 
    try 
      let cart = session.["Cart"] :?> ShoppingCart
      cart
    with
      | _ ->
        ShoppingCart.Empty
  


