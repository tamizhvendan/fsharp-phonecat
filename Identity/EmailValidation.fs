namespace Identity
open Microsoft.AspNet.Identity
open Rop
open System.Text.RegularExpressions
open System

module EmailValidation =  
  
  let isUniqueEmailAddr (userManager : UserManager<User>) emailAddr =
    let user = userManager.FindByName(emailAddr)
    user = null   

  let validateEmailEmptiness createUserRequest =
    if (createUserRequest.Email <> null && createUserRequest.Email <> String.Empty) then
      Success createUserRequest      
    else
      Failure {Property = "Email"; Message = "Email is required"}

  let validateEmailUniqueness isUniqueEmailAddr createUserRequest =
    if isUniqueEmailAddr createUserRequest.Email then
      Success createUserRequest      
    else
      Failure {Property = "Email"; Message = "Email address " + createUserRequest.Email + " already exists"}      

  let validateEmailCorrectness createUserRequest =
    let emailAddrRegex = 
      @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
    let isValidEmailAddress = Regex.IsMatch(createUserRequest.Email, emailAddrRegex, RegexOptions.IgnoreCase)
    if isValidEmailAddress then 
      Success createUserRequest
    else
      Failure {Property = "Email"; Message = "Email address " + createUserRequest.Email + " is invalid"}

