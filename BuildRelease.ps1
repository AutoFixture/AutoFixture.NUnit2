function Build-Solutions
{
    .$env:windir\Microsoft.NET\Framework\v4.0.30319\MSBuild .\BuildRelease.msbuild
}

function Create-NugetPackages
{
    ""
    "Creating NuGet packages"
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\AutoFixture.nuspec -BasePath Release -o Release
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\AutoFixture.AutoMoq.nuspec -BasePath Release -o Release
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\AutoFixture.Xunit.nuspec -BasePath Release -o Release
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\AutoFixture.AutoRhinoMocks.nuspec -BasePath Release -o Release
    & '.\Lib\NuGet\nuget.exe' pack .\NuGet\AutoFixture.Idioms.nuspec -BasePath Release -o Release
}

Build-Solutions
Create-NugetPackages