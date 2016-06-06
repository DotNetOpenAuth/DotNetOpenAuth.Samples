properties {
	$base_directory = Resolve-Path . 
	$src_directory = "$base_directory"
	$output_directory = "$base_directory\build"
	$sln_file = "$src_directory\DotNetOpenAuth.Samples.sln"
	$target_config = "Release"
	$framework_version = "v4.5"
	$nuget_path = "$src_directory\.nuget\nuget.exe"
	
	$buildNumber = 0;
	$version = "2.5.0.0"
	$preRelease = $null
}

task default -depends Clean, Compile
task appVeyor -depends Clean, Compile

task Clean {
	rmdir $output_directory -ea SilentlyContinue -recurse
	exec { msbuild /nologo /verbosity:quiet $sln_file /p:Configuration=$target_config /t:Clean }
}

task Compile -depends UpdateVersion {
	exec { msbuild /nologo /verbosity:q $sln_file /p:Configuration=$target_config /p:TargetFrameworkVersion=v4.5 }
}

task UpdateVersion {
	$vSplit = $version.Split('.')
	if($vSplit.Length -ne 4)
	{
		throw "Version number is invalid. Must be in the form of 0.0.0.0"
	}
	$major = $vSplit[0]
	$minor = $vSplit[1]
	$patch = $vSplit[2]
	$assemblyFileVersion =  "$major.$minor.$patch.$buildNumber"
	$assemblyVersion = "$major.$minor.0.0"
	$versionAssemblyInfoFile = "$src_directory/VersionAssemblyInfo.cs"
	"using System.Reflection;" > $versionAssemblyInfoFile
	"" >> $versionAssemblyInfoFile
	"[assembly: AssemblyVersion(""$assemblyVersion"")]" >> $versionAssemblyInfoFile
	"[assembly: AssemblyFileVersion(""$assemblyFileVersion"")]" >> $versionAssemblyInfoFile
}
