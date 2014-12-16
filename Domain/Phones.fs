namespace PhoneCat.Domain

[<AutoOpen>]
module Phones =    
    
    let getTopSellingPhones (phonesSold : seq<PhoneSold>) (phones : seq<PhoneTypeProvider.Root>) = 
        phonesSold
        |> Seq.map (fun p -> p.Quantity, (phones |> Seq.find (fun p' -> p'.Id = p.Id)))
        |> Seq.sortBy (fun x -> -(fst x))
        |> Seq.take 4
        |> Seq.map snd
        |> Seq.map Phone.ToPhone
    
    let getManufacturers (phones : seq<PhoneTypeProvider.Root>) = 
        phones
        |> Seq.map (fun p -> ManufacturerName.ToManufacturerName p.Name, p)
        |> Seq.groupBy fst
        |> Seq.map (fun (key, values) -> 
               { Name = key
                 Phones = (values |> Seq.map snd |> Seq.map Phone.ToPhone) })
