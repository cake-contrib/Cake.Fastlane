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
            public void Should_Throw_If_Fastlane_Match_Runner_Was_Not_Found()
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
            public void Should_Use_Fastlane_Match_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
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
            public void Should_Find_Fastlane_Match_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/fastlane.exe", result.Path.FullPath);
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

            [WindowsFact]
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

            [OSXFact]
            public void Should_Throw_If_Settings_Null_OSX()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\nParameter name: configuration", result?.Message);
            }

            [Fact]
            public void Should_Add_Match_If_No_Configuration_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match", result.Args);
            }

            [Fact]
            public void Should_Add_Git_URL_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.GitUrl = "https://cake.fastlane.org";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match -r https://cake.fastlane.org", result.Args);
            }

            [Fact]
            public void Should_Add_Git_Branch_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.GitBranch = "develop";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --git_branch develop", result.Args);
            }

            [Theory]
            [InlineData(CertificateType.Development)]
            [InlineData(CertificateType.AdHoc)]
            [InlineData(CertificateType.Enterprise)]
            [InlineData(CertificateType.AppStore)]
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
            public void Should_Add_Key_Chain_Password_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.KeyChainPassword = "password";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"match -p [REDACTED]", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Id_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.TeamId = "456";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"match -b {fixture.Settings.TeamId}", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.TeamName = "NY Mets";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"match -l {fixture.Settings.TeamName}", result.Args);
            }

            [Fact]
            public void Should_Add_Force_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.Force = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --force", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Confirmation_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.SkipConfirmation = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --skip_confirmation", result.Args);
            }

            [Fact]
            public void Should_Add_Shallow_Clone_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.ShallowClone = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --shallow_clone", result.Args);
            }

            [Fact]
            public void Should_Add_Clone_Branch_Directly_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.CloneBranchDirectly = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --clone_branch_directly", result.Args);
            }

            [Fact]
            public void Should_Add_Workspace_If_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.Workspace = "./usr/";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --workspace \"/Working/usr\"", result.Args);
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
            public void Should_Add_Skip_Docs_Provided()
            {
                // Given
                var fixture = new FastlaneMatchFixture();
                fixture.Settings.SkipDocs = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("match --skip_docs", result.Args);
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