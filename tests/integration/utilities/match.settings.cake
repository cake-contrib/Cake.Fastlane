#addin "nuget:https://www.myget.org/F/cake-fastlane/api/v2?package=Cake.Fastlane&prerelease"

using Cake.Fastlane;

public static class MatchConfiguration
{
    public static FastlaneMatchConfiguration Configuration { get; private set; }

    public static FastlaneMatchConfiguration Initialize(ICakeContext context)
    {
        Configuration = new FastlaneMatchConfiguration
        {
            GitUrl = context.EnvironmentVariable("FASTLANE_GIT_URL"),
            GitBranch = "master",
            CertificateType = CertificateType.Development,
            AppIdentifier = context.EnvironmentVariable("FASTLANE_APP_ID"),
            UserName = context.EnvironmentVariable("FASTLANE_GIT_USER"),
        };

        return Configuration;
    }

    public static void WithSettings(ICakeContext context, Action<FastlaneMatchConfiguration> action)
    {
        if(action == null)
        {
            throw new ArgumentNullException();
        }

        var configuration = Initialize(context);

        action(configuration);

        Configuration = configuration;
    }
}

MatchConfiguration.Initialize(Context);