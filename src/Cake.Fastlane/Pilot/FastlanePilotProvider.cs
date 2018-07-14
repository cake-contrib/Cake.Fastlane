using System;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Fastlane
{
    /// <summary>
    /// Provides functionality for fastlane pilot tool.
    /// </summary>
    /// <seealso cref="IFastlaneMatchProvider" />
    internal class FastlanePilotProvider : FastlaneTool<FastlanePilotConfiguration>, IFastlanePilotProvider
    {
        private readonly ICakeEnvironment _environment;

        public void Pilot(FastlanePilotConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Run(configuration, ArgumentBuilder(configuration));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FastlanePilotProvider"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public FastlanePilotProvider(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <summary>
        /// https://github.com/fastlane/fastlane/blob/master/pilot/lib/pilot/options.rb
        /// </summary>
        /// <returns></returns>
        private ProcessArgumentBuilder ArgumentBuilder(FastlanePilotConfiguration configuration)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("pilot");

            if (!string.IsNullOrEmpty(configuration.UserName))
            {
                builder.AppendSwitch("-u", configuration.UserName);
            }

            if (!string.IsNullOrEmpty(configuration.AppIdentifier))
            {
                builder.AppendSwitch("-a", configuration.AppIdentifier);
            }

            if (!string.IsNullOrEmpty(configuration.Platform))
            {
                builder.AppendSwitch("-m", configuration.Platform);
            }

            if (configuration.IpaFilePath != null)
            {
                builder.AppendSwitchQuoted("-i", configuration.IpaFilePath.MakeAbsolute(_environment).FullPath);
            }

            if (!string.IsNullOrEmpty(configuration.ChangeLog))
            {
                builder.AppendSwitch("-w", configuration.ChangeLog);
            }

            if (!string.IsNullOrEmpty(configuration.BetaAppDescription))
            {
                builder.AppendSwitch("-d", configuration.BetaAppDescription);
            }

            if (!string.IsNullOrEmpty(configuration.BetaAppFeedbackEmail))
            {
                builder.AppendSwitch("-n", configuration.BetaAppFeedbackEmail);
            }

            if (configuration.SkipSubmission)
            {
                builder.Append("-s");
            }

            if (configuration.SkipWaiting)
            {
                builder.Append("-z");
            }

            if (!string.IsNullOrEmpty(configuration.AppleId))
            {
                builder.AppendSwitch("-p", configuration.AppleId);
            }

            if (configuration.Distribute)
            {
                builder.Append("--distribute_external");
            }

            if (configuration.Notify)
            {
                //TODO: Determine how to shut this thing off
            }

            if (configuration.DemoAccountRequired)
            {
                builder.Append("--demo_account_required");
            }

            if (!string.IsNullOrEmpty(configuration.FirstName))
            {
                builder.AppendSwitch("-f", configuration.FirstName);
            }

            if (!string.IsNullOrEmpty(configuration.LastName))
            {
                builder.AppendSwitch("-l", configuration.LastName);
            }

            if (!string.IsNullOrEmpty(configuration.Email))
            {
                builder.AppendSwitch("-e", configuration.Email);
            }

            if (configuration.TesterFilePath != null)
            {
                builder.AppendSwitchQuoted("-c", configuration.TesterFilePath.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.WaitProcessingInterval.HasValue && configuration.WaitProcessingInterval != 30)
            {
                builder.AppendSwitch("-k", configuration.WaitProcessingInterval.ToString());
            }

            if (!string.IsNullOrEmpty(configuration.TeamId))
            {
                builder.AppendSwitch("-q", configuration.TeamId);
            }

            if (!string.IsNullOrEmpty(configuration.TeamName))
            {
                builder.AppendSwitch("-r", configuration.TeamName);
            }

            if (!string.IsNullOrEmpty(configuration.PortalTeamId))
            {
                builder.AppendSwitch("--dev_portal_team_id", configuration.PortalTeamId);
            }

            if (!string.IsNullOrEmpty(configuration.ItcProvider))
            {
                builder.AppendSwitch("--itc_provider", configuration.ItcProvider);
            }

            if (configuration.Groups.Any())
            {
                var groups = string.Join(",", configuration.Groups.Select(x => $"\"{x}\""));
                builder.AppendSwitch("-g", groups);
            }

            if (configuration.WaitForUploadedBuild)
            {
                builder.Append("--wait_for_uploaded_build");
            }

            if (configuration.RejectBuildWaitingForReview)
            {
                builder.Append("--reject_build_waiting_for_review");
            }

            return builder.RenderSafe();
        }
    }
}