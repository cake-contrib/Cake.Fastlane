Task("Cake.Fastlane.Match")
    .IsDependentOn("Fastlane.Match")
    .IsDependentOn("Fastlane.Match.Action");

Task("Cake.Fastlane.Deliver")
    .IsDependentOn("Fastlane.Deliver.Action");