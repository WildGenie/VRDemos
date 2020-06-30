using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CreateConfiguration : EditorWindow
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

    //bool middleVR
    bool assignMiddleVR = false;

    //buildtargetgroup name 
    private string buildTargetGroupName;

    //buildtarget name
    private string buildTargetName;

    private string projectName;

    private string description;

    private int index;

    private string[] allScenesPath;

    /// <summary>
    /// <see cref="PlatformDataManager"/> SceneConfManager
    /// </summary>
    PlatformDataManager PlatformDataManager;

    private string configName;


    /// <summary>
    /// Setter for <see cref="SceneConfManager"/> SceneConfManager
    /// </summary>
    /// <param name="platformDataManager"></param>
    public void SetDataManager(PlatformDataManager platformDataManager)
    {
        this.PlatformDataManager = platformDataManager;
        
    }

    public void init()
    {
        this.index = 0;
        this.projectName = PlayerSettings.productName;
        this.description = "";
       
        
    }

    private void OnEnable()
    {
        init();
        
    }

    private void OnGUI()
    {
        loadActiveScenes();
        showCreateConfiguration();
    }

    private void showCreateConfiguration()
    {
        
        GUILayout.BeginArea(new Rect(0, 0, 250, 250));
        GUILayout.Label("Create Configuration:");
        configName = EditorGUILayout.TextField("Config. Name:", configName);
        description = EditorGUILayout.TextField("Description: ", description);
        projectName = EditorGUILayout.TextField("Product Name: ", projectName);
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, 80, 250, 250));      
        index = EditorGUILayout.Popup(
            "Choose Scene:",
            index,
            allScenesPath);
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(0, 100, 250, 250));
        bt = (OptionsBuildTarget)EditorGUILayout.EnumPopup("BuildTarget :", bt);
        btg = (OptionsTargetGroup)EditorGUILayout.EnumPopup("Platform :", btg);
        assignVIU = GUILayout.Toggle(assignVIU, "VIU");
        assignGvR = GUILayout.Toggle(assignGvR, "Google VR");
        assignWaveSDK = GUILayout.Toggle(assignWaveSDK, "Wave SDK");
        assignMiddleVR = GUILayout.Toggle(assignMiddleVR, "MiddleVR");

        if (GUI.Button(new Rect(0,200,50,25), "Save"))
        {
            PlatformData platformData = new PlatformData();
            platformData.configurationName = configName;
            platformData.description = description;
            platformData.projectName = projectName;
            platformData.sceneName = allScenesPath[index];
            getBuildTarget(bt);
            getBuildTargetGroupOption(btg);
            platformData.buildtarget = buildTargetName;
            platformData.buildtargetGroup = buildTargetGroupName;
            platformData.viu = assignVIU;
            platformData.gvr = assignGvR;
            platformData.wavevr = assignWaveSDK;
            platformData.middleVR = assignMiddleVR;
            PlatformDataManager.addPlatformConfiguration(platformData);
            this.Close();
        }

        if (GUI.Button(new Rect(50, 200, 50 , 25), "Cancel"))
        {
            this.Close();
        }
        GUILayout.EndArea();
    }

    private void loadActiveScenes()
    {
        this.allScenesPath = this.PlatformDataManager.getScenesPath();
    }

    /// <summary>
    /// set the user input buildtargetgroup
    /// </summary>
    /// <param name="btg"><see cref="BuildTargetGroup"/>buildtargetgroup</param>
    void getBuildTargetGroupOption(OptionsTargetGroup btg)
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
