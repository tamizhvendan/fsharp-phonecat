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

  let getItems cart =
    match cart with
    | Empty -> Seq.empty
    | Active items -> items |> List.toSeq