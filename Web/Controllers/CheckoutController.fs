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
      let phones = 
          match cart with
          | Empty -> Seq.empty
          | Active xs -> getPhones xs
            
      this.View(phones)

    
