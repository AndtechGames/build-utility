# Build Utility

Simple build utility for Unity

## Quick Start
1. Open the Package Manager window in the Unity Editor.
2. Click "Add package from git URL".
3. Type `https://github.com/AndrewMJordan/build-utility.git#upm` then press enter.

## Usage
Invoke `unity.exe` and use the `executeMethod` option to execute the build utility:
```
Andtech.BuildUtility.Builder.Build [-output <DIRECTORY] [-name <NAME>]
```

### Command Line
1. Run unity.exe with the `executeMethod` option.
```
> unity.exe <PROJECT_PATH> -executeMethod Andtech.BuildUtility.Builder.Build
```

2. Use the `revision` option if you want a revision string available in your build. For example, your CI/CD pipeline can include the commit hash in each build.
```
> unity.exe <PROJECT_PATH> -executeMethod Andtech.BuildUtility.Builder.Build -revision ca82a6df
```

3. Use the `output` option to build to the specified directory.
```
> unity.exe <PROJECT_PATH> -executeMethod Andtech.BuildUtility.Builder.Build -output $HOME
```

4. Use the `name` option to give an explicit name to your build. (If you omit the extension, an extension will automatically be added based on the build target)
```
> unity.exe <PROJECT_PATH> -executeMethod Andtech.BuildUtility.Builder.Build -name MyBuild
> unity.exe <PROJECT_PATH> -executeMethod Andtech.BuildUtility.Builder.Build -name MyAndroidApp.apk
> unity.exe <PROJECT_PATH> -executeMethod Andtech.BuildUtility.Builder.Build -name MyAndroidApp
```

5. Access the revision string via `BuildVersioner`.
```csharp
if (BuildVersioner.TryReadVersionFile(out var versionInfo)) {
	string version = versionInfo.RawVersion;
	string revision = versionInfo.Revision;

	Debug.Log($"Build {version} (Revision: {revision})");
}
```

### Version Label Sample
1. From the Package Manager window, import the Version Label sample.
2. Add the VersionLabel prefab to a canvas in your scene.
3. When your project is build, version information will be displayed on-screen.
