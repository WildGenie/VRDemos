using UnityEngine;

/// <summary>
/// View für eine eine analoge Uhr
/// mit Stunden- und Minutenzeiger
/// </summary>
public class View : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    [Tooltip("Highlight Material setzen")]
    public Material highlightMaterial;
    /// <summary>
    /// Möchten wir Log-Ausgaben in der Callback-Funktion?
    /// </summary>
    [Tooltip("Sollen ausgaben auf der Konsole gemacht werden?")]
    public bool logOutput = false;

    /// <summary>
    /// Model Klasse
    /// </summary>
    protected Model Mod;
    /// <summary>
    /// Controller Klasse
    /// </summary>
    protected Controller Con;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enthält
    /// </summary>
    protected Material myMaterial;

    /// <summary>
    /// Wir fragen die Materialien ab und speichern die Farben als Instanzen
    /// der Klasse Color ab.
    /// </summary>
    protected Color originalColor, highlightColor;

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
    /// Callback, der in Awake() registriert 
    /// und in Update aufgerufen wird.
    /// 
    /// In dieser Funktion ist die Logik für das Wechseln der Farbe implementiert.
    /// Bemerkung: bei mehr als zwei Zuständen sollte man diese
    /// Funktionalität mit Hilfe einer finite state machine lösen!
    /// </summary>
    protected void Draw()
    {
        if (!Mod.status)
        {
            myMaterial.color = originalColor;
        }
        else
        {
            Debug.Log("In Highlight else");
            myMaterial.color = highlightColor;
        }
    }
}
