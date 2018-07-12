using Cake.Fastlane;

Task("Fastlane.Deliver")
    .Does(() =>
    {
        Fastlane.Deliver(DeliverConfiguration.Configuration);
    });

Task("Fastlane.Deliver.Action");