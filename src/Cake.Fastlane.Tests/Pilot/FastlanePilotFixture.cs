using Cake.Testing.Fixtures;

namespace Cake.Fastlane.Tests.Pilot
{
    internal class FastlanePilotFixture : ToolFixture<FastlanePilotConfiguration>
    {
        public FastlanePilotFixture()
            : base("fastlane.exe")
        {
        }

        protected override void RunTool()
        {
            var provider = new FastlanePilotProvider(FileSystem, Environment, ProcessRunner, Tools);

            provider.Pilot(Settings);
        }
    }
}