using Cake.Core.Tooling;

namespace Cake.Fastlane
{
    /// <summary>
    /// Configuration for fastlane.
    /// </summary>
    /// <seealso />
    public class FastlaneConfiguration : ToolSettings
    {
        /// <summary>
        /// Gets or sets the full name of the git user.
        /// </summary>
        public string GitFullName { get; set; }

        /// <summary>
        /// Gets or sets the git users email.
        /// </summary>
        public string GitUserEmail { get; set; }

        /// <summary>
        /// Gets or sets your Apple ID Username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the team identifier.
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// Gets or sets the bundle identifier(s) of your app (comma-separated).
        /// </summary>
        public string AppIdentifier { get; set; }
    }
}