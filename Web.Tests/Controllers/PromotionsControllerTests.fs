namespace PhoneCat.Web.Tests

open NUnit.Framework
open FsUnit
open PhoneCat.Domain
open Web.Controllers

module PromotionsControllerTests =
    
    let phoneIndexes = 
        [
            { Id = "motorola-xoom-with-wi-fi"; Name = "Motorola Xoom"; Age = 0; ImageUrl = "bar"}
            { Id = "motorola-atrix-4g"; Name = "Motorola Atrix 4G"; Age = 1; ImageUrl = "bar"}
            { Id = "nexus-s"; Name = "Nexus S"; ImageUrl = "foo"; Age = 2}
            { Id = "samsung-galaxy-tab"; Name = "Samsung Galaxy Tab"; Age = 3; ImageUrl = "bar"}
        ]


    [<Test>]
    let `` Get returns Promotion Phones for the given Phone Indexes `` () =
        
        let sut = new PromotionsController(getPromotions, phoneIndexes)
        let expectedPromotionPhones : list<PromotionPhone> = [
            { Id = "motorola-xoom-with-wi-fi"; Name = "Motorola Xoom"; ImageUrl = "bar"}
            { Id = "motorola-atrix-4g"; Name = "Motorola Atrix 4G"; ImageUrl = "bar"}
            { Id = "nexus-s"; Name = "Nexus S"; ImageUrl = "foo"}
        ]

        let promotionPhones = sut.Get()

        promotionPhones |> Seq.length |> should equal 3
        promotionPhones |> should equal expectedPromotionPhones

