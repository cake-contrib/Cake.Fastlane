namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that represents fastlane deliver.
    /// </summary>
    public interface IFastlaneDeliverProvider
    {
        /// <summary>
        /// Executes fastlane deliver with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        void Deliver(FastlaneDeliverConfiguration configuration);
    }
}