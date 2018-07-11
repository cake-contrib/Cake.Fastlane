using System;
using Cake.Core;
using Cake.Testing;
using Xunit;

namespace Cake.Fastlane.Tests.Pem
{
    public sealed class FastlanePemTests
    {
        public sealed class ThePemMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
            }

            [Fact]
            public void Should_Throw_If_Fastlane_Pem_Runner_Was_Not_Found()
            {
                // Given
                var fixture = new FastlanePemFixture();
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
            public void Should_Use_Fastlane_Pem_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.ToolPath = toolPath;
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public void Should_Find_Fastlane_Pem_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/fastlane.exe", result.Path.FullPath);
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // Given
                var fixture = new FastlanePemFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new FastlanePemFixture();
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
                var fixture = new FastlanePemFixture();
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("fastlane: Process returned an error (exit code 1).", result?.Message);
            }

            [WindowsFact]
            public void Should_Throw_If_Configuration_Null()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\r\nParameter name: configuration", result?.Message);
            }

            [OSXFact]
            public void Should_Throw_If_Configuration_Null_OSX()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\nParameter name: configuration", result?.Message);
            }

            [Fact]
            public void Should_Add_Pem_If_No_Configuration_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem", result.Args);
            }

            [Fact]
            public void Should_Add_Generate_P12_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.GenerateP12 = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem --generate_p12", result.Args);
            }

            [Fact]
            public void Should_Add_Active_Days_Limit_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.ActiveDaysLimit = 60;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem --active_days_limit 60", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Active_Days_Limit_If_Default()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.ActiveDaysLimit = 30;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Active_Days_Limit_If_Null()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.ActiveDaysLimit = null;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem", result.Args);
            }

            [Fact]
            public void Should_Add_Force_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.Force = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem --force", result.Args);
            }

            [Fact]
            public void Should_Add_Save_Private_Key_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.SavePrivateKey = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem --save_private_key", result.Args);
            }

            [Fact]
            public void Should_Add_App_Identifier_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.AppIdentifier = "com.fastlane.cake.local";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pem --app_identifier {fixture.Settings.AppIdentifier}", result.Args);
            }

            [Fact]
            public void Should_Add_User_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.UserName = "fastlane@cake.com";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pem -u {fixture.Settings.UserName}", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Id_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.TeamId = "456";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pem -b {fixture.Settings.TeamId}", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.TeamName = "NY Mets";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pem -l {fixture.Settings.TeamName}", result.Args);
            }

            [Fact]
            public void Should_Add_P12_Password_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.P12Password = "password";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem --p12_password [REDACTED]", result.Args);
            }

            [Fact]
            public void Should_Add_File_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.FileName = "cake-pem";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pem --pem_name {fixture.Settings.FileName}", result.Args);
            }

            [Fact]
            public void Should_Add_Output_Path_If_Provided()
            {
                // Given
                var fixture = new FastlanePemFixture();
                fixture.Settings.OutputPath = "./";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pem --output_path \"/Working\"", result.Args);
            }
        }
    }
}