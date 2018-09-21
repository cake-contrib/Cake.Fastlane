using System;
using Cake.Core;
using Cake.Testing;
using Xunit;

namespace Cake.Fastlane.Tests.Pilot
{
    public sealed class FastlanePilotTests
    {
        public sealed class ThePilotMethod
        { 
            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                var fixture = new FastlanePilotFixture();
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
                var fixture = new FastlanePilotFixture();
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
                var fixture = new FastlanePilotFixture();
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
                var fixture = new FastlanePilotFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/fastlane.exe", result.Path.FullPath);
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // Given
                var fixture = new FastlanePilotFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new FastlanePilotFixture();
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
                var fixture = new FastlanePilotFixture();
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
                var fixture = new FastlanePilotFixture();
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
                var fixture = new FastlanePilotFixture();
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
                var fixture = new FastlanePilotFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pilot upload", result.Args);
            }

            [Theory]
            [InlineData(PilotCommand.Upload)]
            [InlineData(PilotCommand.Builds)]
            [InlineData(PilotCommand.List)]
            [InlineData(PilotCommand.Find)]
            [InlineData(PilotCommand.Add)]
            [InlineData(PilotCommand.Remove)]
            [InlineData(PilotCommand.Import)]
            [InlineData(PilotCommand.Export)]
            public void Should_Add_Command_If_Provided(PilotCommand command)
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.Command = command;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot {command.ToString().ToLower()}", result.Args);
            }

            [Fact]
            public void Should_Add_User_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.UserName = "user";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -u {fixture.Settings.UserName}", result.Args);
            }

            [Fact]
            public void Should_Add_App_Identifier_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.AppIdentifier = "com.cake.fastlane";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -a {fixture.Settings.AppIdentifier}", result.Args);
            }

            [Fact]
            public void Should_Add_App_Platform_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.Platform = "osx";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -m {fixture.Settings.Platform}", result.Args);
            }

            [Fact]
            public void Should_Add_Ipa_File_Path_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.IpaFilePath = "./cake.fastlane.ipa";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -i \"/Working/cake.fastlane.ipa\"", result.Args);
            }

            [Fact]
            public void Should_Add_Change_Log_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.ChangeLog = "Cake is what is new.";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -w {fixture.Settings.ChangeLog}", result.Args);
            }

            [Fact]
            public void Should_Add_Beta_App_Description_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.BetaAppDescription = "Cake in the fast lane!";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -d {fixture.Settings.BetaAppDescription}", result.Args);
            }

            [Fact]
            public void Should_Add_Beta_App_Feedback_Email_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.BetaAppFeedbackEmail = "feedback@dotnotreply.com";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -n {fixture.Settings.BetaAppFeedbackEmail}", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Submission_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.SkipSubmission = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -s", result.Args);
            }

            [Fact]
            public void Should_Add_Skip_Waiting_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.SkipWaiting = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -z", result.Args);
            }

            [Fact]
            public void Should_Add_Apple_Id_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.AppleId = "cake@appleid.com";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -p {fixture.Settings.AppleId}", result.Args);
            }

            [Fact]
            public void Should_Add_Distribute_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.Distribute = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pilot upload --distribute_external", result.Args);
            }

            [Fact]
            public void Should_Add_Demo_Account_Required_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.DemoAccountRequired = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pilot upload --demo_account_required", result.Args);
            }

            [Fact]
            public void Should_Add_First_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.FirstName = "Cake";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -f {fixture.Settings.FirstName}", result.Args);
            }

            [Fact]
            public void Should_Add_Last_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.LastName = "Build";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -l {fixture.Settings.LastName}", result.Args);
            }

            [Fact]
            public void Should_Add_Email_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.Email = "cake@email.com";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -e {fixture.Settings.Email}", result.Args);
            }

            [Fact]
            public void Should_Add_Tester_File_Path_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.TesterFilePath = "./testers.csv";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -c \"/Working/testers.csv\"", result.Args);
            }

            [Fact]
            public void Should_Add_Wait_Processing_Interval_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.WaitProcessingInterval = 45;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -k {fixture.Settings.WaitProcessingInterval}", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Id_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.TeamId = "456";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -q {fixture.Settings.TeamId}", result.Args);
            }

            [Fact]
            public void Should_Add_Team_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.TeamName = "NY Mets";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -r {fixture.Settings.TeamName}", result.Args);
            }

            [Fact]
            public void Should_Add_Portal_Team_Name_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.PortalTeamId = "NY Mets";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload --dev_portal_team_id {fixture.Settings.PortalTeamId}", result.Args);
            }

            [Fact]
            public void Should_Add_Itc_Provider_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.ItcProvider = "provider";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload --itc_provider {fixture.Settings.ItcProvider}", result.Args);
            }

            [Fact]
            public void Should_Add_Groups_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.Groups = new[] {"Team Wilson", "Brady Bunch"};

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"pilot upload -g \"Team Wilson\",\"Brady Bunch\"", result.Args);
            }

            [Fact]
            public void Should_Add_Wait_For_Build_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.WaitForUploadedBuild = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pilot upload --wait_for_uploaded_build", result.Args);
            }

            [Fact]
            public void Should_Add_Reject_Build_Waiting_For_Review_If_Provided()
            {
                // Given
                var fixture = new FastlanePilotFixture();
                fixture.Settings.RejectBuildWaitingForReview = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("pilot upload --reject_build_waiting_for_review", result.Args);
            }
        }
    }
}