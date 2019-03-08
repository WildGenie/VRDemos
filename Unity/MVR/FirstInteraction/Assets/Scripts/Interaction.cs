/// InteractionTest
/// <summary>
/// Entwickelt aus dem Sample im Users Guide von MiddleVR
/// 
/// Ein Wand-Button wird verwendet, um die Farbe eines Objekts
/// zu wechseln.
/// Der verwendete Wand-Button und auch die Farbe, auf die gewechselt werden
/// soll kann im Editor eingestellt werden.
///
/// Achtung: bei der Simulation mit Keyboard und Maus können wir nur Buttons
/// ab nr. 1 verwenden, wobei für den Wand-Button 1 die ESC-Taste verwendet werden muss.
/// 
/// Damit dies sowohl im Simulator als auch mit dem Vive-Controller läuft
/// ist als Default die Taste 2 eingestellt.
/// </summary>

using UnityEngine;
using MiddleVR_Unity3D;

public class Interaction : MonoBehaviour
{

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
                if (!colorChanged)
                {
                    Debug.unityLogger.Log("Farbe wird auf Highlight gesetzt!");
                    myMaterial.color = highlightColor;
                }
                else
                {
                    Debug.unityLogger.Log("Farbe wird auf Originalfarbe gesetzt!");
                    myMaterial.color = originalColor;
                }
                colorChanged = !colorChanged;

            }
        }
    }
}

