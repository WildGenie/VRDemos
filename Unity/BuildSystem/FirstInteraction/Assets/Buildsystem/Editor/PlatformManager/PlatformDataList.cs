using System;
using System.Collections.Generic;
using System.Xml.Serialization;


[Serializable]
public class PlatformDataList 
{
    [XmlArray("PlatformConfiguration")]
    public List<PlatformData> platformDatas;
}
