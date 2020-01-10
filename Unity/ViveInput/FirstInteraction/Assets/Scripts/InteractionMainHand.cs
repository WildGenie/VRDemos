using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Beispiel einer Interaktion mit dem Wand.
/// 
/// Wir lösen mit einem Button einen Event aus, um die Farbe eines Objekts
/// zu wechseln.
/// Der verwendete Wand-Button und auch die Farbe, auf die gewechselt werden
/// soll kann im Editor eingestellt werden.
/// 
/// Als Default verwenden wir den Wand in der rechten Hand. 
/// Diese "main hand" kann im Editor aber gewechselt werden.
/// </summary>
public class InteractionMainHand : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    public Material highlightMaterial;

    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist der Trigger der Controller.
    /// </summary>
    public ControllerButton theButton = ControllerButton.FullTrigger;

    /// <summary>
    /// Welche Hand wollen wir verwenden?
    /// 
    /// Default ist die rechte Hand.
    /// </summary>
    public HandRole mainHand = HandRole.RightHand;

    ///
    /// <summary>
    /// Möchten wir Log-Ausgaben in der Callback-Funktion?
    /// </summary>
    public bool logOutput = false;

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
        ViveInput.AddListenerEx(mainHand, 
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
        ViveInput.RemoveListenerEx(mainHand,
                                   theButton,
                                   ButtonEventType.Down,
                                   ColorChanger);
    }

    /// <summary>
    /// Abfragen des Materials des GameObjects, dem
    /// diese Klasse hinzugefügt wurde und
    /// setzen der Farbe für das Hervorheben.
    /// </summary>
    protected void Start()
    {
        myMaterial = GetComponent<Renderer>().material;

        originalColor = myMaterial.color;
        highlightColor = highlightMaterial.color;
    }

    /// <summary>
    /// Callback, der in Awake() registriert wird.
    /// 
    /// In dieser Funktion ist die Logik für das Wechseln der Farbe implementiert.
    /// </summary>
    private void ColorChanger()
    {
        if (!colorChanged)
        {
            if (logOutput)
                Debug.unityLogger.Log("Farbe wird auf Highlight gesetzt!");
            myMaterial.color = highlightColor;
        }
        else
        {
            if (logOutput)
                Debug.unityLogger.Log("Farbe wird auf Originalfarbe gesetzt!");
            myMaterial.color = originalColor;
        }
        colorChanged = !colorChanged;
    }
}

