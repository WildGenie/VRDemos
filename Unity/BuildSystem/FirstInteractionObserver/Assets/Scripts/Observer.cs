using UnityEngine;

/// <summary>
/// Abstrakte Basis-Klasse für einen Observer
/// im Observer-Pattern
/// <remarks>
/// In Gamma enthält die Basisklasse für den Observer
/// eine Funktion "Update". Da wir unsere Implementierung
/// in Unity verwenden werden ändern wir diesen Bezeichner in
/// *Refresh*.
/// </remarks>
/// </summary>
public class Observer : MonoBehaviour
{
    /// <summary>
    /// Refresh-Funktion.
    /// </summary>
    public virtual void Refresh() { }

    /// <summary>
    /// Das Subject
    /// </summary>
    protected Subject Mod;
}
