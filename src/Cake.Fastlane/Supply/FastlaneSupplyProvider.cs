using System;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Fastlane
{
#pragma warning disable CS0618 // Type or member is obsolete
    internal class FastlaneSupplyProvider : FastlaneTool<FastlaneSupplyConfiguration>, IFastlaneSupplyProvider
    {
        private readonly ICakeEnvironment _environment;

        public FastlaneSupplyProvider(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        public void Supply(FastlaneSupplyConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Run(configuration, ArgumentBuilder(configuration));
        }

        /// <summary>
        /// https://github.com/fastlane/fastlane/blob/master/supply/lib/supply/options.rb
        /// </summary>
        /// <returns></returns>
        private ProcessArgumentBuilder ArgumentBuilder(FastlaneSupplyConfiguration configuration)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("supply");

            if (!string.IsNullOrEmpty(configuration.PackageName))
            {
                builder.AppendSwitch("-p", configuration.PackageName);
            }

            if (!string.IsNullOrEmpty(configuration.Track))
            {
                builder.AppendSwitch("-a", configuration.Track);
            }

            if (configuration.Rollout.HasValue)
            {
                builder.AppendSwitch("-r", configuration.Rollout.ToString());
            }

            if (configuration.MetadataPath != null)
            {
                builder.AppendSwitchQuoted("-m", configuration.MetadataPath.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.KeyFilePath != null)
            {
                builder.AppendSwitchQuoted("-k", configuration.KeyFilePath.MakeAbsolute(_environment).FullPath);
            }

            if (!string.IsNullOrEmpty(configuration.Issuer))
            {
                builder.AppendSwitch("-i", configuration.Issuer);
            }

            if (configuration.JsonKeyFilePath != null)
            {
                builder.AppendSwitchQuoted("-j", configuration.JsonKeyFilePath.MakeAbsolute(_environment).FullPath);
            }

            if (!string.IsNullOrEmpty(configuration.JsonKeyData))
            {
                builder.AppendSwitch("-c", configuration.JsonKeyData);
            }

            if (configuration.ApkFilePath != null)
            {
                builder.AppendSwitchQuoted("-b", configuration.ApkFilePath.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.ApkFiles.Any())
            {
                var files = string.Join(",",
                    configuration.ApkFiles.Select(x => $"\"{x.MakeAbsolute(_environment).FullPath}\""));
                builder.AppendSwitch("-u", files);
            }

            if (configuration.AAB != null)
            {
                builder.AppendSwitchQuoted("-f", configuration.AAB.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.SkipUploadApk)
            {
                builder.Append("--skip_upload_apk");
            }

            if (configuration.SkipUploadAAB)
            {
                builder.Append("--skip_upload_aab");
            }

            if (configuration.SkipUploadMetadata)
            {
                builder.Append("--skip_upload_metadata");
            }

            if (configuration.SkipUploadImages)
            {
                builder.Append("--skip_upload_images");
            }

            if (configuration.SkipUploadScreenShots)
            {
                builder.Append("--skip_upload_screenshots");
            }

            if (!string.IsNullOrEmpty(configuration.PromoteTrack))
            {
                builder.AppendSwitch("--track_promote_to", configuration.PromoteTrack);
            }

            if (configuration.ValidateOnly)
            {
                builder.Append("--validate_only");
            }

            if (configuration.Mapping != null)
            {
                builder.AppendSwitchQuoted("-d", configuration.Mapping.MakeAbsolute(_environment).FullPath);
            }

            if (configuration.MappingFiles.Any())
            {
                var files = string.Join(",",
                    configuration.MappingFiles.Select(x => $"\"{x.MakeAbsolute(_environment).FullPath}\""));
                builder.AppendSwitch("-s", files);
            }

            if (!string.IsNullOrEmpty(configuration.RootUrl))
            {
                builder.AppendSwitch("--root_url", configuration.RootUrl);
            }

            if (configuration.CheckSupersededTracks)
            {
                builder.Append("--check_superseded_tracks");
            }

            return builder;
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}