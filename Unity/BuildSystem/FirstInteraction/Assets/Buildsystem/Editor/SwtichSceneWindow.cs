using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// This class is used to display the loaded configurations and to switch between the current scenes 
/// </summary>
public class SwtichSceneWindow : EditorWindow
{
    //the SceneConfManager
    SceneConfManager sceneConfManager;
    
    //popup field index
    int index = 0;
    
    //provides the loaded scene names
    public string[] sceneDatas;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="confManager">the SceneConfManager</param>
    public void setSceneConfManager(SceneConfManager confManager)
    {
        this.sceneConfManager = confManager;
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        
    }

    /// <summary>
    /// opend the switch scene window
    /// </summary>
    private void OnGUI()
    {
        drawSceneSwitchWindow();
    }

    /// <summary>
    /// This method shows the existing scenes within a popup window.
    /// The button confirms the selection and applies the associated settings
    /// </summary>
    void drawSceneSwitchWindow()
    {
        sceneDatas = sceneConfManager.getSceneDataLoadAsArray();
        index = EditorGUI.Popup(
            new Rect(10, 10, 250, 250),
            "Switch Scene:",
            index,
            sceneDatas);

        if (GUI.Button(new Rect(0, 25, 50, 50 - 26), "Load"))
        {
            switchActiveSceneAndPlatform(index);
            this.Close();
        }
    }

    /// <summary>
    /// this method loads the scene and its settings selected by the user
    /// </summary>
    /// <param name="i"> index  from popup field</param>
    void switchActiveSceneAndPlatform(int i)
    {
        string sceneName = sceneDatas[i];
        SceneData sceneData = new SceneData();
        sceneData = sceneConfManager.getSceneDataConfiguration(sceneName);

        prepareBuildTarget(sceneData.buildtarget, sceneData.buildtargetGroup);
        prepareScene(sceneData.sceneName);
        prepareAssets(sceneData.viu, sceneData.gvr, sceneData.wavevr);
    }

    /// <summary>
    /// This Method opend the selected scene
    /// </summary>
    /// <param name="name"> scene name</param>
    void prepareScene(string name)
    {
        string path = EditorSceneManager.GetSceneByName(name).path;
        EditorSceneManager.OpenScene("Assets/Buildsystem/Scenes/"+name+".unity");
        
    }

    /// <summary>
    /// This method loads the selected assets and deletes the assets that are not currently required
    /// </summary>
    /// <param name="viu">viu asset</param>
    /// <param name="gvr">gvr asset</param>
    /// <param name="wave">wave asset</param>
    void prepareAssets(bool viu, bool gvr, bool wave)
    {
        if(viu)
        {
            AssetDatabase.DeleteAsset("Assets/WaveVR");
            AssetDatabase.DeleteAsset("Assets/GoogleVR");
            AssetDatabase.ImportPackage("Assets/Resources/ViveInputUtility_v1.10.7.unitypackage", false);
        }
        

        if (gvr)
        {
            AssetDatabase.DeleteAsset("Assets/WaveVR");
            AssetDatabase.DeleteAsset("Assets/HTC.UnityPlugin");
            AssetDatabase.ImportPackage("Assets/Resources/GoogleVRForUnity_1.200.1.unitypackage", false);
        }
        

        if(wave)
        {
            AssetDatabase.DeleteAsset("Assets/GoogleVR");
            AssetDatabase.ImportPackage("Assets/Resources/ViveInputUtility_v1.10.7.unitypackage", false);
            AssetDatabase.ImportPackage("Assets/Resources/wvr_unity_sdk.unitypackage", false);
        }

        if(!viu && !gvr && !wave)
        {
            AssetDatabase.DeleteAsset("Assets/WaveVR");
            AssetDatabase.DeleteAsset("Assets/HTC.UnityPlugin");
            AssetDatabase.DeleteAsset("Assets/GoogleVR");
        }
        
    }

    /// <summary>
    /// this method changes the build target based on the scene selected by the user
    /// </summary>
    /// <param name="buildTarget">unity buildtarget</param>
    /// <param name="buildTargetGroup">unity buildtargetgroup</param>
    void prepareBuildTarget(string buildTarget, string buildTargetGroup)
    {
        if(buildTarget == "Android" && buildTargetGroup == "Android")
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        }

        if(buildTarget == "Standalone" && buildTargetGroup == "StandaloneWindows64")
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        }
    }
}