namespace Cake.Fastlane
{
    /// <summary>
    /// Enumeration for the certificate type used for match.
    /// </summary>
    public enum CertificateType
    {
        /// <summary>
        /// Development certificate
        /// </summary>
        Development = 0,

        /// <summary>
        /// Ad Hoc certificate.
        /// </summary>
        AdHoc,

        /// <summary>
        /// App store certificate.
        /// </summary>
        AppStore,

        /// <summary>
        /// Enterprise certificate.
        /// </summary>
        Enterprise
    }
}