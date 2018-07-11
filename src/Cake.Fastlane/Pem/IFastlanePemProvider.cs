namespace Cake.Fastlane
{
    /// <summary>
    /// Interface that represents fastlane pem.
    /// </summary>
    internal interface IFastlanePemProvider
    {
        /// <summary>
        /// Executes fastlane pem with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        void Pem(FastlanePemConfiguration configuration);
    }
}