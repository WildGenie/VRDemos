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
/// Diese "main hand" kann im Editor gewechselt werden.
/// </summary>
public class HighlighterMainHand : MonoBehaviour
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
    /// Zustand
    /// 
    /// Falls colorChanged=true, dann wird aktuell
    /// die Highlight-Farbe verwendet.
    /// </summary>
    private bool highlight = false;


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
    /// Update für das Material
    /// </summary>
    protected void Update()
    {
        UpdateHighlight();
    }
    /// <summary>
    /// Callback, der in Awake() registriert
    /// und in Update aufgerufen wird.
    /// 
    /// In dieser Funktion ist die Logik für das Wechseln der Farbe implementiert.
    /// </summary>
    private void UpdateHighlight()
    {
        if (!highlight)
        {
            if (logOutput)
                Debug.unityLogger.Log("Farbe wird auf Originalfarbe gesetzt!");
            myMaterial.color = originalColor;
        }
        else
        {
            if (logOutput)
                Debug.unityLogger.Log("Farbe wird auf Highlight gesetzt!");
            myMaterial.color = highlightColor;
        }
    }

    /// <summary>
    /// Listener für den Button erzeugen und
    /// den Callback registrieren.
    /// Da wir "nur" zwei Zustände verwenden
    /// setzen wir kein State-Pattern ein
    /// und toggeln den Wert einer logischen Variable.
    /// 
    /// Wir verwenden nur noch eine Hand, die "main hand".
    /// </summary>
    private void Awake()
    {
        ViveInput.AddListenerEx(mainHand,
                                theButton,
                                ButtonEventType.Down,
                                ChangeHighlightState);

        ViveInput.AddListenerEx(mainHand,
                                theButton,
                               ButtonEventType.Up,
                               ChangeHighlightState);
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
                                   ChangeHighlightState);

        ViveInput.RemoveListenerEx(mainHand,
                                   theButton,
                                   ButtonEventType.Up,
                                   ChangeHighlightState);
    }

    /// <summary>
    /// Wir toggeln den Zustand.
    /// </summary>
    private void ChangeHighlightState()
    {
        highlight = !highlight;
    }
}

