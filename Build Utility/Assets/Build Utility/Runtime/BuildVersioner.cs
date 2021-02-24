using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Andtech.BuildUtility {

	/// <summary>
	/// Metadata for a build.
	/// </summary>
	[Serializable]
	public struct BuildVersionInfo {
		public string RawVersion;
		public string Revision;
		public DateTime Timestamp;
	}

	/// <summary>
	/// Functions for accessing vuild version information during runtime.
	/// </summary>
	public static class BuildVersioner {
		private const string RESOURCE_ROOT = "Assets/Resources";
		private static readonly string VERSION_PATH = Path.Combine(RESOURCE_ROOT, "version.asset");

		/// <summary>
		/// Write the current build information to the project. (Unity Editor only)
		/// </summary>
		/// <param name="revision">A revision string.</param>
		public static void WriteVersionFile(string revision) {
#if UNITY_EDITOR
			var versionString = Application.version;
			var info = new BuildVersionInfo {
				RawVersion = versionString,
				Revision = revision,
				Timestamp = DateTime.UtcNow
			};

			var lines = JsonUtility.ToJson(info);
			var textAsset = new TextAsset(lines);

			if (!AssetDatabase.IsValidFolder(RESOURCE_ROOT))
				AssetDatabase.CreateFolder("Assets", "Resources");
			AssetDatabase.DeleteAsset(VERSION_PATH);
			AssetDatabase.CreateAsset(textAsset, VERSION_PATH);
			AssetDatabase.SaveAssets();
#else
		Debug.LogWarning("Writing to version file is only supported in the Unity editor");
#endif
		}

		/// <summary>
		/// Read the build information for the current build.
		/// </summary>
		/// <returns>Metadata for the build.</returns>
		public static BuildVersionInfo ReadVersionFile() {
			var ta = Resources.Load<TextAsset>("version");

			return JsonUtility.FromJson<BuildVersionInfo>(ta.text);
		}

		/// <summary>
		/// Read the build information for the current build.
		/// </summary>
		/// <param name="info">Metadata for the build.</param>
		/// <returns>Build information was read successfully.</returns>
		public static bool TryReadVersionFile(out BuildVersionInfo info) {
			try {
				var textAsset = Resources.Load<TextAsset>("version");

				info = JsonUtility.FromJson<BuildVersionInfo>(textAsset.text);
				return true;
			}
			catch { }

			info = default;
			return false;
		}
	}
}