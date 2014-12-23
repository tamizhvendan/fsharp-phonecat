namespace PhoneCat.DataAccess.Tests

open NUnit.Framework
open FsUnit
open PhoneCat.Domain
open PhoneCat.Domain.Measures
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


    [<Test>]
    let `` Create Catalog.Phone from PhoneTypeProvider Root Type`` () =
        
        let phoneTypeProviderRoot = PhoneTypeProvider.GetSample()
        
        let phone = ToCatalogPhone phoneTypeProviderRoot

        phone.Android.OS |> should equal phoneTypeProviderRoot.Android.Os
        phone.Android.UI |> should equal phoneTypeProviderRoot.Android.Ui
        phone.Camera.Features |> should equal phoneTypeProviderRoot.Camera.Features
        phone.Camera.Primary |> should equal phoneTypeProviderRoot.Camera.Primary
        phone.Description |> should equal phoneTypeProviderRoot.Description
        phone.Display.ScreenResolution |> should equal phoneTypeProviderRoot.Display.ScreenResolution
        phone.Display.ScreenSize |> should equal 3.5<inch>
        phone.Display.TouchScreen |> should equal phoneTypeProviderRoot.Display.TouchScreen
        phone.Name |> should equal phoneTypeProviderRoot.Name
        phone.Weight |> should equal 105.0<g>
        phone.Storage.Flash |> should equal 130.<MB>
        phone.Storage.Ram |> should equal 256.<MB>
