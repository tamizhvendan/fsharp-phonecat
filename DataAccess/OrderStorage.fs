namespace PhoneCat.DataAccess
open System
open PhoneCat.Domain.Orders

module OrderStorage =
  
  let placeOrder (orderRequest : OrderRequest) =
    let orderId = Guid.NewGuid().ToString()
    {OrderId = orderId; UserId = orderRequest.UserId; ProductIds = orderRequest.ProductIds}

