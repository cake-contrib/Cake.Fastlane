using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Testing;
using Xunit;

namespace Cake.Fastlane.Tests.Supply
{
    public sealed class FastlaneSupplyTests
    {
        public sealed class TheSupplyMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
            }

            [Fact]
            public void Should_Throw_If_Fastlane_Pilot_Runner_Was_Not_Found()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
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
            public void Should_Use_Fastlane_Pilot_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.ToolPath = toolPath;
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public void Should_Find_Fastlane_Pilot_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/fastlane.exe", result.Path.FullPath);
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
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
                var fixture = new FastlaneSupplyFixture();
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
                var fixture = new FastlaneSupplyFixture();
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
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\nParameter name: configuration", result?.Message);
            }

            [Fact]
            public void Should_Add_Action_If_No_Configuration_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply", result.Args);
            }

            [Fact]
            public void Should_Add_Package_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.PackageName = "com.cake.fastlane";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -p {fixture.Settings.PackageName}", result.Args);
            }

            [Fact]
            public void Should_Add_Track_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.Track = "fast track";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -a {fixture.Settings.Track}", result.Args);
            }

            [Fact]
            public void Should_Add_Rollout_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.Rollout = .3;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -r {fixture.Settings.Rollout}", result.Args);
            }

            [Fact]
            public void Should_Add_Metadata_File_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.MetadataPath = "./artifacts/metadata";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -m \"/Working/{fixture.Settings.MetadataPath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Key_File_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.KeyFilePath = "./build/android/key.p12";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -k \"/Working/{fixture.Settings.KeyFilePath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Issuer_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.Issuer = "issuer@cake.com";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -i {fixture.Settings.Issuer}", result.Args);
            }

            [Fact]
            public void Should_Add_Json_Key_File_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.JsonKeyFilePath = "./build/android/key.json";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -j \"/Working/{fixture.Settings.JsonKeyFilePath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Json_Key_Data_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.JsonKeyData = "{}";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -c {fixture.Settings.JsonKeyData}", result.Args);
            }

            [Fact]
            public void Should_Add_Apk_File_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.ApkFilePath = "./cake.fastlane.apk";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -b \"/Working/{fixture.Settings.ApkFilePath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Apk_Files_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.ApkFiles = new FilePath[] {"cake.fastlane.apk", "supply.fastlane.apk"};

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply -u \"/Working/cake.fastlane.apk\",\"/Working/supply.fastlane.apk\"", result.Args);
            }

            [Fact]
            public void Should_Add_AAB_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.AAB = "./cake.fastlane.aab";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -f \"/Working/{fixture.Settings.AAB}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Upload_Apk_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.SkipUploadApk = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply --skip_upload_apk", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Upload_Aab_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.SkipUploadAAB = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply --skip_upload_aab", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Upload_Metadata_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.SkipUploadMetadata = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply --skip_upload_metadata", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Upload_Images_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.SkipUploadImages = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply --skip_upload_images", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Upload_Screen_Shots_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.SkipUploadScreenShots = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply --skip_upload_screenshots", result.Args);
            }

            [Fact]
            public void Should_Add_Promote_Track_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.PromoteTrack = "com.cake.fastlane";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply --track_promote_to {fixture.Settings.PromoteTrack}", result.Args);
            }

            [Fact]
            public void Should_Add_Validate_Only_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.ValidateOnly = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply --validate_only", result.Args);
            }

            [Fact]
            public void Should_Add_Mapping_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.Mapping = "./cake.fastlane.map";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -d \"/Working/{fixture.Settings.Mapping}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Mapping_Files_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.MappingFiles = new FilePath[] {"cake.map", "fastlane.map"};

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply -s \"/Working/cake.map\",\"/Working/fastlane.map\"", result.Args);
            }

            [Fact]
            public void Should_Add_Root_Url_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.RootUrl = "https://fastlane.cake.com";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"supply --root_url {fixture.Settings.RootUrl}", result.Args);
            }

            [Fact]
            public void Should_Add_Check_Superseded_Tracks_If_Provided()
            {
                // Given
                var fixture = new FastlaneSupplyFixture();
                fixture.Settings.CheckSupersededTracks = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("supply --check_superseded_tracks", result.Args);
            }
        }
    }
}