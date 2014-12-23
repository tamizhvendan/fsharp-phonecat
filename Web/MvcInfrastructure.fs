namespace PhoneCat.Web

open PhoneCat.DataAccess
open PhoneCat.Domain
open PhoneCat.Web.Controllers
open System
open System.Web.Mvc

module MvcInfrastructure = 
  type CompositionRoot(phones : seq<PhoneTypeProvider.Root>) = 
    inherit DefaultControllerFactory() with
      override this.GetControllerInstance(requestContext, controllerType) = 
        if controllerType = typeof<HomeController> then 
            let homeController = new HomeController()
            homeController :> IController
        else if controllerType = typeof<PhoneController> then
            let phones' = phones |> Seq.map TypeProviders.ToCatalogPhone
            let phoneController = new PhoneController(phones')
            phoneController :> IController
        else
            raise <| ArgumentException((sprintf "Unknown controller type requested: %A" controllerType))
