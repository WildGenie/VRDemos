using System;


/// <summary>
/// This class is used to send the project information to the web server. 
/// </summary>
[Serializable]
public class UnityProjectFile
{
    //Unity project name
    public string projectName;

    //description about the project
    public string description;

    //git-hub url
    public string gitUrl;

    //used unityversion
    public string unityVersion;

    //used buildtarget in project
    public string buildTargetWindows;

    //used buildtarget in project
    public string buildTargetAndroid;

    //git status: in progress
    public bool inProgress;

    //git status: stable
    public bool stable;
}

