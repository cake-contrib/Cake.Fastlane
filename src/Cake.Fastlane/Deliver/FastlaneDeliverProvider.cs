using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Fastlane
{
    /// <summary>
    /// Provides functionality for fastlane deliver tool.
    /// </summary>
    /// <seealso cref="IFastlaneDeliverProvider" />
    internal class FastlaneDeliverProvider : FastlaneTool<FastlaneDeliverConfiguration>, IFastlaneDeliverProvider
    {
        private readonly ICakeEnvironment _environment;

        private readonly Func<Dictionary<string, string>, string> Aggregate = (dictionary) =>
                {
                    return dictionary.Aggregate(string.Empty, (current, hash) => current + $"{hash.Key}:{hash.Value}")
                        .TrimEnd(',');
                };

        /// <summary>
        /// Initializes a new instance of the <see cref="FastlaneDeliverProvider"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public FastlaneDeliverProvider(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane deliver with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        public void Deliver(FastlaneDeliverConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Run(configuration, ArgumentBuilder(configuration));
        }

        /// <summary>
        /// https://github.com/fastlane/fastlane/blob/master/deliver/lib/deliver/options.rb
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        private ProcessArgumentBuilder ArgumentBuilder(FastlaneDeliverConfiguration configuration)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("deliver");

            if (!string.IsNullOrWhiteSpace(configuration.AppIdentifier))
            {
                builder.AppendSwitch("-a", configuration.AppIdentifier);
            }

            if (!string.IsNullOrWhiteSpace(configuration.AppId))
            {
                builder.AppendSwitch("-p", configuration.AppId);
            }

            if (!string.IsNullOrWhiteSpace(configuration.UserName))
            {
                builder.AppendSwitch("-u", configuration.UserName);
            }

            if (configuration.EditLive)
            {
                builder.Append("-o");
            }

            if (configuration.IpaPath != null)
            {
                builder.AppendSwitchQuoted("-i", configuration.IpaPath.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.PkgPath != null)
            {
                builder.AppendSwitchQuoted("-c", configuration.PkgPath.MakeAbsolute(_environment).FullPath);
            }

            if (!string.IsNullOrWhiteSpace(configuration.Platform))
            {
                builder.AppendSwitch("-j", configuration.Platform);
            }

            if (configuration.MetaDataPath != null)
            {
                builder.AppendSwitchQuoted("-m", configuration.MetaDataPath.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.ScreenShotPath != null)
            {
                builder.AppendSwitchQuoted("-w", configuration.ScreenShotPath.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.SkipBinaryUpload)
            {
                builder.Append("--skip_binary_upload");
            }

            if (configuration.SkipScreenShots)
            {
                builder.Append("--skip_screenshots");
            }

            if (!string.IsNullOrWhiteSpace(configuration.AppVersion))
            {
                builder.AppendSwitch("-z", configuration.AppVersion);
            }

            if (configuration.Force)
            {
                builder.Append("-f");
            }

            if (configuration.SubmitForReview)
            {
                builder.Append("--submit_for_review");
            }

            if (configuration.AutomaticRelease)
            {
                builder.Append("--automatic_release");
            }

            if (configuration.AutomaticReleaseDate.HasValue)
            {
                builder.AppendSwitch("--auto_release_date",
                    configuration.AutomaticReleaseDate.Value.Millisecond.ToString());
            }

            if (configuration.PriceTier.HasValue)
            {
                builder.AppendSwitch("-r", configuration.PriceTier.ToString());
            }

            if (!string.IsNullOrWhiteSpace(configuration.BuildNumber))
            {
                builder.AppendSwitch("-n", configuration.BuildNumber);
            }

            if (configuration.AppRatingConfigPath != null)
            {
                builder.AppendSwitchQuoted("-g", configuration.AppRatingConfigPath.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.SubmissionInformation.Any())
            {
                builder.Append("-b");
            }

            if (!string.IsNullOrWhiteSpace(configuration.TeamId))
            {
                builder.AppendSwitch("-k", configuration.TeamId);
            }

            if (!string.IsNullOrWhiteSpace(configuration.TeamName))
            {
                builder.AppendSwitch("-e", configuration.TeamName);
            }

            if (!string.IsNullOrWhiteSpace(configuration.DevPortalTeamId))
            {
                builder.AppendSwitch("-s", configuration.DevPortalTeamId);
            }

            if (!string.IsNullOrWhiteSpace(configuration.DevPortalTeamName))
            {
                builder.AppendSwitch("-y", configuration.DevPortalTeamName);
            }

            if (!string.IsNullOrWhiteSpace(configuration.ItcProvider))
            {
                builder.AppendSwitch("--itc_provider", configuration.ItcProvider);
            }

            if (configuration.OverWriteScreenShots)
            {
                builder.Append("--overwrite_screenshots");
            }

            if (configuration.AppIcon != null)
            {
                builder.AppendSwitchQuoted("-l", configuration.AppIcon.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.AppleWatchIcon != null)
            {
                builder.AppendSwitchQuoted("-q", configuration.AppleWatchIcon.MakeAbsolute(_environment).FullPath);
            }

            if (!string.IsNullOrWhiteSpace(configuration.Copyright))
            {
                builder.AppendSwitch("--copyright", configuration.Copyright);
            }

            if (!string.IsNullOrWhiteSpace(configuration.PrimaryCategory))
            {
                builder.AppendSwitch("--primary_category", configuration.PrimaryCategory);
            }

            if (!string.IsNullOrWhiteSpace(configuration.SecondaryCategory))
            {
                builder.AppendSwitch("--secondary_category", configuration.SecondaryCategory);
            }

            if (!string.IsNullOrWhiteSpace(configuration.PrimaryFirstSubCategory))
            {
                builder.AppendSwitch("--primary_first_sub_category", configuration.PrimaryFirstSubCategory);
            }

            if (!string.IsNullOrWhiteSpace(configuration.PrimarySecondSubCategory))
            {
                builder.AppendSwitch("--primary_second_sub_category", configuration.PrimarySecondSubCategory);
            }

            if (!string.IsNullOrWhiteSpace(configuration.SecondaryFirstSubCategory))
            {
                builder.AppendSwitch("--secondary_first_sub_category", configuration.SecondaryFirstSubCategory);
            }

            if (!string.IsNullOrWhiteSpace(configuration.SecondarySecondSubCategory))
            {
                builder.AppendSwitch("--secondary_second_sub_category", configuration.SecondarySecondSubCategory);
            }

            if (configuration.AppReviewInformation.Any())
            {
                builder.Append("--app_review_information");
            }

            if (configuration.Name.Any())
            {
                builder.Append("--name");
            }

            if (configuration.Description.Any())
            {
                builder.Append("--description");
            }

            if (configuration.ReleaseNotes.Any())
            {
                builder.Append("--release_notes");
            }

            if (configuration.KeyWords.Any())
            {
                builder.Append("--keywords");
            }

            if (configuration.SkipMetadataUpload)
            {
                builder.Append("--skip_metadata");
            }

            if (configuration.PhasedRelease)
            {
                builder.Append("--phased_release");
            }

            if (!string.IsNullOrWhiteSpace(configuration.PrivacyUrl))
            {
                builder.AppendSwitch("--privacy_url", configuration.PrivacyUrl);
            }

            if (!string.IsNullOrWhiteSpace(configuration.SupportUrl))
            {
                builder.AppendSwitch("--support_url", configuration.SupportUrl);
            }

            if (!string.IsNullOrWhiteSpace(configuration.MarketingUrl))
            {
                builder.AppendSwitch("--marketing_url", configuration.MarketingUrl);
            }

            return builder;
        }
    }
}