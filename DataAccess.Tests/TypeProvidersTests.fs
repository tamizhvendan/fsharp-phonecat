namespace PhoneCat.DataAccess.Tests

open NUnit.Framework
open FsUnit
open PhoneCat.Domain
open PhoneCat.DataAccess.TypeProviders


module TypeProvidersTests = 

    [<Test>]
    let `` Create Phone from PhoneTypeProvider Root Type``() = 
        
        let phoneTypeProviderRoot = PhoneTypeProvider.GetSample()
        
        let phone = ToPhone phoneTypeProviderRoot
        
        phone.Id |> should equal phoneTypeProviderRoot.Id
        phone.Name |> should equal phoneTypeProviderRoot.Name
        phone.Description |> should equal phoneTypeProviderRoot.Description
        phone.ImageUrl |> should equal phoneTypeProviderRoot.Images.[0]


    [<Test>]
    let `` Create PhoneIndex from PhoneIndexTypeProvider Root Type``() = 

        let phoneIndexTypeProviderRoot = PhoneIndexTypeProvider.GetSamples() |> Seq.head
        
        let phoneIndex = ToPhoneIndex phoneIndexTypeProviderRoot
        
        phoneIndex.Id |> should equal phoneIndexTypeProviderRoot.Id
        phoneIndex.Name |> should equal phoneIndexTypeProviderRoot.Name
        phoneIndex.Age |> should equal phoneIndexTypeProviderRoot.Age
        phoneIndex.ImageUrl |> should equal phoneIndexTypeProviderRoot.ImageUrl
