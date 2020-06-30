using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Configuration;

public class PlatformConfigurationManager : EditorWindow
{

    private int index;

    private List<PlatformData> platformDatas;

    private string[] platFormConfigs;

    private PlatformDataManager PlatformDataManager;

    internal void setPlatformDataMangager(PlatformDataManager platformDataManager)
    {
        this.PlatformDataManager = platformDataManager;
    }

    void init()
    {
        this.platFormConfigs = new string[100];
    }

    void OnEnable()
    {
        init();
    }

    void OnGUI()
    {
        ShowPlatformConfigurationManager();
        this.platFormConfigs = PlatformDataManager.getAllConfigurationNamesAsArray();
    }


    void ShowPlatformConfigurationManager()
    {
        
        GUILayout.BeginArea(new Rect(0, 0, 250, 250));
        GUILayout.Label("Platform Configuration:");
        index = EditorGUI.Popup(new Rect(0, 20, 250, 250), "Configurations:", index, platFormConfigs);

        if (GUI.Button(new Rect(0, 45, 50, 50 - 26), "Create"))
        {
            CreateConfiguration createConfigurationWindow =
            (CreateConfiguration)EditorWindow.GetWindow(typeof(CreateConfiguration), true,
            "Create Configuration");
            createConfigurationWindow.SetDataManager(this.PlatformDataManager);
            createConfigurationWindow.Show();
        }

        if (GUI.Button(new Rect(55, 45, 50, 50 - 26), "Edit"))
        {

            EditPlatformDataWindow editConfigurationWindow =
            (EditPlatformDataWindow)EditorWindow.GetWindow(typeof(EditPlatformDataWindow), true,
            "Edit Configuration");
            editConfigurationWindow.SetDataManager(this.PlatformDataManager);
            //editConfigurationWindow.SetPlatformDataToEdit(this.PlatformDataManager.getPlatformDataFromIndex(this.index));
            editConfigurationWindow.SetIndex(this.index);
            editConfigurationWindow.Show();
        }

        if (GUI.Button(new Rect(110, 45, 50, 50 - 26), "Save"))
        {
            this.Close();
        }

        if (GUI.Button(new Rect(0, 90, 50, 50-26), "Load"))
        {
            PrepareLoadConfigurationSetup(this.index);
        }

        if(GUI.Button(new Rect(0, 150, 50, 50 - 26), "Close"))
        {
            this.Close();
        }
        GUILayout.EndArea();
    }

    void PrepareLoadConfigurationSetup(int index)
    {
        PlatformData dataToLoad = new PlatformData();
        dataToLoad = PlatformDataManager.getPlatformDataFromIndex(index);
        LoadScene(dataToLoad.sceneName);
        prepareBuildSettings(dataToLoad.buildtarget, dataToLoad.buildtargetGroup);
        this.Close();

    }

    /// <summary>
    /// This Method opend the selected scene
    /// </summary>
    /// <param name="name"> scene name</param>
    void LoadScene(string sceneName)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
    }


    /// <summary>
    /// this method changes the build target based on the scene selected by the user
    /// </summary>
    /// <param name="buildTarget">unity buildtarget</param>
    /// <param name="buildTargetGroup">unity buildtargetgroup</param>
    void prepareBuildSettings(string buildTarget, string buildTargetGroup)
    {
        

        if (buildTarget == "Android" && buildTargetGroup == "Android")
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        }

        if (buildTarget == "StandaloneWindows64" && buildTargetGroup == "Standalone")
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        }
    }


    /// <summary>
    /// This method loads the selected assets
    /// </summary>
    /// <param name="viu">viu asset</param>
    /// <param name="gvr">gvr asset</param>
    /// <param name="wave">wave asset</param>
    void prepareAssets(bool viu, bool gvr, bool wave)
    {
        

    }
}
