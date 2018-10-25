using System;

namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that defines functionality for fastlane tools.
    /// </summary>
    internal interface IFastlaneProvider : IFastlaneToolProvider,
        IFastlaneMatchProvider,
        IFastlaneDeliverProvider,
        IFastlanePemProvider,
        IFastlanePilotProvider,
        IFastlaneSupplyProvider
    {
        /// <summary>
        /// Executes fastlane deliver with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane deliver configuration action.</param>
        void Deliver(Action<FastlaneDeliverConfiguration> configurator);

        /// <summary>
        /// Executes fastlane match with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane match configuration action.</param>
        void Match(Action<FastlaneMatchConfiguration> configurator);

        /// <summary>
        /// Executes fastlane pem with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane pem configuration action</param>
        void Pem(Action<FastlanePemConfiguration> configurator);

        /// <summary>
        /// Executes fastlane pilot with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane pilot configuration action</param>
        void Pilot(Action<FastlanePilotConfiguration> configurator);

        /// <summary>
        /// Executes fastlane supply with the specified configuration action.
        /// </summary>
        /// <param name="configurator">The fastlane supply configuration action</param>
        void Supply(Action<FastlaneSupplyConfiguration> configurator);
    }
}