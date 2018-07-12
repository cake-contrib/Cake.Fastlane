using Cake.Fastlane;

Task("Fastlane.Match")
    .Does(() =>
    {
        Fastlane.Match(FastlaneMatchConfiguration.Configuration);
    });

Task("Fastlane.Match.Action")
    .Does(() =>
    {
        Fastlane.Match(config =>
        {
            return FastlaneMatchConfiguration.Configuration;
        });
    });