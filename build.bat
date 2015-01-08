@echo off
cls
".nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion"
".nuget\NuGet.exe" "Install" "NUnit.Runners" "-OutputDirectory" "tools" "-ExcludeVersion"
".nuget\NuGet.exe" "Install" "Microsoft.Web.Administration" "-OutputDirectory" "packages" "-ExcludeVersion"
"tools\FAKE\tools\Fake.exe" build.fsx
pause