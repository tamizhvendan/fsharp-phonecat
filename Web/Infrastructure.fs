namespace PhoneCat.Web

open System
open System.Web.Http.Dispatcher
open System.Web.Http.Controllers
open Web.Controllers
open PhoneCat.DataAccess
open PhoneCat.Domain

module Infrastructure =       
    
    type CompositionRoot() =
        interface IHttpControllerActivator with
            member this.Create(request, controllerDescriptor, controllerType) =

                if controllerType = typeof<PromotionsController> then
                    let promotionsController = new PromotionsController(PhoneCat.Domain.Promotions.getPromotions, GitHubRepository.getPhoneIndexes())
                    promotionsController :> IHttpController
            
                else if controllerType = typeof<PhonesController> then
                    let getTopSellingPhones = Phones.getTopSellingPhones (InMemoryInventory.getPhonesSold())
                    let phones = GitHubRepository.getPhones()
                    let phonesController = new PhonesController(getTopSellingPhones, phones) 
                    phonesController :> IHttpController           
            
                else
                    raise <| ArgumentException((sprintf "Unknown controller type requested: %A" controllerType))    


    