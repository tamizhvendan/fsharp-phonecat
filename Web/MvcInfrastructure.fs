namespace PhoneCat.Web

open PhoneCat.DataAccess
open PhoneCat.Domain
open PhoneCat.Web.Controllers
open System
open System.Web.Mvc
open Microsoft.AspNet.Identity
open Microsoft.AspNet.Identity.EntityFramework
open Identity

module MvcInfrastructure =      

  type CompositionRoot(phones : seq<PhoneTypeProvider.Root>) =          
    inherit DefaultControllerFactory() with
      override this.GetControllerInstance(requestContext, controllerType) = 
        let anonymousId = requestContext.HttpContext.Request.AnonymousID

        if controllerType = typeof<HomeController> then 
          let homeController = new HomeController()
          homeController :> IController
        else if controllerType = typeof<PhoneController> then          
          let phones' = phones |> Seq.map TypeProviders.ToCatalogPhone
          let observer = PhoneViewTracker.observePhonesViewed (requestContext.HttpContext.Request.AnonymousID)
          let phoneController = new PhoneController(phones')
          let subscription = phoneController.Subscribe observer                    
          phoneController :> IController
        else if controllerType = typeof<ManufacturerController> then
          let getPhonesByManufactuerName = phones |> Seq.map TypeProviders.ToPhone |> Phones.getPhonesOfManufacturer
          let manufacturerController = new ManufacturerController(getPhonesByManufactuerName)
          manufacturerController :> IController
        else if controllerType = typeof<AuthenticationController> then
          let authenticationController = new AuthenticationController(createUserManager())
          authenticationController :> IController
        else if controllerType = typeof<CheckoutController> then
          let anonymousID = requestContext.HttpContext.Request.AnonymousID
          let shoppingCart = CartStorage.getOrCreate anonymousID
          let getPhonesByIds = phones |> Seq.map TypeProviders.ToPhone |> Phones.getPhonesByIds
          let checkoutController = new CheckoutController(getPhonesByIds, shoppingCart)
          checkoutController :> IController
        else if controllerType = typeof<OrderController> then
          let anonymousID = requestContext.HttpContext.Request.AnonymousID
          let shoppingCart = CartStorage.getOrCreate anonymousID
          let updateCart = CartStorage.update anonymousID
          let orderController = new OrderController(shoppingCart, OrderStorage.placeOrder, updateCart)
          orderController :> IController
        else
          raise <| ArgumentException((sprintf "Unknown controller type requested: %A" controllerType))    
      