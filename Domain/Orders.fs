namespace PhoneCat.Domain
open ShoppingCart

module Orders =

  type OrderRequest = {
    UserId : string
    ProductIds : ProductId seq
  }

  type OrderConfirmed = {
    OrderId : string
    UserId : string
    ProductIds : ProductId seq
  }

