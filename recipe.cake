#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context, 
                            title: "Cake.Fastlane",
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            repositoryOwner: "RLittlesII",  
                            repositoryName: "Cake.Fastlane",  
                            appVeyorAccountName: "RLittlesII",
                            shouldRunDupFinder: false,
                            shouldRunInspectCode: false,
                            shouldRunIntegrationTests: true,
                            integrationTestScriptPath: "./tests/integration/test.cake");

BuildParameters.PrintParameters(Context);

BuildParameters.Tasks.AppVeyorTask.IsDependentOn("Run-Integration-Tests");

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.Fastlane.Tests/*.cs" }, 
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* ", 
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*", 
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();