namespace Identity

open System
open Rop
open Validation

module PasswordValidation = 
  let validatePasswordEmptiness =
    validate 
      (fun createUserRequest -> isNonEmptyString createUserRequest.Password) 
      "Password" 
      "Password is required"     
  
  let validatePasswordLength = 
    validate 
      (fun createUserRequest -> createUserRequest.Name.Length <= 10) 
      "Password" 
      "Password should not contain more than 10 characters"
  
  let validatePasswordStrength  = 
    let specialCharacters = [ "*"; "&"; "%"; "$" ]
    let hasSpecialCharacters (str : String) = 
      specialCharacters |> List.exists (fun c -> str.Contains(c))
    let errorMessage = 
      "Password should contain atleast one of the special characters " 
        + String.Join(",", specialCharacters)
    validate 
      (fun createUserRequest -> 
        hasSpecialCharacters createUserRequest.Password) 
      "Password" 
      errorMessage