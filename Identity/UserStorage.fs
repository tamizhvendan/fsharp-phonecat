namespace Identity

open Microsoft.AspNet.Identity.EntityFramework
open Microsoft.AspNet.Identity
open Rop
open UserValidation

[<AutoOpen>]
module UserStorage =    

  let createUserManager () =
    let userStore = new UserStore<User>(new UserDbContext())
    let userManager = new UserManager<User>(userStore) 
    let userValidator = new UserValidator<User>(userManager)
    userValidator.AllowOnlyAlphanumericUserNames <- false   
    userManager.UserValidator <- userValidator
    userManager
  

  let private createUser' (userManager : UserManager<User>) createUserRequest =
    try
      let user = new User(Name = createUserRequest.Name, 
                          UserName = createUserRequest.Email, 
                          Email = createUserRequest.Email)
      let identityResult = userManager.Create(user, createUserRequest.Password)
      if identityResult.Succeeded then
        Success createUserRequest
      else
        let errors = String.concat "," identityResult.Errors
        Failure {Property = ""; Message = errors}
    with
      | _ ->
        Failure {Property = ""; Message = "An unexpected error happened while creating new user"}

  let createUser (userManager : UserManager<User>) =
      validateCreateUserRequest userManager >>= createUser' userManager
        

