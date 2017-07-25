using Cake.Core;
using Cake.Testing;
using System;
using Xunit;

namespace Cake.Fastlane.Tests.Match
{
    public sealed class FastlaneMatchTests
    {
        public sealed class TheMatchMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
            }

            [Fact]
            public void Should_Throw_If_CSharp_Compiler_Runner_Was_Not_Found()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("fastlane: Could not locate executable.", result?.Message);
            }

            [Theory]
            [InlineData("/bin/tools/fastlane", "/bin/tools/fastlane")]
            [InlineData("./tools/fastlane", "/Working/tools/fastlane")]
            public void Should_Use_CSharp_Compiler_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.ToolPath = toolPath;
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public void Should_Find_CSharp_Compiler_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/fastlane", result.Path.FullPath);
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // Given
                var fixture = new FastlaneMatchFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.GivenProcessCannotStart();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("fastlane: Process was not started.", result?.Message);
            }

            [Fact]
            public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("fastlane: Process returned an error (exit code 1).", result?.Message);
            }

            [Fact]
            public void Should_Throw_If_Settings_Null()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\r\nParameter name: configuration", result?.Message);
            }

            [Fact]
            public void Should_Add_Match_If_No_Match_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match", result.Args);
            }

            [Theory]
            [InlineData(CertificateType.Development)]
            [InlineData(CertificateType.Development)]
            [InlineData(CertificateType.Development)]
            [InlineData(CertificateType.Development)]
            public void Should_Add_Certificate_Type_If_Provided(CertificateType certificateType)
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.CertificateType = certificateType;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"match {certificateType.ToString().ToLowerInvariant()}", result.Args);
            }

            [Fact]
            public void Should_Add_App_Identifier_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.AppIdentifier = "com.fastlane.cake.local";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"match -a {fixture.Settings.AppIdentifier}", result.Args);
            }

            [Fact]
            public void Should_Add_User_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.UserName = "username";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"match -u {fixture.Settings.UserName}", result.Args);
            }

            [Fact]
            public void Should_Add_Key_Chain_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.KeyChainName = "My Key Chain";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"match -s {fixture.Settings.KeyChainName}", result.Args);
            }

            [Fact]
            public void Should_Add_Force_For_New_Devices_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.ForceForNewDevices = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --force_for_new_devices", result.Args);
            }

            [Fact]
            public void Should_Add_Read_Only_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.ReadOnly = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --readonly", result.Args);
            }

            [Fact]
            public void Should_Add_Verbose_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.Verbose = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --verbose", result.Args);
            }
        }
    }
}