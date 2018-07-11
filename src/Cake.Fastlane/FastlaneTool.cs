using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

[assembly: InternalsVisibleTo("Cake.Fastlane.Tests")]

namespace Cake.Fastlane
{
    /// <summary>
    /// Cake tool abstraction for fastlane tools.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Cake.Core.Tooling.Tool{T}" />
    public abstract class FastlaneTool<T> : Tool<T>
        where T : FastlaneConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FastlaneTool{T}"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        protected FastlaneTool(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>
        /// The tool executable name.
        /// </returns>
        protected override IEnumerable<string> GetToolExecutableNames() => new[] { "fastlane.exe", "fastlane" };

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>
        /// The name of the tool.
        /// </returns>
        protected override string GetToolName() => "fastlane";
    }
}