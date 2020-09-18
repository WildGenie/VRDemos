using System.Xml.Serialization;



[System.Serializable]
public class PlatformDataArray
{
    [XmlArray("PlatformConfiguration")]
    public PlatformData[] platformDatas;
}
