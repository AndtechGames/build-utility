/*
 *	Copyright (c) 2020, AndrewMJordan
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Andtech.BuildUtility {

	/// <summary>
	/// Displays version information to the GUI.
	/// </summary>
	public class VersionLabel : MonoBehaviour {
		[SerializeField]
		private TMP_Text textLabel;
		[SerializeField]
		private bool showVersion;
		[SerializeField]
		private bool showRevision;
		[SerializeField]
		private bool showDate;

		#region MONOBEHAVIOUR
		protected virtual void Start() {
			if (BuildVersioner.TryReadVersionFile(out var version)) {
				var format = string.Empty;
				var lines = new List<string>();
				if (showVersion)
					lines.Add(version.RawVersion);
				if (showRevision)
					lines.Add(version.Revision);
				if (showDate)
					lines.Add(version.Timestamp.ToShortDateString());

				textLabel.text = string.Join("\n", lines);
			}
			else {
				textLabel.text = Application.version;
			}
		}
		#endregion
	}
}
