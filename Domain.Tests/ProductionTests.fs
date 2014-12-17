namespace PhoneCat.Domain.Production.Tests

open NUnit.Framework
open FsUnit
open System
open PhoneCat.Domain

module ProductionTests =       
    
    [<Test>]
    [<TestCaseSource("ToManufactureNameTestCases")>]
    let `` Create Manufacture Name from string `` (str, expectedManufactureName) =
        let actualManufacturerName = ManufacturerName.ToManufacturerName str
        
        actualManufacturerName |> should equal expectedManufactureName

    let ToManufactureNameTestCases : Object [] =
        let samsung : Object [] = [| "Samsung"; ManufacturerName.Samsung|]
        let samsung' : Object [] = [| "nexus"; ManufacturerName.Samsung|]
        let tMobile : Object [] = [| "t-mobIle"; ManufacturerName.TMobile|]
        let lg : Object [] = [| "LG SmartPhone"; ManufacturerName.LG|]
        let motorola : Object [] = [| "All New Motorola"; ManufacturerName.Motorola|]
        let dell : Object [] = [| "Dell Streak"; ManufacturerName.Dell|]
        let sanyo : Object [] = [| "sanYo"; ManufacturerName.Sanyo|]
        let unknown : Object [] = [| "fooBaz"; ManufacturerName.Unknown|]

        [| samsung; samsung'; tMobile; lg; motorola; dell; sanyo; unknown |]
        
    [<Test>]
    [<TestCaseSource("ManufactureNameToStringTestCases")>]
    let `` Create string from Manufacture Name `` (manufactureName, expectedString) =
        let actualString = ManufacturerName.ToString manufactureName
        
        actualString |> should equal expectedString

    let ManufactureNameToStringTestCases : Object [] =
        let samsung : Object [] = [| ManufacturerName.Samsung; "Samsung"|]
        let tMobile : Object [] = [| ManufacturerName.TMobile; "T-Mobile"|]
        let lg : Object [] = [|ManufacturerName.LG; "LG"|]
        let motorola : Object [] = [|ManufacturerName.Motorola; "Motorola"|]
        let dell : Object [] = [| ManufacturerName.Dell; "Dell"|]
        let sanyo : Object [] = [| ManufacturerName.Sanyo; "Sanyo"|]
        let unknown : Object [] = [| ManufacturerName.Unknown; "Unknown"|]
        
        [| samsung; tMobile; lg; motorola; dell; sanyo; unknown |]
    
    
    

