using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// This class contains the data structure of the XML representation
/// </summary>
[Serializable]
public class SceneConfig
{
    [XmlArray("SceneConfiguration")]
    public List<SceneData> sceneConfigs;
}
