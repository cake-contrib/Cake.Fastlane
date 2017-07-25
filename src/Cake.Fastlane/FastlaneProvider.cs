using Cake.Core;
using System;

namespace Cake.Fastlane
{
    /// <summary>
    /// Provides functionality for fastlane tools.
    /// </summary>
    public sealed class FastlaneProvider
    {
        private readonly IFastlaneMatchProvider _fastlaneMatchProvider;

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

            context.Tools.RegisterFile("fastlane");

            _fastlaneMatchProvider = new FastlaneMatchProvider(context.FileSystem,
                context.Environment,
                context.ProcessRunner,
                context.Tools);
        }

        public void Match(MatchConfiguration match = null)
        {
            _fastlaneMatchProvider.Match(match ?? new MatchConfiguration());
        }
    }
}