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
    public Material highlightMaterial;
    /// <summary>
    /// Möchten wir Log-Ausgaben in der Callback-Funktion?
    /// </summary>
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
    private Material myMaterial;

    /// <summary>
    /// Wir fragen die Materialien ab und speichern die Farben als Instanzen
    /// der Klasse Color ab.
    /// </summary>
    private Color originalColor, highlightColor;

    /// <summary>
    /// In Awake erstellen wir den Controller und stellen
    /// die Verbindung zur Model-Klasse her.
    /// </summary>
    protected virtual void Awake()
    {
        Mod = new Model(false);
        Con = new Controller(Mod);
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
    /// Update wird frameabhängig aufgerufen.
    /// 
    /// Wir fragen mit DateTime.Now
    /// die aktuelle Uhrzeit ab,
    /// setzen diese Zeit und besetzen die 
    /// Variablen für die Ausgabe der Uhrzeiger.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            if (logOutput)
                Debug.Log(">>> Farbe wird gewechselt!");
            Con.ToggleHighlight();
        }

        ChangeColor();
    }


    /// <summary>
    /// Callback, der in Awake() registriert 
    /// und in Update aufgerufen wird.
    /// 
    /// In dieser Funktion ist die Logik für das Wechseln der Farbe implementiert.
    /// Bemerkung: bei mehr als zwei Zuständen sollte man diese
    /// Funktionalität mit Hilfe einer finite state machine lösen!
    /// </summary>
    private void ChangeColor()
    {
        if (!Mod.status)
        {
            myMaterial.color = originalColor;
        }
        else
        {
            myMaterial.color = highlightColor;
        }
    }
}
