namespace Identity

open Microsoft.AspNet.Identity.EntityFramework
open Microsoft.AspNet.Identity
open System
open System.Threading.Tasks
open System.Text.RegularExpressions

[<AllowNullLiteral>]
type User() = 
  inherit IdentityUser()
  member val Name = "" with get, set

type UserDbContext() =
  inherit IdentityDbContext<User>("IdentityConnection")

type Error = {Property : string; Message : string}

type UserDto = {Name: string; Password: string; Email : string}

type Result<'a> = 
  | Success of 'a
  | Failure of Error


module UserValidation =

  let private isEmailAddrAlreadyExists (userManager : UserManager<User>) emailAddr =
    let user = userManager.FindByName(emailAddr)
    user <> null   

  let private validateEmailForEmptiness userDto =
    if (userDto.Email = null || userDto.Email = String.Empty) then
      Failure {Property = "Email"; Message = "Email is required"}
    else
      Success userDto

  let private validateEmailForLength userDto =
    if userDto.Email.Length > 100 then
      Failure {Property = "Email"; Message = "Email should not contain more than 100 characters"}
    else
      Success userDto 

  let private validateEmailForUniqueness isEmailAddrAlreadyExists userDto =
    if isEmailAddrAlreadyExists userDto.Email then
      Failure {Property = "Email"; Message = "Email address " + userDto.Email + " already exists"}      
    else
      Success userDto

  let private validateEmailForCorrectness userDto =
    let emailAddrRegex = 
      @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
    let isValidEmailAddress = Regex.IsMatch(userDto.Email, emailAddrRegex, RegexOptions.IgnoreCase)
    if isValidEmailAddress then 
      Success userDto
    else
      Failure {Property = "Email"; Message = "Email address " + userDto.Email + " is invalid"}
 
  let private validateNameForEmptiness userDto =
    if (userDto.Name = null || userDto.Name = String.Empty) then
      Failure {Property = "Name"; Message = "Name is required"}
    else
      Success userDto

  let validate userManager userDto =
    match validateEmailForEmptiness userDto with
      | Failure validationError -> Failure validationError
      | Success _ ->
        match validateEmailForCorrectness userDto with
        | Failure validationError -> Failure validationError
        | Success _ -> 
          match validateEmailForLength userDto with
          | Failure validationError -> Failure validationError
          | Success _ -> 
            match validateEmailForUniqueness (isEmailAddrAlreadyExists userManager) userDto with
            | Failure validationError -> Failure validationError
            | Success _ -> 
              match validateNameForEmptiness userDto with
              | Failure validationError -> Failure validationError
              | Success name -> Success name

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
  

  let private createUser' (userManager : UserManager<User>) userDto =
    try
      let user = new User(Name = userDto.Name, 
                          UserName = userDto.Email, 
                          Email = userDto.Email)
      let identityResult = userManager.Create(user, userDto.Password)
      if identityResult.Succeeded then
        Success user
      else
        let errors = String.concat "," identityResult.Errors
        Failure {Property = ""; Message = errors}
    with
      | _ ->
        Failure {Property = ""; Message = "An unexpected error happened while creating new user"}

  let createUser (userManager : UserManager<User>) userDto =
      match UserValidation.validate userManager userDto with
      | Failure validationError -> Failure validationError
      | Success _ -> createUser' userManager userDto
        
        
          