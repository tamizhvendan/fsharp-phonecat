namespace PhoneCat.Domain

[<AutoOpen>]
module Phones =    
    
    let getTopSellingPhones (phonesSold : seq<PhoneSold>) phoneCount (phones : seq<Phone>) = 
        phonesSold
        |> Seq.map (fun p -> p.Quantity, (phones |> Seq.find (fun p' -> p'.Id = p.Id)))
        |> Seq.sortBy (fun x -> -(fst x))
        |> Seq.take phoneCount
        |> Seq.map snd

    let getManufacturers (phones : seq<Phone>) = 
        phones
        |> Seq.map (fun p -> ManufacturerName.ToManufacturerName p.Name, p)
        |> Seq.groupBy fst
        |> Seq.map (fun (key, values) -> 
               { Name = key
                 Phones = (values |> Seq.map snd) })
