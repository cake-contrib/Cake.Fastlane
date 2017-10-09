using Cake.Fastlane;

Task("Fastlane.Match")
    .Does(() =>
    {
        Context.Fastlane().Match(FastlaneMatchConfiguration.Configuration);
    });