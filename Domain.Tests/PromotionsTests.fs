namespace PhoneCat.PromotionsTests
open NUnit.Framework
open FsUnit
open PhoneCat.Domain

module PromotionsTests =     

    [<Test>]
    let ``getPromotions returns phones with age less than 3 `` () =
        let phoneIndexes : seq<PhoneIndexTypeProvider.Root> = seq {
            yield new PhoneIndexTypeProvider.Root(0, "id0", "imageUrl0", "name0", "snippet0")
            yield new PhoneIndexTypeProvider.Root(1, "id1", "imageUrl1", "name1", "snippet1")
            yield new PhoneIndexTypeProvider.Root(2, "id2", "imageUrl2", "name2", "snippet2")
            yield new PhoneIndexTypeProvider.Root(3, "id3", "imageUrl3", "name3", "snippet3")
            yield new PhoneIndexTypeProvider.Root(4, "id4", "imageUrl4", "name4", "snippet4")
        }
        let expectedPromotionItems : seq<PromotionItem> = seq {
            yield { Id = "id0"; ImageUrl = "imageUrl0"; Name = "name0" }
            yield { Id = "id1"; ImageUrl = "imageUrl1"; Name = "name1" }
            yield { Id = "id2"; ImageUrl = "imageUrl2"; Name = "name2" }
        }
        
        let promotionItems = Promotions.getPromotions phoneIndexes

        promotionItems |> should equal expectedPromotionItems