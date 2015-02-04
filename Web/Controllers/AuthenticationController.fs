﻿namespace PhoneCat.Web.Controllers

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

type AuthenticationController (userManager : UserManager<User>) = 
  inherit Controller()

  let signin (userManager : UserManager<User>) (request : HttpRequestBase) user  =
    let identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie)
    let authManager = request.GetOwinContext().Authentication
    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name))
    authManager.SignIn(identity)

  let tryFindUser (userManager : UserManager<User>) email password  =
    let user = userManager.Find(email, password)
    if user = null then
      None
    else
      Some user

  member this.Login() =     
    this.View()

  
  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Login(loginViewModel : LoginViewModel) : ActionResult =

    match tryFindUser userManager loginViewModel.Email loginViewModel.Password  with
    | None ->
      this.ModelState.AddModelError("", "Invalid Email or Password")
      this.View(loginViewModel) :> ActionResult
    | Some user ->
      signin userManager base.Request user
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
      signin userManager base.Request user      
      this.RedirectToAction("Index", "Home") :> ActionResult
    else
      userCreateResult.Errors
      |> Seq.iter(fun err -> this.ModelState.AddModelError("", err))
      this.View(registerViewModel) :> ActionResult 

  override this.Dispose(disposing) =
    if disposing then
      userManager.Dispose()
    base.Dispose(disposing)


