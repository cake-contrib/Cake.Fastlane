using Cake.Core.Tooling;
using System.Collections.Generic;

namespace Cake.Fastlane
{
    public class FastlaneConfiguration : ToolSettings
    {
        public string GitFullName { get; set; }
        public string GitUserEmail { get; set; }
        public IDictionary<string, string> EnvironmentVariables { get; set; }
    }
}