using NUnit.Framework;
using System.IO;
using UnityEditor;

namespace Andtech.BuildUtility.Tests {

	public class BuildTests {

		[Test]
		public void TestDefaults() {
			var helper = new BuildHelper(new string[] { });
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Builds", Path.GetDirectoryName(outputPath));
			Assert.AreEqual(PlayerSettings.productName, Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomDirectory() {
			var helper = new BuildHelper(new string[] { "-output", "Folder" });
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Folder", Path.GetDirectoryName(outputPath));
			Assert.AreEqual(PlayerSettings.productName, Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomDirectoryWithFilename() {
			var helper = new BuildHelper(new string[] { "-output", "Folder/file.exe" });
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Folder", Path.GetDirectoryName(outputPath));
			Assert.AreEqual("file.exe", Path.GetFileName(outputPath));
		}

		[Test]
		public void TestCustomSubDirectory() {
			var helper = new BuildHelper(new string[] { "-output", "Folder/Subfolder" });
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual(Path.Combine("Folder", "Subfolder"), Path.GetDirectoryName(outputPath));
			Assert.AreEqual(PlayerSettings.productName, Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomName() {
			var helper = new BuildHelper(new string[] { "-name", "drop" });
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Builds", Path.GetDirectoryName(outputPath));
			Assert.AreEqual("drop", Path.GetFileNameWithoutExtension(outputPath));
		}

		[Test]
		public void TestCustomNameWithExtension() {
			var helper = new BuildHelper(new string[] { "-name", "drop.app" });
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual("Builds", Path.GetDirectoryName(outputPath));
			Assert.AreEqual("drop.app", Path.GetFileName(outputPath));
		}

		[Test]
		public void TestCustomNameInSubfolder() {
			var helper = new BuildHelper(new string[] { "-name", "Subfolder/drop" });
			var outputPath = helper.GetOutputPath();

			Assert.AreEqual(Path.Combine("Builds", "Subfolder"), Path.GetDirectoryName(outputPath));
			Assert.AreEqual("drop", Path.GetFileNameWithoutExtension(outputPath));
		}
	}
}
