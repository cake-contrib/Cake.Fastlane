using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Testing;
using Xunit;

namespace Cake.Fastlane.Tests.Deliver
{
    public sealed class FastlaneDeliverProviderTests
    {
        private static readonly Func<Dictionary<string, string>, string> Aggregate = (dictionary) =>
        {
            return dictionary.Aggregate(string.Empty, (current, hash) => current + $"{hash.Key}:{hash.Value}")
                .TrimEnd(',');
        };

        public sealed class TheDeliverMethod
        {
            [Fact]
            public void Should_Add_App_Icon_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AppIcon = new FilePath("./cakeicon.png");

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -l \"/Working/{fixture.Settings.AppIcon}\"", result.Args);
            }

            [Fact]
            public void Should_Add_App_Id_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AppId = "123456";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -p {fixture.Settings.AppId}", result.Args);
            }

            [Fact]
            public void Should_Add_App_Identifier_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AppIdentifier = "com.fastlane.cake.local";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -a {fixture.Settings.AppIdentifier}", result.Args);
            }

            [Fact]
            public void Should_Add_App_Rating_Config_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AppRatingConfigPath = new FilePath("./config");

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -g \"/Working/{fixture.Settings.AppRatingConfigPath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_App_Review_Information_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AppReviewInformation = new Dictionary<string, string>
                {
                    { "first_name", "Cake"},
                    { "last_name", "Contrib"},
                    { "phone_number", "+43 123123123"},
                    { "email_address", "cake.contrib@cake.com"},
                    { "demo_user", "cake-contrib"},
                    { "notes", "note"}
                };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --app_review_information", result.Args);
            }

            [Fact]
            public void Should_Add_App_Version_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AppVersion = "1.0.1";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -z {fixture.Settings.AppVersion}", result.Args);
            }

            [Fact]
            public void Should_Add_Apple_Watch_Icon_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AppleWatchIcon = new FilePath("./cakeicon.png");

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -q \"/Working/{fixture.Settings.AppleWatchIcon}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Auto_Release_Date_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AutomaticReleaseDate = DateTimeOffset.Now;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --auto_release_date {fixture.Settings.AutomaticReleaseDate.Value.Millisecond}", result.Args);
            }

            [Fact]
            public void Should_Add_Automatic_Release_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.AutomaticRelease = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver --automatic_release", result.Args);
            }

            [Fact]
            public void Should_Add_Build_Number_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.BuildNumber = "1.3.1.10004";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -n {fixture.Settings.BuildNumber}", result.Args);
            }

            [Fact]
            public void Should_Add_Copyright_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.Copyright = "Cake Copyright";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --copyright {fixture.Settings.Copyright}", result.Args);
            }

            [Fact]
            public void Should_Add_Deliver_If_No_Configuration_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver", result.Args);
            }

            [Fact]
            public void Should_Add_Description_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.Description = new Dictionary<string, string>
                {
                    {"en-Us", "This is a description on cake.fastlane"},
                    {"de-DE", "Dies ist eine Beschreibung auf Cake.Fastlane"}
                };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --description", result.Args);
            }

            [Fact]
            public void Should_Add_Dev_Portal_Team_Id_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.DevPortalTeamId = "13579";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -s {fixture.Settings.DevPortalTeamId}", result.Args);
            }

            [Fact]
            public void Should_Add_Dev_Portal_Team_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.DevPortalTeamName = "Cake Contrib";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -y {fixture.Settings.DevPortalTeamName}", result.Args);
            }

            [Fact]
            public void Should_Add_Edit_Live_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.EditLive = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -o", result.Args);
            }

            [Fact]
            public void Should_Add_Force_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.Force = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver -f", result.Args);
            }

            [Fact]
            public void Should_Add_Ipa_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.IpaPath = new FilePath("./cake.ipa");

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -i \"/Working/{fixture.Settings.IpaPath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Itc_Provider_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.ItcProvider = "Itc Provider Name";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --itc_provider {fixture.Settings.ItcProvider}", result.Args);
            }

            [Fact]
            public void Should_Add_Keywords_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.KeyWords = new Dictionary<string, string>
                {
                    {"key", "word"}
                };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --keywords", result.Args);
            }

            [Fact]
            public void Should_Add_Marketing_Url_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.MarketingUrl = "http://cake.fastlane.com/marketing";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --marketing_url {fixture.Settings.MarketingUrl}", result.Args);
            }

            [Fact]
            public void Should_Add_Metadata_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.MetaDataPath = "./metadata";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -m \"/Working/{fixture.Settings.MetaDataPath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.Name = new Dictionary<string, string>
                {
                    {"en-US", "cake.fastlane"}
                };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --name", result.Args);
            }

            [Fact]
            public void Should_Add_Overwrite_Screen_Shots_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.OverWriteScreenShots = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --overwrite_screenshots", result.Args);
            }

            [Fact]
            public void Should_Add_Phased_Release_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.PhasedRelease = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver --phased_release", result.Args);
            }

            [Fact]
            public void Should_Add_Pkg_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.PkgPath = new FilePath("./cake.pkg");

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -c \"/Working/{fixture.Settings.PkgPath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Platform_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.Platform = "MacOS";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -j {fixture.Settings.Platform}", result.Args);
            }

            [Fact]
            public void Should_Add_Price_Tier_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.PriceTier = 1;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -r {fixture.Settings.PriceTier}", result.Args);
            }

            [Fact]
            public void Should_Add_Primary_Category_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.PrimaryCategory = "First Category";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --primary_category {fixture.Settings.PrimaryCategory}", result.Args);
            }

            [Fact]
            public void Should_Add_Primary_First_Sub_Category_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.PrimaryFirstSubCategory = "Primary First Sub Category";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --primary_first_sub_category {fixture.Settings.PrimaryFirstSubCategory}", result.Args);
            }

            [Fact]
            public void Should_Add_Primary_Second_Sub_Category_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.PrimarySecondSubCategory = "Primary Second Sub Category";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --primary_second_sub_category {fixture.Settings.PrimarySecondSubCategory}", result.Args);
            }

            [Fact]
            public void Should_Add_Privacy_Url_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.PrivacyUrl = "http://cake.fastlane.com/privacy";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --privacy_url {fixture.Settings.PrivacyUrl}", result.Args);
            }

            [Fact]
            public void Should_Add_Release_Notes_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.ReleaseNotes = new Dictionary<string, string>
                {
                    {"en-US", "This is a description on cake.fastlane"}
                };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --release_notes", result.Args);
            }

            [Fact]
            public void Should_Add_Screen_Shots_Path_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.ScreenShotPath = new DirectoryPath("./screenshots");

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -w \"/Working/{fixture.Settings.ScreenShotPath}\"", result.Args);
            }

            [Fact]
            public void Should_Add_Secondary_Category_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SecondaryCategory = "Second Category";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --secondary_category {fixture.Settings.SecondaryCategory}", result.Args);
            }

            [Fact]
            public void Should_Add_Secondary_First_Sub_Category_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SecondaryFirstSubCategory = "Secondary First Sub Category";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --secondary_first_sub_category {fixture.Settings.SecondaryFirstSubCategory}", result.Args);
            }

            [Fact]
            public void Should_Add_Secondary_Second_Sub_Category_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SecondarySecondSubCategory = "Secondary Second Sub Category";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --secondary_second_sub_category {fixture.Settings.SecondarySecondSubCategory}", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Binary_Upload_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SkipBinaryUpload = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver --skip_binary_upload", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Metadata_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SkipMetadataUpload = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver --skip_metadata", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Screen_Shots_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SkipScreenShots = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver --skip_screenshots", result.Args);
            }

            [Fact]
            public void Should_Add_Submission_Information_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SubmissionInformation = new Dictionary<string, string>
                {
                    {"submit", "me" }
                };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -b", result.Args);
            }

            [Fact]
            public void Should_Add_Submit_For_Review_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SubmitForReview = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("deliver --submit_for_review", result.Args);
            }

            [Fact]
            public void Should_Add_Support_Url_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.SupportUrl = "http://cake.fastlane.com/support";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver --support_url {fixture.Settings.SupportUrl}", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Id_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.TeamId = "456";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -k {fixture.Settings.TeamId}", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.TeamName = "NY Mets";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -e {fixture.Settings.TeamName}", result.Args);
            }

            [Fact]
            public void Should_Add_User_Name_If_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.UserName = "username";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"deliver -u {fixture.Settings.UserName}", result.Args);
            }

            [Fact]
            public void Should_Find_Fastlane_Deliver_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/fastlane", result.Path.FullPath);
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Fastlane_Deliver_Runner_Was_Not_Found()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("fastlane: Could not locate executable.", result?.Message);
            }

            [Fact]
            public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("fastlane: Process returned an error (exit code 1).", result?.Message);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.GivenProcessCannotStart();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("fastlane: Process was not started.", result?.Message);
            }

            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
            }

            [WindowsFact]
            public void Should_Throw_If_Settings_Null()
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
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
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("Value cannot be null.\nParameter name: configuration", result?.Message);
            }

            [Theory]
            [InlineData("/bin/tools/fastlane", "/bin/tools/fastlane")]
            [InlineData("./tools/fastlane", "/Working/tools/fastlane")]
            public void Should_Use_Fastlane_Deliver_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new FastlaneDeliverProviderFixture();
                fixture.Settings.ToolPath = toolPath;
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }
        }
    }
}