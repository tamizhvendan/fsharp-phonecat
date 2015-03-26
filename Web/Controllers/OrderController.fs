namespace PhoneCat.Web
open System.Web.Mvc
open PhoneCat.Domain.ShoppingCart
open PhoneCat.Domain.Orders
open System.Security.Claims

type OrderController
  (
    cart : Cart,
    placeOrder : OrderRequest -> OrderConfirmed,
    updateCart : Cart -> Cart
  ) =
  inherit Controller ()

  
    [<Authorize>]
    member this.Checkout () =
      let identity = base.User.Identity :?> ClaimsIdentity
      let userId = identity.FindFirst(ClaimTypes.Name).Value
      let orderConfirmed = placeOrder { UserId = userId; ProductIds = getItems cart}
      updateCart Cart.Empty |> ignore
      this.View(orderConfirmed);
    
  


