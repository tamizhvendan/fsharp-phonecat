namespace PhoneCat.Web

open Owin

type Startup() = 
  member x.Configuration(app : IAppBuilder) = 
    app.MapSignalR() |> ignore
    ()
