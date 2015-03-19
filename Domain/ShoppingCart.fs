namespace PhoneCat.Domain

module ShoppingCart =    

  type ProductId = string
    
  type Cart =
    | Empty
    | Active of ProductId List

  let addItem cart productId =
    match cart with
    | Empty -> Active [productId]
    | Active items -> Active (productId :: items)

  let removeItem cart productId =
    match cart with
    | Empty -> cart
    | Active items ->
      let newItems = items |> List.filter (fun item -> item <> productId)
      match newItems with
      | [] -> Empty
      | _ -> Active newItems
      
