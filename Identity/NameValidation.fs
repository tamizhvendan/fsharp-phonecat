namespace Identity

open System
open Rop
open Validation

module NameValidation = 

  let validateNameEmptiness =
    validate 
      (fun createUserRequest -> isNonEmptyString createUserRequest.Name) 
      "Name" 
      "Name is required"        

  let validateNameLength  =
    validate 
      (fun createUserRequest -> createUserRequest.Name.Length <= 50) 
      "Name" 
      "Name should not contain more than 50 characters"
    

