using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Fastlane
{
    /// <summary>
    /// Contains aliases for obtaining a <see cref="FastlaneProvider"/> instance.
    /// </summary>
    [CakeAliasCategory("Fastlane")]
    public static class FastlaneAliases
    {
        /// <summary>
        /// Gets the fastlane provider.
        /// </summary>
        /// <value>
        /// The fastlane provider.
        /// </value>
        private static FastlaneProvider FastlaneProvider { get; set; }

        /// <summary>
        /// Gets an instance of <see cref="FastlaneProvider"/> that can be used to interact with fast lane tools.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">context</exception>
        [CakePropertyAlias(Cache = true)]
        [CakeAliasCategory("Fastlane")]
        public static FastlaneProvider Fastlane(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return FastlaneProvider ?? (FastlaneProvider = new FastlaneProvider(context));
        }
    }
}