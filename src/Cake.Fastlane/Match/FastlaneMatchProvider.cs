using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;

namespace Cake.Fastlane
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="IFastlaneMatchProvider" />
    public class FastlaneMatchProvider : FastlaneTool<MatchConfiguration>, IFastlaneMatchProvider
    {
        private readonly IToolLocator _tools;
        private IProcessRunner _processRunner;
        private IFileSystem _fileSystem;
        private ICakeEnvironment _environment;

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="environment"></param>
        /// <param name="processRunner"></param>
        /// <param name="tools"></param>
        public FastlaneMatchProvider(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _fileSystem = fileSystem;
            _environment = environment;
            _processRunner = processRunner;
            _tools = tools;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchConfiguration"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Initialize()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        public void Match(MatchConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Run(configuration, ArgumentBuilder(configuration));
        }

        /// <summary>
        /// https://github.com/fastlane/fastlane/blob/master/match/lib/match/options.rb
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private static ProcessArgumentBuilder ArgumentBuilder(MatchConfiguration match)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("match");

            if (match.CertificateType != null)
            {
                builder.Append($"{match.CertificateType.ToString().ToLowerInvariant()}");
            }

            if (!string.IsNullOrEmpty(match.UserName))
            {
                builder.AppendSwitch("-u", match.UserName);
            }

            if (!string.IsNullOrEmpty(match.AppIdentifier))
            {
                builder.AppendSwitch("-a", match.AppIdentifier);
            }

            if (!string.IsNullOrEmpty(match.KeyChainName))
            {
                builder.AppendSwitch("-s", match.KeyChainName);
            }

            if (match.ForceForNewDevices)
            {
                builder.Append("--force_for_new_devices");
            }

            if (match.ReadOnly)
            {
                builder.Append("--readonly");
            }

            if (match.Verbose)
            {
                builder.Append("--verbose");
            }

            return builder;
        }
    }
}