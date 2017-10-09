#addin "nuget:https://www.myget.org/F/cake-fastlane/api/v2?package=Cake.Fastlane&prerelease"

using Cake.Fastlane;

public static class FastlaneMatchConfiguration
{
    public static MatchConfiguration Configuration { get; private set; }

    public static MatchConfiguration Initialize(ICakeContext context)
    {
        Configuration = new MatchConfiguration
        {
            GitUrl = context.EnvironmentVariable("FASTLANE_GIT_URL"),
            GitBranch = "master",
            CertificateType = CertificateType.Development,
            AppIdentifier = context.EnvironmentVariable("FASTLANE_APP_ID"),
            UserName = context.EnvironmentVariable("FASTLANE_GIT_USER"),
        };

        return Configuration;
    }

    public static void WithSettings(ICakeContext context, Action<MatchConfiguration> actions)
    {
        if(actions == null)
        {
            throw new ArgumentNullException();
        }

        var configuration = Initialize(context);

        actions(configuration);

        Configuration = configuration;
    }
}

FastlaneMatchConfiguration.Initialize(Context);