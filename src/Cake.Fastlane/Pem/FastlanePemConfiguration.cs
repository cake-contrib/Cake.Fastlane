using Cake.Core.IO;

namespace Cake.Fastlane
{
    /// <inheritdoc />
    /// <summary>
    /// Fastlane Pem configuration options.
    /// </summary>
    public class FastlanePemConfiguration : FastlaneConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating if the current certificate is active for less than this number of days, generate a new one.
        /// </summary>
        public int? ActiveDaysLimit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to renew the development push certificate instead of the production one.
        /// </summary>
        public bool Development { get; set; }

        /// <summary>
        /// Gets or sets the file name of the generated .pem file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to create a new push certificate, even if the current one is active for 30 (or PEM_ACTIVE_DAYS_LIMIT) more days.
        /// </summary>
        public new bool Force { get; set; }

        /// <summary>
        /// Gets or sets a value determining whether to Generate a p12 file additionally to a PEM file.
        /// </summary>
        /// <value></value>
        public bool GenerateP12 { get; set; }

        /// <summary>
        /// Gets or sets the path to a directory in which all certificates and private keys should be stored.
        /// </summary>
        public DirectoryPath OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the password that is used for your p12 file.
        /// </summary>
        public string P12Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to save the private RSA key.
        /// </summary>
        public bool SavePrivateKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FastlanePemConfiguration"/> class.
        /// </summary>
        public FastlanePemConfiguration()
        {
            GenerateP12 = true;
            ActiveDaysLimit = 30;
        }
    }
}