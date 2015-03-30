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

  let (|HomeController|_|) type' =
    if type' = typeof<HomeController> then
      let homeController = new HomeController()
      Some (homeController :> IController)
    else
      None
  
  let (|PhoneController|_|) phones anonymousId type' =
    if type' = typeof<PhoneController> then          
      let phones' = phones |> Seq.map TypeProviders.ToCatalogPhone
      let observer = PhoneViewTracker.observePhonesViewed anonymousId
      let phoneController = new PhoneController(phones')
      let subscription = phoneController.Subscribe observer                    
      Some (phoneController :> IController)
    else
      None

  let (|ManufacturerController|_|) phones type' =
    if type' = typeof<ManufacturerController> then
      let getPhonesByManufactuerName = phones |> Seq.map TypeProviders.ToPhone |> Phones.getPhonesOfManufacturer
      let manufacturerController = new ManufacturerController(getPhonesByManufactuerName)
      Some (manufacturerController :> IController)
    else
      None

  let (|AuthenticationController|_|) type' =
    if type' = typeof<AuthenticationController> then
      let authenticationController = new AuthenticationController(createUserManager())
      Some (authenticationController :> IController)
    else
      None
      
  let (|CheckoutController|_|) phones anonymousId type' =
    if type' = typeof<CheckoutController> then
      let shoppingCart = CartStorage.getOrCreate anonymousId
      let getPhonesByIds = phones |> Seq.map TypeProviders.ToPhone |> Phones.getPhonesByIds
      let checkoutController = new CheckoutController(getPhonesByIds, shoppingCart)
      Some (checkoutController :> IController)
    else
      None
  
  let (|OrderController|_|) anonymousId type'  =
    if type' = typeof<OrderController> then
      let shoppingCart = CartStorage.getOrCreate anonymousId
      let updateCart = CartStorage.update anonymousId
      let orderController = new OrderController(shoppingCart, OrderStorage.placeOrder, updateCart)
      Some (orderController :> IController)
    else
      None

  type CompositionRoot(phones : seq<PhoneTypeProvider.Root>) =          
    inherit DefaultControllerFactory() with
      override this.GetControllerInstance(requestContext, controllerType) = 
        let anonymousId = requestContext.HttpContext.Request.AnonymousID
        match controllerType with
        | HomeController homeController -> homeController
        | AuthenticationController authenticationController -> authenticationController  
        | ManufacturerController phones manufacturesController -> manufacturesController      
        | PhoneController phones anonymousId phoneController -> phoneController
        | OrderController anonymousId orderController -> orderController
        | CheckoutController phones anonymousId checkoutController -> checkoutController
        | _ -> raise <| ArgumentException((sprintf "Unknown controller type requested: %A" controllerType))
