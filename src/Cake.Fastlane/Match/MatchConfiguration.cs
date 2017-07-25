using Cake.Core.IO;

namespace Cake.Fastlane
{
    /// <summary>
    ///
    /// </summary>
    public class MatchConfiguration : FastlaneConfiguration
    {
        public CertificateType? CertificateType { get; set; }
        public string AppIdentifier { get; set; }
        public string UserName { get; set; }
        public string KeyChainName { get; set; }
        public string KeyChainPassword { get; set; }
        public string TeamName { get; set; }
        public bool Verbose { get; set; }
        public bool Force { get; set; }
        public bool SkipConfirmation { get; set; }
        public bool ShallowClone { get; set; }
        public bool ReadOnly { get; set; }
        public bool ForceForNewDevices { get; set; }
        public bool CloneBranchDirectly { get; set; }
        public FilePath Workspace { get; set; }
        public string Platform { get; set; }
    }
}