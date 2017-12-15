using System;

namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that defines functionality for fastlane tools.
    /// </summary>
    public interface IFastlaneProvider
    {
        /// <summary>
        /// Executes fastlane deliver with the specified configuration.
        /// </summary>
        /// <param name="deliverConfiguration"></param>
        void Deliver(FastlaneDeliverConfiguration deliverConfiguration);

        /// <summary>
        /// Executes fastlane deliver with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane deliver configuration action.</param>
        void Deliver(Action<FastlaneDeliverConfiguration> configurator);

        /// <summary>
        /// Executes fastlane match with the specified configuration.
        /// </summary>
        /// <param name="matchConfiguration">The fastlane match configuration.</param>
        void Match(FastlaneMatchConfiguration matchConfiguration = null);

        /// <summary>
        /// Executes fastlane match with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane match configuration action.</param>
        void Match(Action<FastlaneMatchConfiguration> configurator);

        /// <summary>
        /// Executes fastlane pem with the specified configuration.
        /// </summary>
        /// <param name="pemConfiguration"></param>
        void Pem(FastlanePemConfiguration pemConfiguration = null);

        /// <summary>
        /// Executes fastlane pem with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane pem configuration action</param>
        void Pem(Action<FastlanePemConfiguration> configurator);
    }
}