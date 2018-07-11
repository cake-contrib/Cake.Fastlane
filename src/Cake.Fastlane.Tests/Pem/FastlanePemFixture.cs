using Cake.Testing.Fixtures;

namespace Cake.Fastlane.Tests.Pem
{
    internal class FastlanePemFixture : ToolFixture<FastlanePemConfiguration>
    {
        public FastlanePemFixture()
            : base("fastlane.exe")
        {
            Settings = new FastlanePemConfiguration
            {
                GenerateP12 = false
            };
        }

        protected override void RunTool()
        {
            var provider = new FastlanePemProvider(FileSystem, Environment, ProcessRunner, Tools);

            provider.Pem(Settings);
        }
    }
}