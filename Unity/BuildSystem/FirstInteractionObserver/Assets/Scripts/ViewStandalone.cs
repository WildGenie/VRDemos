using UnityEngine;

/// <summary>
/// View für eine eine analoge Uhr
/// mit Stunden- und Minutenzeiger
/// </summary>
public class ViewStandalone : View
{
    /// <summary>
    /// In Awake erstellen wir den Controller und stellen
    /// die Verbindung zur Model-Klasse her.
    /// </summary>
    private void Awake()
    {
        Mod = new Model(false);
        Mod.Attach(this);
    }

    /// <summary>
    /// Wir verwenden die Tastatur für einen Event und
    /// stellen, falls er auftritt, die Farbe im Controller um.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            if (logOutput)
                Debug.Log(">>> Farbe wird gewechselt!");
            Mod.Toggle();
        }
    }
}
