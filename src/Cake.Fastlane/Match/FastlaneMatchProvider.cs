using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Fastlane
{
    /// <summary>
    /// Provides functionality for fastlane match tool.
    /// </summary>
    /// <seealso cref="IFastlaneMatchProvider" />
    public class FastlaneMatchProvider : FastlaneTool<FastlaneMatchConfiguration>, IFastlaneMatchProvider
    {
        /// <summary>
        /// The environment
        /// </summary>
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes an instance of <see cref="FastlaneMatchProvider"/>.
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
            _environment = environment;
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane match with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Match(FastlaneMatchConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Run(configuration, ArgumentBuilder(configuration));
        }

        /// <summary>
        /// Executes fastlane match with the specified configuration action.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Fastlane.Match(config =>
        ///         {
        ///             config.CertificateType = CertificateType.Development;
        ///             config.AppIdentifier = "com.fastlane.cake";
        ///             config.ForceForNewDevices = true;
        ///         });
        ///     </code>
        /// </example>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        public void Match(Action<FastlaneMatchConfiguration> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var configuration = new FastlaneMatchConfiguration();

            action(configuration);

            Match(configuration);
        }

        /// <summary>
        /// https://github.com/fastlane/fastlane/blob/master/match/lib/match/options.rb
        /// Environment Variable to store Fastlane match password - MATCH_PASSWORD
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private ProcessArgumentBuilder ArgumentBuilder(FastlaneMatchConfiguration match)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("match");

            if (match.CertificateType != null)
            {
                builder.Append($"{match.CertificateType.ToString().ToLowerInvariant()}");
            }

            if (!string.IsNullOrEmpty(match.AppIdentifier))
            {
                builder.AppendSwitch("-a", match.AppIdentifier);
            }

            if (!string.IsNullOrEmpty(match.UserName))
            {
                builder.AppendSwitch("-u", match.UserName);
            }

            if (!string.IsNullOrEmpty(match.KeyChainName))
            {
                builder.AppendSwitch("-s", match.KeyChainName);
            }

            if (!string.IsNullOrEmpty(match.KeyChainPassword))
            {
                builder.AppendSwitchSecret("-p", match.KeyChainPassword);
            }

            if (!string.IsNullOrEmpty(match.TeamId))
            {
                builder.AppendSwitch("-b", match.TeamId);
            }

            if (!string.IsNullOrEmpty(match.GitFullName))
            {
                builder.AppendSwitch("--git_full_name", match.GitFullName);
            }

            if (!string.IsNullOrEmpty(match.GitUserEmail))
            {
                builder.AppendSwitch("--git_user_email", match.GitUserEmail);
            }

            if (!string.IsNullOrEmpty(match.GitUrl))
            {
                builder.AppendSwitch("-r", match.GitUrl);
            }

            if (!string.IsNullOrEmpty(match.GitBranch))
            {
                builder.AppendSwitch("--git_branch", match.GitBranch);
            }

            if (!string.IsNullOrEmpty(match.TeamName))
            {
                builder.AppendSwitch("-l", match.TeamName);
            }

            if (match.Force)
            {
                builder.Append("--force");
            }

            if (match.SkipConfirmation)
            {
                builder.Append("--skip_confirmation");
            }

            if (match.ShallowClone)
            {
                builder.Append("--shallow_clone");
            }

            if (match.CloneBranchDirectly)
            {
                builder.Append("--clone_branch_directly");
            }

            if (match.Workspace != null)
            {
                builder.AppendSwitchQuoted("--workspace", match.Workspace.MakeAbsolute(_environment).FullPath);
            }

            if (match.ForceForNewDevices)
            {
                builder.Append("--force_for_new_devices");
            }

            if (match.SkipDocs)
            {
                builder.Append("--skip_docs");
            }

            if (match.ReadOnly)
            {
                builder.Append("--readonly");
            }

            if (match.Verbose)
            {
                builder.Append("--verbose");
            }

            if (match.Platform != null)
            {
                builder.AppendSwitch("-o", match.Platform);
            }

            return builder.RenderSafe();
        }
    }
}