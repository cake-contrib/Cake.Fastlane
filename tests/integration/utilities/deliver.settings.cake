using Cake.Fastlane;

public static class DeliverConfiguration
{
    public static FastlaneDeliverConfiguration Configuration { get; private set; }

    public static FastlaneDeliverConfiguration Initialize(ICakeContext context)
    {
        Configuration = new FastlaneDeliverConfiguration();

        return Configuration;
    }
}

DeliverConfiguration.Initialize(Context);