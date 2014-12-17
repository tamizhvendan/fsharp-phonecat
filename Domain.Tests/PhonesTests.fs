namespace PhoneCat.Domain.Phones.Tests
open NUnit.Framework
open FsUnit
open PhoneCat.Domain

module PhonesTests =

    let phones = [
            { Id = "motorola-xoom-with-wi-fi"; Name = "Motorola Xoom"; Description = "foo"; ImageUrl = "bar"}
            { Id = "motorola-atrix-4g"; Name = "Motorola Atrix 4G"; Description = "foo"; ImageUrl = "bar"}
            { Id = "nexus-s"; Name = "Nexus S"; Description = "foo"; ImageUrl = "bar"}
            { Id = "samsung-galaxy-tab"; Name = "Samsung Galaxy Tab"; Description = "foo"; ImageUrl = "bar"}
        ]
    
    [<Test>]
    let `` getTopSellingPhones returns phones with large quantity sold `` () =
        let phonesSold = [
            { Id = "motorola-xoom-with-wi-fi"; Quantity = 10}
            { Id = "motorola-atrix-4g"; Quantity = 24}
            { Id = "nexus-s"; Quantity = 4}
            { Id = "samsung-galaxy-tab"; Quantity = 41}
        ]      
        
        let topSellingPhones = Phones.getTopSellingPhones phonesSold 3 phones

        topSellingPhones |> should equal [phones.Item 3; phones.Item 1; phones.Item 0]


    [<Test>]
    let `` getManufacturerNames returns manufacturerNames for the given phones `` () =
        
        let actualManufacturerNames = Phones.getManufacturerNames phones

        actualManufacturerNames 
        |> should equal [ManufacturerName.Motorola; ManufacturerName.Motorola; ManufacturerName.Samsung; ManufacturerName.Samsung]
