// Utilities
#load "./utilities/settings.cake"
#load "./utilities/deliver.settings.cake"
#load "./utilities/xunit.cake"

// Tests
#load "./Fastlane/match.cake"
#load "./Fastlane/deliver.cake"
#load "./Fastlane/FastlaneAliases.cake"

//////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////

var target = Argument<string>("target", "Run-All-Tests");

//////////////////////////////////////////////////
// SETUP / TEARDOWN
//////////////////////////////////////////////////

Setup(ctx =>
{
});

Teardown(ctx =>
{
});

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("Cake.Fastlane")
    .IsDependentOn("Cake.Fastlane.Match")
    .IsDependentOn("Cake.Fastlane.Deliver");

Task("Run-All-Tests")
    .IsDependentOn("Cake.Fastlane");

//////////////////////////////////////////////////

RunTarget(target);