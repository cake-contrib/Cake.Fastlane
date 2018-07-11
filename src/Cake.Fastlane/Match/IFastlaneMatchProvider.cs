using System;

namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that represents fastlane match.
    /// </summary>
    internal interface IFastlaneMatchProvider
    {
        /// <summary>
        /// Executes fastlane match with the specified configuration.
        /// </summary>
        /// <param name="matchConfiguration">The fastlane match configuration.</param>
        void Match(FastlaneMatchConfiguration matchConfiguration = null);

        /// <summary>
        /// Executes fastlane match with the specified configuration action.
        /// </summary>
        /// <param name="action">The action.</param>
        void Match(Action<FastlaneMatchConfiguration> action);
    }
}