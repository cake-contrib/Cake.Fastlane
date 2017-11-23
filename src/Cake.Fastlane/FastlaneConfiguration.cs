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
        /// Gets or sets the full name of the git.
        /// </summary>
        /// <value>
        /// The full name of the git.
        /// </value>
        public string GitFullName { get; set; }

        /// <summary>
        /// Gets or sets the git user email.
        /// </summary>
        /// <value>
        /// The git user email.
        /// </value>
        public string GitUserEmail { get; set; }
    }
}