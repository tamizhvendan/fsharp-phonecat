#r "tools/FAKE/tools/FakeLib.dll"
#r "tools/FAKE/tools/Fake.IIS.dll"

open Fake
open Fake.IISHelper
open Fake.AssemblyInfoFile

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

Target "Deploy" (fun _ -> 
  
  let siteName = "fsharp"
  let appPool = "fsharp.appPool"
  let port = ":9999:"
  let vdir = "/phonecat"  
  let webBuildPath = buildDir + "/_PublishedWebsites/Web"
  let sitePhysicalPath = @"c:\inetpub\wwwroot\phonecat"
  CleanDir sitePhysicalPath
  XCopy webBuildPath sitePhysicalPath 
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