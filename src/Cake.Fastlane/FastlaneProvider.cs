using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Fastlane
{
    /// <summary>
    /// Provides functionality for fastlane tools.
    /// </summary>
    public sealed class FastlaneProvider : IFastlaneProvider
    {
        private readonly ICakeContext _context;
        private IFastlaneMatchProvider _fastlaneMatchProvider;
        private IFastlanePemProvider _fastlanePemProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FastlaneProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FastlaneProvider(ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Environment.Platform.Family != PlatformFamily.OSX)
            {
                throw new CakeException("Use of fastlane tools requires Mac OSX");
            }

            _context = context;
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane match with the specified configuration.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          var configuration = new FastlaneMatchConfiguration
        ///          {
        ///              CertificateType = CertificateType.Development,
        ///              AppIdentifier = "com.fastlane.cake",
        ///              ForceForNewDevices = true
        ///          };
        ///
        ///          Fastlane.Match(configuration);
        ///      </code>
        ///  </example>
        /// <param name="matchConfiguration">The fastlane match configuration.</param>
        [CakeAliasCategory("Match")]
        public void Match(FastlaneMatchConfiguration matchConfiguration = null)
        {
            if (_fastlaneMatchProvider == null)
            {
                _fastlaneMatchProvider = new FastlaneMatchProvider(_context.FileSystem,
                    _context.Environment,
                    _context.ProcessRunner,
                    _context.Tools);
            }

            _fastlaneMatchProvider.Match(matchConfiguration ?? new FastlaneMatchConfiguration());
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pem with the specified configuration action.
        /// </summary>
        /// <example>
        ///     <code>
        ///         Fastlane.Match(config =>
        ///         {
        ///             config.CertificateType = CertificateType.Development;
        ///             config.AppIdentifier = "com.fastlane.cake";
        ///             config.ForceForNewDevices = true;
        ///         });
        ///     </code>
        /// </example>
        /// <param name="configurator"></param>
        [CakeAliasCategory("Match")]
        public void Match(Action<FastlaneMatchConfiguration> configurator)
        {
            if (configurator == null)
            {
                throw new ArgumentNullException(nameof(configurator));
            }

            var configuration = new FastlaneMatchConfiguration();

            configurator(configuration);

            Match(configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pem with the specified configuration.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          var configuration = new FastlanePemConfiguration
        ///          {
        ///              Development = true,
        ///              ActiveDaysLimit = 45
        ///          };
        ///
        ///          Fastlane.Pem(configuration);
        ///      </code>
        ///  </example>
        /// <param name="configuration"></param>
        [CakeAliasCategory("Pem")]
        public void Pem(FastlanePemConfiguration configuration)
        {
            if (_fastlanePemProvider == null)
            {
                _fastlanePemProvider = new FastlanePemProvider(_context.FileSystem,
                _context.Environment,
                _context.ProcessRunner,
                _context.Tools);
            }

            _fastlanePemProvider.Pem(configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes fastlane pem with the specified configuration action.
        /// </summary>
        ///  <example>
        ///      <code>
        ///          Fastlane.Pem(config =>
        ///             {
        ///                 config.Development = true;
        ///                 config.ActiveDaysLimit = 45;
        ///             });
        ///      </code>
        ///  </example>
        /// <param name="configurator"></param>
        [CakeAliasCategory("Pem")]
        public void Pem(Action<FastlanePemConfiguration> configurator)
        {
            if (configurator == null)
            {
                throw new ArgumentNullException(nameof(configurator));
            }

            var configuration = new FastlanePemConfiguration();

            configurator(configuration);

            Pem(configuration);
        }
    }
}