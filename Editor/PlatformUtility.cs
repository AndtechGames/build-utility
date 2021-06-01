/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using UnityEditor;
using UnityEngine;

namespace Andtech.BuildUtility
{
    public class PlatformUtility : MonoBehaviour
    {

        public static string GetExtension(BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case BuildTarget.StandaloneOSX:
                    return ".app";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return ".exe";
                case BuildTarget.iOS:
                    return string.Empty;
                case BuildTarget.Android:
                    return ".apk";
                default:
                    return ".unknown";
            }
        }

        public static BuildTargetGroup ConvertBuildTarget(BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
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

        public static bool DoesTargetBuildAsFolder(BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case BuildTarget.WebGL:
                    return true;
                default:
                    return false;
            }
        }
    }
}
