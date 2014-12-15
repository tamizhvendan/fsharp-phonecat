namespace PhoneCat.Web

open System
open System.Web.Http.Dispatcher
open System.Web.Http.Controllers
open Web.Controllers
open PhoneCat.DataAccess
open PhoneCat.Domain

module Infrastructure =       
    
    type CompositionRoot() =
        member this.phones = GitHubRepository.getPhones()
        member this.phoneIndexes = GitHubRepository.getPhoneIndexes()
                
        interface IHttpControllerActivator with           

            member this.Create(request, controllerDescriptor, controllerType) =

                if controllerType = typeof<PromotionsController> then
                    let promotionsController = new PromotionsController(PhoneCat.Domain.Promotions.getPromotions, this.phoneIndexes)
                    promotionsController :> IHttpController
            
                else if controllerType = typeof<PhonesController> then
                    let getTopSellingPhones = Phones.getTopSellingPhones (InMemoryInventory.getPhonesSold())                    
                    let phonesController = new PhonesController(getTopSellingPhones, this.phones) 
                    phonesController :> IHttpController           
            
                else if controllerType = typeof<ManufacturersController> then
                    let manufacturersController = new ManufacturersController(getManufacturers, this.phones)
                    manufacturersController :> IHttpController 
                else
                    raise <| ArgumentException((sprintf "Unknown controller type requested: %A" controllerType))    


    