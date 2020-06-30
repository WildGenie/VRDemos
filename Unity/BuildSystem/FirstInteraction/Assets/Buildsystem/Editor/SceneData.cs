using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the data structure of the XML representation
/// </summary>
[Serializable]
public class SceneData
{
    public string sceneName;

    public bool viu;

    public bool gvr;

    public bool wavevr;

    public string buildtargetGroup;

    public string buildtarget;

 }