using HTC.UnityPlugin.Vive;
using UnityEngine;

/// InteractionTest
/// <summary>
/// Der Trigger-Button wird verwendet, um die Farbe eines Objekts
/// zu wechseln.
/// 
/// Die Farbe, auf die gewechselt werden
/// soll kann im Editor eingestellt werden.
/// </summary>
public class Interaction : MonoBehaviour
{
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
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.FullTrigger) || ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.FullTrigger))
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