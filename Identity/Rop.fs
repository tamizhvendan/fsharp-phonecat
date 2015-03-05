namespace Identity

module Rop = 

  type Error = {Property : string; Message : string}

  type Result<'a> = 
    | Success of 'a
    | Failure of Error

