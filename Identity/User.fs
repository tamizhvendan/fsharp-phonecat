namespace Identity

open Microsoft.AspNet.Identity.EntityFramework
open Microsoft.AspNet.Identity
open System.Threading.Tasks

[<AllowNullLiteral>]
type User() = 
  inherit IdentityUser()
  member val Name = "" with get, set

type UserDbContext() =
  inherit IdentityDbContext<User>("IdentityConnection")

type ValidationError = {Property : string; Message : string}

type ValidationResult<'a> = 
  | Success of 'a
  | Failure of ValidationError

[<AutoOpen>]
module Users =   
  
  let private createUserValidator userManager =
    let userValidator = new UserValidator<User>(userManager)
    userValidator.AllowOnlyAlphanumericUserNames <- false
    userValidator

  let createUserManager () =
    let userManager = new UserManager<User>(new UserStore<User>(new UserDbContext()))    
    userManager.UserValidator <- (createUserValidator userManager)
    userManager

  let isEmailAddrAlreadyExists (userManager : UserManager<User>) emailAddr =
    let user = userManager.FindByName(emailAddr)
    user <> null