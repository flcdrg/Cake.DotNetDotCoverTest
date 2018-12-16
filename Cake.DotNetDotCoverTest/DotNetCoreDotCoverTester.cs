﻿using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;
using Cake.Core.IO;

using System;

namespace Cake.DotNetDotCoverTest
{
    /// <summary>
    /// .NET Core project tester with dotCover
    /// </summary>
    public sealed class DotNetCoreDotCoverTester : DotNetCoreTool<DotNetCoreTestSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetCoreDotCoverTester" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public DotNetCoreDotCoverTester(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            Core.Tooling.IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        /// <summary>
        /// Tests the project using the specified path with arguments and settings.
        /// </summary>
        /// <param name="project">The target project path.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="dotCoverSettings"></param>
        public void Test(string project, DotNetCoreTestSettings settings, DotNetCoreDotCoverTestSettings dotCoverSettings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (dotCoverSettings == null)
            {
                throw new ArgumentNullException(nameof(dotCoverSettings));
            }

            RunCommand(settings, GetArguments(project, settings, dotCoverSettings));
        }

        private ProcessArgumentBuilder GetArguments(string project, DotNetCoreTestSettings settings, DotNetCoreDotCoverTestSettings dotCoverSettings)
        {
            var builder = CreateArgumentBuilder(settings);

            builder.Append("dotcover");
            builder.Append("test");

            // Specific path?
            if (project != null)
            {
                builder.AppendQuoted(project);
            }

            // Settings
            if (settings.Settings != null)
            {
                builder.Append("--settings");
                builder.AppendQuoted(settings.Settings.MakeAbsolute(_environment).FullPath);
            }

            // Filter
            if (!string.IsNullOrWhiteSpace(settings.Filter))
            {
                builder.Append("--filter");
                builder.AppendQuoted(settings.Filter);
            }

            // Settings
            if (settings.TestAdapterPath != null)
            {
                builder.Append("--test-adapter-path");
                builder.AppendQuoted(settings.TestAdapterPath.MakeAbsolute(_environment).FullPath);
            }

            // Filter
            if (!string.IsNullOrWhiteSpace(settings.Logger))
            {
                builder.Append("--logger");
                builder.AppendQuoted(settings.Logger);
            }

            // Output directory
            if (settings.OutputDirectory != null)
            {
                builder.Append("--output");
                builder.AppendQuoted(settings.OutputDirectory.MakeAbsolute(_environment).FullPath);
            }

            // Frameworks
            if (!string.IsNullOrEmpty(settings.Framework))
            {
                builder.Append("--framework");
                builder.Append(settings.Framework);
            }

            // Configuration
            if (!string.IsNullOrEmpty(settings.Configuration))
            {
                builder.Append("--configuration");
                builder.Append(settings.Configuration);
            }

            // Output directory
            if (settings.DiagnosticFile != null)
            {
                builder.Append("--diag");
                builder.AppendQuoted(settings.DiagnosticFile.MakeAbsolute(_environment).FullPath);
            }

            // No Build
            if (settings.NoBuild)
            {
                builder.Append("--no-build");
            }

            // No Restore
            if (settings.NoRestore)
            {
                builder.Append("--no-restore");
            }

            if (settings.ResultsDirectory != null)
            {
                builder.Append("--results-directory");
                builder.AppendQuoted(settings.ResultsDirectory.MakeAbsolute(_environment).FullPath);
            }

            if (settings.VSTestReportPath != null)
            {
                builder.AppendSwitchQuoted($"--logger trx;LogFileName", "=", settings.VSTestReportPath.MakeAbsolute(_environment).FullPath);
            }

            return builder;
        }
    }
}
