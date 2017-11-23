using Cake.Testing.Fixtures;

namespace Cake.Fastlane.Tests.Match
{
    internal class FastlaneMatchFixture : ToolFixture<MatchConfiguration>
    {
        public FastlaneMatchFixture()
            : base("fastlane.exe")
        {
        }

        protected override void RunTool()
        {
            var match = new FastlaneMatchProvider(FileSystem, Environment, ProcessRunner, Tools);

            match.Match(Settings);
        }
    }
}