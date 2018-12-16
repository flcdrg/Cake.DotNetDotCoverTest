using Cake.Testing;
using System;
using Xunit;

namespace DotNetCoreDotCoverTest.Tests
{
    public class DotNetCoreDotCoverTesterTests
    {
        public sealed class TheTestMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();
                fixture.Project = "./src/*";
                fixture.Settings = null;
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                AssertEx.IsArgumentNullException(result, "settings");
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();
                fixture.Project = "./src/*";
                fixture.GivenProcessCannotStart();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                AssertEx.IsCakeException(result, ".NET Core CLI: Process was not started.");
            }

            [Fact]
            public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();
                fixture.Project = "./src/*";
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                AssertEx.IsCakeException(result, ".NET Core CLI: Process returned an error (exit code 1).");
            }

            [Fact]
            public void Should_Add_Mandatory_Arguments()
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("dotcover test", result.Args);
            }

            [Fact]
            public void Should_Add_Path()
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();
                fixture.Project = "./test/Project.Tests/*";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("dotcover test \"./test/Project.Tests/*\"", result.Args);
            }

            [Theory]
            [InlineData("./test/*", "dotcover test \"./test/*\"")]
            [InlineData("./test/cake unit tests/", "dotcover test \"./test/cake unit tests/\"")]
            [InlineData("./test/cake unit tests/cake core tests", "dotcover test \"./test/cake unit tests/cake core tests\"")]
            public void Should_Quote_Project_Path(string text, string expected)
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();
                fixture.Project = text;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Args);
            }

            [Fact]
            public void Should_Add_Additional_Settings()
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();
                fixture.Settings.NoBuild = true;
                fixture.Settings.NoRestore = true;
                fixture.Settings.Framework = "dnxcore50";
                fixture.Settings.Configuration = "Release";
                fixture.Settings.OutputDirectory = "./artifacts/";
                fixture.Settings.Settings = "./demo.runsettings";
                fixture.Settings.Filter = "Priority = 1";
                fixture.Settings.TestAdapterPath = @"/Working/custom-test-adapter";
                fixture.Settings.Logger = @"trx;LogFileName=/Working/logfile.trx";
                fixture.Settings.DiagnosticFile = "./artifacts/logging/diagnostics.txt";
                fixture.Settings.ResultsDirectory = "./tests/";
                fixture.Settings.VSTestReportPath = "./tests/TestResults.xml";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("dotcover test --settings \"/Working/demo.runsettings\" --filter \"Priority = 1\" --test-adapter-path \"/Working/custom-test-adapter\" --logger \"trx;LogFileName=/Working/logfile.trx\" --output \"/Working/artifacts\" --framework dnxcore50 --configuration Release --diag \"/Working/artifacts/logging/diagnostics.txt\" --no-build --no-restore --results-directory \"/Working/tests\" --logger trx;LogFileName=\"/Working/tests/TestResults.xml\"", result.Args);
            }

            [Fact]
            public void Should_Add_Host_Arguments()
            {
                // Given
                var fixture = new DotNetCoreDotCoverTesterFixture();
                fixture.Settings.DiagnosticOutput = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("--diagnostics dotcover test", result.Args);
            }
        }

    }
}
