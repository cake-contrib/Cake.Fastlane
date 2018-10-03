using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cake.Core.IO;

namespace Cake.Fastlane
{
    /// <summary>
    /// Fastlane pilot configuration options.
    /// </summary>
    /// <seealso cref="Cake.Fastlane.FastlaneConfiguration" />
    public class FastlanePilotConfiguration : FastlaneConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FastlanePilotConfiguration"/> class.
        /// </summary>
        public FastlanePilotConfiguration()
        {
            Notify = true;
            Groups = Enumerable.Empty<string>();
        }

        /// <summary>
        /// Gets or sets the command to run with pilot.
        /// </summary>
        public PilotCommand Command { get; set; }

        /// <summary>
        /// Gets or sets the path to the ipa file to upload.
        /// </summary>
        public FilePath IpaFilePath { get; set; }

        /// <summary>
        /// Gets or sets the 'what's new' text when uploading a new build.
        /// </summary>
        public string ChangeLog { get; set; }

        /// <summary>
        /// Gets or sets the beta app description when uploading a new build.
        /// </summary>
        public string BetaAppDescription { get; set; }

        /// <summary>
        /// Gets or sets the beta app email when uploading a new build.
        /// </summary>
        public string BetaAppFeedbackEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not distributing action of pilot and only upload the ipa file.
        /// </summary>
        public bool SkipSubmission { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to wait for the build to process.
        /// If set to true, the changelog won't be set, `distribute_external` option won't work and no build will be distributed to testers
        /// </summary>
        public bool SkipWaiting { get; set; }

        /// <summary>
        /// Gets or sets the unique App ID provided by App Store Connect.
        /// </summary>
        public string AppleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the build be distributed to external testers.
        /// </summary>
        public bool Distribute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to notify external testers.
        /// </summary>
        public bool Notify { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether you need a demo account for Apple review.
        /// </summary>
        public bool DemoAccountRequired { get; set; }

        /// <summary>
        /// Gets or sets the tester's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the tester's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the tester's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the file path to a CSV file of testers.
        /// </summary>
        public FilePath TesterFilePath { get; set; }

        /// <summary>
        /// Gets or sets the interval in seconds to wait for App Store Connect processing.
        /// </summary>
        public int? WaitProcessingInterval { get; set; }

        /// <summary>
        /// Gets or sets the short ID of your team in the developer portal, if you're in multiple teams. Different from your iTC team ID!".
        /// </summary>
        public string PortalTeamId { get; set; }

        /// <summary>
        /// Gets or sets the provider short name to be used with the iTMSTransporter to identify your team.
        /// To get provider short name run `pathToXcode.app/Contents/Applications/Application\\ Loader.app/Contents/itms/bin/iTMSTransporter -m provider -u 'USERNAME' -p 'PASSWORD' -account_type itunes_connect -v off`.
        /// The short names of providers should be listed in the second column
        /// </summary>
        public string ItcProvider { get; set; }

        /// <summary>
        /// Gets or sets groups to associate tester to one group or more by group name / group id. E.g. `-g \"Team 1\",\"Team 2\"`
        /// </summary>
        public IEnumerable<string> Groups { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use version info from uploaded ipa file to determine what build to use for distribution.
        /// If set to false, latest processing or any latest build will be used
        /// </summary>
        public bool WaitForUploadedBuild { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to expire previous build if it's 'waiting for review'.
        /// </summary>
        public bool RejectBuildWaitingForReview { get; set; }
    }
}