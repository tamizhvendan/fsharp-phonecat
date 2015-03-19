﻿namespace PhoneCat.DataAccess
open PhoneCat.Domain.ShoppingCart
open System.Collections.Generic

module CartStorage =  
  
  let private inMemoryStorage = 
    new Dictionary<string, Cart>()

  let create anonymousId cart = 
    inMemoryStorage.Add(anonymousId, cart)
    cart

  let update anonymousId cart =
    inMemoryStorage.Remove(anonymousId) |> ignore
    create anonymousId cart

  let get anonymousId =
    match inMemoryStorage.ContainsKey(anonymousId) with
    | true -> Some inMemoryStorage.[anonymousId] 
    | _ -> None
