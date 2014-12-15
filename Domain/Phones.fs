namespace PhoneCat.Domain

[<AutoOpen>]
module Phones = 
    
    type TopSellingPhone = {
        Id : string
        Name : string
        Description : string
        ImageUrl : string
    }
    

    let getTopSellingPhones (phonesSold : seq<PhoneSold>)(phones : seq<PhoneTypeProvider.Root>)  = 
        phonesSold
        |> Seq.map (fun p -> p.Quantity, (phones |> Seq.find (fun p' -> p'.Id = p.Id)))
        |> Seq.sortBy (fun x -> (~-) (fst x))
        |> Seq.take 4
        |> Seq.map snd
        |> Seq.map (fun x -> { Id = x.Id; Name = x.Name; Description = x.Description; ImageUrl = x.Images.[0] })


