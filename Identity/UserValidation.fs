namespace Identity

open System
open System.Text.RegularExpressions

module UserValidation =   
  
  let private validateEmailForEmptiness emailAddr =
    if (emailAddr = null || emailAddr = String.Empty) then
      Failure {Property = "Email"; Message = "Email is required"}
    else
      Success emailAddr

  let private validateEmailForLength (emailAddr : String) =
    if emailAddr.Length > 100 then
      Failure {Property = "Email"; Message = "Email should not contain more than 100 characters"}
    else
      Success emailAddr 

  let private validateEmailForUniqueness isEmailAddrAlreadyExists emailAddr =
    if isEmailAddrAlreadyExists emailAddr then
      Failure {Property = "Email"; Message = "Email address " + emailAddr + " already exists"}      
    else
      Success emailAddr

  let private validateEmailForCorrectness emailAddr =
    let emailAddrRegex = 
      @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
    let isValidEmailAddress = Regex.IsMatch(emailAddr, emailAddrRegex, RegexOptions.IgnoreCase)
    if isValidEmailAddress then 
      Success emailAddr
    else
      Failure {Property = "Email"; Message = "Email address " + emailAddr + " is invalid"}  

  let validateEmail userManager emailAddr =
    match validateEmailForEmptiness emailAddr with
    | Failure validationError -> Failure validationError
    | Success _ ->
      match validateEmailForCorrectness emailAddr with
      | Failure validationError -> Failure validationError
      | Success _ -> 
        match validateEmailForLength emailAddr with
        | Failure validationError -> Failure validationError
        | Success _ -> 
          match validateEmailForUniqueness (isEmailAddrAlreadyExists userManager) emailAddr with
          | Failure validationError -> Failure validationError
          | Success _ -> Success emailAddr
          
     