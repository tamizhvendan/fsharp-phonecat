namespace PhoneCat.Web

open System
open System.Web.Http.Dispatcher
open System.Web.Http.Controllers
open PhoneCat.Web.Controllers
open PhoneCat.DataAccess
open PhoneCat.Domain

open System.Web

module Infrastructure =           
    
    type CompositionRoot 
        (
          phones : seq<PhoneTypeProvider.Root>,
          phoneIndexes : seq<PhoneIndexTypeProvider.Root>  
        ) =                
        interface IHttpControllerActivator with           

            member this.Create(request, controllerDescriptor, controllerType) =
                
                let phones' = phones |> Seq.map TypeProviders.ToPhone
                let phoneIndexes' = phoneIndexes |> Seq.map TypeProviders.ToPhoneIndex
                let catalogPhones = phones |> Seq.map TypeProviders.ToCatalogPhone

                if controllerType = typeof<PromotionsController> then
                  let promotionsController = new PromotionsController(getPromotions, phoneIndexes')
                  promotionsController :> IHttpController
            
                else if controllerType = typeof<PhonesController> then                    
                  let getTopSellingPhones = Phones.getTopSellingPhones (InMemoryInventory.getPhonesSold())                    
                  let phonesController = new PhonesController(getTopSellingPhones, phones', catalogPhones)                    
                  phonesController :> IHttpController           
            
                else if controllerType = typeof<ManufacturersController> then
                  let manufacturersController = new ManufacturersController(getManufacturerNames, phones')
                  manufacturersController :> IHttpController 

                else if controllerType = typeof<ShoppingCartController> then
                  let session = HttpContext.Current.Session
                  let shoppingCart = ShoppingCartController.GetShoppingCart(session)
                  let shoppingCartController = new ShoppingCartController(shoppingCart)
                  shoppingCartController :> IHttpController
                else
                    raise <| ArgumentException((sprintf "Unknown controller type requested: %A" controllerType))    


    