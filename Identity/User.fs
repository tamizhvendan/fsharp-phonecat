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


[<AutoOpen>]
module Users =   
  
  let createUserValidator userManager =
    let userValidator = new UserValidator<User>(userManager)
    userValidator.AllowOnlyAlphanumericUserNames <- false
    userValidator

  let createUserManager () =
    let userManager = new UserManager<User>(new UserStore<User>(new UserDbContext()))    
    userManager.UserValidator <- (createUserValidator userManager)
    userManager

  let isUserNameExists (userManager : UserManager<User>) userName =
    let user = userManager.FindByName(userName)
    user <> null

  let validate userManager user =
    let validator = createUserValidator userManager
    async {
      let! result = validator.ValidateAsync(user) |> Async.AwaitTask
      return result.Errors
    } |> Async.RunSynchronously
