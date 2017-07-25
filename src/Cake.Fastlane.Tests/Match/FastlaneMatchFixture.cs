using Cake.Testing.Fixtures;

namespace Cake.Fastlane.Tests.Match
{
    internal class FastlaneMatchFixture : ToolFixture<MatchConfiguration>
    {
        public FastlaneMatchFixture()
            : base("fastlane")
        {
        }

        protected override void RunTool()
        {
            Match();
        }

        private void Match()
        {
            var match = new FastlaneMatchProvider(FileSystem, Environment, ProcessRunner, Tools);

            match.Match(Settings);
        }
    }
}