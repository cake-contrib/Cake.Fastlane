using System.Collections.Generic;
using System.Linq;
using Cake.Core.IO;

namespace Cake.Fastlane
{
    /// <summary>
    /// Fastlane supply configuration options.
    /// </summary>
    /// <seealso cref="Cake.Fastlane.FastlaneConfiguration" />
    public class FastlaneSupplyConfiguration : FastlaneConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FastlaneSupplyConfiguration"/> class.
        /// </summary>
        public FastlaneSupplyConfiguration()
        {
            ApkFiles = MappingFiles = Enumerable.Empty<FilePath>();
        }

        /// <summary>
        /// Gets or sets the package name of the application to use.
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the track of the application to use.
        /// The default available tracks are: #{default_tracks.join(', ')}.
        /// </summary>
        public string Track { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the user fraction when uploading to the rollout track.
        /// </summary>
        public double? Rollout { get; set; }

        /// <summary>
        /// Gets or sets the path to the directory containing the metadata files.
        /// </summary>
        public DirectoryPath MetadataPath { get; set; }

        /// <summary>
        /// Gets or sets the p12 file used to authenticate with Google.
        /// </summary>
        public FilePath KeyFilePath { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the p12 file (email addressof the services account).
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the path to a file containing service account JSON, used to authenticate with Google.
        /// </summary>
        public FilePath JsonKeyFilePath { get; set; }

        /// <summary>
        /// Gets or sets the raw service account JSON data used to authenticate with Google.
        /// </summary>
        public string JsonKeyData { get; set; }

        /// <summary>
        /// Gets or sets the file path to the APK to upload.
        /// </summary>
        public FilePath ApkFilePath { get; set; }

        /// <summary>
        /// Gets or sets an array of APK file paths to upload.
        /// </summary>
        public IEnumerable<FilePath> ApkFiles { get; set; }

        /// <summary>
        /// Gets or sets the file path to the AAB file to upload.
        /// </summary>
        public FilePath AAB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip uploading APK.
        /// </summary>
        public bool SkipUploadApk { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip uploading AAB.
        /// </summary>
        public bool SkipUploadAAB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip uploading metadata.
        /// </summary>
        public bool SkipUploadMetadata { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip uploading images.
        /// </summary>
        public bool SkipUploadImages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip uploading screen shots.
        /// </summary>
        public bool SkipUploadScreenShots { get; set; }

        /// <summary>
        /// Gets or sets the track to promote to.
        /// </summary>
        public string PromoteTrack { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to only validate changes with Google Play rather than actually publish.
        /// </summary>
        public bool ValidateOnly { get; set; }

        /// <summary>
        /// Gets or sets the file path to the mapping file to upload
        /// </summary>
        public FilePath Mapping { get; set; }

        /// <summary>
        /// Gets or sets the root URL for the Google Play API.
        /// The provided URL will be used for API calls in place of https://www.googleapis.com/"
        /// </summary>
        public string RootUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to check the other tracks for superseded versions and disable them.
        /// </summary>
        public bool CheckSupersededTracks { get; set; }

        /// <summary>
        /// Gets or sets the array of paths to mapping files to upload.
        /// </summary>
        public IEnumerable<FilePath> MappingFiles { get; set; }
    }
}