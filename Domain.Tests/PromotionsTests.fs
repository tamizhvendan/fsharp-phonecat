namespace PhoneCat.PromotionsTests
open NUnit.Framework
open FsUnit
open PhoneCat.Domain

module PromotionsTests =     

    [<Test>]
    let ``getPromotions returns phones with age less than 3 `` () =
        let phoneIndexes : seq<PhoneIndex> = 
            let concat str x =
                str + (string x)
            [0..5] |> Seq.map (fun n -> { Id = concat "id" n; Age = n; ImageUrl = concat "imageUrl" n; Name = concat "name" n})
        
        let expectedPromotionItems : seq<PromotionPhone> = seq {
            yield { Id = "id0"; ImageUrl = "imageUrl0"; Name = "name0" }
            yield { Id = "id1"; ImageUrl = "imageUrl1"; Name = "name1" }
            yield { Id = "id2"; ImageUrl = "imageUrl2"; Name = "name2" }
        }
        
        let promotionItems = Promotions.getPromotions phoneIndexes

        promotionItems |> should equal expectedPromotionItems