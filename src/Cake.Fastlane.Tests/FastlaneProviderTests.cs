using Cake.Core;
using System;
using Xunit;

namespace Cake.Fastlane.Tests
{
    public sealed class FastlaneProviderTests
    {
        public sealed class TheConstructorMethod
        {
            [WindowsFact]
            public void Should_Throw_If_Context_Null()
            {
                // Given, When
                var result = Record.Exception(() => new FastlaneProvider(null));

                // Then
                Assert.NotNull(result);
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\r\nParameter name: context", result.Message);
            }
            
            [OSXFact]
            public void Should_Throw_If_Context_Null_OSX()
            {
                // Given, When
                var result = Record.Exception(() => new FastlaneProvider(null));

                // Then
                Assert.NotNull(result);
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\nParameter name: context", result.Message);
            }

            [Theory]
            [InlineData(PlatformFamily.Unknown)]
            [InlineData(PlatformFamily.Linux)]
            [InlineData(PlatformFamily.Windows)]
            public void Should_Throw_If_Platform_Not_Valid(PlatformFamily platform)
            {
                // Given
                var context = new CakeContextFixture().CreateContext(platform);

                // When
                var result = Record.Exception(() => new FastlaneProvider(context));

                // Then
                Assert.NotNull(result);
                Assert.IsType<CakeException>(result);
            }
        }
    }
}