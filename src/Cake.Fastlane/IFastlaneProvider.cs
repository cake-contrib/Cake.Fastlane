namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that defines functionality for fastlane tools.
    /// </summary>
    public interface IFastlaneProvider
    {
        /// <summary>
        /// Executes fastlane match with the specified configuration.
        /// </summary>
        /// <param name="matchConfiguration">The fastlane match configuration.</param>
        void Match(MatchConfiguration matchConfiguration = null);
    }
}