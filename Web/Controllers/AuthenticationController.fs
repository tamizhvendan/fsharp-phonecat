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

type AuthenticationController (userManager : UserManager<User>) = 
  inherit Controller()

  let signin (userManager : UserManager<User>) (request : HttpRequestBase) user  =
    let identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie)
    let authManager = request.GetOwinContext().Authentication
    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name))
    authManager.SignIn(identity)

  let tryFindUser (userManager : UserManager<User>) email password  =
    let user = userManager.Find(email, password)
    if user <> null then Some user else None

  member this.Login() = this.View()
    
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

  member this.Register() = this.View()    

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  member this.Register(registerViewModel : RegisterViewModel) : ActionResult =
    let userDto : UserDto = {
      Name = registerViewModel.Name; 
      Email = registerViewModel.Email; 
      Password = registerViewModel.Password 
    }
    match Users.createUser userManager userDto with
    | Failure error ->
      this.ModelState.AddModelError(error.Property, error.Message)
      this.View(registerViewModel) :> ActionResult
    | Success user ->                                
      signin userManager base.Request user      
      this.RedirectToAction("Index", "Home") :> ActionResult     

  override this.Dispose(disposing) =
    if disposing then
      userManager.Dispose()
    base.Dispose(disposing)


