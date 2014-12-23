namespace PhoneCat.Web.Tests

open NUnit.Framework
open FsUnit
open PhoneCat.Domain
open PhoneCat.Web.Controllers

module PhonesControllerTests =

    [<Test>]
    let `` GetTopSelling returns top 3 selling phones `` () =
        
        let phones = [
            { Id = "motorola-xoom-with-wi-fi"; Name = "Motorola Xoom"; Description = "foo"; ImageUrl = "bar"}
            { Id = "motorola-atrix-4g"; Name = "Motorola Atrix 4G"; Description = "foo"; ImageUrl = "bar"}
            { Id = "nexus-s"; Name = "Nexus S"; Description = "foo"; ImageUrl = "bar"}
            { Id = "samsung-galaxy-tab"; Name = "Samsung Galaxy Tab"; Description = "foo"; ImageUrl = "bar"}
        ]
        let getTopSellingPhones count phones' =
            phones' |> Seq.take count
        let sut = new PhonesController(getTopSellingPhones, phones)

        let actualTopSellingPhones = sut.GetTopSelling()
        
        actualTopSellingPhones |> should equal (phones |> Seq.take 3)       

