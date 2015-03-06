namespace Identity

module Rop = 

  type Error = {Property : string; Message : string}

  type Result<'a> = 
    | Success of 'a
    | Failure of Error

  let bind f1 f2 x =
    match f1 x with
    | Success x -> f2 x
    | Failure err -> Failure err

  let inline (>>=) f1 f2 = bind f1 f2