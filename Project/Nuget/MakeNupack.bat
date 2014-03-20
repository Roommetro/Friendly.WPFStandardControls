rd /s /q "../ReleaseBinary"
"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe" "../Friendly.WPFStandardControls.sln" /rebuild Release
"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe" "../Friendly.WPFStandardControls.sln" /rebuild Release-English
nuget pack Friendly.WPFStandardControls.nuspec