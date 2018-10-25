using Cake.Fastlane;

Task("Fastlane.Match")
    .Does(() =>
    {
        Fastlane.Match(MatchConfiguration.Configuration);
    });

Task("Fastlane.Match.Action")
    .Does(() =>
    {
        Fastlane.Match(config =>
        {
            config = MatchConfiguration.Configuration;
        });
    });