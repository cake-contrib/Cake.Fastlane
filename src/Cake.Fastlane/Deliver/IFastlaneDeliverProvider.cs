using Cake.Core.IO;

namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that represents fastlane deliver.
    /// </summary>
    internal interface IFastlaneDeliverProvider
    {
        /// <summary>
        /// Executes fastlane deliver with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        void Deliver(FastlaneDeliverConfiguration configuration);
    }
}