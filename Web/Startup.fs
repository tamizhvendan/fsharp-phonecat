namespace PhoneCat.Web

open Owin
open Microsoft.Owin
open Microsoft.Owin.Security.Cookies
open Microsoft.AspNet.Identity

type Startup() = 

  let configureAuthentication (app : IAppBuilder) =
    let cookieAuthenticationOptions = new CookieAuthenticationOptions()
    cookieAuthenticationOptions.AuthenticationType <- DefaultAuthenticationTypes.ApplicationCookie 
    cookieAuthenticationOptions.LoginPath <- new PathString("/authentication/login")
    app.UseCookieAuthentication(cookieAuthenticationOptions) |> ignore
    
  
  member x.Configuration(app : IAppBuilder) = 
    app.MapSignalR() |> ignore
    configureAuthentication app
    ()