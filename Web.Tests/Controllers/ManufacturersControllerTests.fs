namespace PhoneCat.Web.Tests

open NUnit.Framework
open FsUnit
open PhoneCat.Domain
open PhoneCat.Web.Controllers

module ManufacturersControllerTests =
    
    let phones = 
        [
            { Id = "motorola-xoom-with-wi-fi"; Name = "Motorola Xoom"; Description = "foo"; ImageUrl = "bar"}
            { Id = "motorola-atrix-4g"; Name = "Motorola Atrix 4G"; Description = "foo"; ImageUrl = "bar"}
            { Id = "nexus-s"; Name = "Nexus S"; Description = "foo"; ImageUrl = "bar"}
            { Id = "samsung-galaxy-tab"; Name = "Samsung Galaxy Tab"; Description = "foo"; ImageUrl = "bar"}
        ]

    [<Test>]
    let `` Get returns manufacturer names of given phones `` () =
        
        let sut = new ManufacturersController(getManufacturerNames, phones)

        let manufacturerNameViewModels = sut.Get()

        manufacturerNameViewModels |> Seq.length |> should equal 2
        manufacturerNameViewModels |> Seq.map (fun vm -> vm.Name) |> should equal ["Motorola";"Samsung"]
