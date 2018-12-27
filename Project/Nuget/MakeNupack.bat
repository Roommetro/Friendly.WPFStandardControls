rd /s /q "../ReleaseBinary"
"%DevEnvDir%devenv.exe" "../Friendly.WPFStandardControls.sln" /rebuild Release
"%DevEnvDir%devenv.exe" "../Friendly.WPFStandardControls.sln" /rebuild Release-English
nuget pack Friendly.WPFStandardControls.nuspec