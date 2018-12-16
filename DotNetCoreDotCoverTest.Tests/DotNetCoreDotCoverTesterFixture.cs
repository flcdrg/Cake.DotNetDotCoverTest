using Cake.Common.Tools.DotNetCore.Test;
using Cake.DotNetDotCoverTest;

namespace DotNetCoreDotCoverTest.Tests
{
    internal sealed class DotNetCoreDotCoverTesterFixture : DotNetCoreDotCoverFixture<DotNetCoreTestSettings, DotNetCoreDotCoverTestSettings>
    {
        public string Project { get; set; }

        public DotNetCoreDotCoverTestSettings DotCoverSettings { get; set; } = new DotNetCoreDotCoverTestSettings();

        protected override void RunTool()
        {
            var tool = new DotNetCoreDotCoverTestTester(FileSystem, Environment, ProcessRunner, Tools);
            tool.Test(Project, Settings, DotCoverSettings);
        }
    }
}