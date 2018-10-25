namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that defines functionality for the fastlane tool.
    /// </summary>
    internal interface IFastlaneToolProvider
    {
        /// <summary>
        /// Executes fastlane updating to the latest version of fastlane.
        /// </summary>
        void Update();
    }
}