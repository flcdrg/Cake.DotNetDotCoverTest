using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;
using Cake.Core.Annotations;

using System;

namespace Cake.DotNetDotCoverTest
{
    /// <summary>
    /// <para>Contains functionality related to <see href="https://github.com/dotnet/cli">.NET Core CLI</see>.</para>
    /// <para>
    /// In order to use the commands for this alias, the .Net Core CLI tools will need to be installed on the machine where
    /// the Cake script is being executed.  See this <see href="https://www.microsoft.com/net/core">page</see> for information
    /// on how to install.
    /// </para>
    /// </summary>
    [CakeAliasCategory("DotNetCore")]
    public static class Aliases
    {
        /// <summary>
        /// Test project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreDotCoverTest();
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Test")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Test")]
        public static void DotNetCoreDotCoverTest(this ICakeContext context)
        {
            context.DotNetCoreDotCoverTest(null, null);
        }

        /// <summary>
        /// Test project with path.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project path.</param>
        /// <example>
        /// <para>Specify the path to the .csproj file in the test project</para>
        /// <code>
        ///     DotNetCoreDotCoverTest("./test/Project.Tests/Project.Tests.csproj");
        /// </code>
        /// <para>You could also specify a task that runs multiple test projects.</para>
        /// <para>Cake task:</para>
        /// <code>
        /// Task("Test")
        ///     .Does(() =>
        /// {
        ///     var projectFiles = GetFiles("./test/**/*.csproj");
        ///     foreach(var file in projectFiles)
        ///     {
        ///         DotNetCoreDotCoverTest(file.FullPath);
        ///     }
        /// });
        /// </code>
        /// <para>If your test project is using project.json, the project parameter should just be the directory path.</para>
        /// <code>
        ///     DotNetCoreDotCoverTest("./test/Project.Tests/");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Test")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Test")]
        public static void DotNetCoreDotCoverTest(this ICakeContext context, string project)
        {
            context.DotNetCoreDotCoverTest(project, null);
        }

        /// <summary>
        /// Test project with settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreDotCoverTestSettings
        ///     {
        ///         Configuration = "Release"
        ///     };
        ///
        ///     DotNetCoreDotCoverTest("./test/Project.Tests/Project.Tests.csproj", settings);
        /// </code>
        /// <para>You could also specify a task that runs multiple test projects.</para>
        /// <para>Cake task:</para>
        /// <code>
        /// Task("Test")
        ///     .Does(() =>
        /// {
        ///     var settings = new DotNetCoreDotCoverTestSettings
        ///     {
        ///         Configuration = "Release"
        ///     };
        ///
        ///     var projectFiles = GetFiles("./test/**/*.csproj");
        ///     foreach(var file in projectFiles)
        ///     {
        ///         DotNetCoreDotCoverTest(file.FullPath, settings);
        ///     }
        /// });
        /// </code>
        /// <para>If your test project is using project.json, the project parameter should just be the directory path.</para>
        /// <code>
        ///     var settings = new DotNetCoreDotCoverTestSettings
        ///     {
        ///         Configuration = "Release"
        ///     };
        ///
        ///     DotNetCoreDotCoverTest("./test/Project.Tests/", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Test")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Test")]
        public static void DotNetCoreDotCoverTest(this ICakeContext context, string project, DotNetCoreTestSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCoreTestSettings();
            }

            var tester = new DotNetCoreDotCoverTester(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            tester.Test(project, settings, new DotNetCoreDotCoverTestSettings());
        }
    }
}
