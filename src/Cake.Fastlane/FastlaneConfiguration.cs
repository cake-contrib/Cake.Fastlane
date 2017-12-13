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
    }
}