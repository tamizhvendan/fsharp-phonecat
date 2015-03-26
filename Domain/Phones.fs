namespace PhoneCat.Domain

[<AutoOpen>]
module Phones =    
    
  let getTopSellingPhones (phonesSold : seq<PhoneSold>) phoneCount (phones : seq<Phone>) = 
    phonesSold
    |> Seq.map (fun p -> p.Quantity, (phones |> Seq.find (fun p' -> p'.Id = p.Id)))
    |> Seq.sortBy (fun x -> -(fst x))
    |> Seq.take phoneCount
    |> Seq.map snd

  let getManufacturerNames (phones : seq<Phone>) = 
    phones
    |> Seq.map (fun p -> ManufacturerName.ToManufacturerName p.Name)

  let getPhoneById (phones : seq<Phone>) phoneId =
    phones |> Seq.find (fun p -> p.Id = phoneId)

  let contains x = Seq.exists ((=) x)

  let getPhonesByIds (phones : seq<Phone>) phoneIds =  
    phones |> Seq.filter (fun p -> contains p.Id phoneIds)

  let getPhonesOfManufacturer (phones : seq<Phone>) (manufacturerName) =
    let phones' = 
      phones
      |> Seq.filter (fun p -> ManufacturerName.ToManufacturerName p.Name = manufacturerName)
    (manufacturerName, phones')
   

      
