namespace PhoneCat.Web
open System.Web.Mvc
open PhoneCat.Domain
open PhoneCat.Domain.ShoppingCart

type CheckoutController
  (
      getPhones : seq<string> -> seq<Phone>,
      cart : Cart
  ) = 
  inherit Controller ()

  member this.ViewCart () =
    cart
    |> getItems 
    |> getPhones
    |> this.View


    
