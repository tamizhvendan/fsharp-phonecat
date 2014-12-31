namespace PhoneCat.Web

open Owin
open Microsoft.AspNet.SignalR
open Hubs
open System

type Startup() = 
  member x.Configuration(app : IAppBuilder) = 
    app.MapSignalR() |> ignore
    ()