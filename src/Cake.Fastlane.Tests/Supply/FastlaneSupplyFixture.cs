using Cake.Testing.Fixtures;

namespace Cake.Fastlane.Tests.Supply
{
    public class FastlaneSupplyFixture : ToolFixture<FastlaneSupplyConfiguration>
    {
        public FastlaneSupplyFixture()
            : base("fastlane.exe")
        {
        }

        protected override void RunTool()
        {
            var provider = new FastlaneSupplyProvider(FileSystem, Environment, ProcessRunner, Tools);

            provider.Supply(Settings);
        }
    }
}