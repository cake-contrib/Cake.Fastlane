using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Fastlane
{
    /// <summary>
    /// Provides functionality for fastlane tool.
    /// </summary>
    /// <seealso cref="FastlaneConfiguration" />
    /// <seealso cref="IFastlaneToolProvider" />
    public class FastlaneToolProvider : FastlaneTool<FastlaneConfiguration>, IFastlaneToolProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FastlaneToolProvider"/> class.
        /// </summary>
        /// <param name="contextFileSystem">The context file system.</param>
        /// <param name="contextEnvironment">The context environment.</param>
        /// <param name="contextProcessRunner">The context process runner.</param>
        /// <param name="contextTools">The context tools.</param>
        public FastlaneToolProvider(IFileSystem contextFileSystem,
            ICakeEnvironment contextEnvironment,
            IProcessRunner contextProcessRunner,
            IToolLocator contextTools)
            : base(contextFileSystem, contextEnvironment, contextProcessRunner, contextTools)
        {
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("update_fastlane");

            Run(new FastlaneConfiguration(), builder);
        }
    }
}