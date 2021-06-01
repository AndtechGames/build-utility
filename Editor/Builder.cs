/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;
using System.Collections.Generic;
using UnityEditor;

namespace Andtech.BuildUtility
{

    /// <summary>
    /// Simple build utility for Unity. Invoke this utility from the command line.
    /// </summary>
    /// <remarks>Source: https://fargesportfolio.com/unity-generic-auto-build/#codesyntax_1</remarks>
    public static class Builder
    {

        public static void Build()
        {
            var helper = new BuildHelper(Environment.GetCommandLineArgs());

            Prebuild();
            var buildTarget = helper.GetBuildTarget();
            var outputFilename = helper.GetOutputPath();

            Build(outputFilename, buildTarget);

            void Prebuild()
            {
                if (helper.TryGetArgument("revision", out string revision))
                {
                    BuildVersioner.WriteVersionFile(revision);
                }
            }
        }

        static BuildPlayerOptions GetDefaultPlayerOptions()
        {
            var listScenes = new List<string>();
            foreach (var s in EditorBuildSettings.scenes)
            {
                if (s.enabled)
                    listScenes.Add(s.path);
            }
            var buildOptions = BuildOptions.None;

            var buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = listScenes.ToArray(),
                options = buildOptions
            };

            return buildPlayerOptions;
        }

        static void Build(string locationPathName, BuildTarget buildTarget)
        {
            if (buildTarget != EditorUserBuildSettings.activeBuildTarget)
            {
                BuildTargetGroup targetGroup = PlatformUtility.ConvertBuildTarget(buildTarget);
                EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, buildTarget);
            }

            BuildPlayerOptions buildPlayerOptions = GetDefaultPlayerOptions();
            buildPlayerOptions.locationPathName = locationPathName;
            buildPlayerOptions.target = buildTarget;

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

            switch (report.summary.result)
            {
                case UnityEditor.Build.Reporting.BuildResult.Succeeded:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Build succeeded! Executable is located at: {0}", buildPlayerOptions.locationPathName);
                    Console.ResetColor();
                    break;
                default:
                    Environment.ExitCode = 1;
                    break;
            }

        }
    }
}
