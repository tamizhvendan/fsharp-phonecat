namespace Identity
open Rop
open System

module Validation =
  
  let validate isValid property message createUserRequest =
    if isValid createUserRequest then
      Success createUserRequest
    else
      Failure {Property = property; Message = message}
      
  let isNonEmptyString str = str <> null && str <> String.Empty
    