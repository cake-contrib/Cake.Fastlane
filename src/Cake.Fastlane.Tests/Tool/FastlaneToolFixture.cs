using System;
using Cake.Testing.Fixtures;

namespace Cake.Fastlane.Tests
{
    public class FastlaneToolFixture : ToolFixture<FastlaneConfiguration>
    {
        public FastlaneToolFixture()
            : base("fastlane")
        {
        }

        protected override void RunTool()
        {
            var runner = new FastlaneToolProvider(FileSystem, Environment, ProcessRunner, Tools);

            runner.Update();
        }
    }
}