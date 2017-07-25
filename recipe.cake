#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context, 
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.Fastlane",
                            repositoryOwner: "RLittlesII",
                            repositoryName: "Cake.Fastlane",
                            appVeyorAccountName: "RLittlesII",
                            shouldPostToGitter: false,
                            shouldPostToSlack: false,
							shouldPostToTwitter: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNetCore();