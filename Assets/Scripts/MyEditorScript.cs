#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

class MyEditorScript
{
    static void PerformBuild()
    {
        string buildFolderPath = "C:/Users/jpris/Desktop/FridayNightBuild";  // Path to the build folder
        string buildPath = buildFolderPath + "/FridayNight.exe";  // Path to the build output file

        // Check if the build folder exists
        if (Directory.Exists(buildFolderPath))
        {
            // Delete all files in the build folder
            string[] files = Directory.GetFiles(buildFolderPath);
            foreach (string file in files)
            {
                File.Delete(file);
            }
            // Optionally delete subdirectories as well
            string[] directories = Directory.GetDirectories(buildFolderPath);
            foreach (string directory in directories)
            {
                Directory.Delete(directory, true);  // true deletes subdirectories
            }
            Debug.Log("Previous build files deleted.");
        }

        // Define the build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/SampleScene.unity" }; // Specify the scenes to include in the build
        buildPlayerOptions.locationPathName = buildPath;  // Specify the output path

        // Define build target (e.g., Windows in this case)
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;

        // Perform the build
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        Debug.Log("Build completed successfully!");
    }
}
#endif
