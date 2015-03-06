namespace Identity
open Microsoft.AspNet.Identity
open Rop
open Validation
open System.Text.RegularExpressions
open System

module EmailValidation =   
  let isUniqueEmailAddr (userManager : UserManager<User>) emailAddr =
    let user = userManager.FindByName(emailAddr)
    user = null 
    
  let isValidEmailAddress emailAddr =
    let emailAddrRegex = 
      @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)" +
        "*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
    Regex.IsMatch(emailAddr, emailAddrRegex, RegexOptions.IgnoreCase)

  let validateEmailEmptiness =
    validate 
      (fun createUserRequest -> isNonEmptyString createUserRequest.Email) 
      "Email" 
      "Email is required"    

  let validateEmailUniqueness isUniqueEmailAddr =
    validate 
      (fun createUserRequest -> isUniqueEmailAddr createUserRequest.Email) 
      "Email" 
      "Email address already exists"    

  let validateEmailCorrectness =
    validate 
      (fun createUserRequest -> isValidEmailAddress createUserRequest.Email) 
      "Email" 
      "Email address is invalid"