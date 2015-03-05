namespace Identity

open EmailValidation
open NameValidation
open PasswordValidation
open Rop
open System

module UserValidation =         

  let validateCreateUserRequest userManager createUserRequest =
    match validateEmailEmptiness createUserRequest with
      | Failure error -> Failure error
      | Success _ ->
        match validateEmailCorrectness createUserRequest with
        | Failure error -> Failure error
        | Success _ ->          
          match validateEmailUniqueness (isUniqueEmailAddr userManager) createUserRequest with
          | Failure error -> Failure error
          | Success _ -> 
            match validateNameEmptiness createUserRequest with
            | Failure error -> Failure error
            | Success _ -> 
              match validateNameLength createUserRequest with
              | Failure error -> Failure error
              | Success _ -> 
                match validatePasswordEmptiness createUserRequest with
                | Failure error -> Failure error
                | Success _ -> 
                  match validatePasswordLength createUserRequest with
                  | Failure error -> Failure error
                  | Success _ ->                    
                    match validatePasswordStrength createUserRequest with
                    | Failure error -> Failure error
                    | Success createUserRequest' -> Success createUserRequest'