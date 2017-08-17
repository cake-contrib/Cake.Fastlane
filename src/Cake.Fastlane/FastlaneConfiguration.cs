using Cake.Core.Tooling;

namespace Cake.Fastlane
{
    /// <summary>
    /// Configuration for fastlane.
    /// </summary>
    /// <seealso />
    public class FastlaneConfiguration : ToolSettings
    {
        public string GitFullName { get; set; }
        public string GitUserEmail { get; set; }
    }
}