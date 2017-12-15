using System;
using System.Collections.Generic;
using Cake.Core.IO;

namespace Cake.Fastlane
{
    /// <inheritdoc />
    /// <summary>
    /// Fastlane deliver configuration options.
    /// </summary>
    /// <seealso cref="T:Cake.Fastlane.FastlaneConfiguration" />
    public class FastlaneDeliverConfiguration : FastlaneConfiguration
    {
        /// <summary>
        /// Gets or sets file path to the application icon.
        /// </summary>
        public FilePath AppIcon { get; set; }

        /// <summary>
        /// Gets or sets The app ID of the app you want to use/modify.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets file path to the apple watch icon.
        /// </summary>
        public FilePath AppleWatchIcon { get; set; }

        /// <summary>
        /// Gets or sets the file path to the application rating configuration.
        /// </summary>
        public FilePath AppRatingConfigPath { get; set; }

        /// <summary>
        /// Gets or sets the application review information.
        /// </summary>
        public Dictionary<string, string> AppReviewInformation { get; set; }

        /// <summary>
        /// Gets or sets the application version that should be edited or created.
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the app should be automatically released once it's approved.
        /// </summary>
        public bool AutomaticRelease { get; set; }

        /// <summary>
        /// Gets or sets the date in milliseconds for automatically releasing on pending approval.
        /// </summary>
        public DateTimeOffset? AutomaticReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the build number (already uploaded to iTC) will be used instead of the current built one.
        /// </summary>
        public string BuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets the localized description.
        /// </summary>
        public Dictionary<string, string> Description { get; set; }

        /// <summary>
        /// Gets or sets your Developer Portal team, if you're in multiple teams. Different from your iTC team ID.
        /// </summary>
        public string DevPortalTeamId { get; set; }

        /// <summary>
        /// Gets or sets the name of your Developer Portal team if you're in multiple teams.
        /// </summary>
        public string DevPortalTeamName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable ipa upload and screenshot upload.
        /// </summary>
        public bool EditLive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore errors when invalid languages are found in metadata and screeenshot directories.
        /// </summary>
        public bool IgnoreLanguageDirectoryValidation { get; set; }

        /// <summary>
        /// Gets or sets the file path for the ipa.
        /// </summary>
        public FilePath IpaPath { get; set; }

        /// <summary>
        /// Gets or sets the provider short name to be used with the iTMSTransporter to identify your team..
        /// </summary>
        public string ItcProvider { get; set; }

        /// <summary>
        /// Gets or sets the list of localized key words.
        /// </summary>
        public Dictionary<string, string> KeyWords { get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        public IEnumerable<string> Languages { get; set; }

        /// <summary>
        /// Gets or sets the localized marketing URL.
        /// </summary>
        public string MarketingUrl { get; set; }

        /// <summary>
        /// Gets or sets the directory path to the folder containing meta data.
        /// </summary>
        public DirectoryPath MetaDataPath { get; set; }

        /// <summary>
        /// Gets or sets the localized name.
        /// </summary>
        public Dictionary<string, string> Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to clear all previously uploaded screenshots before uploading the new ones.
        /// </summary>
        public bool OverWriteScreenShots { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable the phased release feature of iTC.
        /// </summary>
        public bool PhasedRelease { get; set; }

        /// <summary>
        /// Gets or sets the file path for the pkg.
        /// </summary>
        public FilePath PkgPath { get; set; }

        /// <summary>
        /// Gets or sets the default rule level unless otherwise configured.
        /// </summary>
        public string PreCheckDefaultRuleLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether precheck should check in-app purchases.
        /// </summary>
        public bool PreCheckIncludeInAppPurchases { get; set; }

        /// <summary>
        /// Gets or sets the price tier of this application.
        /// </summary>
        public int? PriceTier { get; set; }

        /// <summary>
        /// Gets or sets the primary category.
        /// </summary>
        public string PrimaryCategory { get; set; }

        /// <summary>
        /// Gets or sets the primary first sub category.
        /// </summary>
        public string PrimaryFirstSubCategory { get; set; }

        /// <summary>
        /// Gets or sets the primary second sub category.
        /// </summary>
        public string PrimarySecondSubCategory { get; set; }

        /// <summary>
        /// Gets or sets the localized privacy URL.
        /// </summary>
        public string PrivacyUrl { get; set; }

        /// <summary>
        /// Gets or sets the localized promotional text.
        /// </summary>
        public string PromotionalText { get; set; }

        /// <summary>
        /// Gets or sets the localized release notes.
        /// </summary>
        public Dictionary<string, string> ReleaseNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to run precheck before submitting to app review.
        /// </summary>
        public bool RunPreCheckBeforeSubmit { get; set; }

        /// <summary>
        /// Gets or sets the directory path to the folder containing screen shots.
        /// </summary>
        public DirectoryPath ScreenShotPath { get; set; }

        /// <summary>
        /// Gets or sets the secondary category.
        /// </summary>
        public string SecondaryCategory { get; set; }

        /// <summary>
        /// Gets or sets the secondary first sub category.
        /// </summary>
        public string SecondaryFirstSubCategory { get; set; }

        /// <summary>
        /// Gets or sets the secondary second sub category.
        /// </summary>
        public string SecondarySecondSubCategory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to update app version for submission.
        /// </summary>
        public bool SkipAppVersionUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip uploading an ipa or pkg to iTunes Connect.
        /// </summary>
        public bool SkipBinaryUpload { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to upload metadata.
        /// </summary>
        public bool SkipMetadataUpload { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether upload screen shots.
        /// </summary>
        public bool SkipScreenShots { get; set; }

        /// <summary>
        /// Gets or sets the extra submission information.
        /// </summary>
        public Dictionary<string, string> SubmissionInformation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to submit the new version for review after uploading everything.
        /// </summary>
        public bool SubmitForReview { get; set; }

        /// <summary>
        /// Gets or sets the localized subtitle.
        /// Note: subtitle must be a hash, with the language being the key.
        /// </summary>
        public Dictionary<string, string> Subtitle { get; set; }

        /// <summary>
        /// Gets or sets the localized support URL.
        /// </summary>
        public string SupportUrl { get; set; }

        /// <summary>
        /// Gets or sets the trade representative contact information.
        /// </summary>
        public Dictionary<string, string> TradeRepresentativeContactInformation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FastlaneDeliverConfiguration"/> class.
        /// </summary>
        public FastlaneDeliverConfiguration()
        {
            SubmissionInformation = AppReviewInformation = TradeRepresentativeContactInformation =
                Name = Description = KeyWords = Subtitle = ReleaseNotes = new Dictionary<string, string>();
        }
    }
}