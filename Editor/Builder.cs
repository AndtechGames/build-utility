/*
 *	Copyright (c) 2020, AndrewMJordan
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;
using System.Collections.Generic;
using UnityEditor;

namespace Andtech.BuildUtility {

	/// <summary>
	/// Simple build utility for Unity. Invoke this utility from the command line.
	/// </summary>
	/// <remarks>Source: https://fargesportfolio.com/unity-generic-auto-build/#codesyntax_1</remarks>
	public static class Builder {

		public static void Build() {
			var helper = new BuildHelper(Environment.GetCommandLineArgs());

			Prebuild();
			var buildTarget = helper.GetBuildTarget();
			var outputFilename = helper.GetOutputPath();

			Build(outputFilename, buildTarget);

			void Prebuild() {
				if (helper.TryGetArgument("revision", out string revision)) {
					BuildVersioner.WriteVersionFile(revision);
				}
			}
		}

		static BuildPlayerOptions GetDefaultPlayerOptions() {
			var listScenes = new List<string>();
			foreach (var s in EditorBuildSettings.scenes) {
				if (s.enabled)
					listScenes.Add(s.path);
			}
			var buildOptions = BuildOptions.None;

			var buildPlayerOptions = new BuildPlayerOptions() {
				scenes = listScenes.ToArray(),
				options = buildOptions
			};

			return buildPlayerOptions;
		}

		static void Build(string locationPathName, BuildTarget buildTarget) {
			if (buildTarget != EditorUserBuildSettings.activeBuildTarget) {
				BuildTargetGroup targetGroup = ConvertBuildTarget(buildTarget);
				EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, buildTarget);
			}

			BuildPlayerOptions buildPlayerOptions = GetDefaultPlayerOptions();
			buildPlayerOptions.locationPathName = locationPathName;
			buildPlayerOptions.target = buildTarget;

			Console.WriteLine(BuildPipeline.BuildPlayer(buildPlayerOptions));
			Console.WriteLine("Output at {0}", buildPlayerOptions.locationPathName);
		}

		static BuildTargetGroup ConvertBuildTarget(BuildTarget buildTarget) {
			switch (buildTarget) {
				case BuildTarget.StandaloneOSX:
				case BuildTarget.iOS:
					return BuildTargetGroup.iOS;
				case BuildTarget.StandaloneWindows:
				case BuildTarget.StandaloneLinux:
				case BuildTarget.StandaloneWindows64:
				case BuildTarget.StandaloneLinux64:
				case BuildTarget.StandaloneLinuxUniversal:
					return BuildTargetGroup.Standalone;
				case BuildTarget.Android:
					return BuildTargetGroup.Android;
				case BuildTarget.WebGL:
					return BuildTargetGroup.WebGL;
				case BuildTarget.WSAPlayer:
					return BuildTargetGroup.WSA;
				case BuildTarget.Tizen:
					return BuildTargetGroup.Tizen;
				case BuildTarget.PSP2:
					return BuildTargetGroup.PSP2;
				case BuildTarget.PS4:
					return BuildTargetGroup.PS4;
				case BuildTarget.PSM:
					return BuildTargetGroup.PSM;
				case BuildTarget.XboxOne:
					return BuildTargetGroup.XboxOne;
				case BuildTarget.N3DS:
					return BuildTargetGroup.N3DS;
				case BuildTarget.WiiU:
					return BuildTargetGroup.WiiU;
				case BuildTarget.tvOS:
					return BuildTargetGroup.tvOS;
				case BuildTarget.Switch:
					return BuildTargetGroup.Switch;
				case BuildTarget.NoTarget:
				default:
					return BuildTargetGroup.Standalone;
			}
		}
	}
}
