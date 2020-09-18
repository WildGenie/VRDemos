using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// enum with BuildTargetGroup Options
/// </summary>
public enum OptionsTargetGroup
{
    Standalone,
    Android,
}

/// <summary>
/// enum with BuildTarget options
/// </summary>
public enum OptionsBuildTarget
{
    StandaloneWindows64,
    Android,
}

/// <summary>
/// This class allows the user to edit the found scenes and define settings
/// </summary>
public class SceneConfiguration : EditorWindow
{
    //Enum: provides BuildTarget options
    public OptionsBuildTarget bt;
    
    //Enum: provied BuildTargetGroup options
    public OptionsTargetGroup btg;
    
    //bool vive input utility
    bool assignVIU = false;
    
    //bool goole vr
    bool assignGvR = false;
    
    //bool wave sdk
    bool assignWaveSDK = false;
    
    //buildtargetgroup name 
    private string buildTargetGroupName;
    
    //buildtarget name
    private string buildTargetName;
    
    /// <summary>
    /// <see cref="SceneConfManager"/> SceneConfManager
    /// </summary>
    SceneConfManager SceneConfManager;
    
    /// <summary>
    /// scene name to edit
    /// </summary>
    private string sceneName;

    /// <summary>
    /// Setter for <see cref="SceneConfManager"/> SceneConfManager
    /// </summary>
    /// <param name="sceneConfManager"></param>
    public void SetConfigManager(SceneConfManager sceneConfManager)
    {
        this.SceneConfManager = sceneConfManager;
    }

    /// <summary>
    /// Setter for scene name to edit
    /// </summary>
    /// <param name="sceneName"></param>
    public void SetSceneName(string sceneName)
    {
        this.sceneName = sceneName;
    }

    /// <summary>
    /// 
    /// </summary>
    void OnEnable()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    void OnGUI()
    {
        drawSceneConfiguration();
    }

    /// <summary>
    /// provides the configuration window for each scene and saves the user input scene configuration in 
    /// <see cref="SceneConfManager"/> the SceneConfManager.
    /// </summary>
    void drawSceneConfiguration()
    {
        GUILayout.BeginArea(new Rect(10, 10, 250, 250));
        GUILayout.Label("Scene Configuration");
        GUILayout.Label("Configuration for " + sceneName + " :");

        bt = (OptionsBuildTarget)EditorGUILayout.EnumPopup("BuildTarget :", bt);
        btg = (OptionsTargetGroup)EditorGUILayout.EnumPopup("BuildTargetGroup :", btg);
        assignVIU = GUILayout.Toggle(assignVIU, "VIU");
        assignGvR = GUILayout.Toggle(assignGvR, "Google VR");
        assignWaveSDK = GUILayout.Toggle(assignWaveSDK, "Wave SDK");
        if (GUILayout.Button("Save Config"))
        {
            getBuildTarget(bt);
            getBuildTargetGroupOtion(btg);
            SceneData sceneData = new SceneData();
            sceneData.sceneName = sceneName;
            sceneData.buildtarget = buildTargetName;
            sceneData.buildtargetGroup = buildTargetGroupName;
            sceneData.viu = assignVIU;
            sceneData.gvr = assignGvR;
            sceneData.wavevr = assignWaveSDK;
            SceneConfManager.addSceneData(sceneData);
            this.Close();
            //SceneConfig.sceneConfigs = new SceneData[] { sceneData };
            //Debug.Log("SceneConfigs saved: " + SceneConfig);
            //string saveFile = JsonUtility.ToJson(sceneData, true);
            //Debug.Log(JsonUtility.ToJson(SceneConfig, true));
            //saveIntoJson(saveFile);
        }
        GUILayout.EndArea();
    }

    /// <summary>
    /// set the user input buildtargetgroup
    /// </summary>
    /// <param name="btg"><see cref="BuildTargetGroup"/>buildtargetgroup</param>
    void getBuildTargetGroupOtion(OptionsTargetGroup btg)
    {
        switch (btg)
        {
            case OptionsTargetGroup.Android:
                buildTargetGroupName = "Android";
                break;
            case OptionsTargetGroup.Standalone:
                buildTargetGroupName = "Standalone";
                break;

        }
    }

    /// <summary>
    /// set the user input buildtarget
    /// </summary>
    /// <param name="bt"><see cref="BuildTarget"/>buildtarget</param>
    void getBuildTarget(OptionsBuildTarget bt)
    {
        switch (bt)
        {
            case OptionsBuildTarget.Android:
                buildTargetName = "Android";
                break;
            case OptionsBuildTarget.StandaloneWindows64:
                buildTargetName = "StandaloneWindows64";
                break;
        }
    }
}
    

