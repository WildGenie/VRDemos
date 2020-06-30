using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEditor;

/// <summary>
/// This class manages the loading and writing of configuration files and has specific getter methods
/// </summary>
public class SceneConfManager 
{
    //contains all scenes and their configurations
    private List<SceneData> sceneDatas;

    //contains all platform configurations
    private List<PlatformData> platformDatas;

    //contains the scene data to save
    public SceneConfig sceneConfig;
    
    //contains the loaded scene data from configuration file
    public SceneConfig loadConfig;

    /// <summary>
    /// constructor
    /// </summary>
    public SceneConfManager()
    {
        sceneDatas = new List<SceneData>();
        sceneConfig = new SceneConfig();
    }

    /// <summary>
    /// This method manages the user input and manages the scenes and the associated settings
    /// </summary>
    /// <param name="sceneData"> List of scene configurations</param>
    public void addSceneData(SceneData sceneData)
    {
        bool exists = false;
        foreach(SceneData scene in sceneDatas)
        {
            if(scene.sceneName == sceneData.sceneName)
            {
                exists = true;
            }
            else
            {
                exists = false;
            }
        }

        if(exists == false)
        {
            sceneDatas.Add(sceneData);
        }
        
    }

    /// <summary>
    /// This Method save the scene configurations in a xml file
    /// </summary>
    public void saveData()
    { 
        sceneConfig.sceneConfigs = sceneDatas;
        XmlSerializer serializer = new XmlSerializer(typeof(SceneConfig));
        using (FileStream stream = new FileStream(Application.dataPath +
            "/Buildsystem/StreamingFiles/XML/save_config.xml", FileMode.Create))
        {
            serializer.Serialize(stream, sceneConfig);
            stream.Close();
        }
        Debug.Log("Stream save Closed");
    } 

    /// <summary>
    /// this method loads the scene configuration file
    /// </summary>
    public void loadData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SceneConfig));
        using (FileStream stream = new FileStream(Application.dataPath +
            "/Buildsystem/StreamingFiles/XML/save_config.xml", FileMode.Open))
        {
            loadConfig = new SceneConfig();
            loadConfig = serializer.Deserialize(stream) as SceneConfig;
            stream.Close();
        }
        Debug.Log("Stream load Closed");
    }

    /// <summary>
    /// this method returns all scene names from the actual scene configuration list
    /// </summary>
    /// <returns>string[]</returns>
    public string[] getSceneDataNameArray()
    {
        List<string> sceneDataNames = new List<string>();

        foreach(SceneData sceneData in sceneDatas)
        {
            sceneDataNames.Add(sceneData.sceneName);
        }

        return sceneDataNames.ToArray();
    }

    /// <summary>
    /// this method return all scene names from the loaded scene configuration file
    /// </summary>
    /// <returns></returns>
    public string[] getSceneDataLoadAsArray()
    {
        loadData();
        sceneDatas = loadConfig.sceneConfigs;
        List<string> sceneDataNames = new List<string>();

        foreach (SceneData sceneData in sceneDatas)
        {
            sceneDataNames.Add(sceneData.sceneName);
        }

        return sceneDataNames.ToArray();
    }

    /// <summary>
    /// this method provides the setting of the scene based on the given name
    /// </summary>
    /// <param name="sceneName">scene name</param>
    /// <returns>the configuration to a scene</returns>
    public SceneData getSceneDataConfiguration(string sceneName)
    {
        SceneData sceneDataToSend = new SceneData();

        foreach (SceneData sceneData in sceneDatas)
        {
            if(sceneData.sceneName == sceneName)
            {
                sceneDataToSend = sceneData;
            }
        }
        return sceneDataToSend;
    }

    /// <summary>
    /// this method loads the build target from the existing project configuration
    /// </summary>
    /// <returns></returns>
    public String[] getAllBuildTargets()
    {
        loadData();
        sceneDatas = loadConfig.sceneConfigs;
        List<String> foundBuildTargets = new List<string>();

        foreach (SceneData sceneData in sceneDatas)
        {
            if(!foundBuildTargets.Contains(sceneData.buildtarget))
            {
                foundBuildTargets.Add(sceneData.buildtarget);
            }
        }

        return foundBuildTargets.ToArray();
    }

    /// <summary>
    /// print method for debug
    /// </summary>
    public void showSceneDataCount()
    {
        Debug.Log("saved scene configurations: "+sceneDatas.Count);

        foreach(SceneData scenedata in sceneDatas)
        {
            Debug.Log("SceneName: " + scenedata.sceneName + 
                ", BuildTarget:" + scenedata.buildtarget + 
                ", BuildTargetGroup: "+ scenedata.buildtargetGroup +
                ", GVR: " + scenedata.gvr + 
                ", VIU: " + scenedata.viu + 
                ", WaveSDK: " + scenedata.wavevr);
        }
    }
}

