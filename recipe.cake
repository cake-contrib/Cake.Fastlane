#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context, 
                            title: "Cake.Fastlane",
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            repositoryOwner: "RLittlesII",  
                            repositoryName: "Cake.Fastlane",  
                            appVeyorAccountName: "RLittlesII",
                            integrationTestScriptPath: "./tests/integration/test.cake",
                            shouldRunIntegrationTests: true,
                            shouldRunCodecov: false,
                            shouldRunDotNetCorePack: true);

Task("AzureDevOps")
    .IsDependentOn("Publish-MyGet-Packages")
    .IsDependentOn("Publish-Nuget-Packages")
    .IsDependentOn("Publish-GitHub-Release")
    .IsDependentOn("Publish-Documentation");

BuildParameters.Tasks.IntegrationTestTask.WithCriteria(() => BuildParameters.IsRunningOnUnix);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Fastlane.Tests/**/*.cs", BuildParameters.RootDirectoryPath + "/src/**/Cake.Fastlane.AssemblyInfo.cs"  }, 
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* ", 
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*", 
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();