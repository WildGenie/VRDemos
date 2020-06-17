using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wir erzeugen "Slalomstangen", um die Fortbewegung
/// zu testen.
/// 
/// Dazu verwenden wir ein PreFab "Pole", das im
/// Projekt abgelegt ist.
/// 
/// Der Abstand und die Breite des Parcours können
/// im Inspektor eingestellt werden.
/// </summary>
public class CreatePoles : MonoBehaviour
{
    /// <summary>
    /// Instanz des Prefabs für die Slalomstangen
    /// </summary>
    [Tooltip("Modell für die Säulen")]
    public GameObject polePrefab;
    /// <summary>
    /// Abstand der Säulen im Parcours.
    /// </summary>
    [Range(1.0f, 5.0f)]
    [Tooltip("Abstand der Säulen")]
    public float deltaZ = 2.0f;
    /// <summary>
    /// Länge des Parcours.
    /// 
    /// Es werden immer Paare von Säulen erzeugt.
    /// </summary>
    [Range(2, 20)]
    [Tooltip("Länge des Parcours")]
    public int parcoursLength = 10;

    /// <summary>
    /// Parcours erstellen.
    /// 
    /// Wir berechnen die Positionen der Slalomstangen
    /// und erzeugen so den Parcours.
    /// </summary>
    void Start()
    {
        // Die erste Position ist bei z=0 und x=-1
        Vector3 position = new Vector3(0.0f, 0.0f, 2.0f);
        Instantiate(polePrefab, position, Quaternion.identity);
        for (int i = 0; i < parcoursLength; i++)
        {
            position.z += deltaZ;
            Instantiate(polePrefab, position, Quaternion.identity);
        }
    }
}
