namespace Cake.Fastlane
{
    /// <summary>
    /// Commands for the pilot tool.
    /// </summary>
    public enum PilotCommand
    {

        /// <summary>
        /// Uploads a build.
        /// </summary>
        Upload,

        /// <summary>
        /// Lists all the builds.
        /// </summary>
        Builds,

        /// <summary>
        /// Lists all the testers.
        /// </summary>
        List,

        /// <summary>
        /// Adds a new tester.
        /// </summary>
        Add,

        /// <summary>
        /// Finds a tester.
        /// </summary>
        Find,

        /// <summary>
        /// Remove external beta testers.
        /// </summary>
        Remove,

        /// <summary>
        /// Export all external testers to csv.
        /// </summary>
        Export,

        /// <summary>
        /// Add external testers from csv.
        /// </summary>
        Import
    }
}