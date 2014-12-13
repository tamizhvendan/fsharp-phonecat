namespace PhoneCat.DataAccess
open FSharp.Data

[<AutoOpen>]
module TypeProviders =
    
    type PhoneIndexTypeProvider = JsonProvider<"https://raw.githubusercontent.com/angular/angular-phonecat/master/app/phones/phones.json">
    

