//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Abstrakte Basisklasse für kontinuierliche Fortbewegung in immersiven Anwendungen,
/// abgeleitet von ContinousMovement.
/// 
/// Abgeleitete Klassen berechnen die bewegungsrichtung aus der Differenz der Position
/// von zwei GameObjects.
/// 
/// Eine Anwendung dafür sind insbesondere sogenannte "leaning models".
/// </summary>
public class HipAndHand : DifferentialDirection
{
    /// <summary>
    /// Wir nehmen ein GameObject fest, den Kopf, rechnen
    /// daraus die Hüfte und verwenden ein
    /// </summary>
    protected override void ComputeMovingDirection()
    {
        Vector3 startingPoint = startObject.transform.position;
        Vector3 endPoint = endObject.transform.position;

        movingDirection = endPoint - startingPoint;
        // Testhalber hier die Variable v für die Geschwindigkeit aus der Länge
        // des Verbindungsvektors nehmen, mit einem Clamp
        v = Mathf.Clamp(movingDirection.magnitude, 0.0f, vMax);
        Debug.Log("Bahngeschwindigkeit in ComputingMovingDirection" + v);

        movingDirection.Normalize();
    }
}

