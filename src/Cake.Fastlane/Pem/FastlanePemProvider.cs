using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Fastlane
{
    /// <summary>
    /// Provides functionality for fastlane pem tool.
    /// </summary>
    /// <seealso cref="IFastlaneMatchProvider" />
    internal class FastlanePemProvider : FastlaneTool<FastlanePemConfiguration>, IFastlanePemProvider
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="FastlanePemProvider"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public FastlanePemProvider(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pem with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="T:System.ArgumentNullException">configuration</exception>
        public void Pem(FastlanePemConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Run(configuration, ArgumentBuilder(configuration));
        }

        /// <summary>
        /// https://github.com/fastlane/fastlane/blob/master/pem/lib/pem/options.rb
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private ProcessArgumentBuilder ArgumentBuilder(FastlanePemConfiguration configuration)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("pem");

            if (configuration.Development)
            {
                builder.Append("--development");
            }

            if (configuration.ActiveDaysLimit.HasValue && configuration.ActiveDaysLimit != 30)
            {
                builder.AppendSwitch("--active_days_limit", configuration.ActiveDaysLimit.ToString());
            }

            if (configuration.GenerateP12)
            {
                builder.Append("--generate_p12");
            }

            if (configuration.Force)
            {
                builder.Append("--force");
            }

            if (configuration.SavePrivateKey)
            {
                builder.Append("--save_private_key");
            }

            if (!string.IsNullOrWhiteSpace(configuration.AppIdentifier))
            {
                builder.AppendSwitch("--app_identifier", configuration.AppIdentifier);
            }

            if (!string.IsNullOrEmpty(configuration.UserName))
            {
                builder.AppendSwitch("-u", configuration.UserName);
            }

            if (!string.IsNullOrEmpty(configuration.TeamId))
            {
                builder.AppendSwitch("-b", configuration.TeamId);
            }

            if (!string.IsNullOrEmpty(configuration.TeamName))
            {
                builder.AppendSwitch("-l", configuration.TeamName);
            }

            if (!string.IsNullOrEmpty(configuration.P12Password))
            {
                builder.AppendSwitchSecret("--p12_password", configuration.P12Password);
            }

            if (!string.IsNullOrEmpty(configuration.FileName))
            {
                builder.AppendSwitch("--pem_name", configuration.FileName);
            }

            if (configuration.OutputPath != null)
            {
                builder.AppendSwitchQuoted("--output_path",
                    configuration.OutputPath.MakeAbsolute(_environment).FullPath);
            }

            return builder.RenderSafe();
        }
    }
}