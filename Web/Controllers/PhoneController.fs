namespace Web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax

type PhoneController() =
    inherit Controller()
    member this.View (id : string) = 
        id