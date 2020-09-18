using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// This class provides the configurations window and allows all users to edit and save his scene settings
/// </summary>
public class SceneConfigurationManager : EditorWindow
{
    //actually not needed => rdy to remove later if finally done
    public SceneConfig SceneConfig = new SceneConfig();
    
    //path of all scenes
    public string[] allScenesPath;
    
    //popup field index
    int index = 0;
    
    //provides a list of all scenes
    private List<string> allScenes = new List<string>();
    
    /// <summary>
    /// <see cref="SceneConfManager"/> SceneConfManager
    /// </summary>
    private SceneConfManager SceneConfManager;

    /// <summary>
    /// load all scenes in project
    /// </summary>
    void OnEnable()
    {
        initSceneConfiguration();
    }
    
    /// <summary>
    /// opens the window on start
    /// </summary>
    void OnGUI()
    {
        drawSceneConfiguration();
    }

    /// <summary>
    /// Display a window with all found scenes.
    /// User can Edit (add configurations to the scene) and Save (write configuration file).
    /// </summary>
    void drawSceneConfiguration()
    {
        
        index = EditorGUI.Popup(
            new Rect(10, 10, 250, 250),
            "Component:",
            index,
            allScenesPath);

        if (GUI.Button(new Rect(0, 25, 50, 50 - 26), "Edit"))
        {
            showSceneConfiguraiton(index);
        }

        if (GUI.Button(new Rect(55, 25, 50, 50 - 26), "Save"))
        {
            SceneConfManager.saveData();
            this.Close();
        }
    }


    /// <summary>
    /// openes the scene configuration window and give it a reference to the <see cref="SceneConfManager"/> SceneConfManager
    /// </summary>
    /// <param name="i">index popup</param>
    void showSceneConfiguraiton(int i)
    {
        string actualSceneName = allScenesPath[i];
        SceneConfiguration sceneConfigurationWindow = 
            (SceneConfiguration)EditorWindow.GetWindow(typeof(SceneConfiguration), true, "Scene Configuration");
        sceneConfigurationWindow.SetSceneName(actualSceneName);
        sceneConfigurationWindow.SetConfigManager(SceneConfManager);
        sceneConfigurationWindow.Show();
        SceneConfManager.showSceneDataCount();
    }

    /// <summary>
    /// Provides all scenes if they are marked in the build
    /// ToDO: shows other ways to do but actually not needed maybe later 
    /// </summary>
    public void initSceneConfiguration()
    {

        //for (int i = 0; i < SceneManager.sceneCount; i++)
        //{
        //allScenes.Add(SceneManager.GetSceneAt(i));
        //Debug.Log(SceneManager.sceneCount);
        //actualSceneName = SceneManager.GetSceneAt(i).name;
        //Debug.Log(actualSceneName);

        //}

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
            Debug.Log(sceneName);
            allScenes.Add(sceneName);
        }

        allScenesPath = allScenes.ToArray();


        //var sceneGUIDs = AssetDatabase.FindAssets("t:Scene");
        //var pathList = sceneGUIDs.Select(AssetDatabase.GUIDToAssetPath);

        //   foreach (var guid in sceneGUIDs)
        //   {
        //       var path = AssetDatabase.GUIDToAssetPath(guid);
        //       Debug.Log(path);
        //   }

    }

    /// <summary>
    /// Setter for <see cref="SceneConfManager"/> the SceneConfManager
    /// </summary>
    /// <param name="confManager"></param>
    public void setSceneConfManager(SceneConfManager confManager)
    {
        this.SceneConfManager = confManager;
    }

    /// <summary>
    /// Save Json configuration file
    /// actually not in use
    /// </summary>
    /// <param name="jsontosave"></param>
    void saveIntoJson(string jsontosave)
    {
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/SceneConfig.json", jsontosave);
    }

}
