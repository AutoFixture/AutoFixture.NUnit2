# This PowerShell script assumes that MSBuild and other .NET SDK utilities are available.
# This is most easily enabled by pulling in VSVars32.bat into PS. See e.g.
# http://blogs.msdn.com/b/ploeh/archive/2008/04/09/visualstudio2008powershell.aspx
# to see how this can be done.

function Build-Solutions ($buildConfiguration)
{
    dir Src/*.sln | % { `
        "Building $_ ($buildConfiguration)"
    	msbuild /nologo /clp:Summary /Verbosity:quiet /p:Configuration=$buildConfiguration $_
    	}
}

function Run-Tests
{
    ""
    "Running unit tests"
    & '.\Lib\xUnit.net 1.6.1\xunit.console.exe' .\Src\All.xunit
}

function Create-NugetPackages
{
    ""
    "Creating NuGet packages"
	& '.\Lib\Nuget 1.2\nuget.exe' pack .\Nuget\2.1\AutoFixture.2.1.nuspec -b Release -o Release
	& '.\Lib\Nuget 1.2\nuget.exe' pack .\Nuget\2.1\AutoFixture.AutoMoq.2.1.nuspec -b Release -o Release
	& '.\Lib\Nuget 1.2\nuget.exe' pack .\Nuget\2.1\AutoFixture.Xunit.2.1.nuspec -b Release -o Release
	& '.\Lib\Nuget 1.2\nuget.exe' pack .\Nuget\2.1\AutoFixture.AutoRhinoMocks.2.1.nuspec -b Release -o Release
}

function Copy-Output
{
    copy .\Src\AutoFixture\bin\Release\Ploeh.AutoFixture.dll .\Release
    copy .\Src\AutoFixture\bin\Release\Ploeh.AutoFixture.XML .\Release
    
    copy .\Src\SemanticComparison\bin\Release\Ploeh.SemanticComparison.dll .\Release
    copy .\Src\SemanticComparison\bin\Release\Ploeh.SemanticComparison.XML .\Release
    
    copy .\Src\AutoMoq\bin\Release\Ploeh.AutoFixture.AutoMoq.dll .\Release
    copy .\Src\AutoMoq\bin\Release\Ploeh.AutoFixture.AutoMoq.XML .\Release
    
    copy .\Src\AutoRhinoMock\bin\Release\Ploeh.AutoFixture.AutoRhinoMock.dll .\Release
    copy .\Src\AutoRhinoMock\bin\Release\Ploeh.AutoFixture.AutoRhinoMock.XML .\Release
    
    copy .\Src\AutoFixture.xUnit.net\bin\Release\Ploeh.AutoFixture.Xunit.dll .\Release
    copy .\Src\AutoFixture.xUnit.net\bin\Release\Ploeh.AutoFixture.Xunit.XML .\Release
}

rd .\Release* -Recurse
md Release > $null
Build-Solutions Verify
Build-Solutions Release
Run-Tests
Copy-Output
Create-NugetPackages