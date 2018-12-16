/// InteractionTest
/// <summary>
/// Entwickelt aus dem Sample aus dem Users Guide von MiddleVR
/// 
/// Ein Wand-Button wird verwendet, um die Farbe eines Objekts
/// zu wechseln.
/// 
/// Der verwendete Wand-Button und auch die Farbe, auf die gewechselt werden
/// soll kann im Editor eingestellt werden.
/// </summary>

using UnityEngine;
using MiddleVR_Unity3D;

public class InteractionTest : MonoBehaviour {

    /// <summary>
    /// Welcher Wandbutton soll verwendet werden? Default ist die Nummer 2, 
    /// das entspricht der Taste 1 auf dem Keyboard in der Default-Konfiguration.
    /// </summary>
    public uint buttonNbr = 2;

    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    public Material highlightMaterial;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enthält
    /// </summary>
    private Material myMaterial;

    /// <summary>
    /// Wir fragen die Materialien ab und speichern die Farben als Instanzen
    /// der Klasse Color ab.
    /// </summary>
    private Color originalColor, highlightColor;
    /// <summary>
    /// Wurde die Farbe bereits gewechselt?
    /// </summary>
    private bool colorChanged = false;

    /// <summary>
    /// Abfragen der Farben beim Start der Anwendung
    /// </summary>
    protected void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;
        highlightColor = highlightMaterial.color;
    }

    protected void Update()
    {
        changeColor();
    }

    /// <summary>
    /// In dieser Funktion ist die Logik für das Wechseln der Farbe implementiert.
    /// </summary>
    private void changeColor()
    {
        bool toggled;
        var deviceMgr = MiddleVR.VRDeviceMgr;
        if (deviceMgr != null)
        {
            toggled = deviceMgr.IsWandButtonToggled(buttonNbr);

            if (toggled)
            {
                Debug.unityLogger.Log("Wand Button 2 toggle!");
                if (!colorChanged)
                {
                    Debug.unityLogger.Log("Farbe wird auf Highlight gesetzt!");
                    myMaterial.color = highlightColor;
                }
                else
                {
                    Debug.unityLogger.Log("Farbe wird auf Original gesetzt!");
                    myMaterial.color = originalColor;
                }
                colorChanged = !colorChanged;
                Debug.unityLogger.Log("colorChanged gewechselt");
                
            }
        }
    }
}
