/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using NUnit.Framework;
using System.IO;
using UnityEditor;

namespace Andtech.BuildUtility.Tests {

	public class BuildTests {

		[Test]
		public void TestDefaults() {
			var helper = new BuildHelper();
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Builds", Path.GetDirectoryName(outputPath));
			Assert.AreEqual(PlayerSettings.productName, Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomLocation() {
			var helper = new BuildHelper("-output", "Folder");
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Folder", Path.GetDirectoryName(outputPath));
			Assert.AreEqual(PlayerSettings.productName, Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomLocationWithFilename() {
			var helper = new BuildHelper("-output", "Folder/file.exe");
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Folder", Path.GetDirectoryName(outputPath));
			Assert.AreEqual("file.exe", Path.GetFileName(outputPath));
		}

		[Test]
		public void TestCustomLocationInSubdirectory() {
			var helper = new BuildHelper("-output", "Folder/Subfolder");
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual(Path.Combine("Folder", "Subfolder"), Path.GetDirectoryName(outputPath));
			Assert.AreEqual(PlayerSettings.productName, Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomName() {
			var helper = new BuildHelper("-name", "drop");
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Builds", Path.GetDirectoryName(outputPath));
			Assert.AreEqual("drop", Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomNameWithExtension() {
			var helper = new BuildHelper("-name", "drop.app");
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Builds", Path.GetDirectoryName(outputPath));
			Assert.AreEqual("drop.app", Path.GetFileName(outputPath));
		}

		[Test]
		public void TestCustomNameInSubfolder() {
			var helper = new BuildHelper("-name", "Subfolder/drop");
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual(Path.Combine("Builds", "Subfolder"), Path.GetDirectoryName(outputPath));
			Assert.AreEqual("drop", Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestBuildWebGL()
		{
			var helper = new BuildHelper();
			var outputPath = helper.GetOutputPath(BuildTarget.WebGL);

			Assert.AreEqual(Path.Combine("Builds"), outputPath);
		}

		[Test]
		public void TestBuildWebGLWithCustomLocation()
		{
			var helper = new BuildHelper("-output", "Folder/Subfolder");
			var outputPath = helper.GetOutputPath(BuildTarget.WebGL);

			Assert.AreEqual(Path.Combine("Folder/Subfolder"), outputPath);
		}
	}
}
