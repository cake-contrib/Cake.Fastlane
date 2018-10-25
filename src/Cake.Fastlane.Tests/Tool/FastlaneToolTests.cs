using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cake.Fastlane.Tests
{
    public sealed class FastlaneToolTests
    {
        public class TheUpdateMethod
        {
            [Fact]
            public void Should_Add_Update_Fastlane()
            {
                // Given
                var fixture = new FastlaneToolFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("update_fastlane", result.Args);
            }
        }
    }
}