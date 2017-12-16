using Cake.Core.IO;

namespace Cake.Fastlane
{
    /// <inheritdoc />
    /// <summary>
    /// Fastlane match configuration options.
    /// </summary>
    /// <seealso cref="T:Cake.Fastlane.FastlaneConfiguration" />
    public class MatchConfiguration : FastlaneConfiguration
    {
        /// <summary>
        /// Gets or sets the URL to the git repo containing all the certificates.
        /// </summary>
        public string GitUrl { get; set; }

        /// <summary>
        /// Gets or sets the specific git branch to use.
        /// </summary>
        public string GitBranch { get; set; }

        /// <summary>
        /// Gets or sets the type of the certificate.
        /// </summary>
        public CertificateType? CertificateType { get; set; }

        /// <summary>
        /// Gets or sets the bundle identifier(s) of your app (comma-separated).
        /// </summary>
        public string AppIdentifier { get; set; }

        /// <summary>
        /// Gets or sets your Apple ID Username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the keychain the items should be imported to.
        /// </summary>
        public string KeyChainName { get; set; }

        /// <summary>
        /// Gets or sets the key chain password.
        /// </summary>
        public string KeyChainPassword { get; set; }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the team identifier.
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to print out extra information and all commands.
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to renew the provisioning profiles every time you run match.
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable confirmation prompts during nuke, answering them with yes.
        /// </summary>
        public bool SkipConfirmation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to make a shallow clone of the repository (truncate the history to 1 revision).
        /// </summary>
        public bool ShallowClone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to renew the provisioning profiles if the device count on the developer portal has changed.
        /// </summary>
        public bool ForceForNewDevices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to clone just the branch specified, instead of the whole repo.
        /// This requires that the branch already exists. Otherwise the command will fail.
        /// </summary>
        public bool CloneBranchDirectly { get; set; }

        /// <summary>
        /// Gets or sets the workspace.
        /// </summary>
        public DirectoryPath Workspace { get; set; }

        /// <summary>
        /// Gets or sets the provisioning profile's platform to work with (i.e. ios, tvos)
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip generation of a README.md for the created git repository.
        /// </summary>
        public bool SkipDocs { get; set; }
    }
}