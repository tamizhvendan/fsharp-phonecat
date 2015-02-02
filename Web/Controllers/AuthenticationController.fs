namespace PhoneCat.Web.Controllers

open System.Web.Mvc
open System.ComponentModel.DataAnnotations
open System.Web
open System.Security.Claims
open Microsoft.AspNet.Identity

[<CLIMutable>]
type LoginViewModel = {
  Email : string
  Password : string
}

type AuthenticationController () = 
  inherit Controller()
  member this.Login() =     
    this.View()

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Login(loginViewModel : LoginViewModel) : ActionResult =
    
    if (loginViewModel.Email = "a@b.com" && loginViewModel.Password = "barbar") then
      let authManager = base.Request.GetOwinContext().Authentication
      let claim = new Claim(ClaimTypes.Name, "tam")
      let identity = new ClaimsIdentity([claim], DefaultAuthenticationTypes.ApplicationCookie)
      authManager.SignIn(identity);
      this.RedirectToAction("Index", "Home") :> ActionResult
    else  
      this.View(loginViewModel) :> ActionResult

  member this.Logout() =
    let authManager = base.Request.GetOwinContext().Authentication
    authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
    this.RedirectToAction("Index", "Home")

