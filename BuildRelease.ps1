function Build-Solutions
{
    .$env:windir\Microsoft.NET\Framework\v4.0.30319\MSBuild .\BuildRelease.msbuild
}

function Create-NugetPackages
{
    ""
    "Creating NuGet packages"
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\Ploeh.AutoFixture.nuspec -BasePath Release -o Release
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\Ploeh.AutoFixture.AutoMoq.nuspec -BasePath Release -o Release
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\Ploeh.AutoFixture.Xunit.nuspec -BasePath Release -o Release
	& '.\Lib\NuGet\nuget.exe' pack .\NuGet\Ploeh.AutoFixture.AutoRhinoMocks.nuspec -BasePath Release -o Release
    & '.\Lib\NuGet\nuget.exe' pack .\NuGet\Ploeh.AutoFixture.Idioms.nuspec -BasePath Release -o Release
}

Build-Solutions
Create-NugetPackages