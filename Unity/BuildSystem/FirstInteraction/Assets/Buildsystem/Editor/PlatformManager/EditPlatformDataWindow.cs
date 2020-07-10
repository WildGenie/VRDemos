using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EditPlatformDataWindow : EditorWindow
{

    bool updateOnce = false;

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

    //
    int storedIndex;

    //buildtargetgroup name 
    private string buildTargetGroupName;

    //buildtarget name
    private string buildTargetName;

    //
    private string projectName;

    //
    private string description;

    //
    private int index;

    //
    private string sceneName;

    //
    private string[] allScenesPath;

    /// <summary>
    /// <see cref="PlatformDataManager"/> SceneConfManager
    /// </summary>
    PlatformDataManager PlatformDataManager;

    private string configName;

    private PlatformData platformData;


    /// <summary>
    /// Setter for <see cref="PlatformDataManager"/> PlatformDataManager
    /// </summary>
    /// <param name="platformDataManager"></param>
    public void SetDataManager(PlatformDataManager platformDataManager)
    {
        this.PlatformDataManager = platformDataManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="platformData"></param>
    public void SetPlatformDataToEdit(PlatformData platformData)
    {
        this.platformData = platformData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(int index)
    {
        this.index = index;
        this.storedIndex = index;
    }

    /// <summary>
    /// 
    /// </summary>
    public void init()
    {
        if(!updateOnce)
        {
            this.platformData = new PlatformData();
            this.platformData = PlatformDataManager.getPlatformDataFromIndex(this.index);
            this.projectName = platformData.projectName;
            this.description = platformData.description;
            this.configName = platformData.configurationName;
            this.assignVIU = platformData.viu;
            this.assignGvR = platformData.gvr;
            this.assignWaveSDK = platformData.wavevr;
            this.assignMiddleVR = platformData.middleVR;

            if (platformData.buildtargetGroup == "Android")
            {
                btg = OptionsTargetGroup.Android;
            }

            if (platformData.buildtargetGroup == "Standalone")
            {
                btg = OptionsTargetGroup.Standalone;
            }

            if (platformData.buildtarget == "Android")
            {
                bt = OptionsBuildTarget.Android;
            }

            if (platformData.buildtarget == "StandaloneWindows64")
            {
                bt = OptionsBuildTarget.StandaloneWindows64;
            }

            updateOnce = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        

    }

    /// <summary>
    /// 
    /// </summary>
    private void OnGUI()
    {
        init();
        loadActiveScenes();
        showCreateConfiguration();
    }

    /// <summary>
    /// 
    /// </summary>
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

        if (GUI.Button(new Rect(0, 200, 50, 25), "Save"))
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
            //PlatformDataManager.updatePlatformDataByIndex(storedIndex, platformData);
            //PlatformDataManager.updatePlatformDataByData(platformData.configurationName, platformData);
            PlatformDataManager.updatePlatformData(platformData);
            this.Close();
        }

        if (GUI.Button(new Rect(50, 200, 50, 25), "Cancel"))
        {
            this.Close();
        }
        GUILayout.EndArea();
    }

    /// <summary>
    /// 
    /// </summary>
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
