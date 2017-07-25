namespace Cake.Fastlane
{
    /// <summary>
    /// Represents a service that communicates with fastlane.
    /// </summary>
    public interface IFastlaneMatchProvider
    {
        /// <summary>
        /// Initializes the current folder for use with fastlane tools creates a file.
        /// </summary>
        void Initialize();

        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        void Match(MatchConfiguration configuration);
    }
}