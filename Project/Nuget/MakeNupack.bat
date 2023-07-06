rd /s /q "../ReleaseBinary"
"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe" "../Friendly.WPFStandardControls.sln" /rebuild Release
"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe" "../Friendly.WPFStandardControls.sln" /rebuild Release-English
nuget pack Friendly.WPFStandardControls.nuspec