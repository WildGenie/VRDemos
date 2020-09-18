using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This class is used to read in and display project information.
/// Finally, these are transferred to the web server
/// </summary>
public class SyncServerWindow : EditorWindow
{
    //Unity project name
    private string projectName;
    
    //unity project description
    private string description;

    //git-hub url
    private string gitUrl;

    //used unity version
    private string unityVersion;

    //buildtarget
    private string btStandaloneWindows64;

    //buildtarget
    private string btAndroid;

    //git status: in Progress
    private bool toggleInProgress;

    //git status: stable
    private bool toggleStable;

    //contains all project informations
    private UnityProjectFile unityProjectFile;

    // <summary>
    /// <see cref="SceneConfManager"/> SceneConfManager
    /// </summary>
    SceneConfManager SceneConfManager;

    /// <summary>
    /// Setter for <see cref="SceneConfManager"/> SceneConfManager
    /// </summary>
    /// <param name="sceneConfManager"></param>
    public void SetConfigManager(SceneConfManager sceneConfManager)
    {
        this.SceneConfManager = sceneConfManager;
    }

    /// <summary>
    /// initialize and declare the variable
    /// </summary>
    private void OnEnable()
    {
        projectName = PlayerSettings.productName;
        description = "";
        gitUrl = "";
        btStandaloneWindows64 = "";
        btAndroid = "";
        unityVersion = Application.unityVersion;
        toggleInProgress = false;
        toggleStable = false;
        unityProjectFile = new UnityProjectFile();
    }

    /// <summary>
    /// calls the window content
    /// </summary>
    private void OnGUI()
    {
        DrawSyncSettings();
    }

    /// <summary>
    /// presents the project information and allows the user to review and edit the output
    /// </summary>
    private void DrawSyncSettings()
    {
        GUILayout.BeginArea(new Rect(10, 10, 280, 280));
        GUILayout.Label("Project Data: ");
        projectName = EditorGUILayout.TextField("Project Name:", projectName);
        description = EditorGUILayout.TextField("Description: ", description);
        gitUrl = EditorGUILayout.TextField("Git-Url: ", gitUrl);
        unityVersion = EditorGUILayout.TextField("Unity Version: ", unityVersion);
        GUILayout.EndArea();
        
        GUILayout.BeginArea(new Rect(10, 150, 300, 300));
        GUILayout.Label("Build Configuration: ");
        setBuildTargets(SceneConfManager.getAllBuildTargets());
        btStandaloneWindows64 = EditorGUILayout.TextField("Buildtarget: ", btStandaloneWindows64);
        btAndroid = EditorGUILayout.TextField("Buildtarget: ", btAndroid);
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(10, 220, 280, 280));
        GUILayout.Label("Git Status: ");
        toggleInProgress = GUILayout.Toggle(toggleInProgress, "inProgress");
        toggleStable = GUILayout.Toggle(toggleStable, "stable");
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(10, 320, 280, 280));
        GUILayout.Label("Send to Buildsystem: ");
        if (GUILayout.Button("Send"))
        {
            unityProjectFile.projectName = projectName;
            unityProjectFile.description = description;
            unityProjectFile.gitUrl = gitUrl;
            unityProjectFile.unityVersion = unityVersion;
            unityProjectFile.buildTargetWindows = btStandaloneWindows64;
            unityProjectFile.buildTargetAndroid = btAndroid;
            unityProjectFile.inProgress = toggleInProgress;
            unityProjectFile.stable = toggleStable;

            string json = JsonUtility.ToJson(unityProjectFile);
            Debug.Log(json);
            sendToBuildSystem(json);
        }

        GUILayout.EndArea();

    }

    /// <summary>
    /// this method starts the coroutine to send the project information
    /// </summary>
    /// <param name="jsonString"></param>
    private void sendToBuildSystem(string jsonString)
    {
        EditorCoroutineUtility.StartCoroutine(Upload(jsonString), this);
    }

    /// <summary>
    /// this method sends the project information to the webserver
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    IEnumerator Upload(String jsonString)
    {
        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:8080/api/unity/data", jsonString))
        {
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "application/json");
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError )
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    /// <summary>
    /// loads the buildtargets used within the project
    /// </summary>
    /// <param name="buildtargets"></param>
    private void setBuildTargets(string[] buildtargets)
    {
        foreach(string buildtarget in buildtargets)
        {
            if(buildtarget.Equals("StandaloneWindows64"))
            {
                btStandaloneWindows64 = buildtarget;
            }
            if(buildtarget.Equals("Android"))
            {
                btAndroid = buildtarget;
            }
        }
    }
}
