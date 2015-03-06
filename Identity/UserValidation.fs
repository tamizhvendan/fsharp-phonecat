namespace Identity

open EmailValidation
open NameValidation
open PasswordValidation
open Rop
open System

module UserValidation = 
  let validateCreateUserRequest userManager  = 
      validateEmailEmptiness 
        >>= validateEmailCorrectness
        >>= validateEmailUniqueness (isUniqueEmailAddr userManager)
        >>= validateNameEmptiness
        >>= validateNameLength
        >>= validatePasswordEmptiness
        >>= validatePasswordLength
        >>= validatePasswordStrength