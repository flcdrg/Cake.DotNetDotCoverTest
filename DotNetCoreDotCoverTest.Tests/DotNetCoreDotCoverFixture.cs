using Cake.Common.Tools.DotNetCore;
using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace DotNetCoreDotCoverTest.Tests
{
    internal abstract class DotNetCoreDotCoverFixture<TSettings, TDotCoverSettings> : ToolFixture<TSettings, ToolFixtureResult>
    where TSettings : DotNetCoreSettings, new()
    {
        protected DotNetCoreDotCoverFixture()
            : base("dotnet.exe")
        {
            ProcessRunner.Process.SetStandardOutput(new string[] { });
        }

        protected override ToolFixtureResult CreateResult(FilePath path, ProcessSettings process)
        {
            return new ToolFixtureResult(path, process);
        }
    }
}
