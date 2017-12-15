using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Fastlane.Tests.Deliver
{
    internal class FastlaneDeliverProviderFixture : ToolFixture<FastlaneDeliverConfiguration>
    {
        public FastlaneDeliverProviderFixture()
            : base("fastlane")
        {
        }

        protected override void RunTool()
        {
            var deliver = new FastlaneDeliverProvider(FileSystem, Environment, ProcessRunner, Tools);

            deliver.Deliver(Settings);
        }
    }
}