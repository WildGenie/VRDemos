using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.PackageManager.UI;

/// <summary>
/// The Buildsystem Editor Menu
/// </summary>
public class Buildsystem : MonoBehaviour
{
    /// <summary>
    /// <see cref="SceneConfManager"/>SceneConfManager
    /// </summary>
    public static SceneConfManager sceneConfManager = new SceneConfManager();

    /// <summary>
    /// <see cref="PlatformDataManager"/>SceneConfManager
    /// </summary>
    public static PlatformDataManager platformDataManager = new PlatformDataManager();

    /// <summary>
    /// old switch platform => deleted later
    /// </summary>
    [MenuItem("Buildsystem/Platform/Standalone - Win64")]
    private static void LoadStandaloneWin64Build()
    {
        AssetDatabase.DeleteAsset("Assets/GoogleVR");
        AssetDatabase.DeleteAsset("Assets/HTC.UnityPlugin");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        EditorSceneManager.OpenScene("Assets/Buildsystem/Scenes/WinStandalone.unity");
    }

    /// <summary>
    /// old switch platform => deleted later
    /// </summary>
    [MenuItem("Buildsystem/Platform/LoadVIU - VivePro-Win64")]
    private static void LoadViuViveProWin64Build()
    {
        AssetDatabase.DeleteAsset("Assets/GoogleVR");
        AssetDatabase.DeleteAsset("Assets/HTC.UnityPlugin");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        //Open the Scene in the Editor (do not enter Play Mode)
        EditorSceneManager.OpenScene("Assets/Buildsystem/Scenes/VIU-Win.unity");
        AssetDatabase.ImportPackage("Assets/Resources/ViveInputUtility_v1.10.7.unitypackage", false);
    }

    /// <summary>
    /// old switch platform => deleted later
    /// </summary>
    [MenuItem("Buildsystem/Platform/LoadVIU - ViveFocusPro - Android")]
    private static void LoadViveFocusProAndroidBuild()
    {
        AssetDatabase.DeleteAsset("Assets/GoogleVR");
        AssetDatabase.DeleteAsset("Assets/HTC.UnityPlugin");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        //Open the Scene in the Editor (do not enter Play Mode)
        EditorSceneManager.OpenScene("Assets/Buildsystem/Scenes/VIU-Android.unity");
        AssetDatabase.ImportPackage("Assets/Resources/ViveInputUtility_v1.10.7.unitypackage", false);
    }

    /// <summary>
    /// old switch platform => deleted later
    /// </summary>
    [MenuItem("Buildsystem/Platform/LoadGVR - Cardboard - Android")]
    private static void LoadGvrCardboardAndroidBuild()
    {
        AssetDatabase.DeleteAsset("Assets/GoogleVR");
        AssetDatabase.DeleteAsset("Assets/HTC.UnityPlugin");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorSceneManager.OpenScene("Assets/Buildsystem/Scenes/GVR.unity");
        AssetDatabase.ImportPackage("Assets/Resources/GoogleVRForUnity_1.200.1.unitypackage", false);
    }

    /// <summary>
    /// provides the configuration menu
    /// </summary>
    [MenuItem("Buildsystem/Platform/Configuration")]
    static void ShowSceneConfigurationManger()
    {
        SceneConfigurationManager window =
            (SceneConfigurationManager)EditorWindow.GetWindow(typeof(SceneConfigurationManager), true,
                "Scene Manager");
        window.setSceneConfManager(sceneConfManager);
        window.Show();
    }

    /// <summary>
    /// shows new platform configuration
    /// </summary>
    [MenuItem("Buildsystem/Platform/Platform Configuration")]
    static void ShowPlatformConfigurationManager()
    {
        PlatformConfigurationManager platformConfigurationManager =
            (PlatformConfigurationManager)EditorWindow.GetWindow(typeof(PlatformConfigurationManager), true,
            "Platform Manager");
        platformConfigurationManager.setPlatformDataMangager(platformDataManager);
        platformConfigurationManager.Show();
    }

    /// <summary>
    /// Provides the switch scene Menu
    /// </summary>
    [MenuItem("Buildsystem/Platform/SwitchScene")]
    static void SwitchPlatformAndScene()
    {
        SwtichSceneWindow swtichSceneWindow =
            (SwtichSceneWindow)EditorWindow.GetWindow(typeof(SwtichSceneWindow), true,
            "Switch between Scenes");
        swtichSceneWindow.setSceneConfManager(sceneConfManager);
        swtichSceneWindow.Show();
    }

    /// <summary>
    /// sync unity project with buildsystem server
    /// </summary>
    [MenuItem("Buildsystem/Sync Buildsystem")]
    static void SyncWithServer()
    {
        SyncServerWindow syncServerWindow =
            (SyncServerWindow)EditorWindow.GetWindow(typeof(SyncServerWindow),
            true, "Sync with Buildsystem");
        syncServerWindow.SetConfigManager(sceneConfManager);
        syncServerWindow.Show();
    }
}
