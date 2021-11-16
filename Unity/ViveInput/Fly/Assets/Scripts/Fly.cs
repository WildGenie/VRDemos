//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Fliegen 
/// 
/// Als Bewegungsrichtung verwenden wir die Orientierung
/// des Controllers, der durch die Basisklasse Movement festgelegt ist.
/// </summary>
public class Fly : SingleDirection
{

    /// <summary>
    /// Wir fragen den Vektor forward des Koordinatensystems des Geräts ab, das wir für die
    /// Richtungsdefinition verwenden und definieren damit die Bewegungsrichtung.
    /// </summary>
    protected override void ComputeMovingDirection()
    {
        movingDirection = orientationObject.transform.forward;
        movingDirection.Normalize();
    }
}
