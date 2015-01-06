#r "packages/FAKE/tools/FakeLib.dll"

open Fake

RestorePackages()

let buildDir = "./build"

Target "Clean" (fun _ -> CleanDir buildDir)

Target "Build" (fun _ ->

  !! "./PhoneCat.sln"
    |> MSBuildRelease buildDir "Build"
    |> Log "Build-Output: "

)

"Clean"
  ==> "Build"

RunTargetOrDefault "Build"