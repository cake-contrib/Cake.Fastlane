using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System.Collections.Generic;

namespace Cake.Fastlane
{
    public abstract class FastlaneTool<T> : Tool<T>
        where T : FastlaneConfiguration
    {
        protected FastlaneTool(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
        }

        protected override string GetToolName()
        {
            return "fastlane";
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "fastlane" };
        }
    }
}