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

  let createIdentity (userManager : UserManager<User>) user =
    userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie)

  let signin (request : HttpRequestBase) (identity : ClaimsIdentity) =
    let authManager = request.GetOwinContext().Authentication
    authManager.SignIn(identity)

  let tryGetIdentity (userManager : UserManager<User>) email password  =
    let user = userManager.Find(email, password)
    if user = null then
      Failure
    else
      Success (createIdentity userManager user)

  member this.Login() =     
    this.View()

  
  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Login(loginViewModel : LoginViewModel) : ActionResult =

    match tryGetIdentity userManager loginViewModel.Email loginViewModel.Password  with
    | Failure ->
      this.ModelState.AddModelError("", "Invalid Email or Password")
      this.View(loginViewModel) :> ActionResult
    | Success identity ->
      signin base.Request identity
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
    
    let user = new User(Name = registerViewModel.Name, UserName = registerViewModel.Email , Email = registerViewModel.Email)
    let userCreateResult = userManager.Create(user, registerViewModel.Password)
    if (userCreateResult.Succeeded) then
      user
      |> createIdentity userManager
      |> signin base.Request
      this.RedirectToAction("Index", "Home") :> ActionResult
    else
      userCreateResult.Errors
      |> Seq.iter(fun err -> this.ModelState.AddModelError("", err))
      this.View(registerViewModel) :> ActionResult 

  override this.Dispose(disposing) =
    if disposing then
      userManager.Dispose()
    base.Dispose(disposing)


