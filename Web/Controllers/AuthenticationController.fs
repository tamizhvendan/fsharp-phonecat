namespace PhoneCat.Web.Controllers

open System.Web.Mvc
open System.ComponentModel.DataAnnotations

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
  member this.Login(loginViewModel : LoginViewModel) =
    this.RedirectToAction("Index", "Home")

