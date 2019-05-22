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
using HTC.UnityPlugin.Vive;

public class Interaction : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    public Material highlightMaterial;

    // Einstellungen werden im Editor als Auswahl dargestellt,
    // aber die Auswahlen werden nicht immer korrekt übernommen.
    private ControllerButton theButton = ControllerButton.Trigger;

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
    /// Listener für den Button erzeugen und
    /// den Callback, hier OnTrigger, registrieren.
    /// </summary>
    private void Awake()
    {
        ViveInput.AddListenerEx(HandRole.RightHand, 
            theButton,
            ButtonEventType.Down, 
            ColorChanger);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand,
            theButton,
            ButtonEventType.Down,
            ColorChanger);
    }

    /// <summary>
    /// Abfragen der Farben beim Start der Anwendung
    /// </summary>
    protected void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;
        highlightColor = highlightMaterial.color;
    }

    /// <summary>
    /// In dieser Funktion ist die Logik für das Wechseln der Farbe implementiert.
    /// </summary>
    private void ColorChanger()
    {
        if (!colorChanged)
        {
            myMaterial.color = highlightColor;
        }
        else
        {
            myMaterial.color = originalColor;
        }
        colorChanged = !colorChanged;
    }
}

