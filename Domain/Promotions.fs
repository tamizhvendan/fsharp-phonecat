namespace PhoneCat.Domain
[<AutoOpen>]
module Promotions = 

    type PromotionPhone = {
        Id : string
        ImageUrl : string
        Name : string
    }


    let getPromotions (phoneIndexes : seq<PhoneIndexTypeProvider.Root>) = 
        phoneIndexes 
        |> Seq.filter (fun phone -> phone.Age < 3) 
        |> Seq.map (fun phone -> {Id = phone.Id; Name = phone.Name; ImageUrl = phone.ImageUrl})
