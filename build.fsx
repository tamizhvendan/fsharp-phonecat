#r "tools/FAKE/tools/FakeLib.dll"
#r "tools/FAKE/tools/Fake.IIS.dll"
#r "packages/Microsoft.Web.Administration/lib/net20/Microsoft.Web.Administration.dll"

open Fake
open Fake.IISHelper

let buildDir = "./build"

Target "Clean" (fun _ -> CleanDir buildDir)

Target "Build" (fun _ ->

  RestorePackages()

  !! "./PhoneCat.sln"
    |> MSBuildRelease buildDir "Build"
    |> Log "Build-Output: "

)

Target "Test" (fun _ ->
  !! (buildDir + "/*.Tests.dll")
    |> NUnit (fun p -> {p with ToolPath = "./tools/NUnit.Runners/tools" })
)


let siteName = "fsharp"
let appPool = "fsharp.appPool"
let port = ":9999:"
let vdir = "/phonecat"  
let webBuildPath = buildDir + "/_PublishedWebsites/Web"
Target "Deploy" (fun _ -> 
  let sitePhysicalPath = @"c:\inetpub\wwwroot\phonecat"
  XCopy webBuildPath sitePhysicalPath 
  UnlockSection "system.webServer/security/authentication/anonymousauthentication"
  (IIS
    (Site siteName "http" port @"C:\inetpub\wwwroot" appPool)
    (ApplicationPool appPool true "v4.0")
    (Some(Application vdir sitePhysicalPath)))
)

"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Deploy"

RunTargetOrDefault "Deploy"