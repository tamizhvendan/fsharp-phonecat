namespace PhoneCat.Web.Controllers

open System.Web.Mvc
open System.ComponentModel.DataAnnotations
open System.Web
open System.Security.Claims
open Microsoft.AspNet.Identity
open Identity
open Microsoft.AspNet.Identity
open System.Web.Helpers.Claims

[<CLIMutable>]
type LoginViewModel = {
  Email : string
  Password : string
}

[<CLIMutable>]
type RegisterViewModel = {
  Name : string
  Email : string
  Password : string
}

type LoginResult = 
  | Success of ClaimsIdentity
  | Failure

type AuthenticationController (userManager : UserManager<User>) = 
  inherit Controller()

  let login email password (userManager : UserManager<User>) =
    let user = userManager.Find(email, password)
    if user = null then
      Failure
    else
      let identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie)
      Success identity

  member this.Login() =     
    this.View()

  
  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Login(loginViewModel : LoginViewModel) : ActionResult =

    match login loginViewModel.Email loginViewModel.Password userManager with
    | Failure ->
      this.ModelState.AddModelError("", "Invalid Email or Password")
      this.View(loginViewModel) :> ActionResult
    | Success identity ->
      let authManager = base.Request.GetOwinContext().Authentication
      authManager.SignIn(identity)
      this.RedirectToAction("Index", "Home") :> ActionResult   
    

  member this.Logout() =
    let authManager = base.Request.GetOwinContext().Authentication
    authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
    this.RedirectToAction("Index", "Home")


  member this.Register() =
    this.View()

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Register(registerViewModel : RegisterViewModel) : ActionResult =
    this.View(registerViewModel) :> ActionResult 

  override this.Dispose(disposing) =
    if disposing then
      userManager.Dispose()
    base.Dispose(disposing)


