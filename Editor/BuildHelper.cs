/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;
using System.IO;
using UnityEditor;

namespace Andtech.BuildUtility {

	public class BuildHelper {
		public readonly string[] Args;

		public BuildHelper(params string[] args) {
			Args = args;
		}

		public bool TryGetArgument(string name, out string value) {
			for (int i = 0; i < Args.Length; i++) {
				var arg = Args[i];
				var match = arg.Equals("-" + name);
				if (match) {
					value = Args[i + 1];
					return true;
				}
			}

			value = null;
			return false;
		}

		public BuildTarget GetBuildTarget() {
			BuildTarget buildTarget;

			var hasBuildTarget = TryGetArgument("buildTarget", out string buildTargetName);
			if (hasBuildTarget) {
				if (Enum.TryParse(buildTargetName, out buildTarget)) {
					Console.WriteLine(":: Received custom build target " + buildTargetName);
				}
				else {
					buildTarget = EditorUserBuildSettings.activeBuildTarget;
					Console.WriteLine($":: {nameof(buildTargetName)} \"{buildTargetName}\" not defined on enum {nameof(BuildTarget)}, using {buildTarget} enum to build");
				}
			}
			else {
				buildTarget = EditorUserBuildSettings.activeBuildTarget;
			}

			return buildTarget;
		}

		public string GetOutputPath() => GetOutputPath(GetBuildTarget());

		public string GetOutputPath(BuildTarget buildTarget)
		{
			var extension = PlatformUtility.GetExtension(buildTarget);

			var defaultDirectory = "Builds";
			var defaultFilename = string.Concat(PlayerSettings.productName, extension);
			var outputDirectory = defaultDirectory;

			var outputFilename = defaultFilename;
			if (TryGetArgument("output", out var output))
			{
				if (Path.HasExtension(output))
				{
					outputDirectory = Path.GetDirectoryName(output);
					outputFilename = Path.GetFileName(output);
				}
				else
				{
					outputDirectory = output;
					outputFilename = defaultFilename;
				}
			}

			if (TryGetArgument("name", out string name))
			{
				outputFilename = Path.HasExtension(name) ? name : string.Concat(name, extension);
			}

			if (PlatformUtility.DoesTargetBuildAsFolder(buildTarget))
			{
				return outputDirectory;
			}

			return Path.Combine(outputDirectory, outputFilename);
		}
	}
}
